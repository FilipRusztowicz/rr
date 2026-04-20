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

namespace WpfApplication
{
    public class ZawodnikViewModel
    {
        private ZawodnikRepository _repository;
        private Zawodnik _zawodnikEntity = null;
        public ZawodnikRecord ZawodnikRecord { get; set; }
        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
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
                
                _zawodnikEntity.imie = ZawodnikRecord.Imie;
                _zawodnikEntity.kondycja = ZawodnikRecord.Kondycja;
                _zawodnikEntity.czyKontuzja = ZawodnikRecord.CzyKontuzja;
                _zawodnikEntity.nrKoszulki = ZawodnikRecord.NrKoszulki;

                try
                {
                    if (ZawodnikRecord.ZawodnikId <= 0)
                    {
                        _repository.Add(_zawodnikEntity);
                        MessageBox.Show("New record successfully saved.");
                    }
                    else
                    {
                        _zawodnikEntity.ZawodnikId = ZawodnikRecord.ZawodnikId;
                        _repository.Update(_zawodnikEntity);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
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
        public ZawodnikViewModel(DbPZPN dbPZPN)
        {
            _zawodnikEntity = new Zawodnik();
            _repository = new ZawodnikRepository(dbPZPN);
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
                Kondycja = data.kondycja
            }));
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
