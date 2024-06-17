using Npgsql;
using OxyPlot;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using WaveFit2.Audiogram.ViewModel;

namespace WaveFit2.Audiogram.View
{
    /// <summary>
    /// Interação lógica para StaticAudiographUserControl.xam
    /// </summary>
    public partial class StaticAudiographUserControl : UserControl
    {
        public AudiogramViewModel audiogramViewModel = new AudiogramViewModel();
        public int audiogram;
        public int[,] Marker;
        public string[] Type;
        public char[] Ear;
        public double[,] Intensity;
        public List<double> Frequency = new List<double> { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 };

        public StaticAudiographUserControl(char ear, string title, bool seeMode, int receiver)
        {
            InitializeComponent();

            DataContext = audiogramViewModel;

            if (seeMode == false)
            {
                audiogramViewModel.SetupStaticPlot(ear, title);
                staticPlot.Height = 180;
                staticPlot.Width = 180;
            }
            else
            {
                audiogramViewModel.SetupPlot(ear, title, receiver);

                audiogram = Properties.Settings.Default.audiogramId;
                Intensity = new double[6, 11];
                Type = new string[6];
                Ear = new char[6];
                Marker = new int[6, 11];
                GetFrequencies(audiogram);

                for (int j = 0; j < 6; j++)
                {
                    if (Ear[j] == ear)
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            audiogramViewModel.CreateStaticPoints(Type[j], Frequency[i], Intensity[j, i], Marker[j, i]);
                        }
                    }
                }
            }
        }

        public void GetFrequencies(int idAudiogram)
        {
            var conexao = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    string cmdIdAudiogram = "SELECT * " +
                                            "FROM dbo.frequency " +
                                            "WHERE idaudiogram=" + idAudiogram + "";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, conexao);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    int index = 0;

                    while (dr.Read())
                    {
                        Type[index] = dr.GetString(2);
                        Ear[index] = dr.GetChar(3);

                        for (int i = 4; i < 15; i++)
                        {
                            string[] substrings = dr.GetString(i).Split('&');
                            Intensity[index, i - 4] = double.Parse(substrings[0]);
                            Marker[index, i - 4] = int.Parse(substrings[1]);
                        }
                        index++;
                    }
                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
            }
        }

        public Image CreateAudiogramImage(int height, int width, int resolution = 96)
        {
            Image audiogramImage = new Image();
            audiogramImage.Source = PngExporter.ExportToBitmap(audiogramViewModel.AudiogramPlot, width, height, OxyColor.FromRgb(0xFF, 0xFF, 0xFF), resolution);

            return audiogramImage;
        }

        private void AudiogramPlot_MouseMove(object sender, OxyMouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                e.Handled = true;
            }
        }

        public void AudiogramPlot_MouseDown(object sender, OxyMouseDownEventArgs e)
        {
            if (e.ChangedButton == OxyMouseButton.Left)
            {
                e.Handled = true;
            }
        }

        public void AudiogramPlot_MouseRemove(object sender, OxyMouseDownEventArgs e)
        {
            if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                e.Handled = true;
            }
        }
    }
}