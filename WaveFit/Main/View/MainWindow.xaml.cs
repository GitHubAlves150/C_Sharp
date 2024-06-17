using HandyControl.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using WaveFit2.Audiogram.View;
using WaveFit2.Calibration.View;
using WaveFit2.Database.CRUD;
using WaveFit2.Home.View;
using WaveFit2.Login.View;
using WaveFit2.Patient.View;
using WaveFit2.Records.View;
using WaveFit2.Settings.View;

namespace WaveFit2
{
    public partial class MainWindow : System.Windows.Window
    {
        public Crud crud = new Crud();
        public PatientUserControl patientUserControl = new PatientUserControl();
        public SettingsUserControl settingsUserControl = new SettingsUserControl();
        public HomeUserControl homeUserControl = new HomeUserControl();
        public RecordUserControl recordUserControl;
        private Random random = new Random();

        public int stepCounter = -1;

        public MainWindow()
        {
            InitializeComponent();
            Reset();
            AdjustWindow();
            WindowButtons();
            SideBarButtons();
            SettingButtons();
            SetUserImage();
            Start();
        }

        public void Start()
        {
            HomeSelected();
            NameUser.Text = Properties.Settings.Default.userName;
            CurrentPageText.Text = "Início";
            homeUserControl.StartComponents();

            homeUserControl.MakeAudiogram.Click += MakeAudiogram_Click;
            homeUserControl.AdjustButton.Click += AdjustButton_Click;
            homeUserControl.BackupButton.Click += BackupButton_Click;
            CurrentWindow.Content = homeUserControl;
        }

        public void SettingButtons()
        {
            settingsUserControl.editProfileUserControl.registerUserControl.SaveButton.Click += SaveButton_Click1;
        }

        private void SaveButton_Click1(object sender, RoutedEventArgs e)
        {
            SetUserImage();
        }

        public void HomeSelected()
        {
            HomeItem.IsSelected = true;
            HomeItem.IsEnabled = true;

            PatientItem.IsSelected = false;
            PatientItem.IsEnabled = true;

            RecordsItem.IsSelected = false;
            RecordsItem.IsEnabled = true;

            AudiogramItem.IsSelected = false;
            AudiogramItem.IsEnabled = true;

            CalibrationItem.IsSelected = false;
            CalibrationItem.IsEnabled = true;

            SettingsItem.IsSelected = false;
            SettingsItem.IsEnabled = true;
        }

        public void PatientSelected()
        {
            HomeItem.IsSelected = false;
            HomeItem.IsEnabled = true;

            PatientItem.IsSelected = true;
            PatientItem.IsEnabled = false;

            RecordsItem.IsSelected = false;
            RecordsItem.IsEnabled = true;

            AudiogramItem.IsSelected = false;
            AudiogramItem.IsEnabled = true;

            CalibrationItem.IsSelected = false;
            CalibrationItem.IsEnabled = true;

            SettingsItem.IsSelected = false;
            SettingsItem.IsEnabled = true;
        }

        public void RecordsSelected()
        {
            HomeItem.IsSelected = false;
            HomeItem.IsEnabled = true;

            PatientItem.IsSelected = false;
            PatientItem.IsEnabled = true;

            RecordsItem.IsSelected = true;
            RecordsItem.IsEnabled = false;

            AudiogramItem.IsSelected = false;
            AudiogramItem.IsEnabled = true;

            CalibrationItem.IsSelected = false;
            CalibrationItem.IsEnabled = true;

            SettingsItem.IsSelected = false;
            SettingsItem.IsEnabled = true;
        }

        public void AudiogramSelected()
        {
            HomeItem.IsSelected = false;
            HomeItem.IsEnabled = true;

            PatientItem.IsSelected = false;
            PatientItem.IsEnabled = true;

            RecordsItem.IsSelected = false;
            RecordsItem.IsEnabled = true;

            AudiogramItem.IsSelected = true;
            AudiogramItem.IsEnabled = false;

            CalibrationItem.IsSelected = false;
            CalibrationItem.IsEnabled = true;

            SettingsItem.IsSelected = false;
            SettingsItem.IsEnabled = true;
        }

        public void CalibrationSelected()
        {
            HomeItem.IsSelected = false;
            HomeItem.IsEnabled = true;

            PatientItem.IsSelected = false;
            PatientItem.IsEnabled = true;

            RecordsItem.IsSelected = false;
            RecordsItem.IsEnabled = true;

            AudiogramItem.IsSelected = false;
            AudiogramItem.IsEnabled = true;

            CalibrationItem.IsSelected = true;
            CalibrationItem.IsEnabled = false;

            SettingsItem.IsSelected = false;
            SettingsItem.IsEnabled = true;
        }

        public void SettingsSelected()
        {
            HomeItem.IsSelected = false;
            HomeItem.IsEnabled = true;

            PatientItem.IsSelected = false;
            PatientItem.IsEnabled = true;

            RecordsItem.IsSelected = false;
            RecordsItem.IsEnabled = true;

            AudiogramItem.IsSelected = false;
            AudiogramItem.IsEnabled = true;

            CalibrationItem.IsSelected = false;
            CalibrationItem.IsEnabled = true;

            SettingsItem.IsSelected = true;
            SettingsItem.IsEnabled = false;
        }

        public void AdjustWindow()
        {
            var workArea = SystemParameters.WorkArea;

            var currentMonitor = Screen.FromHandle(new WindowInteropHelper(this).Handle);

            double monitorWidth = currentMonitor.WorkingArea.Width;
            double monitorHeight = currentMonitor.WorkingArea.Height;

            monitorWidth = Math.Min(monitorWidth, workArea.Width);
            monitorHeight = Math.Min(monitorHeight, workArea.Height);

            this.Width = monitorWidth;
            this.Height = monitorHeight;

            this.Left = currentMonitor.WorkingArea.Left;
            this.Top = currentMonitor.WorkingArea.Top;
        }

        public void WindowButtons()
        {
            ClearButton.Click += ClearButton_Click; ;
            CloseButton.Click += CloseButton_Click;
            MaximizeButton.Click += MaximizeButton_Click;
            MinimizeButton.Click += MinimizeButton_Click;
        }

        public void SideBarButtons()
        {
            HomeItem.MouseDown += HomeItem_Selected;
            PatientItem.MouseDown += PatientItem_Selected;
            RecordsItem.MouseDown += RecordsItem_Selected;
            AudiogramItem.MouseDown += AudiogramItem_Selected;
            CalibrationItem.MouseDown += CalibrationItem_Selected;
            SettingsItem.MouseDown += SettingsItem_Selected;
            LogoutItem.MouseDown += LogoutItem_Selected;

            HomeItem.MouseEnter += SideItem_MouseEnter;
            PatientItem.MouseEnter += SideItem_MouseEnter;
            RecordsItem.MouseEnter += SideItem_MouseEnter;
            AudiogramItem.MouseEnter += SideItem_MouseEnter;
            CalibrationItem.MouseEnter += SideItem_MouseEnter;
            SettingsItem.MouseEnter += SideItem_MouseEnter;
            LogoutItem.MouseEnter += SideItem_MouseEnter;

            HomeItem.MouseLeave += SideItem_MouseLeave;
            PatientItem.MouseLeave += SideItem_MouseLeave;
            RecordsItem.MouseLeave += SideItem_MouseLeave;
            AudiogramItem.MouseLeave += SideItem_MouseLeave;
            CalibrationItem.MouseLeave += SideItem_MouseLeave;
            SettingsItem.MouseLeave += SideItem_MouseLeave;
            LogoutItem.MouseLeave += SideItem_MouseLeave;

            MenuButton.Checked += MenuButton_Checked;
            MenuButton.Unchecked += MenuButton_Unchecked; ;
        }

        private void MenuButton_Unchecked(object sender, RoutedEventArgs e)
        {
            NameUser.Visibility = Visibility.Hidden;
        }

        private void MenuButton_Checked(object sender, RoutedEventArgs e)
        {
            NameUser.Visibility = Visibility.Visible;
        }

        private void SideItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateColor(sender, System.Windows.Media.Brushes.Teal);
        }

        private void SideItem_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateColor(sender, System.Windows.Media.Brushes.White);
        }

        private void UpdateColor(object sender, System.Windows.Media.Brush color)
        {
            var item = sender as SideMenuItem;

            if (item != null)
            {
                item.Foreground = color;

                var icon = item.Icon as PackIcon;

                if (icon != null)
                {
                    icon.Foreground = color;
                }
            }
        }

        private void LogoutItem_Selected(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void SettingsItem_Selected(object sender, RoutedEventArgs e)
        {
            Reset();
            SettingsSelected();
            CurrentPageText.Text = "Configuração";
            CurrentWindow.Content = settingsUserControl;
        }

        private void CalibrationItem_Selected(object sender, RoutedEventArgs e)
        {
            Reset();
            CalibrationSelected();
            DetectHIWindow detectWindow = new DetectHIWindow();
            detectWindow.CurrentStep.StepChanged += CurrentStep_StepChanged;
            detectWindow.Closed += DetectWindow_Closed;
            detectWindow.ShowDialog();
        }

        private void CurrentStep_StepChanged(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            stepCounter = stepCounter + 1;
            Console.WriteLine(stepCounter.ToString());
            if (stepCounter == 6)
            {
                Console.WriteLine(Properties.Settings.Default.continuedToCalibrarion.ToString());
                if (Properties.Settings.Default.continuedToCalibrarion == false)
                {
                    HomeSelected();
                    CurrentPageText.Text = "Início";
                    CurrentWindow.Content = homeUserControl;
                }
                else
                {
                    CalibrationUserControl calibrationUserControl = new CalibrationUserControl();
                    CurrentPageText.Text = "Ajuste";
                    CurrentWindow.Content = calibrationUserControl;
                }
            }
        }

        private void DetectWindow_Closed(object sender, EventArgs e)
        {
            if (stepCounter != 6)
            {
                HomeSelected();
                CurrentPageText.Text = "Início";
                CurrentWindow.Content = homeUserControl;
            }
            stepCounter = -1;
        }

        private void AudiogramItem_Selected(object sender, RoutedEventArgs e)
        {
            Reset();
            AudiogramSelected();
            AudiogramUserControl audiogramUserControl = new AudiogramUserControl();
            CurrentPageText.Text = "Avaliações";
            audiogramUserControl.AudiogramInvisible();
            CurrentWindow.Content = audiogramUserControl;
        }

        private void RecordsItem_Selected(object sender, RoutedEventArgs e)
        {
            Reset();
            RecordsSelected();
            recordUserControl = new RecordUserControl();
            CurrentPageText.Text = "Histórico";
            recordUserControl.ResetAudiogramRecords();
            CurrentWindow.Content = recordUserControl;
        }

        private void PatientItem_Selected(object sender, RoutedEventArgs e)
        {
            Reset();
            PatientSelected();
            patientUserControl.LoadData();
            CurrentPageText.Text = "Paciente";
            CurrentWindow.Content = patientUserControl;
        }

        private void HomeItem_Selected(object sender, RoutedEventArgs e)
        {
            Reset();
            HomeSelected();
            CurrentPageText.Text = "Início";
            homeUserControl.StartComponents();
            CurrentWindow.Content = homeUserControl;
        }

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            recordUserControl = new RecordUserControl();
            CurrentPageText.Text = "Histórico";
            recordUserControl.ResetAudiogramRecords();
            recordUserControl.CurrentPatient.Text = Properties.Settings.Default.patientName;
            recordUserControl.TabControlRecords.SelectedItem = recordUserControl.TabCalibration;
            recordUserControl.FindPatientButton.Visibility = Visibility.Collapsed;
            CurrentWindow.Content = recordUserControl;
        }

        private void AdjustButton_Click(object sender, RoutedEventArgs e)
        {
            DetectHIWindow detectWindowHome = new DetectHIWindow();
            detectWindowHome.CurrentStep.StepChanged += CurrentStepHome_StepChanged;
            detectWindowHome.Closed += DetectWindow_Closed;
            detectWindowHome.ShowDialog();
        }

        private void CurrentStepHome_StepChanged(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            stepCounter = stepCounter + 1;
            Console.WriteLine(stepCounter.ToString());
            if (stepCounter == 6)
            {
                Console.WriteLine(Properties.Settings.Default.continuedToCalibrarion.ToString());
                if (Properties.Settings.Default.continuedToCalibrarion == false)
                {
                    HomeSelected();
                    CurrentPageText.Text = "Início";
                    CurrentWindow.Content = homeUserControl;
                }
                else
                {
                    CalibrationUserControl calibrationUserControl = new CalibrationUserControl();
                    CurrentPageText.Text = "Ajuste";
                    CurrentWindow.Content = calibrationUserControl;
                    calibrationUserControl.PatientTextBox.Text = Properties.Settings.Default.patientName;
                    calibrationUserControl.SearchPatient.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void MakeAudiogram_Click(object sender, RoutedEventArgs e)
        {
            AudiogramUserControl audiogramUserControl = new AudiogramUserControl();
            audiogramUserControl.SelectedValues();
            audiogramUserControl.ResetAll();
            audiogramUserControl.CurrentStep.StepIndex = 1;
            CurrentWindow.Content = audiogramUserControl;
            audiogramUserControl.SaveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            homeUserControl.UptadeAudiograms();
            CurrentWindow.Content = homeUserControl;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            HandyControl.Controls.Growl.Clear();
            HandyControl.Controls.Growl.ClearGlobal();
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Windows.Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindow();
        }

        private BitmapImage ConvertBytesToImage(byte[] imageBytes)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }

        private void SetUserImage()
        {
            try
            {
                string[] Avatars = {
                @"Resources/Profiles/arctic-fox.png",
                @"Resources/Profiles/bear.png",
                @"Resources/Profiles/cat.png",
                @"Resources/Profiles/chicken.png",
                @"Resources/Profiles/dog.png",
                @"Resources/Profiles/koala.png",
                @"Resources/Profiles/panda.png",
                @"Resources/Profiles/sea-lion.png",
                @"Resources/Profiles/sloth.png"
            };

                if (Properties.Settings.Default.imageUser == null)
                {
                    string imagePath = Avatars[random.Next(0, 8)];
                    profilePic.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                    settingsUserControl.editProfileUserControl.profilePic.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    byte[] imageBytes = Convert.FromBase64String(Properties.Settings.Default.imageUser);
                    BitmapImage bitmapImage = ConvertBytesToImage(imageBytes);
                    profilePic.ImageSource = bitmapImage;
                    settingsUserControl.editProfileUserControl.profilePic.ImageSource = bitmapImage;
                }
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Show(ex.Message);
            }
        }

        public void Reset()
        {
            Properties.Settings.Default.audiogramId = 0;
            Properties.Settings.Default.ChipIDR = "";
            Properties.Settings.Default.ChipIDL = "";
            Properties.Settings.Default.patientCode = 0;
            Properties.Settings.Default.patientId = 0;
            Properties.Settings.Default.patientName = "";
            Properties.Settings.Default.continuedToCalibrarion = false;
        }
    }
}