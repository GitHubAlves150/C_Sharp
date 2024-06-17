using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Audiogram.ViewModel
{
    public class AudiogramViewModel : Crud
    {
        public PlotModel AudiogramPlot { get; set; }

        public LineSeries[] lineArray;
        public ScatterSeries[] markerArray;

        public OxyColor color;

        public DataPoint[] dataPointsVA = new DataPoint[11];
        public DataPoint[] dataPointsVO = new DataPoint[11];
        public DataPoint[] dataPointsUCL = new DataPoint[11];

        public ScatterPoint[] scatterPointsVA = new ScatterPoint[11];
        public ScatterPoint[] scatterPointsVO = new ScatterPoint[11];
        public ScatterPoint[] scatterPointsUCL = new ScatterPoint[11];

        public DataPoint staticPoint;
        public ScatterPoint scatterPoint;

        public int linesTotal = 3;
        public int markerTotal = 9;

        public int highlighted = -1;

        public bool isPrint = false;
        public bool isReadOnly;

        public int key = 1;

        public char Ear { get; set; }

        public ScreenPoint[] LeftVA { get; set; }
        public ScreenPoint[] LeftVANoAnswer { get; set; }
        public ScreenPoint[] RightVA { get; set; }
        public ScreenPoint[] RightVANoAnswer { get; set; }
        public ScreenPoint[] LeftVO { get; set; }
        public ScreenPoint[] LeftVONoAnswer { get; set; }
        public ScreenPoint[] RightVO { get; set; }
        public ScreenPoint[] RightVONoAnswer { get; set; }
        public ScreenPoint[] LeftUCL { get; set; }
        public ScreenPoint[] RightUCL { get; set; }

        public ScreenPoint[] LeftMaskVA { get; set; }
        public ScreenPoint[] LeftMaskVANoAnswer { get; set; }
        public ScreenPoint[] RightMaskVA { get; set; }
        public ScreenPoint[] RightMaskVANoAnswer { get; set; }
        public ScreenPoint[] LeftMaskVO { get; set; }
        public ScreenPoint[] LeftMaskVONoAnswer { get; set; }
        public ScreenPoint[] RightMaskVO { get; set; }
        public ScreenPoint[] RightMaskVONoAnswer { get; set; }

        public List<AudiogramModel> Audiogram;
        public List<double> dbValues;

        public Dictionary<int, int[]> dictKeys = new Dictionary<int, int[]>();
        public Dictionary<int, DataPoint[]> dictDataPoints = new Dictionary<int, DataPoint[]>();
        public Dictionary<int, ScatterPoint[]> dictScatterPoints = new Dictionary<int, ScatterPoint[]>();
        public Dictionary<int, int[]> dictMarkerPoints = new Dictionary<int, int[]>();

        public Dictionary<int, double> Frequencies;

        public double Hz125, Hz250, Hz500, Hz750, Hz1000, Hz1500, Hz2000, Hz3000, Hz4000, Hz6000, Hz8000;
        public int idAudiogram;
        public bool Mask, NoAnswer;

        public int[] markerTypes = new int[11];

        public AudiogramViewModel()
        {
            AudiogramPlot = new PlotModel();

            lineArray = new LineSeries[linesTotal];
            markerArray = new ScatterSeries[markerTotal];

            Frequencies = new Dictionary<int, double>
            {
                {1, 125},
                {2, 250},
                {3, 500},
                {4, 750},
                {5, 1000},
                {6, 1500},
                {7, 2000},
                {8, 3000},
                {9, 4000},
                {10, 6000},
                {11, 8000},
            };

            scatterPointsVA[1] = null;

            dictKeys.Add(0, new int[11]);
            dictKeys.Add(1, new int[11]);
            dictKeys.Add(2, new int[11]);

            dictDataPoints.Add(0, new DataPoint[11]);
            dictDataPoints.Add(1, new DataPoint[11]);
            dictDataPoints.Add(2, new DataPoint[11]);

            dictScatterPoints.Add(0, new ScatterPoint[11]);
            dictScatterPoints.Add(1, new ScatterPoint[11]);
            dictScatterPoints.Add(2, new ScatterPoint[11]);

            dictMarkerPoints.Add(0, new int[11]);
            dictMarkerPoints.Add(1, new int[11]);
            dictMarkerPoints.Add(2, new int[11]);
        }

        #region Plot

        public string FrequencyFormatter(double Frequency)
        {
            if (Frequency < 1000) return Frequency.ToString();
            else return (Frequency / 1000).ToString() + "k";
        }

        public AreaSeries AddAreaSeries(char ear, int receiver)
        {
            var areaSeries1 = new AreaSeries();
            if (receiver == 0)
            {
                //BVA321
                areaSeries1.Points.Add(new DataPoint(125, 10));
                areaSeries1.Points.Add(new DataPoint(1000, 10));
                areaSeries1.Points.Add(new DataPoint(2000, 20));
                areaSeries1.Points.Add(new DataPoint(8000, 20));
                areaSeries1.Points.Add(new DataPoint(8000, 70));
                areaSeries1.Points.Add(new DataPoint(1000, 70));
                areaSeries1.Points.Add(new DataPoint(750, 60));
                areaSeries1.Points.Add(new DataPoint(125, 60));
                areaSeries1.Points.Add(new DataPoint(125, 10));
            }
            else if (receiver == 1)
            {
                //BVA530
                areaSeries1.Points.Add(new DataPoint(125, 10));
                areaSeries1.Points.Add(new DataPoint(1000, 10));
                areaSeries1.Points.Add(new DataPoint(2000, 20));
                areaSeries1.Points.Add(new DataPoint(8000, 20));
                areaSeries1.Points.Add(new DataPoint(8000, 75));
                areaSeries1.Points.Add(new DataPoint(1000, 75));
                areaSeries1.Points.Add(new DataPoint(750, 65));
                areaSeries1.Points.Add(new DataPoint(125, 65));
                areaSeries1.Points.Add(new DataPoint(125, 10));
            }
            else if (receiver == 2)
            {
                //Power
                areaSeries1.Points.Add(new DataPoint(125, 10));
                areaSeries1.Points.Add(new DataPoint(1000, 10));
                areaSeries1.Points.Add(new DataPoint(2000, 20));
                areaSeries1.Points.Add(new DataPoint(8000, 20));
                areaSeries1.Points.Add(new DataPoint(8000, 85));
                areaSeries1.Points.Add(new DataPoint(1000, 85));
                areaSeries1.Points.Add(new DataPoint(750, 75));
                areaSeries1.Points.Add(new DataPoint(125, 75));
                areaSeries1.Points.Add(new DataPoint(125, 10));
            }

            areaSeries1.Color = ear == 'R' ? OxyColor.FromArgb(255, 255, 0, 0) : OxyColor.FromArgb(255, 50, 100, 200);
            areaSeries1.StrokeThickness = 0.5;
            return areaSeries1;
        }

        public void SetupPlot(char ear, string title, int receiver)
        {
            // Define o título
            AudiogramPlot.Title = title;
            AudiogramPlot.TitleFontSize = 16;
            AudiogramPlot.TitleFontWeight = 0;
            AudiogramPlot.TitleHorizontalAlignment = TitleHorizontalAlignment.CenteredWithinPlotArea;

            // Define a margem e a borda do gráfico
            AudiogramPlot.PlotMargins = new OxyThickness(double.NaN);
            AudiogramPlot.PlotAreaBorderThickness = new OxyThickness(2);
            AudiogramPlot.PlotAreaBorderColor = OxyColor.FromRgb(0x91, 0x91, 0x91);

            // Define a legenda do gráfico
            AudiogramPlot.LegendPlacement = LegendPlacement.Inside;
            AudiogramPlot.LegendPosition = LegendPosition.LeftTop;
            AudiogramPlot.LegendBackground = OxyColors.White;
            AudiogramPlot.LegendBorder = OxyColor.FromRgb(0x91, 0x91, 0x91);
            AudiogramPlot.IsLegendVisible = false;

            // Define como serão os eixos
            var yAxis = new LinearAxis();
            var xAxis = new LogarithmicAxis();
            var xAxisAux = new LogarithmicAxis();

            // Define o eixo Y
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Nível de audição";
            yAxis.TitleFontSize = 12;
            yAxis.FontSize = 11;
            yAxis.Minimum = -15;
            yAxis.Maximum = 125;
            yAxis.MajorStep = 10;
            yAxis.MinorStep = 10;
            yAxis.MajorGridlineStyle = LineStyle.Solid;
            yAxis.ExtraGridlineColor = OxyColors.LightGray;
            yAxis.Unit = "dB HL";
            yAxis.StartPosition = 1;
            yAxis.EndPosition = 0;
            yAxis.AbsoluteMaximum = 120;
            yAxis.AbsoluteMinimum = -10;
            yAxis.IsPanEnabled = false;
            yAxis.IsZoomEnabled = false;
            yAxis.AxisTitleDistance = 15;

            // Define o eixo X
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Frequência";
            xAxis.TitleFontSize = 12;
            xAxis.FontSize = 11;
            xAxis.Minimum = 100;
            xAxis.Maximum = 9000;
            xAxis.Base = 2;
            xAxis.Multiplier = 125;
            xAxis.ExtraGridlines = new Double[] { 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0, 8000.0 };
            xAxis.ExtraGridlineStyle = LineStyle.Solid;
            xAxis.ExtraGridlineColor = OxyColors.Gray;
            xAxis.Unit = "Hz";
            xAxis.AbsoluteMaximum = 8000;
            xAxis.AbsoluteMinimum = 125;
            xAxis.MajorStep = 2000;
            xAxis.MinorStep = 750;
            xAxis.IsPanEnabled = false;
            xAxis.IsZoomEnabled = false;
            xAxis.LabelFormatter = FrequencyFormatter;
            xAxis.AxisTitleDistance = 15;

            // Define o eixo X auxiliar
            xAxisAux.Position = AxisPosition.Bottom;
            xAxisAux.LabelFormatter = d => "";
            xAxisAux.Minimum = 100;
            xAxisAux.Maximum = 9000;
            xAxisAux.Base = 2;
            xAxisAux.Multiplier = 125;
            xAxisAux.ExtraGridlines = new Double[] { 750.0, 1500.0, 3000.0, 6000.0 };
            xAxisAux.ExtraGridlineStyle = LineStyle.Dash;
            xAxisAux.ExtraGridlineColor = OxyColors.Gray;
            xAxisAux.MajorStep = 10000;
            xAxisAux.IsPanEnabled = false;
            xAxisAux.IsZoomEnabled = false;

            // Posiciona o eixo Y de acordo com o lado da audiometria
            if (ear == 'L')
            {
                yAxis.Position = AxisPosition.Right;
            }

            // Caso não seja uma versão de impressão, estiliza o gráfico
            if (!isPrint)
            {
                AudiogramPlot.PlotAreaBorderColor = OxyColors.Transparent;
                yAxis.MajorGridlineColor = OxyColors.White;
                yAxis.ExtraGridlineColor = OxyColors.White;
                yAxis.MajorGridlineThickness = 2;
                yAxis.ExtraGridlineThickness = 2;
                xAxis.AxislineColor = OxyColors.White;
                xAxis.ExtraGridlineColor = OxyColors.White;
                xAxis.AxislineThickness = 2;
                xAxis.ExtraGridlineThickness = 2;
                xAxisAux.AxislineColor = OxyColors.White;
                xAxisAux.ExtraGridlineColor = OxyColors.White;
                xAxisAux.AxislineThickness = 2;
                xAxisAux.ExtraGridlineThickness = 2;
            }

            AudiogramPlot.Axes.Add(yAxis);
            AudiogramPlot.Axes.Add(xAxis);
            AudiogramPlot.Axes.Add(xAxisAux);

            if (ear == 'L' && !isPrint)
            {
                AudiogramPlot.PlotAreaBackground = OxyColor.FromArgb(255, 229, 225, 255);
            }
            else if (!isPrint)
            {
                AudiogramPlot.PlotAreaBackground = OxyColor.FromArgb(255, 250, 232, 234);
            }
            else
            {
                AudiogramPlot.PlotAreaBackground = OxyColor.FromArgb(255, 255, 255, 255);
            }

            SetupLines(ear);
            SetupMarker(ear);
            AudiogramPlot.Series.Add(AddAreaSeries(ear, receiver));
        }

        #endregion Plot

        #region Points

        public double FindClosestValue(double value, List<double> allowedValues)
        {
            return allowedValues.OrderBy(x => Math.Abs(x - value)).First();
        }

        public double GetClosestFrequency(ScreenPoint clickedPoint)
        {
            var xAxis = AudiogramPlot.DefaultXAxis;
            var yAxis = AudiogramPlot.DefaultYAxis;
            double xValue = xAxis.InverseTransform(clickedPoint.X);
            double yValue = yAxis.InverseTransform(clickedPoint.Y);
            return FindClosestValue(xValue, Frequencies.Values.ToList());
        }

        public bool RemovePoint(ScreenPoint clickedPoint)
        {
            var closestDistance = double.MaxValue;
            bool pointRemoved = false;
            int closestIndex = -1;
            int closestType = -1;
            double limiteDeProximidade = 10.0;
            for (int type = 0; type < lineArray.Length; type++)
            {
                for (int i = 0; i < dictScatterPoints[type].Length; i++)
                {
                    var scatterPoint = dictScatterPoints[type][i];
                    if (scatterPoint != null)
                    {
                        var distance = clickedPoint.DistanceTo(new ScreenPoint(AudiogramPlot.DefaultXAxis.Transform(scatterPoint.X),
                                                                                AudiogramPlot.DefaultYAxis.Transform(scatterPoint.Y)));
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestIndex = i;
                            closestType = type;
                        }
                    }
                }
            }

            if (closestIndex != -1 && closestDistance <= limiteDeProximidade)
            {
                dictKeys[closestType][closestIndex] = 0;
                dictDataPoints[closestType][closestIndex] = new DataPoint();
                dictScatterPoints[closestType][closestIndex] = null;
                RebuildLineAndMarkerSeries(closestType);
                pointRemoved = true;
            }
            AudiogramPlot.InvalidatePlot(true);
            return pointRemoved;
        }

        private void RebuildLineAndMarkerSeries(int type)
        {
            lineArray[type].Points.Clear();
            foreach (var series in markerArray)
            {
                series.Points.Clear();
            }

            for (int i = 0; i < 11; i++)
            {
                if (dictKeys[type][i] != 0)
                {
                    lineArray[type].Points.Add(dictDataPoints[type][i]);
                }
            }

            for (int i = 0; i < 11; i++)
            {
                if (dictKeys[type][i] != 0)
                {
                    int markerIndex = dictMarkerPoints[type][i];
                    markerArray[markerIndex].Points.Add(dictScatterPoints[type][i]);
                }
            }
        }

        public void PointCalc(OxyMouseEventArgs e, int type, bool mask)
        {
            var xAxis = AudiogramPlot.DefaultXAxis;
            var yAxis = AudiogramPlot.DefaultYAxis;

            var dataPoint = new OxyPlot.DataPoint(xAxis.InverseTransform(e.Position.X), yAxis.InverseTransform(e.Position.Y));

            if (dataPoint.X < 125) dataPoint.X = 125;
            if (dataPoint.X > 8000) dataPoint.X = 8000;
            if (dataPoint.Y < -10) dataPoint.Y = -10;
            if (dataPoint.Y > 120) dataPoint.Y = 120;

            dataPoint.X = FindClosestValue(dataPoint.X, new List<double> { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 });
            dataPoint.Y = FindClosestValue(dataPoint.Y, new List<double> { -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120 });

            key = Frequencies.FirstOrDefault(x => x.Value == dataPoint.X).Key;

            if (!dictKeys[type].Contains(key))
            {
                dictKeys[type][key - 1] = key;
            }

            dictDataPoints[type][key - 1] = dataPoint;

            lineArray[type].Points.Clear();

            for (int i = 0; i < 11; i++)
            {
                if (dictKeys[type][i] != 0)
                {
                    lineArray[type].Points.Add(dictDataPoints[type][i]);
                }
            }

            AudiogramPlot.InvalidatePlot(true);
        }

        public void MarkerCalc(OxyMouseEventArgs e, int type, bool mask, bool noAnswer)
        {
            var xAxis = AudiogramPlot.DefaultXAxis;
            var yAxis = AudiogramPlot.DefaultYAxis;

            var dataPoint = new OxyPlot.DataPoint(xAxis.InverseTransform(e.Position.X), yAxis.InverseTransform(e.Position.Y));

            if (dataPoint.X < 125) dataPoint.X = 125;
            if (dataPoint.X > 8000) dataPoint.X = 8000;
            if (dataPoint.Y < -10) dataPoint.Y = -10;
            if (dataPoint.Y > 120) dataPoint.Y = 120;

            dataPoint.X = FindClosestValue(dataPoint.X, new List<double> { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 });
            dataPoint.Y = FindClosestValue(dataPoint.Y, new List<double> { -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120 });

            key = Frequencies.FirstOrDefault(x => x.Value == dataPoint.X).Key;

            if (!dictKeys[type].Contains(key))
            {
                dictKeys[type][key - 1] = key;
            }

            dictMarkerPoints[type][key - 1] = FindMarker(type, mask, noAnswer);
            dictScatterPoints[type][key - 1] = new ScatterPoint(dataPoint.X, dataPoint.Y);

            switch (type)
            {
                case 0:

                    for (int j = 0; j < 4; j++)
                    {
                        markerArray[j].Points.Clear();

                        for (int i = 0; i < 11; i++)
                        {
                            if (dictKeys[type][i] != 0)
                            {
                                markerArray[dictMarkerPoints[type][i]].Points.Add(dictScatterPoints[type][i]);
                            }
                        }
                    }
                    break;

                case 1:

                    for (int j = 4; j < 8; j++)
                    {
                        markerArray[j].Points.Clear();

                        for (int i = 0; i < 11; i++)
                        {
                            if (dictKeys[type][i] != 0)
                            {
                                markerArray[dictMarkerPoints[type][i]].Points.Add(dictScatterPoints[type][i]);
                            }
                        }
                    }
                    break;

                case 2:

                    for (int j = 8; j < 9; j++)
                    {
                        markerArray[j].Points.Clear();

                        for (int i = 0; i < 11; i++)
                        {
                            if (dictKeys[type][i] != 0)
                            {
                                markerArray[dictMarkerPoints[type][i]].Points.Add(dictScatterPoints[type][i]);
                            }
                        }
                    }
                    break;
            }
        }

        public (double min, double max) CalculateSaturation(double frequency)
        {
            Dictionary<double, (double min, double max)> saturationValues = new Dictionary<double, (double min, double max)>
            {
                { 125, (-10, 100) },
                { 250, (-10, 100) },
                { 500, (-10, 100) },
                { 750, (-10, 100) },
                { 1000, (-10, 100) },
                { 1500, (-10, 100) },
                { 2000, (-10, 100) },
                { 3000, (-10, 100) },
                { 4000, (-10, 100) },
                { 6000, (-10, 100) },
                { 8000, (-10, 100) }
            };

            if (saturationValues.ContainsKey(frequency))
            {
                return saturationValues[frequency];
            }

            return (-10, 125);
        }

        public int FindMarker(int type, bool mask, bool noAnswer)
        {
            switch (type)
            {
                case 0:
                    if (mask == false)
                    {
                        if (noAnswer == false)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        if (noAnswer == false)
                        {
                            return 2;
                        }
                        else
                        {
                            return 3;
                        }
                    }

                case 1:
                    if (mask == false)
                    {
                        if (noAnswer == false)
                        {
                            return 4;
                        }
                        else
                        {
                            return 5;
                        }
                    }
                    else
                    {
                        if (noAnswer == false)
                        {
                            return 6;
                        }
                        else
                        {
                            return 7;
                        }
                    }

                case 2:
                    return 8;

                default:
                    return 0;
            }
        }

        public void PointUpdate(int type)
        {
            lineArray[type].Points.Clear();
            switch (type)
            {
                case 0:

                    for (int j = 0; j < 4; j++)
                    {
                        markerArray[j].Points.Clear();
                    }
                    break;

                case 1:

                    for (int j = 4; j < 8; j++)
                    {
                        markerArray[j].Points.Clear();
                    }
                    break;

                case 2:

                    for (int j = 8; j < 9; j++)
                    {
                        markerArray[j].Points.Clear();
                    }
                    break;
            }

            for (int i = 0; i < 11; i++)
            {
                if (dictKeys[type][i] != 0)
                {
                    lineArray[type].Points.Add(dictDataPoints[type][i]);
                    markerArray[dictMarkerPoints[type][i]].Points.Add(dictScatterPoints[type][i]);
                }
            }

            AudiogramPlot.InvalidatePlot(true);
        }

        #endregion Points

        #region Lines

        public void SetupLines(char ear)
        {
            int index = 0;

            for (int i = 0; i < linesTotal; i++)
            {
                lineArray[i] = new LineSeries();

                if (ear == 'L' && !isPrint)
                {
                    color = OxyColor.FromArgb(255, 0, 0, 255);
                }
                else if (!isPrint)
                {
                    color = OxyColor.FromArgb(255, 255, 0, 0);
                }
                else
                {
                    color = OxyColor.FromArgb(255, 0, 0, 0);
                }

                // Define o título da linha
                switch (index)
                {
                    case 0:
                        lineArray[i].Title = "VA";
                        break;

                    case 1:
                        lineArray[i].Title = "VO";
                        break;

                    case 2:
                        lineArray[i].Title = "UCL";
                        break;
                }

                lineArray[i] = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    MarkerSize = 4,
                    MarkerStroke = color
                };

                // Define se a linha está destacada e a torna mais espessa
                if (index == highlighted && !isPrint)
                {
                    lineArray[i].MarkerStrokeThickness = 3;
                }
                else
                {
                    lineArray[i].MarkerStrokeThickness = 2;
                }

                // Para a primeira linha, define um estilo de linha
                if (index == 0)
                {
                    // Caso a audiometria corresponda ao lado esquerdo define um estilo tracejado
                    if (ear == 'L')
                    {
                        lineArray[i].LineStyle = LineStyle.Dash;
                        lineArray[i].Color = color;
                    }
                    // Caso a audiometria corresponda ao lado direito define um estilo sólido
                    else
                    {
                        lineArray[i].LineStyle = LineStyle.Solid;
                        lineArray[i].MarkerFill = OxyColor.FromRgb(241, 241, 241);
                        lineArray[i].Color = color;
                    }
                }

                // Para as demais linhas, esconde a linha e mostra apenas os marcadores
                else
                {
                    lineArray[i].StrokeThickness = 0;
                }

                // Adiciona a linha ao plotModel
                AudiogramPlot.Series.Add(lineArray[i]);
                index++;
            }
        }

        public void SetupMarker(char ear)
        {
            GenerateLeftVA();
            GenerateRightVA();
            GenerateLeftVO();
            GenerateRightVO();
            GenerateBothUCL();

            GenerateLeftVANoAnswer();
            GenerateRightVANoAnswer();
            GenerateLeftVONoAnswer();
            GenerateRightVONoAnswer();

            GenerateLeftMaskVA();
            GenerateRightMaskVA();
            GenerateLeftMaskVO();
            GenerateRightMaskVO();

            GenerateLeftMaskVANoAnswer();
            GenerateRightMaskVANoAnswer();
            GenerateLeftMaskVONoAnswer();
            GenerateRightMaskVONoAnswer();

            int index = 0;

            for (int i = 0; i < markerTotal; i++)
            {
                markerArray[i] = new ScatterSeries();

                if (ear == 'L' && !isPrint)
                {
                    color = OxyColor.FromArgb(255, 0, 0, 255);
                }
                else if (!isPrint)
                {
                    color = OxyColor.FromArgb(255, 255, 0, 0);
                }
                else
                {
                    color = OxyColor.FromArgb(255, 0, 0, 0);
                }

                markerArray[i] = new ScatterSeries
                {
                    MarkerType = MarkerType.Custom,
                    MarkerOutline = GetAllMarkers(ear, index),
                    MarkerSize = 4,
                    MarkerStroke = color,
                    MarkerStrokeThickness = 2
                };

                // Define se a linha está destacada e a torna mais espessa
                if (index == highlighted && !isPrint)
                {
                    markerArray[i].MarkerStrokeThickness = 3;
                }
                else
                {
                    markerArray[i].MarkerStrokeThickness = 2;
                }

                // Para a primeira linha, define um estilo de linha
                if (index < 4)
                {
                    markerArray[i].MarkerFill = OxyColors.WhiteSmoke;
                }

                // Adiciona a linha ao plotModel
                AudiogramPlot.Series.Add(markerArray[i]);
                index++;
            }
        }

        public void GenerateLeftVA()
        {
            LeftVA = new ScreenPoint[6];

            LeftVA[0] = new ScreenPoint(-1.5, -1.5);
            LeftVA[1] = new ScreenPoint(1.5, 1.5);
            LeftVA[2] = new ScreenPoint(0, 0);
            LeftVA[3] = new ScreenPoint(1.5, -1.5);
            LeftVA[4] = new ScreenPoint(-1.5, 1.5);
            LeftVA[5] = new ScreenPoint(0, 0);
        }

        public void GenerateRightVA()
        {
            RightVA = new ScreenPoint[17];

            for (int i = 0; i < 17; i++)
            {
                double arg = (i + 4) * Math.PI / 8;
                RightVA[i] = new ScreenPoint(1.5 * Math.Cos(arg), 1.5 * Math.Sin(arg));
            }
        }

        public void GenerateLeftVO()
        {
            LeftVO = new ScreenPoint[4];

            LeftVO[0] = new ScreenPoint(1.5, 0);
            LeftVO[1] = new ScreenPoint(0, -1.5);
            LeftVO[2] = new ScreenPoint(1.5, 0);
            LeftVO[3] = new ScreenPoint(0, 1.5);
        }

        public void GenerateRightVO()
        {
            RightVO = new ScreenPoint[4];

            RightVO[0] = new ScreenPoint(-1.5, 0);
            RightVO[1] = new ScreenPoint(0, -1.5);
            RightVO[2] = new ScreenPoint(-1.5, 0);
            RightVO[3] = new ScreenPoint(0, 1.5);
        }

        public void GenerateBothUCL()
        {
            LeftUCL = new ScreenPoint[9];

            LeftUCL[0] = new ScreenPoint(0, 0.6);
            LeftUCL[1] = new ScreenPoint(0, -0.8);
            LeftUCL[2] = new ScreenPoint(-1.1, -0.8);
            LeftUCL[3] = new ScreenPoint(-1.1, 0.8);
            LeftUCL[4] = new ScreenPoint(-1.1, -0.8);
            LeftUCL[5] = new ScreenPoint(1.1, -0.8);
            LeftUCL[6] = new ScreenPoint(1.1, 0.8);
            LeftUCL[7] = new ScreenPoint(1.1, -0.8);
            LeftUCL[8] = new ScreenPoint(0, -0.8);

            RightUCL = LeftUCL;
        }

        public void GenerateLeftVANoAnswer()
        {
            LeftVANoAnswer = new ScreenPoint[12];

            LeftVANoAnswer[0] = new ScreenPoint(-1.5, -1.5);
            LeftVANoAnswer[1] = new ScreenPoint(1.5, 1.5);
            LeftVANoAnswer[2] = new ScreenPoint(0, 0);
            LeftVANoAnswer[3] = new ScreenPoint(1.5, -1.5);
            LeftVANoAnswer[4] = new ScreenPoint(-1.5, 1.5);
            LeftVANoAnswer[5] = new ScreenPoint(0, 0);
            LeftVANoAnswer[6] = new ScreenPoint(0, 3);
            LeftVANoAnswer[7] = new ScreenPoint(-0.5, 3);
            LeftVANoAnswer[8] = new ScreenPoint(0, 4);
            LeftVANoAnswer[9] = new ScreenPoint(0.5, 3);
            LeftVANoAnswer[10] = new ScreenPoint(0, 3);
            LeftVANoAnswer[11] = new ScreenPoint(0, 0);
        }

        public void GenerateRightVANoAnswer()
        {
            RightVANoAnswer = new ScreenPoint[23];

            for (int i = 0; i < 17; i++)
            {
                double arg = (i + 4) * Math.PI / 8;
                RightVANoAnswer[i] = new ScreenPoint(1.5 * Math.Cos(arg), 1.5 * Math.Sin(arg));
            }

            RightVANoAnswer[17] = new ScreenPoint(0, 3);
            RightVANoAnswer[18] = new ScreenPoint(-0.5, 3);
            RightVANoAnswer[19] = new ScreenPoint(0, 4);
            RightVANoAnswer[20] = new ScreenPoint(0.5, 3);
            RightVANoAnswer[21] = new ScreenPoint(0, 3);
            RightVANoAnswer[22] = new ScreenPoint(0, 1.5);
        }

        public void GenerateLeftVONoAnswer()
        {
            LeftVONoAnswer = new ScreenPoint[10];

            LeftVONoAnswer[0] = new ScreenPoint(1.5, 0);
            LeftVONoAnswer[1] = new ScreenPoint(0, -1.5);
            LeftVONoAnswer[2] = new ScreenPoint(1.5, 0);
            LeftVONoAnswer[3] = new ScreenPoint(0, 1.5);

            LeftVONoAnswer[4] = new ScreenPoint(0, 3.5);
            LeftVONoAnswer[5] = new ScreenPoint(-0.5, 3.5);
            LeftVONoAnswer[6] = new ScreenPoint(0, 4.5);
            LeftVONoAnswer[7] = new ScreenPoint(0.5, 3.5);
            LeftVONoAnswer[8] = new ScreenPoint(0, 3.5);
            LeftVONoAnswer[9] = new ScreenPoint(0, 1.5);
        }

        public void GenerateRightVONoAnswer()
        {
            RightVONoAnswer = new ScreenPoint[10];

            RightVONoAnswer[0] = new ScreenPoint(-1.5, 0);
            RightVONoAnswer[1] = new ScreenPoint(0, -1.5);
            RightVONoAnswer[2] = new ScreenPoint(-1.5, 0);
            RightVONoAnswer[3] = new ScreenPoint(0, 1.5);
            RightVONoAnswer[4] = new ScreenPoint(0, 3.5);
            RightVONoAnswer[5] = new ScreenPoint(-0.5, 3.5);
            RightVONoAnswer[6] = new ScreenPoint(0, 4.5);
            RightVONoAnswer[7] = new ScreenPoint(0.5, 3.5);
            RightVONoAnswer[8] = new ScreenPoint(0, 3.5);
            RightVONoAnswer[9] = new ScreenPoint(0, 1.5);
        }

        public void GenerateLeftMaskVA()
        {
            LeftMaskVA = new ScreenPoint[4];

            LeftMaskVA[0] = new ScreenPoint(-1.5, 1.5);
            LeftMaskVA[1] = new ScreenPoint(1.5, 1.5);
            LeftMaskVA[2] = new ScreenPoint(1.5, -1.5);
            LeftMaskVA[3] = new ScreenPoint(-1.5, -1.5);
        }

        public void GenerateRightMaskVA()
        {
            RightMaskVA = new ScreenPoint[5];

            RightMaskVA[0] = new ScreenPoint(0, 1.5);
            RightMaskVA[1] = new ScreenPoint(-1.5, 1.5);
            RightMaskVA[2] = new ScreenPoint(0, -1.5);
            RightMaskVA[3] = new ScreenPoint(1.5, 1.5);
            RightMaskVA[4] = new ScreenPoint(0, 1.5);
        }

        public void GenerateLeftMaskVO()
        {
            LeftMaskVO = new ScreenPoint[7];

            LeftMaskVO[0] = new ScreenPoint(0, 1.5);
            LeftMaskVO[1] = new ScreenPoint(1.5, 1.5);
            LeftMaskVO[2] = new ScreenPoint(1.5, -1.5);
            LeftMaskVO[3] = new ScreenPoint(0, -1.5);
            LeftMaskVO[4] = new ScreenPoint(1.5, -1.5);
            LeftMaskVO[5] = new ScreenPoint(1.5, 1.5);
            LeftMaskVO[6] = new ScreenPoint(0, 1.5);
        }

        public void GenerateRightMaskVO()
        {
            RightMaskVO = new ScreenPoint[7];

            RightMaskVO[0] = new ScreenPoint(0, 1.5);
            RightMaskVO[1] = new ScreenPoint(-1.5, 1.5);
            RightMaskVO[2] = new ScreenPoint(-1.5, -1.5);
            RightMaskVO[3] = new ScreenPoint(0, -1.5);
            RightMaskVO[4] = new ScreenPoint(-1.5, -1.5);
            RightMaskVO[5] = new ScreenPoint(-1.5, 1.5);
            RightMaskVO[6] = new ScreenPoint(0, 1.5);
        }

        public void GenerateLeftMaskVANoAnswer()
        {
            LeftMaskVANoAnswer = new ScreenPoint[12];

            LeftMaskVANoAnswer[0] = new ScreenPoint(0, 1.5);
            LeftMaskVANoAnswer[1] = new ScreenPoint(-1.5, 1.5);
            LeftMaskVANoAnswer[2] = new ScreenPoint(-1.5, -1.5);
            LeftMaskVANoAnswer[3] = new ScreenPoint(1.5, -1.5);
            LeftMaskVANoAnswer[4] = new ScreenPoint(1.5, 1.5);
            LeftMaskVANoAnswer[5] = new ScreenPoint(0, 1.5);

            LeftMaskVANoAnswer[6] = new ScreenPoint(0, 3);
            LeftMaskVANoAnswer[7] = new ScreenPoint(-0.5, 3);
            LeftMaskVANoAnswer[8] = new ScreenPoint(0, 4);
            LeftMaskVANoAnswer[9] = new ScreenPoint(0.5, 3);
            LeftMaskVANoAnswer[10] = new ScreenPoint(0, 3);
            LeftMaskVANoAnswer[11] = new ScreenPoint(0, 1.5);
        }

        public void GenerateRightMaskVANoAnswer()
        {
            RightMaskVANoAnswer = new ScreenPoint[11];

            RightMaskVANoAnswer[0] = new ScreenPoint(0, 1.5);
            RightMaskVANoAnswer[1] = new ScreenPoint(-1.5, 1.5);
            RightMaskVANoAnswer[2] = new ScreenPoint(0, -1.5);
            RightMaskVANoAnswer[3] = new ScreenPoint(1.5, 1.5);
            RightMaskVANoAnswer[4] = new ScreenPoint(0, 1.5);

            RightMaskVANoAnswer[5] = new ScreenPoint(0, 3);
            RightMaskVANoAnswer[6] = new ScreenPoint(-0.5, 3);
            RightMaskVANoAnswer[7] = new ScreenPoint(0, 4);
            RightMaskVANoAnswer[8] = new ScreenPoint(0.5, 3);
            RightMaskVANoAnswer[9] = new ScreenPoint(0, 3);
            RightMaskVANoAnswer[10] = new ScreenPoint(0, 1.5);
        }

        public void GenerateLeftMaskVONoAnswer()
        {
            LeftMaskVONoAnswer = new ScreenPoint[13];

            LeftMaskVONoAnswer[0] = new ScreenPoint(0, 1.5);
            LeftMaskVONoAnswer[1] = new ScreenPoint(1.5, 1.5);
            LeftMaskVONoAnswer[2] = new ScreenPoint(1.5, -1.5);
            LeftMaskVONoAnswer[3] = new ScreenPoint(0, -1.5);
            LeftMaskVONoAnswer[4] = new ScreenPoint(1.5, -1.5);
            LeftMaskVONoAnswer[5] = new ScreenPoint(1.5, 1.5);
            LeftMaskVONoAnswer[6] = new ScreenPoint(0, 1.5);

            LeftMaskVONoAnswer[7] = new ScreenPoint(0, 3);
            LeftMaskVONoAnswer[8] = new ScreenPoint(-0.5, 3);
            LeftMaskVONoAnswer[9] = new ScreenPoint(0, 4);
            LeftMaskVONoAnswer[10] = new ScreenPoint(0.5, 3);
            LeftMaskVONoAnswer[11] = new ScreenPoint(0, 3);
            LeftMaskVONoAnswer[12] = new ScreenPoint(0, 1.5);
        }

        public void GenerateRightMaskVONoAnswer()
        {
            RightMaskVONoAnswer = new ScreenPoint[13];

            RightMaskVONoAnswer[0] = new ScreenPoint(0, 1.5);
            RightMaskVONoAnswer[1] = new ScreenPoint(-1.5, 1.5);
            RightMaskVONoAnswer[2] = new ScreenPoint(-1.5, -1.5);
            RightMaskVONoAnswer[3] = new ScreenPoint(0, -1.5);
            RightMaskVONoAnswer[4] = new ScreenPoint(-1.5, -1.5);
            RightMaskVONoAnswer[5] = new ScreenPoint(-1.5, 1.5);
            RightMaskVONoAnswer[6] = new ScreenPoint(0, 1.5);

            RightMaskVONoAnswer[7] = new ScreenPoint(0, 3);
            RightMaskVONoAnswer[8] = new ScreenPoint(-0.5, 3);
            RightMaskVONoAnswer[9] = new ScreenPoint(0, 4);
            RightMaskVONoAnswer[10] = new ScreenPoint(0.5, 3);
            RightMaskVONoAnswer[11] = new ScreenPoint(0, 3);
            RightMaskVONoAnswer[12] = new ScreenPoint(0, 1.5);
        }

        public ScreenPoint[] GetAllMarkers(char ear, int index)
        {
            switch (index)
            {
                #region VA

                case 0:
                    if (ear == 'L')
                    {
                        return LeftVA;
                    }
                    else
                    {
                        return RightVA;
                    }

                case 1:
                    if (ear == 'L')
                    {
                        return LeftVANoAnswer;
                    }
                    else
                    {
                        return RightVANoAnswer;
                    }

                case 2:
                    if (ear == 'L')
                    {
                        return LeftMaskVA;
                    }
                    else
                    {
                        return RightMaskVA;
                    }

                case 3:
                    if (ear == 'L')
                    {
                        return LeftMaskVANoAnswer;
                    }
                    else
                    {
                        return RightMaskVANoAnswer;
                    }

                #endregion VA

                #region VO

                case 4:
                    if (ear == 'L')
                    {
                        return LeftVO;
                    }
                    else
                    {
                        return RightVO;
                    }

                case 5:
                    if (ear == 'L')
                    {
                        return LeftVONoAnswer;
                    }
                    else
                    {
                        return RightVONoAnswer;
                    }

                case 6:
                    if (ear == 'L')
                    {
                        return LeftMaskVO;
                    }
                    else
                    {
                        return RightMaskVO;
                    }

                case 7:
                    if (ear == 'L')
                    {
                        return LeftMaskVONoAnswer;
                    }
                    else
                    {
                        return RightMaskVONoAnswer;
                    }

                #endregion VO

                #region UCL

                case 8:
                    if (ear == 'L')
                    {
                        return LeftUCL;
                    }
                    else
                    {
                        return RightUCL;
                    }

                #endregion UCL

                default:
                    return null;
            }
        }

        #endregion Lines

        #region Static

        public void SetupStaticPlot(char ear, string title)
        {
            // Define o título
            AudiogramPlot.Title = title;
            AudiogramPlot.TitleFontSize = 12;
            AudiogramPlot.TitleFontWeight = 0;
            AudiogramPlot.TitleHorizontalAlignment = TitleHorizontalAlignment.CenteredWithinPlotArea;

            // Define a margem e a borda do gráfico
            AudiogramPlot.PlotMargins = new OxyThickness(double.NaN);
            AudiogramPlot.PlotAreaBorderThickness = new OxyThickness(2);
            AudiogramPlot.PlotAreaBorderColor = OxyColor.FromRgb(0x91, 0x91, 0x91);

            // Define a legenda do gráfico
            AudiogramPlot.LegendPlacement = LegendPlacement.Inside;
            AudiogramPlot.LegendPosition = LegendPosition.LeftTop;
            AudiogramPlot.LegendBackground = OxyColors.White;
            AudiogramPlot.LegendBorder = OxyColor.FromRgb(0x91, 0x91, 0x91);
            AudiogramPlot.IsLegendVisible = false;

            // Define como serão os eixos
            var yAxis = new LinearAxis();
            var xAxis = new LogarithmicAxis();
            var xAxisAux = new LogarithmicAxis();

            // Define o eixo Y
            yAxis.Position = AxisPosition.Left;
            yAxis.Minimum = -15;
            yAxis.Maximum = 125;
            yAxis.MajorStep = 10;
            yAxis.MinorStep = 10;
            yAxis.MajorGridlineStyle = LineStyle.Solid;
            yAxis.ExtraGridlineColor = OxyColors.LightGray;
            yAxis.StartPosition = 1;
            yAxis.EndPosition = 0;
            yAxis.AbsoluteMaximum = 120;
            yAxis.AbsoluteMinimum = -10;
            yAxis.IsPanEnabled = false;
            yAxis.IsZoomEnabled = false;
            yAxis.IsAxisVisible = false;
            yAxis.AxisTitleDistance = 15;

            // Define o eixo X
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Minimum = 100;
            xAxis.Maximum = 9000;
            xAxis.Base = 2;
            xAxis.Multiplier = 125;
            xAxis.ExtraGridlines = new Double[] { 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0, 8000.0 };
            xAxis.ExtraGridlineStyle = LineStyle.Solid;
            xAxis.ExtraGridlineColor = OxyColors.Gray;
            xAxis.AbsoluteMaximum = 8000;
            xAxis.AbsoluteMinimum = 125;
            xAxis.MajorStep = 2000;
            xAxis.MinorStep = 750;
            xAxis.IsPanEnabled = false;
            xAxis.IsZoomEnabled = false;
            xAxis.IsAxisVisible = false;
            xAxis.AxisTitleDistance = 15;

            // Define o eixo X auxiliar
            xAxisAux.Position = AxisPosition.Bottom;
            xAxisAux.Minimum = 100;
            xAxisAux.Maximum = 9000;
            xAxisAux.Base = 2;
            xAxisAux.Multiplier = 125;
            xAxisAux.ExtraGridlines = new Double[] { 750.0, 1500.0, 3000.0, 6000.0 };
            xAxisAux.ExtraGridlineStyle = LineStyle.Dash;
            xAxisAux.ExtraGridlineColor = OxyColors.Gray;
            xAxisAux.MajorStep = 10000;
            xAxisAux.IsPanEnabled = false;
            xAxisAux.IsZoomEnabled = false;
            xAxisAux.IsAxisVisible = false;

            // Posiciona o eixo Y de acordo com o lado da audiometria
            if (ear == 'L')
            {
                yAxis.Position = AxisPosition.Right;
            }

            // Caso não seja uma versão de impressão, estiliza o gráfico
            if (!isPrint)
            {
                AudiogramPlot.PlotAreaBorderColor = OxyColors.Transparent;
                yAxis.MajorGridlineColor = OxyColors.White;
                yAxis.ExtraGridlineColor = OxyColors.White;
                yAxis.MajorGridlineThickness = 2;
                yAxis.ExtraGridlineThickness = 2;
                xAxis.AxislineColor = OxyColors.White;
                xAxis.ExtraGridlineColor = OxyColors.White;
                xAxis.AxislineThickness = 2;
                xAxis.ExtraGridlineThickness = 2;
                xAxisAux.AxislineColor = OxyColors.White;
                xAxisAux.ExtraGridlineColor = OxyColors.White;
                xAxisAux.AxislineThickness = 2;
                xAxisAux.ExtraGridlineThickness = 2;
            }

            AudiogramPlot.Axes.Add(yAxis);
            AudiogramPlot.Axes.Add(xAxis);
            AudiogramPlot.Axes.Add(xAxisAux);

            if (ear == 'L' && !isPrint)
            {
                // Azul se a audiometria for do lado esquerdo e não for uma versão de impressão
                AudiogramPlot.PlotAreaBackground = OxyColor.FromArgb(255, 229, 225, 255);
            }
            else if (!isPrint)
            {
                // Vermelho se a audiometria for do lado direito e não for uma versão de impressão
                AudiogramPlot.PlotAreaBackground = OxyColor.FromArgb(255, 250, 232, 234);
            }
            else
            {
                // Branco se for uma versão de impressão
                AudiogramPlot.PlotAreaBackground = OxyColor.FromArgb(255, 255, 255, 255);
            }

            SetupLines(ear);
            SetupMarker(ear);
        }

        public void CreateStaticPoints(string type, double frequency, double intensity, int marker)
        {
            if (!double.IsNaN(intensity))
            {
                staticPoint = new DataPoint();
                staticPoint.X = frequency;
                staticPoint.Y = intensity;

                scatterPoint = new ScatterPoint();
                scatterPoint.X = frequency;
                scatterPoint.Y = intensity;

                key = Frequencies.FirstOrDefault(x => x.Value == scatterPoint.X).Key;

                int convert = 0;
                if (type == "VA") { convert = 0; }
                if (type == "VO") { convert = 1; }
                if (type == "UCL") { convert = 2; }
                lineArray[convert].Points.Add(staticPoint);
                markerArray[marker].Points.Add(scatterPoint);

                AudiogramPlot.InvalidatePlot(true);
            }
        }

        #endregion Static
    }
}