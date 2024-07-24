using DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly PRN221_ProjectContext _ProjectContext;
        protected GenericRepository(PRN221_ProjectContext ProjectContext)
        {
            _ProjectContext = ProjectContext;
        }

        public async Task CreateItem(T item)
        {
            _ProjectContext.Set<T>().Add(item);
            await _ProjectContext.SaveChangesAsync();
        }

        public async Task<List<T>> getAll()
        {
            return await _ProjectContext.Set<T>().ToListAsync();
        }



        public async Task<List<Seat>> GetSeatsByTheaterAndShowTimeAsync(int theaterID, int movieID)
        {

            return await _ProjectContext.Seats
                                       .Where(s => s.TheaterId == theaterID)
                                       .ToListAsync();
        }

        public async Task<T> getSelected(int id)
        {
            return await _ProjectContext.Set<T>().FindAsync(id);
        }

        public async Task<Showtime> GetShowtimeById(int id)
        {
            return await _ProjectContext.Showtimes
                                .Include(s => s.Movie).Include(s => s.Theater)  // Ensure Movie is included
                                .FirstOrDefaultAsync(s => s.ShowtimeId == id);
        }

        public async Task<User> GetUserByUsernamePassword(string username, string pass)
        {
            string s = username;
            User user = await _ProjectContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(username) && x.Password.Equals(pass));
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task RemoveItem(T item)
        {
            _ProjectContext.Set<T>().Remove(item);
            await _ProjectContext.SaveChangesAsync();
        }




    }
}
