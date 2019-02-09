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
    public class CollectionManagerService : ICollectionManagerService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public CollectionManagerService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public CollectionDto Create(CollectionDto collectionDto)
        {
            if (string.IsNullOrWhiteSpace(collectionDto.Name))
                throw new ValidationException("Collection name is empty");

            var collections = _database.Collections.GetAll(c => c.UserId == collectionDto.UserId).ToList();

            if (collections.Any(c => c.Name == collectionDto.Name))
                throw new ValidationException($"Collection name \"{collectionDto.Name}\" is already exists");

            var collection = _mapper.Map<Collection>(collectionDto);

            _database.Collections.Create(collection);
            _database.Save();

            return _mapper.Map<CollectionDto>(collection);
        }

        public IEnumerable<CollectionDto> GetAll(int userId)
        {
            var collections = _database.Collections.GetAll(c => c.UserId == userId).ToList();

            if (collections == null)
                throw new ValidationException("List of collections is empty");

            return _mapper.Map<List<CollectionDto>>(collections);
        }

        public CollectionDto GetById(int collectionId, int userId)
        {
            var collection = _database.Collections.GetById(collectionId);

            if (collection == null || collection.UserId != userId)
                throw new ValidationException("Collection not found");

            return _mapper.Map<CollectionDto>(collection);
        }

        public void Update(CollectionDto collectionDto)
        {
            if (string.IsNullOrWhiteSpace(collectionDto.Name))
                throw new ValidationException("Collection name is empty");

            var currentCollection = _database.Collections.GetById(collectionDto.Id);

            if (currentCollection == null || collectionDto.UserId != currentCollection.UserId)
                throw new ValidationException("Collection not found");

            if (collectionDto.Name != currentCollection.Name)
            {
                // name of collection has changed so check if the new name of collection is already taken
                var userCollections = _database.Collections.GetAll(c => c.UserId == collectionDto.UserId).ToList();

                if (userCollections.Any(c => c.Name == collectionDto.Name))
                    throw new ValidationException($"Collection name \"{collectionDto.Name}\" is already exists");
            }

            // update collection properties
            currentCollection.Name = collectionDto.Name;
            currentCollection.Description = collectionDto.Description;

            _database.Save();
        }

        public void Delete(int collectionId, int userId)
        {
            var collection = _database.Collections.GetById(collectionId);

            if (collection == null || collection.UserId != userId)
            {
                throw new ValidationException("Collection not found");
            }

            _database.Collections.Delete(collectionId);
            _database.Save();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
