using System.ComponentModel.DataAnnotations;
using PaaPinden.API.Models;

namespace PaaPinden.API.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        
        public string Name { get; set; }
        
        public List<FeedPost> FeedPosts { get; set; }
        public List<ForumPost> ForumPosts { get; set; }
        public List<UserSummaryDTO> Followers { get; set; }
        public List<UserSummaryDTO> Following { get; set; }
    }

    public class UserSummaryDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        
        public string Name { get; set; }
    }
    
    public class UserUpdateDTO 
    {
        [MinLength(3)]
        public string Name { get; set; }
    }
}