using System;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Settings.Class;
using WaveFit2.Settings.ViewModel;

namespace WaveFit2.Settings.View
{
    /// <summary>
    /// Interaction logic for HealthCenterUserControl.xaml
    /// </summary>
    public partial class HealthCenterUserControl : UserControl
    {
        public HealthCenterSettingViewModel healthcentersettingViewModel = new HealthCenterSettingViewModel();

        public HealthCenterUserControl()
        {
            InitializeComponent();
            DataContext = healthcentersettingViewModel;
            LoadDatagrid();
            HealthCenterUCTools();
        }

        public void HealthCenterUCTools()
        {
            AddHealthCenter.Click += AddHealthCenter_Click;
        }

        private void AddHealthCenter_Click(object sender, RoutedEventArgs e)
        {
            HealthCenterForms healthCenterForms = new HealthCenterForms();
            healthCenterForms.ShowDialog();
        }

        public void LoadDatagrids()
        {
            healthcentersettingViewModel.LoadHealthCenterToDatagrid(DatagridHealthCenter, true);

            for (int i = 0; i < healthcentersettingViewModel.healthcenterFromDatabase.Count; i++)
            {
                healthcentersettingViewModel.idAudiometer = healthcentersettingViewModel.HealthCenter[i].IdAudiometer;
                healthcentersettingViewModel.idPlace = healthcentersettingViewModel.HealthCenter[i].IdPlace;
                healthcentersettingViewModel.LoadAudiometerToDatagrid(DatagridAudiometer, healthcentersettingViewModel.HealthCenter[i].IdAudiometer);
                healthcentersettingViewModel.LoadPlaceToDatagrid(DatagridPlace, healthcentersettingViewModel.HealthCenter[i].IdPlace);

                healthcentersettingViewModel.idLocation = healthcentersettingViewModel.Place[i].IdLocation;
                healthcentersettingViewModel.LoadLocationToDatagrid(DatagridLocation, healthcentersettingViewModel.Place[i].IdLocation);

                Console.WriteLine(healthcentersettingViewModel.HealthCenter[i].IdAudiometer);
                Console.WriteLine(healthcentersettingViewModel.HealthCenter[i].IdPlace);
                Console.WriteLine(healthcentersettingViewModel.Place[i].IdLocation);
            }
        }

        public void LoadDatagrid()
        {
            healthcentersettingViewModel.LoadCombinedData(true);
        }

        public void LoadDatagridDeleted()
        {
            healthcentersettingViewModel.LoadCombinedData(false);
        }

        public void DataDeleteClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.DataContext is CombinedData data)
            {
                healthcentersettingViewModel.UpdateColBool("dbo.healthcenter", "status", false, data.Id);
                LoadDatagrid();
                HandyControl.Controls.Growl.Success("Clínica deletada com sucesso.");
            }
        }

        public void DataEditClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button.DataContext is CombinedData data)
                {
                    int healthCenterId = data.Id;
                    int audiometerId = data.IdAudiometer;
                    int palceId = data.IdPlace;
                    int locationId = data.IdLocation;

                    //long patientId = patient.PatientCode;
                    //PatientForm patientForm = new PatientForm();
                    //patientForm.PatientName.Text = patient.Name;
                    //patientForm.PatientSurname.Text = patient.Surname;
                    //patientForm.PatientCode.Value = patient.PatientCode;
                    //patientForm.PatientBirthday.SelectedDate = patient.Birthday.ToUniversalTime();
                    //patientForm.gender = patient.Gender;
                    //patientForm.PatientTypeDoc.SelectedItem = patient.TypeDocument;
                    //patientForm.PatientNumDoc.Text = patient.NumDocument.ToString();
                    //switch (patientForm.gender)
                    //{
                    //    case "M":
                    //        patientForm.CheckM.IsChecked = true;
                    //        break;

                    //    case "F":
                    //        patientForm.CheckF.IsChecked = true;
                    //        break;

                    //    case "Outro":
                    //        patientForm.CheckOther.IsChecked = true;
                    //        break;
                    //}
                    //patientForm.RegisterButton.Visibility = Visibility.Hidden;
                    //patientForm.SaveButton.Visibility = Visibility.Visible;
                    //patientForm.SaveButton.Click += SaveButton_Click;
                    //patientForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DataRecoverClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.DataContext is CombinedData data)
            {
                healthcentersettingViewModel.UpdateColBool("dbo.healthcenter", "status", true, data.Id);
                LoadDatagridDeleted();
                HandyControl.Controls.Growl.Success("Clínica Recuperado com sucesso.");
            }
        }

        private void CombinedDataGrid_SelectionChanged()
        {

        }
    }
}