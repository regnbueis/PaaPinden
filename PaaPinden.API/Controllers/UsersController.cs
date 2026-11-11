using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaaPinden.API.Contracts;
using PaaPinden.API.Data;
using PaaPinden.API.DTOs;
using PaaPinden.API.Mappers;

namespace PaaPinden.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _repository;
        
        public UsersController(IUserRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<ApplicationUser> users = await _repository.GetAllAsync();
                List<UserSummaryDTO> userDTOs = users
                    .Select(x => x.ToSummaryDTO())
                    .ToList();

                return Ok(userDTOs);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(string userId)
        {
            try
            {
                ApplicationUser? user = await _repository.GetByIdAsync(userId);
                if (user == null) return NotFound();

                UserDTO userDTO = user.ToDTO();
                return Ok(userDTO);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [Authorize]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateAsync(string userId, [FromBody] UserUpdateDTO userUpdateDto)
        {
            try
            {
                string? authorizedUserId = _userManager.GetUserId(User);
                if (authorizedUserId != userId) return Forbid();
                
                UserDTO userDTO = new UserDTO()
                {
                    Id = userId,
                    Name = userUpdateDto.Name
                };
                
                ApplicationUser user = userDTO.ToModel();
                await _repository.UpdateAsync(user);
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            try
            {
                string? authorizedUserId = _userManager.GetUserId(User);
                if (authorizedUserId != userId) return Forbid();
                
                await _repository.DeleteAsync(userId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("{userId}/following")]
        public async Task<IActionResult> FollowAsync(string userId, [FromBody] FollowDTO followDto)
        {
            try
            {
                string? authorizedUserId = _userManager.GetUserId(User);
                if (authorizedUserId != userId) return Forbid();
                
                await _repository.FollowAsync(userId, followDto.FollowedUserId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [Authorize]
        [HttpDelete("{userId}/following/{followedUserId}")]
        public async Task<IActionResult> UnfollowAsync(string userId, string followedUserId)
        {
            try
            {
                string? authorizedUserId = _userManager.GetUserId(User);
                if (authorizedUserId != userId) return Forbid();
                
                await _repository.UnfollowAsync(userId, followedUserId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}