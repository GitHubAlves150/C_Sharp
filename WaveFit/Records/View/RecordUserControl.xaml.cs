using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WaveFit2.Audiogram.View;
using WaveFit2.Calibration.ViewModel;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Records.View
{
    /// <summary>
    /// Interação lógica para RecordUserControl.xam
    /// </summary>
    public partial class RecordUserControl : UserControl
    {
        public IntriconViewModel intriconViewModel = new IntriconViewModel();

        public RecordAudiogramUserControl recordAudiogramUserControl = new RecordAudiogramUserControl();
        public RecordCalibrationUserControl recordCalibrationUserControl = new RecordCalibrationUserControl();

        public Audion16ViewModel audion16ViewModelR = new Audion16ViewModel();
        public Audion8ViewModel audion8ViewModelR = new Audion8ViewModel();
        public Audion6ViewModel audion6ViewModelR = new Audion6ViewModel();
        public Audion4ViewModel audion4ViewModelR = new Audion4ViewModel();
        public SpinNRViewModel spinNRViewModelR = new SpinNRViewModel();

        public Audion16ViewModel audion16ViewModelL = new Audion16ViewModel();
        public Audion8ViewModel audion8ViewModelL = new Audion8ViewModel();
        public Audion6ViewModel audion6ViewModelL = new Audion6ViewModel();
        public Audion4ViewModel audion4ViewModelL = new Audion4ViewModel();
        public SpinNRViewModel spinNRViewModelL = new SpinNRViewModel();

        public Crud crud = new Crud();
        public List<String> DeviceR = new List<String>();
        public List<String> DeviceL = new List<String>();

        public bool connected = false;
        public bool ShowHIR = true;
        public bool ShowHIL = true;

        public RecordUserControl()
        {
            InitializeComponent();
            CurrentPatient.IsEnabled = false;
            CurrentContent.Visibility = Visibility.Visible;
            CurrentContentImage.Visibility = Visibility.Visible;
            RecordToolActions();
        }

        public void RecordToolActions()
        {
            TabControlRecords.SelectionChanged += TabControlRecords_SelectionChanged;
            FindPatientButton.Click += FindPatientButton_Click;

            CurrentPatient.TextChanged += CurrentPatient_TextChanged;

            recordAudiogramUserControl.AudiogramViewClicked += RecordAudiogramUserControl_AudiogramViewClicked;
            recordAudiogramUserControl.AudiogramPrintClicked += RecordAudiogramUserControl_AudiogramPrintClicked;
            recordAudiogramUserControl.AudiographViewClicked += RecordAudiogramUserControl_AudiographViewClicked;

            recordCalibrationUserControl.FittinRClicked += RecordCalibrationUserControl_FittingRClicked;
            recordCalibrationUserControl.FittinLClicked += RecordCalibrationUserControl_FittingLClicked;

            recordCalibrationUserControl.ViewPlotRClicked += RecordCalibrationUserControl_ViewPlotRClicked;
            recordCalibrationUserControl.ViewPlotLClicked += RecordCalibrationUserControl_ViewPlotLClicked;

            recordCalibrationUserControl.TextHearingAidR.SelectionChanged += TextHearingAidR_SelectionChanged;
            recordCalibrationUserControl.TextHearingAidL.SelectionChanged += TextHearingAidL_SelectionChanged;

            recordCalibrationUserControl.DateRProgram.SelectionChanged += DateRProgram_SelectionChanged; ;
            recordCalibrationUserControl.DateLProgram.SelectionChanged += DateLProgram_SelectionChanged;
        }

        private void TabControlRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                TabItem selectedTabItem = e.AddedItems[0] as TabItem;
                if (selectedTabItem != null)
                {
                    string tabName = selectedTabItem.Name;
                    switch (tabName)
                    {
                        case "TabAudiogram":
                            TabAudiogram.Content = recordAudiogramUserControl;
                            CurrentContent.Content = recordAudiogramUserControl;
                            ContentGrid.Visibility = Visibility.Visible;
                            column1.Width = new GridLength(1, GridUnitType.Auto);
                            column2.Width = new GridLength(1, GridUnitType.Star);
                            BitmapImage imageSource = new BitmapImage(new Uri("/Resources/Audiogram.png", UriKind.Relative));
                            CurrentContentImage.Source = imageSource;
                            ResetAudiogramRecords();
                            break;

                        case "TabCalibration":
                            ContentGrid.Visibility = Visibility.Collapsed;
                            TabCalibration.Content = recordCalibrationUserControl;
                            column1.Width = new GridLength(1, GridUnitType.Star);
                            column2.Width = new GridLength(1, GridUnitType.Auto);

                            break;
                    }
                }
            }
        }

        private void FindPatientButton_Click(object sender, RoutedEventArgs e)
        {
            AudiogramSelectForm audiogramSelectForm = new AudiogramSelectForm();
            audiogramSelectForm.SelectButton.Click += AudiogramSelectFormSelectButton_Click; ;
            audiogramSelectForm.ShowDialog();
        }

        private void AudiogramSelectFormSelectButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPatient.Text = Properties.Settings.Default.patientName;
        }

        private void CurrentPatient_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResetAudiogramRecords();

            recordAudiogramUserControl.recordAudiogramViewModel.LoadAudiogramToDatagrid(recordAudiogramUserControl.DatagridAudiograms, Properties.Settings.Default.patientId);
            recordAudiogramUserControl.DatagridAudiograms.DataContext = recordAudiogramUserControl.recordAudiogramViewModel;

            FillDeviceComboBox('R');
            FillDateComboBox('R');

            if (recordCalibrationUserControl.TextHearingAidR.SelectedIndex >= 0)
            {
                LoadDataR(crud.GetRecordDates(Properties.Settings.Default.patientId, GetIdFromComboBox('R'), "R")[0]);
                recordCalibrationUserControl.DateRProgram.SelectedIndex = 0;
            }
            else
            {
                LoadDataR(crud.GetRecordDate(Properties.Settings.Default.patientId, GetIdFromComboBox('R'), "R"));
            }

            FillDeviceComboBox('L');
            FillDateComboBox('L');

            if (recordCalibrationUserControl.TextHearingAidL.SelectedIndex >= 0)
            {
                LoadDataL(crud.GetRecordDates(Properties.Settings.Default.patientId, GetIdFromComboBox('L'), "L")[0]);
                recordCalibrationUserControl.DateLProgram.SelectedIndex = 0;
            }
            else
            {
                LoadDataL(crud.GetRecordDate(Properties.Settings.Default.patientId, GetIdFromComboBox('L'), "L"));
            }

            GetImagem('R', GetDeviceFromComboBox('R'));
            GetImagem('L', GetDeviceFromComboBox('L'));
        }

        #region Audiogram

        private void RecordAudiogramUserControl_AudiographViewClicked(object sender, EventArgs e)
        {
            CurrentContent.Content = new StaticAudiogramUserControl();
            CurrentContent.Visibility = Visibility.Visible;
            CurrentContentImage.Visibility = Visibility.Collapsed;
        }

        private void RecordAudiogramUserControl_AudiogramViewClicked(object sender, EventArgs e)
        {
            PrintTemplateUserControl printTemplateUserControl = new PrintTemplateUserControl(280, 330);
            FillPrintTemplate(printTemplateUserControl);
            ScrollViewer printScrollViewer = new ScrollViewer();
            printScrollViewer.Content = printTemplateUserControl;
            printScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            printScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            printScrollViewer.CanContentScroll = true;
            CurrentContent.Content = printScrollViewer;
            CurrentContent.Visibility = Visibility.Visible;
            CurrentContentImage.Visibility = Visibility.Collapsed;
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

        public void FillPrintTemplate(PrintTemplateUserControl print)
        {
            foreach (PatientModel patient in crud.GetPatient(Properties.Settings.Default.patientCode))
            {
                print.PatientName.Text = $"{patient.Name} {patient.Surname}";
                DateTime today = DateTime.Today;
                int age = today.Year - patient.Birthday.Year;
                if (patient.Birthday > today.AddYears(-age)) age--;
                print.PatientAge.Text = age.ToString();
                print.PatientGender.Text = patient.Gender.ToString();
                print.PatientID.Text = $"{patient.NumDocument} ({patient.TypeDocument})";
            }

            foreach (AudiogramModel audiogram in crud.GetAudiogramDate(Properties.Settings.Default.audiogramId))
            {
                print.ExamDate.Text = audiogram.Date.ToString("dd/MM/yyyy");
            }

            foreach (HealthCenterModel healthCenter in crud.GetHealthCenter(crud.GetAtributeInt("idhealthcenter", "dbo.session", "idaudiogram", Properties.Settings.Default.audiogramId)))
            {
                print.NameTextBox.Text = $"{healthCenter.Name}";
                print.AddressTextBox.Text = $"{crud.GetAtributeString("address", "dbo.place", "id", healthCenter.IdPlace)}, {crud.GetAtributeString("area", "dbo.place", "id", healthCenter.IdPlace)}";
                print.CEPTextBox.Text = $"CEP: {crud.GetAtributeString("cep", "dbo.place", "id", healthCenter.IdPlace)}";
                print.CityTextBox.Text = $"{crud.GetAtributeString("city", "dbo.place", "id", healthCenter.IdPlace)}, {crud.GetAtributeString("acronym", "dbo.location", "id", crud.GetAtributeInt("idlocation", "dbo.place", "id", healthCenter.IdPlace))}";
                print.CNPJTextBox.Text = $"CNPJ: {healthCenter.CNPJ}";
                print.TelephoneTextBox.Text = $"Telefone: {healthCenter.Telephone}";
                print.AudiometerTextBox.Text = $"{crud.GetAtributeString("model", "dbo.audiometer", "id", healthCenter.IdAudiometer)} (Última Aferição: " +
                    $"{crud.GetAtributeDate("maintenance", "dbo.audiometer", "id", healthCenter.IdAudiometer).Date.ToString("dd/MM/yyyy")})";
                print.LogoBox.Source = ConvertBytesToImage(healthCenter.Logo);
            }

            foreach (AudioEvaluationModel evalutaionModel in
                crud.GetAudioEvaluation(crud.GetAtributeInt("idaudioevaluation", "dbo.session", "idaudiogram", Properties.Settings.Default.audiogramId)))
            {
                #region Meatoscopy

                print.ODMeatoscopy.Text = evalutaionModel.RightMeatoscopy;
                print.OEMeatoscopy.Text = evalutaionModel.LeftMeatoscopy;

                #endregion Meatoscopy

                #region Logo

                print.ODLRF.Text = $"{evalutaionModel.RightEarLRF} dB";
                print.ODLAF.Text = $"{evalutaionModel.RightEarLAF} dB";
                print.OELRF.Text = $"{evalutaionModel.LeftEarLRF} dB";
                print.OELAF.Text = $"{evalutaionModel.LeftEarLAF} dB";

                print.ODIntensid.Text = $"{evalutaionModel.RightEarIntesity} dB";
                print.OEIntensid.Text = $"{evalutaionModel.LeftEarIntesity} dB";

                print.NWordsMono.Text = evalutaionModel.WordsMono;
                print.ODMono.Text = $"{evalutaionModel.ODMono} %";
                print.OEMono.Text = $"{evalutaionModel.OEMono} %";

                print.NWordsDi.Text = evalutaionModel.WordsDi;
                print.ODDi.Text = $"{evalutaionModel.ODDi} %";
                print.OEDi.Text = $"{evalutaionModel.OEDi} %";

                print.NWordsTri.Text = evalutaionModel.WordsTri;
                print.ODTri.Text = $"{evalutaionModel.ODTri} %";
                print.OETri.Text = $"{evalutaionModel.OETri} %";

                #endregion Logo

                #region Mask

                print.ODVAMIN.Text = evalutaionModel.RightEarVAMin;
                print.ODVAMAX.Text = evalutaionModel.RightEarVAMax;
                print.ODVOMIN.Text = evalutaionModel.RightEarVOMin;
                print.ODVOMAX.Text = evalutaionModel.RightEarVOMax;
                print.ODLogo.Text = evalutaionModel.RightEarLogo;

                print.OEVAMIN.Text = evalutaionModel.LeftEarVAMin;
                print.OEVAMAX.Text = evalutaionModel.LeftEarVAMax;
                print.OEVOMIN.Text = evalutaionModel.LeftEarVOMin;
                print.OEVOMAX.Text = evalutaionModel.LeftEarVOMax; ;
                print.OELogo.Text = evalutaionModel.LeftEarLogo;

                #endregion Mask

                #region Obs

                print.Observations.Text = evalutaionModel.Obs;

                #endregion Obs
            }
        }

        private void RecordAudiogramUserControl_AudiogramPrintClicked(object sender, EventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                PrintTemplateUserControl printTemplateUserControl = new PrintTemplateUserControl(280, 330);
                FillPrintTemplate(printTemplateUserControl);
                printDlg.PrintVisual(printTemplateUserControl, "Audiometria");
            }
        }

        #endregion

        #region Fitting

        public void FillDeviceComboBox(char ear)
        {
            if (ear == 'R')
            {
                List<string> HIRight = new List<string>();

                foreach (var idFitting in crud.GetAtributeListInt("id", "dbo.fitting", "idpatient", Properties.Settings.Default.patientId))
                {
                    if (crud.GetAtributeStrings("channel", "dbo.fitting", "id", idFitting).Contains("R"))
                    {
                        foreach (var device in crud.GetAtributeListInt("idhearingaid", "dbo.fitting", "id", idFitting))
                        {
                            string deviceName = crud.GetAtributeString("device", "dbo.hearingaid", "id", device);
                            string mappedName = HINamesR(deviceName); 

                            if(!HIRight.Contains(mappedName + " - " + crud.GetAtributeLong("serialnumber", "dbo.hearingaid", "id", device)))
                            {
                                HIRight.Add(mappedName + " - " + crud.GetAtributeLong("serialnumber", "dbo.hearingaid", "id", device));
                            }
                        }
                    }
                }

                recordCalibrationUserControl.TextHearingAidR.ItemsSource = HIRight;
                recordCalibrationUserControl.TextHearingAidR.SelectedIndex = 0;
            }
            else
            {
                List<string> HILeft = new List<string>();

                foreach (var idFitting in crud.GetAtributeListInt("id", "dbo.fitting", "idpatient", Properties.Settings.Default.patientId))
                {
                    if (crud.GetAtributeStrings("channel", "dbo.fitting", "id", idFitting).Contains("L"))
                    {
                        foreach (var device in crud.GetAtributeListInt("idhearingaid", "dbo.fitting", "id", idFitting))
                        {
                            string deviceName = crud.GetAtributeString("device", "dbo.hearingaid", "id", device);
                            string mappedName = HINamesL(deviceName);

                            if (!HILeft.Contains(mappedName + " - " + crud.GetAtributeLong("serialnumber", "dbo.hearingaid", "id", device)))
                            {
                                HILeft.Add(mappedName + " - " + crud.GetAtributeLong("serialnumber", "dbo.hearingaid", "id", device));
                            }
                        }
                    }
                }

                recordCalibrationUserControl.TextHearingAidL.ItemsSource = HILeft;
                recordCalibrationUserControl.TextHearingAidL.SelectedIndex = 0;
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
                    return deviceName;
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
                    return deviceName;
            }
        }

        private void TextHearingAidR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillDateComboBox('R');

            if (recordCalibrationUserControl.TextHearingAidR.SelectedIndex >= 0)
            {
                LoadDataR(crud.GetRecordDates(Properties.Settings.Default.patientId, GetIdFromComboBox('R'), "R")[0]);
                recordCalibrationUserControl.DateRProgram.SelectedIndex = 0;
            }
            else
            {
                LoadDataR(crud.GetRecordDate(Properties.Settings.Default.patientId, GetIdFromComboBox('R'), "R"));
            }

            GetImagem('R', GetDeviceFromComboBox('R'));
        }

        private void TextHearingAidL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillDateComboBox('L');

            if (recordCalibrationUserControl.TextHearingAidL.SelectedIndex >= 0)
            {
                LoadDataL(crud.GetRecordDates(Properties.Settings.Default.patientId, GetIdFromComboBox('L'), "L")[0]);
                recordCalibrationUserControl.DateLProgram.SelectedIndex = 0;
            }
            else
            {
                LoadDataL(crud.GetRecordDate(Properties.Settings.Default.patientId, GetIdFromComboBox('L'), "L"));
            }

            GetImagem('L', GetDeviceFromComboBox('L'));
        }


        public void FillDateComboBox(char ear)
        {
            try
            {
                if (ear == 'R')
                {
                    List<string> DateR = new List<string>();

                    foreach (var date in crud.GetRecordDates(Properties.Settings.Default.patientId, GetIdFromComboBox('R'), "R"))
                    {
                        DateR.Add(date.ToString("dd/MM/yyyy"));
                    }

                    recordCalibrationUserControl.DateRProgram.ItemsSource = DateR;
                    recordCalibrationUserControl.DateRProgram.SelectedIndex = 0;
                }
                else
                {
                    List<string> DateL = new List<string>();

                    foreach (var date in crud.GetRecordDates(Properties.Settings.Default.patientId, GetIdFromComboBox('L'), "L"))
                    {
                        DateL.Add(date.ToString("dd/MM/yyyy"));
                    }

                    recordCalibrationUserControl.DateLProgram.ItemsSource = DateL;
                    recordCalibrationUserControl.DateLProgram.SelectedIndex = 0;
                }
            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }
        }

        public void LoadDataR(DateTime date)
        {
            recordCalibrationUserControl.recordCalibrationViewModelR.LoadFittingToDatagrid(recordCalibrationUserControl.DatagridFittingR,
                Properties.Settings.Default.patientId, GetIdFromComboBox('R'), "R", date);
            recordCalibrationUserControl.DatagridFittingR.DataContext = recordCalibrationUserControl.recordCalibrationViewModelR;
        }

        public void LoadDataL(DateTime date)
        {
            recordCalibrationUserControl.recordCalibrationViewModelL.LoadFittingToDatagrid(recordCalibrationUserControl.DatagridFittingL,
                Properties.Settings.Default.patientId, GetIdFromComboBox('L'), "L", date);
            recordCalibrationUserControl.DatagridFittingL.DataContext = recordCalibrationUserControl.recordCalibrationViewModelL;
        }

        private void DateRProgram_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<DateTime> DateR = new List<DateTime>();

            foreach (var date in crud.GetRecordDates(Properties.Settings.Default.patientId, GetIdFromComboBox('R'), "R"))
            {
                DateR.Add(date);
            }
            if(DateR.Count > 0 )
            {
                LoadDataR(DateR[recordCalibrationUserControl.DateRProgram.SelectedIndex]);
            }
        }

        private void DateLProgram_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<DateTime> DateL = new List<DateTime>();

            foreach (var date in crud.GetRecordDates(Properties.Settings.Default.patientId, GetIdFromComboBox('L'), "L"))
            {
                DateL.Add(date);
            }
            if (DateL.Count > 0)
            {
                LoadDataL(DateL[recordCalibrationUserControl.DateLProgram.SelectedIndex]);
            }
        }

        private static ImageSource ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return null;
            }

            try
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao converter byte[] para Image: " + ex.Message);
                return null;
            }
        }

        private void RecordCalibrationUserControl_ViewPlotRClicked(object sender, EventArgs e)
        {
            if (ShowHIR == true)
            {
                ShowHIR = false;
                recordCalibrationUserControl.ImageHearingAidR.Visibility = Visibility.Hidden;
                recordCalibrationUserControl.GainPlotImageR.Source = ByteArrayToImage(crud.GetAtributeByteArray("plot", "dbo.gainplot", "idfitting", Properties.Settings.Default.fittingId));
                recordCalibrationUserControl.GainPlotImageR.Visibility = Visibility.Visible;
            }
            else
            {
                ShowHIR = true;
                recordCalibrationUserControl.ImageHearingAidR.Visibility = Visibility.Visible;
                recordCalibrationUserControl.GainPlotImageR.Visibility = Visibility.Hidden;
            }
        }

        private void RecordCalibrationUserControl_ViewPlotLClicked(object sender, EventArgs e)
        {
            if (ShowHIL == true)
            {
                ShowHIL = false;
                recordCalibrationUserControl.ImageHearingAidL.Visibility = Visibility.Hidden;
                recordCalibrationUserControl.GainPlotImageL.Source = ByteArrayToImage(crud.GetAtributeByteArray("plot", "dbo.gainplot", "idfitting", Properties.Settings.Default.fittingId));
                recordCalibrationUserControl.GainPlotImageL.Visibility = Visibility.Visible;
            }
            else
            {
                ShowHIL = true;
                recordCalibrationUserControl.ImageHearingAidL.Visibility = Visibility.Visible;
                recordCalibrationUserControl.GainPlotImageL.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region Backup

        private void RecordCalibrationUserControl_FittingRClicked(object sender, EventArgs e)
        {
            FindHIType('R');
            if (connected == true)
            {
                StartChipR();
                BackupHIFittingR();
                CleanMessages();
                ProgramChipR();
                HandyControl.Controls.Growl.SuccessGlobal("Backup feito corretamente");
            }
        }

        private void RecordCalibrationUserControl_FittingLClicked(object sender, EventArgs e)
        {
            FindHIType('L');
            if (connected == true)
            {
                StartChipL();
                BackupHIFittingL();
                CleanMessages();
                ProgramChipL();
                HandyControl.Controls.Growl.SuccessGlobal("Backup feito corretamente");
            }
        }

        public void FindHIType(char side)
        {
            intriconViewModel.InitializeProgramer();
            if (side == 'R')
            {
                intriconViewModel.StartProgrammer('R');

                if(intriconViewModel.detectProgrammer == 1)
                {
                    if (intriconViewModel.detectedHI[1] != 0)
                    {
                        if (crud.GetAtributeString("device", "dbo.hearingaid", "id", crud.GetAtributeInt("idhearingaid", "dbo.fitting", "id", Properties.Settings.Default.fittingId)) == Properties.Settings.Default.ChipIDR)
                        {
                            connected = true;
                        }
                        else
                        {
                            CleanMessages();
                            HandyControl.Controls.Growl.Warning("Verifique se o aparelho conectado é do modelo correto.");
                            connected = false;
                        }
                    }
                    else
                    {
                        CleanMessages();
                        HandyControl.Controls.Growl.Warning("Verifique se o aparelho está corretamente conectado ao lado direito da programadora.");
                    }
                }
                else
                {
                    CleanMessages();
                    HandyControl.Controls.Growl.Warning("Verifique se a programadora está connectada ao computador corretamente.");
                }
            }
            else
            {
                intriconViewModel.StartProgrammer('L');
                if (intriconViewModel.detectProgrammer == 1)
                {
                    if (intriconViewModel.detectedHI[0] != 0)
                    {
                        if (crud.GetAtributeString("device", "dbo.hearingaid", "id", crud.GetAtributeInt("idhearingaid", "dbo.fitting", "id", Properties.Settings.Default.fittingId)) == Properties.Settings.Default.ChipIDL)
                        {
                            connected = true;
                        }
                        else
                        {
                            CleanMessages();
                            HandyControl.Controls.Growl.Warning("Verifique se o aparelho conectado é do modelo correto.");
                            connected = false;
                        }
                    }
                    else
                    {
                        CleanMessages();
                        HandyControl.Controls.Growl.Warning("Verifique se o aparelho está corretamente conectado ao lado esquerdo da programadora.");
                    }
                }
                else
                {
                    CleanMessages();
                    HandyControl.Controls.Growl.Warning("Verifique se a programadora está connectada ao computador corretamente.");
                }
            }
            intriconViewModel.CloseProgramer();
        }

        public void StartChipR()
        {
            string hearingAid = Properties.Settings.Default.ChipIDR;

            switch (hearingAid)
            {
                case "Audion16":
                    audion16ViewModelR.StartProgrammer();
                    audion16ViewModelR.ChangeSide('R');
                    audion16ViewModelR.SetCurrentProgram(recordCalibrationUserControl.currentProgramR);
                    audion16ViewModelR.ReadVM();
                    break;

                case "Audion8":
                    audion8ViewModelR.StartProgrammer();
                    audion8ViewModelR.ChangeSide('R');
                    audion8ViewModelR.SetCurrentProgram(recordCalibrationUserControl.currentProgramR);
                    audion8ViewModelR.ReadVM();
                    break;

                case "Audion6":
                    audion6ViewModelR.StartProgrammer();
                    audion6ViewModelR.ChangeSide('R');
                    audion6ViewModelR.SetCurrentProgram(recordCalibrationUserControl.currentProgramR);
                    audion6ViewModelR.ReadVM();
                    break;

                case "Audion4":
                    audion4ViewModelR.StartProgrammer();
                    audion4ViewModelR.ChangeSide('R');
                    audion4ViewModelR.SetCurrentProgram(recordCalibrationUserControl.currentProgramR);
                    audion4ViewModelR.ReadVM();
                    break;

                case "SpinNR":
                    spinNRViewModelR.StartProgrammer();
                    spinNRViewModelR.ChangeSide('R');
                    spinNRViewModelR.SetCurrentProgram(recordCalibrationUserControl.currentProgramR);
                    spinNRViewModelR.ReadVM();
                    break;
            }
        }

        public void StartChipL()
        {
            string hearingAid = Properties.Settings.Default.ChipIDL;
            connected = false;

            switch (hearingAid)
            {
                case "Audion16":
                    audion16ViewModelL.StartProgrammer();
                    audion16ViewModelL.ChangeSide('L');
                    audion16ViewModelL.SetCurrentProgram(recordCalibrationUserControl.currentProgramL);
                    audion16ViewModelL.ReadVM();
                    break;

                case "Audion8":
                    audion8ViewModelL.StartProgrammer();
                    audion8ViewModelL.ChangeSide('L');
                    audion8ViewModelL.SetCurrentProgram(recordCalibrationUserControl.currentProgramL);
                    audion8ViewModelL.ReadVM();
                    break;

                case "Audion6":
                    audion6ViewModelL.StartProgrammer();
                    audion6ViewModelL.ChangeSide('L');
                    audion6ViewModelL.SetCurrentProgram(recordCalibrationUserControl.currentProgramL);
                    audion6ViewModelL.ReadVM();
                    break;

                case "Audion4":
                    audion4ViewModelL.StartProgrammer();
                    audion4ViewModelL.ChangeSide('L');
                    audion4ViewModelL.SetCurrentProgram(recordCalibrationUserControl.currentProgramL);
                    audion4ViewModelL.ReadVM();
                    break;

                case "SpinNR":
                    spinNRViewModelL.StartProgrammer();
                    spinNRViewModelL.ChangeSide('L');
                    spinNRViewModelL.SetCurrentProgram(recordCalibrationUserControl.currentProgramL);
                    spinNRViewModelL.ReadVM();
                    break;
            }
        }

        public void BackupHIFittingR()
        {
            string hearingAid = Properties.Settings.Default.ChipIDR;
            string[] Params = crud.GetAtributeStringArray("paramters", "dbo.fitting", "id", Properties.Settings.Default.fittingId);
            string[] Config = crud.GetAtributeStringArray("configuration", "dbo.fitting", "id", Properties.Settings.Default.fittingId);

            switch (hearingAid)
            {
                case "Audion16":
                    audion16ViewModelR.input_mux = int.Parse(Params[0]);
                    audion16ViewModelR.matrix_gain = int.Parse(Params[1]);
                    audion16ViewModelR.preamp_gain1 = int.Parse(Params[2]);
                    audion16ViewModelR.preamp_gain2 = int.Parse(Params[3]);
                    audion16ViewModelR.preamp_gain_digital_1 = int.Parse(Params[4]);
                    audion16ViewModelR.preamp_gain_digital_2 = int.Parse(Params[5]);
                    audion16ViewModelR.feedback_canceller = int.Parse(Params[6]);
                    audion16ViewModelR.noise_reduction = int.Parse(Params[7]);
                    audion16ViewModelR.wind_suppression = int.Parse(Params[8]);
                    audion16ViewModelR.input_filter_low_cut = int.Parse(Params[9]);
                    audion16ViewModelR.low_level_expansion = int.Parse(Params[10]);
                    audion16ViewModelR.beq_gain_1 = int.Parse(Params[11]);
                    audion16ViewModelR.beq_gain_2 = int.Parse(Params[12]);
                    audion16ViewModelR.beq_gain_3 = int.Parse(Params[13]);
                    audion16ViewModelR.beq_gain_4 = int.Parse(Params[14]);
                    audion16ViewModelR.beq_gain_5 = int.Parse(Params[15]);
                    audion16ViewModelR.beq_gain_6 = int.Parse(Params[16]);
                    audion16ViewModelR.beq_gain_7 = int.Parse(Params[17]);
                    audion16ViewModelR.beq_gain_8 = int.Parse(Params[18]);
                    audion16ViewModelR.beq_gain_9 = int.Parse(Params[19]);
                    audion16ViewModelR.beq_gain_10 = int.Parse(Params[20]);
                    audion16ViewModelR.beq_gain_11 = int.Parse(Params[21]);
                    audion16ViewModelR.beq_gain_12 = int.Parse(Params[22]);
                    audion16ViewModelR.beq_gain_13 = int.Parse(Params[23]);
                    audion16ViewModelR.beq_gain_14 = int.Parse(Params[24]);
                    audion16ViewModelR.beq_gain_15 = int.Parse(Params[25]);
                    audion16ViewModelR.beq_gain_16 = int.Parse(Params[26]);
                    audion16ViewModelR.mpo_threshold_1 = int.Parse(Params[27]);
                    audion16ViewModelR.mpo_threshold_2 = int.Parse(Params[28]);
                    audion16ViewModelR.mpo_threshold_3 = int.Parse(Params[29]);
                    audion16ViewModelR.mpo_threshold_4 = int.Parse(Params[30]);
                    audion16ViewModelR.mpo_threshold_5 = int.Parse(Params[31]);
                    audion16ViewModelR.mpo_threshold_6 = int.Parse(Params[32]);
                    audion16ViewModelR.mpo_threshold_7 = int.Parse(Params[33]);
                    audion16ViewModelR.mpo_threshold_8 = int.Parse(Params[34]);
                    audion16ViewModelR.mpo_threshold_9 = int.Parse(Params[35]);
                    audion16ViewModelR.mpo_threshold_10 = int.Parse(Params[36]);
                    audion16ViewModelR.mpo_threshold_11 = int.Parse(Params[37]);
                    audion16ViewModelR.mpo_threshold_12 = int.Parse(Params[38]);
                    audion16ViewModelR.mpo_threshold_13 = int.Parse(Params[39]);
                    audion16ViewModelR.mpo_threshold_14 = int.Parse(Params[40]);
                    audion16ViewModelR.mpo_threshold_15 = int.Parse(Params[41]);
                    audion16ViewModelR.mpo_threshold_16 = int.Parse(Params[42]);
                    audion16ViewModelR.mpo_release = int.Parse(Params[43]);
                    audion16ViewModelR.mpo_attack = int.Parse(Params[44]);
                    audion16ViewModelR.comp_ratio_1 = int.Parse(Params[45]);
                    audion16ViewModelR.comp_ratio_2 = int.Parse(Params[46]);
                    audion16ViewModelR.comp_ratio_3 = int.Parse(Params[47]);
                    audion16ViewModelR.comp_ratio_4 = int.Parse(Params[48]);
                    audion16ViewModelR.comp_ratio_5 = int.Parse(Params[49]);
                    audion16ViewModelR.comp_ratio_6 = int.Parse(Params[50]);
                    audion16ViewModelR.comp_ratio_7 = int.Parse(Params[51]);
                    audion16ViewModelR.comp_ratio_8 = int.Parse(Params[52]);
                    audion16ViewModelR.comp_ratio_9 = int.Parse(Params[53]);
                    audion16ViewModelR.comp_ratio_10 = int.Parse(Params[54]);
                    audion16ViewModelR.comp_ratio_11 = int.Parse(Params[55]);
                    audion16ViewModelR.comp_ratio_12 = int.Parse(Params[56]);
                    audion16ViewModelR.comp_ratio_13 = int.Parse(Params[57]);
                    audion16ViewModelR.comp_ratio_14 = int.Parse(Params[58]);
                    audion16ViewModelR.comp_ratio_15 = int.Parse(Params[59]);
                    audion16ViewModelR.comp_ratio_16 = int.Parse(Params[60]);
                    audion16ViewModelR.comp_threshold_1 = int.Parse(Params[61]);
                    audion16ViewModelR.comp_threshold_2 = int.Parse(Params[62]);
                    audion16ViewModelR.comp_threshold_3 = int.Parse(Params[63]);
                    audion16ViewModelR.comp_threshold_4 = int.Parse(Params[64]);
                    audion16ViewModelR.comp_threshold_5 = int.Parse(Params[65]);
                    audion16ViewModelR.comp_threshold_6 = int.Parse(Params[66]);
                    audion16ViewModelR.comp_threshold_7 = int.Parse(Params[67]);
                    audion16ViewModelR.comp_threshold_8 = int.Parse(Params[68]);
                    audion16ViewModelR.comp_threshold_9 = int.Parse(Params[69]);
                    audion16ViewModelR.comp_threshold_10 = int.Parse(Params[70]);
                    audion16ViewModelR.comp_threshold_11 = int.Parse(Params[71]);
                    audion16ViewModelR.comp_threshold_12 = int.Parse(Params[72]);
                    audion16ViewModelR.comp_threshold_13 = int.Parse(Params[73]);
                    audion16ViewModelR.comp_threshold_14 = int.Parse(Params[74]);
                    audion16ViewModelR.comp_threshold_15 = int.Parse(Params[75]);
                    audion16ViewModelR.comp_threshold_16 = int.Parse(Params[76]);
                    audion16ViewModelR.comp_time_consts_1 = int.Parse(Params[77]);
                    audion16ViewModelR.comp_time_consts_2 = int.Parse(Params[78]);
                    audion16ViewModelR.comp_time_consts_3 = int.Parse(Params[79]);
                    audion16ViewModelR.comp_time_consts_4 = int.Parse(Params[80]);
                    audion16ViewModelR.comp_time_consts_5 = int.Parse(Params[81]);
                    audion16ViewModelR.comp_time_consts_6 = int.Parse(Params[82]);
                    audion16ViewModelR.comp_time_consts_7 = int.Parse(Params[83]);
                    audion16ViewModelR.comp_time_consts_8 = int.Parse(Params[84]);
                    audion16ViewModelR.comp_time_consts_9 = int.Parse(Params[85]);
                    audion16ViewModelR.comp_time_consts_10 = int.Parse(Params[86]);
                    audion16ViewModelR.comp_time_consts_11 = int.Parse(Params[87]);
                    audion16ViewModelR.comp_time_consts_12 = int.Parse(Params[88]);
                    audion16ViewModelR.comp_time_consts_13 = int.Parse(Params[89]);
                    audion16ViewModelR.comp_time_consts_14 = int.Parse(Params[90]);
                    audion16ViewModelR.comp_time_consts_15 = int.Parse(Params[91]);
                    audion16ViewModelR.comp_time_consts_16 = int.Parse(Params[92]);

                    audion16ViewModelR.Switch_Mode = int.Parse(Config[0]);
                    audion16ViewModelR.VC_Mode = int.Parse(Config[1]);
                    audion16ViewModelR.VC_Enable = int.Parse(Config[2]);
                    audion16ViewModelR.Auto_Save = int.Parse(Config[3]);
                    audion16ViewModelR.VC_Prompt_Mode = int.Parse(Config[4]);
                    audion16ViewModelR.Program_Prompt_Mode = int.Parse(Config[5]);
                    audion16ViewModelR.Warning_Prompt_Mode = int.Parse(Config[6]);
                    audion16ViewModelR.Power_On_VC = int.Parse(Config[7]);
                    audion16ViewModelR.Power_On_Program = int.Parse(Config[8]);
                    audion16ViewModelR.VC_Num_Steps = int.Parse(Config[9]);
                    audion16ViewModelR.VC_Step_Size = int.Parse(Config[10]);
                    audion16ViewModelR.VC_Analog_Range = int.Parse(Config[11]);
                    audion16ViewModelR.Num_Programs = int.Parse(Config[12]);
                    audion16ViewModelR.Prompt_Level = int.Parse(Config[13]);
                    audion16ViewModelR.Tone_Frequency = int.Parse(Config[14]);
                    audion16ViewModelR.ADir_Sensitivity = int.Parse(Config[15]);
                    audion16ViewModelR.Auto_Telecoil = int.Parse(Config[16]);
                    audion16ViewModelR.Acoustap_Mode = int.Parse(Config[17]);
                    audion16ViewModelR.Acoustap_Sensitivity = int.Parse(Config[18]);
                    audion16ViewModelR.Power_On_Level = int.Parse(Config[19]);
                    audion16ViewModelR.Power_On_Delay = int.Parse(Config[20]);
                    audion16ViewModelR.Noise_Level = int.Parse(Config[21]);
                    audion16ViewModelR.High_Power_Mode = int.Parse(Config[22]);
                    audion16ViewModelR.Dir_Mic_Cal = int.Parse(Config[23]);
                    audion16ViewModelR.Dir_Mic_Cal_Input = int.Parse(Config[24]);
                    audion16ViewModelR.Dir_Spacing = int.Parse(Config[25]);
                    audion16ViewModelR.test = int.Parse(Config[26]);
                    audion16ViewModelR.Output_Filter_Enable = int.Parse(Config[27]);
                    audion16ViewModelR.Output_Filter_1 = int.Parse(Config[28]);
                    audion16ViewModelR.Output_Filter_2 = int.Parse(Config[29]);
                    audion16ViewModelR.Noise_Filter_Ref = int.Parse(Config[30]);
                    audion16ViewModelR.Noise_Filter_1 = int.Parse(Config[31]);
                    audion16ViewModelR.Noise_Filter_2 = int.Parse(Config[32]);
                    audion16ViewModelR.MANF_ID = int.Parse(Config[33]);
                    audion16ViewModelR.Platform_ID = int.Parse(Config[34]);
                    audion16ViewModelR.AlgVer_Build = int.Parse(Config[35]);
                    audion16ViewModelR.AlgVer_Major = int.Parse(Config[36]);
                    audion16ViewModelR.AlgVer_Minor = int.Parse(Config[37]);
                    audion16ViewModelR.ModelID = int.Parse(Config[38]);
                    audion16ViewModelR.MDA_1 = int.Parse(Config[39]);
                    audion16ViewModelR.MDA_2 = int.Parse(Config[40]);
                    audion16ViewModelR.MDA_3 = int.Parse(Config[41]);
                    audion16ViewModelR.MDA_4 = int.Parse(Config[42]);
                    audion16ViewModelR.MDA_5 = int.Parse(Config[43]);
                    audion16ViewModelR.MDA_6 = int.Parse(Config[44]);
                    audion16ViewModelR.MDA_7 = int.Parse(Config[45]);
                    audion16ViewModelR.MDA_8 = int.Parse(Config[46]);
                    audion16ViewModelR.MDA_9 = int.Parse(Config[47]);
                    audion16ViewModelR.MDA_10 = int.Parse(Config[48]);

                    break;

                case "Audion8":

                    audion8ViewModelR.input_mux = int.Parse(Params[0]);
                    audion8ViewModelR.preamp_gain0 = int.Parse(Params[1]);
                    audion8ViewModelR.preamp_gain1 = int.Parse(Params[2]);
                    audion8ViewModelR.C1_Ratio = int.Parse(Params[3]);
                    audion8ViewModelR.C2_Ratio = int.Parse(Params[4]);
                    audion8ViewModelR.C3_Ratio = int.Parse(Params[5]);
                    audion8ViewModelR.C4_Ratio = int.Parse(Params[6]);
                    audion8ViewModelR.C5_Ratio = int.Parse(Params[7]);
                    audion8ViewModelR.C6_Ratio = int.Parse(Params[8]);
                    audion8ViewModelR.C7_Ratio = int.Parse(Params[9]);
                    audion8ViewModelR.C8_Ratio = int.Parse(Params[10]);
                    audion8ViewModelR.C1_TK = int.Parse(Params[11]);
                    audion8ViewModelR.C2_TK = int.Parse(Params[12]);
                    audion8ViewModelR.C3_TK = int.Parse(Params[13]);
                    audion8ViewModelR.C4_TK = int.Parse(Params[14]);
                    audion8ViewModelR.C5_TK = int.Parse(Params[15]);
                    audion8ViewModelR.C6_TK = int.Parse(Params[16]);
                    audion8ViewModelR.C7_TK = int.Parse(Params[17]);
                    audion8ViewModelR.C8_TK = int.Parse(Params[18]);
                    audion8ViewModelR.C1_MPO = int.Parse(Params[19]);
                    audion8ViewModelR.C2_MPO = int.Parse(Params[20]);
                    audion8ViewModelR.C3_MPO = int.Parse(Params[21]);
                    audion8ViewModelR.C4_MPO = int.Parse(Params[22]);
                    audion8ViewModelR.C5_MPO = int.Parse(Params[23]);
                    audion8ViewModelR.C6_MPO = int.Parse(Params[24]);
                    audion8ViewModelR.C7_MPO = int.Parse(Params[25]);
                    audion8ViewModelR.C8_MPO = int.Parse(Params[26]);
                    audion8ViewModelR.BEQ1_gain = int.Parse(Params[27]);
                    audion8ViewModelR.BEQ2_gain = int.Parse(Params[28]);
                    audion8ViewModelR.BEQ3_gain = int.Parse(Params[29]);
                    audion8ViewModelR.BEQ4_gain = int.Parse(Params[30]);
                    audion8ViewModelR.BEQ5_gain = int.Parse(Params[31]);
                    audion8ViewModelR.BEQ6_gain = int.Parse(Params[32]);
                    audion8ViewModelR.BEQ7_gain = int.Parse(Params[33]);
                    audion8ViewModelR.BEQ8_gain = int.Parse(Params[34]);
                    audion8ViewModelR.BEQ9_gain = int.Parse(Params[35]);
                    audion8ViewModelR.BEQ10_gain = int.Parse(Params[36]);
                    audion8ViewModelR.BEQ11_gain = int.Parse(Params[37]);
                    audion8ViewModelR.BEQ12_gain = int.Parse(Params[38]);
                    audion8ViewModelR.matrix_gain = int.Parse(Params[39]);
                    audion8ViewModelR.Noise_Reduction = int.Parse(Params[40]);
                    audion8ViewModelR.FBC_Enable = int.Parse(Params[41]);
                    audion8ViewModelR.Time_Constants = int.Parse(Params[42]);

                    audion8ViewModelR.AutoSave_Enable = int.Parse(Config[0]);
                    audion8ViewModelR.ATC = int.Parse(Config[1]);
                    audion8ViewModelR.EnableHPmode = int.Parse(Config[2]);
                    audion8ViewModelR.Noise_Level = int.Parse(Config[3]);
                    audion8ViewModelR.POL = int.Parse(Config[4]);
                    audion8ViewModelR.POD = int.Parse(Config[5]);
                    audion8ViewModelR.AD_Sens = int.Parse(Config[6]);
                    audion8ViewModelR.Cal_Input = int.Parse(Config[7]);
                    audion8ViewModelR.Dir_Spacing = int.Parse(Config[8]);
                    audion8ViewModelR.Mic_Cal = int.Parse(Config[9]);
                    audion8ViewModelR.number_of_programs = int.Parse(Config[10]);
                    audion8ViewModelR.PGM_Startup = int.Parse(Config[11]);
                    audion8ViewModelR.Switch_Mode = int.Parse(Config[12]);
                    audion8ViewModelR.Program_Prompt_Mode = int.Parse(Config[13]);
                    audion8ViewModelR.Warning_Prompt_Mode = int.Parse(Config[14]);
                    audion8ViewModelR.Tone_Frequency = int.Parse(Config[15]);
                    audion8ViewModelR.Tone_Level = int.Parse(Config[16]);
                    audion8ViewModelR.VC_Enable = int.Parse(Config[17]);
                    audion8ViewModelR.VC_Analog_Range = int.Parse(Config[18]);
                    audion8ViewModelR.VC_Digital_Numsteps = int.Parse(Config[19]);
                    audion8ViewModelR.VC_Digital_Startup = int.Parse(Config[20]);
                    audion8ViewModelR.VC_Digital_Stepsize = int.Parse(Config[21]);
                    audion8ViewModelR.VC_Mode = int.Parse(Config[22]);
                    audion8ViewModelR.VC_pos = int.Parse(Config[23]);
                    audion8ViewModelR.VC_Prompt_Mode = int.Parse(Config[24]);
                    audion8ViewModelR.AlgVer_Major = int.Parse(Config[25]);
                    audion8ViewModelR.AlgVer_Minor = int.Parse(Config[26]);
                    audion8ViewModelR.MANF_ID = int.Parse(Config[27]);
                    audion8ViewModelR.PlatformID = int.Parse(Config[28]);
                    audion8ViewModelR.reserved1 = int.Parse(Config[29]);
                    audion8ViewModelR.reserved2 = int.Parse(Config[30]);
                    audion8ViewModelR.test = int.Parse(Config[31]);
                    audion8ViewModelR.MANF_reserve_1 = int.Parse(Config[32]);
                    audion8ViewModelR.MANF_reserve_2 = int.Parse(Config[33]);
                    audion8ViewModelR.MANF_reserve_3 = int.Parse(Config[34]);
                    audion8ViewModelR.MANF_reserve_4 = int.Parse(Config[35]);
                    audion8ViewModelR.MANF_reserve_5 = int.Parse(Config[36]);
                    audion8ViewModelR.MANF_reserve_6 = int.Parse(Config[37]);
                    audion8ViewModelR.MANF_reserve_7 = int.Parse(Config[38]);
                    audion8ViewModelR.MANF_reserve_8 = int.Parse(Config[39]);
                    audion8ViewModelR.MANF_reserve_9 = int.Parse(Config[40]);
                    audion8ViewModelR.MANF_reserve_10 = int.Parse(Config[41]);

                    break;

                case "Audion6":

                    audion6ViewModelR.ActiveProgram = int.Parse(Params[0]);
                    audion6ViewModelR.BEQ1_gain = int.Parse(Params[1]);
                    audion6ViewModelR.BEQ2_gain = int.Parse(Params[2]);
                    audion6ViewModelR.BEQ3_gain = int.Parse(Params[3]);
                    audion6ViewModelR.BEQ4_gain = int.Parse(Params[4]);
                    audion6ViewModelR.BEQ5_gain = int.Parse(Params[5]);
                    audion6ViewModelR.BEQ6_gain = int.Parse(Params[6]);
                    audion6ViewModelR.BEQ7_gain = int.Parse(Params[7]);
                    audion6ViewModelR.BEQ8_gain = int.Parse(Params[8]);
                    audion6ViewModelR.BEQ9_gain = int.Parse(Params[9]);
                    audion6ViewModelR.BEQ10_gain = int.Parse(Params[10]);
                    audion6ViewModelR.BEQ11_gain = int.Parse(Params[11]);
                    audion6ViewModelR.BEQ12_gain = int.Parse(Params[12]);
                    audion6ViewModelR.C1_ExpTK = int.Parse(Params[13]);
                    audion6ViewModelR.C2_ExpTK = int.Parse(Params[14]);
                    audion6ViewModelR.C3_ExpTK = int.Parse(Params[15]);
                    audion6ViewModelR.C4_ExpTK = int.Parse(Params[16]);
                    audion6ViewModelR.C5_ExpTK = int.Parse(Params[17]);
                    audion6ViewModelR.C6_ExpTK = int.Parse(Params[18]);
                    audion6ViewModelR.C1_MPO = int.Parse(Params[19]);
                    audion6ViewModelR.C2_MPO = int.Parse(Params[20]);
                    audion6ViewModelR.C3_MPO = int.Parse(Params[21]);
                    audion6ViewModelR.C4_MPO = int.Parse(Params[22]);
                    audion6ViewModelR.C5_MPO = int.Parse(Params[23]);
                    audion6ViewModelR.C6_MPO = int.Parse(Params[24]);
                    audion6ViewModelR.C1_Ratio = int.Parse(Params[25]);
                    audion6ViewModelR.C2_Ratio = int.Parse(Params[26]);
                    audion6ViewModelR.C3_Ratio = int.Parse(Params[27]);
                    audion6ViewModelR.C4_Ratio = int.Parse(Params[28]);
                    audion6ViewModelR.C5_Ratio = int.Parse(Params[29]);
                    audion6ViewModelR.C6_Ratio = int.Parse(Params[30]);
                    audion6ViewModelR.C1_TK = int.Parse(Params[31]);
                    audion6ViewModelR.C2_TK = int.Parse(Params[32]);
                    audion6ViewModelR.C3_TK = int.Parse(Params[33]);
                    audion6ViewModelR.C4_TK = int.Parse(Params[34]);
                    audion6ViewModelR.C5_TK = int.Parse(Params[35]);
                    audion6ViewModelR.C6_TK = int.Parse(Params[36]);
                    audion6ViewModelR.Exp_Attack = int.Parse(Params[37]);
                    audion6ViewModelR.Exp_Ratio = int.Parse(Params[38]);
                    audion6ViewModelR.Exp_Release = int.Parse(Params[39]);
                    audion6ViewModelR.FBC_Enable = int.Parse(Params[40]);
                    audion6ViewModelR.input_mux = int.Parse(Params[41]);
                    audion6ViewModelR.matrix_gain = int.Parse(Params[42]);
                    audion6ViewModelR.MPO_Attack = int.Parse(Params[43]);
                    audion6ViewModelR.MPO_Release = int.Parse(Params[44]);
                    audion6ViewModelR.Noise_Reduction = int.Parse(Params[45]);
                    audion6ViewModelR.preamp_gain0 = int.Parse(Params[46]);
                    audion6ViewModelR.preamp_gain1 = int.Parse(Params[47]);
                    audion6ViewModelR.TimeConstants1 = int.Parse(Params[48]);
                    audion6ViewModelR.TimeConstants2 = int.Parse(Params[49]);
                    audion6ViewModelR.TimeConstants3 = int.Parse(Params[50]);
                    audion6ViewModelR.TimeConstants4 = int.Parse(Params[51]);
                    audion6ViewModelR.TimeConstants5 = int.Parse(Params[52]);
                    audion6ViewModelR.TimeConstants6 = int.Parse(Params[53]);
                    audion6ViewModelR.VcPosition = int.Parse(Params[54]);

                    audion6ViewModelR.Auto_Telecoil_Enable = int.Parse(Config[0]);
                    audion6ViewModelR.Cal_Input = int.Parse(Config[1]);
                    audion6ViewModelR.Dir_Spacing = int.Parse(Config[2]);
                    audion6ViewModelR.Low_Battery_Warning = int.Parse(Config[3]);
                    audion6ViewModelR.Mic_Cal = int.Parse(Config[4]);
                    audion6ViewModelR.number_of_programs = int.Parse(Config[5]);
                    audion6ViewModelR.Output_Mode = int.Parse(Config[6]);
                    audion6ViewModelR.Power_On_Delay = int.Parse(Config[7]);
                    audion6ViewModelR.Power_On_Level = int.Parse(Config[8]);
                    audion6ViewModelR.Program_Beep_Enable = int.Parse(Config[9]);
                    audion6ViewModelR.Program_StartUp = int.Parse(Config[10]);
                    audion6ViewModelR.Switch_Mode = int.Parse(Config[11]);
                    audion6ViewModelR.Tone_Frequency = int.Parse(Config[12]);
                    audion6ViewModelR.Tone_Level = int.Parse(Config[13]);
                    audion6ViewModelR.VC_AnalogRange = int.Parse(Config[14]);
                    audion6ViewModelR.VC_Enable = int.Parse(Config[15]);
                    audion6ViewModelR.VC_Mode = int.Parse(Config[16]);
                    audion6ViewModelR.VC_DigitalNumSteps = int.Parse(Config[17]);
                    audion6ViewModelR.VC_StartUp = int.Parse(Config[18]);
                    audion6ViewModelR.VC_DigitalStepSize = int.Parse(Config[19]);

                    break;

                case "Audion4":
                    audion4ViewModelR.BEQ1_gain = int.Parse(Params[0]);
                    audion4ViewModelR.BEQ2_gain = int.Parse(Params[1]);
                    audion4ViewModelR.BEQ3_gain = int.Parse(Params[2]);
                    audion4ViewModelR.BEQ4_gain = int.Parse(Params[3]);
                    audion4ViewModelR.BEQ5_gain = int.Parse(Params[4]);
                    audion4ViewModelR.BEQ6_gain = int.Parse(Params[5]);
                    audion4ViewModelR.BEQ7_gain = int.Parse(Params[6]);
                    audion4ViewModelR.BEQ8_gain = int.Parse(Params[7]);
                    audion4ViewModelR.BEQ9_gain = int.Parse(Params[8]);
                    audion4ViewModelR.BEQ10_gain = int.Parse(Params[9]);
                    audion4ViewModelR.BEQ11_gain = int.Parse(Params[10]);
                    audion4ViewModelR.BEQ12_gain = int.Parse(Params[11]);
                    audion4ViewModelR.C1_Ratio = int.Parse(Params[12]);
                    audion4ViewModelR.C2_Ratio = int.Parse(Params[13]);
                    audion4ViewModelR.C3_Ratio = int.Parse(Params[14]);
                    audion4ViewModelR.C4_Ratio = int.Parse(Params[15]);
                    audion4ViewModelR.Expansion_Enable = int.Parse(Params[16]);
                    audion4ViewModelR.FBC_Enable = int.Parse(Params[17]);
                    audion4ViewModelR.High_Cut = int.Parse(Params[18]);
                    audion4ViewModelR.input_mux = int.Parse(Params[19]);
                    audion4ViewModelR.Low_Cut = int.Parse(Params[20]);
                    audion4ViewModelR.matrix_gain = int.Parse(Params[21]);
                    audion4ViewModelR.MPO_level = int.Parse(Params[22]);
                    audion4ViewModelR.Noise_Reduction = int.Parse(Params[23]);
                    audion4ViewModelR.preamp_gain0 = int.Parse(Params[24]);
                    audion4ViewModelR.preamp_gain1 = int.Parse(Params[25]);
                    audion4ViewModelR.threshold = int.Parse(Params[26]);

                    audion4ViewModelR.ATC = int.Parse(Config[0]);
                    audion4ViewModelR.Auto_Save = int.Parse(Config[1]);
                    audion4ViewModelR.Cal_Input = int.Parse(Config[2]);
                    audion4ViewModelR.Dir_Spacing = int.Parse(Config[3]);
                    audion4ViewModelR.Low_Batt_Warning = int.Parse(Config[4]);
                    audion4ViewModelR.MAP_HC = int.Parse(Config[5]);
                    audion4ViewModelR.MAP_LC = int.Parse(Config[6]);
                    audion4ViewModelR.MAP_MPO = int.Parse(Config[7]);
                    audion4ViewModelR.MAP_TK = int.Parse(Config[8]);
                    audion4ViewModelR.Mic_Cal = int.Parse(Config[9]);
                    audion4ViewModelR.number_of_programs = int.Parse(Config[10]);
                    audion4ViewModelR.Power_On_Level = int.Parse(Config[11]);
                    audion4ViewModelR.Power_On_Delay = int.Parse(Config[12]);
                    audion4ViewModelR.Program_StartUp = int.Parse(Config[13]);
                    audion4ViewModelR.Out_Mode = int.Parse(Config[14]);
                    audion4ViewModelR.Switch_Mode = int.Parse(Config[15]);
                    audion4ViewModelR.Switch_Tone = int.Parse(Config[16]);
                    audion4ViewModelR.T1_DIR = int.Parse(Config[17]);
                    audion4ViewModelR.T2_DIR = int.Parse(Config[18]);
                    audion4ViewModelR.test = int.Parse(Config[19]);
                    audion4ViewModelR.Tone_Frequency = int.Parse(Config[20]);
                    audion4ViewModelR.Tone_Level = int.Parse(Config[21]);
                    audion4ViewModelR.Time_Constants = int.Parse(Config[22]);
                    audion4ViewModelR.VC_AnalogRange = int.Parse(Config[23]);
                    audion4ViewModelR.VC_Beep_Enable = int.Parse(Config[24]);
                    audion4ViewModelR.VC_DigitalNumSteps = int.Parse(Config[25]);
                    audion4ViewModelR.VC_DigitalStepSize = int.Parse(Config[26]);
                    audion4ViewModelR.VC_Enable = int.Parse(Config[27]);
                    audion4ViewModelR.VC_Mode = int.Parse(Config[28]);
                    audion4ViewModelR.VC_Startup = int.Parse(Config[29]);
                    audion4ViewModelR.Active_PGM = int.Parse(Config[30]);
                    audion4ViewModelR.T1_POS = int.Parse(Config[31]);
                    audion4ViewModelR.T2_POS = int.Parse(Config[32]);
                    audion4ViewModelR.VC_Pos = int.Parse(Config[33]);

                    break;

                case "SpinNR":
                    spinNRViewModelR.input_mux = int.Parse(Params[0]);
                    spinNRViewModelR.preamp_gain0 = int.Parse(Params[1]);
                    spinNRViewModelR.preamp_gain1 = int.Parse(Params[2]);
                    spinNRViewModelR.CRL = int.Parse(Params[3]);
                    spinNRViewModelR.CRH = int.Parse(Params[4]);
                    spinNRViewModelR.threshold = int.Parse(Params[5]);
                    spinNRViewModelR.Low_Cut = int.Parse(Params[6]);
                    spinNRViewModelR.High_Cut = int.Parse(Params[7]);
                    spinNRViewModelR.Noise_Reduction = int.Parse(Params[8]);
                    spinNRViewModelR.BEQ1_gain = int.Parse(Params[9]);
                    spinNRViewModelR.BEQ2_gain = int.Parse(Params[10]);
                    spinNRViewModelR.BEQ3_gain = int.Parse(Params[11]);
                    spinNRViewModelR.BEQ4_gain = int.Parse(Params[12]);
                    spinNRViewModelR.BEQ5_gain = int.Parse(Params[13]);
                    spinNRViewModelR.BEQ6_gain = int.Parse(Params[14]);
                    spinNRViewModelR.BEQ7_gain = int.Parse(Params[15]);
                    spinNRViewModelR.BEQ8_gain = int.Parse(Params[16]);
                    spinNRViewModelR.BEQ9_gain = int.Parse(Params[17]);
                    spinNRViewModelR.BEQ10_gain = int.Parse(Params[18]);
                    spinNRViewModelR.BEQ11_gain = int.Parse(Params[19]);
                    spinNRViewModelR.BEQ12_gain = int.Parse(Params[20]);
                    spinNRViewModelR.matrix_gain = int.Parse(Params[21]);
                    spinNRViewModelR.MPO_level = int.Parse(Params[22]);
                    spinNRViewModelR.FBC_Enable = int.Parse(Params[23]);
                    spinNRViewModelR.Cal_Input = int.Parse(Params[24]);
                    spinNRViewModelR.Mic_Cal = int.Parse(Params[25]);

                    spinNRViewModelR.number_of_programs = int.Parse(Config[0]);
                    spinNRViewModelR.VC_MAP = int.Parse(Config[1]);
                    spinNRViewModelR.VC_Range = int.Parse(Config[2]);
                    spinNRViewModelR.VC_pos = int.Parse(Config[3]);
                    spinNRViewModelR.TK_MAP = int.Parse(Config[4]);
                    spinNRViewModelR.HC_MAP = int.Parse(Config[5]);
                    spinNRViewModelR.LC_MAP = int.Parse(Config[6]);
                    spinNRViewModelR.MPO_MAP = int.Parse(Config[7]);
                    spinNRViewModelR.T1_DIR = int.Parse(Config[8]);
                    spinNRViewModelR.T2_DIR = int.Parse(Config[9]);
                    spinNRViewModelR.T3_DIR = int.Parse(Config[10]);
                    spinNRViewModelR.CoilPGM = int.Parse(Config[11]);
                    spinNRViewModelR.MANF_ID = int.Parse(Config[12]);
                    spinNRViewModelR.OutMode = int.Parse(Config[13]);
                    spinNRViewModelR.Switch_Tone = int.Parse(Config[14]);
                    spinNRViewModelR.Low_Batt_Warning = int.Parse(Config[15]);
                    spinNRViewModelR.Tone_Frequency = int.Parse(Config[16]);
                    spinNRViewModelR.Tone_Level = int.Parse(Config[17]);
                    spinNRViewModelR.ATC = int.Parse(Config[18]);
                    spinNRViewModelR.TimeConstants = int.Parse(Config[19]);
                    spinNRViewModelR.Mic_Expansion = int.Parse(Config[20]);
                    spinNRViewModelR.reserved1 = int.Parse(Config[21]);
                    spinNRViewModelR.reserved2 = int.Parse(Config[22]);
                    spinNRViewModelR.reserved3 = int.Parse(Config[23]);
                    spinNRViewModelR.reserved4 = int.Parse(Config[24]);
                    spinNRViewModelR.test = int.Parse(Config[25]);
                    spinNRViewModelR.T1_POS = int.Parse(Config[26]);
                    spinNRViewModelR.T2_POS = int.Parse(Config[27]);
                    spinNRViewModelR.T3_POS = int.Parse(Config[28]);
                    spinNRViewModelR.MANF_reserve_1 = int.Parse(Config[29]);
                    spinNRViewModelR.MANF_reserve_2 = int.Parse(Config[30]);
                    spinNRViewModelR.MANF_reserve_3 = int.Parse(Config[31]);
                    spinNRViewModelR.MANF_reserve_4 = int.Parse(Config[32]);
                    spinNRViewModelR.MANF_reserve_5 = int.Parse(Config[33]);
                    spinNRViewModelR.MANF_reserve_6 = int.Parse(Config[34]);
                    spinNRViewModelR.MANF_reserve_7 = int.Parse(Config[35]);
                    spinNRViewModelR.MANF_reserve_8 = int.Parse(Config[36]);
                    spinNRViewModelR.MANF_reserve_9 = int.Parse(Config[37]);
                    spinNRViewModelR.MANF_reserve_10 = int.Parse(Config[38]);

                    break;
            }
        }

        public void BackupHIFittingL()
        {
            string hearingAid = Properties.Settings.Default.ChipIDL;
            string[] Params = crud.GetAtributeStringArray("paramters", "dbo.fitting", "id", Properties.Settings.Default.fittingId);
            string[] Config = crud.GetAtributeStringArray("configuration", "dbo.fitting", "id", Properties.Settings.Default.fittingId);

            switch (hearingAid)
            {
                case "Audion16":
                    audion16ViewModelL.input_mux = int.Parse(Params[0]);
                    audion16ViewModelL.matrix_gain = int.Parse(Params[1]);
                    audion16ViewModelL.preamp_gain1 = int.Parse(Params[2]);
                    audion16ViewModelL.preamp_gain2 = int.Parse(Params[3]);
                    audion16ViewModelL.preamp_gain_digital_1 = int.Parse(Params[4]);
                    audion16ViewModelL.preamp_gain_digital_2 = int.Parse(Params[5]);
                    audion16ViewModelL.feedback_canceller = int.Parse(Params[6]);
                    audion16ViewModelL.noise_reduction = int.Parse(Params[7]);
                    audion16ViewModelL.wind_suppression = int.Parse(Params[8]);
                    audion16ViewModelL.input_filter_low_cut = int.Parse(Params[9]);
                    audion16ViewModelL.low_level_expansion = int.Parse(Params[10]);
                    audion16ViewModelL.beq_gain_1 = int.Parse(Params[11]);
                    audion16ViewModelL.beq_gain_2 = int.Parse(Params[12]);
                    audion16ViewModelL.beq_gain_3 = int.Parse(Params[13]);
                    audion16ViewModelL.beq_gain_4 = int.Parse(Params[14]);
                    audion16ViewModelL.beq_gain_5 = int.Parse(Params[15]);
                    audion16ViewModelL.beq_gain_6 = int.Parse(Params[16]);
                    audion16ViewModelL.beq_gain_7 = int.Parse(Params[17]);
                    audion16ViewModelL.beq_gain_8 = int.Parse(Params[18]);
                    audion16ViewModelL.beq_gain_9 = int.Parse(Params[19]);
                    audion16ViewModelL.beq_gain_10 = int.Parse(Params[20]);
                    audion16ViewModelL.beq_gain_11 = int.Parse(Params[21]);
                    audion16ViewModelL.beq_gain_12 = int.Parse(Params[22]);
                    audion16ViewModelL.beq_gain_13 = int.Parse(Params[23]);
                    audion16ViewModelL.beq_gain_14 = int.Parse(Params[24]);
                    audion16ViewModelL.beq_gain_15 = int.Parse(Params[25]);
                    audion16ViewModelL.beq_gain_16 = int.Parse(Params[26]);
                    audion16ViewModelL.mpo_threshold_1 = int.Parse(Params[27]);
                    audion16ViewModelL.mpo_threshold_2 = int.Parse(Params[28]);
                    audion16ViewModelL.mpo_threshold_3 = int.Parse(Params[29]);
                    audion16ViewModelL.mpo_threshold_4 = int.Parse(Params[30]);
                    audion16ViewModelL.mpo_threshold_5 = int.Parse(Params[31]);
                    audion16ViewModelL.mpo_threshold_6 = int.Parse(Params[32]);
                    audion16ViewModelL.mpo_threshold_7 = int.Parse(Params[33]);
                    audion16ViewModelL.mpo_threshold_8 = int.Parse(Params[34]);
                    audion16ViewModelL.mpo_threshold_9 = int.Parse(Params[35]);
                    audion16ViewModelL.mpo_threshold_10 = int.Parse(Params[36]);
                    audion16ViewModelL.mpo_threshold_11 = int.Parse(Params[37]);
                    audion16ViewModelL.mpo_threshold_12 = int.Parse(Params[38]);
                    audion16ViewModelL.mpo_threshold_13 = int.Parse(Params[39]);
                    audion16ViewModelL.mpo_threshold_14 = int.Parse(Params[40]);
                    audion16ViewModelL.mpo_threshold_15 = int.Parse(Params[41]);
                    audion16ViewModelL.mpo_threshold_16 = int.Parse(Params[42]);
                    audion16ViewModelL.mpo_release = int.Parse(Params[43]);
                    audion16ViewModelL.mpo_attack = int.Parse(Params[44]);
                    audion16ViewModelL.comp_ratio_1 = int.Parse(Params[45]);
                    audion16ViewModelL.comp_ratio_2 = int.Parse(Params[46]);
                    audion16ViewModelL.comp_ratio_3 = int.Parse(Params[47]);
                    audion16ViewModelL.comp_ratio_4 = int.Parse(Params[48]);
                    audion16ViewModelL.comp_ratio_5 = int.Parse(Params[49]);
                    audion16ViewModelL.comp_ratio_6 = int.Parse(Params[50]);
                    audion16ViewModelL.comp_ratio_7 = int.Parse(Params[51]);
                    audion16ViewModelL.comp_ratio_8 = int.Parse(Params[52]);
                    audion16ViewModelL.comp_ratio_9 = int.Parse(Params[53]);
                    audion16ViewModelL.comp_ratio_10 = int.Parse(Params[54]);
                    audion16ViewModelL.comp_ratio_11 = int.Parse(Params[55]);
                    audion16ViewModelL.comp_ratio_12 = int.Parse(Params[56]);
                    audion16ViewModelL.comp_ratio_13 = int.Parse(Params[57]);
                    audion16ViewModelL.comp_ratio_14 = int.Parse(Params[58]);
                    audion16ViewModelL.comp_ratio_15 = int.Parse(Params[59]);
                    audion16ViewModelL.comp_ratio_16 = int.Parse(Params[60]);
                    audion16ViewModelL.comp_threshold_1 = int.Parse(Params[61]);
                    audion16ViewModelL.comp_threshold_2 = int.Parse(Params[62]);
                    audion16ViewModelL.comp_threshold_3 = int.Parse(Params[63]);
                    audion16ViewModelL.comp_threshold_4 = int.Parse(Params[64]);
                    audion16ViewModelL.comp_threshold_5 = int.Parse(Params[65]);
                    audion16ViewModelL.comp_threshold_6 = int.Parse(Params[66]);
                    audion16ViewModelL.comp_threshold_7 = int.Parse(Params[67]);
                    audion16ViewModelL.comp_threshold_8 = int.Parse(Params[68]);
                    audion16ViewModelL.comp_threshold_9 = int.Parse(Params[69]);
                    audion16ViewModelL.comp_threshold_10 = int.Parse(Params[70]);
                    audion16ViewModelL.comp_threshold_11 = int.Parse(Params[71]);
                    audion16ViewModelL.comp_threshold_12 = int.Parse(Params[72]);
                    audion16ViewModelL.comp_threshold_13 = int.Parse(Params[73]);
                    audion16ViewModelL.comp_threshold_14 = int.Parse(Params[74]);
                    audion16ViewModelL.comp_threshold_15 = int.Parse(Params[75]);
                    audion16ViewModelL.comp_threshold_16 = int.Parse(Params[76]);
                    audion16ViewModelL.comp_time_consts_1 = int.Parse(Params[77]);
                    audion16ViewModelL.comp_time_consts_2 = int.Parse(Params[78]);
                    audion16ViewModelL.comp_time_consts_3 = int.Parse(Params[79]);
                    audion16ViewModelL.comp_time_consts_4 = int.Parse(Params[80]);
                    audion16ViewModelL.comp_time_consts_5 = int.Parse(Params[81]);
                    audion16ViewModelL.comp_time_consts_6 = int.Parse(Params[82]);
                    audion16ViewModelL.comp_time_consts_7 = int.Parse(Params[83]);
                    audion16ViewModelL.comp_time_consts_8 = int.Parse(Params[84]);
                    audion16ViewModelL.comp_time_consts_9 = int.Parse(Params[85]);
                    audion16ViewModelL.comp_time_consts_10 = int.Parse(Params[86]);
                    audion16ViewModelL.comp_time_consts_11 = int.Parse(Params[87]);
                    audion16ViewModelL.comp_time_consts_12 = int.Parse(Params[88]);
                    audion16ViewModelL.comp_time_consts_13 = int.Parse(Params[89]);
                    audion16ViewModelL.comp_time_consts_14 = int.Parse(Params[90]);
                    audion16ViewModelL.comp_time_consts_15 = int.Parse(Params[91]);
                    audion16ViewModelL.comp_time_consts_16 = int.Parse(Params[92]);

                    audion16ViewModelL.Switch_Mode = int.Parse(Config[0]);
                    audion16ViewModelL.VC_Mode = int.Parse(Config[1]);
                    audion16ViewModelL.VC_Enable = int.Parse(Config[2]);
                    audion16ViewModelL.Auto_Save = int.Parse(Config[3]);
                    audion16ViewModelL.VC_Prompt_Mode = int.Parse(Config[4]);
                    audion16ViewModelL.Program_Prompt_Mode = int.Parse(Config[5]);
                    audion16ViewModelL.Warning_Prompt_Mode = int.Parse(Config[6]);
                    audion16ViewModelL.Power_On_VC = int.Parse(Config[7]);
                    audion16ViewModelL.Power_On_Program = int.Parse(Config[8]);
                    audion16ViewModelL.VC_Num_Steps = int.Parse(Config[9]);
                    audion16ViewModelL.VC_Step_Size = int.Parse(Config[10]);
                    audion16ViewModelL.VC_Analog_Range = int.Parse(Config[11]);
                    audion16ViewModelL.Num_Programs = int.Parse(Config[12]);
                    audion16ViewModelL.Prompt_Level = int.Parse(Config[13]);
                    audion16ViewModelL.Tone_Frequency = int.Parse(Config[14]);
                    audion16ViewModelL.ADir_Sensitivity = int.Parse(Config[15]);
                    audion16ViewModelL.Auto_Telecoil = int.Parse(Config[16]);
                    audion16ViewModelL.Acoustap_Mode = int.Parse(Config[17]);
                    audion16ViewModelL.Acoustap_Sensitivity = int.Parse(Config[18]);
                    audion16ViewModelL.Power_On_Level = int.Parse(Config[19]);
                    audion16ViewModelL.Power_On_Delay = int.Parse(Config[20]);
                    audion16ViewModelL.Noise_Level = int.Parse(Config[21]);
                    audion16ViewModelL.High_Power_Mode = int.Parse(Config[22]);
                    audion16ViewModelL.Dir_Mic_Cal = int.Parse(Config[23]);
                    audion16ViewModelL.Dir_Mic_Cal_Input = int.Parse(Config[24]);
                    audion16ViewModelL.Dir_Spacing = int.Parse(Config[25]);
                    audion16ViewModelL.test = int.Parse(Config[26]);
                    audion16ViewModelL.Output_Filter_Enable = int.Parse(Config[27]);
                    audion16ViewModelL.Output_Filter_1 = int.Parse(Config[28]);
                    audion16ViewModelL.Output_Filter_2 = int.Parse(Config[29]);
                    audion16ViewModelL.Noise_Filter_Ref = int.Parse(Config[30]);
                    audion16ViewModelL.Noise_Filter_1 = int.Parse(Config[31]);
                    audion16ViewModelL.Noise_Filter_2 = int.Parse(Config[32]);
                    audion16ViewModelL.MANF_ID = int.Parse(Config[33]);
                    audion16ViewModelL.Platform_ID = int.Parse(Config[34]);
                    audion16ViewModelL.AlgVer_Build = int.Parse(Config[35]);
                    audion16ViewModelL.AlgVer_Major = int.Parse(Config[36]);
                    audion16ViewModelL.AlgVer_Minor = int.Parse(Config[37]);
                    audion16ViewModelL.ModelID = int.Parse(Config[38]);
                    audion16ViewModelL.MDA_1 = int.Parse(Config[39]);
                    audion16ViewModelL.MDA_2 = int.Parse(Config[40]);
                    audion16ViewModelL.MDA_3 = int.Parse(Config[41]);
                    audion16ViewModelL.MDA_4 = int.Parse(Config[42]);
                    audion16ViewModelL.MDA_5 = int.Parse(Config[43]);
                    audion16ViewModelL.MDA_6 = int.Parse(Config[44]);
                    audion16ViewModelL.MDA_7 = int.Parse(Config[45]);
                    audion16ViewModelL.MDA_8 = int.Parse(Config[46]);
                    audion16ViewModelL.MDA_9 = int.Parse(Config[47]);
                    audion16ViewModelL.MDA_10 = int.Parse(Config[48]);

                    break;

                case "Audion8":

                    audion8ViewModelL.input_mux = int.Parse(Params[0]);
                    audion8ViewModelL.preamp_gain0 = int.Parse(Params[1]);
                    audion8ViewModelL.preamp_gain1 = int.Parse(Params[2]);
                    audion8ViewModelL.C1_Ratio = int.Parse(Params[3]);
                    audion8ViewModelL.C2_Ratio = int.Parse(Params[4]);
                    audion8ViewModelL.C3_Ratio = int.Parse(Params[5]);
                    audion8ViewModelL.C4_Ratio = int.Parse(Params[6]);
                    audion8ViewModelL.C5_Ratio = int.Parse(Params[7]);
                    audion8ViewModelL.C6_Ratio = int.Parse(Params[8]);
                    audion8ViewModelL.C7_Ratio = int.Parse(Params[9]);
                    audion8ViewModelL.C8_Ratio = int.Parse(Params[10]);
                    audion8ViewModelL.C1_TK = int.Parse(Params[11]);
                    audion8ViewModelL.C2_TK = int.Parse(Params[12]);
                    audion8ViewModelL.C3_TK = int.Parse(Params[13]);
                    audion8ViewModelL.C4_TK = int.Parse(Params[14]);
                    audion8ViewModelL.C5_TK = int.Parse(Params[15]);
                    audion8ViewModelL.C6_TK = int.Parse(Params[16]);
                    audion8ViewModelL.C7_TK = int.Parse(Params[17]);
                    audion8ViewModelL.C8_TK = int.Parse(Params[18]);
                    audion8ViewModelL.C1_MPO = int.Parse(Params[19]);
                    audion8ViewModelL.C2_MPO = int.Parse(Params[20]);
                    audion8ViewModelL.C3_MPO = int.Parse(Params[21]);
                    audion8ViewModelL.C4_MPO = int.Parse(Params[22]);
                    audion8ViewModelL.C5_MPO = int.Parse(Params[23]);
                    audion8ViewModelL.C6_MPO = int.Parse(Params[24]);
                    audion8ViewModelL.C7_MPO = int.Parse(Params[25]);
                    audion8ViewModelL.C8_MPO = int.Parse(Params[26]);
                    audion8ViewModelL.BEQ1_gain = int.Parse(Params[27]);
                    audion8ViewModelL.BEQ2_gain = int.Parse(Params[28]);
                    audion8ViewModelL.BEQ3_gain = int.Parse(Params[29]);
                    audion8ViewModelL.BEQ4_gain = int.Parse(Params[30]);
                    audion8ViewModelL.BEQ5_gain = int.Parse(Params[31]);
                    audion8ViewModelL.BEQ6_gain = int.Parse(Params[32]);
                    audion8ViewModelL.BEQ7_gain = int.Parse(Params[33]);
                    audion8ViewModelL.BEQ8_gain = int.Parse(Params[34]);
                    audion8ViewModelL.BEQ9_gain = int.Parse(Params[35]);
                    audion8ViewModelL.BEQ10_gain = int.Parse(Params[36]);
                    audion8ViewModelL.BEQ11_gain = int.Parse(Params[37]);
                    audion8ViewModelL.BEQ12_gain = int.Parse(Params[38]);
                    audion8ViewModelL.matrix_gain = int.Parse(Params[39]);
                    audion8ViewModelL.Noise_Reduction = int.Parse(Params[40]);
                    audion8ViewModelL.FBC_Enable = int.Parse(Params[41]);
                    audion8ViewModelL.Time_Constants = int.Parse(Params[42]);

                    audion8ViewModelL.AutoSave_Enable = int.Parse(Config[0]);
                    audion8ViewModelL.ATC = int.Parse(Config[1]);
                    audion8ViewModelL.EnableHPmode = int.Parse(Config[2]);
                    audion8ViewModelL.Noise_Level = int.Parse(Config[3]);
                    audion8ViewModelL.POL = int.Parse(Config[4]);
                    audion8ViewModelL.POD = int.Parse(Config[5]);
                    audion8ViewModelL.AD_Sens = int.Parse(Config[6]);
                    audion8ViewModelL.Cal_Input = int.Parse(Config[7]);
                    audion8ViewModelL.Dir_Spacing = int.Parse(Config[8]);
                    audion8ViewModelL.Mic_Cal = int.Parse(Config[9]);
                    audion8ViewModelL.number_of_programs = int.Parse(Config[10]);
                    audion8ViewModelL.PGM_Startup = int.Parse(Config[11]);
                    audion8ViewModelL.Switch_Mode = int.Parse(Config[12]);
                    audion8ViewModelL.Program_Prompt_Mode = int.Parse(Config[13]);
                    audion8ViewModelL.Warning_Prompt_Mode = int.Parse(Config[14]);
                    audion8ViewModelL.Tone_Frequency = int.Parse(Config[15]);
                    audion8ViewModelL.Tone_Level = int.Parse(Config[16]);
                    audion8ViewModelL.VC_Enable = int.Parse(Config[17]);
                    audion8ViewModelL.VC_Analog_Range = int.Parse(Config[18]);
                    audion8ViewModelL.VC_Digital_Numsteps = int.Parse(Config[19]);
                    audion8ViewModelL.VC_Digital_Startup = int.Parse(Config[20]);
                    audion8ViewModelL.VC_Digital_Stepsize = int.Parse(Config[21]);
                    audion8ViewModelL.VC_Mode = int.Parse(Config[22]);
                    audion8ViewModelL.VC_pos = int.Parse(Config[23]);
                    audion8ViewModelL.VC_Prompt_Mode = int.Parse(Config[24]);
                    audion8ViewModelL.AlgVer_Major = int.Parse(Config[25]);
                    audion8ViewModelL.AlgVer_Minor = int.Parse(Config[26]);
                    audion8ViewModelL.MANF_ID = int.Parse(Config[27]);
                    audion8ViewModelL.PlatformID = int.Parse(Config[28]);
                    audion8ViewModelL.reserved1 = int.Parse(Config[29]);
                    audion8ViewModelL.reserved2 = int.Parse(Config[30]);
                    audion8ViewModelL.test = int.Parse(Config[31]);
                    audion8ViewModelL.MANF_reserve_1 = int.Parse(Config[32]);
                    audion8ViewModelL.MANF_reserve_2 = int.Parse(Config[33]);
                    audion8ViewModelL.MANF_reserve_3 = int.Parse(Config[34]);
                    audion8ViewModelL.MANF_reserve_4 = int.Parse(Config[35]);
                    audion8ViewModelL.MANF_reserve_5 = int.Parse(Config[36]);
                    audion8ViewModelL.MANF_reserve_6 = int.Parse(Config[37]);
                    audion8ViewModelL.MANF_reserve_7 = int.Parse(Config[38]);
                    audion8ViewModelL.MANF_reserve_8 = int.Parse(Config[39]);
                    audion8ViewModelL.MANF_reserve_9 = int.Parse(Config[40]);
                    audion8ViewModelL.MANF_reserve_10 = int.Parse(Config[41]);

                    break;

                case "Audion6":

                    audion6ViewModelL.ActiveProgram = int.Parse(Params[0]);
                    audion6ViewModelL.BEQ1_gain = int.Parse(Params[1]);
                    audion6ViewModelL.BEQ2_gain = int.Parse(Params[2]);
                    audion6ViewModelL.BEQ3_gain = int.Parse(Params[3]);
                    audion6ViewModelL.BEQ4_gain = int.Parse(Params[4]);
                    audion6ViewModelL.BEQ5_gain = int.Parse(Params[5]);
                    audion6ViewModelL.BEQ6_gain = int.Parse(Params[6]);
                    audion6ViewModelL.BEQ7_gain = int.Parse(Params[7]);
                    audion6ViewModelL.BEQ8_gain = int.Parse(Params[8]);
                    audion6ViewModelL.BEQ9_gain = int.Parse(Params[9]);
                    audion6ViewModelL.BEQ10_gain = int.Parse(Params[10]);
                    audion6ViewModelL.BEQ11_gain = int.Parse(Params[11]);
                    audion6ViewModelL.BEQ12_gain = int.Parse(Params[12]);
                    audion6ViewModelL.C1_ExpTK = int.Parse(Params[13]);
                    audion6ViewModelL.C2_ExpTK = int.Parse(Params[14]);
                    audion6ViewModelL.C3_ExpTK = int.Parse(Params[15]);
                    audion6ViewModelL.C4_ExpTK = int.Parse(Params[16]);
                    audion6ViewModelL.C5_ExpTK = int.Parse(Params[17]);
                    audion6ViewModelL.C6_ExpTK = int.Parse(Params[18]);
                    audion6ViewModelL.C1_MPO = int.Parse(Params[19]);
                    audion6ViewModelL.C2_MPO = int.Parse(Params[20]);
                    audion6ViewModelL.C3_MPO = int.Parse(Params[21]);
                    audion6ViewModelL.C4_MPO = int.Parse(Params[22]);
                    audion6ViewModelL.C5_MPO = int.Parse(Params[23]);
                    audion6ViewModelL.C6_MPO = int.Parse(Params[24]);
                    audion6ViewModelL.C1_Ratio = int.Parse(Params[25]);
                    audion6ViewModelL.C2_Ratio = int.Parse(Params[26]);
                    audion6ViewModelL.C3_Ratio = int.Parse(Params[27]);
                    audion6ViewModelL.C4_Ratio = int.Parse(Params[28]);
                    audion6ViewModelL.C5_Ratio = int.Parse(Params[29]);
                    audion6ViewModelL.C6_Ratio = int.Parse(Params[30]);
                    audion6ViewModelL.C1_TK = int.Parse(Params[31]);
                    audion6ViewModelL.C2_TK = int.Parse(Params[32]);
                    audion6ViewModelL.C3_TK = int.Parse(Params[33]);
                    audion6ViewModelL.C4_TK = int.Parse(Params[34]);
                    audion6ViewModelL.C5_TK = int.Parse(Params[35]);
                    audion6ViewModelL.C6_TK = int.Parse(Params[36]);
                    audion6ViewModelL.Exp_Attack = int.Parse(Params[37]);
                    audion6ViewModelL.Exp_Ratio = int.Parse(Params[38]);
                    audion6ViewModelL.Exp_Release = int.Parse(Params[39]);
                    audion6ViewModelL.FBC_Enable = int.Parse(Params[40]);
                    audion6ViewModelL.input_mux = int.Parse(Params[41]);
                    audion6ViewModelL.matrix_gain = int.Parse(Params[42]);
                    audion6ViewModelL.MPO_Attack = int.Parse(Params[43]);
                    audion6ViewModelL.MPO_Release = int.Parse(Params[44]);
                    audion6ViewModelL.Noise_Reduction = int.Parse(Params[45]);
                    audion6ViewModelL.preamp_gain0 = int.Parse(Params[46]);
                    audion6ViewModelL.preamp_gain1 = int.Parse(Params[47]);
                    audion6ViewModelL.TimeConstants1 = int.Parse(Params[48]);
                    audion6ViewModelL.TimeConstants2 = int.Parse(Params[49]);
                    audion6ViewModelL.TimeConstants3 = int.Parse(Params[50]);
                    audion6ViewModelL.TimeConstants4 = int.Parse(Params[51]);
                    audion6ViewModelL.TimeConstants5 = int.Parse(Params[52]);
                    audion6ViewModelL.TimeConstants6 = int.Parse(Params[53]);
                    audion6ViewModelL.VcPosition = int.Parse(Params[54]);

                    audion6ViewModelL.Auto_Telecoil_Enable = int.Parse(Config[0]);
                    audion6ViewModelL.Cal_Input = int.Parse(Config[1]);
                    audion6ViewModelL.Dir_Spacing = int.Parse(Config[2]);
                    audion6ViewModelL.Low_Battery_Warning = int.Parse(Config[3]);
                    audion6ViewModelL.Mic_Cal = int.Parse(Config[4]);
                    audion6ViewModelL.number_of_programs = int.Parse(Config[5]);
                    audion6ViewModelL.Output_Mode = int.Parse(Config[6]);
                    audion6ViewModelL.Power_On_Delay = int.Parse(Config[7]);
                    audion6ViewModelL.Power_On_Level = int.Parse(Config[8]);
                    audion6ViewModelL.Program_Beep_Enable = int.Parse(Config[9]);
                    audion6ViewModelL.Program_StartUp = int.Parse(Config[10]);
                    audion6ViewModelL.Switch_Mode = int.Parse(Config[11]);
                    audion6ViewModelL.Tone_Frequency = int.Parse(Config[12]);
                    audion6ViewModelL.Tone_Level = int.Parse(Config[13]);
                    audion6ViewModelL.VC_AnalogRange = int.Parse(Config[14]);
                    audion6ViewModelL.VC_Enable = int.Parse(Config[15]);
                    audion6ViewModelL.VC_Mode = int.Parse(Config[16]);
                    audion6ViewModelL.VC_DigitalNumSteps = int.Parse(Config[17]);
                    audion6ViewModelL.VC_StartUp = int.Parse(Config[18]);
                    audion6ViewModelL.VC_DigitalStepSize = int.Parse(Config[19]);

                    break;

                case "Audion4":
                    audion4ViewModelL.BEQ1_gain = int.Parse(Params[0]);
                    audion4ViewModelL.BEQ2_gain = int.Parse(Params[1]);
                    audion4ViewModelL.BEQ3_gain = int.Parse(Params[2]);
                    audion4ViewModelL.BEQ4_gain = int.Parse(Params[3]);
                    audion4ViewModelL.BEQ5_gain = int.Parse(Params[4]);
                    audion4ViewModelL.BEQ6_gain = int.Parse(Params[5]);
                    audion4ViewModelL.BEQ7_gain = int.Parse(Params[6]);
                    audion4ViewModelL.BEQ8_gain = int.Parse(Params[7]);
                    audion4ViewModelL.BEQ9_gain = int.Parse(Params[8]);
                    audion4ViewModelL.BEQ10_gain = int.Parse(Params[9]);
                    audion4ViewModelL.BEQ11_gain = int.Parse(Params[10]);
                    audion4ViewModelL.BEQ12_gain = int.Parse(Params[11]);
                    audion4ViewModelL.C1_Ratio = int.Parse(Params[12]);
                    audion4ViewModelL.C2_Ratio = int.Parse(Params[13]);
                    audion4ViewModelL.C3_Ratio = int.Parse(Params[14]);
                    audion4ViewModelL.C4_Ratio = int.Parse(Params[15]);
                    audion4ViewModelL.Expansion_Enable = int.Parse(Params[16]);
                    audion4ViewModelL.FBC_Enable = int.Parse(Params[17]);
                    audion4ViewModelL.High_Cut = int.Parse(Params[18]);
                    audion4ViewModelL.input_mux = int.Parse(Params[19]);
                    audion4ViewModelL.Low_Cut = int.Parse(Params[20]);
                    audion4ViewModelL.matrix_gain = int.Parse(Params[21]);
                    audion4ViewModelL.MPO_level = int.Parse(Params[22]);
                    audion4ViewModelL.Noise_Reduction = int.Parse(Params[23]);
                    audion4ViewModelL.preamp_gain0 = int.Parse(Params[24]);
                    audion4ViewModelL.preamp_gain1 = int.Parse(Params[25]);
                    audion4ViewModelL.threshold = int.Parse(Params[26]);

                    audion4ViewModelL.ATC = int.Parse(Config[0]);
                    audion4ViewModelL.Auto_Save = int.Parse(Config[1]);
                    audion4ViewModelL.Cal_Input = int.Parse(Config[2]);
                    audion4ViewModelL.Dir_Spacing = int.Parse(Config[3]);
                    audion4ViewModelL.Low_Batt_Warning = int.Parse(Config[4]);
                    audion4ViewModelL.MAP_HC = int.Parse(Config[5]);
                    audion4ViewModelL.MAP_LC = int.Parse(Config[6]);
                    audion4ViewModelL.MAP_MPO = int.Parse(Config[7]);
                    audion4ViewModelL.MAP_TK = int.Parse(Config[8]);
                    audion4ViewModelL.Mic_Cal = int.Parse(Config[9]);
                    audion4ViewModelL.number_of_programs = int.Parse(Config[10]);
                    audion4ViewModelL.Power_On_Level = int.Parse(Config[11]);
                    audion4ViewModelL.Power_On_Delay = int.Parse(Config[12]);
                    audion4ViewModelL.Program_StartUp = int.Parse(Config[13]);
                    audion4ViewModelL.Out_Mode = int.Parse(Config[14]);
                    audion4ViewModelL.Switch_Mode = int.Parse(Config[15]);
                    audion4ViewModelL.Switch_Tone = int.Parse(Config[16]);
                    audion4ViewModelL.T1_DIR = int.Parse(Config[17]);
                    audion4ViewModelL.T2_DIR = int.Parse(Config[18]);
                    audion4ViewModelL.test = int.Parse(Config[19]);
                    audion4ViewModelL.Tone_Frequency = int.Parse(Config[20]);
                    audion4ViewModelL.Tone_Level = int.Parse(Config[21]);
                    audion4ViewModelL.Time_Constants = int.Parse(Config[22]);
                    audion4ViewModelL.VC_AnalogRange = int.Parse(Config[23]);
                    audion4ViewModelL.VC_Beep_Enable = int.Parse(Config[24]);
                    audion4ViewModelL.VC_DigitalNumSteps = int.Parse(Config[25]);
                    audion4ViewModelL.VC_DigitalStepSize = int.Parse(Config[26]);
                    audion4ViewModelL.VC_Enable = int.Parse(Config[27]);
                    audion4ViewModelL.VC_Mode = int.Parse(Config[28]);
                    audion4ViewModelL.VC_Startup = int.Parse(Config[29]);
                    audion4ViewModelL.Active_PGM = int.Parse(Config[30]);
                    audion4ViewModelL.T1_POS = int.Parse(Config[31]);
                    audion4ViewModelL.T2_POS = int.Parse(Config[32]);
                    audion4ViewModelL.VC_Pos = int.Parse(Config[33]);

                    break;

                case "SpinNR":
                    spinNRViewModelL.input_mux = int.Parse(Params[0]);
                    spinNRViewModelL.preamp_gain0 = int.Parse(Params[1]);
                    spinNRViewModelL.preamp_gain1 = int.Parse(Params[2]);
                    spinNRViewModelL.CRL = int.Parse(Params[3]);
                    spinNRViewModelL.CRH = int.Parse(Params[4]);
                    spinNRViewModelL.threshold = int.Parse(Params[5]);
                    spinNRViewModelL.Low_Cut = int.Parse(Params[6]);
                    spinNRViewModelL.High_Cut = int.Parse(Params[7]);
                    spinNRViewModelL.Noise_Reduction = int.Parse(Params[8]);
                    spinNRViewModelL.BEQ1_gain = int.Parse(Params[9]);
                    spinNRViewModelL.BEQ2_gain = int.Parse(Params[10]);
                    spinNRViewModelL.BEQ3_gain = int.Parse(Params[11]);
                    spinNRViewModelL.BEQ4_gain = int.Parse(Params[12]);
                    spinNRViewModelL.BEQ5_gain = int.Parse(Params[13]);
                    spinNRViewModelL.BEQ6_gain = int.Parse(Params[14]);
                    spinNRViewModelL.BEQ7_gain = int.Parse(Params[15]);
                    spinNRViewModelL.BEQ8_gain = int.Parse(Params[16]);
                    spinNRViewModelL.BEQ9_gain = int.Parse(Params[17]);
                    spinNRViewModelL.BEQ10_gain = int.Parse(Params[18]);
                    spinNRViewModelL.BEQ11_gain = int.Parse(Params[19]);
                    spinNRViewModelL.BEQ12_gain = int.Parse(Params[20]);
                    spinNRViewModelL.matrix_gain = int.Parse(Params[21]);
                    spinNRViewModelL.MPO_level = int.Parse(Params[22]);
                    spinNRViewModelL.FBC_Enable = int.Parse(Params[23]);
                    spinNRViewModelL.Cal_Input = int.Parse(Params[24]);
                    spinNRViewModelL.Mic_Cal = int.Parse(Params[25]);

                    spinNRViewModelL.number_of_programs = int.Parse(Config[0]);
                    spinNRViewModelL.VC_MAP = int.Parse(Config[1]);
                    spinNRViewModelL.VC_Range = int.Parse(Config[2]);
                    spinNRViewModelL.VC_pos = int.Parse(Config[3]);
                    spinNRViewModelL.TK_MAP = int.Parse(Config[4]);
                    spinNRViewModelL.HC_MAP = int.Parse(Config[5]);
                    spinNRViewModelL.LC_MAP = int.Parse(Config[6]);
                    spinNRViewModelL.MPO_MAP = int.Parse(Config[7]);
                    spinNRViewModelL.T1_DIR = int.Parse(Config[8]);
                    spinNRViewModelL.T2_DIR = int.Parse(Config[9]);
                    spinNRViewModelL.T3_DIR = int.Parse(Config[10]);
                    spinNRViewModelL.CoilPGM = int.Parse(Config[11]);
                    spinNRViewModelL.MANF_ID = int.Parse(Config[12]);
                    spinNRViewModelL.OutMode = int.Parse(Config[13]);
                    spinNRViewModelL.Switch_Tone = int.Parse(Config[14]);
                    spinNRViewModelL.Low_Batt_Warning = int.Parse(Config[15]);
                    spinNRViewModelL.Tone_Frequency = int.Parse(Config[16]);
                    spinNRViewModelL.Tone_Level = int.Parse(Config[17]);
                    spinNRViewModelL.ATC = int.Parse(Config[18]);
                    spinNRViewModelL.TimeConstants = int.Parse(Config[19]);
                    spinNRViewModelL.Mic_Expansion = int.Parse(Config[20]);
                    spinNRViewModelL.reserved1 = int.Parse(Config[21]);
                    spinNRViewModelL.reserved2 = int.Parse(Config[22]);
                    spinNRViewModelL.reserved3 = int.Parse(Config[23]);
                    spinNRViewModelL.reserved4 = int.Parse(Config[24]);
                    spinNRViewModelL.test = int.Parse(Config[25]);
                    spinNRViewModelL.T1_POS = int.Parse(Config[26]);
                    spinNRViewModelL.T2_POS = int.Parse(Config[27]);
                    spinNRViewModelL.T3_POS = int.Parse(Config[28]);
                    spinNRViewModelL.MANF_reserve_1 = int.Parse(Config[29]);
                    spinNRViewModelL.MANF_reserve_2 = int.Parse(Config[30]);
                    spinNRViewModelL.MANF_reserve_3 = int.Parse(Config[31]);
                    spinNRViewModelL.MANF_reserve_4 = int.Parse(Config[32]);
                    spinNRViewModelL.MANF_reserve_5 = int.Parse(Config[33]);
                    spinNRViewModelL.MANF_reserve_6 = int.Parse(Config[34]);
                    spinNRViewModelL.MANF_reserve_7 = int.Parse(Config[35]);
                    spinNRViewModelL.MANF_reserve_8 = int.Parse(Config[36]);
                    spinNRViewModelL.MANF_reserve_9 = int.Parse(Config[37]);
                    spinNRViewModelL.MANF_reserve_10 = int.Parse(Config[38]);

                    break;
            }
        }

        public void ProgramChipR()
        {
            string hearingAid = Properties.Settings.Default.ChipIDR;

            switch (hearingAid)
            {
                case "Audion16":
                    audion16ViewModelR.Burn();
                    audion16ViewModelR.CloseInterface();
                    break;

                case "Audion8":
                    audion8ViewModelR.Burn();
                    audion8ViewModelR.CloseProgramer();
                    break;

                case "Audion6":
                    audion6ViewModelR.Burn();
                    audion6ViewModelR.CloseProgramer();
                    break;

                case "Audion4":
                    audion4ViewModelR.Burn();
                    audion4ViewModelR.CloseProgramer();
                    break;

                case "SpinNR":
                    spinNRViewModelR.Burn();
                    spinNRViewModelR.CloseProgramer();
                    break;
            }
        }

        public void ProgramChipL()
        {
            string hearingAid = Properties.Settings.Default.ChipIDL;

            switch (hearingAid)
            {
                case "Audion16":
                    audion16ViewModelL.Burn();
                    audion16ViewModelL.CloseInterface();
                    break;

                case "Audion8":
                    audion8ViewModelL.Burn();
                    audion8ViewModelL.CloseProgramer();
                    break;

                case "Audion6":
                    audion6ViewModelL.Burn();
                    audion6ViewModelL.CloseProgramer();
                    break;

                case "Audion4":
                    audion4ViewModelL.Burn();
                    audion4ViewModelL.CloseProgramer();
                    break;

                case "SpinNR":
                    spinNRViewModelL.Burn();
                    spinNRViewModelL.CloseProgramer();
                    break;
            }
        }

        #endregion

        #region Help Functions

        public string GetSN(char side)
        {
            if(side == 'R')
            {
                string[] SNR = recordCalibrationUserControl.TextHearingAidR.SelectedValue.ToString().Split('-');
                return SNR[1];
            }
            else
            {
                string[] SNL = recordCalibrationUserControl.TextHearingAidL.SelectedValue.ToString().Split('-');
                return SNL[1];
            }
        }

        private string GetDeviceFromComboBox(char ear)
        {
            if (ear == 'R')
            {
                if (recordCalibrationUserControl.TextHearingAidR.SelectedIndex >= 0)
                {
                    string selectedText = recordCalibrationUserControl.TextHearingAidR.SelectedItem.ToString();
                    string[] parts = selectedText.Split('-');
                    string device = parts[0].Trim();
                    return device;
                }
                else
                { return ""; }
            }
            else
            {
                if (recordCalibrationUserControl.TextHearingAidL.SelectedIndex >= 0)
                {
                    string selectedText = recordCalibrationUserControl.TextHearingAidL.SelectedItem.ToString();
                    string[] parts = selectedText.Split('-');
                    string device = parts[0].Trim();
                    return device;
                }
                else
                { return ""; }
            }
        }

        private int GetIdFromComboBox(char ear)
        {
            if (ear == 'R')
            {
                if (recordCalibrationUserControl.TextHearingAidR.SelectedIndex >= 0)
                {
                    string selectedText = recordCalibrationUserControl.TextHearingAidR.SelectedItem.ToString();
                    string[] parts = selectedText.Split('-');
                    string serialNumber = parts[1].Trim();
                    return recordCalibrationUserControl.recordCalibrationViewModelR.GetAtributeInt("id", "dbo.hearingaid", "serialnumber", int.Parse(serialNumber));
                }
                else
                { return 0; }
            }
            else
            {
                if (recordCalibrationUserControl.TextHearingAidL.SelectedIndex >= 0)
                {
                    string selectedText = recordCalibrationUserControl.TextHearingAidL.SelectedItem.ToString();
                    Console.WriteLine(selectedText);
                    string[] parts = selectedText.Split('-');
                    string serialNumber = parts[1].Trim();
                    Console.WriteLine(serialNumber);
                    return recordCalibrationUserControl.recordCalibrationViewModelL.GetAtributeInt("id", "dbo.hearingaid", "serialnumber", int.Parse(serialNumber));
                }
                else
                { return 0; }
            }
        }

        public void GetImagem(char ear, string device)
        {
            if (ear == 'R')
            {
                switch (device)
                {
                    case "Landel":
                        recordCalibrationUserControl.ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/LANDELL.png", UriKind.RelativeOrAbsolute));
                        break;

                    case "Dumont":
                        recordCalibrationUserControl.ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/DUMONT.png", UriKind.RelativeOrAbsolute));
                        break;

                    case "Ada":
                        recordCalibrationUserControl.ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        break;

                    case "Mauá":
                        recordCalibrationUserControl.ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/MAUÁ.png", UriKind.RelativeOrAbsolute));
                        break;

                    case "Nise":
                        recordCalibrationUserControl.ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        break;

                    default:
                        recordCalibrationUserControl.ImageHearingAidR.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        break;
                }
            }
            else if (ear == 'L')
            {
                switch (device)
                {
                    case "Landel":
                        recordCalibrationUserControl.ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/LANDELL.png", UriKind.RelativeOrAbsolute));
                        break;

                    case "Dumont":
                        recordCalibrationUserControl.ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/DUMONT.png", UriKind.RelativeOrAbsolute));
                        break;

                    case "Ada":
                        recordCalibrationUserControl.ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        break;

                    case "Mauá":
                        recordCalibrationUserControl.ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/MAUÁ.png", UriKind.RelativeOrAbsolute));
                        break;

                    case "Nise":
                        recordCalibrationUserControl.ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        break;

                    default:
                        recordCalibrationUserControl.ImageHearingAidL.Source = new BitmapImage(new Uri("/Resources/defaultHI2.png", UriKind.RelativeOrAbsolute));
                        break;
                }
            }
        }

        public void ResetAudiogramRecords()
        {
            CurrentContent.Visibility = Visibility.Collapsed;
            CurrentContentImage.Visibility = Visibility.Visible;
            CurrentContent.Content = null;
        }

        public void CleanMessages()
        {
            HandyControl.Controls.Growl.Clear();
            HandyControl.Controls.Growl.ClearGlobal();
        }

        #endregion
    }
}