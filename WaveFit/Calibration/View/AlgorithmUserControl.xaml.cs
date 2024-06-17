using System.Windows;
using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para AlgorithmUserControl.xam
    /// </summary>
    public partial class AlgorithmUserControl : UserControl
    {
        private HIDictionaryViewModel hIDictionaryViewModel = new HIDictionaryViewModel();

        public AlgorithmUserControl(string hearingInstrument)
        {
            InitializeComponent();

            switch (hearingInstrument)
            {
                case "Audion16":
                    Audion16ToolVisibility();
                    Audion16ToolValues();
                    break;

                case "Audion8":
                    Audion8ToolVisibility();
                    Audion8ToolValues();
                    break;

                case "Audion6":
                    Audion6ToolVisibility();
                    Audion6ToolValues();
                    break;

                case "Audion4":
                    Audion4ToolVisibility();
                    Audion4ToolValues();
                    break;

                case "SpinNR":
                    SpinNRToolVisibility();
                    SpinNRToolValues();
                    break;
            }
        }

        public void Audion16ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Visible;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion16ToolValues()
        {
            A16AFC.ItemsSource = hIDictionaryViewModel.Audion16FeedbackCanceller.Keys;
            A16ADS.ItemsSource = hIDictionaryViewModel.Audion16AdaptiveDirectionalSensitivity.Keys;
            A16NR.ItemsSource = hIDictionaryViewModel.Audion16NoiseReduction.Keys;
            A16NGA.ItemsSource = hIDictionaryViewModel.Audion16DigitalNoiseGeneratorAmplitude.Keys;
            A16WS.ItemsSource = hIDictionaryViewModel.Audion16WindSuppression.Keys;
        }

        public void Audion16AlgorithmGetValues(int FBC_Enable, int AD_Sens, int Noise_Reduction, int Noise_Level, int Wind_Supression)
        {
            A16AFC.SelectedIndex = FBC_Enable;
            A16ADS.SelectedIndex = AD_Sens;
            A16NR.SelectedIndex = Noise_Reduction;
            A16NGA.SelectedIndex = Noise_Level;
            A16WS.SelectedIndex = Wind_Supression;
        }

        public void Audion16AlgorithmSetValues(ref int FBC_Enable, ref int AD_Sens, ref int Noise_Reduction, ref int Noise_Level, ref int Wind_Supression)
        {
            FBC_Enable = A16AFC.SelectedIndex;
            AD_Sens = A16ADS.SelectedIndex;
            Noise_Reduction = A16NR.SelectedIndex;
            Noise_Level = A16NGA.SelectedIndex;
            Wind_Supression = A16WS.SelectedIndex;
        }

        public void Audion8ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Visible;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion8ToolValues()
        {
            A8AFC.ItemsSource = hIDictionaryViewModel.Audion8AdaptiveFeedbackCanceller.Keys;
            A8ADS.ItemsSource = hIDictionaryViewModel.Audion8AdaptiveDirectionalSensitivity.Keys;
            A8NR.ItemsSource = hIDictionaryViewModel.Audion8NoiseReduction.Keys;
            A8NGA.ItemsSource = hIDictionaryViewModel.Audion8DigitalNoiseGeneratorAmplitude.Keys;
        }

        public void Audion8AlgorithmGetValues(int FBC_Enable, int AD_Sens, int Noise_Reduction, int Noise_Level)
        {
            A8AFC.SelectedIndex = FBC_Enable;
            A8ADS.SelectedIndex = AD_Sens;
            A8NR.SelectedIndex = Noise_Reduction;
            A8NGA.SelectedIndex = Noise_Level;
        }

        public void Audion8AlgorithmSetValues(ref int FBC_Enable, ref int AD_Sens, ref int Noise_Reduction, ref int Noise_Level)
        {
            FBC_Enable = A8AFC.SelectedIndex;
            AD_Sens = A8ADS.SelectedIndex;
            Noise_Reduction = A8NR.SelectedIndex;
            Noise_Level = A8NGA.SelectedIndex;
        }

        public void Audion6ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Visible;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion6ToolValues()
        {
            A6AFC.ItemsSource = hIDictionaryViewModel.Audion6AdaptiveFeedbackCanceller.Keys;
            A6NR.ItemsSource = hIDictionaryViewModel.Audion6NoiseReduction.Keys;
        }

        public void Audion6AlgorithmGetValues(int FBC_Enable, int Noise_Reduction)
        {
            A6AFC.SelectedIndex = FBC_Enable;
            A6NR.SelectedIndex = Noise_Reduction;
        }

        public void Audion6AlgorithmSetValues(ref int FBC_Enable, ref int Noise_Reduction)
        {
            FBC_Enable = A6AFC.SelectedIndex;
            Noise_Reduction = A6NR.SelectedIndex;
        }

        public void Audion4ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Visible;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion4ToolValues()
        {
            A4AFC.ItemsSource = hIDictionaryViewModel.Audion4AdaptiveFeedbackCanceller.Keys;
            A4NR.ItemsSource = hIDictionaryViewModel.Audion4NoiseReduction.Keys;
            A4ME.ItemsSource = hIDictionaryViewModel.Audion4MicrophoneExpansion.Keys;
        }

        public void Audion4AlgorithmGetValues(int FBC_Enable, int Noise_Reduction, int Microfone_Expansion)
        {
            A4AFC.SelectedIndex = FBC_Enable;
            A4NR.SelectedIndex = Noise_Reduction;
            A4ME.SelectedIndex = Microfone_Expansion;
        }

        public void Audion4AlgorithmSetValues(ref int FBC_Enable, ref int Noise_Reduction, ref int Microfone_Expansion)
        {
            FBC_Enable = A4AFC.SelectedIndex;
            Noise_Reduction = A4NR.SelectedIndex;
            Microfone_Expansion = A4ME.SelectedIndex;
        }

        public void SpinNRToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Visible;
        }

        public void SpinNRToolValues()
        {
            SpinNRAFC.ItemsSource = hIDictionaryViewModel.SpinNRAdaptiveFeedbackCanceller.Keys;
            SpinNRNR.ItemsSource = hIDictionaryViewModel.SpinNRNoiseReduction.Keys;
            SpinNRME.ItemsSource = hIDictionaryViewModel.SpinNRMicrophoneExpansion.Keys;
        }

        public void SpinNRAlgorithmGetValues(int FBC_Enable, int Noise_Reduction, int Microfone_Expansion)
        {
            SpinNRAFC.SelectedIndex = FBC_Enable;
            SpinNRNR.SelectedIndex = Noise_Reduction;
            SpinNRME.SelectedIndex = Microfone_Expansion;
        }

        public void SpinNRAlgorithmSetValues(ref int FBC_Enable, ref int Noise_Reduction, ref int Microfone_Expansion)
        {
            FBC_Enable = SpinNRAFC.SelectedIndex;
            Noise_Reduction = SpinNRNR.SelectedIndex;
            Microfone_Expansion = SpinNRME.SelectedIndex;
        }
    }
}