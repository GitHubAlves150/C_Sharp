using HandyControl.Controls;
using OxyPlot;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WaveFit2.Audiogram.View;
using WaveFit2.Calibration.Class;
using WaveFit2.Calibration.ViewModel;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;
using TabItem = System.Windows.Controls.TabItem;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interação lógica para CalibrationUserControl.xam
    /// </summary>
    public partial class CalibrationUserControl : UserControl
    {
        public FittingAssistance fittingAssistanceR = new FittingAssistance();
        public FittingAssistance fittingAssistanceL = new FittingAssistance();

        public AdjustTabUserControl adjustTabUserControlL;
        public AdjustTabUserControl adjustTabUserControlR;

        public Audion16ViewModel audion16ViewModelR;
        public Audion8ViewModel audion8ViewModelR;
        public Audion6ViewModel audion6ViewModelR;
        public Audion4ViewModel audion4ViewModelR;
        public SpinNRViewModel spinNRViewModelR;

        public Audion16ViewModel audion16ViewModelL;
        public Audion8ViewModel audion8ViewModelL;
        public Audion6ViewModel audion6ViewModelL;
        public Audion4ViewModel audion4ViewModelL;
        public SpinNRViewModel spinNRViewModelL;

        public HearingAidModel hearingAidR;
        public HearingAidModel hearingAidL;
        public List<HearingAidModel> HearingAid = new List<HearingAidModel>();

        public CalibrationModel calibrationR;
        public CalibrationModel calibrationL;
        public List<CalibrationModel> Calibration = new List<CalibrationModel>();

        public HearingAidGainPlotModel hearingAidGainPlotR;
        public HearingAidGainPlotModel hearingAidGainPlotL;
        public List<HearingAidGainPlotModel> HearingAidGainPlot = new List<HearingAidGainPlotModel>();

        public Crud crudViewModel = new Crud();

        public List<AudiogramModel> dataFromDatabase = new List<AudiogramModel>();
        public WaveRule waveRule = new WaveRule();

        public GainPlotUserControl gainPlotUserControlL = new GainPlotUserControl();
        public GainPlotUserControl gainPlotUserControlR = new GainPlotUserControl();

        public bool patientChanged = false;
        public bool patientChoosen = false;

        public bool AudiogramGraphL = false;
        public bool AudiogramGraphR = false;

        public char currentSide = 'L';
        public int currentProgram = 0;
        public int fromProgram = 0;
        public int toProgram = 0;

        public int[] programNumbers;

        public string[] adaptLevels = { "Usuário Iniciante", "Usuário Intermediário", "Usuário Experiente" };
        public double[] windDouble = { 0, 1, 2, 3, 4, 5 };
        public string[] windLevels = { "Sem ventilção", "1 mm", "2 mm", "3 mm", "4 mm", "5 mm" };
        public bool aidInBothSides = false;

        private string[] ReceptorsPath = { @"Resources\RecModel\BVA321.txt", @"Resources\RecModel\BVA530.txt", @"Resources\RecModel\OSPL90.txt" };
        private string[] Receptors = { "BVA321", "BVA530", "Power" };

        private string selectedReceptorPath;

        public bool enableLive = false;

        public bool ForceMuteR = false;
        public bool ForceMuteL = false;

        public CalibrationUserControl()
        {
            InitializeComponent();
            StartTools();
            StartDll();
            StartAdjust();
            CalibrationToolActions();
            InitializeCalibrationForToday();
            FindReceptorR();
            FindReceptorL();
            FittingAssistanceDrawer();

            CurrentGraphL.Content = gainPlotUserControlL;
            CurrentGraphR.Content = gainPlotUserControlR;

            currentProgram = 0;
            fromProgram = 0;
            toProgram = 0;
        }

        public void FittingAssistanceDrawer()
        {
            DrawerLeftContent.Content = fittingAssistanceL;
            DrawerRightContent.Content = fittingAssistanceR;
        }

        public void InitializeCalibrationForToday()
        {
            Calibration.Clear();
            List<CalibrationModel> calibrationsForToday = crudViewModel.GetCalibrationsByDate(DateTime.UtcNow.Date);
            Calibration.AddRange(calibrationsForToday);

            HearingAid.Clear();
            List<HearingAidModel> hearingAidModelsR = crudViewModel.GetHearingAidBySN(GetSerialNumberR());
            List<HearingAidModel> hearingAidModelsL = crudViewModel.GetHearingAidBySN(GetSerialNumberL());
            HearingAid.AddRange(hearingAidModelsR);
            HearingAid.AddRange(hearingAidModelsL);
        }

        private void StartAdjust()
        {
            HandyControl.Controls.Growl.Clear();
            HandyControl.Controls.Growl.ClearGlobal();

            PopulateReceptor();

            if (Properties.Settings.Default.ChipIDR != "Null" && Properties.Settings.Default.ChipIDL != "Null")
            {
                adjustTabUserControlL = new AdjustTabUserControl('L', Properties.Settings.Default.ChipIDL);
                adjustTabUserControlR = new AdjustTabUserControl('R', Properties.Settings.Default.ChipIDR);

                DetectL(true);
                GetValuesL();
                PopulateNProgramsL();
                waveRule.FillTargetGains('L', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);
                GetMicRecL();
                PlotResponseL();
                CheckMuteL();

                DetectR(true);
                GetValuesR();
                PopulateNProgramsR();
                waveRule.FillTargetGains('R', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);
                GetMicRecR();
                PlotResponseR();
                CheckMuteR();

                AddChangeEventsL(adjustTabUserControlL.TabHIAdjust);
                AddChangeEventsR(adjustTabUserControlR.TabHIAdjust);

                ProgramL.SelectedIndex = 0;
                ProgramR.SelectedIndex = 0;

                aidInBothSides = true;

                ChipTools();

                adjustTabUserControlL.toneUserControl.ToneButton.Click += ToneButtonL_Click;
                adjustTabUserControlR.toneUserControl.ToneButton.Click += ToneButtonR_Click;
            }
            else if (Properties.Settings.Default.ChipIDR != "Null" && Properties.Settings.Default.ChipIDL == "Null")
            {
                adjustTabUserControlR = new AdjustTabUserControl('R', Properties.Settings.Default.ChipIDR);

                DetectR(true);
                GetValuesR();
                PopulateNProgramsR();
                waveRule.FillTargetGains('R', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);
                GetMicRecR();
                PlotResponseR();
                CheckMuteR();

                ProgramR.SelectedIndex = 0;
                AddChangeEventsR(adjustTabUserControlR.TabHIAdjust);

                aidInBothSides = false;
                RightChipTools();

                adjustTabUserControlR.toneUserControl.ToneButton.Click += ToneButtonR_Click;
            }
            else if (Properties.Settings.Default.ChipIDR == "Null" && Properties.Settings.Default.ChipIDL != "Null")
            {
                adjustTabUserControlL = new AdjustTabUserControl('L', Properties.Settings.Default.ChipIDL);

                DetectL(true);
                GetValuesL();
                PopulateNProgramsL();
                waveRule.FillTargetGains('L', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);
                GetMicRecL();
                PlotResponseL();
                CheckMuteL();

                ProgramL.SelectedIndex = 0;
                AddChangeEventsL(adjustTabUserControlL.TabHIAdjust);

                aidInBothSides = false;
                LeftChipTools();
                adjustTabUserControlL.toneUserControl.ToneButton.Click += ToneButtonL_Click;
            }
        }

        public void StartDll()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR = new Audion16ViewModel();
                    break;

                case "Audion8":
                    audion8ViewModelR = new Audion8ViewModel();
                    break;

                case "Audion6":
                    audion6ViewModelR = new Audion6ViewModel();
                    break;

                case "Audion4":
                    audion4ViewModelR = new Audion4ViewModel();
                    break;

                case "SpinNR":
                    spinNRViewModelR = new SpinNRViewModel();
                    break;

                case "Null":
                    break;
            }

            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL = new Audion16ViewModel();
                    break;

                case "Audion8":
                    audion8ViewModelL = new Audion8ViewModel();
                    break;

                case "Audion6":
                    audion6ViewModelL = new Audion6ViewModel();
                    break;

                case "Audion4":
                    audion4ViewModelL = new Audion4ViewModel();
                    break;

                case "SpinNR":
                    spinNRViewModelL = new SpinNRViewModel();
                    break;

                case "Null":
                    break;
            }
        }

        public void ErrorSolverR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    if (audion16ViewModelR.errorCode == 2)
                    {
                        DetectR(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "Audion8":
                    if (audion8ViewModelR.errorCode == 2)
                    {
                        DetectR(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "Audion6":
                    if (audion6ViewModelR.errorCode == 2)
                    {
                        DetectR(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "Audion4":
                    if (audion4ViewModelR.errorCode == 2)
                    {
                        DetectR(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "SpinNR":
                    if (spinNRViewModelR.errorCode == 2)
                    {
                        DetectR(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "Null":
                    break;
            }
        }

        public void ErrorSolverL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    if (audion16ViewModelL.errorCode == 2)
                    {
                        DetectL(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "Audion8":
                    if (audion8ViewModelL.errorCode == 2)
                    {
                        DetectL(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "Audion6":
                    if (audion6ViewModelL.errorCode == 2)
                    {
                        DetectL(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "Audion4":
                    if (audion4ViewModelL.errorCode == 2)
                    {
                        DetectL(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "SpinNR":
                    if (spinNRViewModelL.errorCode == 2)
                    {
                        DetectL(true);
                        CleanMessages();
                        Growl.Info("Sua programadora desconectou e a sua ultima operação não foi registrada," +
                            " porém a programadora já foi reconectada automaticamente, por favor faça sua ultima operação novamente");
                    }
                    break;

                case "Null":
                    break;
            }
        }

        public void StartTools()
        {
            PatientTextBox.Text = Properties.Settings.Default.patientName;

            WindLevel.ItemsSource = windLevels;
            WindLevel.SelectedIndex = 0;

            AdaptLevel.ItemsSource = adaptLevels;
            AdaptLevel.SelectedIndex = 0;

            PatientAudiograms.IsEnabled = false;
            AudiogramGraphL = false;
            AudiogramGraphR = false;

            ExpanderAparelho.IsExpanded = true;
            ExpanderAutofit.IsExpanded = false;
            ExpanderMemory.IsExpanded = false;
            ExpanderBurn.IsExpanded = false;
            ExpanderHelp.IsExpanded = false;

            NProgramsGrid.Visibility = Visibility.Visible;

            AutofitGrid.Visibility = Visibility.Collapsed;
            WindGrid.Visibility = Visibility.Collapsed;
            AdaptGrid.Visibility = Visibility.Collapsed;
            CopyGrid.Visibility = Visibility.Collapsed;
            CurrentProgramGrid.Visibility = Visibility.Collapsed;
            GetGrid.Visibility = Visibility.Collapsed;
            ReadGrid.Visibility = Visibility.Collapsed;
            BurnGrid.Visibility = Visibility.Collapsed;
            HelpGrid.Visibility = Visibility.Collapsed;

            currentProgram = (int)ProgramR.SelectedIndex;
        }

        public void CalibrationToolActions()
        {
            RGainGraphButton.Click += RGainGraphButton_Click;
            LGainGraphButton.Click += LGainGraphButton_Click;

            RAudiogramGraphButton.Click += RAudiogramGraphButton_Click;
            LAudiogramGraphButton.Click += LAudiogramGraphButton_Click;

            SearchPatient.Click += SearchPatient_Click;
            PatientTextBox.TextChanged += PatientTextBox_TextChanged;
            PatientAudiograms.SelectionChanged += PatientAudiograms_SelectionChanged;

            ExpanderAparelho.Expanded += ExpanderAparelho_Expanded;
            ExpanderAparelho.Collapsed += ExpanderAparelho_Collapsed;

            ExpanderAutofit.Expanded += ExpanderAutofit_Expanded;
            ExpanderAutofit.Collapsed += ExpanderAutofit_Collapsed;

            ExpanderProgram.Expanded += ExpanderProgram_Expanded;
            ExpanderProgram.Collapsed += ExpanderProgram_Collapsed;

            ExpanderMemory.Expanded += ExpanderMemory_Expanded;
            ExpanderMemory.Collapsed += ExpanderMemory_Collapsed;

            ExpanderBurn.Expanded += ExpanderBurn_Expanded;
            ExpanderBurn.Collapsed += ExpanderBurn_Collapsed;

            ExpanderHelp.Expanded += ExpanderHelp_Expanded;
            ExpanderHelp.Collapsed += ExpanderHelp_Collapsed;

            CopyPR.Click += CopyPR_Click;
            CopyPL.Click += CopyPL_Click;

            AutofitL.Click += AutofitL_Click;
            AutofitR.Click += AutofitR_Click;

            GetL.Click += GetL_Click;
            GetR.Click += GetR_Click;

            ReadL.Click += ReadL_Click;
            ReadR.Click += ReadR_Click;

            BurnL.Click += BurnL_Click;
            BurnR.Click += BurnR_Click;

            HelpL.Click += HelpL_Click;
            HelpR.Click += HelpR_Click;

            DrawerLeft.Opened += DrawerLeft_Opened;
            DrawerRight.Opened += DrawerRight_Opened;

            MuteButtonL.Click += MuteButtonL_Click;
            MuteButtonR.Click += MuteButtonR_Click;

            DetectButtonL.Click += DetectButtonL_Click;
            DetectButtonR.Click += DetectButtonR_Click;

            CopyProgramL.SelectionChanged += CopyProgramL_SelectionChanged;
            CopyProgramR.SelectionChanged += CopyProgramR_SelectionChanged;

            ProgramCL.SelectionChanged += ProgramCL_SelectionChanged;
            ProgramCR.SelectionChanged += ProgramCR_SelectionChanged;

            ProgramL.SelectionChanged += ProgramL_SelectionChanged;
            ProgramR.SelectionChanged += ProgramR_SelectionChanged;

            NProgramL.SelectionChanged += NProgramL_SelectionChanged;
            NProgramR.SelectionChanged += NProgramR_SelectionChanged;

            ReceptorR.SelectionChanged += Receptor_SelectionChanged;
            ReceptorL.SelectionChanged += Receptor_SelectionChanged;

            SerialNumberR.Click += SerialNumberR_Click;
            SerialNumberL.Click += SerialNumberL_Click;
        }

        private void DrawerRight_Opened(object sender, RoutedEventArgs e)
        {
            DrawerLeftContent.Content = fittingAssistanceL;
        }

        private void DrawerLeft_Opened(object sender, RoutedEventArgs e)
        {
            DrawerRightContent.Content = fittingAssistanceR;
        }

        private void SerialNumberL_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.ChipIDL + " Número Serial: " + GetSerialNumberL());
        }

        private void SerialNumberR_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.ChipIDR + " Número Serial: " + GetSerialNumberR());
        }

        public void FindReceptorR()
        {
            int position = Array.IndexOf(Receptors, crudViewModel.GetAtributeString("receptor", "dbo.hearingaid", "serialnumber", GetSerialNumberR()));
            Console.WriteLine($"ReceptorR: {position}");
            if (position >= 0)
            {
                ReceptorR.SelectedIndex = position;
            }
            else
            {
                ReceptorR.SelectedIndex = 0;
            }
        }

        public void FindReceptorL()
        {
            int position = Array.IndexOf(Receptors, crudViewModel.GetAtributeString("receptor", "dbo.hearingaid", "serialnumber", GetSerialNumberL()));
            Console.WriteLine($"ReceptorR: {position}");
            if (position >= 0)
            {
                ReceptorL.SelectedIndex = position;
            }
            else
            {
                ReceptorL.SelectedIndex = 0;
            }
        }

        private void AddChangeEventsR(Control control)
        {
            if (control is System.Windows.Controls.TabControl tabControl)
            {
                foreach (System.Windows.Controls.TabItem tabItem in tabControl.Items)
                {
                    ProcessTabItemR(tabItem);
                }
            }
        }

        private void ProcessTabItemR(TabItem tabItem)
        {
            if (tabItem == null || tabItem.Content == null)
            {
                return;
            }

            if (tabItem.Content is System.Windows.Controls.ScrollViewer scrollViewer)
            {
                ProcessControlsInScrollViewerR(scrollViewer);
            }
        }

        private void ProcessControlsInScrollViewerR(System.Windows.Controls.ScrollViewer scrollViewer)
        {
            if (scrollViewer.Content == null)
            {
                return;
            }

            if (scrollViewer.Content is UserControl userControl)
            {
                try
                {
                    ProcessFrameworkElementR(userControl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProcessFrameworkElementR(FrameworkElement element)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(element).OfType<FrameworkElement>())
            {
                if (child is ComboBox comboBox)
                {
                    AttachEventHandlerR(comboBox);
                }
                else if (child is Slider slider)
                {
                    AttachEventHandlerR(slider);
                }
                else if (child is FrameworkElement frameworkElement)
                {
                    ProcessFrameworkElementR(frameworkElement);
                }
            }
        }

        private void AttachEventHandlerR(Control control)
        {
            if (control is ComboBox comboBox)
            {
                comboBox.PreviewMouseWheel += ComboBox_PreviewMouseWheel;
                comboBox.SelectionChanged += ElementChangedR;
                comboBox.MouseEnter += ElementEnterR;
                comboBox.MouseLeave += ElementLeaveR;
            }
            else if (control is Slider slider)
            {
                slider.PreviewMouseUp += ElementChangedR;
                slider.MouseEnter += ElementEnterR;
                slider.MouseLeave += ElementLeaveR;
            }
        }

        private void ComboBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void ElementChangedR(object sender, EventArgs e)
        {
            if (enableLive == true)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                CleanMessages();
                waveRule.FillTargetGains('R', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);
                SetValuesR();
                WriteSetR(true);
                GetMicRecR();
                CheckMuteR();
                PlotResponseR();
                Mouse.OverrideCursor = null;

                ErrorSolverR();
            }
        }

        private void ElementEnterR(object sender, EventArgs e)
        {
            enableLive = true;
        }

        private void ElementLeaveR(object sender, EventArgs e)
        {
            enableLive = false;
        }

        private void AddChangeEventsL(Control control)
        {
            if (control is System.Windows.Controls.TabControl tabControl)
            {
                foreach (TabItem tabItem in tabControl.Items)
                {
                    ProcessTabItemL(tabItem);
                }
            }
        }

        private void ProcessTabItemL(TabItem tabItem)
        {
            if (tabItem == null || tabItem.Content == null)
            {
                return;
            }

            if (tabItem.Content is System.Windows.Controls.ScrollViewer scrollViewer)
            {
                ProcessControlsInScrollViewerL(scrollViewer);
            }
        }

        private void ProcessControlsInScrollViewerL(System.Windows.Controls.ScrollViewer scrollViewer)
        {
            if (scrollViewer.Content == null)
            {
                return;
            }

            if (scrollViewer.Content is UserControl userControl)
            {
                try
                {
                    ProcessFrameworkElementL(userControl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProcessFrameworkElementL(FrameworkElement element)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(element).OfType<FrameworkElement>())
            {
                if (child is ComboBox comboBox)
                {
                    AttachEventHandlerL(comboBox);
                }
                else if (child is Slider slider)
                {
                    AttachEventHandlerL(slider);
                }
                else if (child is FrameworkElement frameworkElement)
                {
                    ProcessFrameworkElementL(frameworkElement);
                }
            }
        }

        private void AttachEventHandlerL(Control control)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (control is ComboBox comboBox)
            {
                comboBox.PreviewMouseWheel += ComboBox_PreviewMouseWheel;
                comboBox.SelectionChanged += ElementChangedL;
                comboBox.MouseEnter += ElementEnterL;
                comboBox.MouseLeave += ElementLeaveL;
            }
            else if (control is Slider slider)
            {
                slider.PreviewMouseUp += ElementChangedL;
                slider.MouseEnter += ElementEnterL;
                slider.MouseLeave += ElementLeaveL;
            }
            Mouse.OverrideCursor = null;
        }

        private void ElementChangedL(object sender, EventArgs e)
        {
            if (enableLive == true)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                CleanMessages();
                waveRule.FillTargetGains('L', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);
                SetValuesL();
                WriteSetL(true);
                GetMicRecL();
                CheckMuteL();
                PlotResponseL();
                Mouse.OverrideCursor = null;

                ErrorSolverL();
            }
        }

        private void ElementEnterL(object sender, EventArgs e)
        {
            enableLive = true;
        }

        private void ElementLeaveL(object sender, EventArgs e)
        {
            enableLive = false;
        }

        private void PopulateComboBox(ComboBox comboBox, int maxPrograms)
        {
            comboBox.Items.Clear();

            for (int i = 1; i <= maxPrograms; i++)
            {
                comboBox.Items.Add(i);
            }
        }

        private void Receptor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null && comboBox.SelectedItem != null)
            {
                int selectedIndex = comboBox.SelectedIndex;
                if (selectedIndex != -1)
                {
                    selectedReceptorPath = Path.Combine(Directory.GetCurrentDirectory(), ReceptorsPath[selectedIndex]);
                    bool isRightSide = comboBox.Name == "ReceptorR";

                    if (isRightSide)
                    {
                        switch (Properties.Settings.Default.ChipIDR)
                        {
                            case "Audion16":
                                audion16ViewModelR.defaultReceptor = selectedReceptorPath;
                                break;

                            case "Audion8":
                                audion8ViewModelR.defaultReceptor = selectedReceptorPath;
                                break;

                            case "Audion6":
                                audion6ViewModelR.defaultReceptor = selectedReceptorPath;
                                break;

                            case "Audion4":
                                audion4ViewModelR.defaultReceptor = selectedReceptorPath;
                                break;

                            case "SpinNR":
                                spinNRViewModelR.defaultReceptor = selectedReceptorPath;
                                break;
                        }
                        CurrentGraphR.Content = new StaticAudiographUserControl('R', "Orelha Direita", true, ReceptorR.SelectedIndex);
                    }
                    else
                    {
                        switch (Properties.Settings.Default.ChipIDL)
                        {
                            case "Audion16":
                                audion16ViewModelL.defaultReceptor = selectedReceptorPath;
                                break;

                            case "Audion8":
                                audion8ViewModelL.defaultReceptor = selectedReceptorPath;
                                break;

                            case "Audion6":
                                audion6ViewModelL.defaultReceptor = selectedReceptorPath;
                                break;

                            case "Audion4":
                                audion4ViewModelL.defaultReceptor = selectedReceptorPath;
                                break;

                            case "SpinNR":
                                spinNRViewModelL.defaultReceptor = selectedReceptorPath;
                                break;
                        }
                        CurrentGraphL.Content = new StaticAudiographUserControl('L', "Orelha Esquerda", true, ReceptorL.SelectedIndex);
                    }
                }
            }
        }

        public void PopulateReceptor()
        {
            ReceptorR.Items.Clear();
            ReceptorL.Items.Clear();

            foreach (string receptorName in Receptors)
            {
                ReceptorR.Items.Add(receptorName);
                ReceptorL.Items.Add(receptorName);
            }
            ReceptorR.SelectedIndex = 0;
            ReceptorL.SelectedIndex = 0;
        }

        public void PopulateProgramR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    programNumbers = new int[audion16ViewModelR.Num_Programs + 1];
                    for (int i = 0; i <= audion16ViewModelR.Num_Programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion8":
                    programNumbers = new int[audion8ViewModelR.number_of_programs + 1];
                    for (int i = 0; i <= audion8ViewModelR.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion6":
                    programNumbers = new int[audion6ViewModelR.number_of_programs + 1];
                    for (int i = 0; i <= audion6ViewModelR.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion4":
                    programNumbers = new int[audion4ViewModelR.number_of_programs + 1];
                    for (int i = 0; i <= audion4ViewModelR.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "SpinNR":
                    programNumbers = new int[spinNRViewModelR.number_of_programs + 1];
                    for (int i = 0; i <= spinNRViewModelR.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;
            }

            ProgramR.ItemsSource = programNumbers;
            ProgramR.SelectedIndex = 0;

            ProgramCR.ItemsSource = programNumbers;
            ProgramCR.SelectedIndex = 0;

            CopyProgramR.ItemsSource = programNumbers;
            CopyProgramR.SelectedIndex = 0;
        }

        public void PopulateProgramL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    programNumbers = new int[audion16ViewModelL.Num_Programs + 1];
                    for (int i = 0; i <= audion16ViewModelL.Num_Programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion8":
                    programNumbers = new int[audion8ViewModelL.number_of_programs + 1];
                    for (int i = 0; i <= audion8ViewModelL.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion6":
                    programNumbers = new int[audion6ViewModelL.number_of_programs + 1];
                    for (int i = 0; i <= audion6ViewModelL.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion4":
                    programNumbers = new int[audion4ViewModelL.number_of_programs + 1];
                    for (int i = 0; i <= audion4ViewModelL.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "SpinNR":
                    programNumbers = new int[spinNRViewModelL.number_of_programs + 1];
                    for (int i = 0; i <= spinNRViewModelL.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;
            }

            ProgramL.ItemsSource = programNumbers;
            ProgramL.SelectedIndex = 0;

            ProgramCL.ItemsSource = programNumbers;
            ProgramCL.SelectedIndex = 0;

            CopyProgramL.ItemsSource = programNumbers;
            CopyProgramL.SelectedIndex = 0;
        }

        public void PopulateNProgramsR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    PopulateComboBox(NProgramR, 5);
                    GetNumProgramR();
                    NProgramR.SelectedIndex = audion16ViewModelR.Num_Programs;
                    break;

                case "Audion8":
                    PopulateComboBox(NProgramR, 4);
                    GetNumProgramR();
                    NProgramR.SelectedIndex = audion8ViewModelR.number_of_programs;

                    break;

                case "Audion6":
                    PopulateComboBox(NProgramR, 4);
                    GetNumProgramR();
                    NProgramR.SelectedIndex = audion6ViewModelR.number_of_programs;

                    break;

                case "Audion4":
                    PopulateComboBox(NProgramR, 4);
                    GetNumProgramR();
                    NProgramR.SelectedIndex = audion4ViewModelR.number_of_programs;

                    break;

                case "SpinNR":
                    PopulateComboBox(NProgramR, 4);
                    GetNumProgramR();
                    NProgramR.SelectedIndex = spinNRViewModelR.number_of_programs;
                    break;
            }
        }

        public void PopulateNProgramsL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    PopulateComboBox(NProgramL, 5);
                    GetNumProgramL();
                    NProgramL.SelectedIndex = audion16ViewModelL.Num_Programs;
                    break;

                case "Audion8":
                    PopulateComboBox(NProgramL, 4);
                    GetNumProgramL();
                    NProgramL.SelectedIndex = audion8ViewModelL.number_of_programs;
                    break;

                case "Audion6":
                    PopulateComboBox(NProgramL, 4);
                    GetNumProgramL();
                    NProgramL.SelectedIndex = audion6ViewModelL.number_of_programs;
                    break;

                case "Audion4":
                    PopulateComboBox(NProgramL, 4);
                    GetNumProgramL();
                    NProgramL.SelectedIndex = audion4ViewModelL.number_of_programs;
                    break;

                case "SpinNR":
                    PopulateComboBox(NProgramL, 4);
                    GetNumProgramL();
                    NProgramL.SelectedIndex = spinNRViewModelL.number_of_programs;
                    break;
            }
        }

        public void NProgramsR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.Num_Programs = NProgramR.SelectedIndex;
                    break;

                case "Audion8":
                    audion8ViewModelR.number_of_programs = NProgramR.SelectedIndex;
                    break;

                case "Audion6":
                    audion6ViewModelR.number_of_programs = NProgramR.SelectedIndex;
                    break;

                case "Audion4":
                    audion4ViewModelR.number_of_programs = NProgramR.SelectedIndex;
                    break;

                case "SpinNR":
                    spinNRViewModelR.number_of_programs = NProgramR.SelectedIndex;
                    break;
            }
        }

        public void NProgramsL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.Num_Programs = NProgramL.SelectedIndex;
                    break;

                case "Audion8":
                    audion8ViewModelL.number_of_programs = NProgramL.SelectedIndex;
                    break;

                case "Audion6":
                    audion6ViewModelL.number_of_programs = NProgramL.SelectedIndex;
                    break;

                case "Audion4":
                    audion4ViewModelL.number_of_programs = NProgramL.SelectedIndex;
                    break;

                case "SpinNR":
                    spinNRViewModelL.number_of_programs = NProgramL.SelectedIndex;
                    break;
            }
        }

        public void StartProgrammerR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.StartProgrammer();
                    break;

                case "Audion8":
                    audion8ViewModelR.StartProgrammer();
                    break;

                case "Audion6":
                    audion6ViewModelR.StartProgrammer();
                    break;

                case "Audion4":
                    audion4ViewModelR.StartProgrammer();
                    break;

                case "SpinNR":
                    spinNRViewModelR.StartProgrammer();
                    break;
            }
        }

        public void StartProgrammerL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.StartProgrammer();
                    break;

                case "Audion8":
                    audion8ViewModelL.StartProgrammer();
                    break;

                case "Audion6":
                    audion6ViewModelL.StartProgrammer();
                    break;

                case "Audion4":
                    audion4ViewModelL.StartProgrammer();
                    break;

                case "SpinNR":
                    spinNRViewModelL.StartProgrammer();
                    break;
            }
        }

        public void CloseProgrammerR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.CloseInterface();
                    break;

                case "Audion8":
                    audion8ViewModelR.CloseProgramer();
                    break;

                case "Audion6":
                    audion6ViewModelR.CloseProgramer();
                    break;

                case "Audion4":
                    audion4ViewModelR.CloseProgramer();
                    break;

                case "SpinNR":
                    spinNRViewModelR.CloseProgramer();
                    break;
            }
        }

        public void CloseProgrammerL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.CloseInterface();
                    break;

                case "Audion8":
                    audion8ViewModelL.CloseProgramer();
                    break;

                case "Audion6":
                    audion6ViewModelL.CloseProgramer();
                    break;

                case "Audion4":
                    audion4ViewModelL.CloseProgramer();
                    break;

                case "SpinNR":
                    spinNRViewModelL.CloseProgramer();
                    break;
            }
        }

        public void GetMicRecR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.GetMicRec();
                    break;

                case "Audion8":
                    audion8ViewModelR.GetMicRec();
                    break;

                case "Audion6":
                    audion6ViewModelR.GetMicRec();
                    break;

                case "Audion4":
                    audion4ViewModelR.GetMicRec();
                    break;

                case "SpinNR":
                    spinNRViewModelR.GetMicRec();
                    break;
            }
        }

        public void GetMicRecL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.GetMicRec();
                    break;

                case "Audion8":
                    audion8ViewModelL.GetMicRec();
                    break;

                case "Audion6":
                    audion6ViewModelL.GetMicRec();
                    break;

                case "Audion4":
                    audion4ViewModelL.GetMicRec();
                    break;

                case "SpinNR":
                    spinNRViewModelL.GetMicRec();
                    break;
            }
        }

        public void GetNumProgramR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    programNumbers = new int[audion16ViewModelR.Num_Programs + 1];
                    for (int i = 0; i <= audion16ViewModelR.Num_Programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion8":
                    programNumbers = new int[audion8ViewModelR.number_of_programs + 1];
                    for (int i = 0; i <= audion8ViewModelR.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion6":
                    programNumbers = new int[audion6ViewModelR.number_of_programs + 1];
                    for (int i = 0; i <= audion6ViewModelR.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion4":
                    programNumbers = new int[audion4ViewModelR.number_of_programs + 1];
                    for (int i = 0; i <= audion4ViewModelR.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "SpinNR":
                    programNumbers = new int[spinNRViewModelR.number_of_programs + 1];
                    for (int i = 0; i <= spinNRViewModelR.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;
            }
            ProgramR.ItemsSource = programNumbers;
            ProgramR.SelectedIndex = 0;

            ProgramCR.ItemsSource = programNumbers;
            ProgramCR.SelectedIndex = 0;

            CopyProgramR.ItemsSource = programNumbers;
            CopyProgramR.SelectedIndex = 0;
        }

        public void GetNumProgramL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    programNumbers = new int[audion16ViewModelL.Num_Programs + 1];
                    for (int i = 0; i <= audion16ViewModelL.Num_Programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion8":
                    programNumbers = new int[audion8ViewModelL.number_of_programs + 1];
                    for (int i = 0; i <= audion8ViewModelL.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion6":
                    programNumbers = new int[audion6ViewModelL.number_of_programs + 1];
                    for (int i = 0; i <= audion6ViewModelL.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "Audion4":
                    programNumbers = new int[audion4ViewModelL.number_of_programs + 1];
                    for (int i = 0; i <= audion4ViewModelL.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;

                case "SpinNR":
                    programNumbers = new int[spinNRViewModelL.number_of_programs + 1];
                    for (int i = 0; i <= spinNRViewModelL.number_of_programs; i++)
                    {
                        programNumbers[i] = i + 1;
                    }
                    break;
            }
            ProgramL.ItemsSource = programNumbers;
            ProgramL.SelectedIndex = 0;

            ProgramCL.ItemsSource = programNumbers;
            ProgramCL.SelectedIndex = 0;

            CopyProgramL.ItemsSource = programNumbers;
            CopyProgramL.SelectedIndex = 0;
        }

        public void ChangeSideR()
        {
            currentSide = 'R';
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.ChangeSide('R');
                    break;

                case "Audion8":
                    audion8ViewModelR.ChangeSide('R');
                    break;

                case "Audion6":
                    audion6ViewModelR.ChangeSide('R');
                    break;

                case "Audion4":
                    audion4ViewModelR.ChangeSide('R');
                    break;

                case "SpinNR":
                    spinNRViewModelR.ChangeSide('R');
                    break;
            }
        }

        public void ChangeSideL()
        {
            currentSide = 'L';
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.ChangeSide('L');
                    break;

                case "Audion8":
                    audion8ViewModelL.ChangeSide('L');
                    break;

                case "Audion6":
                    audion6ViewModelL.ChangeSide('L');
                    break;

                case "Audion4":
                    audion4ViewModelL.ChangeSide('L');
                    break;

                case "SpinNR":
                    spinNRViewModelL.ChangeSide('L');
                    break;
            }
        }

        public void SetProgramR()
        {
            if (currentProgram != (int)ProgramR.SelectedIndex)
            {
                if ((int)ProgramR.SelectedIndex < 0)
                {
                    currentProgram = 0;
                    switch (Properties.Settings.Default.ChipIDR)
                    {
                        case "Audion16":
                            audion16ViewModelR.SetCurrentProgram(currentProgram);
                            break;

                        case "Audion8":
                            audion8ViewModelR.SetCurrentProgram(currentProgram);
                            break;

                        case "Audion6":
                            audion6ViewModelR.SetCurrentProgram(currentProgram);
                            break;

                        case "Audion4":
                            audion4ViewModelR.SetCurrentProgram(currentProgram);
                            break;

                        case "SpinNR":
                            spinNRViewModelR.SetCurrentProgram(currentProgram);
                            break;
                    }
                }
                else
                {
                    switch (Properties.Settings.Default.ChipIDR)
                    {
                        case "Audion16":
                            audion16ViewModelR.SetCurrentProgram((int)ProgramR.SelectedIndex);
                            break;

                        case "Audion8":
                            audion8ViewModelR.SetCurrentProgram((int)ProgramR.SelectedIndex);
                            break;

                        case "Audion6":
                            audion6ViewModelR.SetCurrentProgram((int)ProgramR.SelectedIndex);
                            break;

                        case "Audion4":
                            audion4ViewModelR.SetCurrentProgram((int)ProgramR.SelectedIndex);
                            break;

                        case "SpinNR":
                            spinNRViewModelR.SetCurrentProgram((int)ProgramR.SelectedIndex);
                            break;
                    }

                    currentProgram = (int)ProgramR.SelectedIndex;
                }
            }
        }

        public void SetProgramL()
        {
            if (currentProgram != (int)ProgramL.SelectedIndex)
            {
                if ((int)ProgramL.SelectedIndex < 0)
                {
                    currentProgram = 0;
                    switch (Properties.Settings.Default.ChipIDL)
                    {
                        case "Audion16":
                            audion16ViewModelL.SetCurrentProgram(currentProgram);
                            break;

                        case "Audion8":
                            audion8ViewModelL.SetCurrentProgram(currentProgram);
                            break;

                        case "Audion6":
                            audion6ViewModelL.SetCurrentProgram(currentProgram);
                            break;

                        case "Audion4":
                            audion4ViewModelL.SetCurrentProgram(currentProgram);
                            break;

                        case "SpinNR":
                            spinNRViewModelL.SetCurrentProgram(currentProgram);
                            break;
                    }
                }
                else
                {
                    switch (Properties.Settings.Default.ChipIDL)
                    {
                        case "Audion16":
                            audion16ViewModelL.SetCurrentProgram((int)ProgramL.SelectedIndex);
                            break;

                        case "Audion8":
                            audion8ViewModelL.SetCurrentProgram((int)ProgramL.SelectedIndex);
                            break;

                        case "Audion6":
                            audion6ViewModelL.SetCurrentProgram((int)ProgramL.SelectedIndex);
                            break;

                        case "Audion4":
                            audion4ViewModelL.SetCurrentProgram((int)ProgramL.SelectedIndex);
                            break;

                        case "SpinNR":
                            spinNRViewModelL.SetCurrentProgram((int)ProgramL.SelectedIndex);
                            break;
                    }

                    currentProgram = (int)ProgramL.SelectedIndex;
                }
            }
        }

        public void SetProgramR(int program)
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.SetCurrentProgram(program);
                    break;

                case "Audion8":
                    audion8ViewModelR.SetCurrentProgram(program);
                    break;

                case "Audion6":
                    audion6ViewModelR.SetCurrentProgram(program);
                    break;

                case "Audion4":
                    audion4ViewModelR.SetCurrentProgram(program);
                    break;

                case "SpinNR":
                    spinNRViewModelR.SetCurrentProgram(program);
                    break;
            }
        }

        public void SetProgramL(int program)
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.SetCurrentProgram(program);
                    break;

                case "Audion8":
                    audion8ViewModelL.SetCurrentProgram(program);
                    break;

                case "Audion6":
                    audion6ViewModelL.SetCurrentProgram(program);
                    break;

                case "Audion4":
                    audion4ViewModelL.SetCurrentProgram(program);
                    break;

                case "SpinNR":
                    spinNRViewModelL.SetCurrentProgram(program);
                    break;
            }
        }

        public void ReadGetR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.ReadVM();
                    break;

                case "Audion8":
                    audion8ViewModelR.ReadVM();
                    break;

                case "Audion6":
                    audion6ViewModelR.ReadVM();
                    break;

                case "Audion4":
                    audion4ViewModelR.ReadVM();
                    break;

                case "SpinNR":
                    spinNRViewModelR.ReadVM();
                    break;
            }
        }

        public void ReadGetL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.ReadVM();
                    break;

                case "Audion8":
                    audion8ViewModelL.ReadVM();
                    break;

                case "Audion6":
                    audion6ViewModelL.ReadVM();
                    break;

                case "Audion4":
                    audion4ViewModelL.ReadVM();
                    break;

                case "SpinNR":
                    spinNRViewModelL.ReadVM();
                    break;
            }
        }

        public void GetStandardR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.GetResetParam();
                    break;

                case "Audion8":
                    audion8ViewModelR.GetResetParam();
                    break;

                case "Audion6":
                    audion6ViewModelR.GetResetParam();
                    break;

                case "Audion4":
                    audion4ViewModelR.GetResetParam();
                    break;

                case "SpinNR":
                    spinNRViewModelR.GetResetParam();
                    break;
            }
        }

        public void GetStandardL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.GetResetParam();
                    break;

                case "Audion8":
                    audion8ViewModelL.GetResetParam();
                    break;

                case "Audion6":
                    audion6ViewModelL.GetResetParam();
                    break;

                case "Audion4":
                    audion4ViewModelL.GetResetParam();
                    break;

                case "SpinNR":
                    spinNRViewModelL.GetResetParam();
                    break;
            }
        }

        public void GetValuesR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":

                    adjustTabUserControlR.algorithmUserControl.Audion16AlgorithmGetValues(audion16ViewModelR.feedback_canceller, audion16ViewModelR.ADir_Sensitivity, audion16ViewModelR.noise_reduction, audion16ViewModelR.Noise_Level, audion16ViewModelR.wind_suppression);

                    adjustTabUserControlR.equalyzerUserControl.Audion16EqualyzerGetValues(audion16ViewModelR.beq_gain_1, audion16ViewModelR.beq_gain_2, audion16ViewModelR.beq_gain_3, audion16ViewModelR.beq_gain_4, audion16ViewModelR.beq_gain_5, audion16ViewModelR.beq_gain_6,
                                                                                  audion16ViewModelR.beq_gain_7, audion16ViewModelR.beq_gain_8, audion16ViewModelR.beq_gain_9, audion16ViewModelR.beq_gain_10, audion16ViewModelR.beq_gain_11, audion16ViewModelR.beq_gain_12,
                                                                                  audion16ViewModelR.beq_gain_13, audion16ViewModelR.beq_gain_14, audion16ViewModelR.beq_gain_15, audion16ViewModelR.beq_gain_16);

                    adjustTabUserControlR.microphoneUserControl.Audion16MicrophoneGetValues(audion16ViewModelR.input_mux, audion16ViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.Audion16OutputGetValues(audion16ViewModelR.comp_threshold_1, audion16ViewModelR.comp_threshold_2, audion16ViewModelR.comp_threshold_3, audion16ViewModelR.comp_threshold_4,
                                                         audion16ViewModelR.comp_threshold_5, audion16ViewModelR.comp_threshold_6, audion16ViewModelR.comp_threshold_7, audion16ViewModelR.comp_threshold_8,
                                                         audion16ViewModelR.comp_threshold_9, audion16ViewModelR.comp_threshold_10, audion16ViewModelR.comp_threshold_11, audion16ViewModelR.comp_threshold_12,
                                                         audion16ViewModelR.comp_threshold_13, audion16ViewModelR.comp_threshold_14, audion16ViewModelR.comp_threshold_15, audion16ViewModelR.comp_threshold_16,
                                                         audion16ViewModelR.mpo_threshold_1, audion16ViewModelR.mpo_threshold_2, audion16ViewModelR.mpo_threshold_3, audion16ViewModelR.mpo_threshold_4,
                                                         audion16ViewModelR.mpo_threshold_5, audion16ViewModelR.mpo_threshold_6, audion16ViewModelR.mpo_threshold_7, audion16ViewModelR.mpo_threshold_8,
                                                         audion16ViewModelR.mpo_threshold_9, audion16ViewModelR.mpo_threshold_10, audion16ViewModelR.mpo_threshold_11, audion16ViewModelR.mpo_threshold_12,
                                                         audion16ViewModelR.mpo_threshold_13, audion16ViewModelR.mpo_threshold_14, audion16ViewModelR.mpo_threshold_15, audion16ViewModelR.mpo_threshold_16,
                                                         audion16ViewModelR.comp_ratio_1, audion16ViewModelR.comp_ratio_2, audion16ViewModelR.comp_ratio_3, audion16ViewModelR.comp_ratio_4,
                                                         audion16ViewModelR.comp_ratio_5, audion16ViewModelR.comp_ratio_6, audion16ViewModelR.comp_ratio_7, audion16ViewModelR.comp_ratio_8,
                                                         audion16ViewModelR.comp_ratio_9, audion16ViewModelR.comp_ratio_10, audion16ViewModelR.comp_ratio_11, audion16ViewModelR.comp_ratio_12,
                                                         audion16ViewModelR.comp_ratio_13, audion16ViewModelR.comp_ratio_14, audion16ViewModelR.comp_ratio_15, audion16ViewModelR.comp_ratio_16);

                    adjustTabUserControlR.powerOnUserControl.Audion16PowerOnGetValues(audion16ViewModelR.Power_On_Delay, audion16ViewModelR.Power_On_Level);

                    adjustTabUserControlR.toneUserControl.Audion16ToneGetValues(audion16ViewModelR.Tone_Frequency, audion16ViewModelR.Prompt_Level, audion16ViewModelR.Warning_Prompt_Mode, audion16ViewModelR.Program_Prompt_Mode);

                    break;

                case "Audion8":

                    adjustTabUserControlR.algorithmUserControl.Audion8AlgorithmGetValues(audion8ViewModelR.FBC_Enable, audion8ViewModelR.AD_Sens, audion8ViewModelR.Noise_Reduction, audion8ViewModelR.Noise_Level);

                    adjustTabUserControlR.equalyzerUserControl.Audion8EqualyzerGetValues(audion8ViewModelR.BEQ1_gain, audion8ViewModelR.BEQ2_gain, audion8ViewModelR.BEQ3_gain, audion8ViewModelR.BEQ4_gain, audion8ViewModelR.BEQ5_gain, audion8ViewModelR.BEQ6_gain,
                                                                                  audion8ViewModelR.BEQ7_gain, audion8ViewModelR.BEQ8_gain, audion8ViewModelR.BEQ9_gain, audion8ViewModelR.BEQ10_gain, audion8ViewModelR.BEQ11_gain, audion8ViewModelR.BEQ12_gain);

                    adjustTabUserControlR.microphoneUserControl.Audion8MicrophoneGetValues(audion8ViewModelR.input_mux, audion8ViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.Audion8OutputGetValues(audion8ViewModelR.C1_TK, audion8ViewModelR.C2_TK, audion8ViewModelR.C3_TK, audion8ViewModelR.C4_TK, audion8ViewModelR.C5_TK, audion8ViewModelR.C6_TK, audion8ViewModelR.C7_TK, audion8ViewModelR.C8_TK,
                                                         audion8ViewModelR.C1_MPO, audion8ViewModelR.C2_MPO, audion8ViewModelR.C3_MPO, audion8ViewModelR.C4_MPO, audion8ViewModelR.C5_MPO, audion8ViewModelR.C6_MPO, audion8ViewModelR.C7_MPO, audion8ViewModelR.C8_MPO,
                                                         audion8ViewModelR.C1_Ratio, audion8ViewModelR.C2_Ratio, audion8ViewModelR.C3_Ratio, audion8ViewModelR.C4_Ratio, audion8ViewModelR.C5_Ratio, audion8ViewModelR.C6_Ratio, audion8ViewModelR.C7_Ratio, audion8ViewModelR.C8_Ratio);

                    adjustTabUserControlR.powerOnUserControl.Audion8PowerOnGetValues(audion8ViewModelR.POD, audion8ViewModelR.POL);

                    adjustTabUserControlR.toneUserControl.Audion8ToneGetValues(audion8ViewModelR.Tone_Frequency, audion8ViewModelR.Tone_Level, audion8ViewModelR.Warning_Prompt_Mode, audion8ViewModelR.Program_Prompt_Mode);

                    break;

                case "Audion6":

                    adjustTabUserControlR.algorithmUserControl.Audion6AlgorithmGetValues(audion6ViewModelR.FBC_Enable, audion6ViewModelR.Noise_Reduction);

                    adjustTabUserControlR.equalyzerUserControl.Audion6EqualyzerGetValues(audion6ViewModelR.BEQ1_gain, audion6ViewModelR.BEQ2_gain, audion6ViewModelR.BEQ3_gain, audion6ViewModelR.BEQ4_gain, audion6ViewModelR.BEQ5_gain, audion6ViewModelR.BEQ6_gain,
                                                                                  audion6ViewModelR.BEQ7_gain, audion6ViewModelR.BEQ8_gain, audion6ViewModelR.BEQ9_gain, audion6ViewModelR.BEQ10_gain, audion6ViewModelR.BEQ11_gain, audion6ViewModelR.BEQ12_gain);

                    adjustTabUserControlR.microphoneUserControl.Audion6MicrophoneGetValues(audion6ViewModelR.input_mux, audion6ViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.Audion6OutputGetValues(audion6ViewModelR.C1_TK, audion6ViewModelR.C2_TK, audion6ViewModelR.C3_TK, audion6ViewModelR.C4_TK, audion6ViewModelR.C5_TK, audion6ViewModelR.C6_TK,
                                                         audion6ViewModelR.C1_MPO, audion6ViewModelR.C2_MPO, audion6ViewModelR.C3_MPO, audion6ViewModelR.C4_MPO, audion6ViewModelR.C5_MPO, audion6ViewModelR.C6_MPO,
                                                         audion6ViewModelR.C1_Ratio, audion6ViewModelR.C2_Ratio, audion6ViewModelR.C3_Ratio, audion6ViewModelR.C4_Ratio, audion6ViewModelR.C5_Ratio, audion6ViewModelR.C6_Ratio);

                    adjustTabUserControlR.powerOnUserControl.Audion6PowerOnGetValues(audion6ViewModelR.Power_On_Delay, audion6ViewModelR.Power_On_Level);

                    adjustTabUserControlR.toneUserControl.Audion6ToneGetValues(audion6ViewModelR.Tone_Frequency, audion6ViewModelR.Tone_Level, audion6ViewModelR.Low_Battery_Warning, audion6ViewModelR.Program_Beep_Enable);

                    break;

                case "Audion4":
                    adjustTabUserControlR.algorithmUserControl.Audion4AlgorithmGetValues(audion4ViewModelR.FBC_Enable, audion4ViewModelR.Noise_Reduction, audion4ViewModelR.Expansion_Enable);

                    adjustTabUserControlR.equalyzerUserControl.Audion4EqualyzerGetValues(audion4ViewModelR.BEQ1_gain, audion4ViewModelR.BEQ2_gain, audion4ViewModelR.BEQ3_gain, audion4ViewModelR.BEQ4_gain, audion4ViewModelR.BEQ5_gain, audion4ViewModelR.BEQ6_gain,
                                                                                  audion4ViewModelR.BEQ7_gain, audion4ViewModelR.BEQ8_gain, audion4ViewModelR.BEQ9_gain, audion4ViewModelR.BEQ10_gain, audion4ViewModelR.BEQ11_gain, audion4ViewModelR.BEQ12_gain);

                    adjustTabUserControlR.filterUserControl.Audion4FilterGetValues(audion4ViewModelR.Low_Cut, audion4ViewModelR.High_Cut);

                    adjustTabUserControlR.microphoneUserControl.Audion4MicrophoneGetValues(audion4ViewModelR.input_mux, audion4ViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.Audion4OutputGetValues(audion4ViewModelR.threshold,
                                                         audion4ViewModelR.MPO_level,
                                                         audion4ViewModelR.C1_Ratio,
                                                         audion4ViewModelR.C2_Ratio,
                                                         audion4ViewModelR.C3_Ratio,
                                                         audion4ViewModelR.C4_Ratio);

                    adjustTabUserControlR.powerOnUserControl.Audion4PowerOnGetValues(audion4ViewModelR.Power_On_Delay, audion4ViewModelR.Power_On_Level);

                    adjustTabUserControlR.toneUserControl.Audion4ToneGetValues(audion4ViewModelR.Tone_Frequency, audion4ViewModelR.Tone_Level, audion4ViewModelR.Low_Batt_Warning, audion4ViewModelR.Switch_Tone);

                    break;

                case "SpinNR":
                    adjustTabUserControlR.algorithmUserControl.SpinNRAlgorithmGetValues(spinNRViewModelR.FBC_Enable, spinNRViewModelR.Noise_Reduction, spinNRViewModelR.Mic_Expansion);

                    adjustTabUserControlR.equalyzerUserControl.SpinNREqualyzerGetValues(spinNRViewModelR.BEQ1_gain, spinNRViewModelR.BEQ2_gain, spinNRViewModelR.BEQ3_gain, spinNRViewModelR.BEQ4_gain, spinNRViewModelR.BEQ5_gain, spinNRViewModelR.BEQ6_gain,
                                                                                  spinNRViewModelR.BEQ7_gain, spinNRViewModelR.BEQ8_gain, spinNRViewModelR.BEQ9_gain, spinNRViewModelR.BEQ10_gain, spinNRViewModelR.BEQ11_gain, spinNRViewModelR.BEQ12_gain);

                    adjustTabUserControlR.filterUserControl.SpinNRFilterGetValues(spinNRViewModelR.Low_Cut, spinNRViewModelR.High_Cut);

                    adjustTabUserControlR.microphoneUserControl.SpinNRMicrophoneGetValues(spinNRViewModelR.input_mux, spinNRViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.SpinNROutputGetValues(spinNRViewModelR.threshold,
                                                        spinNRViewModelR.MPO_level,
                                                        spinNRViewModelR.CRL, spinNRViewModelR.CRH);

                    adjustTabUserControlR.toneUserControl.SpinNRToneGetValues(spinNRViewModelR.Tone_Frequency, spinNRViewModelR.Tone_Level, spinNRViewModelR.Low_Batt_Warning, spinNRViewModelR.Switch_Tone);

                    break;
            }
        }

        public void GetValuesL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":

                    adjustTabUserControlL.algorithmUserControl.Audion16AlgorithmGetValues(audion16ViewModelL.feedback_canceller, audion16ViewModelL.ADir_Sensitivity, audion16ViewModelL.noise_reduction, audion16ViewModelL.Noise_Level, audion16ViewModelL.wind_suppression);

                    adjustTabUserControlL.equalyzerUserControl.Audion16EqualyzerGetValues(audion16ViewModelL.beq_gain_1, audion16ViewModelL.beq_gain_2, audion16ViewModelL.beq_gain_3, audion16ViewModelL.beq_gain_4, audion16ViewModelL.beq_gain_5, audion16ViewModelL.beq_gain_6,
                                                                                  audion16ViewModelL.beq_gain_7, audion16ViewModelL.beq_gain_8, audion16ViewModelL.beq_gain_9, audion16ViewModelL.beq_gain_10, audion16ViewModelL.beq_gain_11, audion16ViewModelL.beq_gain_12,
                                                                                  audion16ViewModelL.beq_gain_13, audion16ViewModelL.beq_gain_14, audion16ViewModelL.beq_gain_15, audion16ViewModelL.beq_gain_16);

                    adjustTabUserControlL.microphoneUserControl.Audion16MicrophoneGetValues(audion16ViewModelL.input_mux, audion16ViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.Audion16OutputGetValues(audion16ViewModelL.comp_threshold_1, audion16ViewModelL.comp_threshold_2, audion16ViewModelL.comp_threshold_3, audion16ViewModelL.comp_threshold_4,
                                                         audion16ViewModelL.comp_threshold_5, audion16ViewModelL.comp_threshold_6, audion16ViewModelL.comp_threshold_7, audion16ViewModelL.comp_threshold_8,
                                                         audion16ViewModelL.comp_threshold_9, audion16ViewModelL.comp_threshold_10, audion16ViewModelL.comp_threshold_11, audion16ViewModelL.comp_threshold_12,
                                                         audion16ViewModelL.comp_threshold_13, audion16ViewModelL.comp_threshold_14, audion16ViewModelL.comp_threshold_15, audion16ViewModelL.comp_threshold_16,
                                                         audion16ViewModelL.mpo_threshold_1, audion16ViewModelL.mpo_threshold_2, audion16ViewModelL.mpo_threshold_3, audion16ViewModelL.mpo_threshold_4,
                                                         audion16ViewModelL.mpo_threshold_5, audion16ViewModelL.mpo_threshold_6, audion16ViewModelL.mpo_threshold_7, audion16ViewModelL.mpo_threshold_8,
                                                         audion16ViewModelL.mpo_threshold_9, audion16ViewModelL.mpo_threshold_10, audion16ViewModelL.mpo_threshold_11, audion16ViewModelL.mpo_threshold_12,
                                                         audion16ViewModelL.mpo_threshold_13, audion16ViewModelL.mpo_threshold_14, audion16ViewModelL.mpo_threshold_15, audion16ViewModelL.mpo_threshold_16,
                                                         audion16ViewModelL.comp_ratio_1, audion16ViewModelL.comp_ratio_2, audion16ViewModelL.comp_ratio_3, audion16ViewModelL.comp_ratio_4,
                                                         audion16ViewModelL.comp_ratio_5, audion16ViewModelL.comp_ratio_6, audion16ViewModelL.comp_ratio_7, audion16ViewModelL.comp_ratio_8,
                                                         audion16ViewModelL.comp_ratio_9, audion16ViewModelL.comp_ratio_10, audion16ViewModelL.comp_ratio_11, audion16ViewModelL.comp_ratio_12,
                                                         audion16ViewModelL.comp_ratio_13, audion16ViewModelL.comp_ratio_14, audion16ViewModelL.comp_ratio_15, audion16ViewModelL.comp_ratio_16);

                    adjustTabUserControlL.powerOnUserControl.Audion16PowerOnGetValues(audion16ViewModelL.Power_On_Delay, audion16ViewModelL.Power_On_Level);

                    adjustTabUserControlL.toneUserControl.Audion16ToneGetValues(audion16ViewModelL.Tone_Frequency, audion16ViewModelL.Prompt_Level, audion16ViewModelL.Warning_Prompt_Mode, audion16ViewModelL.Program_Prompt_Mode);

                    break;

                case "Audion8":

                    adjustTabUserControlL.algorithmUserControl.Audion8AlgorithmGetValues(audion8ViewModelL.FBC_Enable, audion8ViewModelL.AD_Sens, audion8ViewModelL.Noise_Reduction, audion8ViewModelL.Noise_Level);

                    adjustTabUserControlL.equalyzerUserControl.Audion8EqualyzerGetValues(audion8ViewModelL.BEQ1_gain, audion8ViewModelL.BEQ2_gain, audion8ViewModelL.BEQ3_gain, audion8ViewModelL.BEQ4_gain, audion8ViewModelL.BEQ5_gain, audion8ViewModelL.BEQ6_gain,
                                                                                  audion8ViewModelL.BEQ7_gain, audion8ViewModelL.BEQ8_gain, audion8ViewModelL.BEQ9_gain, audion8ViewModelL.BEQ10_gain, audion8ViewModelL.BEQ11_gain, audion8ViewModelL.BEQ12_gain);

                    adjustTabUserControlL.microphoneUserControl.Audion8MicrophoneGetValues(audion8ViewModelL.input_mux, audion8ViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.Audion8OutputGetValues(audion8ViewModelL.C1_TK, audion8ViewModelL.C2_TK, audion8ViewModelL.C3_TK, audion8ViewModelL.C4_TK, audion8ViewModelL.C5_TK, audion8ViewModelL.C6_TK, audion8ViewModelL.C7_TK, audion8ViewModelL.C8_TK,
                                                         audion8ViewModelL.C1_MPO, audion8ViewModelL.C2_MPO, audion8ViewModelL.C3_MPO, audion8ViewModelL.C4_MPO, audion8ViewModelL.C5_MPO, audion8ViewModelL.C6_MPO, audion8ViewModelL.C7_MPO, audion8ViewModelL.C8_MPO,
                                                         audion8ViewModelL.C1_Ratio, audion8ViewModelL.C2_Ratio, audion8ViewModelL.C3_Ratio, audion8ViewModelL.C4_Ratio, audion8ViewModelL.C5_Ratio, audion8ViewModelL.C6_Ratio, audion8ViewModelL.C7_Ratio, audion8ViewModelL.C8_Ratio);

                    adjustTabUserControlL.powerOnUserControl.Audion8PowerOnGetValues(audion8ViewModelL.POD, audion8ViewModelL.POL);

                    adjustTabUserControlL.toneUserControl.Audion8ToneGetValues(audion8ViewModelL.Tone_Frequency, audion8ViewModelL.Tone_Level, audion8ViewModelL.Warning_Prompt_Mode, audion8ViewModelL.Program_Prompt_Mode);

                    break;

                case "Audion6":

                    adjustTabUserControlL.algorithmUserControl.Audion6AlgorithmGetValues(audion6ViewModelL.FBC_Enable, audion6ViewModelL.Noise_Reduction);

                    adjustTabUserControlL.equalyzerUserControl.Audion6EqualyzerGetValues(audion6ViewModelL.BEQ1_gain, audion6ViewModelL.BEQ2_gain, audion6ViewModelL.BEQ3_gain, audion6ViewModelL.BEQ4_gain, audion6ViewModelL.BEQ5_gain, audion6ViewModelL.BEQ6_gain,
                                                                                  audion6ViewModelL.BEQ7_gain, audion6ViewModelL.BEQ8_gain, audion6ViewModelL.BEQ9_gain, audion6ViewModelL.BEQ10_gain, audion6ViewModelL.BEQ11_gain, audion6ViewModelL.BEQ12_gain);

                    adjustTabUserControlL.microphoneUserControl.Audion6MicrophoneGetValues(audion6ViewModelL.input_mux, audion6ViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.Audion6OutputGetValues(audion6ViewModelL.C1_TK, audion6ViewModelL.C2_TK, audion6ViewModelL.C3_TK, audion6ViewModelL.C4_TK, audion6ViewModelL.C5_TK, audion6ViewModelL.C6_TK,
                                                         audion6ViewModelL.C1_MPO, audion6ViewModelL.C2_MPO, audion6ViewModelL.C3_MPO, audion6ViewModelL.C4_MPO, audion6ViewModelL.C5_MPO, audion6ViewModelL.C6_MPO,
                                                         audion6ViewModelL.C1_Ratio, audion6ViewModelL.C2_Ratio, audion6ViewModelL.C3_Ratio, audion6ViewModelL.C4_Ratio, audion6ViewModelL.C5_Ratio, audion6ViewModelL.C6_Ratio);

                    adjustTabUserControlL.powerOnUserControl.Audion6PowerOnGetValues(audion6ViewModelL.Power_On_Delay, audion6ViewModelL.Power_On_Level);

                    adjustTabUserControlL.toneUserControl.Audion6ToneGetValues(audion6ViewModelL.Tone_Frequency, audion6ViewModelL.Tone_Level, audion6ViewModelL.Low_Battery_Warning, audion6ViewModelL.Program_Beep_Enable);

                    break;

                case "Audion4":
                    adjustTabUserControlL.algorithmUserControl.Audion4AlgorithmGetValues(audion4ViewModelL.FBC_Enable, audion4ViewModelL.Noise_Reduction, audion4ViewModelL.Expansion_Enable);

                    adjustTabUserControlL.equalyzerUserControl.Audion4EqualyzerGetValues(audion4ViewModelL.BEQ1_gain, audion4ViewModelL.BEQ2_gain, audion4ViewModelL.BEQ3_gain, audion4ViewModelL.BEQ4_gain, audion4ViewModelL.BEQ5_gain, audion4ViewModelL.BEQ6_gain,
                                                                                  audion4ViewModelL.BEQ7_gain, audion4ViewModelL.BEQ8_gain, audion4ViewModelL.BEQ9_gain, audion4ViewModelL.BEQ10_gain, audion4ViewModelL.BEQ11_gain, audion4ViewModelL.BEQ12_gain);

                    adjustTabUserControlL.filterUserControl.Audion4FilterGetValues(audion4ViewModelL.Low_Cut, audion4ViewModelL.High_Cut);

                    adjustTabUserControlL.microphoneUserControl.Audion4MicrophoneGetValues(audion4ViewModelL.input_mux, audion4ViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.Audion4OutputGetValues(audion4ViewModelL.threshold,
                                                         audion4ViewModelL.MPO_level,
                                                         audion4ViewModelL.C1_Ratio,
                                                         audion4ViewModelL.C2_Ratio,
                                                         audion4ViewModelL.C3_Ratio,
                                                         audion4ViewModelL.C4_Ratio);

                    adjustTabUserControlL.powerOnUserControl.Audion4PowerOnGetValues(audion4ViewModelL.Power_On_Delay, audion4ViewModelL.Power_On_Level);

                    adjustTabUserControlL.toneUserControl.Audion4ToneGetValues(audion4ViewModelL.Tone_Frequency, audion4ViewModelL.Tone_Level, audion4ViewModelL.Low_Batt_Warning, audion4ViewModelL.Switch_Tone);

                    break;

                case "SpinNR":
                    adjustTabUserControlL.algorithmUserControl.SpinNRAlgorithmGetValues(spinNRViewModelL.FBC_Enable, spinNRViewModelL.Noise_Reduction, spinNRViewModelL.Mic_Expansion);

                    adjustTabUserControlL.equalyzerUserControl.SpinNREqualyzerGetValues(spinNRViewModelL.BEQ1_gain, spinNRViewModelL.BEQ2_gain, spinNRViewModelL.BEQ3_gain, spinNRViewModelL.BEQ4_gain, spinNRViewModelL.BEQ5_gain, spinNRViewModelL.BEQ6_gain,
                                                                                  spinNRViewModelL.BEQ7_gain, spinNRViewModelL.BEQ8_gain, spinNRViewModelL.BEQ9_gain, spinNRViewModelL.BEQ10_gain, spinNRViewModelL.BEQ11_gain, spinNRViewModelL.BEQ12_gain);

                    adjustTabUserControlL.filterUserControl.SpinNRFilterGetValues(spinNRViewModelL.Low_Cut, spinNRViewModelL.High_Cut);

                    adjustTabUserControlL.microphoneUserControl.SpinNRMicrophoneGetValues(spinNRViewModelL.input_mux, spinNRViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.SpinNROutputGetValues(spinNRViewModelL.threshold,
                                                        spinNRViewModelL.MPO_level,
                                                        spinNRViewModelL.CRL, spinNRViewModelL.CRH);

                    adjustTabUserControlL.toneUserControl.SpinNRToneGetValues(spinNRViewModelL.Tone_Frequency, spinNRViewModelL.Tone_Level, spinNRViewModelL.Low_Batt_Warning, spinNRViewModelL.Switch_Tone);

                    break;
            }
        }

        public void SetValuesR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":

                    adjustTabUserControlR.algorithmUserControl.Audion16AlgorithmSetValues(ref audion16ViewModelR.feedback_canceller, ref audion16ViewModelR.ADir_Sensitivity, ref audion16ViewModelR.noise_reduction, ref audion16ViewModelR.Noise_Level, ref audion16ViewModelR.wind_suppression);

                    adjustTabUserControlR.equalyzerUserControl.Audion16EqualyzerSetValues(ref audion16ViewModelR.beq_gain_1, ref audion16ViewModelR.beq_gain_2, ref audion16ViewModelR.beq_gain_3, ref audion16ViewModelR.beq_gain_4, ref audion16ViewModelR.beq_gain_5, ref audion16ViewModelR.beq_gain_6,
                                                                                  ref audion16ViewModelR.beq_gain_7, ref audion16ViewModelR.beq_gain_8, ref audion16ViewModelR.beq_gain_9, ref audion16ViewModelR.beq_gain_10, ref audion16ViewModelR.beq_gain_11, ref audion16ViewModelR.beq_gain_12,
                                                                                  ref audion16ViewModelR.beq_gain_13, ref audion16ViewModelR.beq_gain_14, ref audion16ViewModelR.beq_gain_15, ref audion16ViewModelR.beq_gain_16);

                    adjustTabUserControlR.microphoneUserControl.Audion16MicrophoneSetValues(ref audion16ViewModelR.input_mux, ref audion16ViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.Audion16OutputSetValues(ref audion16ViewModelR.comp_threshold_1, ref audion16ViewModelR.comp_threshold_2, ref audion16ViewModelR.comp_threshold_3, ref audion16ViewModelR.comp_threshold_4,
                                                         ref audion16ViewModelR.comp_threshold_5, ref audion16ViewModelR.comp_threshold_6, ref audion16ViewModelR.comp_threshold_7, ref audion16ViewModelR.comp_threshold_8,
                                                         ref audion16ViewModelR.comp_threshold_9, ref audion16ViewModelR.comp_threshold_10, ref audion16ViewModelR.comp_threshold_11, ref audion16ViewModelR.comp_threshold_12,
                                                         ref audion16ViewModelR.comp_threshold_13, ref audion16ViewModelR.comp_threshold_14, ref audion16ViewModelR.comp_threshold_15, ref audion16ViewModelR.comp_threshold_16,
                                                         ref audion16ViewModelR.mpo_threshold_1, ref audion16ViewModelR.mpo_threshold_2, ref audion16ViewModelR.mpo_threshold_3, ref audion16ViewModelR.mpo_threshold_4,
                                                         ref audion16ViewModelR.mpo_threshold_5, ref audion16ViewModelR.mpo_threshold_6, ref audion16ViewModelR.mpo_threshold_7, ref audion16ViewModelR.mpo_threshold_8,
                                                         ref audion16ViewModelR.mpo_threshold_9, ref audion16ViewModelR.mpo_threshold_10, ref audion16ViewModelR.mpo_threshold_11, ref audion16ViewModelR.mpo_threshold_12,
                                                         ref audion16ViewModelR.mpo_threshold_13, ref audion16ViewModelR.mpo_threshold_14, ref audion16ViewModelR.mpo_threshold_15, ref audion16ViewModelR.mpo_threshold_16,
                                                         ref audion16ViewModelR.comp_ratio_1, ref audion16ViewModelR.comp_ratio_2, ref audion16ViewModelR.comp_ratio_3, ref audion16ViewModelR.comp_ratio_4,
                                                         ref audion16ViewModelR.comp_ratio_5, ref audion16ViewModelR.comp_ratio_6, ref audion16ViewModelR.comp_ratio_7, ref audion16ViewModelR.comp_ratio_8,
                                                         ref audion16ViewModelR.comp_ratio_9, ref audion16ViewModelR.comp_ratio_10, ref audion16ViewModelR.comp_ratio_11, ref audion16ViewModelR.comp_ratio_12,
                                                         ref audion16ViewModelR.comp_ratio_13, ref audion16ViewModelR.comp_ratio_14, ref audion16ViewModelR.comp_ratio_15, ref audion16ViewModelR.comp_ratio_16);

                    adjustTabUserControlR.powerOnUserControl.Audion16PowerOnSetValues(ref audion16ViewModelR.Power_On_Delay, ref audion16ViewModelR.Power_On_Level);

                    adjustTabUserControlR.toneUserControl.Audion16ToneSetValues(ref audion16ViewModelR.Tone_Frequency, ref audion16ViewModelR.Prompt_Level, ref audion16ViewModelR.Warning_Prompt_Mode, ref audion16ViewModelR.Program_Prompt_Mode);

                    break;

                case "Audion8":

                    adjustTabUserControlR.algorithmUserControl.Audion8AlgorithmSetValues(ref audion8ViewModelR.FBC_Enable, ref audion8ViewModelR.AD_Sens, ref audion8ViewModelR.Noise_Reduction, ref audion8ViewModelR.Noise_Level);

                    adjustTabUserControlR.equalyzerUserControl.Audion8EqualyzerSetValues(ref audion8ViewModelR.BEQ1_gain, ref audion8ViewModelR.BEQ2_gain, ref audion8ViewModelR.BEQ3_gain, ref audion8ViewModelR.BEQ4_gain, ref audion8ViewModelR.BEQ5_gain, ref audion8ViewModelR.BEQ6_gain,
                                                              ref audion8ViewModelR.BEQ7_gain, ref audion8ViewModelR.BEQ8_gain, ref audion8ViewModelR.BEQ9_gain, ref audion8ViewModelR.BEQ10_gain, ref audion8ViewModelR.BEQ11_gain, ref audion8ViewModelR.BEQ12_gain);

                    adjustTabUserControlR.microphoneUserControl.Audion8MicrophoneSetValues(ref audion8ViewModelR.input_mux, ref audion8ViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.Audion8OutputSetValues(ref audion8ViewModelR.C1_TK, ref audion8ViewModelR.C2_TK, ref audion8ViewModelR.C3_TK, ref audion8ViewModelR.C4_TK, ref audion8ViewModelR.C5_TK, ref audion8ViewModelR.C6_TK, ref audion8ViewModelR.C7_TK, ref audion8ViewModelR.C8_TK,
                                                        ref audion8ViewModelR.C1_MPO, ref audion8ViewModelR.C2_MPO, ref audion8ViewModelR.C3_MPO, ref audion8ViewModelR.C4_MPO, ref audion8ViewModelR.C5_MPO, ref audion8ViewModelR.C6_MPO, ref audion8ViewModelR.C7_MPO, ref audion8ViewModelR.C8_MPO,
                                                        ref audion8ViewModelR.C1_Ratio, ref audion8ViewModelR.C2_Ratio, ref audion8ViewModelR.C3_Ratio, ref audion8ViewModelR.C4_Ratio, ref audion8ViewModelR.C5_Ratio, ref audion8ViewModelR.C6_Ratio, ref audion8ViewModelR.C7_Ratio, ref audion8ViewModelR.C8_Ratio);

                    adjustTabUserControlR.powerOnUserControl.Audion8PowerOnSetValues(ref audion8ViewModelR.POD, ref audion8ViewModelR.POL);

                    adjustTabUserControlR.toneUserControl.Audion8ToneSetValues(ref audion8ViewModelR.Tone_Frequency, ref audion8ViewModelR.Tone_Level, ref audion8ViewModelR.Warning_Prompt_Mode, ref audion8ViewModelR.Program_Prompt_Mode);

                    break;

                case "Audion6":

                    adjustTabUserControlR.algorithmUserControl.Audion6AlgorithmSetValues(ref audion6ViewModelR.FBC_Enable, ref audion6ViewModelR.Noise_Reduction);

                    adjustTabUserControlR.equalyzerUserControl.Audion6EqualyzerSetValues(ref audion6ViewModelR.BEQ1_gain, ref audion6ViewModelR.BEQ2_gain, ref audion6ViewModelR.BEQ3_gain, ref audion6ViewModelR.BEQ4_gain, ref audion6ViewModelR.BEQ5_gain, ref audion6ViewModelR.BEQ6_gain,
                                                                                  ref audion6ViewModelR.BEQ7_gain, ref audion6ViewModelR.BEQ8_gain, ref audion6ViewModelR.BEQ9_gain, ref audion6ViewModelR.BEQ10_gain, ref audion6ViewModelR.BEQ11_gain, ref audion6ViewModelR.BEQ12_gain);

                    adjustTabUserControlR.microphoneUserControl.Audion6MicrophoneSetValues(ref audion6ViewModelR.input_mux, ref audion6ViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.Audion6OutputSetValues(ref audion6ViewModelR.C1_TK, ref audion6ViewModelR.C2_TK, ref audion6ViewModelR.C3_TK, ref audion6ViewModelR.C4_TK, ref audion6ViewModelR.C5_TK, ref audion6ViewModelR.C6_TK,
                                                         ref audion6ViewModelR.C1_MPO, ref audion6ViewModelR.C2_MPO, ref audion6ViewModelR.C3_MPO, ref audion6ViewModelR.C4_MPO, ref audion6ViewModelR.C5_MPO, ref audion6ViewModelR.C6_MPO,
                                                         ref audion6ViewModelR.C1_Ratio, ref audion6ViewModelR.C2_Ratio, ref audion6ViewModelR.C3_Ratio, ref audion6ViewModelR.C4_Ratio, ref audion6ViewModelR.C5_Ratio, ref audion6ViewModelR.C6_Ratio);

                    adjustTabUserControlR.powerOnUserControl.Audion6PowerOnSetValues(ref audion6ViewModelR.Power_On_Delay, ref audion6ViewModelR.Power_On_Level);

                    adjustTabUserControlR.toneUserControl.Audion6ToneSetValues(ref audion6ViewModelR.Tone_Frequency, ref audion6ViewModelR.Tone_Level, ref audion6ViewModelR.Low_Battery_Warning, ref audion6ViewModelR.Program_Beep_Enable);

                    break;

                case "Audion4":

                    adjustTabUserControlR.algorithmUserControl.Audion4AlgorithmSetValues(ref audion4ViewModelR.FBC_Enable, ref audion4ViewModelR.Noise_Reduction, ref audion4ViewModelR.Expansion_Enable);

                    adjustTabUserControlR.equalyzerUserControl.Audion4EqualyzerSetValues(ref audion4ViewModelR.BEQ1_gain, ref audion4ViewModelR.BEQ2_gain, ref audion4ViewModelR.BEQ3_gain, ref audion4ViewModelR.BEQ4_gain, ref audion4ViewModelR.BEQ5_gain, ref audion4ViewModelR.BEQ6_gain,
                                                                                  ref audion4ViewModelR.BEQ7_gain, ref audion4ViewModelR.BEQ8_gain, ref audion4ViewModelR.BEQ9_gain, ref audion4ViewModelR.BEQ10_gain, ref audion4ViewModelR.BEQ11_gain, ref audion4ViewModelR.BEQ12_gain);

                    adjustTabUserControlR.filterUserControl.Audion4FilterSetValues(ref audion4ViewModelR.Low_Cut, ref audion4ViewModelR.High_Cut);

                    adjustTabUserControlR.microphoneUserControl.Audion4MicrophoneSetValues(ref audion4ViewModelR.input_mux, ref audion4ViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.Audion4OutputSetValues(ref audion4ViewModelR.threshold,
                                                                                  ref audion4ViewModelR.MPO_level,
                                                                                  ref audion4ViewModelR.C1_Ratio,
                                                                                  ref audion4ViewModelR.C2_Ratio,
                                                                                  ref audion4ViewModelR.C3_Ratio,
                                                                                  ref audion4ViewModelR.C4_Ratio);

                    adjustTabUserControlR.powerOnUserControl.Audion4PowerOnSetValues(ref audion4ViewModelR.Power_On_Delay, ref audion4ViewModelR.Power_On_Level);

                    adjustTabUserControlR.toneUserControl.Audion4ToneSetValues(ref audion4ViewModelR.Tone_Frequency, ref audion4ViewModelR.Tone_Level, ref audion4ViewModelR.Low_Batt_Warning, ref audion4ViewModelR.Switch_Tone);

                    break;

                case "SpinNR":

                    adjustTabUserControlR.algorithmUserControl.SpinNRAlgorithmSetValues(ref spinNRViewModelR.FBC_Enable, ref spinNRViewModelR.Noise_Reduction, ref spinNRViewModelR.Mic_Expansion);

                    adjustTabUserControlR.equalyzerUserControl.SpinNREqualyzerSetValues(ref spinNRViewModelR.BEQ1_gain, ref spinNRViewModelR.BEQ2_gain, ref spinNRViewModelR.BEQ3_gain, ref spinNRViewModelR.BEQ4_gain, ref spinNRViewModelR.BEQ5_gain, ref spinNRViewModelR.BEQ6_gain,
                                                              ref spinNRViewModelR.BEQ7_gain, ref spinNRViewModelR.BEQ8_gain, ref spinNRViewModelR.BEQ9_gain, ref spinNRViewModelR.BEQ10_gain, ref spinNRViewModelR.BEQ11_gain, ref spinNRViewModelR.BEQ12_gain);

                    adjustTabUserControlR.filterUserControl.SpinNRFilterSetValues(ref spinNRViewModelR.Low_Cut, ref spinNRViewModelR.High_Cut);

                    adjustTabUserControlR.microphoneUserControl.SpinNRMicrophoneSetValues(ref spinNRViewModelR.input_mux, ref spinNRViewModelR.matrix_gain);

                    adjustTabUserControlR.outputUserControl.SpinNROutputSetValues(ref spinNRViewModelR.threshold,
                                                        ref spinNRViewModelR.MPO_level,
                                                        ref spinNRViewModelR.CRL, ref spinNRViewModelR.CRH);

                    adjustTabUserControlR.toneUserControl.SpinNRToneSetValues(ref spinNRViewModelR.Tone_Frequency, ref spinNRViewModelR.Tone_Level, ref spinNRViewModelR.Low_Batt_Warning, ref spinNRViewModelR.Switch_Tone);

                    break;
            }
        }

        public void SetValuesL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":

                    adjustTabUserControlL.algorithmUserControl.Audion16AlgorithmSetValues(ref audion16ViewModelL.feedback_canceller, ref audion16ViewModelL.ADir_Sensitivity, ref audion16ViewModelL.noise_reduction, ref audion16ViewModelL.Noise_Level, ref audion16ViewModelL.wind_suppression);

                    adjustTabUserControlL.equalyzerUserControl.Audion16EqualyzerSetValues(ref audion16ViewModelL.beq_gain_1, ref audion16ViewModelL.beq_gain_2, ref audion16ViewModelL.beq_gain_3, ref audion16ViewModelL.beq_gain_4, ref audion16ViewModelL.beq_gain_5, ref audion16ViewModelL.beq_gain_6,
                                                                                  ref audion16ViewModelL.beq_gain_7, ref audion16ViewModelL.beq_gain_8, ref audion16ViewModelL.beq_gain_9, ref audion16ViewModelL.beq_gain_10, ref audion16ViewModelL.beq_gain_11, ref audion16ViewModelL.beq_gain_12,
                                                                                  ref audion16ViewModelL.beq_gain_13, ref audion16ViewModelL.beq_gain_14, ref audion16ViewModelL.beq_gain_15, ref audion16ViewModelL.beq_gain_16);

                    adjustTabUserControlL.microphoneUserControl.Audion16MicrophoneSetValues(ref audion16ViewModelL.input_mux, ref audion16ViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.Audion16OutputSetValues(ref audion16ViewModelL.comp_threshold_1, ref audion16ViewModelL.comp_threshold_2, ref audion16ViewModelL.comp_threshold_3, ref audion16ViewModelL.comp_threshold_4,
                                                         ref audion16ViewModelL.comp_threshold_5, ref audion16ViewModelL.comp_threshold_6, ref audion16ViewModelL.comp_threshold_7, ref audion16ViewModelL.comp_threshold_8,
                                                         ref audion16ViewModelL.comp_threshold_9, ref audion16ViewModelL.comp_threshold_10, ref audion16ViewModelL.comp_threshold_11, ref audion16ViewModelL.comp_threshold_12,
                                                         ref audion16ViewModelL.comp_threshold_13, ref audion16ViewModelL.comp_threshold_14, ref audion16ViewModelL.comp_threshold_15, ref audion16ViewModelL.comp_threshold_16,
                                                         ref audion16ViewModelL.mpo_threshold_1, ref audion16ViewModelL.mpo_threshold_2, ref audion16ViewModelL.mpo_threshold_3, ref audion16ViewModelL.mpo_threshold_4,
                                                         ref audion16ViewModelL.mpo_threshold_5, ref audion16ViewModelL.mpo_threshold_6, ref audion16ViewModelL.mpo_threshold_7, ref audion16ViewModelL.mpo_threshold_8,
                                                         ref audion16ViewModelL.mpo_threshold_9, ref audion16ViewModelL.mpo_threshold_10, ref audion16ViewModelL.mpo_threshold_11, ref audion16ViewModelL.mpo_threshold_12,
                                                         ref audion16ViewModelL.mpo_threshold_13, ref audion16ViewModelL.mpo_threshold_14, ref audion16ViewModelL.mpo_threshold_15, ref audion16ViewModelL.mpo_threshold_16,
                                                         ref audion16ViewModelL.comp_ratio_1, ref audion16ViewModelL.comp_ratio_2, ref audion16ViewModelL.comp_ratio_3, ref audion16ViewModelL.comp_ratio_4,
                                                         ref audion16ViewModelL.comp_ratio_5, ref audion16ViewModelL.comp_ratio_6, ref audion16ViewModelL.comp_ratio_7, ref audion16ViewModelL.comp_ratio_8,
                                                         ref audion16ViewModelL.comp_ratio_9, ref audion16ViewModelL.comp_ratio_10, ref audion16ViewModelL.comp_ratio_11, ref audion16ViewModelL.comp_ratio_12,
                                                         ref audion16ViewModelL.comp_ratio_13, ref audion16ViewModelL.comp_ratio_14, ref audion16ViewModelL.comp_ratio_15, ref audion16ViewModelL.comp_ratio_16);

                    adjustTabUserControlL.powerOnUserControl.Audion16PowerOnSetValues(ref audion16ViewModelL.Power_On_Delay, ref audion16ViewModelL.Power_On_Level);

                    adjustTabUserControlL.toneUserControl.Audion16ToneSetValues(ref audion16ViewModelL.Tone_Frequency, ref audion16ViewModelL.Prompt_Level, ref audion16ViewModelL.Warning_Prompt_Mode, ref audion16ViewModelL.Program_Prompt_Mode);

                    break;

                case "Audion8":

                    adjustTabUserControlL.algorithmUserControl.Audion8AlgorithmSetValues(ref audion8ViewModelL.FBC_Enable, ref audion8ViewModelL.AD_Sens, ref audion8ViewModelL.Noise_Reduction, ref audion8ViewModelL.Noise_Level);

                    adjustTabUserControlL.equalyzerUserControl.Audion8EqualyzerSetValues(ref audion8ViewModelL.BEQ1_gain, ref audion8ViewModelL.BEQ2_gain, ref audion8ViewModelL.BEQ3_gain, ref audion8ViewModelL.BEQ4_gain, ref audion8ViewModelL.BEQ5_gain, ref audion8ViewModelL.BEQ6_gain,
                                                              ref audion8ViewModelL.BEQ7_gain, ref audion8ViewModelL.BEQ8_gain, ref audion8ViewModelL.BEQ9_gain, ref audion8ViewModelL.BEQ10_gain, ref audion8ViewModelL.BEQ11_gain, ref audion8ViewModelL.BEQ12_gain);

                    adjustTabUserControlL.microphoneUserControl.Audion8MicrophoneSetValues(ref audion8ViewModelL.input_mux, ref audion8ViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.Audion8OutputSetValues(ref audion8ViewModelL.C1_TK, ref audion8ViewModelL.C2_TK, ref audion8ViewModelL.C3_TK, ref audion8ViewModelL.C4_TK, ref audion8ViewModelL.C5_TK, ref audion8ViewModelL.C6_TK, ref audion8ViewModelL.C7_TK, ref audion8ViewModelL.C8_TK,
                                                        ref audion8ViewModelL.C1_MPO, ref audion8ViewModelL.C2_MPO, ref audion8ViewModelL.C3_MPO, ref audion8ViewModelL.C4_MPO, ref audion8ViewModelL.C5_MPO, ref audion8ViewModelL.C6_MPO, ref audion8ViewModelL.C7_MPO, ref audion8ViewModelL.C8_MPO,
                                                        ref audion8ViewModelL.C1_Ratio, ref audion8ViewModelL.C2_Ratio, ref audion8ViewModelL.C3_Ratio, ref audion8ViewModelL.C4_Ratio, ref audion8ViewModelL.C5_Ratio, ref audion8ViewModelL.C6_Ratio, ref audion8ViewModelL.C7_Ratio, ref audion8ViewModelL.C8_Ratio);

                    adjustTabUserControlL.powerOnUserControl.Audion8PowerOnSetValues(ref audion8ViewModelL.POD, ref audion8ViewModelL.POL);

                    adjustTabUserControlL.toneUserControl.Audion8ToneSetValues(ref audion8ViewModelL.Tone_Frequency, ref audion8ViewModelL.Tone_Level, ref audion8ViewModelL.Warning_Prompt_Mode, ref audion8ViewModelL.Program_Prompt_Mode);

                    break;

                case "Audion6":

                    adjustTabUserControlL.algorithmUserControl.Audion6AlgorithmSetValues(ref audion6ViewModelL.FBC_Enable, ref audion6ViewModelL.Noise_Reduction);

                    adjustTabUserControlL.equalyzerUserControl.Audion6EqualyzerSetValues(ref audion6ViewModelL.BEQ1_gain, ref audion6ViewModelL.BEQ2_gain, ref audion6ViewModelL.BEQ3_gain, ref audion6ViewModelL.BEQ4_gain, ref audion6ViewModelL.BEQ5_gain, ref audion6ViewModelL.BEQ6_gain,
                                                                                  ref audion6ViewModelL.BEQ7_gain, ref audion6ViewModelL.BEQ8_gain, ref audion6ViewModelL.BEQ9_gain, ref audion6ViewModelL.BEQ10_gain, ref audion6ViewModelL.BEQ11_gain, ref audion6ViewModelL.BEQ12_gain);

                    adjustTabUserControlL.microphoneUserControl.Audion6MicrophoneSetValues(ref audion6ViewModelL.input_mux, ref audion6ViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.Audion6OutputSetValues(ref audion6ViewModelL.C1_TK, ref audion6ViewModelL.C2_TK, ref audion6ViewModelL.C3_TK, ref audion6ViewModelL.C4_TK, ref audion6ViewModelL.C5_TK, ref audion6ViewModelL.C6_TK,
                                                         ref audion6ViewModelL.C1_MPO, ref audion6ViewModelL.C2_MPO, ref audion6ViewModelL.C3_MPO, ref audion6ViewModelL.C4_MPO, ref audion6ViewModelL.C5_MPO, ref audion6ViewModelL.C6_MPO,
                                                         ref audion6ViewModelL.C1_Ratio, ref audion6ViewModelL.C2_Ratio, ref audion6ViewModelL.C3_Ratio, ref audion6ViewModelL.C4_Ratio, ref audion6ViewModelL.C5_Ratio, ref audion6ViewModelL.C6_Ratio);

                    adjustTabUserControlL.powerOnUserControl.Audion6PowerOnSetValues(ref audion6ViewModelL.Power_On_Delay, ref audion6ViewModelL.Power_On_Level);

                    adjustTabUserControlL.toneUserControl.Audion6ToneSetValues(ref audion6ViewModelL.Tone_Frequency, ref audion6ViewModelL.Tone_Level, ref audion6ViewModelL.Low_Battery_Warning, ref audion6ViewModelL.Program_Beep_Enable);

                    break;

                case "Audion4":

                    adjustTabUserControlL.algorithmUserControl.Audion4AlgorithmSetValues(ref audion4ViewModelL.FBC_Enable, ref audion4ViewModelL.Noise_Reduction, ref audion4ViewModelL.Expansion_Enable);

                    adjustTabUserControlL.equalyzerUserControl.Audion4EqualyzerSetValues(ref audion4ViewModelL.BEQ1_gain, ref audion4ViewModelL.BEQ2_gain, ref audion4ViewModelL.BEQ3_gain, ref audion4ViewModelL.BEQ4_gain, ref audion4ViewModelL.BEQ5_gain, ref audion4ViewModelL.BEQ6_gain,
                                                                                  ref audion4ViewModelL.BEQ7_gain, ref audion4ViewModelL.BEQ8_gain, ref audion4ViewModelL.BEQ9_gain, ref audion4ViewModelL.BEQ10_gain, ref audion4ViewModelL.BEQ11_gain, ref audion4ViewModelL.BEQ12_gain);

                    adjustTabUserControlL.filterUserControl.Audion4FilterSetValues(ref audion4ViewModelL.Low_Cut, ref audion4ViewModelL.High_Cut);

                    adjustTabUserControlL.microphoneUserControl.Audion4MicrophoneSetValues(ref audion4ViewModelL.input_mux, ref audion4ViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.Audion4OutputSetValues(ref audion4ViewModelL.threshold,
                                                                                  ref audion4ViewModelL.MPO_level,
                                                                                  ref audion4ViewModelL.C1_Ratio,
                                                                                  ref audion4ViewModelL.C2_Ratio,
                                                                                  ref audion4ViewModelL.C3_Ratio,
                                                                                  ref audion4ViewModelL.C4_Ratio);

                    adjustTabUserControlL.powerOnUserControl.Audion4PowerOnSetValues(ref audion4ViewModelL.Power_On_Delay, ref audion4ViewModelL.Power_On_Level);

                    adjustTabUserControlL.toneUserControl.Audion4ToneSetValues(ref audion4ViewModelL.Tone_Frequency, ref audion4ViewModelL.Tone_Level, ref audion4ViewModelL.Low_Batt_Warning, ref audion4ViewModelL.Switch_Tone);

                    break;

                case "SpinNR":

                    adjustTabUserControlL.algorithmUserControl.SpinNRAlgorithmSetValues(ref spinNRViewModelL.FBC_Enable, ref spinNRViewModelL.Noise_Reduction, ref spinNRViewModelL.Mic_Expansion);

                    adjustTabUserControlL.equalyzerUserControl.SpinNREqualyzerSetValues(ref spinNRViewModelL.BEQ1_gain, ref spinNRViewModelL.BEQ2_gain, ref spinNRViewModelL.BEQ3_gain, ref spinNRViewModelL.BEQ4_gain, ref spinNRViewModelL.BEQ5_gain, ref spinNRViewModelL.BEQ6_gain,
                                                              ref spinNRViewModelL.BEQ7_gain, ref spinNRViewModelL.BEQ8_gain, ref spinNRViewModelL.BEQ9_gain, ref spinNRViewModelL.BEQ10_gain, ref spinNRViewModelL.BEQ11_gain, ref spinNRViewModelL.BEQ12_gain);

                    adjustTabUserControlL.filterUserControl.SpinNRFilterSetValues(ref spinNRViewModelL.Low_Cut, ref spinNRViewModelL.High_Cut);

                    adjustTabUserControlL.microphoneUserControl.SpinNRMicrophoneSetValues(ref spinNRViewModelL.input_mux, ref spinNRViewModelL.matrix_gain);

                    adjustTabUserControlL.outputUserControl.SpinNROutputSetValues(ref spinNRViewModelL.threshold,
                                                        ref spinNRViewModelL.MPO_level,
                                                        ref spinNRViewModelL.CRL, ref spinNRViewModelL.CRH);

                    adjustTabUserControlL.toneUserControl.SpinNRToneSetValues(ref spinNRViewModelL.Tone_Frequency, ref spinNRViewModelL.Tone_Level, ref spinNRViewModelL.Low_Batt_Warning, ref spinNRViewModelL.Switch_Tone);

                    break;
            }
        }

        public void DoAutofitR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.Autofit(waveRule);
                    break;

                case "Audion8":
                    audion8ViewModelR.Autofit(waveRule);
                    break;

                case "Audion6":
                    audion6ViewModelR.Autofit(waveRule);
                    break;

                case "Audion4":
                    audion4ViewModelR.Autofit(waveRule);
                    break;

                case "SpinNR":
                    spinNRViewModelR.Autofit(waveRule);
                    break;
            }
        }

        public void DoAutofitL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.Autofit(waveRule);
                    break;

                case "Audion8":
                    audion8ViewModelL.Autofit(waveRule);
                    break;

                case "Audion6":
                    audion6ViewModelL.Autofit(waveRule);
                    break;

                case "Audion4":
                    audion4ViewModelL.Autofit(waveRule);
                    break;

                case "SpinNR":
                    spinNRViewModelL.Autofit(waveRule);
                    break;
            }
        }

        public void BurnSetR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.Burn();
                    break;

                case "Audion8":
                    audion8ViewModelR.Burn();
                    break;

                case "Audion6":
                    audion6ViewModelR.Burn();
                    break;

                case "Audion4":
                    audion4ViewModelR.Burn();
                    break;

                case "SpinNR":
                    spinNRViewModelR.Burn();
                    break;
            }
        }

        public void BurnSetL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.Burn();
                    break;

                case "Audion8":
                    audion8ViewModelL.Burn();
                    break;

                case "Audion6":
                    audion6ViewModelL.Burn();
                    break;

                case "Audion4":
                    audion4ViewModelL.Burn();
                    break;

                case "SpinNR":
                    spinNRViewModelL.Burn();
                    break;
            }
        }

        public void WriteSetR(bool write)
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.Write(write);
                    break;

                case "Audion8":
                    audion8ViewModelR.Write(write);
                    break;

                case "Audion6":
                    audion6ViewModelR.Write(write);
                    break;

                case "Audion4":
                    audion4ViewModelR.Write(write);
                    break;

                case "SpinNR":
                    spinNRViewModelR.Write(write);
                    break;
            }
        }

        public void WriteSetL(bool write)
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.Write(write);
                    break;

                case "Audion8":
                    audion8ViewModelL.Write(write);
                    break;

                case "Audion6":
                    audion6ViewModelL.Write(write);
                    break;

                case "Audion4":
                    audion4ViewModelL.Write(write);
                    break;

                case "SpinNR":
                    spinNRViewModelL.Write(write);
                    break;
            }
        }

        public void CopyProgR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.CopyProgramFromTo((short)ProgramR.SelectedIndex, (short)CopyProgramR.SelectedIndex, 1);
                    break;

                case "Audion8":
                    audion8ViewModelR.CopyProgramFromTo((short)ProgramR.SelectedIndex, (short)CopyProgramR.SelectedIndex, ref audion8ViewModelR.Audion8Params[1]);
                    break;

                case "Audion6":
                    audion6ViewModelR.CopyProgramFromTo((short)ProgramR.SelectedIndex, (short)CopyProgramR.SelectedIndex, ref audion6ViewModelR.Audion6Params[1]);
                    break;

                case "Audion4":
                    audion4ViewModelR.CopyProgramFromTo((short)ProgramR.SelectedIndex, (short)CopyProgramR.SelectedIndex, ref audion4ViewModelR.Audion4Params[1]);
                    break;

                case "SpinNR":
                    spinNRViewModelR.CopyProgramFromTo((short)ProgramR.SelectedIndex, (short)CopyProgramR.SelectedIndex, ref spinNRViewModelR.SpinNRParams[1]);
                    break;
            }
        }

        public void CopyProgL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.CopyProgramFromTo((short)ProgramL.SelectedIndex, (short)CopyProgramL.SelectedIndex, 0);
                    break;

                case "Audion8":
                    audion8ViewModelL.CopyProgramFromTo((short)ProgramL.SelectedIndex, (short)CopyProgramL.SelectedIndex, ref audion8ViewModelL.Audion8Params[0]);
                    break;

                case "Audion6":
                    audion6ViewModelL.CopyProgramFromTo((short)ProgramL.SelectedIndex, (short)CopyProgramL.SelectedIndex, ref audion6ViewModelL.Audion6Params[0]);
                    break;

                case "Audion4":
                    audion4ViewModelL.CopyProgramFromTo((short)ProgramL.SelectedIndex, (short)CopyProgramL.SelectedIndex, ref audion4ViewModelL.Audion4Params[0]);
                    break;

                case "SpinNR":
                    spinNRViewModelL.CopyProgramFromTo((short)ProgramL.SelectedIndex, (short)CopyProgramL.SelectedIndex, ref spinNRViewModelL.SpinNRParams[0]);
                    break;
            }
        }

        public void CopyProgR(int fromProgram, int toProgram)
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.CopyProgramFromTo((short)fromProgram, (short)toProgram, 1);
                    break;

                case "Audion8":
                    audion8ViewModelR.CopyProgramFromTo((short)fromProgram, (short)toProgram, ref audion8ViewModelR.Audion8Params[1]);
                    break;

                case "Audion6":
                    audion6ViewModelR.CopyProgramFromTo((short)fromProgram, (short)toProgram, ref audion6ViewModelR.Audion6Params[1]);
                    break;

                case "Audion4":
                    audion4ViewModelR.CopyProgramFromTo((short)fromProgram, (short)toProgram, ref audion4ViewModelR.Audion4Params[1]);
                    break;

                case "SpinNR":
                    spinNRViewModelR.CopyProgramFromTo((short)fromProgram, (short)toProgram, ref spinNRViewModelR.SpinNRParams[1]);
                    break;
            }
        }

        public void CopyProgL(int fromProgram, int toProgram)
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.CopyProgramFromTo((short)fromProgram, (short)toProgram, 0);
                    break;

                case "Audion8":
                    audion8ViewModelL.CopyProgramFromTo((short)fromProgram, (short)toProgram, ref audion8ViewModelL.Audion8Params[0]);
                    break;

                case "Audion6":
                    audion6ViewModelL.CopyProgramFromTo((short)fromProgram, (short)toProgram, ref audion6ViewModelL.Audion6Params[0]);
                    break;

                case "Audion4":
                    audion4ViewModelL.CopyProgramFromTo((short)fromProgram, (short)toProgram, ref audion4ViewModelL.Audion4Params[0]);
                    break;

                case "SpinNR":
                    spinNRViewModelL.CopyProgramFromTo((short)fromProgram, (short)toProgram, ref spinNRViewModelL.SpinNRParams[0]);
                    break;
            }
        }

        public void UnmuteR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.Unmute();
                    break;

                case "Audion8":
                    audion8ViewModelR.Unmute();
                    break;

                case "Audion6":
                    audion6ViewModelR.Unmute();
                    break;

                case "Audion4":
                    audion4ViewModelR.Unmute();
                    break;

                case "SpinNR":
                    spinNRViewModelR.Unmute();
                    break;
            }
        }

        public void UnmuteL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.Unmute();
                    break;

                case "Audion8":
                    audion8ViewModelL.Unmute();
                    break;

                case "Audion6":
                    audion6ViewModelL.Unmute();
                    break;

                case "Audion4":
                    audion4ViewModelL.Unmute();
                    break;

                case "SpinNR":
                    spinNRViewModelL.Unmute();
                    break;
            }
        }

        public void MuteR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    audion16ViewModelR.Mute();
                    break;

                case "Audion8":
                    audion8ViewModelR.Muted();
                    break;

                case "Audion6":
                    audion6ViewModelR.Muted();
                    break;

                case "Audion4":
                    audion4ViewModelR.Muted();
                    break;

                case "SpinNR":
                    spinNRViewModelR.Muted();
                    break;
            }
        }

        public void MuteL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    audion16ViewModelL.Mute();
                    break;

                case "Audion8":
                    audion8ViewModelL.Muted();
                    break;

                case "Audion6":
                    audion6ViewModelL.Muted();
                    break;

                case "Audion4":
                    audion4ViewModelL.Muted();
                    break;

                case "SpinNR":
                    spinNRViewModelL.Muted();
                    break;
            }
        }

        public void CheckMuteL()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (ForceMuteL == true)
            {
                MuteL();
            }
            else
            {
                UnmuteL();
            }
            Mouse.OverrideCursor = null;
        }

        public void CheckMuteR()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (ForceMuteR == true)
            {
                MuteR();
            }
            else
            {
                UnmuteR();
            }
            Mouse.OverrideCursor = null;
        }

        public void PlotResponseRStart()
        {
            try
            {
                waveRule.FillTargetGains('R', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);

                switch (Properties.Settings.Default.ChipIDR)
                {
                    case "Audion16":
                        gainPlotUserControlR.gainPlotViewModel.A16Plot(audion16ViewModelR.micFR,
                                                                               audion16ViewModelR.recFR,
                                                                               audion16ViewModelR.FreqResp,
                                                                               audion16ViewModelR.GDriver);
                        break;

                    case "Audion8":
                        gainPlotUserControlR.gainPlotViewModel.A8Plot(audion8ViewModelR.micFR,
                                                                               audion8ViewModelR.recFR,
                                                                               audion8ViewModelR.FreqResp);
                        break;

                    case "Audion6":
                        gainPlotUserControlR.gainPlotViewModel.A6Plot(audion6ViewModelR.micFR,
                                                                               audion6ViewModelR.recFR,
                                                                               audion6ViewModelR.FreqResp);
                        break;

                    case "Audion4":
                        gainPlotUserControlR.gainPlotViewModel.A4Plot(audion4ViewModelR.micFR,
                                                                               audion4ViewModelR.recFR,
                                                                               audion4ViewModelR.FreqResp);
                        break;

                    case "SpinNR":
                        gainPlotUserControlR.gainPlotViewModel.SNRPlot(spinNRViewModelR.micFR,
                                                                                spinNRViewModelR.recFR,
                                                                                spinNRViewModelR.FreqResp);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void PlotResponseLStart()
        {
            try
            {
                switch (Properties.Settings.Default.ChipIDL)
                {
                    case "Audion16":
                        gainPlotUserControlL.gainPlotViewModel.A16Plot(audion16ViewModelL.micFR,
                                                                               audion16ViewModelL.recFR,
                                                                               audion16ViewModelL.FreqResp,
                                                                               audion16ViewModelL.GDriver);
                        break;

                    case "Audion8":
                        gainPlotUserControlL.gainPlotViewModel.A8Plot(audion8ViewModelL.micFR,
                                                                               audion8ViewModelL.recFR,
                                                                               audion8ViewModelL.FreqResp);
                        break;

                    case "Audion6":
                        gainPlotUserControlL.gainPlotViewModel.A6Plot(audion6ViewModelL.micFR,
                                                                               audion6ViewModelL.recFR,
                                                                               audion6ViewModelL.FreqResp);
                        break;

                    case "Audion4":
                        gainPlotUserControlL.gainPlotViewModel.A4Plot(audion4ViewModelL.micFR,
                                                                               audion4ViewModelL.recFR,
                                                                               audion4ViewModelL.FreqResp);
                        break;

                    case "SpinNR":
                        gainPlotUserControlL.gainPlotViewModel.SNRPlot(spinNRViewModelL.micFR,
                                                                                spinNRViewModelL.recFR,
                                                                                spinNRViewModelL.FreqResp);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void PlotResponseR()
        {
            try
            {
                waveRule.FillTargetGains('R', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);

                switch (Properties.Settings.Default.ChipIDR)
                {
                    case "Audion16":
                        gainPlotUserControlR.gainPlotViewModel.A16Plot50_80_MPO(waveRule,
                                                                               audion16ViewModelR.micFR,
                                                                               audion16ViewModelR.recFR,
                                                                               audion16ViewModelR.FreqResp,
                                                                               audion16ViewModelR.GDriver);
                        break;

                    case "Audion8":
                        gainPlotUserControlR.gainPlotViewModel.A8Plot50_80_MPO(waveRule,
                                                                               audion8ViewModelR.micFR,
                                                                               audion8ViewModelR.recFR,
                                                                               audion8ViewModelR.FreqResp);
                        break;

                    case "Audion6":
                        gainPlotUserControlR.gainPlotViewModel.A6Plot50_80_MPO(waveRule,
                                                                               audion6ViewModelR.micFR,
                                                                               audion6ViewModelR.recFR,
                                                                               audion6ViewModelR.FreqResp);
                        break;

                    case "Audion4":
                        gainPlotUserControlR.gainPlotViewModel.A4Plot50_80_MPO(waveRule,
                                                                               audion4ViewModelR.micFR,
                                                                               audion4ViewModelR.recFR,
                                                                               audion4ViewModelR.FreqResp);
                        break;

                    case "SpinNR":
                        gainPlotUserControlR.gainPlotViewModel.SNRPlot50_80_MPO(waveRule,
                                                                                spinNRViewModelR.micFR,
                                                                                spinNRViewModelR.recFR,
                                                                                spinNRViewModelR.FreqResp);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void PlotResponseL()
        {
            try
            {
                waveRule.FillTargetGains('L', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);

                switch (Properties.Settings.Default.ChipIDL)
                {
                    case "Audion16":
                        gainPlotUserControlL.gainPlotViewModel.A16Plot50_80_MPO(waveRule,
                                                                               audion16ViewModelL.micFR,
                                                                               audion16ViewModelL.recFR,
                                                                               audion16ViewModelL.FreqResp,
                                                                               audion16ViewModelL.GDriver);
                        break;

                    case "Audion8":
                        gainPlotUserControlL.gainPlotViewModel.A8Plot50_80_MPO(waveRule,
                                                                               audion8ViewModelL.micFR,
                                                                               audion8ViewModelL.recFR,
                                                                               audion8ViewModelL.FreqResp);
                        break;

                    case "Audion6":
                        gainPlotUserControlL.gainPlotViewModel.A6Plot50_80_MPO(waveRule,
                                                                               audion6ViewModelL.micFR,
                                                                               audion6ViewModelL.recFR,
                                                                               audion6ViewModelL.FreqResp);
                        break;

                    case "Audion4":
                        gainPlotUserControlL.gainPlotViewModel.A4Plot50_80_MPO(waveRule,
                                                                               audion4ViewModelL.micFR,
                                                                               audion4ViewModelL.recFR,
                                                                               audion4ViewModelL.FreqResp);
                        break;

                    case "SpinNR":
                        gainPlotUserControlL.gainPlotViewModel.SNRPlot50_80_MPO(waveRule,
                                                                                spinNRViewModelL.micFR,
                                                                                spinNRViewModelL.recFR,
                                                                                spinNRViewModelL.FreqResp);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public long GetSerialNumberR()
        {
            string SN1 = "";
            string SN2 = "";
            string SNHEX = "";
            long SerialNumber = 0;

            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    SN1 = audion16ViewModelR.MDA_2.ToString("X2");
                    SN2 = audion16ViewModelR.MDA_3.ToString("X2");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    break;

                case "Audion8":
                    SN1 = audion8ViewModelR.MANF_reserve_3.ToString("X");
                    SN2 = audion8ViewModelR.MANF_reserve_4.ToString("X");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    break;

                case "Audion6":
                    SN1 = audion6ViewModelR.MANF_reserve_3.ToString("X");
                    SN2 = audion6ViewModelR.MANF_reserve_4.ToString("X");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    break;

                case "Audion4":
                    SN1 = audion4ViewModelR.MANF_reserve_3.ToString("X");
                    SN2 = audion4ViewModelR.MANF_reserve_4.ToString("X");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    break;

                case "SpinNR":
                    SN1 = spinNRViewModelR.MANF_reserve_3.ToString("X");
                    SN2 = spinNRViewModelR.MANF_reserve_4.ToString("X");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    break;
            }

            return SerialNumber;
        }

        public long GetSerialNumberL()
        {
            string SN1 = "";
            string SN2 = "";
            string SNHEX = "";
            long SerialNumber = 0;

            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    SN1 = audion16ViewModelL.MDA_2.ToString("X2");
                    SN2 = audion16ViewModelL.MDA_3.ToString("X2");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    Console.WriteLine(SerialNumber.ToString());

                    break;

                case "Audion8":
                    SN1 = audion8ViewModelL.MANF_reserve_3.ToString("X");
                    SN2 = audion8ViewModelL.MANF_reserve_4.ToString("X");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    Console.WriteLine(SerialNumber.ToString());

                    break;

                case "Audion6":
                    SN1 = audion6ViewModelL.MANF_reserve_3.ToString("X");
                    SN2 = audion6ViewModelL.MANF_reserve_4.ToString("X");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    Console.WriteLine(SerialNumber.ToString());

                    break;

                case "Audion4":
                    SN1 = audion4ViewModelL.MANF_reserve_3.ToString("X");
                    SN2 = audion4ViewModelL.MANF_reserve_4.ToString("X");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    Console.WriteLine(SerialNumber.ToString());

                    break;

                case "SpinNR":
                    SN1 = spinNRViewModelL.MANF_reserve_3.ToString("X");
                    SN2 = spinNRViewModelL.MANF_reserve_4.ToString("X");
                    SNHEX = $"{SN1}" + $"{SN2}";
                    SerialNumber = Int64.Parse(SNHEX, System.Globalization.NumberStyles.HexNumber);
                    Console.WriteLine(SerialNumber.ToString());

                    break;
            }

            return SerialNumber;
        }

        public void SaveHearingAidDbR()
        {
            int idHearingAid = crudViewModel.GetAtributeInt("id", "dbo.hearingaid", "serialnumber", GetSerialNumberR());
            var existingHearingAid = HearingAid.FirstOrDefault(c =>
                                                     c.Id == idHearingAid &&
                                                     c.SerialNumber == GetSerialNumberR() &&
                                                     c.Device == Properties.Settings.Default.ChipIDR);
            if (existingHearingAid == null)
            {
                switch (Properties.Settings.Default.ChipIDR)
                {
                    case "Audion16":
                        hearingAidR.SerialNumber = GetSerialNumberR();
                        Console.WriteLine(hearingAidR.SerialNumber.ToString());
                        hearingAidR.Device = "Audion16";

                        break;

                    case "Audion8":
                        hearingAidR.SerialNumber = GetSerialNumberR();
                        Console.WriteLine(hearingAidR.SerialNumber.ToString());
                        hearingAidR.Device = "Audion8";

                        break;

                    case "Audion6":
                        hearingAidR.SerialNumber = GetSerialNumberR();
                        Console.WriteLine(hearingAidR.SerialNumber.ToString());
                        hearingAidR.Device = "Audion6";

                        break;

                    case "Audion4":
                        hearingAidR.SerialNumber = GetSerialNumberR();
                        Console.WriteLine(hearingAidR.SerialNumber.ToString());
                        hearingAidR.Device = "Audion4";

                        break;

                    case "SpinNR":
                        hearingAidR.SerialNumber = GetSerialNumberR();
                        Console.WriteLine(hearingAidR.SerialNumber.ToString());
                        hearingAidR.Device = "SpinNR";

                        break;
                }

                hearingAidR.Receptor = ReceptorR.SelectedValue.ToString();
                HearingAid.Add(hearingAidR);
                crudViewModel.AddHearingAid(hearingAidR);
            }
            else
            {
                crudViewModel.UpdateColString("dbo.hearingaid", ReceptorR.SelectedValue.ToString(), idHearingAid, "receptor");
            }
        }

        public void SaveCalibrationDbR()
        {
            try
            {
                int idHearingAid = crudViewModel.GetAtributeInt("id", "dbo.hearingaid", "serialnumber", GetSerialNumberR());

                var existingCalibration = Calibration.FirstOrDefault(c =>
                                                                     c.IdPatient == Properties.Settings.Default.patientId &&
                                                                     c.IdHearingAid == idHearingAid &&
                                                                     c.Channel == "R" &&
                                                                     c.Program == currentProgram &&
                                                                     c.Date.Date == DateTime.UtcNow.Date);
                if (existingCalibration == null)
                {
                    calibrationR.IdHearingAid = idHearingAid;
                    calibrationR.IdPatient = Properties.Settings.Default.patientId;
                    calibrationR.Channel = "R";
                    calibrationR.Program = currentProgram;

                    SetCalibrationParamsAndConfigR(calibrationR);

                    Calibration.Add(calibrationR);
                    crudViewModel.AddCalibration(Calibration);

                    HandyControl.Controls.Growl.SuccessGlobal("Seus dados foram salvos com sucesso.");
                }
                else
                {
                    SetCalibrationParamsAndConfigR(existingCalibration);
                    crudViewModel.UpdateColString("dbo.fitting", existingCalibration.Params, existingCalibration.Id, "paramters");

                    var calibrationsToUpdate = Calibration.Where(c =>
                                                                 c.IdPatient == Properties.Settings.Default.patientId &&
                                                                 c.IdHearingAid == idHearingAid &&
                                                                 c.Channel == "R" &&
                                                                 c.Date.Date == DateTime.UtcNow.Date);
                    foreach (var calibrationToUpdate in calibrationsToUpdate)
                    {
                        calibrationToUpdate.Config = existingCalibration.Config;
                        crudViewModel.UpdateColString("dbo.fitting", calibrationToUpdate.Config, calibrationToUpdate.Id, "configuration");
                    }

                    HandyControl.Controls.Growl.SuccessGlobal("Seus dados foram atualizados com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetCalibrationParamsAndConfigR(CalibrationModel calibration)
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    calibration.Params =
                        audion16ViewModelR.input_mux.ToString() + "&" +
                        audion16ViewModelR.matrix_gain.ToString() + "&" +

                         audion16ViewModelR.preamp_gain1.ToString() + "&" +
                         audion16ViewModelR.preamp_gain2.ToString() + "&" +

                         audion16ViewModelR.preamp_gain_digital_1.ToString() + "&" +
                         audion16ViewModelR.preamp_gain_digital_2.ToString() + "&" +

                         audion16ViewModelR.feedback_canceller.ToString() + "&" +
                         audion16ViewModelR.noise_reduction.ToString() + "&" +
                         audion16ViewModelR.wind_suppression.ToString() + "&" +

                         audion16ViewModelR.input_filter_low_cut.ToString() + "&" +
                         audion16ViewModelR.low_level_expansion.ToString() + "&" +

                         audion16ViewModelR.beq_gain_1.ToString() + "&" +
                         audion16ViewModelR.beq_gain_2.ToString() + "&" +
                         audion16ViewModelR.beq_gain_3.ToString() + "&" +
                         audion16ViewModelR.beq_gain_4.ToString() + "&" +
                         audion16ViewModelR.beq_gain_5.ToString() + "&" +
                         audion16ViewModelR.beq_gain_6.ToString() + "&" +
                         audion16ViewModelR.beq_gain_7.ToString() + "&" +
                         audion16ViewModelR.beq_gain_8.ToString() + "&" +
                         audion16ViewModelR.beq_gain_9.ToString() + "&" +
                         audion16ViewModelR.beq_gain_10.ToString() + "&" +
                         audion16ViewModelR.beq_gain_11.ToString() + "&" +
                         audion16ViewModelR.beq_gain_12.ToString() + "&" +
                         audion16ViewModelR.beq_gain_13.ToString() + "&" +
                         audion16ViewModelR.beq_gain_14.ToString() + "&" +
                         audion16ViewModelR.beq_gain_15.ToString() + "&" +
                         audion16ViewModelR.beq_gain_16.ToString() + "&" +

                         audion16ViewModelR.mpo_threshold_1.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_2.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_3.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_4.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_5.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_6.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_7.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_8.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_9.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_10.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_11.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_12.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_13.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_14.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_15.ToString() + "&" +
                         audion16ViewModelR.mpo_threshold_16.ToString() + "&" +

                         audion16ViewModelR.mpo_release.ToString() + "&" +
                         audion16ViewModelR.mpo_attack.ToString() + "&" +

                         audion16ViewModelR.comp_ratio_1.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_2.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_3.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_4.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_5.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_6.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_7.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_8.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_9.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_10.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_11.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_12.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_13.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_14.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_15.ToString() + "&" +
                         audion16ViewModelR.comp_ratio_16.ToString() + "&" +

                         audion16ViewModelR.comp_threshold_1.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_2.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_3.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_4.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_5.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_6.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_7.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_8.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_9.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_10.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_11.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_12.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_13.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_14.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_15.ToString() + "&" +
                         audion16ViewModelR.comp_threshold_16.ToString() + "&" +

                         audion16ViewModelR.comp_time_consts_1.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_2.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_3.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_4.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_5.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_6.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_7.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_8.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_9.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_10.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_11.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_12.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_13.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_14.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_15.ToString() + "&" +
                         audion16ViewModelR.comp_time_consts_16;

                    calibration.Config =
                        audion16ViewModelR.Switch_Mode.ToString() + "&" +
                        audion16ViewModelR.VC_Mode.ToString() + "&" +
                        audion16ViewModelR.VC_Enable.ToString() + "&" +
                        audion16ViewModelR.Auto_Save.ToString() + "&" +
                        audion16ViewModelR.VC_Prompt_Mode.ToString() + "&" +
                        audion16ViewModelR.Program_Prompt_Mode.ToString() + "&" +
                        audion16ViewModelR.Warning_Prompt_Mode.ToString() + "&" +
                        audion16ViewModelR.Power_On_VC.ToString() + "&" +
                        audion16ViewModelR.Power_On_Program.ToString() + "&" +
                        audion16ViewModelR.VC_Num_Steps.ToString() + "&" +
                        audion16ViewModelR.VC_Step_Size.ToString() + "&" +
                        audion16ViewModelR.VC_Analog_Range.ToString() + "&" +
                        audion16ViewModelR.Num_Programs.ToString() + "&" +
                        audion16ViewModelR.Prompt_Level.ToString() + "&" +
                        audion16ViewModelR.Tone_Frequency.ToString() + "&" +
                        audion16ViewModelR.ADir_Sensitivity.ToString() + "&" +
                        audion16ViewModelR.Auto_Telecoil.ToString() + "&" +
                        audion16ViewModelR.Acoustap_Mode.ToString() + "&" +
                        audion16ViewModelR.Acoustap_Sensitivity.ToString() + "&" +
                        audion16ViewModelR.Power_On_Level.ToString() + "&" +
                        audion16ViewModelR.Power_On_Delay.ToString() + "&" +
                        audion16ViewModelR.Noise_Level.ToString() + "&" +
                        audion16ViewModelR.High_Power_Mode.ToString() + "&" +
                        audion16ViewModelR.Dir_Mic_Cal.ToString() + "&" +
                        audion16ViewModelR.Dir_Mic_Cal_Input.ToString() + "&" +
                        audion16ViewModelR.Dir_Spacing.ToString() + "&" +
                        audion16ViewModelR.test.ToString() + "&" +
                        audion16ViewModelR.Output_Filter_Enable.ToString() + "&" +
                        audion16ViewModelR.Output_Filter_1.ToString() + "&" +
                        audion16ViewModelR.Output_Filter_2.ToString() + "&" +
                        audion16ViewModelR.Noise_Filter_Ref.ToString() + "&" +
                        audion16ViewModelR.Noise_Filter_1.ToString() + "&" +
                        audion16ViewModelR.Noise_Filter_2.ToString() + "&" +

                        audion16ViewModelR.MANF_ID.ToString() + "&" +
                        audion16ViewModelR.Platform_ID.ToString() + "&" +
                        audion16ViewModelR.AlgVer_Build.ToString() + "&" +
                        audion16ViewModelR.AlgVer_Major.ToString() + "&" +
                        audion16ViewModelR.AlgVer_Minor.ToString() + "&" +
                        audion16ViewModelR.ModelID.ToString() + "&" +
                        audion16ViewModelR.MDA_1.ToString() + "&" +
                        audion16ViewModelR.MDA_2.ToString() + "&" +
                        audion16ViewModelR.MDA_3.ToString() + "&" +
                        audion16ViewModelR.MDA_4.ToString() + "&" +
                        audion16ViewModelR.MDA_5.ToString() + "&" +
                        audion16ViewModelR.MDA_6.ToString() + "&" +
                        audion16ViewModelR.MDA_7.ToString() + "&" +
                        audion16ViewModelR.MDA_8.ToString() + "&" +
                        audion16ViewModelR.MDA_9.ToString() + "&" +
                        audion16ViewModelR.MDA_10.ToString();

                    break;

                case "Audion8":
                    calibration.Params =
                        audion8ViewModelR.input_mux.ToString() + "&" +
                        audion8ViewModelR.preamp_gain0.ToString() + "&" +
                        audion8ViewModelR.preamp_gain1.ToString() + "&" +
                        audion8ViewModelR.C1_Ratio.ToString() + "&" +
                        audion8ViewModelR.C2_Ratio.ToString() + "&" +
                        audion8ViewModelR.C3_Ratio.ToString() + "&" +
                        audion8ViewModelR.C4_Ratio.ToString() + "&" +
                        audion8ViewModelR.C5_Ratio.ToString() + "&" +
                        audion8ViewModelR.C6_Ratio.ToString() + "&" +
                        audion8ViewModelR.C7_Ratio.ToString() + "&" +
                        audion8ViewModelR.C8_Ratio.ToString() + "&" +
                        audion8ViewModelR.C1_TK.ToString() + "&" +
                        audion8ViewModelR.C2_TK.ToString() + "&" +
                        audion8ViewModelR.C3_TK.ToString() + "&" +
                        audion8ViewModelR.C4_TK.ToString() + "&" +
                        audion8ViewModelR.C5_TK.ToString() + "&" +
                        audion8ViewModelR.C6_TK.ToString() + "&" +
                        audion8ViewModelR.C7_TK.ToString() + "&" +
                        audion8ViewModelR.C8_TK.ToString() + "&" +
                        audion8ViewModelR.C1_MPO.ToString() + "&" +
                        audion8ViewModelR.C2_MPO.ToString() + "&" +
                        audion8ViewModelR.C3_MPO.ToString() + "&" +
                        audion8ViewModelR.C4_MPO.ToString() + "&" +
                        audion8ViewModelR.C5_MPO.ToString() + "&" +
                        audion8ViewModelR.C6_MPO.ToString() + "&" +
                        audion8ViewModelR.C7_MPO.ToString() + "&" +
                        audion8ViewModelR.C8_MPO.ToString() + "&" +
                        audion8ViewModelR.BEQ1_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ2_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ3_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ4_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ5_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ6_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ7_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ8_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ9_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ10_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ11_gain.ToString() + "&" +
                        audion8ViewModelR.BEQ12_gain.ToString() + "&" +
                        audion8ViewModelR.matrix_gain.ToString() + "&" +
                        audion8ViewModelR.Noise_Reduction.ToString() + "&" +
                        audion8ViewModelR.FBC_Enable.ToString() + "&" +
                        audion8ViewModelR.Time_Constants.ToString();

                    calibration.Config =
                        audion8ViewModelR.AutoSave_Enable.ToString() + "&" +
                        audion8ViewModelR.ATC.ToString() + "&" +
                        audion8ViewModelR.EnableHPmode.ToString() + "&" +
                        audion8ViewModelR.Noise_Level.ToString() + "&" +
                        audion8ViewModelR.POL.ToString() + "&" +
                        audion8ViewModelR.POD.ToString() + "&" +
                        audion8ViewModelR.AD_Sens.ToString() + "&" +
                        audion8ViewModelR.Cal_Input.ToString() + "&" +
                        audion8ViewModelR.Dir_Spacing.ToString() + "&" +
                        audion8ViewModelR.Mic_Cal.ToString() + "&" +
                        audion8ViewModelR.number_of_programs.ToString() + "&" +
                        audion8ViewModelR.PGM_Startup.ToString() + "&" +
                        audion8ViewModelR.Switch_Mode.ToString() + "&" +
                        audion8ViewModelR.Program_Prompt_Mode.ToString() + "&" +
                        audion8ViewModelR.Warning_Prompt_Mode.ToString() + "&" +
                        audion8ViewModelR.Tone_Frequency.ToString() + "&" +
                        audion8ViewModelR.Tone_Level.ToString() + "&" +
                        audion8ViewModelR.VC_Enable.ToString() + "&" +
                        audion8ViewModelR.VC_Analog_Range.ToString() + "&" +
                        audion8ViewModelR.VC_Digital_Numsteps.ToString() + "&" +
                        audion8ViewModelR.VC_Digital_Startup.ToString() + "&" +
                        audion8ViewModelR.VC_Digital_Stepsize.ToString() + "&" +
                        audion8ViewModelR.VC_Mode.ToString() + "&" +
                        audion8ViewModelR.VC_pos.ToString() + "&" +
                        audion8ViewModelR.VC_Prompt_Mode.ToString() + "&" +
                        audion8ViewModelR.AlgVer_Major.ToString() + "&" +
                        audion8ViewModelR.AlgVer_Minor.ToString() + "&" +
                        audion8ViewModelR.MANF_ID.ToString() + "&" +
                        audion8ViewModelR.PlatformID.ToString() + "&" +
                        audion8ViewModelR.reserved1.ToString() + "&" +
                        audion8ViewModelR.reserved2.ToString() + "&" +
                        audion8ViewModelR.test.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_1.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_2.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_3.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_4.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_5.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_6.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_7.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_8.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_9.ToString() + "&" +
                        audion8ViewModelR.MANF_reserve_10.ToString();

                    break;

                case "Audion6":
                    calibration.Params =
                        audion6ViewModelR.ActiveProgram.ToString() + "&" +
                        audion6ViewModelR.BEQ1_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ2_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ3_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ4_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ5_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ6_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ7_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ8_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ9_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ10_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ11_gain.ToString() + "&" +
                        audion6ViewModelR.BEQ12_gain.ToString() + "&" +
                        audion6ViewModelR.C1_ExpTK.ToString() + "&" +
                        audion6ViewModelR.C2_ExpTK.ToString() + "&" +
                        audion6ViewModelR.C3_ExpTK.ToString() + "&" +
                        audion6ViewModelR.C4_ExpTK.ToString() + "&" +
                        audion6ViewModelR.C5_ExpTK.ToString() + "&" +
                        audion6ViewModelR.C6_ExpTK.ToString() + "&" +
                        audion6ViewModelR.C1_MPO.ToString() + "&" +
                        audion6ViewModelR.C2_MPO.ToString() + "&" +
                        audion6ViewModelR.C3_MPO.ToString() + "&" +
                        audion6ViewModelR.C4_MPO.ToString() + "&" +
                        audion6ViewModelR.C5_MPO.ToString() + "&" +
                        audion6ViewModelR.C6_MPO.ToString() + "&" +
                        audion6ViewModelR.C1_Ratio.ToString() + "&" +
                        audion6ViewModelR.C2_Ratio.ToString() + "&" +
                        audion6ViewModelR.C3_Ratio.ToString() + "&" +
                        audion6ViewModelR.C4_Ratio.ToString() + "&" +
                        audion6ViewModelR.C5_Ratio.ToString() + "&" +
                        audion6ViewModelR.C6_Ratio.ToString() + "&" +
                        audion6ViewModelR.C1_TK.ToString() + "&" +
                        audion6ViewModelR.C2_TK.ToString() + "&" +
                        audion6ViewModelR.C3_TK.ToString() + "&" +
                        audion6ViewModelR.C4_TK.ToString() + "&" +
                        audion6ViewModelR.C5_TK.ToString() + "&" +
                        audion6ViewModelR.C6_TK.ToString() + "&" +
                        audion6ViewModelR.Exp_Attack.ToString() + "&" +
                        audion6ViewModelR.Exp_Ratio.ToString() + "&" +
                        audion6ViewModelR.Exp_Release.ToString() + "&" +
                        audion6ViewModelR.FBC_Enable.ToString() + "&" +
                        audion6ViewModelR.input_mux.ToString() + "&" +
                        audion6ViewModelR.matrix_gain.ToString() + "&" +
                        audion6ViewModelR.MPO_Attack.ToString() + "&" +
                        audion6ViewModelR.MPO_Release.ToString() + "&" +
                        audion6ViewModelR.Noise_Reduction.ToString() + "&" +
                        audion6ViewModelR.preamp_gain0.ToString() + "&" +
                        audion6ViewModelR.preamp_gain1.ToString() + "&" +
                        audion6ViewModelR.TimeConstants1.ToString() + "&" +
                        audion6ViewModelR.TimeConstants2.ToString() + "&" +
                        audion6ViewModelR.TimeConstants3.ToString() + "&" +
                        audion6ViewModelR.TimeConstants4.ToString() + "&" +
                        audion6ViewModelR.TimeConstants5.ToString() + "&" +
                        audion6ViewModelR.TimeConstants6.ToString() + "&" +
                        audion6ViewModelR.VcPosition.ToString();

                    calibration.Config =
                        audion6ViewModelR.Auto_Telecoil_Enable.ToString() + "&" +
                        audion6ViewModelR.Cal_Input.ToString() + "&" +
                        audion6ViewModelR.Dir_Spacing.ToString() + "&" +
                        audion6ViewModelR.Low_Battery_Warning.ToString() + "&" +
                        audion6ViewModelR.Mic_Cal.ToString() + "&" +
                        audion6ViewModelR.number_of_programs.ToString() + "&" +
                        audion6ViewModelR.Output_Mode.ToString() + "&" +
                        audion6ViewModelR.Power_On_Delay.ToString() + "&" +
                        audion6ViewModelR.Power_On_Level.ToString() + "&" +
                        audion6ViewModelR.Program_Beep_Enable.ToString() + "&" +
                        audion6ViewModelR.Program_StartUp.ToString() + "&" +
                        audion6ViewModelR.Switch_Mode.ToString() + "&" +
                        audion6ViewModelR.Tone_Frequency.ToString() + "&" +
                        audion6ViewModelR.Tone_Level.ToString() + "&" +
                        audion6ViewModelR.VC_AnalogRange.ToString() + "&" +
                        audion6ViewModelR.VC_Enable.ToString() + "&" +
                        audion6ViewModelR.VC_Mode.ToString() + "&" +
                        audion6ViewModelR.VC_DigitalNumSteps.ToString() + "&" +
                        audion6ViewModelR.VC_StartUp.ToString() + "&" +
                        audion6ViewModelR.VC_DigitalStepSize.ToString();

                    break;

                case "Audion4":
                    calibration.Params =
                        audion4ViewModelR.BEQ1_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ2_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ3_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ4_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ5_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ6_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ7_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ8_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ9_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ10_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ11_gain.ToString() + "&" +
                        audion4ViewModelR.BEQ12_gain.ToString() + "&" +
                        audion4ViewModelR.C1_Ratio.ToString() + "&" +
                        audion4ViewModelR.C2_Ratio.ToString() + "&" +
                        audion4ViewModelR.C3_Ratio.ToString() + "&" +
                        audion4ViewModelR.C4_Ratio.ToString() + "&" +
                        audion4ViewModelR.Expansion_Enable.ToString() + "&" +
                        audion4ViewModelR.FBC_Enable.ToString() + "&" +
                        audion4ViewModelR.High_Cut.ToString() + "&" +
                        audion4ViewModelR.input_mux.ToString() + "&" +
                        audion4ViewModelR.Low_Cut.ToString() + "&" +
                        audion4ViewModelR.matrix_gain.ToString() + "&" +
                        audion4ViewModelR.MPO_level.ToString() + "&" +
                        audion4ViewModelR.Noise_Reduction.ToString() + "&" +
                        audion4ViewModelR.preamp_gain0.ToString() + "&" +
                        audion4ViewModelR.preamp_gain1.ToString() + "&" +
                        audion4ViewModelR.threshold.ToString();

                    calibration.Config =
                        audion4ViewModelR.ATC.ToString() + "&" +
                        audion4ViewModelR.Auto_Save.ToString() + "&" +
                        audion4ViewModelR.Cal_Input.ToString() + "&" +
                        audion4ViewModelR.Dir_Spacing.ToString() + "&" +
                        audion4ViewModelR.Low_Batt_Warning.ToString() + "&" +
                        audion4ViewModelR.MAP_HC.ToString() + "&" +
                        audion4ViewModelR.MAP_LC.ToString() + "&" +
                        audion4ViewModelR.MAP_MPO.ToString() + "&" +
                        audion4ViewModelR.MAP_TK.ToString() + "&" +
                        audion4ViewModelR.Mic_Cal.ToString() + "&" +
                        audion4ViewModelR.number_of_programs.ToString() + "&" +
                        audion4ViewModelR.Power_On_Level.ToString() + "&" +
                        audion4ViewModelR.Power_On_Delay.ToString() + "&" +
                        audion4ViewModelR.Program_StartUp.ToString() + "&" +
                        audion4ViewModelR.Out_Mode.ToString() + "&" +
                        audion4ViewModelR.Switch_Mode.ToString() + "&" +
                        audion4ViewModelR.Switch_Tone.ToString() + "&" +
                        audion4ViewModelR.T1_DIR.ToString() + "&" +
                        audion4ViewModelR.T2_DIR.ToString() + "&" +
                        audion4ViewModelR.test.ToString() + "&" +
                        audion4ViewModelR.Tone_Frequency.ToString() + "&" +
                        audion4ViewModelR.Tone_Level.ToString() + "&" +
                        audion4ViewModelR.Time_Constants.ToString() + "&" +
                        audion4ViewModelR.VC_AnalogRange.ToString() + "&" +
                        audion4ViewModelR.VC_Beep_Enable.ToString() + "&" +
                        audion4ViewModelR.VC_DigitalNumSteps.ToString() + "&" +
                        audion4ViewModelR.VC_DigitalStepSize.ToString() + "&" +
                        audion4ViewModelR.VC_Enable.ToString() + "&" +
                        audion4ViewModelR.VC_Mode.ToString() + "&" +
                        audion4ViewModelR.VC_Startup.ToString() + "&" +
                        audion4ViewModelR.Active_PGM.ToString() + "&" +
                        audion4ViewModelR.T1_POS.ToString() + "&" +
                        audion4ViewModelR.T2_POS.ToString() + "&" +
                        audion4ViewModelR.VC_Pos.ToString();

                    break;

                case "SpinNR":
                    calibration.Params =
                        spinNRViewModelR.input_mux.ToString() + "&" +
                        spinNRViewModelR.preamp_gain0.ToString() + "&" +
                        spinNRViewModelR.preamp_gain1.ToString() + "&" +
                        spinNRViewModelR.CRL.ToString() + "&" +
                        spinNRViewModelR.CRH.ToString() + "&" +
                        spinNRViewModelR.threshold.ToString() + "&" +
                        spinNRViewModelR.Low_Cut.ToString() + "&" +
                        spinNRViewModelR.High_Cut.ToString() + "&" +
                        spinNRViewModelR.Noise_Reduction.ToString() + "&" +
                        spinNRViewModelR.BEQ1_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ2_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ3_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ4_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ5_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ6_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ7_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ8_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ9_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ10_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ11_gain.ToString() + "&" +
                        spinNRViewModelR.BEQ12_gain.ToString() + "&" +
                        spinNRViewModelR.matrix_gain.ToString() + "&" +
                        spinNRViewModelR.MPO_level.ToString() + "&" +
                        spinNRViewModelR.FBC_Enable.ToString() + "&" +
                        spinNRViewModelR.Cal_Input.ToString() + "&" +
                        spinNRViewModelR.Mic_Cal.ToString();

                    calibration.Config =
                        spinNRViewModelR.number_of_programs.ToString() + "&" +
                        spinNRViewModelR.VC_MAP.ToString() + "&" +
                        spinNRViewModelR.VC_Range.ToString() + "&" +
                        spinNRViewModelR.VC_pos.ToString() + "&" +
                        spinNRViewModelR.TK_MAP.ToString() + "&" +
                        spinNRViewModelR.HC_MAP.ToString() + "&" +
                        spinNRViewModelR.LC_MAP.ToString() + "&" +
                        spinNRViewModelR.MPO_MAP.ToString() + "&" +
                        spinNRViewModelR.T1_DIR.ToString() + "&" +
                        spinNRViewModelR.T2_DIR.ToString() + "&" +
                        spinNRViewModelR.T3_DIR.ToString() + "&" +
                        spinNRViewModelR.CoilPGM.ToString() + "&" +
                        spinNRViewModelR.MANF_ID.ToString() + "&" +
                        spinNRViewModelR.OutMode.ToString() + "&" +
                        spinNRViewModelR.Switch_Tone.ToString() + "&" +
                        spinNRViewModelR.Low_Batt_Warning.ToString() + "&" +
                        spinNRViewModelR.Tone_Frequency.ToString() + "&" +
                        spinNRViewModelR.Tone_Level.ToString() + "&" +
                        spinNRViewModelR.ATC.ToString() + "&" +
                        spinNRViewModelR.TimeConstants.ToString() + "&" +
                        spinNRViewModelR.Mic_Expansion.ToString() + "&" +
                        spinNRViewModelR.reserved1.ToString() + "&" +
                        spinNRViewModelR.reserved2.ToString() + "&" +
                        spinNRViewModelR.reserved3.ToString() + "&" +
                        spinNRViewModelR.reserved4.ToString() + "&" +
                        spinNRViewModelR.test.ToString() + "&" +
                        spinNRViewModelR.T1_POS.ToString() + "&" +
                        spinNRViewModelR.T2_POS.ToString() + "&" +
                        spinNRViewModelR.T3_POS.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_1.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_2.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_3.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_4.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_5.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_6.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_7.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_8.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_9.ToString() + "&" +
                        spinNRViewModelR.MANF_reserve_10.ToString();
                    break;
            }
        }

        public void SaveGainPlotDbR()
        {
            try
            {
                int idHearingAid = crudViewModel.GetAtributeInt("id", "dbo.hearingaid", "serialnumber", GetSerialNumberR());

                var existingCalibration = Calibration.FirstOrDefault(c =>
                                                                     c.IdPatient == Properties.Settings.Default.patientId &&
                                                                     c.IdHearingAid == idHearingAid &&
                                                                     c.Channel == "R" &&
                                                                     c.Program == currentProgram &&
                                                                     c.Date.Date == DateTime.UtcNow.Date);

                if (crudViewModel.GetAtributeInt("id", "dbo.gainplot", "idfitting", existingCalibration.Id) == 0)
                {
                    hearingAidGainPlotR.IdCalibration = existingCalibration.Id;
                    hearingAidGainPlotR.Plot = CreateGainPlotImageR(280, 330);

                    HearingAidGainPlot.Add(hearingAidGainPlotR);
                    crudViewModel.AddHearingAidGainPLot(HearingAidGainPlot);
                }
                else
                {
                    Console.WriteLine($"Fitting ID R: {existingCalibration.Id}");
                    Console.WriteLine($"ID: {crudViewModel.GetAtributeInt("id", "dbo.gainplot", "idfitting", existingCalibration.Id)}");
                    crudViewModel.UpdateColImage("dbo.gainplot", CreateGainPlotImageR(280, 330), crudViewModel.GetAtributeInt("id", "dbo.gainplot", "idfitting", existingCalibration.Id), "plot");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SaveDbR()
        {
            hearingAidR = new HearingAidModel();
            calibrationR = new CalibrationModel();
            hearingAidGainPlotR = new HearingAidGainPlotModel();

            calibrationR.Date = DateTime.UtcNow.Date;
            currentSide = 'R';

            if (currentProgram < 0)
            {
                currentProgram = 0;
            }

            SaveHearingAidDbR();
            SaveCalibrationDbR();
            SaveGainPlotDbR();
        }

        public void SaveHearingAidDbL()
        {
            int idHearingAid = crudViewModel.GetAtributeInt("id", "dbo.hearingaid", "serialnumber", GetSerialNumberL());
            var existingHearingAid = HearingAid.FirstOrDefault(c =>
                                                     c.Id == idHearingAid &&
                                                     c.SerialNumber == GetSerialNumberL() &&
                                                     c.Device == Properties.Settings.Default.ChipIDL);
            if (existingHearingAid == null)
            {
                switch (Properties.Settings.Default.ChipIDL)
                {
                    case "Audion16":
                        hearingAidL.SerialNumber = GetSerialNumberL();
                        hearingAidL.Device = "Audion16";
                        break;

                    case "Audion8":
                        hearingAidL.SerialNumber = GetSerialNumberL();
                        hearingAidL.Device = "Audion8";
                        break;

                    case "Audion6":
                        hearingAidL.SerialNumber = GetSerialNumberL();
                        hearingAidL.Device = "Audion6";
                        break;

                    case "Audion4":
                        hearingAidL.SerialNumber = GetSerialNumberL();
                        hearingAidL.Device = "Audion4";
                        break;

                    case "SpinNR":
                        hearingAidL.SerialNumber = GetSerialNumberL();
                        hearingAidL.Device = "SpinNR";
                        break;
                }

                hearingAidL.Receptor = ReceptorL.SelectedValue.ToString();
                HearingAid.Add(hearingAidL);
                crudViewModel.AddHearingAid(hearingAidL);
            }
            else
            {
                crudViewModel.UpdateColString("dbo.hearingaid", ReceptorL.SelectedValue.ToString(), idHearingAid, "receptor");
            }
        }

        public void SaveCalibrationDbL()
        {
            try
            {
                int idHearingAid = crudViewModel.GetAtributeInt("id", "dbo.hearingaid", "serialnumber", GetSerialNumberL());

                var existingCalibration = Calibration.FirstOrDefault(c =>
                                                                     c.IdPatient == Properties.Settings.Default.patientId &&
                                                                     c.IdHearingAid == idHearingAid &&
                                                                     c.Channel == "L" &&
                                                                     c.Program == currentProgram &&
                                                                     c.Date.Date == DateTime.UtcNow.Date);
                if (existingCalibration == null)
                {
                    calibrationL.IdHearingAid = idHearingAid;
                    calibrationL.IdPatient = Properties.Settings.Default.patientId;
                    calibrationL.Channel = "L";
                    calibrationL.Program = currentProgram;

                    SetCalibrationParamsAndConfigL(calibrationL);

                    Calibration.Add(calibrationL);
                    crudViewModel.AddCalibration(Calibration);

                    HandyControl.Controls.Growl.SuccessGlobal("Seus dados foram salvos com sucesso.");
                }
                else
                {
                    SetCalibrationParamsAndConfigL(existingCalibration);

                    crudViewModel.UpdateColString("dbo.fitting", existingCalibration.Params, existingCalibration.Id, "paramters");

                    var calibrationsToUpdate = Calibration.Where(c =>
                                                                 c.IdPatient == Properties.Settings.Default.patientId &&
                                                                 c.IdHearingAid == idHearingAid &&
                                                                 c.Channel == "L" &&
                                                                 c.Date.Date == DateTime.UtcNow.Date);
                    foreach (var calibrationToUpdate in calibrationsToUpdate)
                    {
                        calibrationToUpdate.Config = existingCalibration.Config;
                        crudViewModel.UpdateColString("dbo.fitting", existingCalibration.Config, calibrationToUpdate.Id, "configuration");
                    }

                    HandyControl.Controls.Growl.SuccessGlobal("Seus dados foram atualizados com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetCalibrationParamsAndConfigL(CalibrationModel calibration)
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    calibration.Params =
                       audion16ViewModelL.input_mux.ToString() + "&" +
                       audion16ViewModelL.matrix_gain.ToString() + "&" +

                        audion16ViewModelL.preamp_gain1.ToString() + "&" +
                        audion16ViewModelL.preamp_gain2.ToString() + "&" +

                        audion16ViewModelL.preamp_gain_digital_1.ToString() + "&" +
                        audion16ViewModelL.preamp_gain_digital_2.ToString() + "&" +

                        audion16ViewModelL.feedback_canceller.ToString() + "&" +
                        audion16ViewModelL.noise_reduction.ToString() + "&" +
                        audion16ViewModelL.wind_suppression.ToString() + "&" +

                        audion16ViewModelL.input_filter_low_cut.ToString() + "&" +
                        audion16ViewModelL.low_level_expansion.ToString() + "&" +

                        audion16ViewModelL.beq_gain_1.ToString() + "&" +
                        audion16ViewModelL.beq_gain_2.ToString() + "&" +
                        audion16ViewModelL.beq_gain_3.ToString() + "&" +
                        audion16ViewModelL.beq_gain_4.ToString() + "&" +
                        audion16ViewModelL.beq_gain_5.ToString() + "&" +
                        audion16ViewModelL.beq_gain_6.ToString() + "&" +
                        audion16ViewModelL.beq_gain_7.ToString() + "&" +
                        audion16ViewModelL.beq_gain_8.ToString() + "&" +
                        audion16ViewModelL.beq_gain_9.ToString() + "&" +
                        audion16ViewModelL.beq_gain_10.ToString() + "&" +
                        audion16ViewModelL.beq_gain_11.ToString() + "&" +
                        audion16ViewModelL.beq_gain_12.ToString() + "&" +
                        audion16ViewModelL.beq_gain_13.ToString() + "&" +
                        audion16ViewModelL.beq_gain_14.ToString() + "&" +
                        audion16ViewModelL.beq_gain_15.ToString() + "&" +
                        audion16ViewModelL.beq_gain_16.ToString() + "&" +

                        audion16ViewModelL.mpo_threshold_1.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_2.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_3.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_4.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_5.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_6.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_7.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_8.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_9.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_10.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_11.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_12.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_13.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_14.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_15.ToString() + "&" +
                        audion16ViewModelL.mpo_threshold_16.ToString() + "&" +

                        audion16ViewModelL.mpo_release.ToString() + "&" +
                        audion16ViewModelL.mpo_attack.ToString() + "&" +

                        audion16ViewModelL.comp_ratio_1.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_2.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_3.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_4.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_5.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_6.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_7.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_8.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_9.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_10.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_11.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_12.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_13.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_14.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_15.ToString() + "&" +
                        audion16ViewModelL.comp_ratio_16.ToString() + "&" +

                        audion16ViewModelL.comp_threshold_1.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_2.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_3.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_4.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_5.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_6.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_7.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_8.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_9.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_10.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_11.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_12.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_13.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_14.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_15.ToString() + "&" +
                        audion16ViewModelL.comp_threshold_16.ToString() + "&" +

                        audion16ViewModelL.comp_time_consts_1.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_2.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_3.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_4.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_5.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_6.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_7.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_8.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_9.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_10.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_11.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_12.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_13.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_14.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_15.ToString() + "&" +
                        audion16ViewModelL.comp_time_consts_16.ToString();

                    calibration.Config =
                        audion16ViewModelL.Switch_Mode.ToString() + "&" +
                        audion16ViewModelL.VC_Mode.ToString() + "&" +
                        audion16ViewModelL.VC_Enable.ToString() + "&" +
                        audion16ViewModelL.Auto_Save.ToString() + "&" +
                        audion16ViewModelL.VC_Prompt_Mode.ToString() + "&" +
                        audion16ViewModelL.Program_Prompt_Mode.ToString() + "&" +
                        audion16ViewModelL.Warning_Prompt_Mode.ToString() + "&" +
                        audion16ViewModelL.Power_On_VC.ToString() + "&" +
                        audion16ViewModelL.Power_On_Program.ToString() + "&" +
                        audion16ViewModelL.VC_Num_Steps.ToString() + "&" +
                        audion16ViewModelL.VC_Step_Size.ToString() + "&" +
                        audion16ViewModelL.VC_Analog_Range.ToString() + "&" +
                        audion16ViewModelL.Num_Programs.ToString() + "&" +
                        audion16ViewModelL.Prompt_Level.ToString() + "&" +
                        audion16ViewModelL.Tone_Frequency.ToString() + "&" +
                        audion16ViewModelL.ADir_Sensitivity.ToString() + "&" +
                        audion16ViewModelL.Auto_Telecoil.ToString() + "&" +
                        audion16ViewModelL.Acoustap_Mode.ToString() + "&" +
                        audion16ViewModelL.Acoustap_Sensitivity.ToString() + "&" +
                        audion16ViewModelL.Power_On_Level.ToString() + "&" +
                        audion16ViewModelL.Power_On_Delay.ToString() + "&" +
                        audion16ViewModelL.Noise_Level.ToString() + "&" +
                        audion16ViewModelL.High_Power_Mode.ToString() + "&" +
                        audion16ViewModelL.Dir_Mic_Cal.ToString() + "&" +
                        audion16ViewModelL.Dir_Mic_Cal_Input.ToString() + "&" +
                        audion16ViewModelL.Dir_Spacing.ToString() + "&" +
                        audion16ViewModelL.test.ToString() + "&" +
                        audion16ViewModelL.Output_Filter_Enable.ToString() + "&" +
                        audion16ViewModelL.Output_Filter_1.ToString() + "&" +
                        audion16ViewModelL.Output_Filter_2.ToString() + "&" +
                        audion16ViewModelL.Noise_Filter_Ref.ToString() + "&" +
                        audion16ViewModelL.Noise_Filter_1.ToString() + "&" +
                        audion16ViewModelL.Noise_Filter_2.ToString() + "&" +

                        audion16ViewModelL.MANF_ID.ToString() + "&" +
                        audion16ViewModelL.Platform_ID.ToString() + "&" +
                        audion16ViewModelL.AlgVer_Build.ToString() + "&" +
                        audion16ViewModelL.AlgVer_Major.ToString() + "&" +
                        audion16ViewModelL.AlgVer_Minor.ToString() + "&" +
                        audion16ViewModelL.ModelID.ToString() + "&" +
                        audion16ViewModelL.MDA_1.ToString() + "&" +
                        audion16ViewModelL.MDA_2.ToString() + "&" +
                        audion16ViewModelL.MDA_3.ToString() + "&" +
                        audion16ViewModelL.MDA_4.ToString() + "&" +
                        audion16ViewModelL.MDA_5.ToString() + "&" +
                        audion16ViewModelL.MDA_6.ToString() + "&" +
                        audion16ViewModelL.MDA_7.ToString() + "&" +
                        audion16ViewModelL.MDA_8.ToString() + "&" +
                        audion16ViewModelL.MDA_9.ToString() + "&" +
                        audion16ViewModelL.MDA_10.ToString();

                    break;

                case "Audion8":
                    calibration.Params =
                        audion8ViewModelL.input_mux.ToString() + "&" +
                        audion8ViewModelL.preamp_gain0.ToString() + "&" +
                        audion8ViewModelL.preamp_gain1.ToString() + "&" +
                        audion8ViewModelL.C1_Ratio.ToString() + "&" +
                        audion8ViewModelL.C2_Ratio.ToString() + "&" +
                        audion8ViewModelL.C3_Ratio.ToString() + "&" +
                        audion8ViewModelL.C4_Ratio.ToString() + "&" +
                        audion8ViewModelL.C5_Ratio.ToString() + "&" +
                        audion8ViewModelL.C6_Ratio.ToString() + "&" +
                        audion8ViewModelL.C7_Ratio.ToString() + "&" +
                        audion8ViewModelL.C8_Ratio.ToString() + "&" +
                        audion8ViewModelL.C1_TK.ToString() + "&" +
                        audion8ViewModelL.C2_TK.ToString() + "&" +
                        audion8ViewModelL.C3_TK.ToString() + "&" +
                        audion8ViewModelL.C4_TK.ToString() + "&" +
                        audion8ViewModelL.C5_TK.ToString() + "&" +
                        audion8ViewModelL.C6_TK.ToString() + "&" +
                        audion8ViewModelL.C7_TK.ToString() + "&" +
                        audion8ViewModelL.C8_TK.ToString() + "&" +
                        audion8ViewModelL.C1_MPO.ToString() + "&" +
                        audion8ViewModelL.C2_MPO.ToString() + "&" +
                        audion8ViewModelL.C3_MPO.ToString() + "&" +
                        audion8ViewModelL.C4_MPO.ToString() + "&" +
                        audion8ViewModelL.C5_MPO.ToString() + "&" +
                        audion8ViewModelL.C6_MPO.ToString() + "&" +
                        audion8ViewModelL.C7_MPO.ToString() + "&" +
                        audion8ViewModelL.C8_MPO.ToString() + "&" +
                        audion8ViewModelL.BEQ1_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ2_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ3_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ4_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ5_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ6_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ7_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ8_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ9_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ10_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ11_gain.ToString() + "&" +
                        audion8ViewModelL.BEQ12_gain.ToString() + "&" +
                        audion8ViewModelL.matrix_gain.ToString() + "&" +
                        audion8ViewModelL.Noise_Reduction.ToString() + "&" +
                        audion8ViewModelL.FBC_Enable.ToString() + "&" +
                        audion8ViewModelL.Time_Constants.ToString();

                    calibration.Config =
                        audion8ViewModelL.AutoSave_Enable.ToString() + "&" +
                        audion8ViewModelL.ATC.ToString() + "&" +
                        audion8ViewModelL.EnableHPmode.ToString() + "&" +
                        audion8ViewModelL.Noise_Level.ToString() + "&" +
                        audion8ViewModelL.POL.ToString() + "&" +
                        audion8ViewModelL.POD.ToString() + "&" +
                        audion8ViewModelL.AD_Sens.ToString() + "&" +
                        audion8ViewModelL.Cal_Input.ToString() + "&" +
                        audion8ViewModelL.Dir_Spacing.ToString() + "&" +
                        audion8ViewModelL.Mic_Cal.ToString() + "&" +
                        audion8ViewModelL.number_of_programs.ToString() + "&" +
                        audion8ViewModelL.PGM_Startup.ToString() + "&" +
                        audion8ViewModelL.Switch_Mode.ToString() + "&" +
                        audion8ViewModelL.Program_Prompt_Mode.ToString() + "&" +
                        audion8ViewModelL.Warning_Prompt_Mode.ToString() + "&" +
                        audion8ViewModelL.Tone_Frequency.ToString() + "&" +
                        audion8ViewModelL.Tone_Level.ToString() + "&" +
                        audion8ViewModelL.VC_Enable.ToString() + "&" +
                        audion8ViewModelL.VC_Analog_Range.ToString() + "&" +
                        audion8ViewModelL.VC_Digital_Numsteps.ToString() + "&" +
                        audion8ViewModelL.VC_Digital_Startup.ToString() + "&" +
                        audion8ViewModelL.VC_Digital_Stepsize.ToString() + "&" +
                        audion8ViewModelL.VC_Mode.ToString() + "&" +
                        audion8ViewModelL.VC_pos.ToString() + "&" +
                        audion8ViewModelL.VC_Prompt_Mode.ToString() + "&" +
                        audion8ViewModelL.AlgVer_Major.ToString() + "&" +
                        audion8ViewModelL.AlgVer_Minor.ToString() + "&" +
                        audion8ViewModelL.MANF_ID.ToString() + "&" +
                        audion8ViewModelL.PlatformID.ToString() + "&" +
                        audion8ViewModelL.reserved1.ToString() + "&" +
                        audion8ViewModelL.reserved2.ToString() + "&" +
                        audion8ViewModelL.test.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_1.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_2.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_3.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_4.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_5.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_6.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_7.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_8.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_9.ToString() + "&" +
                        audion8ViewModelL.MANF_reserve_10.ToString();

                    break;

                case "Audion6":
                    calibration.Params =
                        audion6ViewModelL.ActiveProgram.ToString() + "&" +
                        audion6ViewModelL.BEQ1_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ2_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ3_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ4_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ5_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ6_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ7_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ8_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ9_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ10_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ11_gain.ToString() + "&" +
                        audion6ViewModelL.BEQ12_gain.ToString() + "&" +
                        audion6ViewModelL.C1_ExpTK.ToString() + "&" +
                        audion6ViewModelL.C2_ExpTK.ToString() + "&" +
                        audion6ViewModelL.C3_ExpTK.ToString() + "&" +
                        audion6ViewModelL.C4_ExpTK.ToString() + "&" +
                        audion6ViewModelL.C5_ExpTK.ToString() + "&" +
                        audion6ViewModelL.C6_ExpTK.ToString() + "&" +
                        audion6ViewModelL.C1_MPO.ToString() + "&" +
                        audion6ViewModelL.C2_MPO.ToString() + "&" +
                        audion6ViewModelL.C3_MPO.ToString() + "&" +
                        audion6ViewModelL.C4_MPO.ToString() + "&" +
                        audion6ViewModelL.C5_MPO.ToString() + "&" +
                        audion6ViewModelL.C6_MPO.ToString() + "&" +
                        audion6ViewModelL.C1_Ratio.ToString() + "&" +
                        audion6ViewModelL.C2_Ratio.ToString() + "&" +
                        audion6ViewModelL.C3_Ratio.ToString() + "&" +
                        audion6ViewModelL.C4_Ratio.ToString() + "&" +
                        audion6ViewModelL.C5_Ratio.ToString() + "&" +
                        audion6ViewModelL.C6_Ratio.ToString() + "&" +
                        audion6ViewModelL.C1_TK.ToString() + "&" +
                        audion6ViewModelL.C2_TK.ToString() + "&" +
                        audion6ViewModelL.C3_TK.ToString() + "&" +
                        audion6ViewModelL.C4_TK.ToString() + "&" +
                        audion6ViewModelL.C5_TK.ToString() + "&" +
                        audion6ViewModelL.C6_TK.ToString() + "&" +
                        audion6ViewModelL.Exp_Attack.ToString() + "&" +
                        audion6ViewModelL.Exp_Ratio.ToString() + "&" +
                        audion6ViewModelL.Exp_Release.ToString() + "&" +
                        audion6ViewModelL.FBC_Enable.ToString() + "&" +
                        audion6ViewModelL.input_mux.ToString() + "&" +
                        audion6ViewModelL.matrix_gain.ToString() + "&" +
                        audion6ViewModelL.MPO_Attack.ToString() + "&" +
                        audion6ViewModelL.MPO_Release.ToString() + "&" +
                        audion6ViewModelL.Noise_Reduction.ToString() + "&" +
                        audion6ViewModelL.preamp_gain0.ToString() + "&" +
                        audion6ViewModelL.preamp_gain1.ToString() + "&" +
                        audion6ViewModelL.TimeConstants1.ToString() + "&" +
                        audion6ViewModelL.TimeConstants2.ToString() + "&" +
                        audion6ViewModelL.TimeConstants3.ToString() + "&" +
                        audion6ViewModelL.TimeConstants4.ToString() + "&" +
                        audion6ViewModelL.TimeConstants5.ToString() + "&" +
                        audion6ViewModelL.TimeConstants6.ToString() + "&" +
                        audion6ViewModelL.VcPosition.ToString();

                    calibration.Config =
                        audion6ViewModelL.Auto_Telecoil_Enable.ToString() + "&" +
                        audion6ViewModelL.Cal_Input.ToString() + "&" +
                        audion6ViewModelL.Dir_Spacing.ToString() + "&" +
                        audion6ViewModelL.Low_Battery_Warning.ToString() + "&" +
                        audion6ViewModelL.Mic_Cal.ToString() + "&" +
                        audion6ViewModelL.number_of_programs.ToString() + "&" +
                        audion6ViewModelL.Output_Mode.ToString() + "&" +
                        audion6ViewModelL.Power_On_Delay.ToString() + "&" +
                        audion6ViewModelL.Power_On_Level.ToString() + "&" +
                        audion6ViewModelL.Program_Beep_Enable.ToString() + "&" +
                        audion6ViewModelL.Program_StartUp.ToString() + "&" +
                        audion6ViewModelL.Switch_Mode.ToString() + "&" +
                        audion6ViewModelL.Tone_Frequency.ToString() + "&" +
                        audion6ViewModelL.Tone_Level.ToString() + "&" +
                        audion6ViewModelL.VC_AnalogRange.ToString() + "&" +
                        audion6ViewModelL.VC_Enable.ToString() + "&" +
                        audion6ViewModelL.VC_Mode.ToString() + "&" +
                        audion6ViewModelL.VC_DigitalNumSteps.ToString() + "&" +
                        audion6ViewModelL.VC_StartUp.ToString() + "&" +
                        audion6ViewModelL.VC_DigitalStepSize.ToString();

                    break;

                case "Audion4":
                    calibration.Params =
                        audion4ViewModelL.BEQ1_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ2_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ3_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ4_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ5_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ6_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ7_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ8_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ9_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ10_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ11_gain.ToString() + "&" +
                        audion4ViewModelL.BEQ12_gain.ToString() + "&" +
                        audion4ViewModelL.C1_Ratio.ToString() + "&" +
                        audion4ViewModelL.C2_Ratio.ToString() + "&" +
                        audion4ViewModelL.C3_Ratio.ToString() + "&" +
                        audion4ViewModelL.C4_Ratio.ToString() + "&" +
                        audion4ViewModelL.Expansion_Enable.ToString() + "&" +
                        audion4ViewModelL.FBC_Enable.ToString() + "&" +
                        audion4ViewModelL.High_Cut.ToString() + "&" +
                        audion4ViewModelL.input_mux.ToString() + "&" +
                        audion4ViewModelL.Low_Cut.ToString() + "&" +
                        audion4ViewModelL.matrix_gain.ToString() + "&" +
                        audion4ViewModelL.MPO_level.ToString() + "&" +
                        audion4ViewModelL.Noise_Reduction.ToString() + "&" +
                        audion4ViewModelL.preamp_gain0.ToString() + "&" +
                        audion4ViewModelL.preamp_gain1.ToString() + "&" +
                        audion4ViewModelL.threshold.ToString();

                    calibration.Config =
                        audion4ViewModelL.ATC.ToString() + "&" +
                        audion4ViewModelL.Auto_Save.ToString() + "&" +
                        audion4ViewModelL.Cal_Input.ToString() + "&" +
                        audion4ViewModelL.Dir_Spacing.ToString() + "&" +
                        audion4ViewModelL.Low_Batt_Warning.ToString() + "&" +
                        audion4ViewModelL.MAP_HC.ToString() + "&" +
                        audion4ViewModelL.MAP_LC.ToString() + "&" +
                        audion4ViewModelL.MAP_MPO.ToString() + "&" +
                        audion4ViewModelL.MAP_TK.ToString() + "&" +
                        audion4ViewModelL.Mic_Cal.ToString() + "&" +
                        audion4ViewModelL.number_of_programs.ToString() + "&" +
                        audion4ViewModelL.Power_On_Level.ToString() + "&" +
                        audion4ViewModelL.Power_On_Delay.ToString() + "&" +
                        audion4ViewModelL.Program_StartUp.ToString() + "&" +
                        audion4ViewModelL.Out_Mode.ToString() + "&" +
                        audion4ViewModelL.Switch_Mode.ToString() + "&" +
                        audion4ViewModelL.Switch_Tone.ToString() + "&" +
                        audion4ViewModelL.T1_DIR.ToString() + "&" +
                        audion4ViewModelL.T2_DIR.ToString() + "&" +
                        audion4ViewModelL.test.ToString() + "&" +
                        audion4ViewModelL.Tone_Frequency.ToString() + "&" +
                        audion4ViewModelL.Tone_Level.ToString() + "&" +
                        audion4ViewModelL.Time_Constants.ToString() + "&" +
                        audion4ViewModelL.VC_AnalogRange.ToString() + "&" +
                        audion4ViewModelL.VC_Beep_Enable.ToString() + "&" +
                        audion4ViewModelL.VC_DigitalNumSteps.ToString() + "&" +
                        audion4ViewModelL.VC_DigitalStepSize.ToString() + "&" +
                        audion4ViewModelL.VC_Enable.ToString() + "&" +
                        audion4ViewModelL.VC_Mode.ToString() + "&" +
                        audion4ViewModelL.VC_Startup.ToString() + "&" +
                        audion4ViewModelL.Active_PGM.ToString() + "&" +
                        audion4ViewModelL.T1_POS.ToString() + "&" +
                        audion4ViewModelL.T2_POS.ToString() + "&" +
                        audion4ViewModelL.VC_Pos.ToString();

                    break;

                case "SpinNR":
                    calibration.Params =
                        spinNRViewModelL.input_mux.ToString() + "&" +
                        spinNRViewModelL.preamp_gain0.ToString() + "&" +
                        spinNRViewModelL.preamp_gain1.ToString() + "&" +
                        spinNRViewModelL.CRL.ToString() + "&" +
                        spinNRViewModelL.CRH.ToString() + "&" +
                        spinNRViewModelL.threshold.ToString() + "&" +
                        spinNRViewModelL.Low_Cut.ToString() + "&" +
                        spinNRViewModelL.High_Cut.ToString() + "&" +
                        spinNRViewModelL.Noise_Reduction.ToString() + "&" +
                        spinNRViewModelL.BEQ1_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ2_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ3_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ4_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ5_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ6_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ7_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ8_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ9_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ10_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ11_gain.ToString() + "&" +
                        spinNRViewModelL.BEQ12_gain.ToString() + "&" +
                        spinNRViewModelL.matrix_gain.ToString() + "&" +
                        spinNRViewModelL.MPO_level.ToString() + "&" +
                        spinNRViewModelL.FBC_Enable.ToString() + "&" +
                        spinNRViewModelL.Cal_Input.ToString() + "&" +
                        spinNRViewModelL.Mic_Cal.ToString();

                    calibration.Config =
                        spinNRViewModelL.number_of_programs.ToString() + "&" +
                        spinNRViewModelL.VC_MAP.ToString() + "&" +
                        spinNRViewModelL.VC_Range.ToString() + "&" +
                        spinNRViewModelL.VC_pos.ToString() + "&" +
                        spinNRViewModelL.TK_MAP.ToString() + "&" +
                        spinNRViewModelL.HC_MAP.ToString() + "&" +
                        spinNRViewModelL.LC_MAP.ToString() + "&" +
                        spinNRViewModelL.MPO_MAP.ToString() + "&" +
                        spinNRViewModelL.T1_DIR.ToString() + "&" +
                        spinNRViewModelL.T2_DIR.ToString() + "&" +
                        spinNRViewModelL.T3_DIR.ToString() + "&" +
                        spinNRViewModelL.CoilPGM.ToString() + "&" +
                        spinNRViewModelL.MANF_ID.ToString() + "&" +
                        spinNRViewModelL.OutMode.ToString() + "&" +
                        spinNRViewModelL.Switch_Tone.ToString() + "&" +
                        spinNRViewModelL.Low_Batt_Warning.ToString() + "&" +
                        spinNRViewModelL.Tone_Frequency.ToString() + "&" +
                        spinNRViewModelL.Tone_Level.ToString() + "&" +
                        spinNRViewModelL.ATC.ToString() + "&" +
                        spinNRViewModelL.TimeConstants.ToString() + "&" +
                        spinNRViewModelL.Mic_Expansion.ToString() + "&" +
                        spinNRViewModelL.reserved1.ToString() + "&" +
                        spinNRViewModelL.reserved2.ToString() + "&" +
                        spinNRViewModelL.reserved3.ToString() + "&" +
                        spinNRViewModelL.reserved4.ToString() + "&" +
                        spinNRViewModelL.test.ToString() + "&" +
                        spinNRViewModelL.T1_POS.ToString() + "&" +
                        spinNRViewModelL.T2_POS.ToString() + "&" +
                        spinNRViewModelL.T3_POS.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_1.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_2.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_3.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_4.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_5.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_6.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_7.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_8.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_9.ToString() + "&" +
                        spinNRViewModelL.MANF_reserve_10.ToString();
                    break;
            }
        }

        public void SaveGainPlotDbL()
        {
            try
            {
                int idHearingAid = crudViewModel.GetAtributeInt("id", "dbo.hearingaid", "serialnumber", GetSerialNumberL());

                var existingCalibration = Calibration.FirstOrDefault(c =>
                                                                     c.IdPatient == Properties.Settings.Default.patientId &&
                                                                     c.IdHearingAid == idHearingAid &&
                                                                     c.Channel == "L" &&
                                                                     c.Program == currentProgram &&
                                                                     c.Date.Date == DateTime.UtcNow.Date);

                if (crudViewModel.GetAtributeInt("id", "dbo.gainplot", "idfitting", existingCalibration.Id) == 0)
                {
                    hearingAidGainPlotL.IdCalibration = calibrationL.Id;
                    hearingAidGainPlotL.Plot = CreateGainPlotImageL(280, 330);

                    HearingAidGainPlot.Add(hearingAidGainPlotL);
                    crudViewModel.AddHearingAidGainPLot(HearingAidGainPlot);
                }
                else
                {
                    Console.WriteLine($"Fitting ID L: {existingCalibration.Id}");
                    Console.WriteLine($"ID: {crudViewModel.GetAtributeInt("id", "dbo.gainplot", "idfitting", existingCalibration.Id)}");
                    crudViewModel.UpdateColImage("dbo.gainplot", CreateGainPlotImageL(280, 330), crudViewModel.GetAtributeInt("id", "dbo.gainplot", "idfitting", existingCalibration.Id), "plot");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SaveDbL()
        {
            hearingAidL = new HearingAidModel();
            calibrationL = new CalibrationModel();
            hearingAidGainPlotL = new HearingAidGainPlotModel();

            calibrationL.Date = DateTime.UtcNow.Date;
            currentSide = 'L';

            if (currentProgram < 0)
            {
                currentProgram = 0;
            }

            SaveHearingAidDbL();
            SaveCalibrationDbL();
            SaveGainPlotDbL();
        }

        public byte[] CreateGainPlotImageR(int height, int width, int resolution = 96)
        {
            Image gainPlotImage = new Image();
            gainPlotImage.Source = PngExporter.ExportToBitmap(gainPlotUserControlR.gainPlotViewModel.GainPlot, width, height, OxyColor.FromRgb(0xFF, 0xFF, 0xFF), resolution);

            BitmapSource bitmapSource = (BitmapSource)gainPlotImage.Source;
            // Encode the BitmapSource as PNG
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            // Save the PNG to a MemoryStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);

                // Convert MemoryStream to byte array
                return memoryStream.ToArray();
            }
        }

        public byte[] CreateGainPlotImageL(int height, int width, int resolution = 96)
        {
            Image gainPlotImage = new Image();
            gainPlotImage.Source = PngExporter.ExportToBitmap(gainPlotUserControlL.gainPlotViewModel.GainPlot, width, height, OxyColor.FromRgb(0xFF, 0xFF, 0xFF), resolution);

            BitmapSource bitmapSource = (BitmapSource)gainPlotImage.Source;
            // Encode the BitmapSource as PNG
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            // Save the PNG to a MemoryStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);

                // Convert MemoryStream to byte array
                return memoryStream.ToArray();
            }
        }

        public void DetectR(bool read)
        {
            StartProgrammerR();
            ChangeSideR();
            SetProgramR();
            if (read == true)
            {
                ReadGetR();
            }
            CleanMessages();
        }

        public void DetectL(bool read)
        {
            StartProgrammerL();
            ChangeSideL();
            SetProgramL();
            if (read == true)
            {
                ReadGetL();
            }
            CleanMessages();
        }

        private void DetectButtonR_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            EnableRight();
            DetectR(true);
            Growl.Success("Canal Direito Reconectado com Sucessso");
            Mouse.OverrideCursor = null;
        }

        private void DetectButtonL_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            EnableLeft();
            DetectL(true);
            Growl.Success("Canal Esquedo Reconectado com Sucessso");
            Mouse.OverrideCursor = null;
        }

        private void MuteButtonR_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            if (MuteButtonR.IsChecked == true)
            {
                ErrorSolverR();
                ForceMuteR = true;
                MuteR();
            }
            else
            {
                ErrorSolverR();
                ForceMuteR = false;
                UnmuteR();
            }

            Mouse.OverrideCursor = null;
        }

        private void MuteButtonL_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            if (MuteButtonL.IsChecked == true)
            {
                ErrorSolverL();
                ForceMuteL = true;
                MuteL();
            }
            else
            {
                ErrorSolverL();
                ForceMuteL = false;
                UnmuteL();
            }

            Mouse.OverrideCursor = null;
        }

        private void HelpR_Click(object sender, RoutedEventArgs e)
        {
            DrawerLeft.IsOpen = false;
            DrawerRight.IsOpen = true;
        }

        private void HelpL_Click(object sender, RoutedEventArgs e)
        {
            DrawerRight.IsOpen = false;
            DrawerLeft.IsOpen = true;
        }

        private void BurnR_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            SetValuesR();
            BurnSetR();
            CheckMuteR();
            PlotResponseR();

            if (!String.IsNullOrEmpty(Properties.Settings.Default.patientName))
            {
                SaveDbR();
            }

            ErrorSolverR();

            Mouse.OverrideCursor = null;
        }

        private void BurnL_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            SetValuesL();
            BurnSetL();
            CheckMuteL();
            PlotResponseL();

            if (!String.IsNullOrEmpty(Properties.Settings.Default.patientName))
            {
                SaveDbL();
            }

            ErrorSolverL();

            Mouse.OverrideCursor = null;
        }

        private void AutofitR_Click(object sender, RoutedEventArgs e)
        {
            if (currentProgram == 0)
            {
                Mouse.OverrideCursor = Cursors.Wait;

                CleanMessages();
                SetValuesR();
                waveRule.FillTargetGains('R', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);
                DoAutofitR();
                GetValuesR();

                WriteSetR(true);
                PlotResponseR();

                CheckMuteR();
                ErrorSolverR();

                Mouse.OverrideCursor = null;
            }
            else
            {
                CleanMessages();
                HandyControl.Controls.Growl.WarningGlobal("O autofit deve ser feito no programa 1, caso deseje realizar autofit por favor voltar ao programa 1.");
            }
        }

        private void AutofitL_Click(object sender, RoutedEventArgs e)
        {
            if (currentProgram == 0)
            {
                Mouse.OverrideCursor = Cursors.Wait;

                CleanMessages();
                SetValuesL();
                waveRule.FillTargetGains('L', aidInBothSides, AdaptLevel.SelectedIndex, windDouble[WindLevel.SelectedIndex]);
                DoAutofitL();
                GetValuesL();

                WriteSetL(true);
                PlotResponseL();

                CheckMuteL();
                ErrorSolverL();

                Mouse.OverrideCursor = null;
            }
            else
            {
                CleanMessages();
                HandyControl.Controls.Growl.WarningGlobal("O autofit deve ser feito no programa 1, caso deseje realizar autofit por favor voltar ao programa 1.");
            }
        }

        private void NProgramR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            NProgramsR();
            BurnSetR();
            PopulateProgramR();
            CheckMuteR();
            ErrorSolverR();

            Mouse.OverrideCursor = null;
        }

        private void NProgramL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            NProgramsL();
            BurnSetL();
            PopulateProgramL();
            CheckMuteL();
            ErrorSolverL();

            Mouse.OverrideCursor = null;
        }

        private void ProgramR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            DetectR(true);
            GetValuesR();
            CheckMuteR();
            PlotResponseR();

            Mouse.OverrideCursor = null;
        }

        private void ProgramL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            DetectL(true);
            GetValuesL();
            CheckMuteL();
            PlotResponseL();

            Mouse.OverrideCursor = null;
        }

        private void ProgramCR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fromProgram = ProgramCR.SelectedIndex;
        }

        private void ProgramCL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fromProgram = ProgramCL.SelectedIndex;
        }

        private void CopyProgramR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            toProgram = CopyProgramR.SelectedIndex;
        }

        private void CopyProgramL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            toProgram = CopyProgramL.SelectedIndex;
        }

        private void CopyPR_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            SetProgramR(fromProgram);
            ReadGetR();
            GetValuesR();
            SetProgramR(toProgram);
            BurnSetR();

            SetProgramR(currentProgram);
            ReadGetR();
            GetValuesR();
            CheckMuteR();
            CleanMessages();

            HandyControl.Controls.Growl.SuccessGlobal($"Programa {fromProgram + 1} copiado para programa {toProgram + 1} com sucesso.");

            ErrorSolverR();

            Mouse.OverrideCursor = null;
        }

        private void CopyPL_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            SetProgramL(fromProgram);
            ReadGetL();
            GetValuesL();
            SetProgramL(toProgram);
            BurnSetL();

            SetProgramL(currentProgram);
            ReadGetL();
            GetValuesL();
            CheckMuteL();
            CleanMessages();

            HandyControl.Controls.Growl.SuccessGlobal($"Programa {fromProgram + 1} copiado para programa {toProgram + 1} com sucesso.");
            ErrorSolverL();

            Mouse.OverrideCursor = null;
        }

        private void ReadR_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            DetectR(true);
            GetValuesR();
            CheckMuteR();

            Mouse.OverrideCursor = null;
        }

        private void ReadL_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            DetectL(true);
            GetValuesL();
            CheckMuteL();

            Mouse.OverrideCursor = null;
        }

        private void GetR_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            DetectR(false);
            GetStandardR();
            GetValuesR();
            CheckMuteR();

            Mouse.OverrideCursor = null;
        }

        private void GetL_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            CleanMessages();
            DetectL(false);
            GetStandardL();
            GetValuesL();
            CheckMuteL();

            Mouse.OverrideCursor = null;
        }

        private void ToneButtonR_Click(object sender, RoutedEventArgs e)
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    ErrorSolverR();
                    audion16ViewModelR.TestAlert((short)(currentProgram + 1));
                    break;

                case "Audion8":
                    ErrorSolverR();
                    audion8ViewModelR.TestAlert((short)(currentProgram + 1));
                    break;

                case "Audion6":
                    ErrorSolverR();
                    audion6ViewModelR.TestAlert((short)(currentProgram + 1));
                    break;

                case "Audion4":
                    ErrorSolverR();
                    audion4ViewModelR.TestAlert((short)(currentProgram + 1));
                    break;

                case "SpinNR":
                    ErrorSolverR();
                    spinNRViewModelR.TestAlert((short)(currentProgram + 1));
                    break;

                case "Null":
                    break;
            }
        }

        private void ToneButtonL_Click(object sender, RoutedEventArgs e)
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    ErrorSolverL();
                    audion16ViewModelL.TestAlert((short)(currentProgram + 1));
                    break;

                case "Audion8":
                    ErrorSolverL();
                    audion8ViewModelL.TestAlert((short)(currentProgram + 1));
                    break;

                case "Audion6":
                    ErrorSolverL();
                    audion6ViewModelL.TestAlert((short)(currentProgram + 1));
                    break;

                case "Audion4":
                    ErrorSolverL();
                    audion4ViewModelL.TestAlert((short)(currentProgram + 1));
                    break;

                case "SpinNR":
                    ErrorSolverL();
                    spinNRViewModelL.TestAlert((short)(currentProgram + 1));
                    break;

                case "Null":
                    break;
            }
        }

        private void SearchPatient_Click(object sender, RoutedEventArgs e)
        {
            patientChanged = true;
            AudiogramSelectForm searchPatient = new AudiogramSelectForm();
            searchPatient.SelectButton.Click += SelectButton_Click; ;
            searchPatient.ShowDialog();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            PatientTextBox.Text = Properties.Settings.Default.patientName;
        }

        private void LAudiogramGraphButton_Click(object sender, RoutedEventArgs e)
        {
            AudiogramGraphL = true;
            CurrentGraphL.Content = new StaticAudiographUserControl('L', "Orelha Esquerda", true, ReceptorL.SelectedIndex);
        }

        private void RAudiogramGraphButton_Click(object sender, RoutedEventArgs e)
        {
            AudiogramGraphR = true;
            CurrentGraphR.Content = new StaticAudiographUserControl('R', "Orelha Direita", true, ReceptorR.SelectedIndex);
        }

        private void LGainGraphButton_Click(object sender, RoutedEventArgs e)
        {
            AudiogramGraphL = false;
            CurrentGraphL.Content = gainPlotUserControlL;
        }

        private void RGainGraphButton_Click(object sender, RoutedEventArgs e)
        {
            AudiogramGraphR = false;
            CurrentGraphR.Content = gainPlotUserControlR;
        }

        private void ExpanderHelp_Collapsed(object sender, RoutedEventArgs e)
        {
            HelpGrid.Visibility = Visibility.Collapsed;
        }

        private void ExpanderHelp_Expanded(object sender, RoutedEventArgs e)
        {
            HelpGrid.Visibility = Visibility.Visible;
        }

        private void ExpanderBurn_Collapsed(object sender, RoutedEventArgs e)
        {
            BurnGrid.Visibility = Visibility.Collapsed;
        }

        private void ExpanderBurn_Expanded(object sender, RoutedEventArgs e)
        {
            BurnGrid.Visibility = Visibility.Visible;
        }

        private void ExpanderMemory_Collapsed(object sender, RoutedEventArgs e)
        {
            GetGrid.Visibility = Visibility.Collapsed;
            ReadGrid.Visibility = Visibility.Collapsed;
        }

        private void ExpanderMemory_Expanded(object sender, RoutedEventArgs e)
        {
            GetGrid.Visibility = Visibility.Collapsed;
            ReadGrid.Visibility = Visibility.Visible;
        }

        private void ExpanderProgram_Expanded(object sender, RoutedEventArgs e)
        {
            CopyGrid.Visibility = Visibility.Visible;
            CurrentProgramGrid.Visibility = Visibility.Visible;
        }

        private void ExpanderProgram_Collapsed(object sender, RoutedEventArgs e)
        {
            CopyGrid.Visibility = Visibility.Collapsed;
            CurrentProgramGrid.Visibility = Visibility.Collapsed;
        }

        private void ExpanderAutofit_Collapsed(object sender, RoutedEventArgs e)
        {
            AutofitGrid.Visibility = Visibility.Collapsed;
            AdaptGrid.Visibility = Visibility.Collapsed;
            WindGrid.Visibility = Visibility.Collapsed;
        }

        private void ExpanderAutofit_Expanded(object sender, RoutedEventArgs e)
        {
            AutofitGrid.Visibility = Visibility.Visible;
            AdaptGrid.Visibility = Visibility.Visible;
            WindGrid.Visibility = Visibility.Visible;
        }

        private void ExpanderAparelho_Collapsed(object sender, RoutedEventArgs e)
        {
            NProgramsGrid.Visibility = Visibility.Collapsed;
            HINameGrid.Visibility = Visibility.Collapsed;
            ReceptorGrid.Visibility = Visibility.Collapsed;
            ButtonsGrid.Visibility = Visibility.Collapsed;
        }

        private void ExpanderAparelho_Expanded(object sender, RoutedEventArgs e)
        {
            NProgramsGrid.Visibility = Visibility.Visible;
            HINameGrid.Visibility = Visibility.Visible;
            ReceptorGrid.Visibility = Visibility.Visible;
            ButtonsGrid.Visibility = Visibility.Visible;
        }

        private void PatientAudiograms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (patientChanged == false)
            {
                for (int i = 0; i < PatientAudiograms.Items.Count; i++)
                {
                    if (GetDates()[i].ToString() == PatientAudiograms.SelectedItem.ToString())
                    {
                        crudViewModel.GetIdAudiogram(GetDates()[i]);
                        CheckAudiogram();
                    }
                }
            }
            if (Properties.Settings.Default.ChipIDR != "Null" && Properties.Settings.Default.ChipIDL != "Null")
            {
                PlotResponseR();
                PlotResponseL();
            }
            else if (Properties.Settings.Default.ChipIDR != "Null" && Properties.Settings.Default.ChipIDL == "Null")
            {
                PlotResponseR();
            }
            else if (Properties.Settings.Default.ChipIDR == "Null" && Properties.Settings.Default.ChipIDL != "Null")
            {
                PlotResponseL();
            }
        }

        public List<DateTime> GetDates()
        {
            List<DateTime> dates = new List<DateTime>();

            foreach (AudiogramModel item in dataFromDatabase)
            {
                if (item != null)
                {
                    dates.Add(item.Date);
                }
            }

            return dates;
        }

        private void PatientTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataFromDatabase = crudViewModel.LoadAudiogramDataFromDatabase(Properties.Settings.Default.patientId);
            patientChoosen = true;

            if (dataFromDatabase.Count == 0)
            {
                PatientAudiograms.IsEnabled = false;
                Properties.Settings.Default.audiogramId = 0;
                PatientAudiograms.ItemsSource = null;
                patientChanged = false;
            }
            else
            {
                PatientAudiograms.IsEnabled = true;
                PatientAudiograms.ItemsSource = GetDates();
                PatientAudiograms.SelectedItem = GetDates().Last();
                crudViewModel.GetIdAudiogram(GetDates().Last());
                CheckAudiogram();
                patientChanged = false;
            }
        }

        public void CheckAudiogram()
        {
            if (AudiogramGraphR == true && AudiogramGraphL == false)
            {
                CurrentGraphR.Content = new StaticAudiographUserControl('R', "Orelha Direita", true, ReceptorR.SelectedIndex);
            }
            else if (AudiogramGraphR == false && AudiogramGraphL == true)
            {
                CurrentGraphL.Content = new StaticAudiographUserControl('L', "Orelha Esquerda", true, ReceptorL.SelectedIndex);
            }
            else if (AudiogramGraphR == true && AudiogramGraphL == true)
            {
                CurrentGraphR.Content = new StaticAudiographUserControl('R', "Orelha Direita", true, ReceptorR.SelectedIndex);
                CurrentGraphL.Content = new StaticAudiographUserControl('L', "Orelha Esquerda", true, ReceptorL.SelectedIndex);
            }
        }

        public string HINamesR()
        {
            switch (Properties.Settings.Default.ChipIDR)
            {
                case "Audion16":
                    return "Landel";

                case "Audion8":
                    return "Dumont";

                case "Audion6":
                    return "Ada";

                case "Audion4":
                    return "Mauá";

                case "SpinNR":
                    return "Nise";

                default:
                    return "";
            }
        }

        public string HINamesL()
        {
            switch (Properties.Settings.Default.ChipIDL)
            {
                case "Audion16":
                    return "Landel";

                case "Audion8":
                    return "Dumont";

                case "Audion6":
                    return "Ada";

                case "Audion4":
                    return "Mauá";

                case "SpinNR":
                    return "Nise";

                default:
                    return "";
            }
        }

        public void LeftChipTools()
        {
            RHI.Text = "";
            LHI.Text = HINamesL() + "-" + GetSerialNumberL().ToString();

            AdjustMenuR.Content = null;
            AdjustRGrid.IsEnabled = false;

            BurnR.IsEnabled = false;
            ReadR.IsEnabled = false;
            GetR.IsEnabled = false;
            AutofitR.IsEnabled = false;
            NProgramR.IsEnabled = false;
            ProgramR.IsEnabled = false;
            ProgramCR.IsEnabled = false;
            CopyProgramR.IsEnabled = false;
            CopyPR.IsEnabled = false;
            DetectButtonR.IsEnabled = false;
            MuteButtonR.IsEnabled = false;
            SerialNumberR.IsEnabled = false;
            ReceptorR.IsEnabled = false;

            AdjustMenuL.Content = adjustTabUserControlL;
            AdjustLGrid.IsEnabled = true;

            BurnL.IsEnabled = true;
            ReadL.IsEnabled = true;
            GetL.IsEnabled = true;
            AutofitL.IsEnabled = true;
            NProgramL.IsEnabled = true;
            ProgramL.IsEnabled = true;
            ProgramCL.IsEnabled = true;
            CopyProgramL.IsEnabled = true;
            CopyPL.IsEnabled = true;
            DetectButtonL.IsEnabled = true;
            MuteButtonL.IsEnabled = true;
            SerialNumberL.IsEnabled = true;
            ReceptorL.IsEnabled = true;
        }

        public void RightChipTools()
        {
            RHI.Text = HINamesR() + "-" + GetSerialNumberR().ToString();
            LHI.Text = "";

            AdjustMenuR.Content = adjustTabUserControlR;
            AdjustRGrid.IsEnabled = true;

            BurnR.IsEnabled = true;
            ReadR.IsEnabled = true;
            GetR.IsEnabled = true;
            AutofitR.IsEnabled = true;
            NProgramR.IsEnabled = true;
            ProgramR.IsEnabled = true;
            ProgramCR.IsEnabled = true;
            CopyProgramR.IsEnabled = true;
            CopyPR.IsEnabled = true;
            DetectButtonR.IsEnabled = true;
            MuteButtonR.IsEnabled = true;
            SerialNumberR.IsEnabled = true;
            ReceptorR.IsEnabled = true;

            AdjustMenuL.Content = null;
            AdjustLGrid.IsEnabled = false;

            BurnL.IsEnabled = false;
            ReadL.IsEnabled = false;
            GetL.IsEnabled = false;
            AutofitL.IsEnabled = false;
            NProgramL.IsEnabled = false;
            ProgramL.IsEnabled = false;
            ProgramCL.IsEnabled = false;
            CopyProgramL.IsEnabled = false;
            CopyPL.IsEnabled = false;
            DetectButtonL.IsEnabled = false;
            MuteButtonL.IsEnabled = false;
            SerialNumberL.IsEnabled = false;
            ReceptorL.IsEnabled = false;
        }

        public void ChipTools()
        {
            LHI.Text = HINamesL() + "-" + GetSerialNumberL().ToString();
            RHI.Text = HINamesR() + "-" + GetSerialNumberR().ToString();

            AdjustMenuL.Content = adjustTabUserControlL;
            AdjustLGrid.IsEnabled = false;

            BurnL.IsEnabled = false;
            ReadL.IsEnabled = false;
            GetL.IsEnabled = false;
            AutofitL.IsEnabled = false;
            NProgramL.IsEnabled = false;
            ProgramL.IsEnabled = false;
            ProgramCL.IsEnabled = false;
            CopyProgramL.IsEnabled = false;
            CopyPL.IsEnabled = false;
            DetectButtonL.IsEnabled = true;
            MuteButtonL.IsEnabled = false;
            SerialNumberL.IsEnabled = false;
            ReceptorL.IsEnabled = false;

            AdjustMenuR.Content = adjustTabUserControlR;
            AdjustRGrid.IsEnabled = true;

            BurnR.IsEnabled = true;
            ReadR.IsEnabled = true;
            GetR.IsEnabled = true;
            AutofitR.IsEnabled = true;
            NProgramR.IsEnabled = true;
            ProgramR.IsEnabled = true;
            ProgramCR.IsEnabled = true;
            CopyProgramR.IsEnabled = true;
            CopyPR.IsEnabled = true;
            DetectButtonR.IsEnabled = true;
            MuteButtonR.IsEnabled = true;
            SerialNumberR.IsEnabled = true;
            ReceptorR.IsEnabled = true;
        }

        public void EnableRight()
        {
            AdjustRGrid.IsEnabled = true;
            BurnR.IsEnabled = true;
            ReadR.IsEnabled = true;
            GetR.IsEnabled = true;
            AutofitR.IsEnabled = true;
            NProgramR.IsEnabled = true;
            ProgramR.IsEnabled = true;
            ProgramCR.IsEnabled = true;
            CopyProgramR.IsEnabled = true;
            CopyPR.IsEnabled = true;
            MuteButtonR.IsEnabled = true;
            SerialNumberR.IsEnabled = true;
            ReceptorR.IsEnabled = true;

            AdjustLGrid.IsEnabled = false;
            BurnL.IsEnabled = false;
            ReadL.IsEnabled = false;
            GetL.IsEnabled = false;
            AutofitL.IsEnabled = false;
            NProgramL.IsEnabled = false;
            ProgramL.IsEnabled = false;
            ProgramCL.IsEnabled = false;
            CopyProgramL.IsEnabled = false;
            CopyPL.IsEnabled = false;
            MuteButtonL.IsEnabled = false;
            SerialNumberL.IsEnabled = false;
            ReceptorL.IsEnabled = false;
        }

        public void EnableLeft()
        {
            AdjustRGrid.IsEnabled = false;
            BurnR.IsEnabled = false;
            ReadR.IsEnabled = false;
            GetR.IsEnabled = false;
            AutofitR.IsEnabled = false;
            NProgramR.IsEnabled = false;
            ProgramR.IsEnabled = false;
            ProgramCR.IsEnabled = false;
            CopyProgramR.IsEnabled = false;
            CopyPR.IsEnabled = false;
            MuteButtonR.IsEnabled = false;
            SerialNumberR.IsEnabled = false;
            ReceptorR.IsEnabled = false;

            AdjustLGrid.IsEnabled = true;
            BurnL.IsEnabled = true;
            ReadL.IsEnabled = true;
            GetL.IsEnabled = true;
            AutofitL.IsEnabled = true;
            NProgramL.IsEnabled = true;
            ProgramL.IsEnabled = true;
            ProgramCL.IsEnabled = true;
            CopyProgramL.IsEnabled = true;
            CopyPL.IsEnabled = true;
            MuteButtonL.IsEnabled = true;
            SerialNumberL.IsEnabled = true;
            ReceptorL.IsEnabled = true;
        }

        public void CleanMessages()
        {
            HandyControl.Controls.Growl.Clear();
            HandyControl.Controls.Growl.ClearGlobal();
        }
    }
}