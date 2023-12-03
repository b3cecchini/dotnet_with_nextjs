using Contracts;
using MassTransit;
using svc_auction.Data;

namespace svc_auction.Consumers;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionDbContext _dbContext;
    public AuctionFinishedConsumer(AuctionDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
        Console.WriteLine(" ==> Consuming Auction Finished");
        var auction = await _dbContext.Auctions.FindAsync(context.Message.AuctionId);

        if (context.Message.ItemSold)
        {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount;
        }

        auction.Status = auction.SoldAmount > auction.ReservePrice ? Models.Status.Closed : Models.Status.ReserveNotMet;

        await _dbContext.SaveChangesAsync();
    }
}
