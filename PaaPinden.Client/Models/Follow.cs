using PaaPinden.Data;

namespace PaaPinden.Models
{
    public class Follow
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FollowedUserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ApplicationUser? FollowedUser { get; set; }
    }
}
