using Microsoft.EntityFrameworkCore;
using PaaPinden.Contracts;
using PaaPinden.Data;
using PaaPinden.Models;

namespace PaaPinden.Persistence
{
    public class ForumPostRepository : IForumPostRepository
    {

        private readonly IDbContextFactory<PaaPindenContext> _factory;

        public ForumPostRepository(IDbContextFactory<PaaPindenContext> factory)
        {
            _factory = factory;
        }

        public async Task CreateAsync(ForumPost forumPost)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            _context.ForumPosts.Add(forumPost);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string forumPostid)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            ForumPost? forumPostToDelete = await _context.ForumPosts.FindAsync(forumPostid);
            if (forumPostToDelete == null) return;

            _context.ForumPosts.Remove(forumPostToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForumPost forumPost)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            ForumPost? forumPostToUpdate = await _context.ForumPosts.FindAsync(forumPost);
            if (forumPostToUpdate == null) return;

            forumPostToUpdate.Id = forumPost.Id;
            await _context.SaveChangesAsync();
        }

        public async Task<List<ForumPost>> GetAllAsync()
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            return await _context.ForumPosts
                .AsNoTracking()
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<ForumPost?> GetByIdAsync(String id)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            return await _context.ForumPosts
                .Include(x => x.Id)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ForumPost>> GetAllByUserIdAsync(String userId)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            return await _context.ForumPosts
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<ForumPost>> GetAllByTopicAsync(ForumPostTopic forumPostTopic)
        {
            using PaaPindenContext _context = _factory.CreateDbContext();

            return await _context.ForumPosts
                .Where(x => x.Topic == forumPostTopic)
                .ToListAsync();
        }
    }
}
