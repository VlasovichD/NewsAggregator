using AutoMapper;
using FeedAggregator.BLL.Dtos;
using FeedAggregator.WebAPI.Models;

namespace FeedAggregator.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();
            CreateMap<CollectionModel, CollectionDto>();
            CreateMap<CollectionDto, CollectionModel>();
            CreateMap<FeedModel, FeedDto>();
            CreateMap<FeedDto, FeedModel>();
        }
    }
}
