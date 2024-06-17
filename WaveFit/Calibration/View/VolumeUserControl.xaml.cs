using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para VolumeUserControl.xam
    /// </summary>
    public partial class VolumeUserControl : UserControl
    {
        private HIDictionaryViewModel hiDictionaryViewModel = new HIDictionaryViewModel();
        private CalibrationDictionaryViewModel calibrationDictionaryViewModel = new CalibrationDictionaryViewModel();

        public VolumeUserControl(string hearingInstrument)
        {
            InitializeComponent();

            Start();
            VolumeTools();

            if (hearingInstrument == "SpinNR")
            {
                SpinNRToolValues();
                SpinNRVisibility();
            }
            else if (hearingInstrument == "Audion4")
            {
                Audion4ToolValues();
                Audion4Visibility();
            }
            else if (hearingInstrument == "Audion8")
            {
                Audion8ToolValues();
                Audion8Visibility();
            }
        }

        public void Audion8VolumeGetValues(int VC_Mode, int VC_Analog_Range, int VC_Digital_Numsteps, int VC_Digital_Stepsize)
        {
            Audion8VCMode.SelectedIndex = VC_Mode;

            Audion8VCAR.SelectedIndex = VC_Analog_Range;

            Audion8StepNumberSlider.Value = calibrationDictionaryViewModel.Audion8VolumeControlDigitalNumberOfSteps[VC_Digital_Numsteps];

            Audion8StepSizeSlider.Value = calibrationDictionaryViewModel.Audion8VolumeControlDigitalStepSize[VC_Digital_Stepsize];
        }

        public void Audion8VolumeSetValues(ref int VC_Mode, ref int VC_Analog_Range, ref int VC_Digital_Numsteps, ref int VC_Digital_Stepsize)
        {
            VC_Mode = Audion8VCMode.SelectedIndex;

            if (VC_Mode == 0)
            {
                VC_Analog_Range = Audion8VCAR.SelectedIndex;
            }
            else
            {
                VC_Digital_Numsteps = hiDictionaryViewModel.Audion8VolumeControlDigitalNumberOfSteps[(int)Audion8StepNumberSlider.Value];

                VC_Digital_Stepsize = hiDictionaryViewModel.Audion8VolumeControlDigitalStepSize[(int)Audion8StepSizeSlider.Value];
            }
        }

        public void Audion4VolumeGetValues(int VC_Enable, int VC_Beep_Enable, int VC_Mode, int VC_AnalogRange, int VC_DigitalNumSteps, int VC_DigitalStepSize)
        {
            Audion4VCEnable.SelectedIndex = VC_Enable;

            Audion4VCBeep.SelectedIndex = VC_Beep_Enable;

            Audion4VCMode.SelectedIndex = VC_Mode;

            Audion4VCAR.SelectedIndex = VC_AnalogRange;

            Audion4StepNumberSlider.Value = calibrationDictionaryViewModel.Audion8VolumeControlDigitalNumberOfSteps[VC_DigitalNumSteps];

            Audion4StepSizeSlider.Value = calibrationDictionaryViewModel.Audion8VolumeControlDigitalStepSize[VC_DigitalStepSize];
        }

        public void Audion4VolumeSetValues(ref int VC_Enable, ref int VC_Beep_Enable, ref int VC_Mode, ref int VC_AnalogRange, ref int VC_DigitalNumSteps, ref int VC_DigitalStepSize)
        {
            VC_Enable = Audion4VCEnable.SelectedIndex;

            VC_Beep_Enable = Audion4VCBeep.SelectedIndex;

            VC_Mode = Audion4VCMode.SelectedIndex;

            if (VC_Mode == 0)
            {
                VC_AnalogRange = Audion4VCAR.SelectedIndex;
            }
            else
            {
                VC_DigitalNumSteps = hiDictionaryViewModel.Audion4VolumeControlDigitalNumberOfSteps[(int)Audion4StepNumberSlider.Value];

                VC_DigitalStepSize = hiDictionaryViewModel.Audion4VolumeControlDigitalStepSize[(int)Audion4StepSizeSlider.Value];
            }
        }

        public void SpinNRVolumeGetValues(int VC_MAP, int VC_Range)
        {
            SpinNRVCMode.SelectedIndex = VC_MAP;
            SpinNRVCAR.SelectedIndex = VC_Range;
            SpinNRVCRR.SelectedIndex = VC_Range;
        }

        public void SpinNRVolumeSetValues(ref int VC_MAP, ref int VC_Range)
        {
            VC_MAP = SpinNRVCMode.SelectedIndex;

            if (VC_MAP == 0)
            {
                VC_Range = SpinNRVCAR.SelectedIndex;
            }
            else
            {
                VC_Range = SpinNRVCRR.SelectedIndex;
            }
        }

        public void VolumeTools()
        {
            Audion8VCMode.SelectionChanged += Audion8VCMode_SelectionChanged;
            Audion8StepSizeSlider.ValueChanged += Audion8StepSizeSlider_ValueChanged;
            Audion8StepNumberSlider.ValueChanged += Audion8StepNumberSlider_ValueChanged;

            Audion4VCEnable.SelectionChanged += Audion4VCEnable_SelectionChanged;
            Audion4VCMode.SelectionChanged += Audion4VCMode_SelectionChanged;
            Audion4StepSizeSlider.ValueChanged += Audion4StepSizeSlider_ValueChanged;
            Audion4StepNumberSlider.ValueChanged += Audion4StepNumberSlider_ValueChanged;

            SpinNRVCMode.SelectionChanged += SpinNRVCMode_SelectionChanged;
        }

        private void Audion4StepNumberSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ValueAudion4StepNumber.Text = Audion4StepNumberSlider.Value.ToString();
        }

        private void Audion4StepSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ValueAudion4StepSize.Text = Audion4StepSizeSlider.Value.ToString();
        }

        private void Audion8StepNumberSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ValueAudion8StepNumber.Text = Audion8StepNumberSlider.Value.ToString();
        }

        private void Audion8StepSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ValueAudion8StepSize.Text = Audion8StepSizeSlider.Value.ToString();
        }

        private void SpinNRVCMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpinNRVisibility();
        }

        private void Audion4VCMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Audion4Visibility();
        }

        private void Audion4VCEnable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Audion4Visibility();
        }

        private void Audion8VCMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Audion8Visibility();
        }

        public void Start()
        {
            Audion8VCMode.SelectedIndex = 0;

            Audion4VCEnable.SelectedIndex = 1;
            Audion4VCBeep.SelectedIndex = 0;
            Audion4VCMode.SelectedIndex = 0;

            SpinNRVCMode.SelectedIndex = 0;
        }

        public void Audion8ToolValues()
        {
            Audion8VCMode.ItemsSource = hiDictionaryViewModel.Audion8VolumeControlMode.Keys;
            Audion8VCAR.ItemsSource = hiDictionaryViewModel.Audion8VolumeControlAnalogRange.Keys;

            Audion8StepSizeSlider.Maximum = hiDictionaryViewModel.Audion8VolumeControlDigitalStepSize.Keys.Max();
            Audion8StepSizeSlider.Minimum = hiDictionaryViewModel.Audion8VolumeControlDigitalStepSize.Keys.Min();
            Audion8StepSizeSlider.TickFrequency = 1;

            Audion8StepNumberSlider.Maximum = hiDictionaryViewModel.Audion8VolumeControlDigitalNumberOfSteps.Keys.Max();
            Audion8StepNumberSlider.Minimum = hiDictionaryViewModel.Audion8VolumeControlDigitalNumberOfSteps.Keys.Min();
            Audion8StepNumberSlider.TickFrequency = 5;
        }

        public void Audion4ToolValues()
        {
            Audion4VCEnable.ItemsSource = hiDictionaryViewModel.Audion4VolumeControlEnable.Keys;
            Audion4VCBeep.ItemsSource = hiDictionaryViewModel.Audion4VolumeControlBeepEnable.Keys;
            Audion4VCMode.ItemsSource = hiDictionaryViewModel.Audion4VolumeControlMode.Keys;
            Audion4VCAR.ItemsSource = hiDictionaryViewModel.Audion4VolumeControlAnalogRange.Keys;

            Audion4StepSizeSlider.Maximum = hiDictionaryViewModel.Audion4VolumeControlDigitalStepSize.Keys.Max();
            Audion4StepSizeSlider.Minimum = hiDictionaryViewModel.Audion4VolumeControlDigitalStepSize.Keys.Min();
            Audion4StepSizeSlider.TickFrequency = 1;

            Audion4StepNumberSlider.Maximum = hiDictionaryViewModel.Audion4VolumeControlDigitalNumberOfSteps.Keys.Max();
            Audion4StepNumberSlider.Minimum = hiDictionaryViewModel.Audion4VolumeControlDigitalNumberOfSteps.Keys.Min();
            Audion4StepNumberSlider.TickFrequency = 5;
        }

        public void SpinNRToolValues()
        {
            SpinNRVCMode.ItemsSource = hiDictionaryViewModel.SpinNRVolumeControlMode.Keys;
            SpinNRVCAR.ItemsSource = hiDictionaryViewModel.SpinNRVolumeControlAnalogRange.Keys;
            SpinNRVCRR.ItemsSource = hiDictionaryViewModel.SpinNRVolumeControlRockerRange.Keys;
        }

        public void Audion8Visibility()
        {
            Audion4VC.Visibility = Visibility.Collapsed;
            SpinNRVC.Visibility = Visibility.Collapsed;

            Audion8VC.Visibility = Visibility.Visible;
            if (Audion8VCMode.SelectedValue == "Analog VC Enabled")
            {
                Audion8Analog.Visibility = Visibility.Visible;
                Audion8Digital.Visibility = Visibility.Collapsed;
            }
            else
            {
                Audion8Analog.Visibility = Visibility.Collapsed;
                Audion8Digital.Visibility = Visibility.Visible;
            }
        }

        public void Audion4Visibility()
        {
            Audion8VC.Visibility = Visibility.Collapsed;
            SpinNRVC.Visibility = Visibility.Collapsed;

            Audion4VC.Visibility = Visibility.Visible;
            if (Audion4VCEnable.SelectedValue == "Enabled")
            {
                Audion4Mode.Visibility = Visibility.Visible;
                if (Audion4VCMode.SelectedValue == "Analog VC Enabled")
                {
                    Audion4Analog.Visibility = Visibility.Visible;
                    Audion4Digital.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Audion4Analog.Visibility = Visibility.Collapsed;
                    Audion4Digital.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Audion4Mode.Visibility = Visibility.Collapsed;
                Audion4Analog.Visibility = Visibility.Collapsed;
                Audion4Digital.Visibility = Visibility.Collapsed;
            }
        }

        public void SpinNRVisibility()
        {
            Audion8VC.Visibility = Visibility.Collapsed;
            Audion4VC.Visibility = Visibility.Collapsed;

            SpinNRVC.Visibility = Visibility.Visible;
            if (SpinNRVCMode.SelectedValue == "Analog VC")
            {
                SpinNRAnalog.Visibility = Visibility.Visible;
                SpinNRRocker.Visibility = Visibility.Collapsed;
            }
            else
            {
                SpinNRAnalog.Visibility = Visibility.Collapsed;
                SpinNRRocker.Visibility = Visibility.Visible;
            }
        }
    }
}