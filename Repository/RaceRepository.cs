using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDBContext _context;

        public RaceRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool Add(Race Race)
        {
            _context.Add(Race);
            return Save();
        }

        public bool Delete(Race Race)
        {
            _context.Remove(Race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Race> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await _context.Races.Where(x => x.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return  saved > 0 ? true : false;
            
        }

        public bool Update(Race Race)
        {
             _context.Update(Race);
             return Save();
        }
    }
}
