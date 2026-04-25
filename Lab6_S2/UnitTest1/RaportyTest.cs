using Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest1
{
    public class RaportyTest
    {
        private Raporty DaneTestowe()
        {
            var zawodnikRepo = new FakeRepository<Zawodnik>();
            var klubRepo = new FakeRepository<Klub>();
            var statystykaRepo = new FakeRepository<Statystyka>();

        
            klubRepo.Add(new Klub { KlubId = 1, Nazwa = "Klub A" });
            klubRepo.Add(new Klub { KlubId = 2, Nazwa = "Klub B" });

            
            zawodnikRepo.Add(new Zawodnik { ZawodnikId = 1, KlubId = 1, czyKontuzja = false, kondycja = 8.0 });
            zawodnikRepo.Add(new Zawodnik { ZawodnikId = 2, KlubId = 1, czyKontuzja = true, kondycja = 5.0 });
            zawodnikRepo.Add(new Zawodnik { ZawodnikId = 3, KlubId = 1, czyKontuzja = false, kondycja = 10.0 });

           
            zawodnikRepo.Add(new Zawodnik { ZawodnikId = 4, KlubId = 2, czyKontuzja = true, kondycja = 7.0 });

           
            statystykaRepo.Add(new Statystyka { StatystykaId = 1, ZawodnikId = 1, zdobyteGole = 10 });
            statystykaRepo.Add(new Statystyka { StatystykaId = 2, ZawodnikId = 2, zdobyteGole = 5 });
            statystykaRepo.Add(new Statystyka { StatystykaId = 3, ZawodnikId = 3, zdobyteGole = 15 });
            statystykaRepo.Add(new Statystyka { StatystykaId = 4, ZawodnikId = 4, zdobyteGole = 2 });

           
            return new Raporty(zawodnikRepo, klubRepo, statystykaRepo);
        }
        [Fact]
        public void PobierzZawodikowKlubu_DlaIstniejacegoKlubu_ZwracaTylkoJegoZawodikow()
        {
            
            var service = DaneTestowe();

            
            var zawodnicyKlubu1 = service.PobierzZawodnikowKlubu(1).ToList();
            var zawodnicyKlubu2 = service.PobierzZawodnikowKlubu(2).ToList();

            
            Assert.Equal(3, zawodnicyKlubu1.Count);
            Assert.All(zawodnicyKlubu1, z => Assert.Equal(1, z.KlubId)); 

            Assert.Single(zawodnicyKlubu2); 
            Assert.Equal(2, zawodnicyKlubu2.First().KlubId);
        }

        [Fact]
        public void ObliczLacznaLiczbeGoliKlubu_ZwracaZsumowaneGoleZawodikowKlubu()
        {
            
            var service = DaneTestowe();

           
            var goleKlub1 = service.GoleKlubu(1);
            var goleKlub2 = service.GoleKlubu(2);
            var goleKlub3 = service.GoleKlubu(3); 

            
            Assert.Equal(30, goleKlub1);
            Assert.Equal(2, goleKlub2);  
            Assert.Equal(0, goleKlub3); 
        }

        [Fact]
        public void ObliczSredniaKondycjeZdrowychWKlubie_DlaKlubuZeZdrowymi_ZwracaPoprawnaSrednia()
        {
            
            var service = DaneTestowe();

           
            var sredniaKlub1 = service.SredniaKondycjaZawodnikow(1);

            
            Assert.Equal(9.0, sredniaKlub1);
        }

        [Fact]
        public void ObliczSredniaKondycjeZdrowychWKlubie_KiedyBrakZdrowychZawodikow_ZwracaZero()
        {
            
            var service = DaneTestowe();

            
            var sredniaKlub2 = service.SredniaKondycjaZawodnikow(2); 
            var sredniaKlub3 = service.SredniaKondycjaZawodnikow(3); 

            
            Assert.Equal(0.0, sredniaKlub2);
            Assert.Equal(0.0, sredniaKlub3);
        }
    }
}
