
using System.ComponentModel.DataAnnotations.Schema;

namespace centuras.org.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? CoverImage { get; set; }
        [NotMapped]
        public IFormFile? RawImage { get; set; }
        public string? ZipPath { get; set; } = null!;
        [NotMapped]
        public IFormFile? ZipFile { get; set; } = null!;
    }
}
