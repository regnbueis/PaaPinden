using Microsoft.EntityFrameworkCore;
using PaaPinden.API.Data;
using PaaPinden.API.Models;

namespace PaaPinden.API.Persistence
{
    public class ForumPostRepository : IForumPostRepository
    {
        
        private readonly Context _context;

        public ForumPostRepository(Context context)
        {
            _context = context;
        }

        
        public async Task CreateAsync(ForumPost forumPost)
        {
            _context.ForumPosts.Add(forumPost);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string forumPostid) 
        {
            ForumPost? forumPostToDelete = await _context.ForumPosts.FindAsync(forumPostid);
            if (forumPostToDelete == null) return;

            _context.ForumPosts.Remove(forumPostToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForumPost forumPost) 
        {
            ForumPost? forumPostToUpdate = await _context.ForumPosts.FindAsync(forumPost);
            if (forumPostToUpdate == null) return;

            forumPostToUpdate.Id = forumPost.Id;
            await _context.SaveChangesAsync();
        }

        public async Task<List<ForumPost>> GetAllAsync()
        {
            return await _context.ForumPosts
                .AsNoTracking()
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<ForumPost?> GetByIdAsync(String id)
        {
            return await _context.ForumPosts
                .Include(x => x.Id)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ForumPost>> GetAllByUserIdAsync(String userId)
        {
            return await _context.ForumPosts
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        
    }
}
