using System;
using System.Collections.Generic;

namespace Tourify.Core.Entities  // Entities yazımını kontrol edin
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public PlaceType Type { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<PlaceReview> Reviews { get; set; } = new();
        public List<UserFavorite> UserFavorites { get; set; } = new();
    }

    public enum PlaceType
    {
        Restaurant,
        Cafe,
        Museum,
        HistoricalSite,
        Park,
        Church,
        Theater,
        ShoppingCenter,
        Other
    }
}