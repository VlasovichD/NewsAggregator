using AutoMapper;
using FeedAggregator.BLL.Dtos;
using FeedAggregator.BLL.Infrastructure;
using FeedAggregator.DAL.Entities;
using FeedAggregator.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace FeedAggregator.BLL.Services
{
    public class FeedManagerService : IFeedManagerService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public FeedManagerService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public FeedDto Create(FeedDto feedDto, int userId)
        {
            if (string.IsNullOrWhiteSpace(feedDto.Name))
                throw new ValidationException("Feed name is empty");

            var collection = _database.Collections.GetById(feedDto.CollectionId);

            if (collection == null || collection.UserId != userId)
                throw new ValidationException("Collection not found");

            var feeds = _database.Feeds.GetAll(f => f.CollectionId == feedDto.CollectionId).ToList();

            if (feeds.Any(f => f.Name == feedDto.Name))
                throw new ValidationException($"Feed name \"{feedDto.Name}\" is already exists");

            var feed = _mapper.Map<Feed>(feedDto);

            _database.Feeds.Create(feed);
            _database.Save();

            return _mapper.Map<FeedDto>(feed);
        }

        public IEnumerable<FeedDto> GetAll(int collectionId, int userId)
        {
            var collection = _database.Collections.GetById(collectionId);

            if (collection == null || collection.UserId != userId)
                throw new ValidationException("Collection not found");

            var feeds = _database.Feeds.GetAll(f => f.CollectionId == collectionId).ToList();

            if (feeds == null)
                throw new ValidationException("List of feeds is empty");

            return _mapper.Map<List<FeedDto>>(feeds);
        }

        public FeedDto GetById(int collectionId, int feedId, int userId)
        {

            var feed = _database.Feeds.GetById(feedId);

            if (feed == null || feed.CollectionId != collectionId)
                throw new ValidationException("Feed not found");

            var collection = _database.Collections.GetById(collectionId);

            if (collection == null || collection.UserId != userId)
                throw new ValidationException("Feed not found");

            return _mapper.Map<FeedDto>(feed);
        }

        public void Update(FeedDto feedDto, int userId)
        {
            if (string.IsNullOrWhiteSpace(feedDto.Name))
                throw new ValidationException("Feed name is empty");

            var currentFeed = _database.Feeds.GetById(feedDto.Id);

            if (currentFeed == null)
                throw new ValidationException("Feed not found");

            var collection = _database.Collections.GetById(feedDto.CollectionId);

            if (collection == null || collection.UserId != userId)
                throw new ValidationException("Feed not found");

            if (feedDto.Name != currentFeed.Name)
            {
                // name of feed has changed so check if the new name of feed is already taken
                var userCollectonFeeds = _database.Feeds.GetAll(f => f.CollectionId == feedDto.CollectionId).ToList();

                if (userCollectonFeeds.Any(f => f.Name == feedDto.Name))
                    throw new ValidationException($"Feed name \"{feedDto.Name}\" is already exists");
            }

            // update feed properties
            currentFeed.Name = feedDto.Name;
            currentFeed.Description = feedDto.Description;
            currentFeed.Url = feedDto.Url;

            _database.Save();
        }

        public void Delete(int collectionId, int feedId, int userId)
        {
            var feed = _database.Feeds.GetById(feedId);

            if (feed == null || feed.CollectionId != collectionId)
                throw new ValidationException("Feed not found");

            var collection = _database.Collections.GetById(collectionId);

            if (collection == null || collection.UserId != userId)
                throw new ValidationException("Feed not found");

            _database.Feeds.Delete(feedId);
            _database.Save();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
