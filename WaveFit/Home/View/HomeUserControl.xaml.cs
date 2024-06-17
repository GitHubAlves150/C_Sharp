using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WaveFit2.Audiogram.View;
using WaveFit2.Calibration.Class;
using WaveFit2.Calibration.ViewModel;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Patient.View;

namespace WaveFit2.Home.View
{
    public partial class HomeUserControl : UserControl
    {
        public Crud crudViewModel = new Crud();
        public List<AudiogramModel> dataFromDatabase = new List<AudiogramModel>();
        public bool patientChanged = false;
        public WaveRule waveRule = new WaveRule();
        public Audion8ViewModel audion8ViewModel = new Audion8ViewModel();
        public bool BackupEnableR = false;
        public bool BackupEnableL = false;

        public HomeUserControl()
        {
            InitializeComponent();
            StartComponents();
            HomeTools();
            DataContext = this;
        }

        public void StartComponents()
        {
            PatientFilter.Text = Properties.Settings.Default.patientName;
            AdjustGrid.IsEnabled = false;
            AudiogramGrid.IsEnabled = false;
        }

        public void EnableComponents()
        {
            AdjustGrid.IsEnabled = true;
            AudiogramGrid.IsEnabled = true;
        }

        public void NoAudiogramsEnable()
        {
            PatientAudiograms.IsEnabled = false;
            BackupButton.IsEnabled = false;
            AdjustButton.IsEnabled = false;
        }

        public void AudiogramsEnable()
        {
            PatientAudiograms.IsEnabled = true;
            AdjustButton.IsEnabled = true;

            if (BackupEnableR == false && BackupEnableL == false)
            {
                BackupButton.IsEnabled = false;
            }
            else
            {
                BackupButton.IsEnabled = true;
            }
        }

        public void HomeTools()
        {
            SearchPatient.Click += SearchPatient_Click;
            CreatePatient.Click += CreatePatient_Click;
            PatientFilter.TextChanged += PatientFilter_TextChanged;
            PatientAudiograms.SelectionChanged += PatientAudiograms_SelectionChanged;
        }

        private void SimulateAudiogram_Click(object sender, RoutedEventArgs e)
        {
            HandyControl.Controls.MessageBox.Show(Directory.GetCurrentDirectory().ToString());
            HandyControl.Controls.MessageBox.Show(Environment.CurrentDirectory);
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
                        Audiogram.Content = new StaticAudiogramUserControl();
                    }
                }
            }
        }

        public void FindDevice(char ear)
        {
            if (ear == 'R')
            {
                HINameR.Text = "";
                GetImagem(ear, "");
                foreach (var device in crudViewModel.GetAtributeListIntById("idhearingaid", "dbo.fitting", "idpatient", Properties.Settings.Default.patientId))
                {
                    if (crudViewModel.GetAtributeStrings("channel", "dbo.fitting", "idhearingaid", device).Contains("R"))
                    {
                        string deviceName = crudViewModel.GetAtributeString("device", "dbo.hearingaid", "id", device);
                        string mappedName = HINamesR(deviceName);

                        HINameR.Text = mappedName + " - " + crudViewModel.GetAtributeLong("serialnumber", "dbo.hearingaid", "id", device);
                        GetImagem(ear, mappedName);
                    }
                }
            }
            else
            {
                HINameL.Text = "";
                GetImagem(ear, "");
                foreach (var device in crudViewModel.GetAtributeListIntById("idhearingaid", "dbo.fitting", "idpatient", Properties.Settings.Default.patientId))
                {
                    if (crudViewModel.GetAtributeStrings("channel", "dbo.fitting", "idhearingaid", device).Contains("L"))
                    {
                        string deviceName = crudViewModel.GetAtributeString("device", "dbo.hearingaid", "id", device);
                        string mappedName = HINamesL(deviceName);

                        HINameL.Text = mappedName + " - " + crudViewModel.GetAtributeLong("serialnumber", "dbo.hearingaid", "id", device);
                        GetImagem(ear, mappedName);
                    }
                }
            }
        }

        public void GetImagem(char ear, string device)
        {
            if (ear == 'R')
            {
                switch (device)
                {
                    case "Landel":
                        ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/LANDELL.png", UriKind.RelativeOrAbsolute));
                        BackupEnableR = true;
                        break;

                    case "Dumont":
                        ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/DUMONT.png", UriKind.RelativeOrAbsolute));
                        BackupEnableR = true;
                        break;

                    case "Ada":
                        ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        BackupEnableR = true;
                        break;

                    case "Mauá":
                        ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/MAUÁ.png", UriKind.RelativeOrAbsolute));
                        BackupEnableR = true;
                        break;

                    case "Nise":
                        ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        BackupEnableR = true;
                        break;

                    default:
                        ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        BackupEnableR = false;
                        break;
                }
            }
            else if (ear == 'L')
            {
                switch (device)
                {
                    case "Landel":
                        ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/LANDELL.png", UriKind.RelativeOrAbsolute));
                        BackupEnableL = true;
                        break;

                    case "Dumont":
                        ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/DUMONT.png", UriKind.RelativeOrAbsolute));
                        BackupEnableL = true;
                        break;

                    case "Ada":
                        ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        BackupEnableL = true;
                        break;

                    case "Mauá":
                        ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/MAUÁ.png", UriKind.RelativeOrAbsolute));
                        BackupEnableL = true;
                        break;

                    case "Nise":
                        ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        BackupEnableL = true;
                        break;

                    default:
                        ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        BackupEnableL = false;
                        break;
                }
            }
        }

        public string HINamesR(string deviceName)
        {
            switch (deviceName)
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
                    return deviceName; // Retorna o nome do dispositivo se não for encontrado no mapeamento
            }
        }

        public string HINamesL(string deviceName)
        {
            switch (deviceName)
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
                    return deviceName; // Retorna o nome do dispositivo se não for encontrado no mapeamento
            }
        }

        private void PatientFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                EnableComponents();
                dataFromDatabase = crudViewModel.LoadAudiogramDataFromDatabase(Properties.Settings.Default.patientId);
                FindDevice('R');
                FindDevice('L');

                if (dataFromDatabase.Count == 0)
                {
                    NoAudiogramsEnable();
                    Properties.Settings.Default.audiogramId = 0;
                    PatientAudiograms.ItemsSource = null;
                    Audiogram.Content = null;
                    patientChanged = false;
                }
                else
                {
                    AudiogramsEnable();
                    PatientAudiograms.ItemsSource = GetDates();
                    PatientAudiograms.SelectedItem = GetDates().Last();
                    crudViewModel.GetIdAudiogram(GetDates().Last());
                    Audiogram.Content = new StaticAudiogramUserControl();
                    patientChanged = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UptadeAudiograms()
        {
            dataFromDatabase = crudViewModel.LoadAudiogramDataFromDatabase(Properties.Settings.Default.patientId);

            if (dataFromDatabase.Count == 0)
            {
                NoAudiogramsEnable();
                Properties.Settings.Default.audiogramId = 0;
                PatientAudiograms.ItemsSource = null;
                Audiogram.Content = null;
                patientChanged = false;
            }
            else
            {
                AudiogramsEnable();
                PatientAudiograms.ItemsSource = GetDates();
                PatientAudiograms.SelectedItem = GetDates().Last();
                crudViewModel.GetIdAudiogram(GetDates().Last());
                Audiogram.Content = new StaticAudiogramUserControl();
                patientChanged = false;
            }
        }

        private void CreatePatient_Click(object sender, RoutedEventArgs e)
        {
            PatientForm patientForm = new PatientForm();
            patientForm.RegisterButton.Click += RegisterButton_Click;
            patientForm.ShowDialog();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            PatientFilter.Text = Properties.Settings.Default.patientName;
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

        private void SearchPatient_Click(object sender, RoutedEventArgs e)
        {
            patientChanged = true;
            AudiogramSelectForm searchPatient = new AudiogramSelectForm();
            searchPatient.SelectButton.Click += SelectButton_Click;
            searchPatient.ShowDialog();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            PatientFilter.Text = Properties.Settings.Default.patientName;
        }
    }
}