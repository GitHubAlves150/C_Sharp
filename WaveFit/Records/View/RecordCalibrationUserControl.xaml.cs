using System;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Records.ViewModel;

namespace WaveFit2.Records.View
{
    /// <summary>
    /// Interação lógica para RecordCalibrationUserControl.xam
    /// </summary>
    public partial class RecordCalibrationUserControl : UserControl
    {
        public string connectionString = Properties.Settings.Default.connectDB;
        public int SerialNumber, idHearingAid, idPatient, idFitting, currentProgramR, currentProgramL;
        public Crud crudOperations = new Crud();
        public RecordCalibrationViewModel recordCalibrationViewModelR = new RecordCalibrationViewModel();
        public RecordCalibrationViewModel recordCalibrationViewModelL = new RecordCalibrationViewModel();

        public event EventHandler FittinRClicked;

        public event EventHandler FittinLClicked;

        public event EventHandler ViewPlotRClicked;

        public event EventHandler ViewPlotLClicked;

        public RecordCalibrationUserControl()
        {
            InitializeComponent();
            DatagridFittingR.DataContext = recordCalibrationViewModelR;
            DatagridFittingL.DataContext = recordCalibrationViewModelL;
        }

        public void ViewPlotRClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Obtém a linha do DataGrid que contém o botão clicado
            DataGridRow row = (DataGridRow)DatagridFittingR.ItemContainerGenerator.ContainerFromItem(button.DataContext);

            // Obtém o objeto associado à linha (no caso, a classe que representa seus dados)
            CalibrationModel fitting = (CalibrationModel)row.Item;

            // Obtém o valor do código da linha clicada
            idFitting = fitting.Id;
            currentProgramR = fitting.Program;

            Properties.Settings.Default.fittingId = idFitting;
            ViewPlotRClicked?.Invoke(this, EventArgs.Empty);
        }

        public void ViewPlotLClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Obtém a linha do DataGrid que contém o botão clicado
            DataGridRow row = (DataGridRow)DatagridFittingL.ItemContainerGenerator.ContainerFromItem(button.DataContext);

            // Obtém o objeto associado à linha (no caso, a classe que representa seus dados)
            CalibrationModel fitting = (CalibrationModel)row.Item;

            // Obtém o valor do código da linha clicada
            idFitting = fitting.Id;
            currentProgramL = fitting.Program;

            Properties.Settings.Default.fittingId = idFitting;
            ViewPlotLClicked?.Invoke(this, EventArgs.Empty);
        }

        public void FittingRClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Obtém a linha do DataGrid que contém o botão clicado
            DataGridRow row = (DataGridRow)DatagridFittingR.ItemContainerGenerator.ContainerFromItem(button.DataContext);

            // Obtém o objeto associado à linha (no caso, a classe que representa seus dados)
            CalibrationModel fitting = (CalibrationModel)row.Item;

            // Obtém o valor do código da linha clicada
            idFitting = fitting.Id;
            currentProgramR = fitting.Program;

            Properties.Settings.Default.fittingId = idFitting;
            FittinRClicked?.Invoke(this, EventArgs.Empty);
        }

        public void FittingLClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Obtém a linha do DataGrid que contém o botão clicado
            DataGridRow row = (DataGridRow)DatagridFittingL.ItemContainerGenerator.ContainerFromItem(button.DataContext);

            // Obtém o objeto associado à linha (no caso, a classe que representa seus dados)
            CalibrationModel fitting = (CalibrationModel)row.Item;

            // Obtém o valor do código da linha clicada
            idFitting = fitting.Id;
            currentProgramL = fitting.Program;

            Properties.Settings.Default.fittingId = idFitting;
            FittinLClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}