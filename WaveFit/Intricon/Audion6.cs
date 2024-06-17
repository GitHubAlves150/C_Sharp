using System;
using System.Runtime.InteropServices;

namespace IAmp
{
    /// <summary>
    /// This is a .NET interface to the legacy Audion6.dll.
    /// Includes some extra features such as Autofit target
    /// file read/write, PGM file read/write, and VC calculations.
    /// </summary>
    public class Audion6
    {
        private const short numberOfManfReserveWords = 94;

        [StructLayout(LayoutKind.Sequential)]
        public struct MDA
        {
            public short AlgVer_Major;
            public short AlgVer_Minor;
            public short LayoutVersion;
            public short ManufacturerID;
            public short Platform_ID;
            public Int32 ModelID;
            public Int32 PassCode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 94)]
            public Int32[] MANF_reserve;

            public MDA(bool init)
            {
                AlgVer_Major = 0;
                AlgVer_Minor = 0;
                LayoutVersion = 0;
                ManufacturerID = 0;
                ModelID = 0;
                PassCode = 0;
                Platform_ID = 0;
                MANF_reserve = new Int32[94];

                for (int i = 0; i < 94; i++)
                {
                    MANF_reserve[i] = 0;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DetectData
        {
            public short Platform_ID;
            public short AlgVer_Major;
            public short AlgVer_Minor;
            public short LayoutVersion;
            public short MANF_ID;
            public Int32 MANF_reserve_1;
            public Int32 MANF_reserve_2;
            public Int32 MANF_reserve_3;
            public Int32 MANF_reserve_4;
            public Int32 MANF_reserve_5;
            public Int32 MANF_reserve_6;
            public Int32 MANF_reserve_7;
            public Int32 MANF_reserve_8;
            public Int32 MANF_reserve_9;
            public Int32 MANF_reserve_10;
            public short ModelID;
            public short reserved1;
            public short reserved2;
            public short reserved3;
            public short reserved4;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ParamsClassic
        {
            public short ActiveProgram;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ1_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ2_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ3_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ4_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ5_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ6_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ7_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ8_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ9_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ10_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ11_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ12_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Exp_Attack;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Exp_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Exp_Release;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] FBC_Enable;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] input_mux;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] matrix_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] MPO_Attack;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] MPO_Release;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Noise_Reduction;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] preamp_gain0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] preamp_gain1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants2;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants3;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants4;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants5;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants6;

            public short VcPosition;

            public ParamsClassic(bool init)
            {
                ActiveProgram = 0;
                BEQ1_gain = new short[5];
                BEQ2_gain = new short[5];
                BEQ3_gain = new short[5];
                BEQ4_gain = new short[5];
                BEQ5_gain = new short[5];
                BEQ6_gain = new short[5];
                BEQ7_gain = new short[5];
                BEQ8_gain = new short[5];
                BEQ9_gain = new short[5];
                BEQ10_gain = new short[5];
                BEQ11_gain = new short[5];
                BEQ12_gain = new short[5];
                C1_ExpTK = new short[5];
                C2_ExpTK = new short[5];
                C3_ExpTK = new short[5];
                C4_ExpTK = new short[5];
                C5_ExpTK = new short[5];
                C6_ExpTK = new short[5];
                C1_MPO = new short[5];
                C2_MPO = new short[5];
                C3_MPO = new short[5];
                C4_MPO = new short[5];
                C5_MPO = new short[5];
                C6_MPO = new short[5];
                C1_Ratio = new short[5];
                C2_Ratio = new short[5];
                C3_Ratio = new short[5];
                C4_Ratio = new short[5];
                C5_Ratio = new short[5];
                C6_Ratio = new short[5];
                C1_TK = new short[5];
                C2_TK = new short[5];
                C3_TK = new short[5];
                C4_TK = new short[5];
                C5_TK = new short[5];
                C6_TK = new short[5];
                Exp_Attack = new short[5];
                Exp_Ratio = new short[5];
                Exp_Release = new short[5];
                FBC_Enable = new short[5];
                input_mux = new short[5];
                matrix_gain = new short[5];
                MPO_Attack = new short[5];
                MPO_Release = new short[5];
                Noise_Reduction = new short[5];
                preamp_gain0 = new short[5];
                preamp_gain1 = new short[5];
                TimeConstants1 = new short[5];
                TimeConstants2 = new short[5];
                TimeConstants3 = new short[5];
                TimeConstants4 = new short[5];
                TimeConstants5 = new short[5];
                TimeConstants6 = new short[5];
                VcPosition = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ParamsHighLowGain
        {
            public short ActiveProgram;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ1_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ2_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ3_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ4_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ5_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ6_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ7_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ8_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ9_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ10_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ11_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] BEQ12_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_ExpTK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_HighGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_HighGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_HighGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_HighGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_HighGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_HighGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_HighGainMin;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_HighGainMin;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_HighGainMin;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_HighGainMin;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_HighGainMin;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_HighGainMin;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public Single[] C1_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public Single[] C2_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public Single[] C3_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public Single[] C4_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public Single[] C5_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public Single[] C6_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_LowGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_LowGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_LowGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_LowGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_LowGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_LowGain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C1_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C5_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C6_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Exp_Attack;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Exp_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Exp_Release;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] FBC_Enable;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] input_mux;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] matrix_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] MPO_Attack;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] MPO_Release;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Noise_Reduction;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] preamp_gain0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] preamp_gain1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants2;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants3;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants4;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants5;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] TimeConstants6;

            public short VcPosition;

            public ParamsHighLowGain(bool init)
            {
                ActiveProgram = 0;
                BEQ1_gain = new short[5];
                BEQ2_gain = new short[5];
                BEQ3_gain = new short[5];
                BEQ4_gain = new short[5];
                BEQ5_gain = new short[5];
                BEQ6_gain = new short[5];
                BEQ7_gain = new short[5];
                BEQ8_gain = new short[5];
                BEQ9_gain = new short[5];
                BEQ10_gain = new short[5];
                BEQ11_gain = new short[5];
                BEQ12_gain = new short[5];
                C1_ExpTK = new short[5];
                C2_ExpTK = new short[5];
                C3_ExpTK = new short[5];
                C4_ExpTK = new short[5];
                C5_ExpTK = new short[5];
                C6_ExpTK = new short[5];
                C1_HighGain = new short[5];
                C2_HighGain = new short[5];
                C3_HighGain = new short[5];
                C4_HighGain = new short[5];
                C5_HighGain = new short[5];
                C6_HighGain = new short[5];
                C1_HighGainMin = new short[5];
                C2_HighGainMin = new short[5];
                C3_HighGainMin = new short[5];
                C4_HighGainMin = new short[5];
                C5_HighGainMin = new short[5];
                C6_HighGainMin = new short[5];
                C1_Ratio = new Single[5];
                C2_Ratio = new Single[5];
                C3_Ratio = new Single[5];
                C4_Ratio = new Single[5];
                C5_Ratio = new Single[5];
                C6_Ratio = new Single[5];
                C1_LowGain = new short[5];
                C2_LowGain = new short[5];
                C3_LowGain = new short[5];
                C4_LowGain = new short[5];
                C5_LowGain = new short[5];
                C6_LowGain = new short[5];
                C1_MPO = new short[5];
                C2_MPO = new short[5];
                C3_MPO = new short[5];
                C4_MPO = new short[5];
                C5_MPO = new short[5];
                C6_MPO = new short[5];
                C1_TK = new short[5];
                C2_TK = new short[5];
                C3_TK = new short[5];
                C4_TK = new short[5];
                C5_TK = new short[5];
                C6_TK = new short[5];
                Exp_Attack = new short[5];
                Exp_Ratio = new short[5];
                Exp_Release = new short[5];
                FBC_Enable = new short[5];
                input_mux = new short[5];
                matrix_gain = new short[5];
                MPO_Attack = new short[5];
                MPO_Release = new short[5];
                Noise_Reduction = new short[5];
                preamp_gain0 = new short[5];
                preamp_gain1 = new short[5];
                TimeConstants1 = new short[5];
                TimeConstants2 = new short[5];
                TimeConstants3 = new short[5];
                TimeConstants4 = new short[5];
                TimeConstants5 = new short[5];
                TimeConstants6 = new short[5];
                VcPosition = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Config
        {
            public short Auto_Telecoil_Enable;
            public short Cal_Input;
            public short Dir_Spacing;
            public short Low_Battery_Warning;
            public short Mic_Cal;
            public short number_of_programs;
            public short Output_Mode;
            public short Power_On_Level;
            public short Power_On_Delay;
            public short Program_Beep_Enable;
            public short Program_StartUp;
            public short Switch_Mode;
            public short Tone_Frequency;
            public short Tone_Level;
            public short VC_AnalogRange;
            public short VC_Enable;
            public short VC_Mode;
            public short VC_DigitalNumSteps;
            public short VC_StartUp;
            public short VC_DigitalStepSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Response
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65)]
            public Single[] element;

            public Response(bool init)
            {
                element = new Single[65];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TargetParams
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Single[] sng50;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Single[] sng80;

            public short MPO;
            public short ResGain;

            public TargetParams(bool init)
            {
                sng50 = new Single[11];
                sng80 = new Single[11];
                MPO = 95;
                ResGain = 6;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Target2Params
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Single[] sng50;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Single[] sng80;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public Single[] CR;

            public short SpeechTK;  // Speech equivalent settings in the thresholds. Negative values set all TKs to that absolute value. Example: SpeechTK = 55 give 55dB speech equivalent (55,55,50,50,45,45,40,40).

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public short[] MPO;

            public short ResGain;

            public Target2Params(bool init)
            {
                sng50 = new Single[11];
                sng80 = new Single[11];
                CR = new Single[6];
                SpeechTK = 65;
                MPO = new short[6];
                ResGain = 6;
            }
        }

        /// <summary>
        /// Page options used for Read() operation from HI.
        /// </summary>
        public enum ReadPages
        {
            ManufacturingDataOnly = -2,
            AllPages = -1
        }

        /// <summary>
        /// Page options used for Write() operation to HI.
        /// </summary>
        public enum LoadPages
        {
            ChangedProgsConfigs = -2,
            AllPages = -1
        }

        public enum RL_channel
        {
            channelLeft = 0,
            channelRight = 1
        }

        public enum DataPass
        {
            loadProgramUpdates = -2,
            passAll = -1,
            passParamsAndConfig = 0,
            passMDA = 1,
            passParams = 2,
            passConfig = 3
        }

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Audio_on(short active_program);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short AutoFit(short Datversion, short ManID, ref TargetParams target_params);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Close();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Connected();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Detect(ref DetectData detdata);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_active_program(ref short program);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_Autofit_Matrix_gain_ceiling(ref short ceiling);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_config(short version, ref Config Config);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_Auto_Save_Flag(ref short enableFlag);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short GetEEPROMData();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_FR_array(short input_level, ref Response Audion6_Response);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_HighLow_HighReference(ref short level);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_interface_type(ref short type);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_last_interface_error();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_MDA(short Passcode, short version, ref MDA MDAparams);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Get_paramsClassic(short version, ref ParamsClassic Params);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Get_paramsHighLowGain(short version, ref ParamsHighLowGain Params);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short GetNextParamError(String paramName, String errorDescription); //TODO: needs testing

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_RL_channel(ref short channel);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short HaveAnyParamErrors();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short InitializeDriver();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Load(short page);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short LoadConfig();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short LoadMDA();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short LoadParms();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short LoadParamUpdates();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Lock(short page);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short LockConfig();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short LockMDA();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short LockParams();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Mute();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short PlayValidationTone(short Freq, short Level, short Duration);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Read(short page);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short ReadParams();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short ReadConfig();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short ReadMDA();

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_Autofit_Matrix_gain_ceiling(short ceiling);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_active_program(short program);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_Active_VC_Position(short position);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_config(short version, ref Config Config);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_Auto_Save_Flag(short enableFlag);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_extension(String path); //TODO: need testing

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_HighLow_HighReference(short level);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_interface_type(short type);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Set_MDA(short version, ref MDA MDAparams);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_mic_response(ref Response mic_array);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Set_paramsClassic(short version, ref ParamsClassic Params);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Set_paramsHighLowGain(short version, ref ParamsHighLowGain Params);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_PassCode(short CurrentPassCode, short NewPassCode);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_rec_response(ref Response rec_array);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_RL_channel(short channel);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetRec_Saturation(Single Sat_level);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetToTest(short Datversion, short ManID);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_platform_id(short ID);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetProgram(short program);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetValidationMode(short Mode);

        [DllImport("Audion6.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short TestTone(short numBeeps);

        public string DriverErrorString(int ErrorCode)
        {
            string DriverErrorString;

            switch (ErrorCode)
            {
                case 0:
                    DriverErrorString = "(Erro # 0) Êxito: Sem erros detectados. Operação concluída com sucesso.";
                    break;

                case 1:
                    DriverErrorString = "(Erro # 1) Sem Programadora: Verifique a conexão entre programadora e o PC e clique no botão de reconectar.";
                    break;

                case 2:
                    DriverErrorString = "(Erro # 2) Aparelho Auditivo Não Conectado: Verifique a conexão entre o aparelho e programadora e clique no botão de reconectar.";
                    break;

                case 3:
                    DriverErrorString = "(Erro # 3) Argumento Inválido: Um argumento fornecido é inválido ou incorreto.";
                    break;

                case 4:
                    DriverErrorString = "(Erro # 4) Programadora não Inicializada: A programadora não foi inicializada corretamente.";
                    break;

                case 5:
                    DriverErrorString = "(Erro # 5) Não Lido: Os dados não puderam ser lidos.";
                    break;

                case 6:
                    DriverErrorString = "(Erro # 6) Erro de Soma de Verificação: Houve um erro na verificação da integridade dos dados.";
                    break;

                case 7:
                    DriverErrorString = "(Erro # 7) Versão Inválida: A versão do software ou do dispositivo é incompatível.";
                    break;

                case 8:
                    DriverErrorString = "(Erro # 8) Erro do Programador: Houve um erro durante a execução do programa.";
                    break;

                case 9:
                    DriverErrorString = "(Erro # 9) Erro CMF: Ocorreu um erro no fluxo de comunicação.";
                    break;

                case 10:
                    DriverErrorString = "(Erro # 10) Aparelho Errado: O dispositivo conectado não é o correto para esta operação.";
                    break;

                case 11:
                    DriverErrorString = "(Erro # 11) Erro de Inicialização: Houve um erro durante a inicialização do sistema.";
                    break;

                case 12:
                    DriverErrorString = "(Erro # 12) Sem Driver NL: O driver necessário para o dispositivo não está instalado.";
                    break;

                case 13:
                    DriverErrorString = "(Erro # 13) NL em Uso: O dispositivo já está em uso por outro processo.";
                    break;

                default:
                    DriverErrorString = "Erro # " + ErrorCode + " é um código de erro desconhecido.";
                    break;
            }
            return DriverErrorString;
        }

        public static void CopyProgram(short fromProgram, short toProgram, ref ParamsClassic programs)
        {
            //programs.ActiveProgram = programs.ActiveProgram;
            programs.BEQ1_gain[toProgram] = programs.BEQ1_gain[fromProgram];
            programs.BEQ2_gain[toProgram] = programs.BEQ2_gain[fromProgram];
            programs.BEQ3_gain[toProgram] = programs.BEQ3_gain[fromProgram];
            programs.BEQ4_gain[toProgram] = programs.BEQ4_gain[fromProgram];
            programs.BEQ5_gain[toProgram] = programs.BEQ5_gain[fromProgram];
            programs.BEQ6_gain[toProgram] = programs.BEQ6_gain[fromProgram];
            programs.BEQ7_gain[toProgram] = programs.BEQ7_gain[fromProgram];
            programs.BEQ8_gain[toProgram] = programs.BEQ8_gain[fromProgram];
            programs.BEQ9_gain[toProgram] = programs.BEQ9_gain[fromProgram];
            programs.BEQ10_gain[toProgram] = programs.BEQ10_gain[fromProgram];
            programs.BEQ11_gain[toProgram] = programs.BEQ11_gain[fromProgram];
            programs.BEQ12_gain[toProgram] = programs.BEQ12_gain[fromProgram];
            programs.C1_ExpTK[toProgram] = programs.C1_ExpTK[fromProgram];
            programs.C1_MPO[toProgram] = programs.C1_MPO[fromProgram];
            programs.C1_Ratio[toProgram] = programs.C1_Ratio[fromProgram];
            programs.C1_TK[toProgram] = programs.C1_TK[fromProgram];
            programs.C2_ExpTK[toProgram] = programs.C2_ExpTK[fromProgram];
            programs.C2_MPO[toProgram] = programs.C2_MPO[fromProgram];
            programs.C2_Ratio[toProgram] = programs.C2_Ratio[fromProgram];
            programs.C2_TK[toProgram] = programs.C2_TK[fromProgram];
            programs.C3_ExpTK[toProgram] = programs.C3_ExpTK[fromProgram];
            programs.C3_MPO[toProgram] = programs.C3_MPO[fromProgram];
            programs.C3_Ratio[toProgram] = programs.C3_Ratio[fromProgram];
            programs.C3_TK[toProgram] = programs.C3_TK[fromProgram];
            programs.C4_ExpTK[toProgram] = programs.C4_ExpTK[fromProgram];
            programs.C4_MPO[toProgram] = programs.C4_MPO[fromProgram];
            programs.C4_Ratio[toProgram] = programs.C4_Ratio[fromProgram];
            programs.C4_TK[toProgram] = programs.C4_TK[fromProgram];
            programs.C5_ExpTK[toProgram] = programs.C5_ExpTK[fromProgram];
            programs.C5_MPO[toProgram] = programs.C5_MPO[fromProgram];
            programs.C5_Ratio[toProgram] = programs.C5_Ratio[fromProgram];
            programs.C5_TK[toProgram] = programs.C5_TK[fromProgram];
            programs.C6_ExpTK[toProgram] = programs.C6_ExpTK[fromProgram];
            programs.C6_MPO[toProgram] = programs.C6_MPO[fromProgram];
            programs.C6_Ratio[toProgram] = programs.C6_Ratio[fromProgram];
            programs.C6_TK[toProgram] = programs.C6_TK[fromProgram];
            programs.Exp_Attack[toProgram] = programs.Exp_Attack[fromProgram];
            programs.Exp_Ratio[toProgram] = programs.Exp_Ratio[fromProgram];
            programs.Exp_Release[toProgram] = programs.Exp_Release[fromProgram];
            programs.FBC_Enable[toProgram] = programs.FBC_Enable[fromProgram];
            programs.input_mux[toProgram] = programs.input_mux[fromProgram];
            programs.matrix_gain[toProgram] = programs.matrix_gain[fromProgram];
            programs.MPO_Attack[toProgram] = programs.MPO_Attack[fromProgram];
            programs.MPO_Release[toProgram] = programs.MPO_Release[fromProgram];
            programs.Noise_Reduction[toProgram] = programs.Noise_Reduction[fromProgram];
            programs.preamp_gain0[toProgram] = programs.preamp_gain0[fromProgram];
            programs.preamp_gain1[toProgram] = programs.preamp_gain1[fromProgram];
            programs.TimeConstants1[toProgram] = programs.TimeConstants1[fromProgram];
            programs.TimeConstants2[toProgram] = programs.TimeConstants2[fromProgram];
            programs.TimeConstants3[toProgram] = programs.TimeConstants3[fromProgram];
            programs.TimeConstants4[toProgram] = programs.TimeConstants4[fromProgram];
            programs.TimeConstants5[toProgram] = programs.TimeConstants5[fromProgram];
            programs.TimeConstants6[toProgram] = programs.TimeConstants6[fromProgram];
            //programs.VcPosition = programs.VcPosition;
        }

        public static void CopyProgram(short fromProgram, short toProgram, ParamsClassic fromPrograms, ref ParamsClassic toPrograms)
        {
            toPrograms.ActiveProgram = fromPrograms.ActiveProgram;
            toPrograms.BEQ1_gain[toProgram] = fromPrograms.BEQ1_gain[fromProgram];
            toPrograms.BEQ2_gain[toProgram] = fromPrograms.BEQ2_gain[fromProgram];
            toPrograms.BEQ3_gain[toProgram] = fromPrograms.BEQ3_gain[fromProgram];
            toPrograms.BEQ4_gain[toProgram] = fromPrograms.BEQ4_gain[fromProgram];
            toPrograms.BEQ5_gain[toProgram] = fromPrograms.BEQ5_gain[fromProgram];
            toPrograms.BEQ6_gain[toProgram] = fromPrograms.BEQ6_gain[fromProgram];
            toPrograms.BEQ7_gain[toProgram] = fromPrograms.BEQ7_gain[fromProgram];
            toPrograms.BEQ8_gain[toProgram] = fromPrograms.BEQ8_gain[fromProgram];
            toPrograms.BEQ9_gain[toProgram] = fromPrograms.BEQ9_gain[fromProgram];
            toPrograms.BEQ10_gain[toProgram] = fromPrograms.BEQ10_gain[fromProgram];
            toPrograms.BEQ11_gain[toProgram] = fromPrograms.BEQ11_gain[fromProgram];
            toPrograms.BEQ12_gain[toProgram] = fromPrograms.BEQ12_gain[fromProgram];
            toPrograms.C1_ExpTK[toProgram] = fromPrograms.C1_ExpTK[fromProgram];
            toPrograms.C1_MPO[toProgram] = fromPrograms.C1_MPO[fromProgram];
            toPrograms.C1_Ratio[toProgram] = fromPrograms.C1_Ratio[fromProgram];
            toPrograms.C1_TK[toProgram] = fromPrograms.C1_TK[fromProgram];
            toPrograms.C2_ExpTK[toProgram] = fromPrograms.C2_ExpTK[fromProgram];
            toPrograms.C2_MPO[toProgram] = fromPrograms.C2_MPO[fromProgram];
            toPrograms.C2_Ratio[toProgram] = fromPrograms.C2_Ratio[fromProgram];
            toPrograms.C2_TK[toProgram] = fromPrograms.C2_TK[fromProgram];
            toPrograms.C3_ExpTK[toProgram] = fromPrograms.C3_ExpTK[fromProgram];
            toPrograms.C3_MPO[toProgram] = fromPrograms.C3_MPO[fromProgram];
            toPrograms.C3_Ratio[toProgram] = fromPrograms.C3_Ratio[fromProgram];
            toPrograms.C3_TK[toProgram] = fromPrograms.C3_TK[fromProgram];
            toPrograms.C4_ExpTK[toProgram] = fromPrograms.C4_ExpTK[fromProgram];
            toPrograms.C4_MPO[toProgram] = fromPrograms.C4_MPO[fromProgram];
            toPrograms.C4_Ratio[toProgram] = fromPrograms.C4_Ratio[fromProgram];
            toPrograms.C4_TK[toProgram] = fromPrograms.C4_TK[fromProgram];
            toPrograms.C5_ExpTK[toProgram] = fromPrograms.C5_ExpTK[fromProgram];
            toPrograms.C5_MPO[toProgram] = fromPrograms.C5_MPO[fromProgram];
            toPrograms.C5_Ratio[toProgram] = fromPrograms.C5_Ratio[fromProgram];
            toPrograms.C5_TK[toProgram] = fromPrograms.C5_TK[fromProgram];
            toPrograms.C6_ExpTK[toProgram] = fromPrograms.C6_ExpTK[fromProgram];
            toPrograms.C6_MPO[toProgram] = fromPrograms.C6_MPO[fromProgram];
            toPrograms.C6_Ratio[toProgram] = fromPrograms.C6_Ratio[fromProgram];
            toPrograms.C6_TK[toProgram] = fromPrograms.C6_TK[fromProgram];
            toPrograms.Exp_Attack[toProgram] = fromPrograms.Exp_Attack[fromProgram];
            toPrograms.Exp_Ratio[toProgram] = fromPrograms.Exp_Ratio[fromProgram];
            toPrograms.Exp_Release[toProgram] = fromPrograms.Exp_Release[fromProgram];
            toPrograms.FBC_Enable[toProgram] = fromPrograms.FBC_Enable[fromProgram];
            toPrograms.input_mux[toProgram] = fromPrograms.input_mux[fromProgram];
            toPrograms.matrix_gain[toProgram] = fromPrograms.matrix_gain[fromProgram];
            toPrograms.MPO_Attack[toProgram] = fromPrograms.MPO_Attack[fromProgram];
            toPrograms.MPO_Release[toProgram] = fromPrograms.MPO_Release[fromProgram];
            toPrograms.Noise_Reduction[toProgram] = fromPrograms.Noise_Reduction[fromProgram];
            toPrograms.preamp_gain0[toProgram] = fromPrograms.preamp_gain0[fromProgram];
            toPrograms.preamp_gain1[toProgram] = fromPrograms.preamp_gain1[fromProgram];
            toPrograms.TimeConstants1[toProgram] = fromPrograms.TimeConstants1[fromProgram];
            toPrograms.TimeConstants2[toProgram] = fromPrograms.TimeConstants2[fromProgram];
            toPrograms.TimeConstants3[toProgram] = fromPrograms.TimeConstants3[fromProgram];
            toPrograms.TimeConstants4[toProgram] = fromPrograms.TimeConstants4[fromProgram];
            toPrograms.TimeConstants5[toProgram] = fromPrograms.TimeConstants5[fromProgram];
            toPrograms.TimeConstants6[toProgram] = fromPrograms.TimeConstants6[fromProgram];
            toPrograms.VcPosition = fromPrograms.VcPosition;
        }

        public static void CopyProgram(short fromProgram, short toProgram, ref ParamsHighLowGain programs)
        {
            //programs.ActiveProgram = programs.ActiveProgram;
            programs.BEQ1_gain[toProgram] = programs.BEQ1_gain[fromProgram];
            programs.BEQ2_gain[toProgram] = programs.BEQ2_gain[fromProgram];
            programs.BEQ3_gain[toProgram] = programs.BEQ3_gain[fromProgram];
            programs.BEQ4_gain[toProgram] = programs.BEQ4_gain[fromProgram];
            programs.BEQ5_gain[toProgram] = programs.BEQ5_gain[fromProgram];
            programs.BEQ6_gain[toProgram] = programs.BEQ6_gain[fromProgram];
            programs.BEQ7_gain[toProgram] = programs.BEQ7_gain[fromProgram];
            programs.BEQ8_gain[toProgram] = programs.BEQ8_gain[fromProgram];
            programs.BEQ9_gain[toProgram] = programs.BEQ9_gain[fromProgram];
            programs.BEQ10_gain[toProgram] = programs.BEQ10_gain[fromProgram];
            programs.BEQ11_gain[toProgram] = programs.BEQ11_gain[fromProgram];
            programs.BEQ12_gain[toProgram] = programs.BEQ12_gain[fromProgram];
            programs.C1_ExpTK[toProgram] = programs.C1_ExpTK[fromProgram];
            programs.C1_HighGain[toProgram] = programs.C1_HighGain[fromProgram];
            programs.C1_HighGainMin[toProgram] = programs.C1_HighGainMin[fromProgram];
            programs.C1_LowGain[toProgram] = programs.C1_LowGain[fromProgram];
            programs.C1_MPO[toProgram] = programs.C1_MPO[fromProgram];
            programs.C1_Ratio[toProgram] = programs.C1_Ratio[fromProgram];
            programs.C1_TK[toProgram] = programs.C1_TK[fromProgram];
            programs.C2_ExpTK[toProgram] = programs.C2_ExpTK[fromProgram];
            programs.C2_HighGain[toProgram] = programs.C2_HighGain[fromProgram];
            programs.C2_HighGainMin[toProgram] = programs.C2_HighGainMin[fromProgram];
            programs.C2_LowGain[toProgram] = programs.C2_LowGain[fromProgram];
            programs.C2_MPO[toProgram] = programs.C2_MPO[fromProgram];
            programs.C2_Ratio[toProgram] = programs.C2_Ratio[fromProgram];
            programs.C2_TK[toProgram] = programs.C2_TK[fromProgram];
            programs.C3_ExpTK[toProgram] = programs.C3_ExpTK[fromProgram];
            programs.C3_HighGain[toProgram] = programs.C3_HighGain[fromProgram];
            programs.C3_HighGainMin[toProgram] = programs.C3_HighGainMin[fromProgram];
            programs.C3_LowGain[toProgram] = programs.C3_LowGain[fromProgram];
            programs.C3_MPO[toProgram] = programs.C3_MPO[fromProgram];
            programs.C3_Ratio[toProgram] = programs.C3_Ratio[fromProgram];
            programs.C3_TK[toProgram] = programs.C3_TK[fromProgram];
            programs.C4_ExpTK[toProgram] = programs.C4_ExpTK[fromProgram];
            programs.C4_HighGain[toProgram] = programs.C4_HighGain[fromProgram];
            programs.C4_HighGainMin[toProgram] = programs.C4_HighGainMin[fromProgram];
            programs.C4_LowGain[toProgram] = programs.C4_LowGain[fromProgram];
            programs.C4_MPO[toProgram] = programs.C4_MPO[fromProgram];
            programs.C4_Ratio[toProgram] = programs.C4_Ratio[fromProgram];
            programs.C4_TK[toProgram] = programs.C4_TK[fromProgram];
            programs.C5_ExpTK[toProgram] = programs.C5_ExpTK[fromProgram];
            programs.C5_HighGain[toProgram] = programs.C5_HighGain[fromProgram];
            programs.C5_HighGainMin[toProgram] = programs.C5_HighGainMin[fromProgram];
            programs.C5_LowGain[toProgram] = programs.C5_LowGain[fromProgram];
            programs.C5_MPO[toProgram] = programs.C5_MPO[fromProgram];
            programs.C5_Ratio[toProgram] = programs.C5_Ratio[fromProgram];
            programs.C5_TK[toProgram] = programs.C5_TK[fromProgram];
            programs.C6_ExpTK[toProgram] = programs.C6_ExpTK[fromProgram];
            programs.C6_HighGain[toProgram] = programs.C6_HighGain[fromProgram];
            programs.C6_HighGainMin[toProgram] = programs.C6_HighGainMin[fromProgram];
            programs.C6_LowGain[toProgram] = programs.C6_LowGain[fromProgram];
            programs.C6_MPO[toProgram] = programs.C6_MPO[fromProgram];
            programs.C6_Ratio[toProgram] = programs.C6_Ratio[fromProgram];
            programs.C6_TK[toProgram] = programs.C6_TK[fromProgram];
            programs.Exp_Attack[toProgram] = programs.Exp_Attack[fromProgram];
            programs.Exp_Ratio[toProgram] = programs.Exp_Ratio[fromProgram];
            programs.Exp_Release[toProgram] = programs.Exp_Release[fromProgram];
            programs.FBC_Enable[toProgram] = programs.FBC_Enable[fromProgram];
            programs.input_mux[toProgram] = programs.input_mux[fromProgram];
            programs.matrix_gain[toProgram] = programs.matrix_gain[fromProgram];
            programs.MPO_Attack[toProgram] = programs.MPO_Attack[fromProgram];
            programs.MPO_Release[toProgram] = programs.MPO_Release[fromProgram];
            programs.Noise_Reduction[toProgram] = programs.Noise_Reduction[fromProgram];
            programs.preamp_gain0[toProgram] = programs.preamp_gain0[fromProgram];
            programs.preamp_gain1[toProgram] = programs.preamp_gain1[fromProgram];
            programs.TimeConstants1[toProgram] = programs.TimeConstants1[fromProgram];
            programs.TimeConstants2[toProgram] = programs.TimeConstants2[fromProgram];
            programs.TimeConstants3[toProgram] = programs.TimeConstants3[fromProgram];
            programs.TimeConstants4[toProgram] = programs.TimeConstants4[fromProgram];
            programs.TimeConstants5[toProgram] = programs.TimeConstants5[fromProgram];
            programs.TimeConstants6[toProgram] = programs.TimeConstants6[fromProgram];
            //programs.VcPosition = programs.VcPosition;
        }

        public static void CopyProgram(short fromProgram, short toProgram, ParamsHighLowGain fromPrograms, ref ParamsHighLowGain toPrograms)
        {
            toPrograms.ActiveProgram = fromPrograms.ActiveProgram;
            toPrograms.BEQ1_gain[toProgram] = fromPrograms.BEQ1_gain[fromProgram];
            toPrograms.BEQ2_gain[toProgram] = fromPrograms.BEQ2_gain[fromProgram];
            toPrograms.BEQ3_gain[toProgram] = fromPrograms.BEQ3_gain[fromProgram];
            toPrograms.BEQ4_gain[toProgram] = fromPrograms.BEQ4_gain[fromProgram];
            toPrograms.BEQ5_gain[toProgram] = fromPrograms.BEQ5_gain[fromProgram];
            toPrograms.BEQ6_gain[toProgram] = fromPrograms.BEQ6_gain[fromProgram];
            toPrograms.BEQ7_gain[toProgram] = fromPrograms.BEQ7_gain[fromProgram];
            toPrograms.BEQ8_gain[toProgram] = fromPrograms.BEQ8_gain[fromProgram];
            toPrograms.BEQ9_gain[toProgram] = fromPrograms.BEQ9_gain[fromProgram];
            toPrograms.BEQ10_gain[toProgram] = fromPrograms.BEQ10_gain[fromProgram];
            toPrograms.BEQ11_gain[toProgram] = fromPrograms.BEQ11_gain[fromProgram];
            toPrograms.BEQ12_gain[toProgram] = fromPrograms.BEQ12_gain[fromProgram];
            toPrograms.C1_ExpTK[toProgram] = fromPrograms.C1_ExpTK[fromProgram];
            toPrograms.C1_HighGain[toProgram] = fromPrograms.C1_HighGain[fromProgram];
            toPrograms.C1_HighGainMin[toProgram] = fromPrograms.C1_HighGainMin[fromProgram];
            toPrograms.C1_LowGain[toProgram] = fromPrograms.C1_LowGain[fromProgram];
            toPrograms.C1_MPO[toProgram] = fromPrograms.C1_MPO[fromProgram];
            toPrograms.C1_Ratio[toProgram] = fromPrograms.C1_Ratio[fromProgram];
            toPrograms.C1_TK[toProgram] = fromPrograms.C1_TK[fromProgram];
            toPrograms.C2_ExpTK[toProgram] = fromPrograms.C2_ExpTK[fromProgram];
            toPrograms.C2_HighGain[toProgram] = fromPrograms.C2_HighGain[fromProgram];
            toPrograms.C2_HighGainMin[toProgram] = fromPrograms.C2_HighGainMin[fromProgram];
            toPrograms.C2_LowGain[toProgram] = fromPrograms.C2_LowGain[fromProgram];
            toPrograms.C2_MPO[toProgram] = fromPrograms.C2_MPO[fromProgram];
            toPrograms.C2_Ratio[toProgram] = fromPrograms.C2_Ratio[fromProgram];
            toPrograms.C2_TK[toProgram] = fromPrograms.C2_TK[fromProgram];
            toPrograms.C3_ExpTK[toProgram] = fromPrograms.C3_ExpTK[fromProgram];
            toPrograms.C3_HighGain[toProgram] = fromPrograms.C3_HighGain[fromProgram];
            toPrograms.C3_HighGainMin[toProgram] = fromPrograms.C3_HighGainMin[fromProgram];
            toPrograms.C3_LowGain[toProgram] = fromPrograms.C3_LowGain[fromProgram];
            toPrograms.C3_MPO[toProgram] = fromPrograms.C3_MPO[fromProgram];
            toPrograms.C3_Ratio[toProgram] = fromPrograms.C3_Ratio[fromProgram];
            toPrograms.C3_TK[toProgram] = fromPrograms.C3_TK[fromProgram];
            toPrograms.C4_ExpTK[toProgram] = fromPrograms.C4_ExpTK[fromProgram];
            toPrograms.C4_HighGain[toProgram] = fromPrograms.C4_HighGain[fromProgram];
            toPrograms.C4_HighGainMin[toProgram] = fromPrograms.C4_HighGainMin[fromProgram];
            toPrograms.C4_LowGain[toProgram] = fromPrograms.C4_LowGain[fromProgram];
            toPrograms.C4_MPO[toProgram] = fromPrograms.C4_MPO[fromProgram];
            toPrograms.C4_Ratio[toProgram] = fromPrograms.C4_Ratio[fromProgram];
            toPrograms.C4_TK[toProgram] = fromPrograms.C4_TK[fromProgram];
            toPrograms.C5_ExpTK[toProgram] = fromPrograms.C5_ExpTK[fromProgram];
            toPrograms.C5_HighGain[toProgram] = fromPrograms.C5_HighGain[fromProgram];
            toPrograms.C5_HighGainMin[toProgram] = fromPrograms.C5_HighGainMin[fromProgram];
            toPrograms.C5_LowGain[toProgram] = fromPrograms.C5_LowGain[fromProgram];
            toPrograms.C5_MPO[toProgram] = fromPrograms.C5_MPO[fromProgram];
            toPrograms.C5_Ratio[toProgram] = fromPrograms.C5_Ratio[fromProgram];
            toPrograms.C5_TK[toProgram] = fromPrograms.C5_TK[fromProgram];
            toPrograms.C6_ExpTK[toProgram] = fromPrograms.C6_ExpTK[fromProgram];
            toPrograms.C6_HighGain[toProgram] = fromPrograms.C6_HighGain[fromProgram];
            toPrograms.C6_HighGainMin[toProgram] = fromPrograms.C6_HighGainMin[fromProgram];
            toPrograms.C6_LowGain[toProgram] = fromPrograms.C6_LowGain[fromProgram];
            toPrograms.C6_MPO[toProgram] = fromPrograms.C6_MPO[fromProgram];
            toPrograms.C6_Ratio[toProgram] = fromPrograms.C6_Ratio[fromProgram];
            toPrograms.C6_TK[toProgram] = fromPrograms.C6_TK[fromProgram];
            toPrograms.Exp_Attack[toProgram] = fromPrograms.Exp_Attack[fromProgram];
            toPrograms.Exp_Ratio[toProgram] = fromPrograms.Exp_Ratio[fromProgram];
            toPrograms.Exp_Release[toProgram] = fromPrograms.Exp_Release[fromProgram];
            toPrograms.FBC_Enable[toProgram] = fromPrograms.FBC_Enable[fromProgram];
            toPrograms.input_mux[toProgram] = fromPrograms.input_mux[fromProgram];
            toPrograms.matrix_gain[toProgram] = fromPrograms.matrix_gain[fromProgram];
            toPrograms.MPO_Attack[toProgram] = fromPrograms.MPO_Attack[fromProgram];
            toPrograms.MPO_Release[toProgram] = fromPrograms.MPO_Release[fromProgram];
            toPrograms.Noise_Reduction[toProgram] = fromPrograms.Noise_Reduction[fromProgram];
            toPrograms.preamp_gain0[toProgram] = fromPrograms.preamp_gain0[fromProgram];
            toPrograms.preamp_gain1[toProgram] = fromPrograms.preamp_gain1[fromProgram];
            toPrograms.TimeConstants1[toProgram] = fromPrograms.TimeConstants1[fromProgram];
            toPrograms.TimeConstants2[toProgram] = fromPrograms.TimeConstants2[fromProgram];
            toPrograms.TimeConstants3[toProgram] = fromPrograms.TimeConstants3[fromProgram];
            toPrograms.TimeConstants4[toProgram] = fromPrograms.TimeConstants4[fromProgram];
            toPrograms.TimeConstants5[toProgram] = fromPrograms.TimeConstants5[fromProgram];
            toPrograms.TimeConstants6[toProgram] = fromPrograms.TimeConstants6[fromProgram];
            toPrograms.VcPosition = fromPrograms.VcPosition;
        }
    }
}