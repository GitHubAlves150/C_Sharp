using System;
using System.Collections.Generic;

namespace WaveFit2.Calibration.Class
{
    internal class HearingAidCurve
    {
        private double[] keyFrequencies;
        private double[] recSensInterp;
        private double[] recSatInterp;
        private double[] micInterpGains;

        public double[] CalculateInputOutput(double frequency, double[] inputs)
        {
            List<double> micOutputList = new List<double>();

            int frequencyIndex = Array.IndexOf(keyFrequencies, frequency);

            for (int i = 0; i < inputs.Length; i++)
            {
                micOutputList.Add(53 + micInterpGains[frequencyIndex] + inputs[i]);
            }

            double[] micOutput = micOutputList.ToArray();

            // Calcula a saída da DLL com o microfone e receptor planos, passando a saída do microfone como entrada
            double[] dllOutput = { 1, 2 };//CalculateOutputCurve(keyFrequencies[frequencyIndex], micOutput);

            double[] recOutput = CalculateRecOutput(frequencyIndex, dllOutput);

            return recOutput;
        }

        // Calcula o output para um determinado nivel de input e frequencia
        public double[] CalculateOutput(double inputLevel, double[] plotFrequencies)
        {
            // Saída do microfone

            List<double> micOutputList = new List<double>();

            for (int i = 0; i < keyFrequencies.Length; i++)
            {
                micOutputList.Add(53 + micInterpGains[i] + inputLevel);
            }

            double[] micOutput = micOutputList.ToArray();

            // Calcula a saída da DLL com o microfone e receptor planos, passando a saída do microfone como entrada
            double[] dllOutput = { 1, 2 };  //CalculateOutputCurve(plotFrequencies, micOutput);

            double[] recOutput = CalculateRecOutput(plotFrequencies, dllOutput);

            return recOutput;
        }

        // Calcula o ganho para um determinado nivel de input e frequencia
        public double[] CalculateGain(double inputLevel, double[] plotFrequencies)
        {
            // Calcula o ganho a partir da saída menos a entrada
            double[] gain = CalculateOutput(inputLevel, plotFrequencies);

            for (int i = 0; i < gain.Length; i++)
            {
                gain[i] = gain[i] - inputLevel;
            }

            return gain;
        }

        private double[] CalculateRecOutput(double[] plotFrequencies, double[] dllOutput)
        {
            List<double> saturatedOutput = new List<double>();

            for (int i = 0; i < keyFrequencies.Length; i++)
            {
                if (dllOutput[i] + recSensInterp[i] - 127 < recSatInterp[i])
                {
                    saturatedOutput.Add(dllOutput[i] + recSensInterp[i] - 127);
                }
                else
                {
                    saturatedOutput.Add(recSatInterp[i]);
                }
            }

            return saturatedOutput.ToArray();
        }

        private double[] CalculateRecOutput(int frequencyIndex, double[] dllOutput)
        {
            List<double> saturatedOutput = new List<double>();

            for (int i = 0; i < dllOutput.Length; i++)
            {
                if (dllOutput[i] + recSensInterp[frequencyIndex] - 127 < recSatInterp[frequencyIndex])
                {
                    saturatedOutput.Add(dllOutput[i] + recSensInterp[frequencyIndex] - 127);
                }
                else
                {
                    saturatedOutput.Add(recSatInterp[frequencyIndex]);
                }
            }

            return saturatedOutput.ToArray();
        }
    }
}