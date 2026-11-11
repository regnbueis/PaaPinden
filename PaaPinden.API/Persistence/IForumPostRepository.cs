using PaaPinden.API.Models;

namespace PaaPinden.API.Persistence
{
    public interface IForumPostRepository
    {
        Task CreateAsync(ForumPost forumpost);
        Task DeleteAsync(string forumPostId);
        Task UpdateAsync(ForumPost forumPost);
        Task<List<ForumPost>> GetAllAsync();
        Task<ForumPost> GetByIdAsync(string forumPostId);
        Task<List<ForumPost>> GetAllByUserIdAsync(string userId);
    }
}
