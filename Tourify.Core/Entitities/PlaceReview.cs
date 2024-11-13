using System;

namespace Tourify.Core.Entities
{
    public class PlaceReview
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}