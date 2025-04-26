using AuctionService.Data;
using AuctionService.Entities;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionDbContext _auctionDb;

    public AuctionFinishedConsumer(AuctionDbContext auctionDb)
    {
        _auctionDb = auctionDb;
    }
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
        Console.WriteLine("--> Consuming Auction Finished: " + context.Message.AuctionId);

        var auction = await _auctionDb.Auctions.FindAsync(context.Message.AuctionId);

        if(context.Message.ItemSold)
        {
            auction!.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount;
        }

        auction!.Status = auction.SoldAmount > auction.ReservePrice ? Status.Finished : Status.ReserveNotMet;

        await _auctionDb.SaveChangesAsync();
    }
}
