using PaaPinden.API.Data;

namespace PaaPinden.API.DTOs
{
    public class FeedPostDTO
    {
        public string Id { get; set; }
        public Byte[] Media { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string UserId { get; set; }
    }
}
