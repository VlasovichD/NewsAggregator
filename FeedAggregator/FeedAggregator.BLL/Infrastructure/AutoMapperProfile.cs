using AutoMapper;
using FeedAggregator.BLL.Dtos;
using FeedAggregator.DAL.Entities;

namespace FeedAggregator.BLL.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Collection, CollectionDto>();
            CreateMap<CollectionDto, Collection>();
            CreateMap<Feed, FeedDto>();
            CreateMap<FeedDto, Feed>();
        }
    }
}
