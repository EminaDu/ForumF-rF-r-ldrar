using ForumFörFöräldrar.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ForumFörFöräldrar.Models;


namespace ForumFörFöräldrar.Data;

public class ForumFörFöräldrarContext : IdentityDbContext<ForumFörFöräldrarUserT>
{
    public ForumFörFöräldrarContext(DbContextOptions<ForumFörFöräldrarContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Comment> Comment { get; set; } = default!;
    public DbSet<Models.ForumMainCategory> ForumMainCategory { get; set; } = default!;
    public DbSet<Models.ForumSubCategory> ForumSubCategory { get; set; } = default!;
    public DbSet<Models.Message> Message { get; set; } = default!;
    public DbSet<Models.Post> Post { get; set; } = default!;
    public DbSet<Models.Report> Reports { get; set; } = default!;
}
