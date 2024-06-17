using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Patient.View;
using WaveFit2.Patient.ViewModel;

namespace WaveFit2.Audiogram.View
{
    /// <summary>
    /// Lógica interna para AudiogramSelectForm.xaml
    /// </summary>
    public partial class AudiogramSelectForm : Window
    {
        public long codePatient;
        public PatientViewModel patientViewModel = new PatientViewModel();
        public ActivePatientUserControl activePatientUserControl = new ActivePatientUserControl();
        public string patientName;
        public bool patientBox = true;
        public bool codeBox = true;

        public AudiogramSelectForm()
        {
            InitializeComponent();
            CreateDatabase();
            ComboBoxContent();
            AudiogramSelectFormTools();
        }

        public void CreateDatabase()
        {
            patientViewModel.LoadPatientToDatagrid(activePatientUserControl.DatagridPatientActive, true);
        }

        public void ComboBoxContent()
        {
            if (patientViewModel.dataFromDatabase.FirstOrDefault() != null)
            {
                List<long> codes = patientViewModel.dataFromDatabase.Select(patient => patient.PatientCode).ToList();
                CodeComboBox.ItemsSource = codes;

                List<string> patients = patientViewModel.dataFromDatabase.Select(patient => patient.PatientCode + " - " + patient.Name + " " + patient.Surname).ToList();
                PatientComboBox.ItemsSource = patients;
            }
        }

        public void AudiogramSelectFormTools()
        {
            SelectButton.Click += SelectButton_Click;
            CloseButton.Click += CloseButton_Click;

            CodeComboBox.SelectionChanged += CodeComboBox_SelectionChanged;
            PatientComboBox.SelectionChanged += PatientComboBox_SelectionChanged;

            PatientFilter.TextChanged += PatientFilter_TextChanged;
        }

        private void PatientFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = PatientFilter.Text.ToLower();

            if (!string.IsNullOrEmpty(filter))
            {
                List<long> filteredCode = patientViewModel.dataFromDatabase
                          .Where(patient => patient.PatientCode.ToString().Contains(filter))
                          .Select(patient => patient.PatientCode)
                          .ToList();
                CodeComboBox.ItemsSource = filteredCode;

                List<string> filteredName = patientViewModel.dataFromDatabase
                                            .Where(patient => (patient.PatientCode + " - " + patient.Name + " " + patient.Surname).ToLower().Contains(filter))
                                            .Select(patient => patient.PatientCode + " - " + patient.Name + " " + patient.Surname)
                                            .ToList();
                PatientComboBox.ItemsSource = filteredName;
            }
            else
            {
                ComboBoxContent();
            }
        }

        private void PatientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PatientFilter.Text = string.Empty;
            int selectedIndex = PatientComboBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < patientViewModel.dataFromDatabase.Count && patientBox == true)
            {
                codeBox = false;
                long code = patientViewModel.dataFromDatabase[selectedIndex].PatientCode;
                int codeIndex = CodeComboBox.Items.IndexOf(code);
                CodeComboBox.SelectedIndex = codeIndex;
                codeBox = true;
            }
            else if (selectedIndex >= 0 && selectedIndex < patientViewModel.dataFromDatabase.Count && patientBox == false)
            {
                PatientComboBox.SelectedIndex = CodeComboBox.SelectedIndex;
            }
            else
            {
                CodeComboBox.SelectedIndex = -1;
            }
        }

        private void CodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PatientFilter.Text = string.Empty;

            int selectedIndex = CodeComboBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < patientViewModel.dataFromDatabase.Count && codeBox == true)
            {
                patientBox = false;
                string name = patientViewModel.dataFromDatabase[selectedIndex].Name;
                string surname = patientViewModel.dataFromDatabase[selectedIndex].Surname;
                string nomeCompleto = $"{name} {surname}";
                int nameIndex = PatientComboBox.Items.IndexOf(nomeCompleto);
                PatientComboBox.SelectedIndex = nameIndex;
                patientBox = true;
            }
            else if (selectedIndex >= 0 && selectedIndex < patientViewModel.dataFromDatabase.Count && codeBox == false)
            {
                CodeComboBox.SelectedIndex = PatientComboBox.SelectedIndex;
            }
            else
            {
                PatientComboBox.SelectedIndex = -1;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientComboBox.SelectedIndex == -1 || CodeComboBox.SelectedIndex == -1)
            {
                HandyControl.Controls.Growl.Warning("Selecione um paciente!");
            }
            else
            {
                Properties.Settings.Default.patientName = PatientComboBox.SelectedItem.ToString();
                Properties.Settings.Default.patientCode = (long)CodeComboBox.SelectedItem;
                patientViewModel.GetPatientId(Properties.Settings.Default.patientCode);
                this.Close();
            }
        }
    }
}