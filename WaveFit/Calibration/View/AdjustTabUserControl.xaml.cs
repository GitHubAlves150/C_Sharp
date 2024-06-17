using System.Windows;
using System.Windows.Controls;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para AdjustTabUserControl.xam
    /// </summary>
    public partial class AdjustTabUserControl : UserControl
    {
        public AlgorithmUserControl algorithmUserControl;
        public OutputUserControl outputUserControl;
        public EqualyzerUserControl equalyzerUserControl;
        public FilterUserControl filterUserControl;
        public MicrophoneUserControl microphoneUserControl;
        public PowerOnUserControl powerOnUserControl;
        public ToneUserControl toneUserControl;
        public VolumeUserControl volumeUserControl;

        public AdjustTabUserControl(char side, string hearingInstrument)
        {
            InitializeComponent();

            if (hearingInstrument != "Null")
            {
                algorithmUserControl = new AlgorithmUserControl(hearingInstrument);
                outputUserControl = new OutputUserControl(hearingInstrument);
                equalyzerUserControl = new EqualyzerUserControl(hearingInstrument);
                filterUserControl = new FilterUserControl(hearingInstrument);
                powerOnUserControl = new PowerOnUserControl(hearingInstrument);
                toneUserControl = new ToneUserControl(hearingInstrument);
                volumeUserControl = new VolumeUserControl(hearingInstrument);
                microphoneUserControl = new MicrophoneUserControl(hearingInstrument);

                CreateContent();

                if (hearingInstrument == "SpinNR")
                {
                    SpinNRTabs();
                }
                else if (hearingInstrument == "Audion4")
                {
                    Audion4Tabs();
                }
                else if (hearingInstrument == "Audion6")
                {
                    Audion6Tabs();
                }
                else if (hearingInstrument == "Audion8")
                {
                    Audion8Tabs();
                }
                else if (hearingInstrument == "Audion16")
                {
                    Audion16Tabs();
                }
            }
        }

        public void Audion16Tabs()
        {
            TabFilter.Visibility = Visibility.Collapsed;
            TabPowerOn.Visibility = Visibility.Visible;
            TabVolume.Visibility = Visibility.Collapsed;
        }

        public void Audion8Tabs()
        {
            TabFilter.Visibility = Visibility.Collapsed;
            TabPowerOn.Visibility = Visibility.Visible;
            TabVolume.Visibility = Visibility.Collapsed;
        }

        public void Audion6Tabs()
        {
            TabFilter.Visibility = Visibility.Collapsed;
            TabPowerOn.Visibility = Visibility.Visible;
            TabVolume.Visibility = Visibility.Collapsed;
        }

        public void Audion4Tabs()
        {
            TabFilter.Visibility = Visibility.Visible;
            TabPowerOn.Visibility = Visibility.Visible;
            TabVolume.Visibility = Visibility.Collapsed;
        }

        public void SpinNRTabs()
        {
            TabFilter.Visibility = Visibility.Visible;
            TabPowerOn.Visibility = Visibility.Collapsed;
            TabVolume.Visibility = Visibility.Collapsed;
        }

        public void CreateContent()
        {
            ScrollViewer algorithmScrollViewer = new ScrollViewer();
            algorithmScrollViewer.Content = algorithmUserControl;
            algorithmScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            algorithmScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            algorithmScrollViewer.CanContentScroll = true;
            TabAlgorithm.Content = algorithmScrollViewer;

            ScrollViewer outputScrollViewer = new ScrollViewer();
            outputScrollViewer.Content = outputUserControl;
            outputScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            outputScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            outputScrollViewer.CanContentScroll = true;
            TabOutput.Content = outputScrollViewer;

            ScrollViewer equalizerScrollViewer = new ScrollViewer();
            equalizerScrollViewer.Content = equalyzerUserControl;
            equalizerScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            equalizerScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            equalizerScrollViewer.CanContentScroll = true;
            TabEqualizer.Content = equalizerScrollViewer;

            ScrollViewer filterScrollViewer = new ScrollViewer();
            filterScrollViewer.Content = filterUserControl;
            filterScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            filterScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            filterScrollViewer.CanContentScroll = true;
            TabFilter.Content = filterScrollViewer;

            ScrollViewer microphoneScrollViewer = new ScrollViewer();
            microphoneScrollViewer.Content = microphoneUserControl;
            microphoneScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            microphoneScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            microphoneScrollViewer.CanContentScroll = true;
            TabMicrophone.Content = microphoneScrollViewer;

            ScrollViewer powerONScrollViewer = new ScrollViewer();
            powerONScrollViewer.Content = powerOnUserControl;
            powerONScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            powerONScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            powerONScrollViewer.CanContentScroll = true;
            TabPowerOn.Content = powerONScrollViewer;

            ScrollViewer toneScrollViewer = new ScrollViewer();
            toneScrollViewer.Content = toneUserControl;
            toneScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            toneScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            toneScrollViewer.CanContentScroll = true;
            TabTone.Content = toneScrollViewer;

            ScrollViewer volumeScrollViewer = new ScrollViewer();
            volumeScrollViewer.Content = volumeUserControl;
            volumeScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            volumeScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            volumeScrollViewer.CanContentScroll = true;
            TabVolume.Content = volumeScrollViewer;
        }
    }
}