using Microsoft.EntityFrameworkCore;
using PaaPinden.API.Contracts;
using PaaPinden.API.Data;
using PaaPinden.API.Models;

namespace PaaPinden.API.Persistence
{
    public class FeedPostRepository : IFeedPostRepository
    {
        private readonly Context _context;

        public FeedPostRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateAsync(FeedPost feedPost)
        {
            feedPost.Id = Guid.NewGuid().ToString();
            _context.FeedPosts.Add(feedPost);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string feedPostId)
        {
            FeedPost? feedPostToDelete = await _context.FeedPosts.FindAsync(feedPostId);
            if (feedPostToDelete == null) return;

            _context.FeedPosts.Remove(feedPostToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FeedPost>> GetAllAsync()
        {
            return await _context.FeedPosts
                .AsNoTracking()
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<List<FeedPost>> GetAllByUserIdAsync(string userId)
        {
            return await _context.FeedPosts
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<FeedPost?> GetByIdAsync(string feedPostId)
        {
            return await _context.FeedPosts
                .Where(x => x.Id == feedPostId)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(FeedPost feedPost)
        {
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
