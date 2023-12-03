using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;


namespace svc_search.Consumers;

public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
{
    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        Console.WriteLine(" ==> Consuming auction deleted: " + context.Message.Id);

        var result = await DB.DeleteAsync<Item>(context.Message.Id);

        if (!result.IsAcknowledged) throw new MessageException(typeof(AuctionDeleted), "Unable to update mongoDb");
    }
}
