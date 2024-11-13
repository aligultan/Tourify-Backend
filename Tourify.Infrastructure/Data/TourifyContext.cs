// TourifyContext.cs
using Microsoft.EntityFrameworkCore;
using Tourify.Core.Entities;

namespace Tourify.Infrastructure.Data
{
    public class TourifyContext : DbContext
    {
        public TourifyContext(DbContextOptions<TourifyContext> options) : base(options)
        {
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PlaceReview> PlaceReviews { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserFavorite için composite key tanımlama
            modelBuilder.Entity<UserFavorite>()
                .HasKey(uf => new { uf.UserId, uf.PlaceId });

            // Place - UserFavorite ilişkisi
            modelBuilder.Entity<UserFavorite>()
                .HasOne(uf => uf.Place)
                .WithMany(p => p.UserFavorites)
                .HasForeignKey(uf => uf.PlaceId);

            // User - UserFavorite ilişkisi
            modelBuilder.Entity<UserFavorite>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(uf => uf.UserId);
        }
    }
}