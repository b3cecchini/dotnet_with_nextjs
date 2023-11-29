namespace svc_search.Data;

using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using svc_search.Services;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("SearchDB", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        using var scope = app.Services.CreateScope();

        var client = scope.ServiceProvider.GetRequiredService<AuctionClient>();

        var items = await client.GetItemsForDb();

        Console.WriteLine(items.Count + " items return from Auction Service");

        if (items.Count > 0) await DB.SaveAsync(items);
    }
}
