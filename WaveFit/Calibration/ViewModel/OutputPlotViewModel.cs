using System;

namespace WaveFit2.Calibration.ViewModel
{
    public class OutputPlotViewModel
    {
        private string type;
        private bool hasData;
        private string plotName;
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

        public void New()
        {
            hasData = false;
        }

        public void setPlotDateToNow()
        {
            dateTaken = DateTime.Now;
        }

        //======== channel response points methods
        // these are channel based graphing

        public float[] getChannelResponse()
        {
            return channelResponseData;
        }

        public void setChannelResponse(float[] value)
        {
            dateTaken = DateTime.Now;
            channelResponseData = value;
        }

        public float getChannelResponsePoint(int index)
        {
            return channelResponseData[index];
        }

        public void setChannelResponsePoint(int index, float value)
        {
            channelResponseData[index] = value;
            hasData = true;
        }

        //======== END channel response points methods

        public float[] getResponse()
        {
            return responseData;
        }

        public void setResponse(float[] value)
        {
            dateTaken = DateTime.Now;
            responseData = value;
        }

        /// <summary>
        ///  Uses legacy target frequencies
        /// </summary>
        public void setResponseLegacyTargetArray(float[] value, int InputLevel)
        {
            dateTaken = DateTime.Now;
            responseData = new float[65];

            //set values to null
            for (int i = 0; i < 23; i++)
                responseData[i] = -999;

            responseData[0] = value[0] + InputLevel;
            responseData[4] = value[1] + InputLevel;
            responseData[16] = value[2] + InputLevel;
            responseData[23] = value[3] + InputLevel;
            responseData[28] = value[4] + InputLevel;
            responseData[35] = value[5] + InputLevel;
            responseData[40] = value[6] + InputLevel;
            responseData[47] = value[7] + InputLevel;
            responseData[52] = value[8] + InputLevel;
            responseData[59] = value[9] + InputLevel;
            responseData[64] = value[10] + InputLevel;
        }

        /// <summary>
        ///  Uses 8 channel frequencies.
        /// </summary>
        public void setMpoTargetArray_8Channel(short[] value)
        {
            dateTaken = DateTime.Now;
            responseData = new float[65];

            //set values to null
            for (int i = 0; i < 23; i++)
                responseData[i] = -999;

            responseData[0] = value[0]; //200 Hz
            responseData[16] = value[1]; //500 Hz
            responseData[28] = value[2]; //1000 Hz
            responseData[35] = value[3]; //1500 Hz
            responseData[40] = value[4]; //2000 Hz
            responseData[47] = value[5]; //3000 Hz
            responseData[52] = value[6]; //4000 Hz
            responseData[64] = value[7]; //8000 Hz
        }

        /// <summary>
        /// Uses universal target frequencies
        /// </summary>
        /// <param name="value"></param>
        public void setResponseUTargetArray(float[] value)
        {
            dateTaken = DateTime.Now;
            responseData = new float[65];

            //set values to null
            for (int i = 0; i < 23; i++)
                responseData[i] = -999;

            //Map same freqs across arrays. Interpolate the in-between freqs
            responseData[0] = value[2];
            responseData[1] = (value[2] * 2 + value[3]) / 3;
            responseData[2] = (value[2] + value[3]) / 2;
            responseData[3] = (value[2] + value[3] * 2) / 3;

            responseData[4] = value[3];
            responseData[5] = (value[3] * 2 + value[4]) / 3;
            responseData[6] = (value[3] + value[4]) / 2;
            responseData[7] = (value[3] + value[4] * 2) / 3;

            responseData[8] = value[4];
            responseData[9] = (value[4] * 2 + value[5]) / 3;
            responseData[10] = (value[4] + value[5]) / 2;
            responseData[11] = (value[4] + value[5] * 2) / 3;

            responseData[12] = value[5];
            responseData[13] = (value[5] * 2 + value[6]) / 3;
            responseData[14] = (value[5] + value[6]) / 2;
            responseData[15] = (value[5] + value[6] * 2) / 3;

            responseData[16] = value[6];
            responseData[17] = (value[6] * 2 + value[7]) / 3;
            responseData[18] = (value[6] + value[7]) / 2;
            responseData[19] = (value[6] + value[7] * 2) / 3;

            responseData[20] = value[7];
            responseData[21] = (value[7] * 2 + value[8]) / 3;
            responseData[22] = (value[7] + value[8]) / 2;

            responseData[23] = value[8];
            responseData[24] = value[9];
            responseData[25] = (value[9] * 2 + value[10]) / 3;
            responseData[26] = (value[9] + value[10]) / 2;
            responseData[27] = (value[9] + value[10] * 2) / 3;

            responseData[28] = value[10];
            responseData[29] = (value[10] * 2 + value[11]) / 3;
            responseData[30] = (value[10] + value[11]) / 2;
            responseData[31] = (value[10] + value[11] * 2) / 3;

            responseData[32] = value[11];
            responseData[33] = (value[11] * 2 + value[12]) / 3;
            responseData[34] = (value[11] + value[12]) / 2;

            responseData[35] = value[12];

            responseData[36] = value[13];
            responseData[37] = (value[13] * 2 + value[14]) / 3;
            responseData[38] = (value[13] + value[14]) / 2;
            responseData[39] = (value[13] + value[14] * 2) / 3;

            responseData[40] = value[14];
            responseData[41] = (value[14] * 2 + value[15]) / 3;
            responseData[42] = (value[14] + value[15]) / 2;
            responseData[43] = (value[14] + value[15] * 2) / 3;

            responseData[44] = value[15];
            responseData[45] = (value[15] * 2 + value[16]) / 3;
            responseData[46] = (value[15] + value[16]) / 2;

            responseData[47] = value[16];

            responseData[48] = value[17];
            responseData[49] = (value[17] * 2 + value[18]) / 3;
            responseData[50] = (value[17] + value[18]) / 2;
            responseData[51] = (value[17] + value[18] * 2) / 3;

            responseData[52] = value[18];
            responseData[53] = (value[18] * 2 + value[19]) / 3;
            responseData[54] = (value[18] + value[19]) / 2;
            responseData[55] = (value[18] + value[19] * 2) / 3;

            responseData[56] = value[19];
            responseData[57] = (value[19] * 2 + value[20]) / 3;
            responseData[58] = (value[19] + value[20]) / 2;
            responseData[59] = (value[19] + value[20] * 2) / 3;

            responseData[60] = value[21];
            responseData[61] = (value[21] * 2 + value[22]) / 3;
            responseData[62] = (value[21] + value[22]) / 2;
            responseData[63] = (value[21] + value[22] * 2) / 3;

            responseData[64] = value[22];
        }

        public int getResponseLength()
        {
            return 64;
        }

        public bool getContainsData()
        {
            return hasData;
        }

        public int getFrequencyPoint(int index)
        {
            if (index < 0 || index > 64)
            {
                return 0;
            }
            return frequency[index];
        }

        public void setFrequencyPoint(int index, int value)
        {
            if (index < 0 || index > 64)
            {
                return;
            }
            frequency[index] = value;
        }

        public float getResponsePoint(int index)
        {
            return responseData[index];
        }

        public void setResponsePoint(int index, float value)
        {
            responseData[index] = value;
            hasData = true;
        }

        public string getPlotName()
        {
            return plotName;
        }

        public void setPlotName(string value)
        {
            plotName = value;
        }

        public string getPlotType()
        {
            return type;
        }

        public void setPlotType(string value)
        {
            type = value;
        }

        public DateTime getPlotDate()
        {
            return dateTaken;
        }

        public void setPlotDate(DateTime value)
        {
            dateTaken = value;
        }

        public string plotDateString()
        {
            return String.Format("s", dateTaken);
        }
    }
}