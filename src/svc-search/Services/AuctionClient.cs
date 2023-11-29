using MongoDB.Entities;

namespace svc_search.Services;

public class AuctionClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AuctionClient(HttpClient client, IConfiguration config)
    {
        this._httpClient = client;
        this._config = config;
    }

    public async Task<List<Item>> GetItemsForDb()
    {
        var lastUpdated = await DB.Find<Item, string>()
            .Sort(x => x.Descending(x => x.UpdatedAt))
            .Project(x => x.UpdatedAt.ToString())
            .ExecuteFirstAsync();

        return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] 
            + "/api/auctions?date=" + lastUpdated);
    }

}
