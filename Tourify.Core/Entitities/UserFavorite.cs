using System;

namespace Tourify.Core.Entities
{
    public class UserFavorite
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool HasVisited { get; set; }
    }
}