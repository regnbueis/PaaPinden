using PaaPinden.API.Models;

namespace PaaPinden.API.Contracts
{
    public interface IFeedPostRepository
    {
        Task CreateAsync(FeedPost feedPost);
        Task DeleteAsync(string feedPostId);
        Task UpdateAsync(FeedPost feedPost);
        Task<List<FeedPost>> GetAllAsync();
        Task<FeedPost?> GetByIdAsync(string feedPostId);
        Task<List<FeedPost>> GetAllByUserIdAsync(string userId);

    }
}
