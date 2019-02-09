using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedAggregator.WebAPI.Models
{
    public class FeedContentModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string LastUpdatedDateString { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<FeedItemContentModel> Items { get; set; }
    }
}
