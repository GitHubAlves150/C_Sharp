using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using WaveFit2.Audiogram.ViewModel;

namespace WaveFit2.Audiogram.View
{
    public partial class AudiographUserControl : UserControl
    {
        public AudiogramViewModel audiogramViewModel = new AudiogramViewModel();

        public int type = 0;
        public int tabletype;
        public int key = 1;
        public int currentFrequency;
        public double dB;
        public int i;

        private List<AudiogramTableModel> audiogramModel = new List<AudiogramTableModel>();
        private List<double> Intensities = new List<double> { -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120 };

        public double[] intensity = new double[11];

        public AudiographUserControl(char ear, string title, int receiver)
        {
            InitializeComponent();

            audiogramViewModel = new AudiogramViewModel();
            audiogramViewModel.SetupPlot(ear, title, receiver);

            AudiogramTable.DataContext = audiogramViewModel;
            DataContext = audiogramViewModel;

            StartTable();
            AudiographTools();

            audiogramViewModel.Ear = ear;
        }

        public void AudiographTools()
        {
            audiogramViewModel.AudiogramPlot.MouseDown += AudiogramPlot_MouseDown;
            audiogramViewModel.AudiogramPlot.MouseMove += AudiogramPlot_MouseMove;
            audiogramViewModel.AudiogramPlot.MouseDown += AudiogramPlot_MouseRemove;
            AudiogramTable.CellEditEnding += AudiogramTable_CellEditEnding;
        }

        private void AudiogramPlot_MouseMove(object sender, OxyMouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                e.Handled = true;

                audiogramViewModel.PointCalc(e, type, Properties.Settings.Default.mask);
                audiogramViewModel.MarkerCalc(e, type, Properties.Settings.Default.mask, Properties.Settings.Default.noAnswer);
                PointToText(audiogramViewModel.key, false);
            }
        }

        public void AudiogramPlot_MouseDown(object sender, OxyMouseDownEventArgs e)
        {
            if (e.ChangedButton == OxyMouseButton.Left)
            {
                e.Handled = true;
                audiogramViewModel.PointCalc(e, type, Properties.Settings.Default.mask);
                audiogramViewModel.MarkerCalc(e, type, Properties.Settings.Default.mask, Properties.Settings.Default.noAnswer);
                PointToText(audiogramViewModel.key, false);
            }
        }

        public void AudiogramPlot_MouseRemove(object sender, OxyMouseDownEventArgs e)
        {
            if (e.ChangedButton == OxyMouseButton.Right)
            {
                try
                {

                    e.Handled = true;
                    var clickedPoint = new ScreenPoint(e.Position.X, e.Position.Y);
                    double closestFrequency = audiogramViewModel.GetClosestFrequency(clickedPoint);
                    audiogramViewModel.key = audiogramViewModel.Frequencies.FirstOrDefault(x => x.Value == closestFrequency).Key;
                    if (closestFrequency == 125)
                    {
                        intensity[0] = double.NaN;
                        currentFrequency = 125;
                        i = 0;
                    }
                    if (closestFrequency == 250)
                    {
                        intensity[1] = double.NaN;
                        currentFrequency = 250;
                        i = 1;
                    }
                    if (closestFrequency == 500)
                    {
                        intensity[2] = double.NaN;
                        currentFrequency = 500;
                        i = 2;
                    }
                    if (closestFrequency == 750)
                    {
                        intensity[3] = double.NaN;
                        currentFrequency = 750;
                        i = 3;
                    }
                    if (closestFrequency == 1000)
                    {
                        intensity[4] = double.NaN;
                        currentFrequency = 1000;
                        i = 4;
                    }
                    if (closestFrequency == 1500)
                    {
                        intensity[5] = double.NaN;
                        currentFrequency = 1500;
                        i = 5;
                    }
                    if (closestFrequency == 2000)
                    {
                        intensity[6] = double.NaN;
                        currentFrequency = 2000;
                        i = 6;
                    }
                    if (closestFrequency == 3000)
                    {
                        intensity[7] = double.NaN;
                        currentFrequency = 3000;
                        i = 7;
                    }
                    if (closestFrequency == 4000)
                    {
                        intensity[8] = double.NaN;
                        currentFrequency = 4000;
                        i = 8;
                    }
                    if (closestFrequency == 6000)
                    {
                        intensity[9] = double.NaN;
                        currentFrequency = 6000;
                        i = 9;
                    }
                    if (closestFrequency == 8000)
                    {
                        intensity[10] = double.NaN;
                        currentFrequency = 8000;
                        i = 10;
                    }
                    DeletePoint(type, currentFrequency);
                    PointToText(audiogramViewModel.key, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void SelectedLine(int type)
        {
            switch (type)
            {
                case 0:
                    audiogramViewModel.lineArray[type].MarkerStrokeThickness = 3;
                    audiogramViewModel.lineArray[type].StrokeThickness = 3;
                    audiogramViewModel.lineArray[1].MarkerStrokeThickness = 2;
                    audiogramViewModel.lineArray[2].MarkerStrokeThickness = 2;
                    audiogramViewModel.AudiogramPlot.InvalidatePlot(true);
                    break;

                case 1:
                    audiogramViewModel.lineArray[type].MarkerStrokeThickness = 3;
                    audiogramViewModel.lineArray[0].StrokeThickness = 2;
                    audiogramViewModel.lineArray[0].MarkerStrokeThickness = 2;
                    audiogramViewModel.lineArray[2].MarkerStrokeThickness = 2;
                    audiogramViewModel.AudiogramPlot.InvalidatePlot(true);
                    break;

                case 2:
                    audiogramViewModel.lineArray[type].MarkerStrokeThickness = 3;
                    audiogramViewModel.lineArray[0].StrokeThickness = 2;
                    audiogramViewModel.lineArray[0].MarkerStrokeThickness = 2;
                    audiogramViewModel.lineArray[1].MarkerStrokeThickness = 2;
                    audiogramViewModel.AudiogramPlot.InvalidatePlot(true);
                    break;
            }
        }

        public void DeleteLine(int type)
        {
            audiogramViewModel.lineArray[type].Points.Clear();
            switch (type)
            {
                case 0:

                    for (int j = 0; j < 4; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;

                case 1:

                    for (int j = 4; j < 8; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;

                case 2:

                    for (int j = 8; j < 9; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;
            }

            audiogramViewModel.AudiogramPlot.InvalidatePlot(true);

            for (int i = 0; i < audiogramViewModel.dictKeys[type].Count(); i++)
            {
                audiogramViewModel.dictDataPoints[type][i] = DataPoint.Undefined;
                audiogramViewModel.dictMarkerPoints[type][i] = 0;
                audiogramViewModel.dictScatterPoints[type][i] = null;
                audiogramViewModel.dictKeys[type][i] = 0;
            }
            TextReset(type);
        }

        public void DeletePoint(int type, double frequency)
        {
            key = audiogramViewModel.Frequencies.FirstOrDefault(x => x.Value == frequency).Key;

            audiogramViewModel.lineArray[type].Points.Remove(audiogramViewModel.dictDataPoints[type][i]);
            switch (type)
            {
                case 0:

                    for (int j = 0; j < 4; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Remove(audiogramViewModel.dictScatterPoints[type][i]);
                    }
                    break;

                case 1:

                    for (int j = 4; j < 8; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Remove(audiogramViewModel.dictScatterPoints[type][i]);
                    }
                    break;

                case 2:

                    for (int j = 8; j < 9; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Remove(audiogramViewModel.dictScatterPoints[type][i]);
                    }
                    break;
            }
            audiogramViewModel.dictDataPoints[type][i] = DataPoint.Undefined;
            audiogramViewModel.dictMarkerPoints[type][i] = 0;
            audiogramViewModel.dictScatterPoints[type][i] = null;
            audiogramViewModel.dictKeys[type][key - 1] = 0;

            audiogramViewModel.PointUpdate(type);
        }

        public void StartTable()
        {
            audiogramModel.Add(new AudiogramTableModel() { Type = "VA" });
            audiogramModel.Add(new AudiogramTableModel() { Type = "VO" });
            audiogramModel.Add(new AudiogramTableModel() { Type = "UCL" });
            AudiogramTable.ItemsSource = audiogramModel;
        }

        public double ClosestValue(double value, List<double> Values)
        {
            return Values.OrderBy(x => Math.Abs(x - value)).First();
        }

        public void TextToPoint(int type, double frequency, double newValue, bool mask, bool noAnswer)
        {
            if (newValue < -10) newValue = -10;
            if (newValue > 120) newValue = 120;
            if (type < 0) type = 0;

            key = audiogramViewModel.Frequencies.FirstOrDefault(x => x.Value == frequency).Key;

            if (!audiogramViewModel.dictKeys[type].Contains(key))
            {
                audiogramViewModel.dictKeys[type][key - 1] = key;
            }

            audiogramViewModel.dictDataPoints[type][key - 1] = new DataPoint(frequency, newValue);
            audiogramViewModel.dictScatterPoints[type][key - 1] = new ScatterPoint(frequency, newValue);
            audiogramViewModel.dictMarkerPoints[type][key - 1] = audiogramViewModel.FindMarker(type, mask, noAnswer);

            audiogramViewModel.lineArray[type].Points.Clear();
            switch (type)
            {
                case 0:

                    for (int j = 0; j < 4; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;

                case 1:

                    for (int j = 4; j < 8; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;

                case 2:

                    for (int j = 8; j < 9; j++)
                    {
                        audiogramViewModel.markerArray[j].Points.Clear();
                    }
                    break;
            }

            for (int i = 0; i < 11; i++)
            {
                if (audiogramViewModel.dictKeys[type][i] != 0)
                {
                    audiogramViewModel.lineArray[type].Points.Add(audiogramViewModel.dictDataPoints[type][i]);
                    audiogramViewModel.markerArray[audiogramViewModel.dictMarkerPoints[type][i]].Points.Add(audiogramViewModel.dictScatterPoints[type][i]);
                }
            }
            audiogramViewModel.AudiogramPlot.InvalidatePlot(true);
        }

        public void PointToText(int key, bool empty)
        {
            if (empty == false)
            {
                switch (key)
                {
                    case 1:
                        audiogramModel[type].Hz125 = audiogramViewModel.dictDataPoints[type][0].Y;
                        break;

                    case 2:
                        audiogramModel[type].Hz250 = audiogramViewModel.dictDataPoints[type][1].Y;
                        break;

                    case 3:
                        audiogramModel[type].Hz500 = audiogramViewModel.dictDataPoints[type][2].Y;
                        break;

                    case 4:
                        audiogramModel[type].Hz750 = audiogramViewModel.dictDataPoints[type][3].Y;
                        break;

                    case 5:
                        audiogramModel[type].Hz1000 = audiogramViewModel.dictDataPoints[type][4].Y;
                        break;

                    case 6:
                        audiogramModel[type].Hz1500 = audiogramViewModel.dictDataPoints[type][5].Y;
                        break;

                    case 7:
                        audiogramModel[type].Hz2000 = audiogramViewModel.dictDataPoints[type][6].Y;
                        break;

                    case 8:
                        audiogramModel[type].Hz3000 = audiogramViewModel.dictDataPoints[type][7].Y;
                        break;

                    case 9:
                        audiogramModel[type].Hz4000 = audiogramViewModel.dictDataPoints[type][8].Y;
                        break;

                    case 10:
                        audiogramModel[type].Hz6000 = audiogramViewModel.dictDataPoints[type][9].Y;
                        break;

                    case 11:
                        audiogramModel[type].Hz8000 = audiogramViewModel.dictDataPoints[type][10].Y;
                        break;
                }
            }
            else
            {
                switch (key)
                {
                    case 1:
                        audiogramModel[type].Hz125 = null;
                        break;

                    case 2:
                        audiogramModel[type].Hz250 = null;
                        break;

                    case 3:
                        audiogramModel[type].Hz500 = null;
                        break;

                    case 4:
                        audiogramModel[type].Hz750 = null;
                        break;

                    case 5:
                        audiogramModel[type].Hz1000 = null;
                        break;

                    case 6:
                        audiogramModel[type].Hz1500 = null;
                        break;

                    case 7:
                        audiogramModel[type].Hz2000 = null;
                        break;

                    case 8:
                        audiogramModel[type].Hz3000 = null;
                        break;

                    case 9:
                        audiogramModel[type].Hz4000 = null;
                        break;

                    case 10:
                        audiogramModel[type].Hz6000 = null;
                        break;

                    case 11:
                        audiogramModel[type].Hz8000 = null;
                        break;
                }
            }
            AudiogramTable.ItemsSource = audiogramModel;
            AudiogramTable.Items.Refresh();
        }

        public void TextReset(int type)
        {
            audiogramModel[type].Hz125 = null;
            audiogramModel[type].Hz250 = null;
            audiogramModel[type].Hz500 = null;
            audiogramModel[type].Hz750 = null;
            audiogramModel[type].Hz1000 = null;
            audiogramModel[type].Hz1500 = null;
            audiogramModel[type].Hz2000 = null;
            audiogramModel[type].Hz3000 = null;
            audiogramModel[type].Hz4000 = null;
            audiogramModel[type].Hz6000 = null;
            audiogramModel[type].Hz8000 = null;

            AudiogramTable.ItemsSource = audiogramModel;
            AudiogramTable.Items.Refresh();
        }

        public void AudiogramTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!Double.TryParse(((TextBox)e.EditingElement).Text, out dB))
            {
                ((TextBox)e.EditingElement).Clear();
                ((TextBox)e.EditingElement).Text = null;
            }

            if (!string.IsNullOrEmpty(((TextBox)e.EditingElement).Text))
            {
                dB = ClosestValue(dB, Intensities);

                ((TextBox)e.EditingElement).Text = dB.ToString();

                var audiogram = (AudiogramTableModel)e.Row.Item;

                tabletype = AudiogramTable.Items.IndexOf(AudiogramTable.SelectedItem);
                if (tabletype != -1)
                {
                    if (e.Column.Header.ToString() == "125")
                    {
                        audiogram.Hz125 = dB;
                        intensity[0] = dB;
                        currentFrequency = 125;
                        i = 0;
                    }
                    if (e.Column.Header.ToString() == "250")
                    {
                        audiogram.Hz250 = dB;
                        intensity[1] = dB;
                        currentFrequency = 250;
                        i = 1;
                    }
                    if (e.Column.Header.ToString() == "500")
                    {
                        audiogram.Hz500 = dB;
                        intensity[2] = dB;
                        currentFrequency = 500;
                        i = 2;
                    }
                    if (e.Column.Header.ToString() == "750")
                    {
                        audiogram.Hz750 = dB;
                        intensity[3] = dB;
                        currentFrequency = 750;
                        i = 3;
                    }
                    if (e.Column.Header.ToString() == "1k")
                    {
                        audiogram.Hz1000 = dB;
                        intensity[4] = dB;
                        currentFrequency = 1000;
                        i = 4;
                    }
                    if (e.Column.Header.ToString() == "1,5k")
                    {
                        audiogram.Hz1500 = dB;
                        intensity[5] = dB;
                        currentFrequency = 1500;
                        i = 5;
                    }
                    if (e.Column.Header.ToString() == "2k")
                    {
                        audiogram.Hz2000 = dB;
                        intensity[6] = dB;
                        currentFrequency = 2000;
                        i = 6;
                    }
                    if (e.Column.Header.ToString() == "3k")
                    {
                        audiogram.Hz3000 = dB;
                        intensity[7] = dB;
                        currentFrequency = 3000;
                        i = 7;
                    }
                    if (e.Column.Header.ToString() == "4k")
                    {
                        audiogram.Hz4000 = dB;
                        intensity[8] = dB;
                        currentFrequency = 4000;
                        i = 8;
                    }
                    if (e.Column.Header.ToString() == "6k")
                    {
                        audiogram.Hz6000 = dB;
                        intensity[9] = dB;
                        currentFrequency = 6000;
                        i = 9;
                    }
                    if (e.Column.Header.ToString() == "8k")
                    {
                        audiogram.Hz8000 = dB;
                        intensity[10] = dB;
                        currentFrequency = 8000;
                        i = 10;
                    }

                    TextToPoint(tabletype, currentFrequency, intensity[i], Properties.Settings.Default.mask, Properties.Settings.Default.noAnswer);
                }
            }
            else
            {
                var audiogram = (AudiogramTableModel)e.Row.Item;
                tabletype = AudiogramTable.Items.IndexOf(AudiogramTable.SelectedItem);
                if (tabletype != -1)
                {
                    if (e.Column.Header.ToString() == "125")
                    {
                        audiogram.Hz125 = null;
                        intensity[0] = double.NaN;
                        currentFrequency = 125;
                        i = 0;
                    }
                    if (e.Column.Header.ToString() == "250")
                    {
                        audiogram.Hz250 = null;
                        intensity[1] = double.NaN;
                        currentFrequency = 250;
                        i = 1;
                    }
                    if (e.Column.Header.ToString() == "500")
                    {
                        audiogram.Hz500 = null;
                        intensity[2] = double.NaN;
                        currentFrequency = 500;
                        i = 2;
                    }
                    if (e.Column.Header.ToString() == "750")
                    {
                        audiogram.Hz750 = null;
                        intensity[3] = double.NaN;
                        currentFrequency = 750;
                        i = 3;
                    }
                    if (e.Column.Header.ToString() == "1k")
                    {
                        audiogram.Hz1000 = null;
                        intensity[4] = double.NaN;
                        currentFrequency = 1000;
                        i = 4;
                    }
                    if (e.Column.Header.ToString() == "1,5k")
                    {
                        audiogram.Hz1500 = null;
                        intensity[5] = double.NaN;
                        currentFrequency = 1500;
                        i = 5;
                    }
                    if (e.Column.Header.ToString() == "2k")
                    {
                        audiogram.Hz2000 = null;
                        intensity[6] = double.NaN;
                        currentFrequency = 2000;
                        i = 6;
                    }
                    if (e.Column.Header.ToString() == "3k")
                    {
                        audiogram.Hz3000 = null;
                        intensity[7] = double.NaN;
                        currentFrequency = 3000;
                        i = 7;
                    }
                    if (e.Column.Header.ToString() == "4k")
                    {
                        audiogram.Hz4000 = null;
                        intensity[8] = double.NaN;
                        currentFrequency = 4000;
                        i = 8;
                    }
                    if (e.Column.Header.ToString() == "6k")
                    {
                        audiogram.Hz6000 = null;
                        intensity[9] = double.NaN;
                        currentFrequency = 6000;
                        i = 9;
                    }
                    if (e.Column.Header.ToString() == "8k")
                    {
                        audiogram.Hz8000 = null;
                        intensity[10] = double.NaN;
                        currentFrequency = 8000;
                        i = 10;
                    }

                    DeletePoint(tabletype, currentFrequency);
                }
            }
        }
    }
}