using PaaPinden.API.Contracts;
using PaaPinden.API.Data;

namespace PaaPinden.API.Models
{
    public class ForumPost : IPost
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Byte[] Media { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public string Link { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
