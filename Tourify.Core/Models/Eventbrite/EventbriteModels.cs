// Tourify.Core/Models/Eventbrite/EventbriteModels.cs
namespace Tourify.Core.Models.Eventbrite
{
    public class EventbriteResponse
    {
        public Pagination Pagination { get; set; } = null!;
        public List<EventbriteEvent> Events { get; set; } = new();
    }

    public class EventbriteEvent
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public EventbriteDateTime Start { get; set; } = null!;
        public EventbriteDateTime End { get; set; } = null!;
        public EventbriteVenue? Venue { get; set; }
        public EventbriteOrganizer? Organizer { get; set; }
        public bool Is_Free { get; set; }
        public EventbriteTicket? Ticket_Availability { get; set; }
    }

    public class EventbriteDateTime
    {
        public string Timezone { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        public string Utc { get; set; } = string.Empty;
    }

    public class EventbriteVenue
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }

    public class EventbriteOrganizer
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class EventbriteTicket
    {
        public bool Has_Available_Tickets { get; set; }
        public decimal? Minimum_Ticket_Price { get; set; }
    }

    public class Pagination
    {
        public int Page_Count { get; set; }
        public int Page_Number { get; set; }
        public int Page_Size { get; set; }
        public int Total_Count { get; set; }
    }
}