// Tourify.Core/Models/Ticketmaster/TicketmasterModels.cs
namespace Tourify.Core.Models.Ticketmaster
{
    public class TicketmasterResponse
    {
        public TicketmasterEmbedded _embedded { get; set; } = null!;
        public TicketmasterPage Page { get; set; } = null!;
    }

    public class TicketmasterEmbedded
    {
        public List<TicketmasterEvent> events { get; set; } = new();
    }

    public class TicketmasterEvent
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public TicketmasterDates Dates { get; set; } = null!;
        public List<TicketmasterPriceRange>? PriceRanges { get; set; }
        public TicketmasterEmbeddedVenues _embedded { get; set; } = null!;
        public List<TicketmasterImage> Images { get; set; } = new();
    }

    public class TicketmasterDates
    {
        public TicketmasterStart Start { get; set; } = null!;
        public TicketmasterStatus Status { get; set; } = null!;
    }

    public class TicketmasterStart
    {
        public string LocalDate { get; set; } = string.Empty;
        public string LocalTime { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }

    public class TicketmasterStatus
    {
        public string Code { get; set; } = string.Empty;
    }

    public class TicketmasterPriceRange
    {
        public string Type { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }

    public class TicketmasterEmbeddedVenues
    {
        public List<TicketmasterVenue> Venues { get; set; } = new();
    }

    public class TicketmasterVenue
    {
        public string Name { get; set; } = string.Empty;
        public TicketmasterCity City { get; set; } = null!;
        public TicketmasterCountry Country { get; set; } = null!;
        public TicketmasterAddress Address { get; set; } = null!;
    }

    public class TicketmasterCity
    {
        public string Name { get; set; } = string.Empty;
    }

    public class TicketmasterCountry
    {
        public string Name { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
    }

    public class TicketmasterAddress
    {
        public string Line1 { get; set; } = string.Empty;
    }

    public class TicketmasterImage
    {
        public string Url { get; set; } = string.Empty;
        public string Ratio { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class TicketmasterPage
    {
        public int Size { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public int Number { get; set; }
    }
}