using PaaPinden.Contracts;
using PaaPinden.Data;
using System.ComponentModel.DataAnnotations;

namespace PaaPinden.Models
{
    public class FeedPost : IPost
    {
        public string Id { get; set; }

        [Required]
        public Byte[] Media { get; set; }

        [Required]
        public string MediaType { get; set; }

        [Required]
        public string Description { get; set; }
        
        public int Likes { get; set; }
        public string Link { get; set; }

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
