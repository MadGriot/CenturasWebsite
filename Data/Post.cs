namespace centuras.org.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string? FeaturedImageUrl { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
