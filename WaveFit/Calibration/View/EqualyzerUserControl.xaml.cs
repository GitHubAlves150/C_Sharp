using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para EqualyzerUserControl.xam
    /// </summary>
    public partial class EqualyzerUserControl : UserControl
    {
        private HIDictionaryViewModel hiDictionaryViewModel = new HIDictionaryViewModel();
        private CalibrationDictionaryViewModel calibrationDictionaryViewModel = new CalibrationDictionaryViewModel();

        public EqualyzerUserControl(string hearingInstrument)
        {
            InitializeComponent();
            EqualyzerToolActions();

            if (hearingInstrument == "SpinNR")
            {
                SpinNRSlider();
                LegacyVisibility();
            }
            else if (hearingInstrument == "Audion4")
            {
                Audion4Slider();
                LegacyVisibility();
            }
            else if (hearingInstrument == "Audion6")
            {
                Audion6Slider();
                LegacyVisibility();
            }
            else if (hearingInstrument == "Audion8")
            {
                Audion8Slider();
                LegacyVisibility();
            }
            else if (hearingInstrument == "Audion16")
            {
                Audion16Slider();
                GenericVisibility();
            }
        }

        public void Audion16EqualyzerGetValues(int BEQ1_gain, int BEQ2_gain, int BEQ3_gain, int BEQ4_gain, int BEQ5_gain, int BEQ6_gain,
                             int BEQ7_gain, int BEQ8_gain, int BEQ9_gain, int BEQ10_gain, int BEQ11_gain, int BEQ12_gain, int BEQ13_gain,
                             int BEQ14_gain, int BEQ15_gain, int BEQ16_gain)
        {
            A16Slider0.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ1_gain];

            A16Slider500Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ2_gain];

            A16Slider1000Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ3_gain];

            A16Slider1500Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ4_gain];

            A16Slider2000Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ5_gain];

            A16Slider2500Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ6_gain];

            A16Slider3000Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ7_gain];

            A16Slider3500Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ8_gain];

            A16Slider4000Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ9_gain];

            A16Slider4500Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ10_gain];

            A16Slider5000Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ11_gain];

            A16Slider5500Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ12_gain];

            A16Slider6000Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ13_gain];

            A16Slider6500Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ14_gain];

            A16Slider7000Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ15_gain];

            A16Slider7500Hz.Value = calibrationDictionaryViewModel.Audion16BandEqualizationFilter[BEQ16_gain];
        }

        public void Audion16EqualyzerSetValues(ref int BEQ1_gain, ref int BEQ2_gain, ref int BEQ3_gain, ref int BEQ4_gain, ref int BEQ5_gain, ref int BEQ6_gain,
                                              ref int BEQ7_gain, ref int BEQ8_gain, ref int BEQ9_gain, ref int BEQ10_gain, ref int BEQ11_gain, ref int BEQ12_gain,
                                              ref int BEQ13_gain, ref int BEQ14_gain, ref int BEQ15_gain, ref int BEQ16_gain)
        {
            BEQ1_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider0.Value];

            BEQ2_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider500Hz.Value];

            BEQ3_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider1000Hz.Value];

            BEQ4_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider1500Hz.Value];

            BEQ5_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider2000Hz.Value];

            BEQ6_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider2500Hz.Value];

            BEQ7_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider3000Hz.Value];

            BEQ8_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider3500Hz.Value];

            BEQ9_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider4000Hz.Value];

            BEQ10_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider4500Hz.Value];

            BEQ11_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider5000Hz.Value];

            BEQ12_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider5500Hz.Value];

            BEQ13_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider6000Hz.Value];

            BEQ14_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider6500Hz.Value];

            BEQ15_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider7000Hz.Value];

            BEQ16_gain = hiDictionaryViewModel.Audion16BandEqualizationFilter[(int)A16Slider7500Hz.Value];
        }

        public void Audion8EqualyzerGetValues(int BEQ1_gain, int BEQ2_gain, int BEQ3_gain, int BEQ4_gain, int BEQ5_gain, int BEQ6_gain,
                                     int BEQ7_gain, int BEQ8_gain, int BEQ9_gain, int BEQ10_gain, int BEQ11_gain, int BEQ12_gain)
        {
            SliderDC.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ1_gain];

            Slider500Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ2_gain];

            Slider1000Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ3_gain];

            Slider1500Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ4_gain];

            Slider2000Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ5_gain];

            Slider2500Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ6_gain];

            Slider3000Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ7_gain];

            Slider3500Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ8_gain];

            Slider4250Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ9_gain];

            Slider5250Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ10_gain];

            Slider6250Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ11_gain];

            Slider7250Hz.Value = calibrationDictionaryViewModel.Audion8BandEqualizationFilter[BEQ12_gain];
        }

        public void Audion8EqualyzerSetValues(ref int BEQ1_gain, ref int BEQ2_gain, ref int BEQ3_gain, ref int BEQ4_gain, ref int BEQ5_gain, ref int BEQ6_gain,
                                              ref int BEQ7_gain, ref int BEQ8_gain, ref int BEQ9_gain, ref int BEQ10_gain, ref int BEQ11_gain, ref int BEQ12_gain)
        {
            BEQ1_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)SliderDC.Value];

            BEQ2_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider500Hz.Value];

            BEQ3_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider1000Hz.Value];

            BEQ4_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider1500Hz.Value];

            BEQ5_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider2000Hz.Value];

            BEQ6_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider2500Hz.Value];

            BEQ7_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider3000Hz.Value];

            BEQ8_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider3500Hz.Value];

            BEQ9_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider4250Hz.Value];

            BEQ10_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider5250Hz.Value];

            BEQ11_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider6250Hz.Value];

            BEQ12_gain = hiDictionaryViewModel.Audion8BandEqualizationFilter[(int)Slider7250Hz.Value];
        }

        public void Audion6EqualyzerGetValues(int BEQ1_gain, int BEQ2_gain, int BEQ3_gain, int BEQ4_gain, int BEQ5_gain, int BEQ6_gain,
                             int BEQ7_gain, int BEQ8_gain, int BEQ9_gain, int BEQ10_gain, int BEQ11_gain, int BEQ12_gain)
        {
            SliderDC.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ1_gain];

            Slider500Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ2_gain];

            Slider1000Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ3_gain];

            Slider1500Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ4_gain];

            Slider2000Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ5_gain];

            Slider2500Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ6_gain];

            Slider3000Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ7_gain];

            Slider3500Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ8_gain];

            Slider4250Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ9_gain];

            Slider5250Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ10_gain];

            Slider6250Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ11_gain];

            Slider7250Hz.Value = calibrationDictionaryViewModel.Audion6BandEqualizationFilter[BEQ12_gain];
        }

        public void Audion6EqualyzerSetValues(ref int BEQ1_gain, ref int BEQ2_gain, ref int BEQ3_gain, ref int BEQ4_gain, ref int BEQ5_gain, ref int BEQ6_gain,
                                              ref int BEQ7_gain, ref int BEQ8_gain, ref int BEQ9_gain, ref int BEQ10_gain, ref int BEQ11_gain, ref int BEQ12_gain)
        {
            BEQ1_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)SliderDC.Value];

            BEQ2_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider500Hz.Value];

            BEQ3_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider1000Hz.Value];

            BEQ4_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider1500Hz.Value];

            BEQ5_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider2000Hz.Value];

            BEQ6_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider2500Hz.Value];

            BEQ7_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider3000Hz.Value];

            BEQ8_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider3500Hz.Value];

            BEQ9_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider4250Hz.Value];

            BEQ10_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider5250Hz.Value];

            BEQ11_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider6250Hz.Value];

            BEQ12_gain = hiDictionaryViewModel.Audion6BandEqualizationFilter[(int)Slider7250Hz.Value];
        }

        public void Audion4EqualyzerGetValues(int BEQ1_gain, int BEQ2_gain, int BEQ3_gain, int BEQ4_gain, int BEQ5_gain, int BEQ6_gain,
                     int BEQ7_gain, int BEQ8_gain, int BEQ9_gain, int BEQ10_gain, int BEQ11_gain, int BEQ12_gain)
        {
            SliderDC.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ1_gain];

            Slider500Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ2_gain];

            Slider1000Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ3_gain];

            Slider1500Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ4_gain];

            Slider2000Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ5_gain];

            Slider2500Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ6_gain];

            Slider3000Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ7_gain];

            Slider3500Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ8_gain];

            Slider4250Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ9_gain];

            Slider5250Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ10_gain];

            Slider6250Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ11_gain];

            Slider7250Hz.Value = calibrationDictionaryViewModel.Audion4BandEqualizationFilter[BEQ12_gain];
        }

        public void Audion4EqualyzerSetValues(ref int BEQ1_gain, ref int BEQ2_gain, ref int BEQ3_gain, ref int BEQ4_gain, ref int BEQ5_gain, ref int BEQ6_gain,
                                              ref int BEQ7_gain, ref int BEQ8_gain, ref int BEQ9_gain, ref int BEQ10_gain, ref int BEQ11_gain, ref int BEQ12_gain)
        {
            BEQ1_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)SliderDC.Value];

            BEQ2_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider500Hz.Value];

            BEQ3_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider1000Hz.Value];

            BEQ4_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider1500Hz.Value];

            BEQ5_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider2000Hz.Value];

            BEQ6_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider2500Hz.Value];

            BEQ7_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider3000Hz.Value];

            BEQ8_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider3500Hz.Value];

            BEQ9_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider4250Hz.Value];

            BEQ10_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider5250Hz.Value];

            BEQ11_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider6250Hz.Value];

            BEQ12_gain = hiDictionaryViewModel.Audion4BandEqualizationFilter[(int)Slider7250Hz.Value];
        }

        public void SpinNREqualyzerGetValues(int BEQ1_gain, int BEQ2_gain, int BEQ3_gain, int BEQ4_gain, int BEQ5_gain, int BEQ6_gain,
                             int BEQ7_gain, int BEQ8_gain, int BEQ9_gain, int BEQ10_gain, int BEQ11_gain, int BEQ12_gain)
        {
            SliderDC.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ1_gain];

            Slider500Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ2_gain];

            Slider1000Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ3_gain];

            Slider1500Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ4_gain];

            Slider2000Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ5_gain];

            Slider2500Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ6_gain];

            Slider3000Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ7_gain];

            Slider3500Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ8_gain];

            Slider4250Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ9_gain];

            Slider5250Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ10_gain];

            Slider6250Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ11_gain];

            Slider7250Hz.Value = calibrationDictionaryViewModel.SpinNRBandEqualizationFilter[BEQ12_gain];
        }

        public void SpinNREqualyzerSetValues(ref int BEQ1_gain, ref int BEQ2_gain, ref int BEQ3_gain, ref int BEQ4_gain, ref int BEQ5_gain, ref int BEQ6_gain,
                                              ref int BEQ7_gain, ref int BEQ8_gain, ref int BEQ9_gain, ref int BEQ10_gain, ref int BEQ11_gain, ref int BEQ12_gain)
        {
            BEQ1_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)SliderDC.Value];

            BEQ2_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider500Hz.Value];

            BEQ3_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider1000Hz.Value];

            BEQ4_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider1500Hz.Value];

            BEQ5_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider2000Hz.Value];

            BEQ6_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider2500Hz.Value];

            BEQ7_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider3000Hz.Value];

            BEQ8_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider3500Hz.Value];

            BEQ9_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider4250Hz.Value];

            BEQ10_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider5250Hz.Value];

            BEQ11_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider6250Hz.Value];

            BEQ12_gain = hiDictionaryViewModel.SpinNRBandEqualizationFilter[(int)Slider7250Hz.Value];
        }

        public void EqualyzerToolActions()
        {
            SliderDC.ValueChanged += Slider_ValueChanged;
            Slider500Hz.ValueChanged += Slider_ValueChanged;
            Slider1000Hz.ValueChanged += Slider_ValueChanged;
            Slider1500Hz.ValueChanged += Slider_ValueChanged;
            Slider2000Hz.ValueChanged += Slider_ValueChanged;
            Slider2500Hz.ValueChanged += Slider_ValueChanged;
            Slider3000Hz.ValueChanged += Slider_ValueChanged;
            Slider3500Hz.ValueChanged += Slider_ValueChanged;
            Slider4250Hz.ValueChanged += Slider_ValueChanged;
            Slider5250Hz.ValueChanged += Slider_ValueChanged;
            Slider6250Hz.ValueChanged += Slider_ValueChanged;
            Slider7250Hz.ValueChanged += Slider_ValueChanged;

            A16Slider0.ValueChanged += Slider_ValueChanged;
            A16Slider500Hz.ValueChanged += Slider_ValueChanged;
            A16Slider1000Hz.ValueChanged += Slider_ValueChanged;
            A16Slider1500Hz.ValueChanged += Slider_ValueChanged;
            A16Slider2000Hz.ValueChanged += Slider_ValueChanged;
            A16Slider2500Hz.ValueChanged += Slider_ValueChanged;
            A16Slider3000Hz.ValueChanged += Slider_ValueChanged;
            A16Slider3500Hz.ValueChanged += Slider_ValueChanged;
            A16Slider4000Hz.ValueChanged += Slider_ValueChanged;
            A16Slider4500Hz.ValueChanged += Slider_ValueChanged;
            A16Slider5000Hz.ValueChanged += Slider_ValueChanged;
            A16Slider5500Hz.ValueChanged += Slider_ValueChanged;
            A16Slider6000Hz.ValueChanged += Slider_ValueChanged;
            A16Slider6500Hz.ValueChanged += Slider_ValueChanged;
            A16Slider7000Hz.ValueChanged += Slider_ValueChanged;
            A16Slider7500Hz.ValueChanged += Slider_ValueChanged;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            TextBlock textBlock = FindName(slider.Name.Replace("Slider", "Value")) as TextBlock;

            if (textBlock != null)
            {
                textBlock.Text = slider.Value.ToString() + "dB";
            }
        }

        public void Audion16Slider()
        {
            A16Slider0.Maximum = 0;
            A16Slider0.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value0.Text = A16Slider0.Value.ToString() + "dB";

            A16Slider500Hz.Maximum = 0;
            A16Slider500Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value500Hz.Text = A16Slider500Hz.Value.ToString() + "dB";

            A16Slider1000Hz.Maximum = 0;
            A16Slider1000Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value1000Hz.Text = A16Slider1000Hz.Value.ToString() + "dB";

            A16Slider1500Hz.Maximum = 0;
            A16Slider1500Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value1500Hz.Text = A16Slider1500Hz.Value.ToString() + "dB";

            A16Slider2000Hz.Maximum = 0;
            A16Slider2000Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value2000Hz.Text = A16Slider2000Hz.Value.ToString() + "dB";

            A16Slider2500Hz.Maximum = 0;
            A16Slider2500Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value2500Hz.Text = A16Slider2500Hz.Value.ToString() + "dB";

            A16Slider3000Hz.Maximum = 0;
            A16Slider3000Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value3000Hz.Text = A16Slider3000Hz.Value.ToString() + "dB";

            A16Slider3500Hz.Maximum = 0;
            A16Slider3500Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value3500Hz.Text = A16Slider3500Hz.Value.ToString() + "dB";

            A16Slider4000Hz.Maximum = 0;
            A16Slider4000Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value4000Hz.Text = A16Slider4000Hz.Value.ToString() + "dB";

            A16Slider4500Hz.Maximum = 0;
            A16Slider4500Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value4500Hz.Text = A16Slider4500Hz.Value.ToString() + "dB";

            A16Slider5000Hz.Maximum = 0;
            A16Slider5000Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value5000Hz.Text = A16Slider5000Hz.Value.ToString() + "dB";

            A16Slider5500Hz.Maximum = 0;
            A16Slider5500Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value5500Hz.Text = A16Slider5500Hz.Value.ToString() + "dB";

            A16Slider6000Hz.Maximum = 0;
            A16Slider6000Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value6000Hz.Text = A16Slider6000Hz.Value.ToString() + "dB";

            A16Slider6500Hz.Maximum = 0;
            A16Slider6500Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value6500Hz.Text = A16Slider6500Hz.Value.ToString() + "dB";

            A16Slider7000Hz.Maximum = 0;
            A16Slider7000Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value7000Hz.Text = A16Slider7000Hz.Value.ToString() + "dB";

            A16Slider7500Hz.Maximum = 0;
            A16Slider7500Hz.Minimum = hiDictionaryViewModel.Audion16BandEqualizationFilter.Keys.Min();
            A16Value7500Hz.Text = A16Slider7500Hz.Value.ToString() + "dB";
        }

        public void Audion8Slider()
        {
            SliderDC.Maximum = 0;
            SliderDC.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            ValueDC.Text = SliderDC.Value.ToString() + "dB";

            Slider500Hz.Maximum = 0;
            Slider500Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value500Hz.Text = Slider500Hz.Value.ToString() + "dB";

            Slider1000Hz.Maximum = 0;
            Slider1000Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value1000Hz.Text = Slider1000Hz.Value.ToString() + "dB";

            Slider1500Hz.Maximum = 0;
            Slider1500Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value1500Hz.Text = Slider1500Hz.Value.ToString() + "dB";

            Slider2000Hz.Maximum = 0;
            Slider2000Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value2000Hz.Text = Slider2000Hz.Value.ToString() + "dB";

            Slider2500Hz.Maximum = 0;
            Slider2500Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value2500Hz.Text = Slider2500Hz.Value.ToString() + "dB";

            Slider3000Hz.Maximum = 0;
            Slider3000Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value3000Hz.Text = Slider3000Hz.Value.ToString() + "dB";

            Slider3500Hz.Maximum = 0;
            Slider3500Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value3500Hz.Text = Slider3500Hz.Value.ToString() + "dB";

            Slider4250Hz.Maximum = 0;
            Slider4250Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value4250Hz.Text = Slider4250Hz.Value.ToString() + "dB";

            Slider5250Hz.Maximum = 0;
            Slider5250Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value5250Hz.Text = Slider5250Hz.Value.ToString() + "dB";

            Slider6250Hz.Maximum = 0;
            Slider6250Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value6250Hz.Text = Slider6250Hz.Value.ToString() + "dB";

            Slider7250Hz.Maximum = 0;
            Slider7250Hz.Minimum = hiDictionaryViewModel.Audion8BandEqualizationFilter.Keys.Min();
            Value7250Hz.Text = Slider7250Hz.Value.ToString() + "dB";
        }

        public void Audion6Slider()
        {
            SliderDC.Maximum = 0;
            SliderDC.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            ValueDC.Text = SliderDC.Value.ToString() + "dB";

            Slider500Hz.Maximum = 0;
            Slider500Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value500Hz.Text = Slider500Hz.Value.ToString() + "dB";

            Slider1000Hz.Maximum = 0;
            Slider1000Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value1000Hz.Text = Slider1000Hz.Value.ToString() + "dB";

            Slider1500Hz.Maximum = 0;
            Slider1500Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value1500Hz.Text = Slider1500Hz.Value.ToString() + "dB";

            Slider2000Hz.Maximum = 0;
            Slider2000Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value2000Hz.Text = Slider2000Hz.Value.ToString() + "dB";

            Slider2500Hz.Maximum = 0;
            Slider2500Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value2500Hz.Text = Slider2500Hz.Value.ToString() + "dB";

            Slider3000Hz.Maximum = 0;
            Slider3000Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value3000Hz.Text = Slider3000Hz.Value.ToString() + "dB";

            Slider3500Hz.Maximum = 0;
            Slider3500Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value3500Hz.Text = Slider3500Hz.Value.ToString() + "dB";

            Slider4250Hz.Maximum = 0;
            Slider4250Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value4250Hz.Text = Slider4250Hz.Value.ToString() + "dB";

            Slider5250Hz.Maximum = 0;
            Slider5250Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value5250Hz.Text = Slider5250Hz.Value.ToString() + "dB";

            Slider6250Hz.Maximum = 0;
            Slider6250Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value6250Hz.Text = Slider6250Hz.Value.ToString() + "dB";

            Slider7250Hz.Maximum = 0;
            Slider7250Hz.Minimum = hiDictionaryViewModel.Audion6BandEqualizationFilter.Keys.Min();
            Value7250Hz.Text = Slider7250Hz.Value.ToString() + "dB";
        }

        public void Audion4Slider()
        {
            SliderDC.Maximum = 0;
            SliderDC.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            ValueDC.Text = SliderDC.Value.ToString() + "dB";

            Slider500Hz.Maximum = 0;
            Slider500Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value500Hz.Text = Slider500Hz.Value.ToString() + "dB";

            Slider1000Hz.Maximum = 0;
            Slider1000Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value1000Hz.Text = Slider1000Hz.Value.ToString() + "dB";

            Slider1500Hz.Maximum = 0;
            Slider1500Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value1500Hz.Text = Slider1500Hz.Value.ToString() + "dB";

            Slider2000Hz.Maximum = 0;
            Slider2000Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value2000Hz.Text = Slider2000Hz.Value.ToString() + "dB";

            Slider2500Hz.Maximum = 0;
            Slider2500Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value2500Hz.Text = Slider2500Hz.Value.ToString() + "dB";

            Slider3000Hz.Maximum = 0;
            Slider3000Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value3000Hz.Text = Slider3000Hz.Value.ToString() + "dB";

            Slider3500Hz.Maximum = 0;
            Slider3500Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value3500Hz.Text = Slider3500Hz.Value.ToString() + "dB";

            Slider4250Hz.Maximum = 0;
            Slider4250Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value4250Hz.Text = Slider4250Hz.Value.ToString() + "dB";

            Slider5250Hz.Maximum = 0;
            Slider5250Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value5250Hz.Text = Slider5250Hz.Value.ToString() + "dB";

            Slider6250Hz.Maximum = 0;
            Slider6250Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value6250Hz.Text = Slider6250Hz.Value.ToString() + "dB";

            Slider7250Hz.Maximum = 0;
            Slider7250Hz.Minimum = hiDictionaryViewModel.Audion4BandEqualizationFilter.Keys.Min();
            Value7250Hz.Text = Slider7250Hz.Value.ToString() + "dB";
        }

        public void SpinNRSlider()
        {
            SliderDC.Maximum = 0;
            SliderDC.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            ValueDC.Text = SliderDC.Value.ToString() + "dB";

            Slider500Hz.Maximum = 0;
            Slider500Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value500Hz.Text = Slider500Hz.Value.ToString() + "dB";

            Slider1000Hz.Maximum = 0;
            Slider1000Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value1000Hz.Text = Slider1000Hz.Value.ToString() + "dB";

            Slider1500Hz.Maximum = 0;
            Slider1500Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value1500Hz.Text = Slider1500Hz.Value.ToString() + "dB";

            Slider2000Hz.Maximum = 0;
            Slider2000Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value2000Hz.Text = Slider2000Hz.Value.ToString() + "dB";

            Slider2500Hz.Maximum = 0;
            Slider2500Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value2500Hz.Text = Slider2500Hz.Value.ToString() + "dB";

            Slider3000Hz.Maximum = 0;
            Slider3000Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value3000Hz.Text = Slider3000Hz.Value.ToString() + "dB";

            Slider3500Hz.Maximum = 0;
            Slider3500Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value3500Hz.Text = Slider3500Hz.Value.ToString() + "dB";

            Slider4250Hz.Maximum = 0;
            Slider4250Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value4250Hz.Text = Slider4250Hz.Value.ToString() + "dB";

            Slider5250Hz.Maximum = 0;
            Slider5250Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value5250Hz.Text = Slider5250Hz.Value.ToString() + "dB";

            Slider6250Hz.Maximum = 0;
            Slider6250Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value6250Hz.Text = Slider6250Hz.Value.ToString() + "dB";

            Slider7250Hz.Maximum = 0;
            Slider7250Hz.Minimum = hiDictionaryViewModel.SpinNRBandEqualizationFilter.Keys.Min();
            Value7250Hz.Text = Slider7250Hz.Value.ToString() + "dB";
        }

        public void LegacyVisibility()
        {
            GridLegacy.Visibility = Visibility.Visible;
            GridGeneric.Visibility = Visibility.Collapsed;
        }

        public void GenericVisibility()
        {
            GridLegacy.Visibility = Visibility.Collapsed;
            GridGeneric.Visibility = Visibility.Visible;
        }
    }
}