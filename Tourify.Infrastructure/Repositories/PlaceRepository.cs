using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tourify.Core.Entities;
using Tourify.Core.Interfaces;
using Tourify.Infrastructure.Data;

namespace Tourify.Infrastructure.Repositories
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        public PlaceRepository(TourifyContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Place>> GetPlacesByCity(string city)
        {
            return await _context.Places
                .Where(p => p.City.ToLower() == city.ToLower())
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Place>> GetPlacesByType(string city, PlaceType type)
        {
            return await _context.Places
                .Where(p => p.City.ToLower() == city.ToLower() && p.Type == type)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Place>> GetTopRatedPlaces(string city, int count)
        {
            return await _context.Places
                .Where(p => p.City.ToLower() == city.ToLower())
                .OrderByDescending(p => p.Rating)
                .Take(count)
                .ToListAsync();
        }
    }
}