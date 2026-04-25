using System.Linq;
using Domain;
using Services;
using Xunit;
using WpfApplication;

namespace UnitTest1
{
    public class ZawodnikViewModelTests
    {
        private ZawodnikViewModel UtworzViewModelDoTestow()
        {
            var zawodnikRepo = new FakeRepository<Zawodnik>();
            var klubRepo = new FakeRepository<Klub>();
            var statystykaRepo = new FakeRepository<Statystyka>();

            zawodnikRepo.Add(new Zawodnik
            {
                ZawodnikId = 1,
                imie = "Testowy",
                kondycja = 5.0,
                nrKoszulki = 10,
                KlubId = 1
            });

            var service = new Raporty(zawodnikRepo, klubRepo, statystykaRepo);

            return new ZawodnikViewModel(zawodnikRepo, service);
        }

        [Fact]
        public void Konstruktor_WywolujeGetAll_I_WypelniaListeZawodikow()
        {
            var viewModel = UtworzViewModelDoTestow();

            Assert.NotNull(viewModel.ZawodnikRecord);
            Assert.NotNull(viewModel.ZawodnikRecord.ZawodnikRecords);

            Assert.Single(viewModel.ZawodnikRecord.ZawodnikRecords);
            Assert.Equal("Testowy", viewModel.ZawodnikRecord.ZawodnikRecords[0].Imie);
        }

        [Fact]
        public void ResetData_CzysciWszystkieWlasciwosciFormularza()
        {
            var viewModel = UtworzViewModelDoTestow();

            viewModel.ZawodnikRecord.ZawodnikId = 99;
            viewModel.ZawodnikRecord.Imie = "Jan Kowalski";
            viewModel.ZawodnikRecord.Kondycja = 10.0;
            viewModel.ZawodnikRecord.CzyKontuzja = true;

            viewModel.ResetData();

            Assert.Equal(0, viewModel.ZawodnikRecord.ZawodnikId);
            Assert.Equal(string.Empty, viewModel.ZawodnikRecord.Imie);
            Assert.Equal(0.0, viewModel.ZawodnikRecord.Kondycja);
            Assert.False(viewModel.ZawodnikRecord.CzyKontuzja);
        }

        [Fact]
        public void EditData_WypelniaFormularz_DanymiWybranegoZTableliZawodnika()
        {
            var viewModel = UtworzViewModelDoTestow();

            viewModel.EditData(1);

            Assert.Equal(1, viewModel.ZawodnikRecord.ZawodnikId);
            Assert.Equal("Testowy", viewModel.ZawodnikRecord.Imie);
            Assert.Equal(5.0, viewModel.ZawodnikRecord.Kondycja);
            Assert.Equal(10, viewModel.ZawodnikRecord.NrKoszulki);
        }

        [Fact]
        public void SaveData_GdyZazwodnikMaIdZero_DodajeNowyRekordDoBazy()
        {
            var viewModel = UtworzViewModelDoTestow();

            viewModel.ResetData();

            viewModel.ZawodnikRecord.Imie = "Nowy Gracz";
            viewModel.ZawodnikRecord.Kondycja = 9.9;
            viewModel.ZawodnikRecord.KlubId = 1;

            viewModel.SaveData();

            Assert.Equal(2, viewModel.ZawodnikRecord.ZawodnikRecords.Count);

            var dodanyGracz = viewModel.ZawodnikRecord.ZawodnikRecords.Last();
            Assert.Equal("Nowy Gracz", dodanyGracz.Imie);
        }
    }
}