using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase 
{
    private readonly IAuctionRepository _repo;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    public AuctionsController(IAuctionRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _repo = repo;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDTO>>> GetAllAuctions(string? date) 
    {
        return await _repo.GetAuctionsAsync(date);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDTO>> GetAuctionById(Guid id) 
    {
        var auction = await _repo.GetAuctionByIdAsync(id);

        if(auction == null) return NotFound();
        return auction;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDTO>> CreateAuction(CreateAuctionDTO auctionDTO) 
    {
        var auction = _mapper.Map<Auction>(auctionDTO);

        // TODO : add current user as seller
        auction.Seller = User.Identity!.Name;

        _repo.AddAuction(auction);

        var newAuction = _mapper.Map<AuctionDTO>(auction);

        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));

        var result = await _repo.SaveChangesAsync();

        if(!result)
        {
            return BadRequest("Could not save changes to DB");
        }

        return CreatedAtAction(nameof(GetAuctionById), new {auction.Id}, newAuction);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDTO updateAuctionDTO) 
    {
        var auction = await _repo.GetAuctionEntityById(id);
        if(auction == null) return NotFound();

        // TODO : check seller == username
        if(auction.Seller != User.Identity!.Name) return Forbid();

        auction.Item!.Make = updateAuctionDTO.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDTO.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDTO.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDTO.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDTO.Year ?? auction.Item.Year;

        await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

        var result = await _repo.SaveChangesAsync();
        if(!result) return BadRequest("Could not save changes to DB");
        return Ok();
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id) 
    {
        var auction = await _repo.GetAuctionEntityById(id);
        if(auction == null) return NotFound();

        // TODO : check seller == username
        if(auction.Seller != User.Identity!.Name) return Forbid();

        _repo.RemoveAuction(auction);

        await _publishEndpoint.Publish<AuctionDeleted>(new
        { 
            Id = auction.Id.ToString() 
        });

        var result = await _repo.SaveChangesAsync();
        if(!result) return BadRequest("Could not save changes to DB");
        return Ok();
    }
}