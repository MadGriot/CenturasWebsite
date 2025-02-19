using System.ComponentModel.DataAnnotations.Schema;

namespace centuras.org.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte[] CoverImage { get; set; } = null!;
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
