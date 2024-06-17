using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WaveFit2.Audiogram.ViewModel;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Audiogram.View
{
    public partial class AudiogramUserControl : UserControl
    {
        public Crud crud = new Crud();

        public AudiographUserControl audiogramRight = new AudiographUserControl('R', "Orelha Direita", -1);
        public AudiographUserControl audiogramLeft = new AudiographUserControl('L', "Orelha Esquerda", -1);

        public ExamInformation examInformation = new ExamInformation();

        public AudiogramViewModel audiogramViewModel = new AudiogramViewModel();

        public AudiogramModel audiogram;
        public AudioEvaluationModel audioEvaluation;
        public HealthCenterModel healthCenter;

        public List<AudiogramModel> Audiograms = new List<AudiogramModel>();
        public List<AudioEvaluationModel> AudioEvaluations = new List<AudioEvaluationModel>();
        public List<HealthCenterModel> HealthCenters = new List<HealthCenterModel>();

        public List<SessionModel> Sessions = new List<SessionModel>();
        public List<FrequencyModel> Frequencies = new List<FrequencyModel>();

        public PrintTemplateUserControl printTemplateUserControl;
        public PrintTemplateUserControl printView;

        public int[] IdF = new int[6];
        public int IdA;

        public List<string> HealthCenterItems = new List<string>();
        public List<string> AudiometerItems = new List<string>();

        public string patientName;
        public long patientCode;

        private int type;

        public bool hasChanges = true;
        public bool mask, noanswer = false;

        public AudiogramUserControl()
        {
            InitializeComponent();
            SelectedValues();

            RightAudiograph.Content = audiogramRight;
            LeftAudiograph.Content = audiogramLeft;

            AudiogramTools();

            audiogramRight.SelectedLine(type);
            audiogramLeft.SelectedLine(type);
            Properties.Settings.Default.mask = false;

            FillHealthCenterBox();
        }

        public void FillHealthCenterBox()
        {
            HealthCenterItems.Clear();
            HealthCenterItems = crud.GetAtributeStrings("nickname", "dbo.healthcenter", "status", true);
            HealthCenterBox.ItemsSource = HealthCenterItems.Distinct();
            HealthCenterBox.SelectedIndex = 0;
        }

        public void FillAudiometerBox()
        {
            List<int> list = new List<int>();
            list = crud.GetAtributeListInt("idaudiometer", "dbo.healthcenter", "nickname", HealthCenterBox.SelectedValue.ToString(), "status", true);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
                var codes = crud.GetAtributeString("code", "dbo.audiometer", "id", list[i]);
                var models = crud.GetAtributeString("model", "dbo.audiometer", "id", list[i]);
                var maintenances = crud.GetAtributeDate("maintenance", "dbo.audiometer", "id", list[i]);

                var item = $"{codes}-{models}-{maintenances.Date.ToString("dd/MM/yyyy")}";
                AudiometerItems.Add(item);
                AudiometerBox.Items.Add(AudiometerItems[i]);
            }
            AudiometerBox.SelectedIndex = 0;
        }

        public void AudiogramTools()
        {
            VA.Click += VA_Click;
            VO.Click += VO_Click;
            UCL.Click += UCL_Click;

            CopyLtoR.Click += CopyLtoR_Click;
            CopyRtoL.Click += CopyRtoL_Click;

            DeleteR.Click += DeleteR_Click;
            DeleteL.Click += DeleteL_Click;

            NextButton.Click += NextButton_Click;
            BackButton.Click += BackButton_Click;

            FindPatientButton.Click += FindPatientButton_Click;
            SelectedPatient.TextChanged += SelectedPatient_TextChanged;

            CurrentStep.StepChanged += CurrentStep_StepChanged;

            SaveButton.Click += SaveButton_Click;
            PrintButton.Click += PrintButton_Click;

            MaskButton.Click += MaskButton_Click;
            NoAnswerButton.Click += NoAnswerButton_Click;

            HealthCenterBox.SelectionChanged += HealthCenterBox_SelectionChanged;

            TextChangedEventHandlers();
        }

        private void HealthCenterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AudiometerBox.Items.Clear();
            AudiometerItems.Clear();
            FillAudiometerBox();
        }

        public void FindIcon(bool mask, bool noanswer)
        {
            if (!mask && !noanswer)
            {
                VAIcons.Visibility = Visibility.Visible;
                VANoAnswerIcons.Visibility = Visibility.Hidden;
                VAMaskIcons.Visibility = Visibility.Hidden;
                VAMaskNoAnswerIcons.Visibility = Visibility.Hidden;

                VOIcon.Visibility = Visibility.Hidden;
                VOImage.Visibility = Visibility.Visible;

                RVOimage.Source = new BitmapImage(new Uri(@"/Resources/AudiometrySymbols/RVO.png", UriKind.RelativeOrAbsolute));
                RVOimage.Width = 20;
                RVOimage.Height = 20;

                LVOimage.Source = new BitmapImage(new Uri(@"/Resources/AudiometrySymbols/LVO.png", UriKind.RelativeOrAbsolute));
                LVOimage.Width = 20;
                LVOimage.Height = 20;
            }
            else if (!mask && noanswer)
            {
                VAIcons.Visibility = Visibility.Hidden;
                VANoAnswerIcons.Visibility = Visibility.Visible;
                VAMaskIcons.Visibility = Visibility.Hidden;
                VAMaskNoAnswerIcons.Visibility = Visibility.Hidden;

                VOIcon.Visibility = Visibility.Hidden;
                VOImage.Visibility = Visibility.Visible;

                RVOimage.Source = new BitmapImage(new Uri(@"/Resources/AudiometrySymbols/RVONoAnswer.png", UriKind.RelativeOrAbsolute));
                RVOimage.Width = 20;
                RVOimage.Height = 20;

                LVOimage.Source = new BitmapImage(new Uri(@"/Resources/AudiometrySymbols/LVONoAnswer.png", UriKind.RelativeOrAbsolute));
                LVOimage.Width = 20;
                LVOimage.Height = 20;
            }
            else if (mask && !noanswer)
            {
                VAIcons.Visibility = Visibility.Hidden;
                VANoAnswerIcons.Visibility = Visibility.Hidden;
                VAMaskIcons.Visibility = Visibility.Visible;
                VAMaskNoAnswerIcons.Visibility = Visibility.Hidden;

                VOIcon.Visibility = Visibility.Hidden;
                VOImage.Visibility = Visibility.Visible;

                RVOimage.Source = new BitmapImage(new Uri(@"/Resources/AudiometrySymbols/RVOMask.png", UriKind.RelativeOrAbsolute));
                RVOimage.Width = 20;
                RVOimage.Height = 20;

                LVOimage.Source = new BitmapImage(new Uri(@"/Resources/AudiometrySymbols/LVOMask.png", UriKind.RelativeOrAbsolute));
                LVOimage.Width = 20;
                LVOimage.Height = 20;
            }
            else if (mask && noanswer)
            {
                VAIcons.Visibility = Visibility.Hidden;
                VANoAnswerIcons.Visibility = Visibility.Hidden;
                VAMaskIcons.Visibility = Visibility.Hidden;
                VAMaskNoAnswerIcons.Visibility = Visibility.Visible;

                VOIcon.Visibility = Visibility.Hidden;
                VOImage.Visibility = Visibility.Visible;

                RVOimage.Source = new BitmapImage(new Uri(@"/Resources/AudiometrySymbols/RVOMaksNoAnswer.png", UriKind.RelativeOrAbsolute));
                RVOimage.Width = 20;
                RVOimage.Height = 20;

                LVOimage.Source = new BitmapImage(new Uri(@"/Resources/AudiometrySymbols/LVOMaskNoAnswer.png", UriKind.RelativeOrAbsolute));
                LVOimage.Width = 20;
                LVOimage.Height = 20;
            }
        }

        private void NoAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            noanswer = !noanswer;
            Properties.Settings.Default.noAnswer = noanswer;
            FindIcon(Properties.Settings.Default.mask, Properties.Settings.Default.noAnswer);
        }

        private void MaskButton_Click(object sender, RoutedEventArgs e)
        {
            mask = !mask;
            Properties.Settings.Default.mask = mask;
            FindIcon(Properties.Settings.Default.mask, Properties.Settings.Default.noAnswer);
        }

        private void CurrentStep_StepChanged(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            bool isPatientIdZero = Properties.Settings.Default.patientId == 0;
            switch (CurrentStep.StepIndex)
            {
                case 0:

                    BackButton.Visibility = Visibility.Hidden;
                    NextButton.Visibility = isPatientIdZero ? Visibility.Hidden : Visibility.Visible;
                    SaveButton.Visibility = Visibility.Hidden;
                    PrintButton.Visibility = Visibility.Hidden;
                    CurrentContent.Content = null;

                    break;

                case 1:
                    AudiogramInvisible();
                    BackButton.Visibility = isPatientIdZero ? Visibility.Hidden : Visibility.Visible;
                    NextButton.Visibility = Visibility.Visible;
                    SaveButton.Visibility = Visibility.Hidden;
                    PrintButton.Visibility = Visibility.Hidden;
                    examInformation.MeatoscopyStep();
                    CurrentContent.Content = examInformation;

                    break;

                case 2:
                    AudiogramVisible();
                    CurrentContent.Content = null;
                    break;

                case 3:
                    AudiogramInvisible();
                    examInformation.LogoStep();
                    examInformation.logoUserControl.ODInt.Text = TritonalMean(audiogramRight).ToString();
                    examInformation.logoUserControl.OEInt.Text = TritonalMean(audiogramLeft).ToString();
                    CurrentContent.Content = examInformation;
                    break;

                case 4:
                    examInformation.MaskStep();
                    CurrentContent.Content = examInformation;
                    break;

                case 5:
                    examInformation.ObsStep();
                    CurrentContent.Content = examInformation;
                    NextButton.Visibility = Visibility.Visible;
                    SaveButton.Visibility = Visibility.Hidden;
                    PrintButton.Visibility = Visibility.Hidden;
                    break;

                case 6:
                    examInformation.FinalStep();
                    CurrentContent.Content = examInformation;
                    if (hasChanges)
                    {
                        NextButton.Visibility = Visibility.Hidden;
                        SaveButton.Visibility = Visibility.Visible;
                        PrintButton.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        NextButton.Visibility = Visibility.Visible;
                        SaveButton.Visibility = Visibility.Hidden;
                        PrintButton.Visibility = Visibility.Hidden;
                    }
                    PrintContent.Content = null;
                    BackButton.IsEnabled = true;
                    break;

                case 7:
                    NextButton.Visibility = Visibility.Hidden;
                    SaveButton.Visibility = Visibility.Hidden;
                    PrintButton.Visibility = Visibility.Visible;
                    hasChanges = false;

                    printTemplateUserControl = new PrintTemplateUserControl(270, 330);
                    printView = new PrintTemplateUserControl(270, 330);
                    FillPrintTemplate(printTemplateUserControl);
                    FillPrintTemplate(printView);
                    ScrollViewer printScrollViewer = new ScrollViewer();
                    printScrollViewer.Content = printView;
                    printScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    printScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    printScrollViewer.CanContentScroll = true;
                    CurrentContent.Content = printScrollViewer;

                    break;
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();

            if (printDlg.ShowDialog() == true)
            {
                printDlg.PrintVisual(printTemplateUserControl, "Audiometria");
            }
        }

        public string TritonalMean(AudiographUserControl audiograph)
        {
            if (!double.IsNaN(audiograph.audiogramViewModel.dictDataPoints[0][2].Y) &&
               !double.IsNaN(audiograph.audiogramViewModel.dictDataPoints[0][4].Y) &&
               !double.IsNaN(audiograph.audiogramViewModel.dictDataPoints[0][6].Y))

            {
                double tritonal = ((audiograph.audiogramViewModel.dictDataPoints[0][2].Y +
                                    audiograph.audiogramViewModel.dictDataPoints[0][4].Y +
                                    audiograph.audiogramViewModel.dictDataPoints[0][6].Y) / 3) + 40;

                return (Math.Round(tritonal / 5) * 5).ToString();
            }
            return "";
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
            try
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
                    print.ExamDate.Text = today.ToShortDateString();
                }

                foreach (HealthCenterModel healthCenter in crud.GetHealthCenter(crud.GetAtributeInt("id", "dbo.healthcenter", "nickname", HealthCenterBox.SelectedValue.ToString())))
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

                foreach (PatientModel patient in crud.GetPatient(Properties.Settings.Default.patientCode))
                {
                    print.PatientName.Text = $"{patient.Name} {patient.Surname}";
                    DateTime today = DateTime.Today;
                    int age = today.Year - patient.Birthday.Year;
                    if (patient.Birthday > today.AddYears(-age)) age--;
                    print.PatientAge.Text = age.ToString();
                    print.PatientGender.Text = patient.Gender.ToString();
                    print.PatientID.Text = $"{patient.NumDocument} ({patient.TypeDocument})";
                    print.ExamDate.Text = today.ToShortDateString();
                }

                foreach (AudioEvaluationModel evaluationModel in
                    crud.GetAudioEvaluation(crud.GetAtributeInt("idaudioevaluation", "dbo.session", "idaudiogram", Properties.Settings.Default.audiogramId)))
                {
                    print.ODMeatoscopy.Text = evaluationModel.RightMeatoscopy;
                    print.OEMeatoscopy.Text = evaluationModel.LeftMeatoscopy;

                    print.ODLRF.Text = $"{evaluationModel.RightEarLRF} dB";
                    print.ODLAF.Text = $"{evaluationModel.RightEarLAF} dB";
                    print.OELRF.Text = $"{evaluationModel.LeftEarLRF} dB";
                    print.OELAF.Text = $"{evaluationModel.LeftEarLAF} dB";

                    print.ODIntensid.Text = $"{evaluationModel.RightEarIntesity} dB";
                    print.OEIntensid.Text = $"{evaluationModel.LeftEarIntesity} dB";

                    print.NWordsMono.Text = evaluationModel.WordsMono;
                    print.ODMono.Text = $"{evaluationModel.ODMono} %";
                    print.OEMono.Text = $"{evaluationModel.OEMono} %";

                    print.NWordsDi.Text = evaluationModel.WordsDi;
                    print.ODDi.Text = $"{evaluationModel.ODDi} %";
                    print.OEDi.Text = $"{evaluationModel.OEDi} %";

                    print.NWordsTri.Text = evaluationModel.WordsTri;
                    print.ODTri.Text = $"{evaluationModel.ODTri} %";
                    print.OETri.Text = $"{evaluationModel.OETri} %";

                    print.ODVAMIN.Text = evaluationModel.RightEarVAMin;
                    print.ODVAMAX.Text = evaluationModel.RightEarVAMax;
                    print.ODVOMIN.Text = evaluationModel.RightEarVOMin;
                    print.ODVOMAX.Text = evaluationModel.RightEarVOMax;
                    print.ODLogo.Text = evaluationModel.RightEarLogo;

                    print.OEVAMIN.Text = evaluationModel.LeftEarVAMin;
                    print.OEVAMAX.Text = evaluationModel.LeftEarVAMax;
                    print.OEVOMIN.Text = evaluationModel.LeftEarVOMin;
                    print.OEVOMAX.Text = evaluationModel.LeftEarVOMax;
                    print.OELogo.Text = evaluationModel.LeftEarLogo;

                    print.Observations.Text = evaluationModel.Obs;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentStep.StepIndex -= 1;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentStep.StepIndex += 1;
        }

        private void FindPatientButton_Click(object sender, RoutedEventArgs e)
        {
            AudiogramSelectForm audiogramSelectForm = new AudiogramSelectForm();
            audiogramSelectForm.CloseButton.Click += AudiogramSelectFormCloseButton_Click;
            audiogramSelectForm.SelectButton.Click += AudiogramSelectFormSelectButton_Click; ;
            audiogramSelectForm.ShowDialog();
        }

        private void AudiogramSelectFormSelectButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedValues();
        }

        private void AudiogramSelectFormCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (patientCode == 0)
            {
                AudiogramInvisible();
            }
        }

        private void SelectedPatient_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResetAll();

            for (int i = 0; i < 3; i++)
            {
                audiogramLeft.DeleteLine(i);
                audiogramRight.DeleteLine(i);
            }

            CurrentStep.StepIndex = 1;
        }

        public void SelectedValues()
        {
            patientCode = Properties.Settings.Default.patientCode;
            patientName = Properties.Settings.Default.patientName;
            SelectedPatient.Text = patientName;
        }

        public void AudiogramVisible()
        {
            AudiographGrid.Visibility = Visibility.Visible;
        }

        public void AudiogramInvisible()
        {
            AudiographGrid.Visibility = Visibility.Hidden;
        }

        private void DeleteL_Click(object sender, RoutedEventArgs e)
        {
            audiogramLeft.DeleteLine(type);
        }

        private void DeleteR_Click(object sender, RoutedEventArgs e)
        {
            audiogramRight.DeleteLine(type);
        }

        private void CopyRtoL_Click(object sender, RoutedEventArgs e)
        {
            audiogramLeft.audiogramViewModel.lineArray[type].Points.Clear();
            switch (type)
            {
                case 0:

                    for (int j = 0; j < 4; j++)
                    {
                        audiogramLeft.audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;

                case 1:

                    for (int j = 4; j < 8; j++)
                    {
                        audiogramLeft.audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;

                case 2:

                    for (int j = 8; j < 9; j++)
                    {
                        audiogramLeft.audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;
            }

            audiogramLeft.audiogramViewModel.dictKeys[type] = audiogramRight.audiogramViewModel.dictKeys[type].ToArray();
            audiogramLeft.audiogramViewModel.dictDataPoints[type] = audiogramRight.audiogramViewModel.dictDataPoints[type].ToArray();
            audiogramLeft.audiogramViewModel.dictScatterPoints[type] = audiogramRight.audiogramViewModel.dictScatterPoints[type].ToArray();
            audiogramLeft.audiogramViewModel.dictMarkerPoints[type] = audiogramRight.audiogramViewModel.dictMarkerPoints[type].ToArray();

            for (int i = 0; i < 11; i++)
            {
                if (audiogramLeft.audiogramViewModel.dictKeys[type][i] != 0)
                {
                    audiogramLeft.audiogramViewModel.lineArray[type].Points.Add(audiogramLeft.audiogramViewModel.dictDataPoints[type][i]);
                    audiogramLeft.audiogramViewModel.markerArray[audiogramLeft.audiogramViewModel.dictMarkerPoints[type][i]].Points.Add(audiogramLeft.audiogramViewModel.dictScatterPoints[type][i]);

                    audiogramLeft.PointToText(i + 1, false);
                }
                else
                {
                    audiogramLeft.PointToText(i + 1, true);
                }
            }

            audiogramLeft.audiogramViewModel.AudiogramPlot.InvalidatePlot(true);
        }

        private void CopyLtoR_Click(object sender, RoutedEventArgs e)
        {
            audiogramRight.audiogramViewModel.lineArray[type].Points.Clear();
            switch (type)
            {
                case 0:

                    for (int j = 0; j < 4; j++)
                    {
                        audiogramRight.audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;

                case 1:

                    for (int j = 4; j < 8; j++)
                    {
                        audiogramRight.audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;

                case 2:

                    for (int j = 8; j < 9; j++)
                    {
                        audiogramRight.audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;
            }

            audiogramRight.audiogramViewModel.dictKeys[type] = audiogramLeft.audiogramViewModel.dictKeys[type].ToArray();
            audiogramRight.audiogramViewModel.dictDataPoints[type] = audiogramLeft.audiogramViewModel.dictDataPoints[type].ToArray();
            audiogramRight.audiogramViewModel.dictScatterPoints[type] = audiogramLeft.audiogramViewModel.dictScatterPoints[type].ToArray();
            audiogramRight.audiogramViewModel.dictMarkerPoints[type] = audiogramLeft.audiogramViewModel.dictMarkerPoints[type].ToArray();

            for (int i = 0; i < 11; i++)
            {
                if (audiogramRight.audiogramViewModel.dictKeys[type][i] != 0)
                {
                    audiogramRight.audiogramViewModel.lineArray[type].Points.Add(audiogramRight.audiogramViewModel.dictDataPoints[type][i]);
                    audiogramRight.audiogramViewModel.markerArray[audiogramRight.audiogramViewModel.dictMarkerPoints[type][i]].Points.Add(audiogramRight.audiogramViewModel.dictScatterPoints[type][i]);
                    audiogramRight.PointToText(i + 1, false);
                }
                else
                {
                    audiogramRight.PointToText(i + 1, true);
                }
            }

            audiogramRight.audiogramViewModel.AudiogramPlot.InvalidatePlot(true);
        }

        public void UCL_Click(object sender, RoutedEventArgs e)
        {
            type = 2;
            audiogramRight.type = type;
            audiogramLeft.type = type;

            audiogramRight.SelectedLine(type);
            audiogramLeft.SelectedLine(type);
        }

        public void VO_Click(object sender, RoutedEventArgs e)
        {
            type = 1;
            audiogramRight.type = type;
            audiogramLeft.type = type;

            audiogramRight.SelectedLine(type);
            audiogramLeft.SelectedLine(type);
        }

        public void VA_Click(object sender, RoutedEventArgs e)
        {
            type = 0;
            audiogramRight.type = type;
            audiogramLeft.type = type;

            audiogramRight.SelectedLine(type);
            audiogramLeft.SelectedLine(type);
        }

        private void FindIdF(AudiographUserControl audiograph, int type, int idF)
        {
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][0].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][0]}", IdF[idF], "hz125");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][1].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][1]}", IdF[idF], "hz250");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][2].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][2]}", IdF[idF], "hz500");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][3].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][3]}", IdF[idF], "hz750");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][4].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][4]}", IdF[idF], "hz1000");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][5].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][5]}", IdF[idF], "hz1500");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][6].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][6]}", IdF[idF], "hz2000");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][7].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][7]}", IdF[idF], "hz3000");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][8].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][8]}", IdF[idF], "hz4000");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][9].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][9]}", IdF[idF], "hz6000");
            audiogramViewModel.UpdateColString("dbo.frequency", $"{audiograph.audiogramViewModel.dictDataPoints[type][10].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][10]}", IdF[idF], "hz8000");
        }

        private void SetDBFrequency(AudiographUserControl audiograph, string ear, int type, int t)
        {
            var convert = "";
            if (type == 0) { convert = "VA"; }
            if (type == 1) { convert = "VO"; }
            if (type == 2) { convert = "UCL"; }

            Frequencies = new List<FrequencyModel>
                {
                    new FrequencyModel
                    {
                        _125Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][0].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][0]}",
                        _250Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][1].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][1]}",
                        _500Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][2].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][2]}",
                        _750Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][3].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][3]}",
                        _1000Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][4].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][4]}",
                        _1500Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][5].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][5]}",
                        _2000Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][6].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][6]}",
                        _3000Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][7].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][7]}",
                        _4000Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][8].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][8]}",
                        _6000Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][9].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][9]}",
                        _8000Hz = $"{audiograph.audiogramViewModel.dictDataPoints[type][10].Y}&{audiograph.audiogramViewModel.dictMarkerPoints[type][10]}",
                        Ear = ear,
                        Type = convert,
                        Audiogram = Audiograms[0]
                    }
                };

            audiogramViewModel.AddFrequencies(Frequencies);
            IdF[t] = Frequencies[0].Id;
        }

        private string ExtractCodes(string input)
        {
            // Divide a string usando o caractere '-' como separador
            string[] parts = input.Split('-');

            // Retorna a primeira parte da string (codes)
            return parts[0];
        }

        private void SetDBSession()
        {
            Sessions = new List<SessionModel>
            {
                new SessionModel
                {
                    Audiogram = Audiograms[0],
                    IdPatient = Properties.Settings.Default.patientId,
                    IdUser = Properties.Settings.Default.userId,
                    IdAudioEvaluation = audioEvaluation.Id,
                    IdHealthCenter = crud.GetAtributeInt("id", "dbo.healthcenter", "idaudiometer",
                    crud.GetAtributeInt("id", "dbo.audiometer", "code", ExtractCodes(AudiometerBox.SelectedValue.ToString())))
                }
            };
            audiogramViewModel.AddSession(Sessions);
        }

        private void UpdateSession()
        {
            audiogramViewModel.UpdateColInt("dbo.session", crud.GetAtributeInt("id", "dbo.healthcenter", "nickname", HealthCenterBox.SelectedValue.ToString()), Sessions[0].Id, "idhealthcenter");
        }

        public void SetDBAudiogram()
        {
            if (!Audiograms.Contains(audiogram))
            {
                Audiograms.Add(audiogram);
                audiogramViewModel.AddAudiogram(Audiograms);
            }
            else
            {
            }
        }

        public void SaveAudiogram()
        {
            SetDBAudiogram();

            if (IdF.Any(x => x == 0))
            {
                for (int t = 0; t < 3; t++)
                {
                    SetDBFrequency(audiogramRight, "R", t, t);
                    SetDBFrequency(audiogramLeft, "L", t, t + 3);
                }
                SetDBSession();
            }
            else
            {
                for (int t = 0; t < 3; t++)
                {
                    FindIdF(audiogramRight, t, t);
                    FindIdF(audiogramLeft, t, t + 3);
                }
                UpdateSession();
            }
        }

        public string Percentage(string over, string under)
        {
            try
            {
                double numberUnder = double.Parse(under);
                double numberOver = double.Parse(over);

                if (numberOver > numberUnder)
                {
                    numberOver = numberUnder;
                    double result = Math.Round(((numberOver * 100) / numberUnder), 2);
                    return result.ToString();
                }
                else
                {
                    double result = Math.Round(((numberOver * 100) / numberUnder), 2);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public void SaveAudioEvaluation()
        {
            audioEvaluation.LeftMeatoscopy = examInformation.meatoscopyUserControl.OEMeatoscopy.Text.ToString();
            audioEvaluation.RightMeatoscopy = examInformation.meatoscopyUserControl.ODMeatoscopy.Text.ToString();

            audioEvaluation.LeftEarLRF = examInformation.logoUserControl.OELRF.Text.ToString();
            audioEvaluation.RightEarLRF = examInformation.logoUserControl.ODLRF.Text.ToString();
            audioEvaluation.LeftEarLAF = examInformation.logoUserControl.OELAF.Text.ToString();
            audioEvaluation.RightEarLAF = examInformation.logoUserControl.ODLAF.Text.ToString();
            audioEvaluation.LeftEarIntesity = examInformation.logoUserControl.OEInt.Text.ToString();
            audioEvaluation.RightEarIntesity = examInformation.logoUserControl.ODInt.Text.ToString();
            audioEvaluation.WordsMono = examInformation.logoUserControl.WordsMono.Text.ToString();
            audioEvaluation.WordsDi = examInformation.logoUserControl.WordsDi.Text.ToString();
            audioEvaluation.WordsTri = examInformation.logoUserControl.WordsTri.Text.ToString();
            audioEvaluation.OEMono = Percentage(examInformation.logoUserControl.OEMono.Text, examInformation.logoUserControl.WordsMono.Text);
            audioEvaluation.OEDi = Percentage(examInformation.logoUserControl.OEDi.Text, examInformation.logoUserControl.WordsDi.Text);
            audioEvaluation.OETri = Percentage(examInformation.logoUserControl.OETri.Text, examInformation.logoUserControl.WordsTri.Text);
            audioEvaluation.ODMono = Percentage(examInformation.logoUserControl.ODMono.Text, examInformation.logoUserControl.WordsMono.Text);
            audioEvaluation.ODDi = Percentage(examInformation.logoUserControl.ODDi.Text, examInformation.logoUserControl.WordsDi.Text);
            audioEvaluation.ODTri = Percentage(examInformation.logoUserControl.ODTri.Text, examInformation.logoUserControl.WordsTri.Text);

            audioEvaluation.LeftEarVAMin = examInformation.maskUserControl.OEVAMIN.Text.ToString();
            audioEvaluation.LeftEarVAMax = examInformation.maskUserControl.OEVAMAX.Text.ToString();
            audioEvaluation.LeftEarVOMin = examInformation.maskUserControl.OEVOMIN.Text.ToString();
            audioEvaluation.LeftEarVOMax = examInformation.maskUserControl.OEVOMAX.Text.ToString();
            audioEvaluation.LeftEarLogo = examInformation.maskUserControl.OEQUANT.Text.ToString();
            audioEvaluation.RightEarVAMin = examInformation.maskUserControl.ODVAMIN.Text.ToString();
            audioEvaluation.RightEarVAMax = examInformation.maskUserControl.ODVAMAX.Text.ToString();
            audioEvaluation.RightEarVOMin = examInformation.maskUserControl.ODVOMIN.Text.ToString();
            audioEvaluation.RightEarVOMax = examInformation.maskUserControl.ODVOMAX.Text.ToString();
            audioEvaluation.RightEarLogo = examInformation.maskUserControl.ODQUANT.Text.ToString();

            audioEvaluation.Obs = examInformation.obsUserControl.Obs.Text;

            if (!AudioEvaluations.Contains(audioEvaluation))
            {
                AudioEvaluations.Add(audioEvaluation);
                crud.AddAudioEvaluation(AudioEvaluations);
            }
            else
            {
                crud.UpdateColString("dbo.audioevaluation", examInformation.meatoscopyUserControl.OEMeatoscopy.Text, audioEvaluation.Id, "leftmeatoscopy");
                crud.UpdateColString("dbo.audioevaluation", examInformation.meatoscopyUserControl.ODMeatoscopy.Text, audioEvaluation.Id, "rightmeatoscopy");

                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.OELRF.Text, audioEvaluation.Id, "leftearlrf");
                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.ODLRF.Text, audioEvaluation.Id, "rightearlrf");
                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.OELAF.Text, audioEvaluation.Id, "leftearlaf");
                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.ODLAF.Text, audioEvaluation.Id, "rightearlaf");
                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.OEInt.Text, audioEvaluation.Id, "leftearintesity");
                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.ODInt.Text, audioEvaluation.Id, "rightearintesity");
                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.WordsMono.Text, audioEvaluation.Id, "wordsmono");
                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.WordsDi.Text, audioEvaluation.Id, "wordsdi");
                crud.UpdateColString("dbo.audioevaluation", examInformation.logoUserControl.WordsTri.Text, audioEvaluation.Id, "wordstri");
                crud.UpdateColString("dbo.audioevaluation", Percentage(examInformation.logoUserControl.OEMono.Text, examInformation.logoUserControl.WordsMono.Text), audioEvaluation.Id, "leftearmono");
                crud.UpdateColString("dbo.audioevaluation", Percentage(examInformation.logoUserControl.OEDi.Text, examInformation.logoUserControl.WordsDi.Text), audioEvaluation.Id, "lefteardi");
                crud.UpdateColString("dbo.audioevaluation", Percentage(examInformation.logoUserControl.OETri.Text, examInformation.logoUserControl.WordsTri.Text), audioEvaluation.Id, "lefteartri");
                crud.UpdateColString("dbo.audioevaluation", Percentage(examInformation.logoUserControl.ODMono.Text, examInformation.logoUserControl.WordsMono.Text), audioEvaluation.Id, "rightearmono");
                crud.UpdateColString("dbo.audioevaluation", Percentage(examInformation.logoUserControl.ODDi.Text, examInformation.logoUserControl.WordsDi.Text), audioEvaluation.Id, "righteardi");
                crud.UpdateColString("dbo.audioevaluation", Percentage(examInformation.logoUserControl.ODTri.Text, examInformation.logoUserControl.WordsTri.Text), audioEvaluation.Id, "righteartri");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.OEVAMIN.Text, audioEvaluation.Id, "leftearvamin");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.OEVAMAX.Text, audioEvaluation.Id, "leftearvamax");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.OEVOMIN.Text, audioEvaluation.Id, "leftearvomin");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.OEVOMAX.Text, audioEvaluation.Id, "leftearvomax");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.OEQUANT.Text, audioEvaluation.Id, "leftearlogo");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.ODVAMIN.Text, audioEvaluation.Id, "rightearvamin");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.ODVAMAX.Text, audioEvaluation.Id, "rightearvamax");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.ODVOMIN.Text, audioEvaluation.Id, "rightearvomin");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.ODVOMAX.Text, audioEvaluation.Id, "rightearvomax");
                crud.UpdateColString("dbo.audioevaluation", examInformation.maskUserControl.ODQUANT.Text, audioEvaluation.Id, "rightearlogo");

                crud.UpdateColString("dbo.audioevaluation", examInformation.obsUserControl.Obs.Text, audioEvaluation.Id, "obs");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveAudioEvaluation();
            SaveAudiogram();
            HandyControl.Controls.Growl.Success("Avaliação Audiológica salva com sucesso.");
            Properties.Settings.Default.audiogramId = Audiograms[0].Id;
            CurrentStep.StepIndex += 1;
        }

        private void TextChangedEventHandlers()
        {
            HealthCenterBox.SelectionChanged += ComboBox_SelectionChanged;
            AudiometerBox.SelectionChanged += ComboBox_SelectionChanged;

            examInformation.meatoscopyUserControl.OEMeatoscopy.SelectionChanged += ComboBox_SelectionChanged;
            examInformation.meatoscopyUserControl.ODMeatoscopy.SelectionChanged += ComboBox_SelectionChanged;

            examInformation.logoUserControl.OELRF.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.ODLRF.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.OELAF.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.ODLAF.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.OEInt.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.ODInt.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.WordsMono.SelectionChanged += ComboBox_SelectionChanged;
            examInformation.logoUserControl.WordsDi.SelectionChanged += ComboBox_SelectionChanged;
            examInformation.logoUserControl.WordsTri.SelectionChanged += ComboBox_SelectionChanged;
            examInformation.logoUserControl.OEMono.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.OEDi.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.OETri.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.ODMono.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.ODDi.TextChanged += TextBox_TextChanged;
            examInformation.logoUserControl.ODTri.TextChanged += TextBox_TextChanged;

            examInformation.maskUserControl.OEVAMIN.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.OEVAMAX.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.OEVOMIN.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.OEVOMAX.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.OEQUANT.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.ODVAMIN.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.ODVAMAX.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.ODVOMIN.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.ODVOMAX.TextChanged += TextBox_TextChanged;
            examInformation.maskUserControl.ODQUANT.TextChanged += TextBox_TextChanged;

            examInformation.obsUserControl.Obs.TextChanged += TextBox_TextChanged;
        }

        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hasChanges = true;
            if (CurrentStep.StepIndex == 6)
            {
                NextButton.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Visible;
                PrintButton.Visibility = Visibility.Hidden;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hasChanges = true;
            if (CurrentStep.StepIndex == 6)
            {
                NextButton.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Visible;
                PrintButton.Visibility = Visibility.Hidden;
            }
        }

        public void ResetAll()
        {
            audiogram = new AudiogramModel();

            audioEvaluation = new AudioEvaluationModel();

            audiogram.Date = DateTime.UtcNow;

            for (int i = 0; i < 3; i++)
            {
                audiogramLeft.DeleteLine(i);
                audiogramRight.DeleteLine(i);
            }

            examInformation.logoUserControl.ODLRF.Text = "";
            examInformation.logoUserControl.ODLAF.Text = "";
            examInformation.logoUserControl.OELRF.Text = "";
            examInformation.logoUserControl.OELAF.Text = "";

            examInformation.meatoscopyUserControl.ODMeatoscopy.Text = "";
            examInformation.meatoscopyUserControl.OEMeatoscopy.Text = "";

            examInformation.logoUserControl.ODInt.Text = "";
            examInformation.logoUserControl.OEInt.Text = "";

            examInformation.logoUserControl.WordsMono.Text = "";
            examInformation.logoUserControl.ODMono.Text = "";
            examInformation.logoUserControl.OEMono.Text = "";

            examInformation.logoUserControl.WordsDi.Text = "";
            examInformation.logoUserControl.ODDi.Text = "";
            examInformation.logoUserControl.OEDi.Text = "";

            examInformation.logoUserControl.WordsTri.Text = "";
            examInformation.logoUserControl.ODTri.Text = "";
            examInformation.logoUserControl.OETri.Text = "";

            examInformation.maskUserControl.ODVAMIN.Text = "";
            examInformation.maskUserControl.ODVAMAX.Text = "";
            examInformation.maskUserControl.ODVOMIN.Text = "";
            examInformation.maskUserControl.ODVOMAX.Text = "";
            examInformation.maskUserControl.ODQUANT.Text = "";

            examInformation.maskUserControl.OEVAMIN.Text = "";
            examInformation.maskUserControl.OEVAMAX.Text = "";
            examInformation.maskUserControl.OEVOMIN.Text = "";
            examInformation.maskUserControl.OEVOMAX.Text = "";
            examInformation.maskUserControl.OEQUANT.Text = "";

            examInformation.obsUserControl.Obs.Text = "Perda auditiva do tipo  (Silman e Silverman, 1997) de grau  (Lloyd e Kaplan, 1978) e configuração  (Silman e Silverman, 1997; Carhart, 1945).";
        }
    }
}