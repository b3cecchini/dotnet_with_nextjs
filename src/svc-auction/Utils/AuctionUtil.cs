using svc_auction.DTO;
using svc_auction.Models;

namespace svc_auction.Utils;
public class AuctionUtil
{
    public static List<DTO.Auction> GetAuctionDto(List<Models.Auction> auctions)
    {
        List<DTO.Auction> response = new ();
        auctions.ForEach(auction => {
            response.Add(
                new DTO.Auction
                {
                    Id = auction.Id,
                    CreatedAt = auction.CreatedAt,
                    UpdatedAt = auction.UpdatedAt,
                    AuctionEnd = auction.AuctionEnd,
                    Seller = auction.Seller,
                    Winner = auction.Winner,
                    Make = auction.Item.Make,
                    Model = auction.Item.Model,
                    Year = auction.Item.Year,
                    Color = auction.Item.Color,
                    Mileage = auction.Item.Mileage,
                    ImageUrl = auction.Item.ImageUrl,
                    Status = auction.Status.ToString(),
                    ReservePrice = auction.ReservePrice,
                    SoldAmount = auction.SoldAmount,
                    CurrentHighBid = auction.CurrentHighBid
                }
            );
        });
        return response;
    }

    // public static List<Models.Auction> GetAuctionModel(List<AuctionCreateRequest> auctions)
    // {
    //     List<Models.Auction> response = new ();
    //     auctions.ForEach(auction => {
    //         response.Add(
    //             new Models.Auction
    //             {
    //                 ReservePrice = auction.ReservePrice,
    //                 Imag
    //                 SoldAmount = auction.SoldAmount,
    //                 CurrentHighBid = auction.CurrentHighBid,
    //                 Item = new Item{

    //                 }
    //             }
    //         );
    //     });
    //     return response;
    // }

    /*
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
    public int Year { get; set; }
    public int ReservePrice { get; set; }
    public string ImageUrl { get; set; }
    public DateTime AuctionEnd { get; set; }

    to: 

    public Guid Id { get; set; }
    public int ReservePrice { get; set; } = 0;
    public string Seller { get; set; } 
    public string Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime AuctionEnd { get; set; }
    public Status Status { get; set; } = Status.Live;
    public Item Item { get; set; }
*/
}
