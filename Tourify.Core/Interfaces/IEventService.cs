public interface IEventService
{
    Task<IEnumerable<Event>> GetUpcomingEvents(int limit = 50);
    Task<Event> GetEventById(string eventId);
    Task<IEnumerable<Event>> GetEventsByCity(string city, int limit = 50);
    // Yeni metodlar
    Task<IEnumerable<Event>> GetEventsByCategory(string category, int limit = 50);
    Task<IEnumerable<Event>> GetEventsByPriceRange(decimal minPrice, decimal maxPrice, int limit = 50);
    Task<IEnumerable<Event>> GetEventsByDateRange(DateTime startDate, DateTime endDate, int limit = 50);
    Task<IEnumerable<Event>> SearchEvents(string searchTerm, int limit = 50);
    Task<IEnumerable<Event>> GetEventsOrderByDate(bool ascending = true, int limit = 50);
}