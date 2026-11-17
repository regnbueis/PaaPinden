using Microsoft.AspNetCore.Identity;
using PaaPinden.Models;
namespace PaaPinden.Data;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public override string? UserName
    {
        get => base.UserName;
        set
        {
            base.UserName = value;

            if (string.IsNullOrEmpty(Name))
            {
                Name = value?.Split("@")?[0] ?? string.Empty;
            }
        }
    }

    public byte[]? Image { get; set; }
    public string? ImageFormat { get; set; }
    public List<FeedPost> FeedPosts { get; set; }
    public List<ForumPost> ForumPosts { get; set; }
    public List<Follow> Followers { get; set; }
    public List<Follow> Following { get; set; }
}
