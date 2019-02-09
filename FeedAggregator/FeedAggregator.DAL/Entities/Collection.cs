using System.Collections.Generic;

namespace FeedAggregator.DAL.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Feed> Feeds { get; set; }
    }
}
