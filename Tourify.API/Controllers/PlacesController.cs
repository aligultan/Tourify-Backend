using Microsoft.AspNetCore.Mvc;
using Tourify.Core.Entities;
using Tourify.Core.Interfaces;

namespace Tourify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceRepository _placeRepository;

        public PlacesController(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        [HttpGet("city/{city}")]
        public async Task<ActionResult<IReadOnlyList<Place>>> GetPlacesByCity(string city)
        {
            var places = await _placeRepository.GetPlacesByCity(city);
            return Ok(places);
        }

        [HttpGet("city/{city}/type/{type}")]
        public async Task<ActionResult<IReadOnlyList<Place>>> GetPlacesByType(string city, PlaceType type)
        {
            var places = await _placeRepository.GetPlacesByType(city, type);
            return Ok(places);
        }

        [HttpGet("city/{city}/top/{count}")]
        public async Task<ActionResult<IReadOnlyList<Place>>> GetTopRatedPlaces(string city, int count)
        {
            var places = await _placeRepository.GetTopRatedPlaces(city, count);
            return Ok(places);
        }
    }
}