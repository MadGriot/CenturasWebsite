namespace centuras.org.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        List<Post>? Posts { get; set; }
    }
}
