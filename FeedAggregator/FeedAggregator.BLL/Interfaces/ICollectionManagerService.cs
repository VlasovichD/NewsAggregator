using FeedAggregator.BLL.Dtos;
using System.Collections.Generic;

namespace FeedAggregator.BLL.Services
{
    public interface ICollectionManagerService
    {
        CollectionDto Create(CollectionDto collectionDto);
        IEnumerable<CollectionDto> GetAll(int userId);
        CollectionDto GetById(int collectionId, int userId);
        void Update(CollectionDto collection);
        void Delete(int collectionId, int userId);
        void Dispose();
    }
}
