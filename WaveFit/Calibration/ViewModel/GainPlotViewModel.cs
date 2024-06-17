using GenericAudion16;
using GenericCommon;
using IAmp;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WaveFit2.Calibration.Class;

namespace WaveFit2.Calibration.ViewModel
{
    public class GainPlotViewModel
    {
        public PlotModel GainPlot { get; set; }

        public LineSeries[] PlotSeries;
        public LineSeries[] RuleSeries;

        // Listas dos pontos do dispositivo onde é realizado o binding
        public DataPoint[] softPoints = new DataPoint[65];

        public DataPoint[] moderatePoints = new DataPoint[65];
        public DataPoint[] loudPoints = new DataPoint[65];

        // Listas dos pontos das regras de ganho onde é realizado o binding
        public DataPoint[] ruleSoftPoints = new DataPoint[65];

        public DataPoint[] ruleModeratePoints = new DataPoint[65];
        public DataPoint[] ruleLoudPoints = new DataPoint[65];

        public float[] inputLevels = new float[11];

        public Tuple<List<double>, List<List<double>>> prescriptiveGains;

        private bool hasData;

        private DateTime dateTaken;

        private float[] channelResponseData = new float[16];

        private float[] responseData = new float[65];

        public int[] frequency = new int[65] {200, 210, 223, 236, 250, 265, 281, 297, 315, 334, 354, 375, 397, 420, 445,
                                    472, 500, 530, 561, 595, 630, 667, 707, 749, 794, 841, 891, 944, 1000, 1059, 1122, 1189, 1260,
                                   1335, 1414, 1498, 1587, 1682, 1782, 1888, 2000, 2119, 2245, 2378, 2520, 2670, 2828,
                                   2997, 3175, 3364, 3564, 3775, 4000, 4238, 4490, 4757, 5040, 5339, 5657, 5993, 6350, 6727, 7127, 7551, 8000};

        public int[] LegacyTargetFrequency = new int[11] { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 };

        public int[] UniversalTargetFrequency = new int[19] { 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250, 1600, 2000, 2500, 3150, 4000, 5000, 6300, 8000 };

        public int[] IFSAudioGramFreqs = new int[13] { 125, 188, 250, 375, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 };

        public int[] Audion16CenterFreqs = new int[16] { 125, 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000, 6500, 7000, 7625 };

        private int errorCode;

        public string response50, response80, response90, target50, target80, target90;

        public GainPlotViewModel()
        {
            GainPlot = new PlotModel();
            PlotSeries = new LineSeries[3];
            RuleSeries = new LineSeries[3];
        }

        private string FrequencyFormatter(double Frequency)
        {
            if (Frequency < 1000) return Frequency.ToString();
            else return (Frequency / 1000).ToString() + "k";
        }

        public void SetupPlot()
        {
            // Define o título
            GainPlot.Title = "Resposta";
            GainPlot.TitleFontSize = 16;
            GainPlot.TitleFontWeight = 0;
            GainPlot.TitleHorizontalAlignment = TitleHorizontalAlignment.CenteredWithinPlotArea;

            // Define a margem e a borda do gráfico
            GainPlot.PlotMargins = new OxyThickness(double.NaN);
            GainPlot.PlotAreaBorderThickness = new OxyThickness(2);
            GainPlot.PlotAreaBorderColor = OxyColor.FromRgb(0x91, 0x91, 0x91);

            // Define a legenda do gráfico
            GainPlot.LegendPlacement = LegendPlacement.Inside;
            GainPlot.LegendPosition = LegendPosition.LeftTop;
            GainPlot.LegendBackground = OxyColors.White;
            GainPlot.LegendBorder = OxyColor.FromRgb(0x91, 0x91, 0x91);
            GainPlot.LegendFontSize = 10;
            GainPlot.IsLegendVisible = true;

            // Define como serão os eixos
            var yAxis = new LinearAxis();
            var xAxis = new LogarithmicAxis();
            var xAxisAux = new LogarithmicAxis();

            // Define o eixo Y
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Ganho";
            yAxis.TitleFontSize = 12;
            yAxis.FontSize = 11;
            yAxis.Minimum = -10;
            yAxis.Maximum = 105;
            yAxis.MajorStep = 10;
            yAxis.MajorGridlineStyle = LineStyle.Solid;
            yAxis.MajorGridlineColor = OxyColors.LightGray;
            yAxis.MinorGridlineStyle = LineStyle.None;
            yAxis.MinorGridlineColor = OxyColors.LightGray;
            yAxis.Unit = "dB";
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
            xAxisAux.ExtraGridlineStyle = LineStyle.Solid;
            xAxisAux.ExtraGridlineColor = OxyColors.Gray;
            xAxisAux.MajorStep = 10000;
            xAxisAux.IsPanEnabled = false;
            xAxisAux.IsZoomEnabled = false;

            GainPlot.Axes.Add(yAxis);
            GainPlot.Axes.Add(xAxis);
            GainPlot.Axes.Add(xAxisAux);

            GainPlot.PlotAreaBackground = OxyColor.FromArgb(255, 255, 255, 255);

            SetupLinesRules();
            SetupLinesPlot();
        }

        public void SetupLinesRules()
        {
            int index = 0;
            //OxyColor color;

            for (int i = 0; i < 3; i++)
            {
                RuleSeries[i] = new LineSeries();
                RuleSeries[i].LineStyle = LineStyle.Solid;
                RuleSeries[i].StrokeThickness = 3;
                RuleSeries[i].Smooth = true;

                if (index == 0)
                {
                    RuleSeries[i].Color = OxyColor.Parse("#ef9a9a");
                }
                else if (index == 1)
                {
                    RuleSeries[i].Color = OxyColor.Parse("#81d4fa");
                }
                else
                {
                    RuleSeries[i].Color = OxyColor.Parse("#c5e1a5");
                }

                // Define o título da linha
                switch (index)
                {
                    case 0:
                        RuleSeries[i].Title = "50 dB Alvo";
                        break;

                    case 1:
                        RuleSeries[i].Title = "80 dB Alvo";
                        break;

                    case 2:
                        RuleSeries[i].Title = "90 dB Alvo";
                        break;
                }

                GainPlot.Series.Add(RuleSeries[i]);
                index++;
            }
        }

        public void SetupLinesPlot()
        {
            int index = 0;
            //OxyColor color;

            for (int i = 0; i < 3; i++)
            {
                PlotSeries[i] = new LineSeries();
                PlotSeries[i].LineStyle = LineStyle.Dot;
                PlotSeries[i].StrokeThickness = 3;
                PlotSeries[i].Smooth = false;

                if (index == 0)
                {
                    PlotSeries[i].Color = OxyColors.Red;
                }
                else if (index == 1)
                {
                    PlotSeries[i].Color = OxyColors.Blue;
                }
                else
                {
                    PlotSeries[i].Color = OxyColors.Green;
                }

                // Define o título da linha
                switch (index)
                {
                    case 0:
                        PlotSeries[i].Title = "50 dB Resposta";
                        break;

                    case 1:
                        PlotSeries[i].Title = "80 dB Resposta";
                        break;

                    case 2:
                        PlotSeries[i].Title = "90 dB Resposta";
                        break;
                }

                GainPlot.Series.Add(PlotSeries[i]);
                index++;
            }
        }

        public void setResponseLegacyTargetArray(WaveRule waveRule, int inputLevel)
        {
            try
            {
                responseData = new float[11];

                if (inputLevel <= 50)
                {
                    responseData[0] = (float)waveRule.targetGains[0][0];
                    responseData[1] = (float)waveRule.targetGains[0][1];
                    responseData[2] = (float)waveRule.targetGains[0][2];
                    responseData[3] = (float)waveRule.targetGains[0][3];
                    responseData[4] = (float)waveRule.targetGains[0][4];
                    responseData[5] = (float)waveRule.targetGains[0][5];
                    responseData[6] = (float)waveRule.targetGains[0][6];
                    responseData[7] = (float)waveRule.targetGains[0][7];
                    responseData[8] = (float)waveRule.targetGains[0][8];
                    responseData[9] = (float)waveRule.targetGains[0][9];
                    responseData[10] = (float)waveRule.targetGains[0][10];

                    for (int i = 0; i <= 10; i++)
                    {
                        RuleSeries[0].Points.Add(new DataPoint(LegacyTargetFrequency[i], responseData[i]));
                    }
                }
                else if (inputLevel > 50 && inputLevel <= 85)
                {
                    responseData[0] = (float)waveRule.targetGains[1][0];
                    responseData[1] = (float)waveRule.targetGains[1][1];
                    responseData[2] = (float)waveRule.targetGains[1][2];
                    responseData[3] = (float)waveRule.targetGains[1][3];
                    responseData[4] = (float)waveRule.targetGains[1][4];
                    responseData[5] = (float)waveRule.targetGains[1][5];
                    responseData[6] = (float)waveRule.targetGains[1][6];
                    responseData[7] = (float)waveRule.targetGains[1][7];
                    responseData[8] = (float)waveRule.targetGains[1][8];
                    responseData[9] = (float)waveRule.targetGains[1][9];
                    responseData[10] = (float)waveRule.targetGains[1][10];

                    for (int i = 0; i <= 10; i++)
                    {
                        RuleSeries[1].Points.Add(new DataPoint(LegacyTargetFrequency[i], responseData[i]));
                    }
                }
                else if (inputLevel > 85)
                {
                    responseData[0] = (float)waveRule.targetGains[2][0];
                    responseData[1] = (float)waveRule.targetGains[2][1];
                    responseData[2] = (float)waveRule.targetGains[2][2];
                    responseData[3] = (float)waveRule.targetGains[2][3];
                    responseData[4] = (float)waveRule.targetGains[2][4];
                    responseData[5] = (float)waveRule.targetGains[2][5];
                    responseData[6] = (float)waveRule.targetGains[2][6];
                    responseData[7] = (float)waveRule.targetGains[2][7];
                    responseData[8] = (float)waveRule.targetGains[2][8];
                    responseData[9] = (float)waveRule.targetGains[2][9];
                    responseData[10] = (float)waveRule.targetGains[2][10];

                    for (int i = 0; i <= 10; i++)
                    {
                        RuleSeries[2].Points.Add(new DataPoint(LegacyTargetFrequency[i], responseData[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void A16plotResponse(short inputLevel, float[] A16micFR, float[] A16recFR, ref float[] A16FreqResp, G_Common GDriver)
        {
            errorCode = GDriver.SetMicResponse(A16micFR);
            if (errorCode == 0)
            {
                errorCode = GDriver.SetRecResponse(A16recFR);
                if (errorCode == 0)
                {
                    A16FreqResp = (GDriver as G_Audion16).GetFrArray(inputLevel);
                    if (errorCode != 0)
                    {
                        MessageBox.Show("Plot Response driver error #" + errorCode);
                    }
                    else
                    {
                        for (int i = 0; i <= 64; i++)
                        {
                            if (inputLevel <= 50)
                            {
                                softPoints[i].Y = A16FreqResp[i] - inputLevel;
                                softPoints[i].X = frequency[i];
                                PlotSeries[0].Points.Add(softPoints[i]);
                            }
                            else if (inputLevel > 50 && inputLevel <= 85)
                            {
                                moderatePoints[i].Y = A16FreqResp[i] - inputLevel;
                                moderatePoints[i].X = frequency[i];
                                PlotSeries[1].Points.Add(moderatePoints[i]);
                            }
                            else if (inputLevel > 85)
                            {
                                loudPoints[i].Y = A16FreqResp[i] - inputLevel;
                                loudPoints[i].X = frequency[i];
                                PlotSeries[2].Points.Add(loudPoints[i]);
                            }
                        }
                    }
                }
            }
        }

        private void A8plotResponse(short inputLevel, Audion8.Response A8micFR, Audion8.Response A8recFR, Audion8.Response A8FreqResp)
        {
            errorCode = Audion8.Set_mic_response(ref A8micFR);
            if (errorCode == 0)
            {
                errorCode = Audion8.Set_rec_response(ref A8recFR);
                if (errorCode == 0)
                {
                    errorCode = Audion8.Get_FR_array(inputLevel, ref A8FreqResp);
                    if (errorCode != 0)
                    {
                        MessageBox.Show("Plot Response driver error #" + errorCode);
                    }
                    else
                    {
                        for (int i = 0; i <= 64; i++)
                        {
                            if (inputLevel <= 50)
                            {
                                softPoints[i].Y = A8FreqResp.element[i] - inputLevel;
                                softPoints[i].X = frequency[i];
                                PlotSeries[0].Points.Add(softPoints[i]);
                            }
                            else if (inputLevel > 50 && inputLevel <= 85)
                            {
                                moderatePoints[i].Y = A8FreqResp.element[i] - inputLevel;
                                moderatePoints[i].X = frequency[i];
                                PlotSeries[1].Points.Add(moderatePoints[i]);
                            }
                            else if (inputLevel > 85)
                            {
                                loudPoints[i].Y = A8FreqResp.element[i] - inputLevel;
                                loudPoints[i].X = frequency[i];
                                PlotSeries[2].Points.Add(loudPoints[i]);
                            }
                        }
                    }
                }
            }
        }

        private void A6plotResponse(short inputLevel, Audion6.Response A6micFR, Audion6.Response A6recFR, Audion6.Response A6FreqResp)
        {
            errorCode = Audion6.Set_mic_response(ref A6micFR);
            if (errorCode == 0)
            {
                errorCode = Audion6.Set_rec_response(ref A6recFR);
                if (errorCode == 0)
                {
                    errorCode = Audion6.Get_FR_array(inputLevel, ref A6FreqResp);
                    if (errorCode != 0)
                    {
                        MessageBox.Show("Plot Response driver error #" + errorCode);
                    }
                    else
                    {
                        for (int i = 0; i <= 64; i++)
                        {
                            if (inputLevel <= 50)
                            {
                                softPoints[i].Y = A6FreqResp.element[i] - inputLevel;
                                softPoints[i].X = frequency[i];
                                PlotSeries[0].Points.Add(softPoints[i]);
                            }
                            else if (inputLevel > 50 && inputLevel <= 85)
                            {
                                moderatePoints[i].Y = A6FreqResp.element[i] - inputLevel;
                                moderatePoints[i].X = frequency[i];
                                PlotSeries[1].Points.Add(moderatePoints[i]);
                            }
                            else if (inputLevel > 85)
                            {
                                loudPoints[i].Y = A6FreqResp.element[i] - inputLevel;
                                loudPoints[i].X = frequency[i];
                                PlotSeries[2].Points.Add(loudPoints[i]);
                            }
                        }
                    }
                }
            }
        }

        private void A4plotResponse(short inputLevel, Audion4.Response A4micFR, Audion4.Response A4recFR, Audion4.Response A4FreqResp)
        {
            errorCode = Audion4.Set_mic_response(ref A4micFR);
            if (errorCode == 0)
            {
                errorCode = Audion4.Set_rec_response(ref A4recFR);
                if (errorCode == 0)
                {
                    errorCode = Audion4.Get_FR_array(inputLevel, ref A4FreqResp);
                    if (errorCode != 0)
                    {
                        MessageBox.Show("Plot Response driver error #" + errorCode);
                    }
                    else
                    {
                        for (int i = 0; i <= 64; i++)
                        {
                            if (inputLevel <= 50)
                            {
                                softPoints[i].Y = A4FreqResp.element[i] - inputLevel;
                                softPoints[i].X = frequency[i];
                                PlotSeries[0].Points.Add(softPoints[i]);
                            }
                            else if (inputLevel > 50 && inputLevel <= 85)
                            {
                                moderatePoints[i].Y = A4FreqResp.element[i] - inputLevel;
                                moderatePoints[i].X = frequency[i];
                                PlotSeries[1].Points.Add(moderatePoints[i]);
                            }
                            else if (inputLevel > 85)
                            {
                                loudPoints[i].Y = A4FreqResp.element[i] - inputLevel;
                                loudPoints[i].X = frequency[i];
                                PlotSeries[2].Points.Add(loudPoints[i]);
                            }
                        }
                    }
                }
            }
        }

        private void SNRplotResponse(short inputLevel, SpinNR.Response SNRmicFR, SpinNR.Response SNRrecFR, SpinNR.Response SNRFreqResp)
        {
            errorCode = SpinNR.Set_mic_response(ref SNRmicFR);
            if (errorCode == 0)
            {
                errorCode = SpinNR.Set_rec_response(ref SNRrecFR);
                if (errorCode == 0)
                {
                    errorCode = SpinNR.Get_FR_array(inputLevel, ref SNRFreqResp);
                    if (errorCode != 0)
                    {
                        MessageBox.Show("Plot Response driver error #" + errorCode);
                    }
                    else
                    {
                        for (int i = 0; i <= 64; i++)
                        {
                            if (inputLevel <= 50)
                            {
                                softPoints[i].Y = SNRFreqResp.element[i] - inputLevel;
                                softPoints[i].X = frequency[i];
                                PlotSeries[0].Points.Add(softPoints[i]);
                            }
                            else if (inputLevel > 50 && inputLevel <= 85)
                            {
                                moderatePoints[i].Y = SNRFreqResp.element[i] - inputLevel;
                                moderatePoints[i].X = frequency[i];
                                PlotSeries[1].Points.Add(moderatePoints[i]);
                            }
                            else if (inputLevel > 85)
                            {
                                loudPoints[i].Y = SNRFreqResp.element[i] - inputLevel;
                                loudPoints[i].X = frequency[i];
                                PlotSeries[2].Points.Add(loudPoints[i]);
                            }
                        }
                    }
                }
            }
        }

        public void A16Plot50_80_MPO(WaveRule waveRule, float[] A16micFR, float[] A16recFR, float[] A16FreqResp, G_Common GDriver)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            A16plotResponse(50, A16micFR, A16recFR, ref A16FreqResp, GDriver);
            setResponseLegacyTargetArray(waveRule, 50);

            A16plotResponse(80, A16micFR, A16recFR, ref A16FreqResp, GDriver);
            setResponseLegacyTargetArray(waveRule, 80);

            A16plotResponse(90, A16micFR, A16recFR, ref A16FreqResp, GDriver);
            setResponseLegacyTargetArray(waveRule, 90);

            GainPlot.InvalidatePlot(true);

            for (int i = 0; i < PlotSeries[0].Points.Count(); i++)
            {
                if (i == PlotSeries[0].Points.Count() - 1)
                {
                    response50 = response50 + PlotSeries[0].Points[i].X + "&" + PlotSeries[0].Points[i].Y;
                    response80 = response80 + PlotSeries[1].Points[i].X + "&" + PlotSeries[1].Points[i].Y;
                    response90 = response90 + PlotSeries[2].Points[i].X + "&" + PlotSeries[2].Points[i].Y;
                }
                else
                {
                    response50 = response50 + PlotSeries[0].Points[i].X + "&" + PlotSeries[0].Points[i].Y + "&";
                    response80 = response80 + PlotSeries[1].Points[i].X + "&" + PlotSeries[1].Points[i].Y + "&";
                    response90 = response90 + PlotSeries[2].Points[i].X + "&" + PlotSeries[2].Points[i].Y + "&";
                }
            }

            for (int i = 0; i < RuleSeries[0].Points.Count(); i++)
            {
                if (i == PlotSeries[0].Points.Count() - 1)
                {
                    target50 = target50 + RuleSeries[0].Points[i].X + "&" + RuleSeries[0].Points[i].Y;
                    target80 = target80 + RuleSeries[1].Points[i].X + "&" + RuleSeries[1].Points[i].Y;
                    target90 = target90 + RuleSeries[2].Points[i].X + "&" + RuleSeries[2].Points[i].Y;
                }
                else
                {
                    target50 = target50 + RuleSeries[0].Points[i].X + "&" + RuleSeries[0].Points[i].Y + "&";
                    target80 = target80 + RuleSeries[1].Points[i].X + "&" + RuleSeries[1].Points[i].Y + "&";
                    target90 = target90 + RuleSeries[2].Points[i].X + "&" + RuleSeries[2].Points[i].Y + "&";
                }
            }
        }

        public void A8Plot50_80_MPO(WaveRule waveRule, Audion8.Response A8micFR, Audion8.Response A8recFR, Audion8.Response A8FreqResp)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            A8plotResponse(50, A8micFR, A8recFR, A8FreqResp);
            setResponseLegacyTargetArray(waveRule, 50);

            A8plotResponse(80, A8micFR, A8recFR, A8FreqResp);
            setResponseLegacyTargetArray(waveRule, 80);

            A8plotResponse(90, A8micFR, A8recFR, A8FreqResp);
            setResponseLegacyTargetArray(waveRule, 90);

            GainPlot.InvalidatePlot(true);

            for (int i = 0; i < PlotSeries[0].Points.Count(); i++)
            {
                if (i == PlotSeries[0].Points.Count() - 1)
                {
                    response50 = response50 + PlotSeries[0].Points[i].X + "&" + PlotSeries[0].Points[i].Y;
                    response80 = response80 + PlotSeries[1].Points[i].X + "&" + PlotSeries[1].Points[i].Y;
                    response90 = response90 + PlotSeries[2].Points[i].X + "&" + PlotSeries[2].Points[i].Y;
                }
                else
                {
                    response50 = response50 + PlotSeries[0].Points[i].X + "&" + PlotSeries[0].Points[i].Y + "&";
                    response80 = response80 + PlotSeries[1].Points[i].X + "&" + PlotSeries[1].Points[i].Y + "&";
                    response90 = response90 + PlotSeries[2].Points[i].X + "&" + PlotSeries[2].Points[i].Y + "&";
                }
            }

            for (int i = 0; i < RuleSeries[0].Points.Count(); i++)
            {
                if (i == PlotSeries[0].Points.Count() - 1)
                {
                    target50 = target50 + RuleSeries[0].Points[i].X + "&" + RuleSeries[0].Points[i].Y;
                    target80 = target80 + RuleSeries[1].Points[i].X + "&" + RuleSeries[1].Points[i].Y;
                    target90 = target90 + RuleSeries[2].Points[i].X + "&" + RuleSeries[2].Points[i].Y;
                }
                else
                {
                    target50 = target50 + RuleSeries[0].Points[i].X + "&" + RuleSeries[0].Points[i].Y + "&";
                    target80 = target80 + RuleSeries[1].Points[i].X + "&" + RuleSeries[1].Points[i].Y + "&";
                    target90 = target90 + RuleSeries[2].Points[i].X + "&" + RuleSeries[2].Points[i].Y + "&";
                }
            }
        }

        public void A6Plot50_80_MPO(WaveRule waveRule, Audion6.Response A6micFR, Audion6.Response A6recFR, Audion6.Response A6FreqResp)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            A6plotResponse(50, A6micFR, A6recFR, A6FreqResp);
            setResponseLegacyTargetArray(waveRule, 50);

            A6plotResponse(80, A6micFR, A6recFR, A6FreqResp);
            setResponseLegacyTargetArray(waveRule, 80);

            A6plotResponse(90, A6micFR, A6recFR, A6FreqResp);
            setResponseLegacyTargetArray(waveRule, 90);

            GainPlot.InvalidatePlot(true);
        }

        public void A4Plot50_80_MPO(WaveRule waveRule, Audion4.Response A4micFR, Audion4.Response A4recFR, Audion4.Response A4FreqResp)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            A4plotResponse(50, A4micFR, A4recFR, A4FreqResp);
            setResponseLegacyTargetArray(waveRule, 50);

            A4plotResponse(80, A4micFR, A4recFR, A4FreqResp);
            setResponseLegacyTargetArray(waveRule, 80);

            A4plotResponse(90, A4micFR, A4recFR, A4FreqResp);
            setResponseLegacyTargetArray(waveRule, 90);

            GainPlot.InvalidatePlot(true);

            for (int i = 0; i < PlotSeries[0].Points.Count(); i++)
            {
                if (i == PlotSeries[0].Points.Count() - 1)
                {
                    response50 = response50 + PlotSeries[0].Points[i].X + "&" + PlotSeries[0].Points[i].Y;
                    response80 = response80 + PlotSeries[1].Points[i].X + "&" + PlotSeries[1].Points[i].Y;
                    response90 = response90 + PlotSeries[2].Points[i].X + "&" + PlotSeries[2].Points[i].Y;
                }
                else
                {
                    response50 = response50 + PlotSeries[0].Points[i].X + "&" + PlotSeries[0].Points[i].Y + "&";
                    response80 = response80 + PlotSeries[1].Points[i].X + "&" + PlotSeries[1].Points[i].Y + "&";
                    response90 = response90 + PlotSeries[2].Points[i].X + "&" + PlotSeries[2].Points[i].Y + "&";
                }
            }

            for (int i = 0; i < RuleSeries[0].Points.Count(); i++)
            {
                if (i == PlotSeries[0].Points.Count() - 1)
                {
                    target50 = target50 + RuleSeries[0].Points[i].X + "&" + RuleSeries[0].Points[i].Y;
                    target80 = target80 + RuleSeries[1].Points[i].X + "&" + RuleSeries[1].Points[i].Y;
                    target90 = target90 + RuleSeries[2].Points[i].X + "&" + RuleSeries[2].Points[i].Y;
                }
                else
                {
                    target50 = target50 + RuleSeries[0].Points[i].X + "&" + RuleSeries[0].Points[i].Y + "&";
                    target80 = target80 + RuleSeries[1].Points[i].X + "&" + RuleSeries[1].Points[i].Y + "&";
                    target90 = target90 + RuleSeries[2].Points[i].X + "&" + RuleSeries[2].Points[i].Y + "&";
                }
            }
        }

        public void SNRPlot50_80_MPO(WaveRule waveRule, SpinNR.Response SNRmicFR, SpinNR.Response SNRrecFR, SpinNR.Response SNRFreqResp)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            SNRplotResponse(50, SNRmicFR, SNRrecFR, SNRFreqResp);
            setResponseLegacyTargetArray(waveRule, 50);

            SNRplotResponse(80, SNRmicFR, SNRrecFR, SNRFreqResp);
            setResponseLegacyTargetArray(waveRule, 80);

            SNRplotResponse(90, SNRmicFR, SNRrecFR, SNRFreqResp);
            setResponseLegacyTargetArray(waveRule, 90);

            GainPlot.InvalidatePlot(true);

            for (int i = 0; i < PlotSeries[0].Points.Count(); i++)
            {
                if (i == PlotSeries[0].Points.Count() - 1)
                {
                    response50 = response50 + PlotSeries[0].Points[i].X + "&" + PlotSeries[0].Points[i].Y;
                    response80 = response80 + PlotSeries[1].Points[i].X + "&" + PlotSeries[1].Points[i].Y;
                    response90 = response90 + PlotSeries[2].Points[i].X + "&" + PlotSeries[2].Points[i].Y;
                }
                else
                {
                    response50 = response50 + PlotSeries[0].Points[i].X + "&" + PlotSeries[0].Points[i].Y + "&";
                    response80 = response80 + PlotSeries[1].Points[i].X + "&" + PlotSeries[1].Points[i].Y + "&";
                    response90 = response90 + PlotSeries[2].Points[i].X + "&" + PlotSeries[2].Points[i].Y + "&";
                }
            }

            for (int i = 0; i < RuleSeries[0].Points.Count(); i++)
            {
                if (i == PlotSeries[0].Points.Count() - 1)
                {
                    target50 = target50 + RuleSeries[0].Points[i].X + "&" + RuleSeries[0].Points[i].Y;
                    target80 = target80 + RuleSeries[1].Points[i].X + "&" + RuleSeries[1].Points[i].Y;
                    target90 = target90 + RuleSeries[2].Points[i].X + "&" + RuleSeries[2].Points[i].Y;
                }
                else
                {
                    target50 = target50 + RuleSeries[0].Points[i].X + "&" + RuleSeries[0].Points[i].Y + "&";
                    target80 = target80 + RuleSeries[1].Points[i].X + "&" + RuleSeries[1].Points[i].Y + "&";
                    target90 = target90 + RuleSeries[2].Points[i].X + "&" + RuleSeries[2].Points[i].Y + "&";
                }
            }
        }

        public void A16Plot(float[] A16micFR, float[] A16recFR, float[] A16FreqResp, G_Common GDriver)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            A16plotResponse(50, A16micFR, A16recFR, ref A16FreqResp, GDriver);
            A16plotResponse(80, A16micFR, A16recFR, ref A16FreqResp, GDriver);
            A16plotResponse(90, A16micFR, A16recFR, ref A16FreqResp, GDriver);

            GainPlot.InvalidatePlot(true);
        }

        public void A8Plot(Audion8.Response A8micFR, Audion8.Response A8recFR, Audion8.Response A8FreqResp)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            A8plotResponse(50, A8micFR, A8recFR, A8FreqResp);
            A8plotResponse(80, A8micFR, A8recFR, A8FreqResp);
            A8plotResponse(90, A8micFR, A8recFR, A8FreqResp);

            GainPlot.InvalidatePlot(true);
        }

        public void A6Plot(Audion6.Response A6micFR, Audion6.Response A6recFR, Audion6.Response A6FreqResp)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            A6plotResponse(50, A6micFR, A6recFR, A6FreqResp);
            A6plotResponse(80, A6micFR, A6recFR, A6FreqResp);
            A6plotResponse(90, A6micFR, A6recFR, A6FreqResp);

            GainPlot.InvalidatePlot(true);
        }

        public void A4Plot(Audion4.Response A4micFR, Audion4.Response A4recFR, Audion4.Response A4FreqResp)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            A4plotResponse(50, A4micFR, A4recFR, A4FreqResp);
            A4plotResponse(80, A4micFR, A4recFR, A4FreqResp);
            A4plotResponse(90, A4micFR, A4recFR, A4FreqResp);

            GainPlot.InvalidatePlot(true);
        }

        public void SNRPlot(SpinNR.Response SNRmicFR, SpinNR.Response SNRrecFR, SpinNR.Response SNRFreqResp)
        {
            PlotSeries[0].Points.Clear();
            PlotSeries[1].Points.Clear();
            PlotSeries[2].Points.Clear();

            RuleSeries[0].Points.Clear();
            RuleSeries[1].Points.Clear();
            RuleSeries[2].Points.Clear();

            SNRplotResponse(50, SNRmicFR, SNRrecFR, SNRFreqResp);
            SNRplotResponse(80, SNRmicFR, SNRrecFR, SNRFreqResp);
            SNRplotResponse(90, SNRmicFR, SNRrecFR, SNRFreqResp);

            GainPlot.InvalidatePlot(true);
        }
    }
}