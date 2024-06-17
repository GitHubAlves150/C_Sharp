using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Records.ViewModel
{
    public class RecordAudiogramViewModel : Crud
    {
        public ObservableCollection<AudiogramModel> Audiogram { get; set; } = new ObservableCollection<AudiogramModel>();
        public ObservableCollection<AudiogramModel> emptyAudiogram { get; set; } = new ObservableCollection<AudiogramModel>();

        public List<AudiogramModel> dataFromDatabase = new List<AudiogramModel>();

        public void LoadAudiogramToDatagrid(DataGrid dataGrid, int idPatient)
        {
            try
            {
                Audiogram.Clear();

                dataFromDatabase = LoadAudiogramDataFromDatabase(idPatient);

                foreach (AudiogramModel item in dataFromDatabase)
                {
                    Audiogram.Add(item);
                }
                dataGrid.ItemsSource = Audiogram;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadEmptyAudiogramToDatagrid(DataGrid dataGrid)
        {
            try
            {
                emptyAudiogram.Clear();
                dataGrid.ItemsSource = emptyAudiogram;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}