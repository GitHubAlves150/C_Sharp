using System;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Patient.ViewModel;

namespace WaveFit2.Patient.View
{
    public partial class ActivePatientUserControl : UserControl
    {
        public PatientViewModel patientViewModel = new PatientViewModel();

        public Crud crudOperations = new Crud();

        public int idPatient;

        public ActivePatientUserControl()
        {
            InitializeComponent();
            DataContext = patientViewModel;
            patientViewModel.LoadPatientToDatagrid(DatagridPatientActive, true);
        }

        public void PatientDeleteClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.DataContext is PatientModel patient)
            {
                long patientCode = patient.PatientCode;
                crudOperations.UpdateStatePatient(patientCode, false);
                patientViewModel.LoadPatientToDatagrid(DatagridPatientActive, true);
                HandyControl.Controls.Growl.Success("Paciente Deletado com sucesso.");
            }
        }

        public void PatientEditClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button.DataContext is PatientModel patient)
                {
                    long patientId = patient.PatientCode;
                    PatientForm patientForm = new PatientForm();
                    patientForm.PatientName.Text = patient.Name;
                    patientForm.PatientSurname.Text = patient.Surname;
                    patientForm.PatientCode.Value = patient.PatientCode;
                    patientForm.PatientBirthday.SelectedDate = patient.Birthday.ToUniversalTime();
                    patientForm.gender = patient.Gender;
                    patientForm.PatientTypeDoc.SelectedItem = patient.TypeDocument;
                    patientForm.PatientNumDoc.Text = patient.NumDocument.ToString();
                    switch (patientForm.gender)
                    {
                        case "M":
                            patientForm.CheckM.IsChecked = true;
                            break;

                        case "F":
                            patientForm.CheckF.IsChecked = true;
                            break;

                        case "Outro":
                            patientForm.CheckOther.IsChecked = true;
                            break;
                    }
                    patientForm.RegisterButton.Visibility = Visibility.Hidden;
                    patientForm.SaveButton.Visibility = Visibility.Visible;
                    patientForm.SaveButton.Click += SaveButton_Click;
                    patientForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                patientViewModel.LoadPatientToDatagrid(DatagridPatientActive, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}