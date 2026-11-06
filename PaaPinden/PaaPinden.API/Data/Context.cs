using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaaPinden.API.Models;
using System.Reflection.Emit;

namespace PaaPinden.API.Data
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FeedPost> FeedPosts { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<Follow> Follows { get; set; }

        public Context(DbContextOptions options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FeedPost>()
                .HasOne<ApplicationUser>(feedPost => feedPost.User)
                .WithMany(user => user.FeedPosts)
                .HasForeignKey(feedPost => feedPost.UserId);

            builder.Entity<ForumPost>()
                .HasOne<ApplicationUser>(forumPost => forumPost.User)
                .WithMany(user => user.ForumPosts)
                .HasForeignKey(forumPost => forumPost.UserId);

            builder.Entity<Follow>()
                .HasKey(follow => new { follow.UserId, follow.FollowedUserId });

            builder.Entity<Follow>()
                .HasOne<ApplicationUser>(follow => follow.User)
                .WithMany(user => user.Following)
                .HasForeignKey(follow => follow.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Follow>()
                .HasOne<ApplicationUser>(follow => follow.FollowedUser)
                .WithMany(user => user.Followers)
                .HasForeignKey(follow => follow.FollowedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }


    }
}

