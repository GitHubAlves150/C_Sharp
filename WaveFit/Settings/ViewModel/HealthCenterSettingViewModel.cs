using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Settings.Class;

namespace WaveFit2.Settings.ViewModel
{
    public class HealthCenterSettingViewModel : Crud, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region HealthCenter

        public ObservableCollection<HealthCenterModel> HealthCenter { get; set; } = new ObservableCollection<HealthCenterModel>();
        public List<HealthCenterModel> healthcenterFromDatabase = new List<HealthCenterModel>();

        public void LoadHealthCenterToDatagrid(DataGrid dataGrid, bool status)
        {
            healthcenterFromDatabase = LoadHealthCenterDataFromDatabase(status);

            foreach (HealthCenterModel item in healthcenterFromDatabase)
            {
                HealthCenter.Add(item);
            }
            dataGrid.ItemsSource = HealthCenter;
        }

        public void LoadHealthCenterToDatagridClear(DataGrid dataGrid)
        {
            HealthCenter.Clear();
            dataGrid.ItemsSource = HealthCenter;
        }

        #endregion HealthCenter

        #region Audiometer

        public ObservableCollection<AudiometerModel> Audiometer { get; set; } = new ObservableCollection<AudiometerModel>();
        public List<AudiometerModel> audiometerFromDatabase = new List<AudiometerModel>();
        public int idAudiometer;

        public void LoadAudiometerToDatagrid(DataGrid dataGrid, int idAudiometer)
        {
            audiometerFromDatabase.Clear();
            audiometerFromDatabase = LoadAudiometerDataFromDatabase(idAudiometer);

            foreach (AudiometerModel item in audiometerFromDatabase)
            {
                Audiometer.Add(item);
            }
            dataGrid.ItemsSource = Audiometer;
        }

        public void LoadAudiometerToDatagridClear(DataGrid dataGrid)
        {
            Audiometer.Clear();
            dataGrid.ItemsSource = Audiometer;
        }

        #endregion Audiometer

        #region Location

        public ObservableCollection<LocationModel> Location { get; set; } = new ObservableCollection<LocationModel>();
        public List<LocationModel> locationFromDatabase = new List<LocationModel>();
        public int idLocation;

        public void LoadLocationToDatagrid(DataGrid dataGrid, int idLocation)
        {
            locationFromDatabase.Clear();
            locationFromDatabase = LoadLocationDataFromDatabase(idLocation);

            foreach (LocationModel item in locationFromDatabase)
            {
                Location.Add(item);
            }
            dataGrid.ItemsSource = Location;
        }

        public void LoadLocationToDatagridClear(DataGrid dataGrid)
        {
            Location.Clear();
            dataGrid.ItemsSource = Location;
        }

        #endregion Location

        #region Place

        public ObservableCollection<PlaceModel> Place { get; set; } = new ObservableCollection<PlaceModel>();
        public List<PlaceModel> placeFromDatabase = new List<PlaceModel>();
        public int idPlace;

        public void LoadPlaceToDatagrid(DataGrid dataGrid, int idPlace)
        {
            placeFromDatabase.Clear();
            placeFromDatabase = LoadPlaceDataFromDatabase(idPlace);

            foreach (PlaceModel item in placeFromDatabase)
            {
                Place.Add(item);
            }
            dataGrid.ItemsSource = Place;
        }

        public void LoadPlaceToDatagridClear(DataGrid dataGrid)
        {
            Place.Clear();
            dataGrid.ItemsSource = Place;
        }

        #endregion Place

        public ObservableCollection<CombinedData> CombinedDataList { get; set; } = new ObservableCollection<CombinedData>();

        public void LoadCombinedData(bool status)
        {
            CombinedDataList.Clear();

            var healthCenters = LoadHealthCenterDataFromDatabase(status);
            foreach (var healthCenter in healthCenters)
            {
                var combinedData = new CombinedData
                {
                    Id = healthCenter.Id,
                    Name = healthCenter.Name,
                    Nickname = healthCenter.Nickname,
                    Logo = healthCenter.Logo,
                    CNPJ = healthCenter.CNPJ,
                    Telephone = healthCenter.Telephone,
                    IdPlace = healthCenter.IdPlace,
                    IdAudiometer = healthCenter.IdAudiometer,
                };

                var audiometer = LoadAudiometerDataFromDatabase(healthCenter.IdAudiometer).FirstOrDefault();
                if (audiometer != null)
                {
                    combinedData.Code = audiometer.Code;
                    combinedData.Model = audiometer.Model;
                    combinedData.Maintenance = audiometer.Maintenance;
                }

                var place = LoadPlaceDataFromDatabase(healthCenter.IdPlace).FirstOrDefault();
                if (place != null)
                {
                    combinedData.City = place.City;
                    combinedData.Area = place.Area;
                    combinedData.Address = place.Address;
                    combinedData.CEP = place.CEP;
                    combinedData.IdLocation = place.IdLocation;
                }

                var location = LoadLocationDataFromDatabase(place.IdLocation).FirstOrDefault();
                if (location != null)
                {
                    combinedData.Acronym = location.Acronym;
                    combinedData.State = location.State;
                }

                CombinedDataList.Add(combinedData);
            }

            OnPropertyChanged(nameof(CombinedDataList));
        }
    }
}