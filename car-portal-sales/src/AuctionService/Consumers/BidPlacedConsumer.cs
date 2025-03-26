using AuctionService.Data;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly AuctionDbContext _auctionDb;

    public BidPlacedConsumer(AuctionDbContext auctionDb)
    {
        _auctionDb = auctionDb;
    }
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("--> Consuming bid placed: " + context.Message.AuctionId);

        var auction = await _auctionDb.Auctions.FindAsync(context.Message.AuctionId);

        if(auction!.CurrentHighBid == null || context.Message.BidStatus.Contains("Accepted") && context.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = context.Message.Amount;
            await _auctionDb.SaveChangesAsync();
        }
    }
}
