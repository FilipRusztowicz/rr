using Domain;
using Interfaces;

namespace Services
{
    public class Raporty : IRaporty
    {
        private readonly IRepository<Zawodnik> _zawodnikRepository;
        private readonly IRepository<Klub> _klubRepository;
        private readonly IRepository<Statystyka> _statystykaRepository;

        public Raporty(IRepository<Zawodnik> zawodnikRepository,IRepository<Klub> klubRepository,IRepository<Statystyka> statystykaRepository)
        {
            _zawodnikRepository = zawodnikRepository;
            _klubRepository = klubRepository;
            _statystykaRepository = statystykaRepository;
        }
        public int GoleKlubu(int klubId)
        {
           Console.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!GOLEKLUBU!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return _statystykaRepository.GetAll()
                     .Where(s => s.Zawodnik.KlubId == klubId)
                     .Select(s => (int)s.zdobyteGole)
                     .ToList() 
                     .Sum();   
        }

        public IEnumerable<Zawodnik> PobierzZawodnikowKlubu(int klubId)
        {
            Console.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!ZAWODNICYKLUBU!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return _zawodnikRepository.GetAll().Where(z => z.KlubId == klubId).ToList();
        }

        public double SredniaKondycjaZawodnikow(int klubId)
        {
            Console.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!SREDNIAKONDYCJA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");    
            return _zawodnikRepository.GetAll()
                .Where(z => z.KlubId == klubId && !z.czyKontuzja)
                .Average(z => (double?)z.kondycja) ?? 0.0;
        }
    }
}
