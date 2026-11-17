using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PaaPinden.Contracts;
using PaaPinden.Data;
using PaaPinden.Models;

namespace PaaPinden.Persistence
{
    public class FeedPostRepository : IFeedPostRepository
    {
        private readonly IDbContextFactory<PaaPindenContext> _factory;

        public FeedPostRepository(IDbContextFactory<PaaPindenContext> factory)
        {
            _factory = factory;
        }

        public async Task CreateAsync(FeedPost feedPost)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            feedPost.Id = Guid.NewGuid().ToString();
            _context.FeedPosts.Add(feedPost);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string feedPostId)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            FeedPost? feedPostToDelete = await _context.FeedPosts.FindAsync(feedPostId);
            if (feedPostToDelete == null) return;

            _context.FeedPosts.Remove(feedPostToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FeedPost>> GetAllAsync()
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            return await _context.FeedPosts
                .AsNoTracking()
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<List<FeedPost>> GetAllByUserIdAsync(string userId)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            return await _context.FeedPosts
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<FeedPost?> GetByIdAsync(string feedPostId)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            return await _context.FeedPosts
                .Where(x => x.Id == feedPostId)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(FeedPost feedPost)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            FeedPost? feedPostToUpdate = await _context.FeedPosts.FindAsync(feedPost.Id);
            if (feedPostToUpdate == null) return;

            feedPostToUpdate.Media = feedPost.Media;
            feedPostToUpdate.MediaType = feedPost.MediaType;
            feedPostToUpdate.Description = feedPost.Description;
            feedPostToUpdate.Link = feedPost.Link;

            await _context.SaveChangesAsync();
        }
    }
}
