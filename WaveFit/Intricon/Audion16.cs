using WaveFit2.Calibration.Class;

namespace WaveFit2.Intricon
{
    internal class Audion16
    {
        public class Audion16TargetFileStructure
        {
            public short CalculateCR;
            public short UseMpoCh;
            public short MatrixGainCeiling;
            public short ForceReserveGain;
            public short ReserveGain;
            public short SpeechTK;
            public short UseTK;
            public short DisableHighFreqRollOff;
            public CRa CR;
            public TKa TK;
            public TGMPOch TargetMPOch;
            public TG50 TargetGain50;
            public TG80 TargetGain80;
            public TGMPO TargetMPO;
        }

        public class CRa
        {
            public float Channel_1;
            public float Channel_2;
            public float Channel_3;
            public float Channel_4;
            public float Channel_5;
            public float Channel_6;
            public float Channel_7;
            public float Channel_8;
            public float Channel_9;
            public float Channel_10;
            public float Channel_11;
            public float Channel_12;
            public float Channel_13;
            public float Channel_14;
            public float Channel_15;
            public float Channel_16;
        }

        public class TKa
        {
            public float Channel_1;
            public float Channel_2;
            public float Channel_3;
            public float Channel_4;
            public float Channel_5;
            public float Channel_6;
            public float Channel_7;
            public float Channel_8;
            public float Channel_9;
            public float Channel_10;
            public float Channel_11;
            public float Channel_12;
            public float Channel_13;
            public float Channel_14;
            public float Channel_15;
            public float Channel_16;
        }

        public class TGMPOch
        {
            public float Channel_1;
            public float Channel_2;
            public float Channel_3;
            public float Channel_4;
            public float Channel_5;
            public float Channel_6;
            public float Channel_7;
            public float Channel_8;
            public float Channel_9;
            public float Channel_10;
            public float Channel_11;
            public float Channel_12;
            public float Channel_13;
            public float Channel_14;
            public float Channel_15;
            public float Channel_16;
        }

        public class TG50
        {
            public float Freq_125;
            public float Freq_160;
            public float Freq_200;
            public float Freq_250;
            public float Freq_315;
            public float Freq_400;
            public float Freq_500;
            public float Freq_630;
            public float Freq_750;
            public float Freq_800;
            public float Freq_1000;
            public float Freq_1250;
            public float Freq_1500;
            public float Freq_1600;
            public float Freq_2000;
            public float Freq_2500;
            public float Freq_3000;
            public float Freq_3150;
            public float Freq_4000;
            public float Freq_5000;
            public float Freq_6000;
            public float Freq_6300;
            public float Freq_8000;
        }

        public class TG80
        {
            public float Freq_125;
            public float Freq_160;
            public float Freq_200;
            public float Freq_250;
            public float Freq_315;
            public float Freq_400;
            public float Freq_500;
            public float Freq_630;
            public float Freq_750;
            public float Freq_800;
            public float Freq_1000;
            public float Freq_1250;
            public float Freq_1500;
            public float Freq_1600;
            public float Freq_2000;
            public float Freq_2500;
            public float Freq_3000;
            public float Freq_3150;
            public float Freq_4000;
            public float Freq_5000;
            public float Freq_6000;
            public float Freq_6300;
            public float Freq_8000;
        }

        public class TGMPO
        {
            public float Freq_125;
            public float Freq_160;
            public float Freq_200;
            public float Freq_250;
            public float Freq_315;
            public float Freq_400;
            public float Freq_500;
            public float Freq_630;
            public float Freq_750;
            public float Freq_800;
            public float Freq_1000;
            public float Freq_1250;
            public float Freq_1500;
            public float Freq_1600;
            public float Freq_2000;
            public float Freq_2500;
            public float Freq_3000;
            public float Freq_3150;
            public float Freq_4000;
            public float Freq_5000;
            public float Freq_6000;
            public float Freq_6300;
            public float Freq_8000;
        }

        public static void WriteTarget(GenericAudion16.TargetAudion16 targetData, WaveRule waveRule)
        {
            Audion16TargetFileStructure Data = new Audion16TargetFileStructure();

            targetData.CalculateCR = 1;
            targetData.UseMpoCh = 0;
            targetData.MatrixGainCeiling = -20;
            targetData.ForceReserveGain = 0;
            targetData.ReserveGain = 6;
            targetData.DisableHighFreqRollOff = 0;
            targetData.SpeechTK = 65;
            targetData.UseTK = 0;

            targetData.CR[0] = 0;
            targetData.CR[1] = 0;
            targetData.CR[2] = 0;
            targetData.CR[3] = 0;
            targetData.CR[4] = 0;
            targetData.CR[5] = 0;
            targetData.CR[6] = 0;
            targetData.CR[7] = 0;
            targetData.CR[8] = 0;
            targetData.CR[9] = 0;
            targetData.CR[10] = 0;
            targetData.CR[11] = 0;
            targetData.CR[12] = 0;
            targetData.CR[13] = 0;
            targetData.CR[14] = 0;
            targetData.CR[15] = 0;

            targetData.TK[0] = 60;
            targetData.TK[1] = 60;
            targetData.TK[2] = 60;
            targetData.TK[3] = 65;
            targetData.TK[4] = 65;
            targetData.TK[5] = 65;
            targetData.TK[6] = 65;
            targetData.TK[7] = 65;
            targetData.TK[8] = 65;
            targetData.TK[9] = 65;
            targetData.TK[10] = 65;
            targetData.TK[11] = 65;
            targetData.TK[12] = 65;
            targetData.TK[13] = 65;
            targetData.TK[14] = 65;
            targetData.TK[15] = 65;

            targetData.TargetMPOch[0] = 0;
            targetData.TargetMPOch[1] = 0;
            targetData.TargetMPOch[2] = 0;
            targetData.TargetMPOch[3] = 0;
            targetData.TargetMPOch[4] = 0;
            targetData.TargetMPOch[5] = 0;
            targetData.TargetMPOch[6] = 0;
            targetData.TargetMPOch[7] = 0;
            targetData.TargetMPOch[8] = 0;
            targetData.TargetMPOch[9] = 0;
            targetData.TargetMPOch[10] = 0;
            targetData.TargetMPOch[11] = 0;
            targetData.TargetMPOch[12] = 0;
            targetData.TargetMPOch[13] = 0;
            targetData.TargetMPOch[14] = 0;
            targetData.TargetMPOch[15] = 0;

            targetData.TargetGain50[0] = (float)waveRule.targetGains[0][0];
            targetData.TargetGain50[1] = -999;
            targetData.TargetGain50[2] = -999;
            targetData.TargetGain50[3] = (float)waveRule.targetGains[0][1];
            targetData.TargetGain50[4] = -999;
            targetData.TargetGain50[5] = -999;
            targetData.TargetGain50[6] = (float)waveRule.targetGains[0][2];
            targetData.TargetGain50[7] = -999;
            targetData.TargetGain50[8] = (float)waveRule.targetGains[0][3];
            targetData.TargetGain50[9] = -999;
            targetData.TargetGain50[10] = (float)waveRule.targetGains[0][4];
            targetData.TargetGain50[11] = -999;
            targetData.TargetGain50[12] = (float)waveRule.targetGains[0][5];
            targetData.TargetGain50[13] = -999;
            targetData.TargetGain50[14] = (float)waveRule.targetGains[0][6];
            targetData.TargetGain50[15] = -999;
            targetData.TargetGain50[16] = (float)waveRule.targetGains[0][7];
            targetData.TargetGain50[17] = -999;
            targetData.TargetGain50[18] = (float)waveRule.targetGains[0][8];
            targetData.TargetGain50[19] = -999;
            targetData.TargetGain50[20] = (float)waveRule.targetGains[0][9];
            targetData.TargetGain50[21] = -999;
            targetData.TargetGain50[22] = (float)waveRule.targetGains[0][10];

            targetData.TargetGain80[0] = (float)waveRule.targetGains[1][0];
            targetData.TargetGain80[1] = -999;
            targetData.TargetGain80[2] = -999;
            targetData.TargetGain80[3] = (float)waveRule.targetGains[1][1];
            targetData.TargetGain80[4] = -999;
            targetData.TargetGain80[5] = -999;
            targetData.TargetGain80[6] = (float)waveRule.targetGains[1][2];
            targetData.TargetGain80[7] = -999;
            targetData.TargetGain80[8] = (float)waveRule.targetGains[1][3];
            targetData.TargetGain80[9] = -999;
            targetData.TargetGain80[10] = (float)waveRule.targetGains[1][4];
            targetData.TargetGain80[11] = -999;
            targetData.TargetGain80[12] = (float)waveRule.targetGains[1][5];
            targetData.TargetGain80[13] = -999;
            targetData.TargetGain80[14] = (float)waveRule.targetGains[1][6];
            targetData.TargetGain80[15] = -999;
            targetData.TargetGain80[16] = (float)waveRule.targetGains[1][7];
            targetData.TargetGain80[17] = -999;
            targetData.TargetGain80[18] = (float)waveRule.targetGains[1][8];
            targetData.TargetGain80[19] = -999;
            targetData.TargetGain80[20] = (float)waveRule.targetGains[1][9];
            targetData.TargetGain80[21] = -999;
            targetData.TargetGain80[22] = (float)waveRule.targetGains[1][10];

            targetData.TargetMPO[0] = 95;
            targetData.TargetMPO[1] = 95;
            targetData.TargetMPO[2] = 95;
            targetData.TargetMPO[3] = 95;
            targetData.TargetMPO[4] = 95;
            targetData.TargetMPO[5] = 95;
            targetData.TargetMPO[6] = 95;
            targetData.TargetMPO[7] = 95;
            targetData.TargetMPO[8] = 95;
            targetData.TargetMPO[9] = 95;
            targetData.TargetMPO[10] = 95;
            targetData.TargetMPO[11] = 95;
            targetData.TargetMPO[12] = 95;
            targetData.TargetMPO[13] = 95;
            targetData.TargetMPO[14] = 95;
            targetData.TargetMPO[15] = 95;
            targetData.TargetMPO[16] = 95;
            targetData.TargetMPO[17] = 95;
            targetData.TargetMPO[18] = 95;
            targetData.TargetMPO[19] = 95;
            targetData.TargetMPO[20] = 95;
            targetData.TargetMPO[21] = 95;
            targetData.TargetMPO[22] = 95;
        }
    }
}