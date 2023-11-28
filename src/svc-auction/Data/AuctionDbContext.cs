using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using svc_auction.Models;
namespace svc_auction.Data;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions options) : base (options) { }

    public DbSet<Auction> Auctions { get; set; }
}
