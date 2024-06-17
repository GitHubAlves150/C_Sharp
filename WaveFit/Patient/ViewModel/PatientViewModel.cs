using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Patient.ViewModel
{
    public class PatientViewModel : Crud
    {
        public ObservableCollection<PatientModel> Patient { get; set; } = new ObservableCollection<PatientModel>();
        public List<PatientModel> dataFromDatabase = new List<PatientModel>();

        public void LoadPatientToDatagrid(DataGrid dataGrid, bool statePatient)
        {
            Patient.Clear();

            dataFromDatabase = LoadPatientDataFromDatabase(statePatient);

            foreach (PatientModel item in dataFromDatabase)
            {
                Patient.Add(item);
            }
            dataGrid.ItemsSource = Patient;
        }
    }
}