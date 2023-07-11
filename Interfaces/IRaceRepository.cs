using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<IEnumerable<Race>> GetRaceByCity(string city);
        bool Add(Race Race);
        bool Update(Race Race);
        bool Delete(Race Race);
        bool Save();
    }
}
