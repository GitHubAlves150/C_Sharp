using MathNet.Numerics.Interpolation;
using System;
using System.Collections.Generic;
using System.Linq;
using WaveFit2.Audiogram.ViewModel;
using WaveFit2.Database.CRUD;

namespace WaveFit2.Calibration.Class
{
    public class WaveRule
    {
        public Crud crud = new Crud();
        public AudiographModel audiographModel = new AudiographModel();
        public List<List<double>> targetGains;
        public Tuple<List<double>, List<List<double>>> prescriptiveGains;
        private double[] waveWeight = { 0.92, 0.92, 0.95, 1, 1.02, 1.06, 1.1, 1.1, 1.12, 1.12, 1.1 };

        private double[] soundIntensity = { 50, 65, 90 };

        public int audiogramId;
        public int[,] Marker;
        public string[] Type;
        public char[] Ear;
        public double[,] Intensity;
        private double[] keyFrequencies = new double[] { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 };

        public void FillTargetGains(char ear, bool aidInBothSides, int adaptation, double ventSize)
        {
            try
            {
                prescriptiveGains = CalcWaveRule(ear, aidInBothSides, adaptation, ventSize);
                targetGains = new List<List<double>>();

                for (int i = 0; i < prescriptiveGains.Item2.Count; i++)
                {
                    targetGains.Add(new List<double>());

                    for (int keyFrequencyIndex = 0, prescriptiveFrequencyIndex = 0;
                        keyFrequencyIndex < keyFrequencies.Count();
                        keyFrequencyIndex++)
                    {
                        if (Math.Abs(keyFrequencies[keyFrequencyIndex] - prescriptiveGains.Item1[prescriptiveFrequencyIndex]) < 0.01)
                        {
                            targetGains[i].Add(prescriptiveGains.Item2[i][prescriptiveFrequencyIndex]);

                            if (prescriptiveFrequencyIndex < (prescriptiveGains.Item1.Count - 1))
                            {
                                prescriptiveFrequencyIndex++;
                            }
                        }
                        else
                        {
                            if (keyFrequencies[keyFrequencyIndex] < prescriptiveGains.Item1[0])
                            {
                                targetGains[i].Add(prescriptiveGains.Item2[i][0]);
                            }
                            else
                            {
                                if (keyFrequencies[keyFrequencyIndex] >
                                    prescriptiveGains.Item1[prescriptiveGains.Item1.Count - 1])
                                {
                                    targetGains[i].Add(prescriptiveGains.Item2[i][prescriptiveGains.Item1.Count - 1]);
                                }
                                else
                                {
                                    double interpolationValue;

                                    int closestKeyFrequencyUpIndex = 0;
                                    int closestKeyFrequencyDownIndex = 0;

                                    int closestPrescriptiveFrequencyUpIndex = 0;
                                    int closestPrescriptiveFrequencyDownIndex = 0;

                                    for (int l = prescriptiveGains.Item1.Count - 1; l >= 0; l--)
                                    {
                                        if (prescriptiveGains.Item1[l] > keyFrequencies[keyFrequencyIndex])
                                        {
                                            closestKeyFrequencyUpIndex =
                                                Array.IndexOf(keyFrequencies, keyFrequencies.OrderBy(x => Math.Abs((long)x - prescriptiveGains.Item1[l])).First());
                                            closestPrescriptiveFrequencyUpIndex = l;
                                        }
                                    }

                                    for (int l = 0; l < prescriptiveGains.Item1.Count; l++)
                                    {
                                        if (prescriptiveGains.Item1[l] < keyFrequencies[keyFrequencyIndex])
                                        {
                                            closestKeyFrequencyDownIndex =
                                                Array.IndexOf(keyFrequencies, keyFrequencies.OrderBy(x => Math.Abs((long)x - prescriptiveGains.Item1[l])).First());
                                            closestPrescriptiveFrequencyDownIndex = l;
                                        }
                                    }

                                    int upperFrequencyDistance = closestKeyFrequencyUpIndex - keyFrequencyIndex;
                                    int lowerFrequencyDistance = keyFrequencyIndex - closestKeyFrequencyDownIndex;

                                    interpolationValue = (prescriptiveGains.Item2[i][closestPrescriptiveFrequencyDownIndex] *
                                                          upperFrequencyDistance +
                                                          prescriptiveGains.Item2[i][closestPrescriptiveFrequencyUpIndex] *
                                                          lowerFrequencyDistance) /
                                                         (upperFrequencyDistance + lowerFrequencyDistance);

                                    targetGains[i].Add(interpolationValue);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Tuple<List<double>, List<List<double>>> CalcWaveRule(char ear, bool aidInBothSides, int adaptation, double ventSize)
        {
            audiogramId = Properties.Settings.Default.audiogramId;
            double[] keyFrequencies = { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 };
            var genderAdjusts = new int[3] { 1, 1, 0 };
            var pediatricAdjusts = new int[3, 11]
            {
                {12,12,10,9,10,9,11,17,18,13,14},
                {5,5,6,7,7,6,6,13,15,11,12},
                {5,5,6,6,9,8,10,13,14,10,10}
            };
            var AuralAdjusts = new int[3, 11]
            {
                {0,0,-1,-2,-3,-3,-4,-3,-3,-2,-3},
                {0,0,-3,-3,-4,-4,-4,-4,-4,-4,-4},
                {-2,-2,-3,-2,-3,-3,-4,-4,-4,-3,-3}
            };
            var AdaptationAdjusts = new int[2, 3, 11]
            {
                {
                    {-1,-1,-2,-2,-3,-3,-4,-6,-5,-6,-6},
                    {-1,-1,-3,-2,-2,-2,-3,-5,-5,-5,-5},
                    {-3,-3,-3,-3,-3,-3,-4,-6,-5,-6,-6}
                },
                {
                    {0,0,-1,-1,-1,-1,-2,-3,-2,-2,-3},
                    {-1,-1,-1,-1,-1,-1,-1,-2,-2,-2,-2},
                    {-2,-2,-2,-2,-2,-2,-2,-3,-2,-3,-3}
                }
            };

            DateTime birthday = crud.GetAtributeDate("birthday", "dbo.patients", "id", Properties.Settings.Default.patientId);
            int age = DateTime.Today.Year - birthday.Year;
            if (DateTime.Now.Month < birthday.Month ||
                (DateTime.Now.Month == birthday.Month && DateTime.Now.Day < birthday.Day))
            {
                age--;
            }

            string gender = crud.GetGender(Properties.Settings.Default.patientId);

            audiographModel = crud.GetAudiograph(audiogramId);

            double audiogramMean = CalculateAudiogramMean(audiographModel, ear);

            var (audXPoints, audYPoints) = CalculateAudPoints(audiographModel, audiogramMean, ear);

            var intensityLevel = CalculateIntensityLevel(audYPoints);

            var ventSizeAdjusts = VentSizeAdjustArray(ventSize);

            for (int intensityIndex = 0; intensityIndex < intensityLevel.Count; intensityIndex++)
            {
                for (int keyFrequencyIndex = 0, audiogramXIndex = 0; keyFrequencyIndex < keyFrequencies.Length; keyFrequencyIndex++)
                {
                    if (keyFrequencies[keyFrequencyIndex] == audXPoints[audiogramXIndex])
                    {
                        intensityLevel[intensityIndex][audiogramXIndex] = intensityLevel[intensityIndex][audiogramXIndex] + ventSizeAdjusts[intensityIndex, keyFrequencyIndex];

                        if ((adaptation == 0) || (adaptation == 1))
                        {
                            intensityLevel[intensityIndex][audiogramXIndex] = intensityLevel[intensityIndex][audiogramXIndex]
                                + AdaptationAdjusts[adaptation, intensityIndex, keyFrequencyIndex];
                        }
                        if (audiogramXIndex < (audXPoints.Count - 1))
                        {
                            audiogramXIndex++;
                        }
                    }
                }
            }

            if (age < 12)
            {
                for (int intensityIndex = 0; intensityIndex < intensityLevel.Count; intensityIndex++)
                {
                    for (int keyFrequencyIndex = 0, audiogramXIndex = 0; keyFrequencyIndex < keyFrequencies.Length; keyFrequencyIndex++)
                    {
                        if (keyFrequencies[keyFrequencyIndex] == audXPoints[audiogramXIndex])
                        {
                            intensityLevel[intensityIndex][audiogramXIndex] = intensityLevel[intensityIndex][audiogramXIndex]
                                + Convert.ToInt32(pediatricAdjusts[intensityIndex, keyFrequencyIndex] * ((1728 - Math.Pow(age, 3)) / 1728));

                            if (aidInBothSides)
                            {
                                intensityLevel[intensityIndex][audiogramXIndex] = intensityLevel[intensityIndex][audiogramXIndex]
                                    + Convert.ToInt32(AuralAdjusts[intensityIndex, keyFrequencyIndex] * (Math.Pow(age, 3) / 1728));
                            }
                            if (audiogramXIndex < (audXPoints.Count - 1))
                            {
                                audiogramXIndex++;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int intensityIndex = 0; intensityIndex < intensityLevel.Count; intensityIndex++)
                {
                    for (int keyFrequencyIndex = 0, audiogramXIndex = 0; keyFrequencyIndex < keyFrequencies.Length; keyFrequencyIndex++)
                    {
                        if (keyFrequencies[keyFrequencyIndex] == audXPoints[audiogramXIndex])
                        {
                            if (aidInBothSides)
                            {
                                intensityLevel[intensityIndex][audiogramXIndex] = intensityLevel[intensityIndex][audiogramXIndex]
                                    + AuralAdjusts[intensityIndex, keyFrequencyIndex];
                            }

                            if (gender.Equals("Masculino"))
                            {
                                intensityLevel[intensityIndex][audiogramXIndex] = intensityLevel[intensityIndex][audiogramXIndex] + genderAdjusts[0];
                            }
                            if (gender.Equals("Feminino"))
                            {
                                intensityLevel[intensityIndex][audiogramXIndex] = intensityLevel[intensityIndex][audiogramXIndex] + genderAdjusts[1];
                            }

                            if (audiogramXIndex < (audXPoints.Count - 1))
                            {
                                audiogramXIndex++;
                            }
                        }
                    }
                }
            }

            for (int intensityIndex = 0; intensityIndex < intensityLevel.Count; intensityIndex++)
            {
                for (int audiogramXIndex = 0; audiogramXIndex < audXPoints.Count; audiogramXIndex++)
                {
                    if (intensityLevel[intensityIndex][audiogramXIndex] < 0)
                    {
                        intensityLevel[intensityIndex][audiogramXIndex] = 0;
                    }
                }
            }

            Tuple<List<double>, List<List<double>>> desiredGainTuple = Tuple.Create(audXPoints, intensityLevel);

            return desiredGainTuple;
        }

        public double CalculateAudiogramMean(AudiographModel audiographModel, char ear)
        {
            double audiogramMean = 0;
            int audiogramNumberOfPoints = 0;

            for (int i = 0; i < 11; i++)
            {
                if (ear == 'R')
                {
                    if (!Double.IsNaN(audiographModel.Intensity[0, i]))
                    {
                        audiogramMean += audiographModel.Intensity[0, i];

                        audiogramNumberOfPoints++;
                    }
                }
                else
                {
                    if (!Double.IsNaN(audiographModel.Intensity[1, i]))
                    {
                        audiogramMean += audiographModel.Intensity[1, i];

                        audiogramNumberOfPoints++;
                    }
                }
            }

            return audiogramMean / audiogramNumberOfPoints;
        }

        public (List<double> audXPoints, List<double> audYPoints) CalculateAudPoints(AudiographModel audiographModel, double audiogramMean, char ear)
        {
            var audXPoints = new List<double>();
            var audYPoints = new List<double>();
            var validXPoints = new List<double>();
            var validYPoints = new List<double>();

            // Coletar pontos válidos
            for (int i = 0; i < 11; i++)
            {
                if (!Double.IsNaN(audiographModel.Intensity[ear == 'R' ? 0 : 1, i]))
                {
                    validXPoints.Add(audiographModel.Frequency[i]);
                    validYPoints.Add(audiographModel.Intensity[ear == 'R' ? 0 : 1, i]);
                }
            }

            // Criar função de interpolação cúbica com pontos válidos
            var cubicSpline = LinearSpline.Interpolate(validXPoints, validYPoints);

            // Calcular todos os pontos, preenchendo valores ausentes com interpolação
            for (int i = 0; i < 11; i++)
            {
                double yValue;
                if (!Double.IsNaN(audiographModel.Intensity[ear == 'R' ? 0 : 1, i]))
                {
                    yValue = audiographModel.Intensity[ear == 'R' ? 0 : 1, i];
                }
                else
                {
                    // Interpolar o valor ausente
                    yValue = cubicSpline.Interpolate(audiographModel.Frequency[i]);
                }

                // Aplicar peso e ajustar com a média do audiograma
                yValue = yValue * waveWeight[i] * 0.8 + 0.2 * audiogramMean;

                audXPoints.Add(audiographModel.Frequency[i]);
                audYPoints.Add(yValue);
            }

            return (audXPoints, audYPoints);
        }

        public List<List<double>> CalculateIntensityLevel(List<double> audYPoints)
        {
            var waveRuleYPoints = new List<List<double>>();
            for (int i = 0; i < 3; i++)
            {
                waveRuleYPoints.Add(new List<double>());
            }

            for (int i = 0; i < audYPoints.Count; i++)
            {
                double intensity0 = 0;
                double intensity1 = 0;
                double intensity2 = 0;

                // Entrada de baixa intensidade
                if (audYPoints[i] < 16)
                {
                    intensity0 = 0;
                }
                else if (audYPoints[i] >= 16)
                {
                    intensity0 = 0.65 * audYPoints[i] - 10;
                }

                // Entrada de intensidade moderada
                if (audYPoints[i] < 20)
                {
                    intensity1 = 0;
                }
                else if (audYPoints[i] >= 20 && audYPoints[i] < 60)
                {
                    intensity1 = 0.5 * (audYPoints[i] - 20);
                }
                else if (audYPoints[i] >= 60)
                {
                    intensity1 = 0.6 * audYPoints[i] - 16;
                }

                // Entrada de alta intensidade
                if (audYPoints[i] < 40)
                {
                    intensity2 = 0;
                }
                else if (audYPoints[i] >= 40)
                {
                    intensity2 = 0.1 * Math.Pow(audYPoints[i] - 40, 1.4);
                }

                waveRuleYPoints[0].Add(intensity0);
                waveRuleYPoints[1].Add(intensity1);
                waveRuleYPoints[2].Add(intensity2);
            }
            return waveRuleYPoints;
        }

        public static double[,] VentSizeAdjustArray(double ventSize)
        {
            var ventSizeAdjust = new double[3, 11];

            // Ajuste 125hz
            for (int i = 0; i < 3; i++)
            {
                ventSizeAdjust[i, 0] = 0;
            }

            // Ajuste 250hz
            for (int i = 0; i < 3; i++)
            {
                ventSizeAdjust[i, 1] = -(3 - i) * ventSize;
            }

            // Ajuste 500hz
            for (int i = 0; i < 3; i++)
            {
                if (ventSize < 1.2)
                {
                    ventSizeAdjust[i, 2] = 0;
                }
                else
                {
                    ventSizeAdjust[i, 2] = (0.2 * i - 3.2) * (ventSize - 1.2);
                }
            }

            // Ajuste 750hz
            for (int i = 0; i < 3; i++)
            {
                if (ventSize < 0.8)
                {
                    ventSizeAdjust[i, 3] = 1.25 * ventSize;
                }
                else
                {
                    if (ventSize < 1.8)
                    {
                        ventSizeAdjust[i, 3] = 1 + 2 * (ventSize - 0.8);
                    }
                    else
                    {
                        ventSizeAdjust[i, 3] = 3 - (-0.3 * i + 3) * (ventSize - 1.8);
                    }
                }
            }

            // Ajuste 1Khz
            for (int i = 0; i < 3; i++)
            {
                if (ventSize < 0.8)
                {
                    ventSizeAdjust[i, 4] = 0;
                }
                else
                {
                    if (ventSize < 3)
                    {
                        if (i == 0)
                        {
                            ventSizeAdjust[i, 4] = 0.9 * (ventSize - 0.8);
                        }
                        if (i == 1)
                        {
                            ventSizeAdjust[i, 4] = 1.2 * (ventSize - 0.8);
                        }
                        if (i == 2)
                        {
                            ventSizeAdjust[i, 4] = 1.6 * (ventSize - 0.8);
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            ventSizeAdjust[i, 4] = 1.98 - 2 * (ventSize - 3);
                        }
                        if (i == 1)
                        {
                            ventSizeAdjust[i, 4] = 2.64 - 2 * (ventSize - 3);
                        }
                        if (i == 2)
                        {
                            ventSizeAdjust[i, 4] = 3.52 - 2 * (ventSize - 3);
                        }
                    }
                }
            }

            // Ajuste 1.5Khz
            for (int i = 0; i < 3; i++)
            {
                if (ventSize < 0.8)
                {
                    if (i == 0)
                    {
                        ventSizeAdjust[i, 5] = ventSize;
                    }
                    if (i == 1)
                    {
                        ventSizeAdjust[i, 5] = ventSize;
                    }
                }
                else
                {
                    if (ventSize < 1.6)
                    {
                        if (i == 0)
                        {
                            ventSizeAdjust[i, 5] = 1.6 - ventSize;
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            ventSizeAdjust[i, 5] = 0.8 * (ventSize - 1.6);
                        }
                    }
                    if (i == 1)
                    {
                        ventSizeAdjust[i, 5] = 0.8 + 0.5 * ventSize;
                    }
                }
                if (i == 2)
                {
                    ventSizeAdjust[i, 5] = 0.8 * ventSize;
                }
            }

            // Ajuste 2Khz
            for (int i = 0; i < 3; i++)
            {
                if (ventSize < 0.8)
                {
                    ventSizeAdjust[i, 6] = 0;
                }
                else
                {
                    if (ventSize < 1.8)
                    {
                        if ((i == 0) || (i == 1))
                        {
                            ventSizeAdjust[i, 6] = 0.7 * (ventSize - 0.8);
                        }
                        if (i == 2)
                        {
                            ventSizeAdjust[i, 6] = 0.4 * (ventSize - 0.8);
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            ventSizeAdjust[i, 6] = 0.7 + 0.6 * (ventSize - 1.8);
                        }
                        if (i == 1)
                        {
                            ventSizeAdjust[i, 6] = 0.7 + 0.7 * (ventSize - 1.8);
                        }
                        if (i == 2)
                        {
                            ventSizeAdjust[i, 6] = 0.4 + 0.9 * (ventSize - 1.8);
                        }
                    }
                }
            }

            // Ajuste 3Khz
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    ventSizeAdjust[i, 7] = 0;
                }
                else
                {
                    if (ventSize < 2.5)
                    {
                        ventSizeAdjust[i, 7] = 0;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            ventSizeAdjust[i, 7] = 0.2 * (ventSize - 2.5);
                        }
                        if (i == 2)
                        {
                            ventSizeAdjust[i, 7] = 0.6 * (ventSize - 2.5);
                        }
                    }
                }
            }

            // Ajuste 4Khz
            for (int i = 0; i < 3; i++)
            {
                ventSizeAdjust[i, 8] = 0;
            }

            // Ajuste 6Khz
            for (int i = 0; i < 3; i++)
            {
                ventSizeAdjust[i, 9] = 0;
            }

            // Ajuste 8Khz
            for (int i = 0; i < 3; i++)
            {
                ventSizeAdjust[i, 10] = 0;
            }

            return ventSizeAdjust;
        }

        public double CalculateUCL(short side)
        {
            double audiogramMean = 0;

            if (side == 1)
            {
                if (!double.IsNaN(audiographModel.Intensity[4, 2]) &&
                   !double.IsNaN(audiographModel.Intensity[4, 4]) &&
                   !double.IsNaN(audiographModel.Intensity[4, 6]) &&
                   !double.IsNaN(audiographModel.Intensity[4, 8]))
                {
                    audiogramMean = audiographModel.Intensity[4, 2] + audiographModel.Intensity[4, 4] + audiographModel.Intensity[4, 6] + audiographModel.Intensity[4, 8];
                }
            }
            else
            {
                if (!double.IsNaN(audiographModel.Intensity[5, 2]) &&
                   !double.IsNaN(audiographModel.Intensity[5, 4]) &&
                   !double.IsNaN(audiographModel.Intensity[5, 6]) &&
                   !double.IsNaN(audiographModel.Intensity[5, 8]))
                {
                    audiogramMean = audiographModel.Intensity[5, 2] + audiographModel.Intensity[5, 4] + audiographModel.Intensity[5, 6] + audiographModel.Intensity[5, 8];
                }
            }

            if (audiogramMean == 0)
            {
                return 110;
            }
            else
            {
                return audiogramMean / 4;
            }
        }
    }
}