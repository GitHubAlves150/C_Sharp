using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Patient.ViewModel;

namespace WaveFit2.Patient.View
{
    /// <summary>
    /// Interação lógica para DisabledPatientUserControl.xam
    /// </summary>
    public partial class DisabledPatientUserControl : UserControl
    {
        public PatientViewModel patientViewModel = new PatientViewModel();
        public Crud crudOperations = new Crud();

        public DisabledPatientUserControl()
        {
            InitializeComponent();
            DataContext = patientViewModel;
            patientViewModel.LoadPatientToDatagrid(DatagridPatientDisabled, false);
        }

        private void PatientRecoverClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.DataContext is PatientModel patient)
            {
                long patientId = patient.PatientCode;
                crudOperations.UpdateStatePatient(patientId, true);
                HandyControl.Controls.Growl.Success("Paciente Recuperado com sucesso.");
                patientViewModel.LoadPatientToDatagrid(DatagridPatientDisabled, false);
            }
        }
    }
}