using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Tourify.Core.Entities;
using Tourify.Core.Interfaces;
using Tourify.Core.Models.Ticketmaster;

namespace Tourify.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public EventService(
            ILogger<EventService> logger,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiKey = configuration["Ticketmaster:ApiKey"];
            _httpClient.BaseAddress = new Uri("https://app.ticketmaster.com/discovery/v2/");
        }

        public async Task<IEnumerable<Event>> GetUpcomingEvents(int limit = 50)
        {
            try
            {
                var url = $"events.json?apikey={_apiKey}&size={limit}&sort=date,asc";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Ticketmaster API Response: {content}");

                var ticketmasterResponse = JsonSerializer.Deserialize<TicketmasterResponse>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (ticketmasterResponse?._embedded?.events == null || !ticketmasterResponse._embedded.events.Any())
                {
                    _logger.LogWarning("No events found in Ticketmaster response");
                    return new List<Event>();
                }

                return ticketmasterResponse._embedded.events.Select(MapToEvent).ToList();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling Ticketmaster API");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Ticketmaster response");
                throw;
            }
        }

        public async Task<Event> GetEventById(string eventId)
        {
            try
            {
                var url = $"events/{eventId}.json?apikey={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var ticketmasterEvent = JsonSerializer.Deserialize<TicketmasterEvent>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (ticketmasterEvent == null)
                {
                    throw new Exception($"Event with ID {eventId} not found");
                }

                return MapToEvent(ticketmasterEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting event with ID {eventId}");
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsByCity(string city, int limit = 50)
        {
            try
            {
                var url = $"events.json?apikey={_apiKey}&city={city}&size={limit}&sort=date,asc";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var ticketmasterResponse = JsonSerializer.Deserialize<TicketmasterResponse>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (ticketmasterResponse?._embedded?.events == null)
                {
                    return new List<Event>();
                }

                return ticketmasterResponse._embedded.events.Select(MapToEvent).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting events for city {city}");
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsByCategory(string category, int limit = 50)
        {
            try
            {
                // Ticketmaster API'de segmentName parametresi kullanılıyor kategori için
                var url = $"events.json?apikey={_apiKey}&segmentName={category}&size={limit}&sort=date,asc";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var ticketmasterResponse = JsonSerializer.Deserialize<TicketmasterResponse>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (ticketmasterResponse?._embedded?.events == null)
                {
                    return new List<Event>();
                }

                return ticketmasterResponse._embedded.events.Select(MapToEvent).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting events for category {category}");
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsByPriceRange(decimal minPrice, decimal maxPrice, int limit = 50)
        {
            try
            {
                // Önce tüm events'leri çekip client-side filtering yapalım
                // Ticketmaster API'de fiyat filtresi yok
                var events = await GetUpcomingEvents(100); // Daha fazla event çekip filtreleyeceğiz
                return events.Where(e =>
                        (e.MinPrice >= minPrice || e.MinPrice == null) &&
                        (e.MaxPrice <= maxPrice || e.MaxPrice == null))
                    .Take(limit)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting events for price range {minPrice}-{maxPrice}");
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsByDateRange(DateTime startDate, DateTime endDate, int limit = 50)
        {
            try
            {
                var startDateStr = startDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                var endDateStr = endDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                var url = $"events.json?apikey={_apiKey}&startDateTime={startDateStr}&endDateTime={endDateStr}&size={limit}&sort=date,asc";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var ticketmasterResponse = JsonSerializer.Deserialize<TicketmasterResponse>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (ticketmasterResponse?._embedded?.events == null)
                {
                    return new List<Event>();
                }

                return ticketmasterResponse._embedded.events.Select(MapToEvent).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting events by date range");
                throw;
            }
        }

        public async Task<IEnumerable<Event>> SearchEvents(string searchTerm, int limit = 50)
        {
            try
            {
                var url = $"events.json?apikey={_apiKey}&keyword={searchTerm}&size={limit}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var ticketmasterResponse = JsonSerializer.Deserialize<TicketmasterResponse>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (ticketmasterResponse?._embedded?.events == null)
                {
                    return new List<Event>();
                }

                return ticketmasterResponse._embedded.events.Select(MapToEvent).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error searching events with term {searchTerm}");
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsOrderByDate(bool ascending = true, int limit = 50)
        {
            try
            {
                var url = $"events.json?apikey={_apiKey}&size={limit}&sort=date,{(ascending ? "asc" : "desc")}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var ticketmasterResponse = JsonSerializer.Deserialize<TicketmasterResponse>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (ticketmasterResponse?._embedded?.events == null)
                {
                    return new List<Event>();
                }

                return ticketmasterResponse._embedded.events.Select(MapToEvent).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting events ordered by date");
                throw;
            }
        }

        private Event MapToEvent(TicketmasterEvent tmEvent)
        {
            var venue = tmEvent._embedded?.Venues?.FirstOrDefault();
            var priceRange = tmEvent.PriceRanges?.FirstOrDefault();

            return new Event
            {
                Id = int.TryParse(tmEvent.Id, out int id) ? id : 0,
                Name = tmEvent.Name,
                Description = tmEvent.Type,
                URL = tmEvent.Url,
                StartDate = tmEvent.Dates.Start.DateTime,
                EndDate = tmEvent.Dates.Start.DateTime.AddHours(3), // Approximate duration
                Location = venue?.Address?.Line1 ?? string.Empty,
                Venue = venue?.Name ?? string.Empty,
                City = venue?.City?.Name ?? string.Empty,
                Country = venue?.Country?.Name ?? string.Empty,
                Category = tmEvent.Type,
                ImageUrl = tmEvent.Images.FirstOrDefault()?.Url ?? string.Empty,
                Status = tmEvent.Dates.Status.Code,
                MinPrice = priceRange?.Min,
                MaxPrice = priceRange?.Max,
                IsFree = priceRange == null || priceRange.Min == 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}