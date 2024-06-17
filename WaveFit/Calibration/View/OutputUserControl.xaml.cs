using System.Windows;
using System.Windows.Controls;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para OutputUserControl.xam
    /// </summary>
    public partial class OutputUserControl : UserControl
    {
        private HIDictionaryViewModel hiDictionaryViewModel = new HIDictionaryViewModel();

        public OutputUserControl(string hearingInstrument)
        {
            InitializeComponent();

            if (hearingInstrument == "SpinNR")
            {
                SpinNRComboBox();
                SpinNRVisibility();
            }
            else if (hearingInstrument == "Audion4")
            {
                Audion4ComboBox();
                Audion4Visibility();
            }
            else if (hearingInstrument == "Audion6")
            {
                Audion6ComboBox();
                Audion6Visibility();
            }
            else if (hearingInstrument == "Audion8")
            {
                Audion8ComboBox();
                Audion8Visibility();
            }
            else if (hearingInstrument == "Audion16")
            {
                Audion16ComboBox();
                Audion16Visibility();
            }
        }

        public void Audion16ComboBox()
        {
            A16C1TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C2TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C3TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C4TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C5TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C6TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C7TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C8TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C9TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C10TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C11TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C12TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C13TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C14TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C15TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;
            A16C16TK.ItemsSource = hiDictionaryViewModel.Audion16CompressionThresholds.Keys;

            A16C1MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C2MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C3MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C4MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C5MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C6MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C7MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C8MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C9MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C10MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C11MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C12MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C13MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C14MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C15MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;
            A16C16MPO.ItemsSource = hiDictionaryViewModel.Audion16OutputCompressionLimiter.Keys;

            A16C1R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C2R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C3R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C4R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C5R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C6R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C7R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C8R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C9R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C10R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C11R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C12R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C13R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C14R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C15R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
            A16C16R.ItemsSource = hiDictionaryViewModel.Audion16CompressionRatio.Keys;
        }

        public void Audion8ComboBox()
        {
            A8C1TK.ItemsSource = hiDictionaryViewModel.Audion8CompressionThresholds.Keys;
            A8C2TK.ItemsSource = hiDictionaryViewModel.Audion8CompressionThresholds.Keys;
            A8C3TK.ItemsSource = hiDictionaryViewModel.Audion8CompressionThresholds.Keys;
            A8C4TK.ItemsSource = hiDictionaryViewModel.Audion8CompressionThresholds.Keys;
            A8C5TK.ItemsSource = hiDictionaryViewModel.Audion8CompressionThresholds.Keys;
            A8C6TK.ItemsSource = hiDictionaryViewModel.Audion8CompressionThresholds.Keys;
            A8C7TK.ItemsSource = hiDictionaryViewModel.Audion8CompressionThresholds.Keys;
            A8C8TK.ItemsSource = hiDictionaryViewModel.Audion8CompressionThresholds.Keys;

            A8C1MPO.ItemsSource = hiDictionaryViewModel.Audion8OutputCompressionLimiter.Keys;
            A8C2MPO.ItemsSource = hiDictionaryViewModel.Audion8OutputCompressionLimiter.Keys;
            A8C3MPO.ItemsSource = hiDictionaryViewModel.Audion8OutputCompressionLimiter.Keys;
            A8C4MPO.ItemsSource = hiDictionaryViewModel.Audion8OutputCompressionLimiter.Keys;
            A8C5MPO.ItemsSource = hiDictionaryViewModel.Audion8OutputCompressionLimiter.Keys;
            A8C6MPO.ItemsSource = hiDictionaryViewModel.Audion8OutputCompressionLimiter.Keys;
            A8C7MPO.ItemsSource = hiDictionaryViewModel.Audion8OutputCompressionLimiter.Keys;
            A8C8MPO.ItemsSource = hiDictionaryViewModel.Audion8OutputCompressionLimiter.Keys;

            A8C1R.ItemsSource = hiDictionaryViewModel.Audion8CompressionRatio.Keys;
            A8C2R.ItemsSource = hiDictionaryViewModel.Audion8CompressionRatio.Keys;
            A8C3R.ItemsSource = hiDictionaryViewModel.Audion8CompressionRatio.Keys;
            A8C4R.ItemsSource = hiDictionaryViewModel.Audion8CompressionRatio.Keys;
            A8C5R.ItemsSource = hiDictionaryViewModel.Audion8CompressionRatio.Keys;
            A8C6R.ItemsSource = hiDictionaryViewModel.Audion8CompressionRatio.Keys;
            A8C7R.ItemsSource = hiDictionaryViewModel.Audion8CompressionRatio.Keys;
            A8C8R.ItemsSource = hiDictionaryViewModel.Audion8CompressionRatio.Keys;
        }

        public void Audion6ComboBox()
        {
            A6C1TK.ItemsSource = hiDictionaryViewModel.Audion6CompressionThresholds.Keys;
            A6C2TK.ItemsSource = hiDictionaryViewModel.Audion6CompressionThresholds.Keys;
            A6C3TK.ItemsSource = hiDictionaryViewModel.Audion6CompressionThresholds.Keys;
            A6C4TK.ItemsSource = hiDictionaryViewModel.Audion6CompressionThresholds.Keys;
            A6C5TK.ItemsSource = hiDictionaryViewModel.Audion6CompressionThresholds.Keys;
            A6C6TK.ItemsSource = hiDictionaryViewModel.Audion6CompressionThresholds.Keys;

            A6C1MPO.ItemsSource = hiDictionaryViewModel.Audion6OutputCompressionLimiter.Keys;
            A6C2MPO.ItemsSource = hiDictionaryViewModel.Audion6OutputCompressionLimiter.Keys;
            A6C3MPO.ItemsSource = hiDictionaryViewModel.Audion6OutputCompressionLimiter.Keys;
            A6C4MPO.ItemsSource = hiDictionaryViewModel.Audion6OutputCompressionLimiter.Keys;
            A6C5MPO.ItemsSource = hiDictionaryViewModel.Audion6OutputCompressionLimiter.Keys;
            A6C6MPO.ItemsSource = hiDictionaryViewModel.Audion6OutputCompressionLimiter.Keys;

            A6C1R.ItemsSource = hiDictionaryViewModel.Audion6CompressionRatio.Keys;
            A6C2R.ItemsSource = hiDictionaryViewModel.Audion6CompressionRatio.Keys;
            A6C3R.ItemsSource = hiDictionaryViewModel.Audion6CompressionRatio.Keys;
            A6C4R.ItemsSource = hiDictionaryViewModel.Audion6CompressionRatio.Keys;
            A6C5R.ItemsSource = hiDictionaryViewModel.Audion6CompressionRatio.Keys;
            A6C6R.ItemsSource = hiDictionaryViewModel.Audion6CompressionRatio.Keys;
        }

        public void Audion4ComboBox()
        {
            A4CTK.ItemsSource = hiDictionaryViewModel.Audion4CompressionThresholds.Keys;

            A4CMPO.ItemsSource = hiDictionaryViewModel.Audion4OutputCompressionLimiter.Keys;

            A4C1R.ItemsSource = hiDictionaryViewModel.Audion4CompressionRatio.Keys;
            A4C2R.ItemsSource = hiDictionaryViewModel.Audion4CompressionRatio.Keys;
            A4C3R.ItemsSource = hiDictionaryViewModel.Audion4CompressionRatio.Keys;
            A4C4R.ItemsSource = hiDictionaryViewModel.Audion4CompressionRatio.Keys;
        }

        public void SpinNRComboBox()
        {
            SpinNRCTK.ItemsSource = hiDictionaryViewModel.SpinNRCompressionThresholds.Keys;

            SpinNRCMPO.ItemsSource = hiDictionaryViewModel.SpinNROutputCompressionLimiter.Keys;

            SpinNRC1R.ItemsSource = hiDictionaryViewModel.SpinNRCompressionRatio.Keys;
            SpinNRC2R.ItemsSource = hiDictionaryViewModel.SpinNRCompressionRatio.Keys;
        }

        public void Audion16Visibility()
        {
            OutputTab.Visibility = Visibility.Visible;
            Audion16Grid.Visibility = Visibility.Visible;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion8Visibility()
        {
            OutputTab.Visibility = Visibility.Visible;
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Visible;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion6Visibility()
        {
            OutputTab.Visibility = Visibility.Visible;
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Visible;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void Audion4Visibility()
        {
            OutputTab.Visibility = Visibility.Visible;
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Visible;
            SpinNRGrid.Visibility = Visibility.Collapsed;
        }

        public void SpinNRVisibility()
        {
            OutputTab.Visibility = Visibility.Visible;
            Audion16Grid.Visibility = Visibility.Collapsed;
            Audion8Grid.Visibility = Visibility.Collapsed;
            Audion6Grid.Visibility = Visibility.Collapsed;
            Audion4Grid.Visibility = Visibility.Collapsed;
            SpinNRGrid.Visibility = Visibility.Visible;
        }

        public void SpinNROutputGetValues(int C_TK,
                                    int C_MPO,
                                    int C1_Ratio, int C2_Ratio)
        {
            SpinNRCTK.SelectedIndex = C_TK;

            SpinNRCMPO.SelectedIndex = C_MPO;

            SpinNRC1R.SelectedIndex = C1_Ratio;
            SpinNRC2R.SelectedIndex = C2_Ratio;
        }

        public void SpinNROutputSetValues(ref int C_TK,
                            ref int C_MPO,
                            ref int C1_Ratio, ref int C2_Ratio)
        {
            C_TK = SpinNRCTK.SelectedIndex;

            C_MPO = SpinNRCMPO.SelectedIndex;

            C1_Ratio = SpinNRC1R.SelectedIndex;
            C2_Ratio = SpinNRC2R.SelectedIndex;
        }

        public void Audion4OutputGetValues(int C1_TK,
                                     int C1_MPO,
                                     int C1_Ratio, int C2_Ratio, int C3_Ratio, int C4_Ratio)
        {
            A4CTK.SelectedIndex = C1_TK;

            A4CMPO.SelectedIndex = C1_MPO;

            A4C1R.SelectedIndex = C1_Ratio;
            A4C2R.SelectedIndex = C2_Ratio;
            A4C3R.SelectedIndex = C3_Ratio;
            A4C4R.SelectedIndex = C4_Ratio;
        }

        public void Audion4OutputSetValues(ref int C1_TK,
                             ref int C1_MPO,
                             ref int C1_Ratio, ref int C2_Ratio, ref int C3_Ratio, ref int C4_Ratio)
        {
            C1_TK = A4CTK.SelectedIndex;

            C1_MPO = A4CMPO.SelectedIndex;

            C1_Ratio = A4C1R.SelectedIndex;
            C2_Ratio = A4C2R.SelectedIndex;
            C3_Ratio = A4C3R.SelectedIndex;
            C4_Ratio = A4C4R.SelectedIndex;
        }

        public void Audion6OutputGetValues(int C1_TK, int C2_TK, int C3_TK, int C4_TK, int C5_TK, int C6_TK,
                             int C1_MPO, int C2_MPO, int C3_MPO, int C4_MPO, int C5_MPO, int C6_MPO,
                             int C1_Ratio, int C2_Ratio, int C3_Ratio, int C4_Ratio, int C5_Ratio, int C6_Ratio)
        {
            A6C1TK.SelectedIndex = C1_TK;
            A6C2TK.SelectedIndex = C2_TK;
            A6C3TK.SelectedIndex = C3_TK;
            A6C4TK.SelectedIndex = C4_TK;
            A6C5TK.SelectedIndex = C5_TK;
            A6C6TK.SelectedIndex = C6_TK;

            A6C1MPO.SelectedIndex = C1_MPO;
            A6C2MPO.SelectedIndex = C2_MPO;
            A6C3MPO.SelectedIndex = C3_MPO;
            A6C4MPO.SelectedIndex = C4_MPO;
            A6C5MPO.SelectedIndex = C5_MPO;
            A6C6MPO.SelectedIndex = C6_MPO;

            A6C1R.SelectedIndex = C1_Ratio;
            A6C2R.SelectedIndex = C2_Ratio;
            A6C3R.SelectedIndex = C3_Ratio;
            A6C4R.SelectedIndex = C4_Ratio;
            A6C5R.SelectedIndex = C5_Ratio;
            A6C6R.SelectedIndex = C6_Ratio;
        }

        public void Audion6OutputSetValues(ref int C1_TK, ref int C2_TK, ref int C3_TK, ref int C4_TK, ref int C5_TK, ref int C6_TK,
                             ref int C1_MPO, ref int C2_MPO, ref int C3_MPO, ref int C4_MPO, ref int C5_MPO, ref int C6_MPO,
                             ref int C1_Ratio, ref int C2_Ratio, ref int C3_Ratio, ref int C4_Ratio, ref int C5_Ratio, ref int C6_Ratio)
        {
            C1_TK = A6C1TK.SelectedIndex;
            C2_TK = A6C2TK.SelectedIndex;
            C3_TK = A6C3TK.SelectedIndex;
            C4_TK = A6C4TK.SelectedIndex;
            C5_TK = A6C5TK.SelectedIndex;
            C6_TK = A6C6TK.SelectedIndex;

            C1_MPO = A6C1MPO.SelectedIndex;
            C2_MPO = A6C2MPO.SelectedIndex;
            C3_MPO = A6C3MPO.SelectedIndex;
            C4_MPO = A6C4MPO.SelectedIndex;
            C5_MPO = A6C5MPO.SelectedIndex;
            C6_MPO = A6C6MPO.SelectedIndex;

            C1_Ratio = A6C1R.SelectedIndex;
            C2_Ratio = A6C2R.SelectedIndex;
            C3_Ratio = A6C3R.SelectedIndex;
            C4_Ratio = A6C4R.SelectedIndex;
            C5_Ratio = A6C5R.SelectedIndex;
            C6_Ratio = A6C6R.SelectedIndex;
        }

        public void Audion8OutputGetValues(int C1_TK, int C2_TK, int C3_TK, int C4_TK, int C5_TK, int C6_TK, int C7_TK, int C8_TK,
                                     int C1_MPO, int C2_MPO, int C3_MPO, int C4_MPO, int C5_MPO, int C6_MPO, int C7_MPO, int C8_MPO,
                                     int C1_Ratio, int C2_Ratio, int C3_Ratio, int C4_Ratio, int C5_Ratio, int C6_Ratio, int C7_Ratio, int C8_Ratio)
        {
            A8C1TK.SelectedIndex = C1_TK;
            A8C2TK.SelectedIndex = C2_TK;
            A8C3TK.SelectedIndex = C3_TK;
            A8C4TK.SelectedIndex = C4_TK;
            A8C5TK.SelectedIndex = C5_TK;
            A8C6TK.SelectedIndex = C6_TK;
            A8C7TK.SelectedIndex = C7_TK;
            A8C8TK.SelectedIndex = C8_TK;

            A8C1MPO.SelectedIndex = C1_MPO;
            A8C2MPO.SelectedIndex = C2_MPO;
            A8C3MPO.SelectedIndex = C3_MPO;
            A8C4MPO.SelectedIndex = C4_MPO;
            A8C5MPO.SelectedIndex = C5_MPO;
            A8C6MPO.SelectedIndex = C6_MPO;
            A8C7MPO.SelectedIndex = C7_MPO;
            A8C8MPO.SelectedIndex = C8_MPO;

            A8C1R.SelectedIndex = C1_Ratio;
            A8C2R.SelectedIndex = C2_Ratio;
            A8C3R.SelectedIndex = C3_Ratio;
            A8C4R.SelectedIndex = C4_Ratio;
            A8C5R.SelectedIndex = C5_Ratio;
            A8C6R.SelectedIndex = C6_Ratio;
            A8C7R.SelectedIndex = C7_Ratio;
            A8C8R.SelectedIndex = C8_Ratio;
        }

        public void Audion8OutputSetValues(ref int C1_TK, ref int C2_TK, ref int C3_TK, ref int C4_TK, ref int C5_TK, ref int C6_TK, ref int C7_TK, ref int C8_TK,
                             ref int C1_MPO, ref int C2_MPO, ref int C3_MPO, ref int C4_MPO, ref int C5_MPO, ref int C6_MPO, ref int C7_MPO, ref int C8_MPO,
                             ref int C1_Ratio, ref int C2_Ratio, ref int C3_Ratio, ref int C4_Ratio, ref int C5_Ratio, ref int C6_Ratio, ref int C7_Ratio, ref int C8_Ratio)
        {
            C1_TK = A8C1TK.SelectedIndex;
            C2_TK = A8C2TK.SelectedIndex;
            C3_TK = A8C3TK.SelectedIndex;
            C4_TK = A8C4TK.SelectedIndex;
            C5_TK = A8C5TK.SelectedIndex;
            C6_TK = A8C6TK.SelectedIndex;
            C7_TK = A8C7TK.SelectedIndex;
            C8_TK = A8C8TK.SelectedIndex;

            C1_MPO = A8C1MPO.SelectedIndex;
            C2_MPO = A8C2MPO.SelectedIndex;
            C3_MPO = A8C3MPO.SelectedIndex;
            C4_MPO = A8C4MPO.SelectedIndex;
            C5_MPO = A8C5MPO.SelectedIndex;
            C6_MPO = A8C6MPO.SelectedIndex;
            C7_MPO = A8C7MPO.SelectedIndex;
            C8_MPO = A8C8MPO.SelectedIndex;

            C1_Ratio = A8C1R.SelectedIndex;
            C2_Ratio = A8C2R.SelectedIndex;
            C3_Ratio = A8C3R.SelectedIndex;
            C4_Ratio = A8C4R.SelectedIndex;
            C5_Ratio = A8C5R.SelectedIndex;
            C6_Ratio = A8C6R.SelectedIndex;
            C7_Ratio = A8C7R.SelectedIndex;
            C8_Ratio = A8C8R.SelectedIndex;
        }

        public void Audion16OutputGetValues(int C1_TK, int C2_TK, int C3_TK, int C4_TK, int C5_TK, int C6_TK, int C7_TK, int C8_TK,
                             int C9_TK, int C10_TK, int C11_TK, int C12_TK, int C13_TK, int C14_TK, int C15_TK, int C16_TK,
                             int C1_MPO, int C2_MPO, int C3_MPO, int C4_MPO, int C5_MPO, int C6_MPO, int C7_MPO, int C8_MPO,
                             int C9_MPO, int C10_MPO, int C11_MPO, int C12_MPO, int C13_MPO, int C14_MPO, int C15_MPO, int C16_MPO,
                             int C1_Ratio, int C2_Ratio, int C3_Ratio, int C4_Ratio, int C5_Ratio, int C6_Ratio, int C7_Ratio, int C8_Ratio,
                             int C9_Ratio, int C10_Ratio, int C11_Ratio, int C12_Ratio, int C13_Ratio, int C14_Ratio, int C15_Ratio, int C16_Ratio)
        {
            A16C1TK.SelectedIndex = C1_TK;
            A16C2TK.SelectedIndex = C2_TK;
            A16C3TK.SelectedIndex = C3_TK;
            A16C4TK.SelectedIndex = C4_TK;
            A16C5TK.SelectedIndex = C5_TK;
            A16C6TK.SelectedIndex = C6_TK;
            A16C7TK.SelectedIndex = C7_TK;
            A16C8TK.SelectedIndex = C8_TK;
            A16C9TK.SelectedIndex = C9_TK;
            A16C10TK.SelectedIndex = C10_TK;
            A16C11TK.SelectedIndex = C11_TK;
            A16C12TK.SelectedIndex = C12_TK;
            A16C13TK.SelectedIndex = C13_TK;
            A16C14TK.SelectedIndex = C14_TK;
            A16C15TK.SelectedIndex = C15_TK;
            A16C16TK.SelectedIndex = C16_TK;

            A16C1MPO.SelectedIndex = C1_MPO;
            A16C2MPO.SelectedIndex = C2_MPO;
            A16C3MPO.SelectedIndex = C3_MPO;
            A16C4MPO.SelectedIndex = C4_MPO;
            A16C5MPO.SelectedIndex = C5_MPO;
            A16C6MPO.SelectedIndex = C6_MPO;
            A16C7MPO.SelectedIndex = C7_MPO;
            A16C8MPO.SelectedIndex = C8_MPO;
            A16C9MPO.SelectedIndex = C9_MPO;
            A16C10MPO.SelectedIndex = C10_MPO;
            A16C11MPO.SelectedIndex = C11_MPO;
            A16C12MPO.SelectedIndex = C12_MPO;
            A16C13MPO.SelectedIndex = C13_MPO;
            A16C14MPO.SelectedIndex = C14_MPO;
            A16C15MPO.SelectedIndex = C15_MPO;
            A16C16MPO.SelectedIndex = C16_MPO;

            A16C1R.SelectedIndex = C1_Ratio;
            A16C2R.SelectedIndex = C2_Ratio;
            A16C3R.SelectedIndex = C3_Ratio;
            A16C4R.SelectedIndex = C4_Ratio;
            A16C5R.SelectedIndex = C5_Ratio;
            A16C6R.SelectedIndex = C6_Ratio;
            A16C7R.SelectedIndex = C7_Ratio;
            A16C8R.SelectedIndex = C8_Ratio;
            A16C9R.SelectedIndex = C9_Ratio;
            A16C10R.SelectedIndex = C10_Ratio;
            A16C11R.SelectedIndex = C11_Ratio;
            A16C12R.SelectedIndex = C12_Ratio;
            A16C13R.SelectedIndex = C13_Ratio;
            A16C14R.SelectedIndex = C14_Ratio;
            A16C15R.SelectedIndex = C15_Ratio;
            A16C16R.SelectedIndex = C16_Ratio;
        }

        public void Audion16OutputSetValues(ref int C1_TK, ref int C2_TK, ref int C3_TK, ref int C4_TK, ref int C5_TK, ref int C6_TK, ref int C7_TK, ref int C8_TK,
                             ref int C9_TK, ref int C10_TK, ref int C11_TK, ref int C12_TK, ref int C13_TK, ref int C14_TK, ref int C15_TK, ref int C16_TK,
                             ref int C1_MPO, ref int C2_MPO, ref int C3_MPO, ref int C4_MPO, ref int C5_MPO, ref int C6_MPO, ref int C7_MPO, ref int C8_MPO,
                             ref int C9_MPO, ref int C10_MPO, ref int C11_MPO, ref int C12_MPO, ref int C13_MPO, ref int C14_MPO, ref int C15_MPO, ref int C16_MPO,
                             ref int C1_Ratio, ref int C2_Ratio, ref int C3_Ratio, ref int C4_Ratio, ref int C5_Ratio, ref int C6_Ratio, ref int C7_Ratio, ref int C8_Ratio,
                             ref int C9_Ratio, ref int C10_Ratio, ref int C11_Ratio, ref int C12_Ratio, ref int C13_Ratio, ref int C14_Ratio, ref int C15_Ratio, ref int C16_Ratio)
        {
            C1_TK = A16C1TK.SelectedIndex;
            C2_TK = A16C2TK.SelectedIndex;
            C3_TK = A16C3TK.SelectedIndex;
            C4_TK = A16C4TK.SelectedIndex;
            C5_TK = A16C5TK.SelectedIndex;
            C6_TK = A16C6TK.SelectedIndex;
            C7_TK = A16C7TK.SelectedIndex;
            C8_TK = A16C8TK.SelectedIndex;
            C9_TK = A16C9TK.SelectedIndex;
            C10_TK = A16C10TK.SelectedIndex;
            C11_TK = A16C11TK.SelectedIndex;
            C12_TK = A16C12TK.SelectedIndex;
            C13_TK = A16C13TK.SelectedIndex;
            C14_TK = A16C14TK.SelectedIndex;
            C15_TK = A16C15TK.SelectedIndex;
            C16_TK = A16C16TK.SelectedIndex;

            C1_MPO = A16C1MPO.SelectedIndex;
            C2_MPO = A16C2MPO.SelectedIndex;
            C3_MPO = A16C3MPO.SelectedIndex;
            C4_MPO = A16C4MPO.SelectedIndex;
            C5_MPO = A16C5MPO.SelectedIndex;
            C6_MPO = A16C6MPO.SelectedIndex;
            C7_MPO = A16C7MPO.SelectedIndex;
            C8_MPO = A16C8MPO.SelectedIndex;
            C9_MPO = A16C9MPO.SelectedIndex;
            C10_MPO = A16C10MPO.SelectedIndex;
            C11_MPO = A16C11MPO.SelectedIndex;
            C12_MPO = A16C12MPO.SelectedIndex;
            C13_MPO = A16C13MPO.SelectedIndex;
            C14_MPO = A16C14MPO.SelectedIndex;
            C15_MPO = A16C15MPO.SelectedIndex;
            C16_MPO = A16C16MPO.SelectedIndex;

            C1_Ratio = A16C1R.SelectedIndex;
            C2_Ratio = A16C2R.SelectedIndex;
            C3_Ratio = A16C3R.SelectedIndex;
            C4_Ratio = A16C4R.SelectedIndex;
            C5_Ratio = A16C5R.SelectedIndex;
            C6_Ratio = A16C6R.SelectedIndex;
            C7_Ratio = A16C7R.SelectedIndex;
            C8_Ratio = A16C8R.SelectedIndex;
            C9_Ratio = A16C9R.SelectedIndex;
            C10_Ratio = A16C10R.SelectedIndex;
            C11_Ratio = A16C11R.SelectedIndex;
            C12_Ratio = A16C12R.SelectedIndex;
            C13_Ratio = A16C13R.SelectedIndex;
            C14_Ratio = A16C14R.SelectedIndex;
            C15_Ratio = A16C15R.SelectedIndex;
            C16_Ratio = A16C16R.SelectedIndex;
        }
    }
}