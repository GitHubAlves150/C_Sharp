using System.Windows;
using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para PowerOnUserControl.xam
    /// </summary>
    public partial class PowerOnUserControl : UserControl
    {
        private HIDictionaryViewModel hIDictionaryViewModel = new HIDictionaryViewModel();

        public PowerOnUserControl(string hearingInstrument)
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
            }
        }

        public void Audion16ToolValues()
        {
            A16POD.ItemsSource = hIDictionaryViewModel.Audion16PowerOnDelay.Keys;
            A16POL.ItemsSource = hIDictionaryViewModel.Audion16PowerOnLevel.Keys;
        }

        public void Audion16ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Visible;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
        }

        public void Audion16PowerOnGetValues(int Power_On_Delay, int Power_On_Level)
        {
            A16POD.SelectedIndex = Power_On_Delay;
            A16POL.SelectedIndex = Power_On_Level;
        }

        public void Audion16PowerOnSetValues(ref int Power_On_Delay, ref int Power_On_Level)
        {
            Power_On_Delay = A16POD.SelectedIndex;
            Power_On_Level = A16POL.SelectedIndex;
        }

        public void Audion8ToolValues()
        {
            A8POD.ItemsSource = hIDictionaryViewModel.Audion8PowerOnDelay.Keys;
            A8POL.ItemsSource = hIDictionaryViewModel.Audion8PowerOnLevel.Keys;
        }

        public void Audion8ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Visible;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
        }

        public void Audion8PowerOnGetValues(int Power_On_Delay, int Power_On_Level)
        {
            A8POD.SelectedIndex = Power_On_Delay;
            A8POL.SelectedIndex = Power_On_Level;
        }

        public void Audion8PowerOnSetValues(ref int Power_On_Delay, ref int Power_On_Level)
        {
            Power_On_Delay = A8POD.SelectedIndex;
            Power_On_Level = A8POL.SelectedIndex;
        }

        public void Audion6ToolValues()
        {
            A6POD.ItemsSource = hIDictionaryViewModel.Audion6PowerOnDelay.Keys;
            A6POL.ItemsSource = hIDictionaryViewModel.Audion6PowerOnLevel.Keys;
        }

        public void Audion6ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Visible;
            Audion4Grid.Visibility = Visibility.Collapsed;
        }

        public void Audion6PowerOnGetValues(int Power_On_Delay, int Power_On_Level)
        {
            A6POD.SelectedIndex = Power_On_Delay;
            A6POL.SelectedIndex = Power_On_Level;
        }

        public void Audion6PowerOnSetValues(ref int Power_On_Delay, ref int Power_On_Level)
        {
            Power_On_Delay = A6POD.SelectedIndex;
            Power_On_Level = A6POL.SelectedIndex;
        }

        public void Audion4ToolValues()
        {
            A4POD.ItemsSource = hIDictionaryViewModel.Audion4PowerOnDelay.Keys;
            A4POL.ItemsSource = hIDictionaryViewModel.Audion4PowerOnLevel.Keys;
        }

        public void Audion4ToolVisibility()
        {
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Visible;
        }

        public void Audion4PowerOnGetValues(int Power_On_Delay, int Power_On_Level)
        {
            A4POD.SelectedIndex = Power_On_Delay;
            A4POL.SelectedIndex = Power_On_Level;
        }

        public void Audion4PowerOnSetValues(ref int Power_On_Delay, ref int Power_On_Level)
        {
            Power_On_Delay = A4POD.SelectedIndex;
            Power_On_Level = A4POL.SelectedIndex;
        }
    }
}