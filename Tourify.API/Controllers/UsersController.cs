using Microsoft.AspNetCore.Mvc;
using Tourify.Core.Entities;
using Tourify.Core.Interfaces;

namespace Tourify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{userId}/favorites")]
        public async Task<ActionResult<IReadOnlyList<Place>>> GetUserFavorites(int userId)
        {
            var favorites = await _userRepository.GetUserFavorites(userId);
            return Ok(favorites);
        }

        [HttpPost("{userId}/favorites/{placeId}")]
        public async Task<ActionResult> AddToFavorites(int userId, int placeId)
        {
            var result = await _userRepository.AddToFavorites(userId, placeId);
            if (!result)
                return BadRequest("Place is already in favorites");
            return Ok();
        }

        [HttpPut("{userId}/favorites/{placeId}/visited")]
        public async Task<ActionResult> MarkAsVisited(int userId, int placeId)
        {
            var result = await _userRepository.MarkAsVisited(userId, placeId);
            if (!result)
                return NotFound("Favorite not found");
            return Ok();
        }
    }
}