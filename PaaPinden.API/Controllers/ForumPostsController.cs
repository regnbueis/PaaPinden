using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaaPinden.API.Data;
using PaaPinden.API.Models;
using PaaPinden.API.Persistence;
using System.Linq.Expressions;


namespace PaaPinden.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForumPostsController : ControllerBase
    {
        private readonly IForumPostRepository forumPostRepository;

        private readonly UserManager<ApplicationUser> userManager;

        public ForumPostsController(IForumPostRepository forumPostRepository, UserManager<ApplicationUser> userManager)
        {
            this.forumPostRepository = forumPostRepository;
            this.userManager = userManager;
            
            
        }

        [Route("/api/admin/[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            var posts = await forumPostRepository.GetAllAsync();
            return Ok(posts);
            /*try
            {
                string? userId = userManager.GetUserId(User);
                if (userId == null) return BadRequest();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }*/

        }
        [HttpGet("{forumPostId}")]
        public async Task<IActionResult> GetByIdAsync(int forumPostId)
        {
            try
            {
                string? userId = userManager.GetUserId(User);
                if (userId == null) return BadRequest();

                ForumPost? forumPost = await forumPostRepository.GetByIdAsync(userId);
                if (forumPost == null) return NotFound();

                if (forumPost.UserId != userId)
                {
                    if (!User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }

            }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllByUserIdAsync()
        {
            try
            {
                string? userId = userManager.GetUserId(User);
                if (userId == null) return BadRequest();

                List<ForumPost> forumPosts = await forumPostRepository.GetAllByUserIdAsync(userId);
                return Ok(forumPosts);
            }
            catch
            { 
                return BadRequest(); 
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync([FromBody] ForumPost forumPost)
        {
            string? userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            forumPost.Id = Guid.NewGuid().ToString();
            forumPost.UserId = userId;

            await forumPostRepository.CreateAsync(forumPost);
            return Ok();
            /*try
            {
                string? userId = userManager.GetUserId(User);
                if (userId == null) return BadRequest();

                await forumPostRepository.CreateAsync();
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            catch
            {
                return BadRequest();
            }*/
        }
        [HttpPatch("{forumPostId}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ForumPost forumPost)
        {
            await forumPostRepository.UpdateAsync(forumPost);
            return NoContent();
            /*try
            {
                var userId = userManager.GetUserId(User);
                if (userId == null) return BadRequest();

                ForumPost? forumPost = await forumPostRepository.GetByIdAsync(forumPostId);
                if (forumPost == null) return NotFound();

                if (forumPost.UserId != userId)
                {
                    if (!User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }
                }

                await forumPostRepository.UpdateAsync(forumPostId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }*/
        }
        [HttpDelete("{forumPostId}")]
        public async Task<IActionResult> DeleteAsync(string forumPostId)
        {
            await forumPostRepository.DeleteAsync(forumPostId);
            return NoContent();

            /*try
            {
                string? userId = userManager.GetUserId(User);
                if (userId == null) return BadRequest();

                ForumPost? forumPost = await forumPostRepository.GetByIdAsync(forumPostId);
                if (forumPost == null) return NotFound();

                if(forumPost.UserId != userId)
                {
                    if (!User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }
                }
                await forumPostRepository.DeleteAsync(forumPost);
                return Ok();
                
            }
            catch 
            { 
                return BadRequest(); 
            }*/
        }
    }
}
