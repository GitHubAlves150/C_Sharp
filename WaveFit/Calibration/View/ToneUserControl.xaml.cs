using System.Windows;
using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para ToneUserControl.xam
    /// </summary>
    public partial class ToneUserControl : UserControl
    {
        private HIDictionaryViewModel hIDictionaryViewModel = new HIDictionaryViewModel();

        public ToneUserControl(string hearingInstrument)
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

        public void Audion16ToolValues()
        {
            A16TF.ItemsSource = hIDictionaryViewModel.Audion16ToneFrequency.Keys;
            A16TL.ItemsSource = hIDictionaryViewModel.Audion16PromptLevel.Keys;
            A16LBWT.ItemsSource = hIDictionaryViewModel.Audion16WarningPromptMode.Keys;
            A16PST.ItemsSource = hIDictionaryViewModel.Audion16ProgramPromptMode.Keys;
        }

        public void Audion16ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Visible;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion16ToneGetValues(int Tone_Frequency, int Tone_Level, int Warning_Prompt_Mode, int Program_Prompt_Mode)
        {
            A16TF.SelectedIndex = Tone_Frequency;
            A16TL.SelectedIndex = Tone_Level;
            A16LBWT.SelectedIndex = Warning_Prompt_Mode;
            A16PST.SelectedIndex = Program_Prompt_Mode;
        }

        public void Audion16ToneSetValues(ref int Tone_Frequency, ref int Tone_Level, ref int Warning_Prompt_Mode, ref int Program_Prompt_Mode)
        {
            Tone_Frequency = A16TF.SelectedIndex;
            Tone_Level = A16TL.SelectedIndex;
            Warning_Prompt_Mode = A16LBWT.SelectedIndex;
            Program_Prompt_Mode = A16PST.SelectedIndex;
        }

        public void Audion8ToolValues()
        {
            A8TF.ItemsSource = hIDictionaryViewModel.Audion8ToneFrequency.Keys;
            A8TL.ItemsSource = hIDictionaryViewModel.Audion8PromptLevelIndexTL.Keys;
            A8LBWT.ItemsSource = hIDictionaryViewModel.Audion8LowBatteryPromptMode.Keys;
            A8PST.ItemsSource = hIDictionaryViewModel.Audion8ProgramPromptMode.Keys;
        }

        public void Audion8ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Visible;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion8ToneGetValues(int Tone_Frequency, int Tone_Level, int Warning_Prompt_Mode, int Program_Prompt_Mode)
        {
            A8TF.SelectedIndex = Tone_Frequency;
            A8TL.SelectedIndex = Tone_Level;
            A8LBWT.SelectedIndex = Warning_Prompt_Mode;
            A8PST.SelectedIndex = Program_Prompt_Mode;
        }

        public void Audion8ToneSetValues(ref int Tone_Frequency, ref int Tone_Level, ref int Warning_Prompt_Mode, ref int Program_Prompt_Mode)
        {
            Tone_Frequency = A8TF.SelectedIndex;
            Tone_Level = A8TL.SelectedIndex;
            Warning_Prompt_Mode = A8LBWT.SelectedIndex;
            Program_Prompt_Mode = A8PST.SelectedIndex;
        }

        public void Audion6ToolValues()
        {
            A6TF.ItemsSource = hIDictionaryViewModel.Audion6ToneFrequency.Keys;
            A6TL.ItemsSource = hIDictionaryViewModel.Audion6ToneLevel.Keys;
            A6LBWT.ItemsSource = hIDictionaryViewModel.Audion6LowBatteryTones.Keys;
            A6PST.ItemsSource = hIDictionaryViewModel.Audion6ProgramSwitchTones.Keys;
        }

        public void Audion6ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Visible;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion6ToneGetValues(int Tone_Frequency, int Tone_Level, int Warning_Prompt_Mode, int Program_Prompt_Mode)
        {
            A6TF.SelectedIndex = Tone_Frequency;
            A6TL.SelectedIndex = Tone_Level;
            A6LBWT.SelectedIndex = Warning_Prompt_Mode;
            A6PST.SelectedIndex = Program_Prompt_Mode;
        }

        public void Audion6ToneSetValues(ref int Tone_Frequency, ref int Tone_Level, ref int Warning_Prompt_Mode, ref int Program_Prompt_Mode)
        {
            Tone_Frequency = A6TF.SelectedIndex;
            Tone_Level = A6TL.SelectedIndex;
            Warning_Prompt_Mode = A6LBWT.SelectedIndex;
            Program_Prompt_Mode = A6PST.SelectedIndex;
        }

        public void Audion4ToolValues()
        {
            A4TF.ItemsSource = hIDictionaryViewModel.Audion4ToneFrequency.Keys;
            A4TL.ItemsSource = hIDictionaryViewModel.Audion4ToneLevel.Keys;
            A4LBWT.ItemsSource = hIDictionaryViewModel.Audion4LowBatteryTones.Keys;
            A4PST.ItemsSource = hIDictionaryViewModel.Audion4ProgramSwitchTones.Keys;
        }

        public void Audion4ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Visible;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion4ToneGetValues(int Tone_Frequency, int Tone_Level, int Warning_Prompt_Mode, int Program_Prompt_Mode)
        {
            A4TF.SelectedIndex = Tone_Frequency;
            A4TL.SelectedIndex = Tone_Level;
            A4LBWT.SelectedIndex = Warning_Prompt_Mode;
            A4PST.SelectedIndex = Program_Prompt_Mode;
        }

        public void Audion4ToneSetValues(ref int Tone_Frequency, ref int Tone_Level, ref int Warning_Prompt_Mode, ref int Program_Prompt_Mode)
        {
            Tone_Frequency = A4TF.SelectedIndex;
            Tone_Level = A4TL.SelectedIndex;
            Warning_Prompt_Mode = A4LBWT.SelectedIndex;
            Program_Prompt_Mode = A4PST.SelectedIndex;
        }

        public void SpinNRToolValues()
        {
            SpinNRTF.ItemsSource = hIDictionaryViewModel.SpinNRToneFrequency.Keys;
            SpinNRTL.ItemsSource = hIDictionaryViewModel.SpinNRToneLevel.Keys;
            SpinNRLBWT.ItemsSource = hIDictionaryViewModel.SpinNRLowBatteryTones.Keys;
            SpinNRPST.ItemsSource = hIDictionaryViewModel.SpinNRProgramSwitchTones.Keys;
        }

        public void SpinNRToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Visible;
        }

        public void SpinNRToneGetValues(int Tone_Frequency, int Tone_Level, int Warning_Prompt_Mode, int Program_Prompt_Mode)
        {
            SpinNRTF.SelectedIndex = Tone_Frequency;
            SpinNRTL.SelectedIndex = Tone_Level;
            SpinNRLBWT.SelectedIndex = Warning_Prompt_Mode;
            SpinNRPST.SelectedIndex = Program_Prompt_Mode;
        }

        public void SpinNRToneSetValues(ref int Tone_Frequency, ref int Tone_Level, ref int Warning_Prompt_Mode, ref int Program_Prompt_Mode)
        {
            Tone_Frequency = SpinNRTF.SelectedIndex;
            Tone_Level = SpinNRTL.SelectedIndex;
            Warning_Prompt_Mode = SpinNRLBWT.SelectedIndex;
            Program_Prompt_Mode = SpinNRPST.SelectedIndex;
        }
    }
}