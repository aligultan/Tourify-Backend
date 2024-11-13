// Tourify.Core/Entities/Event.cs
// Tourify.Core/Entities/Event.cs
public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty; // Yeni eklendi
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty; // Yeni eklendi
    public string Organizer { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool IsFree { get; set; }
    public decimal? TicketPrice { get; set; }
    public int? AvailableSeats { get; set; } // Yeni eklendi
    public string Status { get; set; } = "Active"; // Yeni eklendi
    public decimal? MinPrice { get; set; } // Yeni eklendi
    public decimal? MaxPrice { get; set; } // Yeni eklendi
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}