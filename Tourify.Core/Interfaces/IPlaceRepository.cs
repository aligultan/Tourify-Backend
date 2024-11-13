// IPlaceRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourify.Core.Entities;

namespace Tourify.Core.Interfaces
{
    public interface IPlaceRepository : IGenericRepository<Place>
    {
        Task<IReadOnlyList<Place>> GetPlacesByCity(string city);
        Task<IReadOnlyList<Place>> GetPlacesByType(string city, PlaceType type);
        Task<IReadOnlyList<Place>> GetTopRatedPlaces(string city, int count);
    }
}