using System.Windows;
using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para FilterUserControl.xam
    /// </summary>
    public partial class FilterUserControl : UserControl
    {
        private HIDictionaryViewModel hiDictionaryViewModel = new HIDictionaryViewModel();

        public FilterUserControl(string hearingInstrument)
        {
            InitializeComponent();

            if (hearingInstrument == "SpinNR")
            {
                SpinNRVisibility();
                SpinNRToolValues();
            }
            else if (hearingInstrument == "Audion4")
            {
                Audion4Visibility();
                Audion4ToolValues();
            }
        }

        public void Audion4FilterGetValues(int Low_Cut, int High_Cut)
        {
            A4FPB.SelectedIndex = Low_Cut;
            A4FPA.SelectedIndex = High_Cut;
        }

        public void Audion4FilterSetValues(ref int Low_Cut, ref int High_Cut)
        {
            Low_Cut = A4FPB.SelectedIndex;
            High_Cut = A4FPA.SelectedIndex;
        }

        public void SpinNRFilterGetValues(int Low_Cut, int High_Cut)
        {
            SpinNRFPB.SelectedIndex = Low_Cut;
            SpinNRFPA.SelectedIndex = High_Cut;
        }

        public void SpinNRFilterSetValues(ref int Low_Cut, ref int High_Cut)
        {
            Low_Cut = SpinNRFPB.SelectedIndex;
            High_Cut = SpinNRFPA.SelectedIndex;
        }

        public void Audion4ToolValues()
        {
            A4FPA.ItemsSource = hiDictionaryViewModel.Audion4HighCutFilter.Keys;
            A4FPB.ItemsSource = hiDictionaryViewModel.Audion4LowCutFilter.Keys;
        }

        public void Audion4Visibility()
        {
            Audion4Grid.Visibility = Visibility.Visible;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void SpinNRToolValues()
        {
            SpinNRFPA.ItemsSource = hiDictionaryViewModel.SpinNRHighCutFilter.Keys;
            SpinNRFPB.ItemsSource = hiDictionaryViewModel.SpinNRLowCutFilter.Keys;
        }

        public void SpinNRVisibility()
        {
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Visible;
        }
    }
}