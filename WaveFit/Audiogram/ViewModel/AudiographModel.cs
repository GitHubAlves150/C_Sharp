using System.Collections.Generic;

namespace WaveFit2.Audiogram.ViewModel
{
    public class AudiographModel
    {
        public int[,] Marker;
        public string[] Type;
        public char[] Ear;
        public double[,] Intensity;
        public List<double> Frequency;

        public AudiographModel()
        {
            Marker = new int[6, 11];
            Type = new string[6];
            Ear = new char[6];
            Intensity = new double[6, 11];
            Frequency = new List<double> { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 };
        }
    }
}