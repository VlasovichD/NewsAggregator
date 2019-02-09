namespace FeedAggregator.BLL.Dtos
{
    public class FeedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int CollectionId { get; set; }
    }
}
