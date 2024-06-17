using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para MicrophoneUserControl.xam
    /// </summary>
    public partial class MicrophoneUserControl : UserControl
    {
        private HIDictionaryViewModel hiDictionaryViewModel = new HIDictionaryViewModel();
        private CalibrationDictionaryViewModel calibrationDictionaryViewModel = new CalibrationDictionaryViewModel();

        public MicrophoneUserControl(string hearingInstrument)
        {
            InitializeComponent();
            FrontEndToolActions();

            if (hearingInstrument == "SpinNR")
            {
                SpinNRToolValues();
            }
            else if (hearingInstrument == "Audion4")
            {
                Audion4ToolValues();
            }
            else if (hearingInstrument == "Audion6")
            {
                Audion6ToolValues();
            }
            else if (hearingInstrument == "Audion8")
            {
                Audion8ToolValues();
            }
            else if (hearingInstrument == "Audion16")
            {
                Audion16ToolValues();
            }
        }

        public void FrontEndToolActions()
        {
            SliderMatrixGain.Value.ToString();
            SliderMatrixGain.ValueChanged += SliderMatrixGain_ValueChanged;
        }

        private void SliderMatrixGain_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ValueSliderMatrixGain.Text = SliderMatrixGain.Value.ToString();
        }

        public void Audion16ToolValues()
        {
            Microphone.ItemsSource = hiDictionaryViewModel.Audion16InputMultiplexer.Keys;
            SliderMatrixGain.Maximum = 0;
            SliderMatrixGain.Minimum = hiDictionaryViewModel.Audion16MatrixGain.Keys.Min();
            SliderMatrixGain.TickFrequency = 1;
        }

        public void Audion16MicrophoneGetValues(int input_mux, int matrix_gain)
        {
            try
            {
                Microphone.SelectedIndex = calibrationDictionaryViewModel.Audion16InputMultiplexer[input_mux];
                SliderMatrixGain.Value = calibrationDictionaryViewModel.Audion16MatrixGain[matrix_gain];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Audion16MicrophoneSetValues(ref int input_mux, ref int matrix_gain)
        {
            try
            {
                input_mux = hiDictionaryViewModel.Audion16InputMultiplexer[Microphone.SelectedValue.ToString()];
                matrix_gain = hiDictionaryViewModel.Audion16MatrixGain[(int)SliderMatrixGain.Value];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Audion8ToolValues()
        {
            Microphone.ItemsSource = hiDictionaryViewModel.Audion8InputMultiplexer.Keys;
            SliderMatrixGain.Maximum = 0;
            SliderMatrixGain.Minimum = hiDictionaryViewModel.Audion8MatrixGain.Keys.Min();
            SliderMatrixGain.TickFrequency = 1;
        }

        public void Audion8MicrophoneGetValues(int input_mux, int matrix_gain)
        {
            try
            {
                Microphone.SelectedIndex = calibrationDictionaryViewModel.Audion8InputMultiplexer[input_mux];
                SliderMatrixGain.Value = calibrationDictionaryViewModel.Audion8MatrixGain[matrix_gain];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Audion8MicrophoneSetValues(ref int input_mux, ref int matrix_gain)
        {
            try
            {
                input_mux = hiDictionaryViewModel.Audion8InputMultiplexer[Microphone.SelectedValue.ToString()];
                matrix_gain = hiDictionaryViewModel.Audion8MatrixGain[(int)SliderMatrixGain.Value];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Audion6ToolValues()
        {
            Microphone.ItemsSource = hiDictionaryViewModel.Audion6InputMultiplexer.Keys;
            SliderMatrixGain.Maximum = 0;
            SliderMatrixGain.Minimum = hiDictionaryViewModel.Audion6MatrixGain.Keys.Min();
            SliderMatrixGain.TickFrequency = 1;
        }

        public void Audion6MicrophoneGetValues(int input_mux, int matrix_gain)
        {
            try
            {
                Microphone.SelectedIndex = calibrationDictionaryViewModel.Audion6InputMultiplexer[input_mux];
                SliderMatrixGain.Value = calibrationDictionaryViewModel.Audion6MatrixGain[matrix_gain];
            }
            catch (Exception ex)
            {
                Microphone.SelectedIndex = 0;
                SliderMatrixGain.Value = 0;
                Console.WriteLine(ex.Message);
            }
        }

        public void Audion6MicrophoneSetValues(ref int input_mux, ref int matrix_gain)
        {
            try
            {
                input_mux = hiDictionaryViewModel.Audion6InputMultiplexer[Microphone.SelectedValue.ToString()];
                matrix_gain = hiDictionaryViewModel.Audion6MatrixGain[(int)SliderMatrixGain.Value];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Audion4ToolValues()
        {
            Microphone.ItemsSource = hiDictionaryViewModel.Audion4InputMultiplexer.Keys;
            SliderMatrixGain.Maximum = 0;
            SliderMatrixGain.Minimum = hiDictionaryViewModel.Audion4MatrixGain.Keys.Min();
            SliderMatrixGain.TickFrequency = 1;
        }

        public void Audion4MicrophoneGetValues(int input_mux, int matrix_gain)
        {
            try
            {
                Microphone.SelectedIndex = calibrationDictionaryViewModel.Audion4InputMultiplexer[input_mux];
                SliderMatrixGain.Value = calibrationDictionaryViewModel.Audion4MatrixGain[matrix_gain];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Audion4MicrophoneSetValues(ref int input_mux, ref int matrix_gain)
        {
            try
            {
                input_mux = hiDictionaryViewModel.Audion4InputMultiplexer[Microphone.SelectedValue.ToString()];
                matrix_gain = hiDictionaryViewModel.Audion4MatrixGain[(int)SliderMatrixGain.Value];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SpinNRToolValues()
        {
            Microphone.ItemsSource = hiDictionaryViewModel.SpinNRInputMultiplexer.Keys;
            SliderMatrixGain.Maximum = 0;
            SliderMatrixGain.Minimum = hiDictionaryViewModel.SpinNRMatrixGain.Keys.Min();
            SliderMatrixGain.TickFrequency = 1;
        }

        public void SpinNRMicrophoneGetValues(int input_mux, int matrix_gain)
        {
            try
            {
                Microphone.SelectedIndex = calibrationDictionaryViewModel.SpinNRInputMultiplexer[input_mux];
                SliderMatrixGain.Value = calibrationDictionaryViewModel.SpinNRMatrixGain[matrix_gain];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SpinNRMicrophoneSetValues(ref int input_mux, ref int matrix_gain)
        {
            try
            {
                input_mux = hiDictionaryViewModel.SpinNRInputMultiplexer[Microphone.SelectedValue.ToString()];
                matrix_gain = hiDictionaryViewModel.SpinNRMatrixGain[(int)SliderMatrixGain.Value];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}