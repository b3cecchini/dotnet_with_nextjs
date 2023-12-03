using Contracts;
using MassTransit;
using svc_auction.Data;

namespace svc_auction.Consumers;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly AuctionDbContext _dbContext;
    public BidPlacedConsumer(AuctionDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine(" ==> Consuming Bid Placed");
        var auction = await _dbContext.Auctions.FindAsync(context.Message.AuctionId);

        if (auction.CurrentHighBid == null 
            || context.Message.BidStatus.Contains("Accepted")
            && context.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = context.Message.Amount;
            await _dbContext.SaveChangesAsync();
        }
    }
}
