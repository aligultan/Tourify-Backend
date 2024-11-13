using Microsoft.EntityFrameworkCore;
using Tourify.Core.Entities;

namespace Tourify.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(TourifyContext context)
        {
            if (!context.Places.Any())
            {
                var places = new List<Place>
                {
                    new Place
                    {
                        Name = "Eyfel Kulesi",
                        Description = "Paris'in sembol yapısı olan 324 metre yüksekliğindeki demir kule",
                        Address = "Champ de Mars, 5 Avenue Anatole France",
                        City = "Paris",
                        Country = "Fransa",
                        Latitude = 48.858844,
                        Longitude = 2.294351,
                        Type = PlaceType.HistoricalSite,
                        Rating = 4.8,
                        ImageUrl = "https://example.com/eiffel.jpg",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Place
                    {
                        Name = "Louvre Müzesi",
                        Description = "Dünyanın en büyük sanat müzelerinden biri",
                        Address = "Rue de Rivoli",
                        City = "Paris",
                        Country = "Fransa",
                        Latitude = 48.860294,
                        Longitude = 2.338629,
                        Type = PlaceType.Museum,
                        Rating = 4.9,
                        ImageUrl = "https://example.com/louvre.jpg",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Place
                    {
                        Name = "Colosseum",
                        Description = "Roma İmparatorluğu'nun ikonik amfi tiyatrosu",
                        Address = "Piazza del Colosseo",
                        City = "Roma",
                        Country = "İtalya",
                        Latitude = 41.890251,
                        Longitude = 12.492373,
                        Type = PlaceType.HistoricalSite,
                        Rating = 4.7,
                        ImageUrl = "https://example.com/colosseum.jpg",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Place
                    {
                        Name = "Sultanahmet Camii",
                        Description = "Osmanlı mimarisinin en önemli eserlerinden biri",
                        Address = "Sultanahmet Meydanı",
                        City = "İstanbul",
                        Country = "Türkiye",
                        Latitude = 41.005270,
                        Longitude = 28.976960,
                        Type = PlaceType.HistoricalSite,
                        Rating = 4.9,
                        ImageUrl = "https://example.com/bluemosque.jpg",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                };

                await context.Places.AddRangeAsync(places);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Username = "johndoe",
                        Email = "john@example.com",
                        PasswordHash = "hashedpassword123",
                        CreatedAt = DateTime.Now
                    },
                    new User
                    {
                        Username = "janesmith",
                        Email = "jane@example.com",
                        PasswordHash = "hashedpassword456",
                        CreatedAt = DateTime.Now
                    }
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                // Kullanıcılar için favori yerler ekleme
                var favorites = new List<UserFavorite>
                {
                    new UserFavorite
                    {
                        UserId = 1,
                        PlaceId = 1,
                        CreatedAt = DateTime.Now,
                        HasVisited = true
                    },
                    new UserFavorite
                    {
                        UserId = 1,
                        PlaceId = 2,
                        CreatedAt = DateTime.Now,
                        HasVisited = false
                    }
                };

                await context.UserFavorites.AddRangeAsync(favorites);
                await context.SaveChangesAsync();
            }
        }
    }
}