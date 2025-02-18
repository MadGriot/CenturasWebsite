using centuras.org.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace centuras.org.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "News"},
            new Category { Id = 2, Name = "Pathfinder"},
            new Category { Id = 3, Name = "Games"}
            );

        base.OnModelCreating(builder);
    }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }

}
