using FeedAggregator.BLL.Dtos;
using System.Collections.Generic;

namespace FeedAggregator.BLL.Services
{
    public interface IFeedManagerService
    {
        FeedDto Create(FeedDto feedDto, int userId);
        IEnumerable<FeedDto> GetAll(int collectionId, int userId);
        FeedDto GetById(int collectionId, int feedId, int userId);
        void Update(FeedDto feedDto, int userId);
        void Delete(int collectionId, int feedId, int userId);
        void Dispose();
    }
}
