using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para GainPlotUserControl.xam
    /// </summary>
    public partial class GainPlotUserControl : UserControl
    {
        public GainPlotViewModel gainPlotViewModel = new GainPlotViewModel();

        public GainPlotUserControl()
        {
            InitializeComponent();
            gainPlotViewModel.SetupPlot();

            DataContext = gainPlotViewModel;
        }

        private string FrequencyFormatter(double Frequency)
        {
            if (Frequency < 1000) return Frequency.ToString();
            else return (Frequency / 1000).ToString() + "k";
        }
    }
}