using PaaPinden.API.DTOs;
using PaaPinden.API.Models;

namespace PaaPinden.API.Mappers
{
    public static class FeedPostMapper
    {
        public static FeedPostDTO ToDTO(this FeedPost feedPost)
        {
            return new FeedPostDTO
            {
                Id = feedPost.Id,
                Media = feedPost.Media,
                MediaType = feedPost.MediaType,
                Description = feedPost.Description,
                Link = feedPost.Link,
                UserId = feedPost.UserId
            };
        }
        public static FeedPost ToModel(this FeedPostDTO feedPostDTO)
        {
            return new FeedPost
            {
                Id = feedPostDTO.Id,
                Media = feedPostDTO.Media,
                MediaType = feedPostDTO.MediaType,
                Description = feedPostDTO.Description,
                Link = feedPostDTO.Link,
                UserId = feedPostDTO.UserId
            };
        }
    }
}
