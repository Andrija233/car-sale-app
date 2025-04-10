using System;
using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidsController : ControllerBase
{
    private readonly IMapper _mapper;
    public BidsController(IMapper mapper)
    {
        _mapper = mapper;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BidDTO>> PlaceBid(string auctionId, int amount)
    {
        Console.WriteLine(auctionId);
        var auction = await DB.Find<Auction>().OneAsync(auctionId);
        Console.WriteLine(auction);

        if (auction == null)
        {
            // TODO
            return BadRequest("Cannot accept bids on this auction at this time");
        }

        if (auction.Seller == User.Identity?.Name)
        {
            return BadRequest("You cannot bid on your own item");
        }

        if (User.Identity?.Name == null) return Unauthorized();

        var bid = new Bid()
        {
            Amount = amount,
            AuctionId = auctionId,
            Bidder = User.Identity.Name
        };

        if (auction.AuctionEnd < DateTime.UtcNow)
        {
            bid.BidStatus = BidStatus.Finished;
        }
        else
        {
            var highBid = await DB.Find<Bid>()
                .Match(a => a.AuctionId == auctionId)
                .Sort(b => b.Descending(x => x.Amount))
                .ExecuteFirstAsync();

            if (highBid != null && amount > highBid.Amount || highBid == null)
            {
                bid.BidStatus = amount > auction.ReservePrice
                    ? BidStatus.Accepted
                    : BidStatus.AcceptedBelowReserve;
            }

            if (highBid != null && bid.Amount <= highBid.Amount)
            {
                bid.BidStatus = BidStatus.TooLow;
            }
        }

        await DB.SaveAsync(bid);

        return Ok(_mapper.Map<BidDTO>(bid));
    }

    [HttpGet("{auctionId}")]
    public async Task<ActionResult<List<BidDTO>>> GetBidsForAuction(string auctionId)
    {
        var bids = await DB.Find<Bid>()
            .Match(a => a.AuctionId == auctionId)
            .Sort(b => b.Descending(a => a.BidTime))
            .ExecuteAsync();

        return bids.Select(_mapper.Map<BidDTO>).ToList();
    }
}