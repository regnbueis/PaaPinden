using Microsoft.AspNetCore.Identity;
using PaaPinden.Models;
namespace PaaPinden.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public byte[] Image { get; set; }
    public string ImageFormat { get; set; }
    public List<FeedPost> FeedPosts { get; set; }
    public List<ForumPost> ForumPosts { get; set; }
    public List<Follow> Followers { get; set; }
    public List<Follow> Following { get; set; }
}
