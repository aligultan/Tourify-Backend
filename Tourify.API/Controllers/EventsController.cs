using Microsoft.AspNetCore.Mvc;
using Tourify.Core.Entities;
using Tourify.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<EventsController> _logger;

    public EventsController(
        IEventService eventService,
        ILogger<EventsController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<IEnumerable<Event>>> GetUpcomingEvents(
        [FromQuery] int limit = 50)
    {
        try
        {
            var events = await _eventService.GetUpcomingEvents(limit);
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting upcoming events");
            return StatusCode(500, "An error occurred while fetching events");
        }
    }

    [HttpGet("city/{city}")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsByCity(
        string city,
        [FromQuery] int limit = 50)
    {
        try
        {
            var events = await _eventService.GetEventsByCity(city, limit);
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting events for city {city}");
            return StatusCode(500, $"An error occurred while fetching events for city {city}");
        }
    }


    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsByCategory(
        string category,
        [FromQuery] int limit = 50)
    {
        try
        {
            var events = await _eventService.GetEventsByCategory(category, limit);
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting events for category {category}");
            return StatusCode(500, $"An error occurred while fetching events for category {category}");
        }
    }

    [HttpGet("price-range")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsByPriceRange(
        [FromQuery] decimal minPrice,
        [FromQuery] decimal maxPrice,
        [FromQuery] int limit = 50)
    {
        try
        {
            var events = await _eventService.GetEventsByPriceRange(minPrice, maxPrice, limit);
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting events by price range");
            return StatusCode(500, "An error occurred while fetching events");
        }
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] int limit = 50)
    {
        try
        {
            var events = await _eventService.GetEventsByDateRange(startDate, endDate, limit);
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting events by date range");
            return StatusCode(500, "An error occurred while fetching events");
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Event>>> SearchEvents(
        [FromQuery] string searchTerm,
        [FromQuery] int limit = 50)
    {
        try
        {
            var events = await _eventService.SearchEvents(searchTerm, limit);
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching events");
            return StatusCode(500, "An error occurred while searching events");
        }
    }

    [HttpGet("ordered-by-date")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsOrderByDate(
        [FromQuery] bool ascending = true,
        [FromQuery] int limit = 50)
    {
        try
        {
            var events = await _eventService.GetEventsOrderByDate(ascending, limit);
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting events ordered by date");
            return StatusCode(500, "An error occurred while fetching events");
        }
    }
}