using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Interfaces;

namespace WpfApplication
{
    public class ZawodnikViewModel:ViewModelBase
    {
        private IRepository<Zawodnik> _repository;
        private IRaporty _raporty;
        private Zawodnik _zawodnikEntity = null;
        public ZawodnikRecord ZawodnikRecord { get; set; }
        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private int _liczbaZawodnikowKlubu;
        public int LiczbaZawodnikowKlubu
        {
            get => _liczbaZawodnikowKlubu;
            set { _liczbaZawodnikowKlubu = value; OnPropertyChanged("LiczbaZawodnikowKlubu"); }
        }
        private int _laczneGoleKlubu;

        public int LaczneGoleKlubu
        {
            get => _laczneGoleKlubu;
            set { _laczneGoleKlubu = value; OnPropertyChanged("LaczneGoleKlubu"); }
        }

        private double _sredniaKondycjaZdrowych;
        public double SredniaKondycjaZdrowych
        {
            get => _sredniaKondycjaZdrowych;
            set { _sredniaKondycjaZdrowych = value; OnPropertyChanged("SredniaKondycjaZdrowych"); }
        }
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), null);

                return _resetCommand;
            }
        }
        public void ResetData()
        {
            ZawodnikRecord.ZawodnikId = 0;
            ZawodnikRecord.Imie = string.Empty;
            ZawodnikRecord.Kondycja = 0.0;
            ZawodnikRecord.CzyKontuzja = false;
            ZawodnikRecord.NrKoszulki = 0;
            ZawodnikRecord.KlubId = null; 
            ZawodnikRecord.StatystykaId = 0;
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(), null);

                return _saveCommand;
            }
        }
        public void SaveData()
        {
            if (ZawodnikRecord != null)
            {
                try
                {
                    if (ZawodnikRecord.ZawodnikId <= 0)
                    {
                        
                        var nowyZawodnik = new Zawodnik
                        {
                            imie = ZawodnikRecord.Imie,
                            kondycja = ZawodnikRecord.Kondycja,
                            czyKontuzja = ZawodnikRecord.CzyKontuzja,
                            nrKoszulki = ZawodnikRecord.NrKoszulki,
                            KlubId = ZawodnikRecord.KlubId,
                            StatystykaId = ZawodnikRecord.StatystykaId
                        };

                        _repository.Add(nowyZawodnik);
                        MessageBox.Show("New record successfully saved.");
                    }
                    else
                    {
                       
                        var zawodnikDoEdycji = _repository.GetById(ZawodnikRecord.ZawodnikId);

                        if (zawodnikDoEdycji != null)
                        {
                            zawodnikDoEdycji.imie = ZawodnikRecord.Imie;
                            zawodnikDoEdycji.kondycja = ZawodnikRecord.Kondycja;
                            zawodnikDoEdycji.czyKontuzja = ZawodnikRecord.CzyKontuzja;
                            zawodnikDoEdycji.nrKoszulki = ZawodnikRecord.NrKoszulki;
                            zawodnikDoEdycji.KlubId = ZawodnikRecord.KlubId;
                            zawodnikDoEdycji.StatystykaId = ZawodnikRecord.StatystykaId;

                            _repository.Update(zawodnikDoEdycji);
                            MessageBox.Show("Record successfully updated.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + (ex.InnerException?.Message ?? ex.Message));
                }
                finally
                {
                    GetAll();
                    ResetData();
                }
            }
        }
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((int)param), null);

                return _editCommand;
            }
        }
        public void EditData(int id)
        {
            var model = _repository.GetById(id);
            ZawodnikRecord.ZawodnikId = model.ZawodnikId;
            ZawodnikRecord.Imie = model.imie;
            ZawodnikRecord.Kondycja = model.kondycja;
            ZawodnikRecord.CzyKontuzja = model.czyKontuzja;
            ZawodnikRecord.NrKoszulki = model.nrKoszulki;
            ZawodnikRecord.KlubId = model.KlubId;             
            ZawodnikRecord.StatystykaId = model.StatystykaId; 
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeleteZawodnik((int)param), null);

                return _deleteCommand;
            }
        }
        public ZawodnikViewModel(IRepository<Zawodnik> repository, IRaporty pilkaNoznaService)
        {
            _repository = repository;
            _raporty = pilkaNoznaService;
            _zawodnikEntity = new Zawodnik();
            ZawodnikRecord = new ZawodnikRecord();
            GetAll();
        }

        public void GetAll()
        {
            this.ZawodnikRecord.ZawodnikRecords = new ObservableCollection<ZawodnikRecord>();
            _repository.GetAll().ToList().ForEach(data => ZawodnikRecord.ZawodnikRecords.Add(new ZawodnikRecord()
            {
                ZawodnikId = data.ZawodnikId,
                Imie = data.imie,
                NrKoszulki = data.nrKoszulki,
                CzyKontuzja = data.czyKontuzja,
                Kondycja = data.kondycja,
                StatystykaId = data.StatystykaId,
                KlubId = data.KlubId ?? 0
            }));
            OdswiezStatystyki(1);
        }
        private void OdswiezStatystyki(int klubId)
        {
            LaczneGoleKlubu = _raporty.GoleKlubu(klubId);
            SredniaKondycjaZdrowych = _raporty.SredniaKondycjaZawodnikow(klubId);
            LiczbaZawodnikowKlubu = _raporty.PobierzZawodnikowKlubu(klubId).Count();
        }

        public void DeleteZawodnik(int id)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Zawodnik", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.Delete(id);
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                }
            }
        }
    }
}
