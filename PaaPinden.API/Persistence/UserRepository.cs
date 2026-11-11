using Microsoft.EntityFrameworkCore;
using PaaPinden.API.Contracts;
using PaaPinden.API.Data;
using PaaPinden.API.Models;

namespace PaaPinden.API.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }
        
        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users
                .ToListAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string userId)
        {
            return await _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.FeedPosts)
                .Include(x => x.ForumPosts)
                .Include(x => x.Followers)
                    .ThenInclude(x => x.User)
                .Include(x => x.Following)
                    .ThenInclude(x => x.FollowedUser)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            ApplicationUser? userToUpdate = await _context.Users.FindAsync(user.Id);
            if (userToUpdate == null) return;
            
            userToUpdate.Name = user.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId)
        {
            ApplicationUser? user = await _context.Users.FindAsync(userId);
            if (user == null) return;
         
            List<Follow> follows = await _context.Follows
                .Where(x => x.UserId == user.Id || x.FollowedUserId == user.Id)
                .ToListAsync();
            
            _context.Follows.RemoveRange(follows);
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task FollowAsync(string userId, string followedUserId)
        {
            Follow? existingFollow = await _context.Follows
                .Where(x => x.UserId == userId &&  x.FollowedUserId == followedUserId)
                .FirstOrDefaultAsync();
            
            if (existingFollow != null) return;

            Follow follow = new Follow()
            {
                UserId = userId,
                FollowedUserId = followedUserId
            };
            
            await _context.Follows.AddAsync(follow);
            await _context.SaveChangesAsync();
        }

        public async Task UnfollowAsync(string userId, string followedUserId)
        {
            Follow? follow = await _context.Follows
                .Where(x => x.UserId == userId &&  x.FollowedUserId == followedUserId)
                .FirstOrDefaultAsync();
            
            if (follow == null) return;
            
            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
        }
    }
}