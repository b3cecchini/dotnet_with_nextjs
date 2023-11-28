using System.Collections.Frozen;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using svc_auction.Data;
using svc_auction.DTO;
using svc_auction.Models;
using svc_auction.Utils;

namespace svc_auction.Controllers;
[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    private readonly AuctionDbContext _context;

    private readonly IMapper _mapper;

    public AuctionsController(AuctionDbContext context, IMapper mapper) 
    {
        this._context = context;
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<DTO.Auction>>> GetAllAuctions()
    {
        var dbAuctions = await _context.Auctions.Include(x => x.Item).ToListAsync();


        var dtoResponse = AuctionUtil.GetAuctionDto(dbAuctions);

        return this.Ok(dtoResponse);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<DTO.Auction>> GetAuctionByIdAsync([FromRoute] Guid id)
    {
        ActionResult response;

        var auction = await _context.Auctions.Include(b => b.Item).Where(b => b.Id == id).FirstOrDefaultAsync();

        if(auction == null)
        {
            return this.NotFound();
        }
        else
        {
            var dtoResponse = AuctionUtil.GetAuctionDto(new List<Models.Auction>{ auction });

            response = Ok(dtoResponse.FirstOrDefault());
        }

        return response;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuctionAsync(AuctionCreateRequest request)
    {
        ActionResult response = null;

        if(request == null)
        {
            response = this.BadRequest("request object is not valid");
        }
        else
        {

            var auction = this._mapper.Map<Models.Auction>(request);

            auction.Seller = "TestSeller";

            _context.Auctions.Add(auction);

            var result = await _context.SaveChangesAsync() > 0;

            if(!result) response = this.BadRequest("Unable to save changes to the database");

            response = this.Created( new Uri($"http://localhost:7001/api/auctions/{auction.Id}"), this._mapper.Map<DTO.Auction>(auction));
        }

        return response;
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAuctionAsync([FromRoute]Guid id, AuctionUpdateRequest updateRequest)
    {
        ActionResult response = null;

        if(id == Guid.Empty)
        {
            response = this.BadRequest("Auction Id is invalid");
        }
        else
        {
            try
            {
                var auction = await _context.Auctions.Include(b => b.Item).Where(b => b.Id == id).FirstOrDefaultAsync();

                if (auction == null) response = this.NotFound();

                auction.Item.Make = updateRequest.Make ?? auction.Item.Make;
                auction.Item.Model = updateRequest.Model ?? auction.Item.Model;
                auction.Item.Color = updateRequest.Color ?? auction.Item.Color;
                auction.Item.Mileage = updateRequest.Mileage ?? auction.Item.Mileage;
                auction.Item.Year = updateRequest.Year ?? auction.Item.Year;

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) response = this.BadRequest("Unable to save changes to the database");

                var responseObj = new AuctionUpdateResponse{
                    Id = auction.Id.ToString(),
                    Color = auction.Item.Color,
                    Make = auction.Item.Make,
                    Mileage = auction.Item.Mileage,
                    Model = auction.Item.Model,
                    Year = auction.Item.Year
                };

                response = this.Ok(responseObj);
            }
            catch (Exception e)
            {
                response = this.StatusCode((int)HttpStatusCode.InternalServerError, e.Message + " - " + e.InnerException.Message);
            }
            
        }
        return response;
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAuctionByIdAsync([FromRoute] Guid id)
    {
        ActionResult response;

        var auction = await _context.Auctions.FindAsync(id);

        if(auction == null)
        {
            response = this.NotFound();
        }
        else
        {
            _context.Auctions.Remove(auction);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                response = this.StatusCode((int)HttpStatusCode.InternalServerError, "Unable to delete auction: " + id);
            }
            else 
            {
                response = this.NoContent();
            }
        }

        return response;
    }
}
