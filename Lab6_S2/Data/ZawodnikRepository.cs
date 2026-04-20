using Domain;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ZawodnikRepository : IZawodnikRepository
    {

        private DbPZPN db;

        public ZawodnikRepository(DbPZPN datab)
        {
           this.db = datab;
        }

        public void Add(Zawodnik zawodnik)
        {
            if (zawodnik != null)
            {
                db.Zawodnicy.Add(zawodnik);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
           
            var studObj = db.Zawodnicy.Find(id);
            if (studObj != null)
            {
                db.Zawodnicy.Remove(studObj);
                db.SaveChanges();
            }
        
        }

        public IEnumerable<Zawodnik> GetAll()
        {
            return db.Zawodnicy.ToList();
        }

        public Zawodnik GetById(int id)
        {
            return db.Zawodnicy.Find(id);
        }

        public void Update(Zawodnik zawodnik)
        {
            var zawodnikFind = this.GetById(zawodnik.ZawodnikId);
            if (zawodnikFind != null)
            {
                zawodnikFind.imie = zawodnik.imie;
                zawodnikFind.kondycja = zawodnik.kondycja;
                zawodnikFind.czyKontuzja = zawodnik.czyKontuzja;
                zawodnikFind.nrKoszulki = zawodnik.nrKoszulki;
                db.SaveChanges();
            }
        }
    }
}
