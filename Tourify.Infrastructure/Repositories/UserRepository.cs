using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tourify.Core.Entities;
using Tourify.Core.Interfaces;
using Tourify.Infrastructure.Data;

namespace Tourify.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TourifyContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IReadOnlyList<Place>> GetUserFavorites(int userId)
        {
            var favorites = await _context.UserFavorites
                .Where(uf => uf.UserId == userId)
                .Include(uf => uf.Place)
                .Select(uf => uf.Place)
                .ToListAsync();

            return favorites;
        }

        public async Task<bool> AddToFavorites(int userId, int placeId)
        {
            var favorite = await _context.UserFavorites
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.PlaceId == placeId);

            if (favorite != null)
                return false;

            favorite = new UserFavorite
            {
                UserId = userId,
                PlaceId = placeId,
                CreatedAt = DateTime.UtcNow,
                HasVisited = false
            };

            await _context.UserFavorites.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAsVisited(int userId, int placeId)
        {
            var favorite = await _context.UserFavorites
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.PlaceId == placeId);

            if (favorite == null)
                return false;

            favorite.HasVisited = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}