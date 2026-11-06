using Microsoft.AspNetCore.Identity;
using PaaPinden.API.Models;
namespace PaaPinden.API.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public List<FeedPost> FeedPosts { get; set; }
    public List<ForumPost> ForumPosts { get; set; }
    public List<Follow> Followers { get; set; }
    public List<Follow> Following { get; set; }
}
