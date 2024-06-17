using System;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Records.ViewModel;

namespace WaveFit2.Records.View
{
    /// <summary>
    /// Interação lógica para RecordAudiogramUserControl.xam
    /// </summary>
    public partial class RecordAudiogramUserControl : UserControl
    {
        public string connectionString = Properties.Settings.Default.connectDB;
        public int idAudiogram, idUser, idPatient, idFrequency;

        public Crud crudOperations = new Crud();
        public RecordAudiogramViewModel recordAudiogramViewModel = new RecordAudiogramViewModel();

        public event EventHandler AudiographViewClicked;

        public event EventHandler AudiogramViewClicked;

        public event EventHandler AudiogramPrintClicked;

        public RecordAudiogramUserControl()
        {
            InitializeComponent();
            //DatagridAudiograms.DataContext = recordAudiogramViewModel;
        }

        public void AudiographViewClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Obtém a linha do DataGrid que contém o botão clicado
            DataGridRow row = (DataGridRow)DatagridAudiograms.ItemContainerGenerator.ContainerFromItem(button.DataContext);

            // Obtém o objeto associado à linha (no caso, a classe que representa seus dados)
            AudiogramModel code = (AudiogramModel)row.Item;

            // Obtém o valor do código da linha clicada
            idAudiogram = code.Id;

            Properties.Settings.Default.audiogramId = idAudiogram;

            AudiographViewClicked?.Invoke(this, EventArgs.Empty);
        }

        public void AudiogramViewClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Obtém a linha do DataGrid que contém o botão clicado
            DataGridRow row = (DataGridRow)DatagridAudiograms.ItemContainerGenerator.ContainerFromItem(button.DataContext);

            // Obtém o objeto associado à linha (no caso, a classe que representa seus dados)
            AudiogramModel code = (AudiogramModel)row.Item;

            // Obtém o valor do código da linha clicada
            idAudiogram = code.Id;

            Properties.Settings.Default.audiogramId = idAudiogram;

            AudiogramViewClicked?.Invoke(this, EventArgs.Empty);
        }

        public void AudiogramPrintClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Obtém a linha do DataGrid que contém o botão clicado
            DataGridRow row = (DataGridRow)DatagridAudiograms.ItemContainerGenerator.ContainerFromItem(button.DataContext);

            // Obtém o objeto associado à linha (no caso, a classe que representa seus dados)
            AudiogramModel code = (AudiogramModel)row.Item;

            // Obtém o valor do código da linha clicada
            idAudiogram = code.Id;

            Properties.Settings.Default.audiogramId = idAudiogram;

            AudiogramPrintClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}