using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Records.ViewModel
{
    public class RecordCalibrationViewModel : Crud
    {
        public ObservableCollection<CalibrationModel> Fitting { get; set; } = new ObservableCollection<CalibrationModel>();
        public ObservableCollection<CalibrationModel> emptyFitting { get; set; } = new ObservableCollection<CalibrationModel>();

        public List<CalibrationModel> dataFromDatabase = new List<CalibrationModel>();

        public void LoadFittingToDatagrid(DataGrid dataGrid, int idPatient, int idDevice, string channel, DateTime date)
        {
            try
            {
                Fitting.Clear();

                dataFromDatabase = LoadFittingDataFromDatabase(idPatient, idDevice, channel, date);

                foreach (CalibrationModel item in dataFromDatabase)
                {
                    Fitting.Add(item);
                }
                dataGrid.ItemsSource = Fitting;
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Date: {ex.Message}");
            }
        }
    }
}