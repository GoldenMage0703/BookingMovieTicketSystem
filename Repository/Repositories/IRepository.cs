using DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> getAll();
        Task<T> getSelected(int id);
        Task RemoveItem(T item);
        Task CreateItem(T item);
        Task<List<Seat>> GetSeatsByTheaterAndShowTimeAsync(int theaterID,int movieID);

        Task<Showtime> GetShowtimeById(int id);

        Task<User> GetUserByUsernamePassword(string username,string pass);
    }
}
