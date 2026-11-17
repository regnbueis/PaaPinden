using PaaPinden.API.Data;

namespace PaaPinden.API.Contracts
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByIdAsync(string userId);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string userId);
        Task FollowAsync(string userId, string followedUserId);
        Task UnfollowAsync(string userId, string followedUserId);
    }
}