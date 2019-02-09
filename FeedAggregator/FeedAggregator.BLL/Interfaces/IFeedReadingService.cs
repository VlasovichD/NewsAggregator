using CodeHollow.FeedReader;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedAggregator.BLL.Services
{
    public interface IFeedReadingService
    {
        IEnumerable<Feed> ReadAll(int collectionId, int userId);
        Feed ReadById(int collectionId, int feedId, int userId);
        Feed ReadByUrl(string url);
    }
}
