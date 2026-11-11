using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaaPinden.API.Contracts;
using PaaPinden.API.Data;
using PaaPinden.API.DTOs;
using PaaPinden.API.Mappers;
using PaaPinden.API.Models;

namespace PaaPinden.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedPostsController : ControllerBase
    {
        private readonly IFeedPostRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public FeedPostsController(IFeedPostRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<FeedPost> feedPosts = await _repository.GetAllAsync();
                List<FeedPostDTO> feedPostDTOs = feedPosts.Select(x => x.ToDTO()).ToList();

                return Ok(feedPostDTOs);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllByUserIdAsync(string userId)
        {
            try
            {
                List<FeedPost> feedPosts = await _repository.GetAllByUserIdAsync(userId);
                List<FeedPostDTO> feedPostDTOs = feedPosts.Select(x => x.ToDTO()).ToList();

                return Ok(feedPostDTOs);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{feedPostId}")]
        public async Task<IActionResult> GetByIdAsync(string feedPostId)
        {
            try
            {
                FeedPost? feedPost = await _repository.GetByIdAsync(feedPostId);
                if (feedPost == null) return NotFound();

                FeedPostDTO feedPostDTO = feedPost.ToDTO();

                return Ok(feedPostDTO);
            }
            catch
            {
                return BadRequest();
            }
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] FeedPostDTO feedPostDTO)
        {
            try
            {
                //string? userId = _userManager.GetUserId(User);
                //if (userId == null) return BadRequest();

                FeedPost feedPost = feedPostDTO.ToModel();
                await _repository.CreateAsync(feedPost);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        //[Authorize]
        [HttpPatch("{feedPostId}")]
        public async Task<IActionResult> UpdateAsync(string feedPostId, [FromBody] FeedPostDTO feedPostDTO)
        {
            try
            {
                //string? userId = _userManager.GetUserId(User);
                //if (userId == null) return BadRequest();

                FeedPost? feedPost = await _repository.GetByIdAsync(feedPostId);
                if (feedPost == null) return NotFound();

                //if (_userManager.GetUserId(User) != feedPost.UserId)
                //{
                //        return Forbid();
                //}

                await _repository.UpdateAsync(feedPostDTO.ToModel());
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string feedPostId)
        {
            try
            {
                FeedPost feedPost = await _repository.GetByIdAsync(feedPostId);
                if (feedPost == null) return BadRequest();

                //if (_userManager.GetUserId(User) != feedPost.UserId)
                //{
                //    if (!User.IsInRole("Admin"))
                //    {
                //        return Forbid();
                //    }
                //}

                await _repository.DeleteAsync(feedPostId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
