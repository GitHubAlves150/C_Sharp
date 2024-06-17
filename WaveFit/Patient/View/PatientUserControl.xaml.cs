using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.Model;
using WaveFit2.Patient.ViewModel;

namespace WaveFit2.Patient.View
{
    public partial class PatientUserControl : UserControl
    {
        private ActivePatientUserControl activePatientUserControl = new ActivePatientUserControl();
        private DisabledPatientUserControl disabledPatientUserControl = new DisabledPatientUserControl();
        private PatientViewModel patientViewModel = new PatientViewModel();
        public bool active = true;

        public PatientUserControl()
        {
            InitializeComponent();
            PatientButtons();
        }

        public void PatientButtons()
        {
            AddPatientButton.Click += AddPatientButton_Click;
        }

        public void SearchPatient(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchPatientTexbox.Text.ToLower();
            ObservableCollection<PatientModel> FilteredPatient = new ObservableCollection<PatientModel>();

            if (active == true)
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    activePatientUserControl.DatagridPatientActive.ItemsSource = patientViewModel.dataFromDatabase;
                }
                else
                {
                    activePatientUserControl.DatagridPatientActive.ItemsSource = patientViewModel.dataFromDatabase;
                    foreach (PatientModel patiente in activePatientUserControl.DatagridPatientActive.ItemsSource)
                    {
                        if (patiente.PatientCode.ToString().ToLower().Contains(searchText) || patiente.Name.ToLower().Contains(searchText) || patiente.Surname.ToLower().Contains(searchText) || patiente.NumDocument.ToString().Contains(searchText))
                        {
                            FilteredPatient.Add(patiente);
                        }
                    }
                    activePatientUserControl.DatagridPatientActive.ItemsSource = FilteredPatient;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    disabledPatientUserControl.DatagridPatientDisabled.ItemsSource = patientViewModel.dataFromDatabase;
                }
                else
                {
                    disabledPatientUserControl.DatagridPatientDisabled.ItemsSource = patientViewModel.dataFromDatabase;
                    foreach (PatientModel patiente in disabledPatientUserControl.DatagridPatientDisabled.ItemsSource)
                    {
                        if (patiente.PatientCode.ToString().ToLower().Contains(searchText) || patiente.Name.ToLower().Contains(searchText) || patiente.Surname.ToLower().Contains(searchText))
                        {
                            FilteredPatient.Add(patiente);
                        }
                    }
                    disabledPatientUserControl.DatagridPatientDisabled.ItemsSource = FilteredPatient;
                }
            }
        }

        public void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            PatientForm patientForm = new PatientForm();
            patientForm.RegisterButton.Click += RegisterButton_Click;
            patientForm.ShowDialog();
        }

        public void LoadData()
        {
            patientViewModel.LoadPatientToDatagrid(activePatientUserControl.DatagridPatientActive, true);
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            patientViewModel.LoadPatientToDatagrid(activePatientUserControl.DatagridPatientActive, true);
        }

        public void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTabItem = e.AddedItems[0] as TabItem;
            if (selectedTabItem != null)
            {
                string tabName = selectedTabItem.Name;
                switch (tabName)
                {
                    case "TabActivated":

                        PatientMode.Content = activePatientUserControl;
                        active = true;
                        SearchPatientTexbox.Clear();
                        patientViewModel.LoadPatientToDatagrid(activePatientUserControl.DatagridPatientActive, active);

                        break;

                    case "TabDeleted":
                        PatientMode.Content = disabledPatientUserControl;
                        active = false;
                        SearchPatientTexbox.Clear();
                        patientViewModel.LoadPatientToDatagrid(disabledPatientUserControl.DatagridPatientDisabled, active);
                        break;
                }
            }
        }
    }
}