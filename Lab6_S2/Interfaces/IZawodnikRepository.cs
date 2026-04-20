using Domain;

namespace Interfaces
    
{
    public interface IZawodnikRepository
    {
        IEnumerable<Zawodnik> GetAll();
        Zawodnik GetById(int id);
        void Add(Zawodnik zawodnik);
        void Update(Zawodnik zawodnik);
        void Delete(int id);
    }
}
