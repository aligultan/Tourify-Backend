// IUserRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourify.Core.Entities;

namespace Tourify.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<IReadOnlyList<Place>> GetUserFavorites(int userId);
        Task<bool> AddToFavorites(int userId, int placeId);
        Task<bool> MarkAsVisited(int userId, int placeId);
    }
}