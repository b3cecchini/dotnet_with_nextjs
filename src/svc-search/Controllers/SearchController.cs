using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using svc_search.DTO;

namespace svc_search.Controllers;

[ApiController]
[Route("/api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> SearchItems([FromQuery] SearchParams searchParams)
    {
        ActionResult response = null;

        var query = DB.PagedSearch<Item, Item>();

        if(!string.IsNullOrWhiteSpace(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        query = searchParams.OrderBy switch
        {
            "make" => query.Sort(x => x.Ascending(a => a.Make)),
            "new" => query.Sort(x => x.Ascending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd)),
        };

        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x => x.AuctionEnd > DateTime.UtcNow && x.AuctionEnd < DateTime.UtcNow.AddHours(6)),
            _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow),
        };

        if(!string.IsNullOrWhiteSpace(searchParams.Seller))
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }

        if(!string.IsNullOrWhiteSpace(searchParams.Winner))
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }
        
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);
        var result = await query.ExecuteAsync();

        response = Ok(new {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });

        return response;

    }


}
