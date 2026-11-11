using PaaPinden.API.Data;
using PaaPinden.API.DTOs;
using PaaPinden.API.Models;

namespace PaaPinden.API.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this ApplicationUser user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Username = user.UserName ?? string.Empty, 
                Name = user.Name,
                FeedPosts = user.FeedPosts,
                ForumPosts = user.ForumPosts,
                Followers = user.Followers?.Select(x => x.User.ToSummaryDTO()).ToList() ?? new List<UserSummaryDTO>(),
                Following = user.Following?.Select(x => x.FollowedUser.ToSummaryDTO()).ToList() ?? new List<UserSummaryDTO>()
            };
        }
        
        public static UserSummaryDTO ToSummaryDTO(this ApplicationUser user)
        {
            return new UserSummaryDTO()
            {
                Id = user.Id,
                Username =  user.UserName ?? string.Empty,
                Name = user.Name,
            };
        }

        public static ApplicationUser ToModel(this UserDTO userDTO)
        {
            return new ApplicationUser()
            {
                Id = userDTO.Id,
                UserName = userDTO.Username,
                Name = userDTO.Name
            };
        }
    }
}