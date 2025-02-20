using centuras.org.Models;
using Microsoft.AspNetCore.Identity;
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
            new Category { Id = 1, Name = "One-Shot"},
            new Category { Id = 2, Name = "Adventure"},
            new Category { Id = 3, Name = "Campaign"}
            );
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "98a350dd-ca1f-4339-bc59-6a61547f534a",
                Name = "Member",
                NormalizedName = "MEMBER"
            },
            new IdentityRole
            {
                Id = "300b643c-ed3e-405f-8e2c-358b7df75f9c",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Id = "88c66549-aabb-4e28-a8cb-c37b0303d932",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });

        PasswordHasher<IdentityUser> hahser = new PasswordHasher<IdentityUser>();
        builder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0",
                Email = "tpscott@centuras.org",
                NormalizedEmail = "TPSCOTT@CENTURAS.ORG",
                UserName = "tpscott",
                NormalizedUserName = "TPSCOTT",
                PasswordHash= hahser.HashPassword(null, "OpenUp63!"),
                EmailConfirmed = true
            });

        builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    //Same as admin role
                    RoleId = "88c66549-aabb-4e28-a8cb-c37b0303d932",
                    //Same as the IdentityUser from above
                    UserId = "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0"
                }
            );
        base.OnModelCreating(builder);
    }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Game> Games { get; set; }

}
