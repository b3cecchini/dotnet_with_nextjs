using svc_auction.DTO;
using svc_auction.Models;
using AutoMapper;
using System.Diagnostics.Contracts;
using Contracts;

namespace svc_auction.Utils;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Models.Auction, DTO.Auction>().IncludeMembers(x => x.Item);
        CreateMap<Item, DTO.Auction>();
        CreateMap<AuctionCreateRequest, Models.Auction>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s));
        CreateMap<AuctionCreateRequest, Item>();
        CreateMap<DTO.Auction, AuctionCreated>();
        CreateMap<Models.Auction, AuctionUpdated>().IncludeMembers(a => a.Item);
        CreateMap<Item, AuctionUpdated>();
    }
}