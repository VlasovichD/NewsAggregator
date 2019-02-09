using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleClient.Models
{
    public class FeedItemContentModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PublishingDateString { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string Author { get; set; }
        public string Id { get; set; }
        public ICollection<string> Categories { get; set; }
        public string Content { get; set; }
    }
}
