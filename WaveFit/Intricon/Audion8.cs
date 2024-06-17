using System;
using System.Runtime.InteropServices;

namespace IAmp
{
    public class Audion8
    {
        public enum ReadPages
        {
            ManufacturingDataOnly = -2,
            AllPages = -1
        }

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

        [StructLayout(LayoutKind.Sequential)]
        public struct scanInfo
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] name;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] addr;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] custom;

            public byte orientation;
            public byte RSSI;
            public byte bond;
            public byte my_type;

            public scanInfo(bool doInit)
            {
                name = new char[16];
                addr = new char[16];
                custom = new char[20];
                orientation = 0;
                RSSI = 0;
                bond = 0xFF;
                my_type = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DetectData
        {
            public Int16 Platform_ID;
            public Int16 AlgVer_Major;
            public Int16 AlgVer_Minor;
            public Int16 LayoutVersion;
            public Int16 MANF_ID;
            public Int16 ModelID;
            public Int16 reserved1;
            public Int16 reserved2;
            public Int16 reserved3;
            public Int16 reserved4;
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
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Params
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] input_mux;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] preamp_gain0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] preamp_gain1;

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
            public short[] C7_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C8_Ratio;

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
            public short[] C7_TK;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C8_TK;

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
            public short[] C7_MPO;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C8_MPO;

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
            public short[] matrix_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Noise_Reduction;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] FBC_Enable;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Time_Constants;

            public Params(bool init)
            {
                input_mux = new short[5];
                preamp_gain0 = new short[5];
                preamp_gain1 = new short[5];
                C1_Ratio = new short[5];
                C2_Ratio = new short[5];
                C3_Ratio = new short[5];
                C4_Ratio = new short[5];
                C5_Ratio = new short[5];
                C6_Ratio = new short[5];
                C7_Ratio = new short[5];
                C8_Ratio = new short[5];
                C1_TK = new short[5];
                C2_TK = new short[5];
                C3_TK = new short[5];
                C4_TK = new short[5];
                C5_TK = new short[5];
                C6_TK = new short[5];
                C7_TK = new short[5];
                C8_TK = new short[5];
                C1_MPO = new short[5];
                C2_MPO = new short[5];
                C3_MPO = new short[5];
                C4_MPO = new short[5];
                C5_MPO = new short[5];
                C6_MPO = new short[5];
                C7_MPO = new short[5];
                C8_MPO = new short[5];
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
                matrix_gain = new short[5];
                Noise_Reduction = new short[5];
                FBC_Enable = new short[5];
                Time_Constants = new short[5];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Config
        {
            public short AutoSave_Enable;
            public short ATC;
            public short EnableHPmode;
            public short Noise_Level;
            public short POL;
            public short POD;
            public short AD_Sens;
            public short Cal_Input;
            public short Dir_Spacing;
            public short Mic_Cal;
            public short number_of_programs;
            public short PGM_Startup;
            public short Switch_Mode;
            public short Program_Prompt_Mode;
            public short Warning_Prompt_Mode;
            public short Tone_Frequency;
            public short Tone_Level;
            public short VC_Enable;
            public short VC_Analog_Range;
            public short VC_Digital_Numsteps;
            public short VC_Digital_Startup;
            public short VC_Digital_Stepsize;
            public short VC_Mode;
            public short VC_pos;
            public short VC_Prompt_Mode;
            public short AlgVer_Major;
            public short AlgVer_Minor;
            public short MANF_ID;
            public short PlatformID;
            public short reserved1;
            public short reserved2;
            public short test;
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
        public struct Response_Int
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65)]
            public short[] element;

            public Response_Int(bool init)
            {
                element = new short[65];
            }
        }

        private enum VpLanguage_type
        {
            English = 1,
            Russian = 2,
            Turkish = 3,
            Chinese = 4,
            German = 5
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

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public Single[] CR;

            public short SpeechTK;  /// Speech equivalent settings in the thresholds. Negative values set all TKs to that absolute value. Example: SpeechTK = 55 give 55dB speech equivalent (55,55,50,50,45,45,40,40).

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public short[] MPO;

            public short ResGain;
            public short Use_CR; /// Bit 1: 1 = use values passed in with CR, 0 = calculate them based on sng80 to sng50 difference. Bit 2: 1 = use TK values currently set in driver, 0 = set TKs as defined by SpeechTK target parameter. Bit 3 when calc CRs: 0 = CR half rounding, 1 = CR down rounding.

            public Target2Params(bool init)
            {
                sng50 = new Single[11];
                sng80 = new Single[11];
                CR = new Single[8];
                SpeechTK = 65;
                MPO = new short[8];
                ResGain = 6;
                Use_CR = 3;
            }

            public bool AreEqual(Target2Params obj, ref string failsOn)
            {
                bool passes = true;
                if (obj.ResGain != ResGain)
                {
                    failsOn += "ResGain, ";
                    passes = false;
                }

                if (obj.SpeechTK != SpeechTK)
                {
                    failsOn += "SpeechTK, ";
                    passes = false;
                }

                if (obj.Use_CR != Use_CR)
                {
                    failsOn += "Use_CR, ";
                    passes = false;
                }

                for (int i = 0; i < 11; i++)
                {
                    if (!Utilities.NearlyEqual(obj.sng50[i], sng50[i], 0.001))
                    {
                        failsOn += "sng50[" + i + "], ";
                        passes = false;
                    }
                    if (!Utilities.NearlyEqual(obj.sng80[i], sng80[i], 0.001))
                    {
                        failsOn += "sng80[" + i + "], ";
                        passes = false;
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    if (!Utilities.NearlyEqual(obj.CR[i], CR[i], 0.001))
                    {
                        failsOn += "CR[" + i + "], ";
                        passes = false;
                    }
                    if (obj.MPO[i] != MPO[i])
                    {
                        failsOn += "MPO[" + i + "], ";
                        passes = false;
                    }
                }

                if (failsOn != "")
                {
                    char[] charac = { ' ', ',' };
                    failsOn = failsOn.TrimEnd(charac);
                }

                return passes;
            }
        }

        public struct Audion8_AutofitParams
        {
            public short[] TK;

            //Constructor/Initializer: Must send in a value to initialize values
            public Audion8_AutofitParams(bool init)
            {
                TK = new short[8];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Events
        {
            public Int32 EventStatus;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DataLog
        {
            public Int32 DateCode;
            public Int32 Clock;
            public Int32 EventNum;
            public Int32 EventPtr;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 500)]
            public Events[] Events;

            public DataLog(bool init)
            {
                Events = new Events[500];
                DateCode = 0;
                Clock = 0;
                EventNum = 0;
                EventPtr = 0;
            }
        }

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short InitializeDriver();

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Close();

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Read(short page);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Load(short page);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Lock();

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Detect(ref DetectData detdata);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Connected();

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Audio_on(short active_program);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_last_interface_error();

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Get_params(short version, ref Params Params);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Set_params(short version, ref Params Params);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_config(short version, ref Config Config);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_config(short version, ref Config Config);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_active_program(ref short program);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_active_program(short program);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_RL_channel(short type);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_RL_channel(ref short channel);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_interface_type(short type);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_interface_type(ref short type);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_FR_array(short input_level, ref Response Audion8_Response);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_Voice_Prompt_Language(ref short languageValue);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_mic_response(ref Response mic_array);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_rec_response(ref Response rec_array);

        [DllImport("Audion8.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern short SetRec_Saturation(float Sat_level);

        [DllImport("Audion8.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern float GetRec_Saturation();

        [DllImport("Audion8.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern short SetMatrixGainCeiling(short MaxIndex);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short AutoFit(short Datversion, short ManID, ref TargetParams target_params);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short AutoFit2(short Earmold, ref Target2Params target_params);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetToTest(short Datversion, short ManID);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_platform_id(short ID);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Mute();

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short GetEEPROMData();

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_extension(ref char path);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetValidationMode(short Mode);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short PlayTone(short Freq, short Level, short Duration);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetActive_VCPos(short Start_Val);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_DataLog(short version, ref DataLog Data_Log);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Reset_DataLog(short version);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetProgram(short program);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short TestPrompt(short numBeeps, short Prompt1, short Prompt2);

        [DllImport("Audion8.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short IsConnectedStealth();

        [DllImport("Audion8.dll", EntryPoint = "bleStartScan", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short BleStartScan();

        [DllImport("Audion8.dll", EntryPoint = "bleStopScan", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short BleStopScan();

        [DllImport("Audion8.dll", EntryPoint = "bleGetScanNum", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short BleGetScanNum();

        [DllImport("Audion8.dll", EntryPoint = "bleGetScanItem", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short BleGetScanItem(short item, ref IAmp.Audion8.scanInfo info);

        [DllImport("Audion8.dll", EntryPoint = "bleConnect", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short BleConnect(short Index, short side);

        [DllImport("Audion8.dll", EntryPoint = "bleDisconnect", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short BleDisconnect(short side);

        [DllImport("Audion8.dll", EntryPoint = "ble_DeleteBonds", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short Ble_DeleteBonds(short side);

        public static void CopyProgram(short fromProgram, short toProgram, ref Params programs)
        {
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
            programs.C1_MPO[toProgram] = programs.C1_MPO[fromProgram];
            programs.C1_Ratio[toProgram] = programs.C1_Ratio[fromProgram];
            programs.C1_TK[toProgram] = programs.C1_TK[fromProgram];
            programs.C2_MPO[toProgram] = programs.C2_MPO[fromProgram];
            programs.C2_Ratio[toProgram] = programs.C2_Ratio[fromProgram];
            programs.C2_TK[toProgram] = programs.C2_TK[fromProgram];
            programs.C3_MPO[toProgram] = programs.C3_MPO[fromProgram];
            programs.C3_Ratio[toProgram] = programs.C3_Ratio[fromProgram];
            programs.C3_TK[toProgram] = programs.C3_TK[fromProgram];
            programs.C4_MPO[toProgram] = programs.C4_MPO[fromProgram];
            programs.C4_Ratio[toProgram] = programs.C4_Ratio[fromProgram];
            programs.C4_TK[toProgram] = programs.C4_TK[fromProgram];
            programs.C5_MPO[toProgram] = programs.C5_MPO[fromProgram];
            programs.C5_Ratio[toProgram] = programs.C5_Ratio[fromProgram];
            programs.C5_TK[toProgram] = programs.C5_TK[fromProgram];
            programs.C6_MPO[toProgram] = programs.C6_MPO[fromProgram];
            programs.C6_Ratio[toProgram] = programs.C6_Ratio[fromProgram];
            programs.C6_TK[toProgram] = programs.C6_TK[fromProgram];
            programs.C7_MPO[toProgram] = programs.C7_MPO[fromProgram];
            programs.C7_Ratio[toProgram] = programs.C7_Ratio[fromProgram];
            programs.C7_TK[toProgram] = programs.C7_TK[fromProgram];
            programs.C8_MPO[toProgram] = programs.C8_MPO[fromProgram];
            programs.C8_TK[toProgram] = programs.C8_TK[fromProgram];
            programs.C8_Ratio[toProgram] = programs.C8_Ratio[fromProgram];
            programs.FBC_Enable[toProgram] = programs.FBC_Enable[fromProgram];
            programs.input_mux[toProgram] = programs.input_mux[fromProgram];
            programs.matrix_gain[toProgram] = programs.matrix_gain[fromProgram];
            programs.Noise_Reduction[toProgram] = programs.Noise_Reduction[fromProgram];
            programs.preamp_gain0[toProgram] = programs.preamp_gain0[fromProgram];
            programs.preamp_gain1[toProgram] = programs.preamp_gain1[fromProgram];
            programs.Time_Constants[toProgram] = programs.Time_Constants[fromProgram];
        }

        public static void CopyProgram(short fromProgram, short toProgram, Params fromPrograms, ref Params toPrograms)
        {
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
            toPrograms.C1_MPO[toProgram] = fromPrograms.C1_MPO[fromProgram];
            toPrograms.C1_Ratio[toProgram] = fromPrograms.C1_Ratio[fromProgram];
            toPrograms.C1_TK[toProgram] = fromPrograms.C1_TK[fromProgram];
            toPrograms.C2_MPO[toProgram] = fromPrograms.C2_MPO[fromProgram];
            toPrograms.C2_Ratio[toProgram] = fromPrograms.C2_Ratio[fromProgram];
            toPrograms.C2_TK[toProgram] = fromPrograms.C2_TK[fromProgram];
            toPrograms.C3_MPO[toProgram] = fromPrograms.C3_MPO[fromProgram];
            toPrograms.C3_Ratio[toProgram] = fromPrograms.C3_Ratio[fromProgram];
            toPrograms.C3_TK[toProgram] = fromPrograms.C3_TK[fromProgram];
            toPrograms.C4_MPO[toProgram] = fromPrograms.C4_MPO[fromProgram];
            toPrograms.C4_Ratio[toProgram] = fromPrograms.C4_Ratio[fromProgram];
            toPrograms.C4_TK[toProgram] = fromPrograms.C4_TK[fromProgram];
            toPrograms.C5_MPO[toProgram] = fromPrograms.C5_MPO[fromProgram];
            toPrograms.C5_Ratio[toProgram] = fromPrograms.C5_Ratio[fromProgram];
            toPrograms.C5_TK[toProgram] = fromPrograms.C5_TK[fromProgram];
            toPrograms.C6_MPO[toProgram] = fromPrograms.C6_MPO[fromProgram];
            toPrograms.C6_Ratio[toProgram] = fromPrograms.C6_Ratio[fromProgram];
            toPrograms.C6_TK[toProgram] = fromPrograms.C6_TK[fromProgram];
            toPrograms.C7_MPO[toProgram] = fromPrograms.C7_MPO[fromProgram];
            toPrograms.C7_Ratio[toProgram] = fromPrograms.C7_Ratio[fromProgram];
            toPrograms.C7_TK[toProgram] = fromPrograms.C7_TK[fromProgram];
            toPrograms.C8_MPO[toProgram] = fromPrograms.C8_MPO[fromProgram];
            toPrograms.C8_TK[toProgram] = fromPrograms.C8_TK[fromProgram];
            toPrograms.C8_Ratio[toProgram] = fromPrograms.C8_Ratio[fromProgram];
            toPrograms.FBC_Enable[toProgram] = fromPrograms.FBC_Enable[fromProgram];
            toPrograms.input_mux[toProgram] = fromPrograms.input_mux[fromProgram];
            toPrograms.matrix_gain[toProgram] = fromPrograms.matrix_gain[fromProgram];
            toPrograms.Noise_Reduction[toProgram] = fromPrograms.Noise_Reduction[fromProgram];
            toPrograms.preamp_gain0[toProgram] = fromPrograms.preamp_gain0[fromProgram];
            toPrograms.preamp_gain1[toProgram] = fromPrograms.preamp_gain1[fromProgram];
            toPrograms.Time_Constants[toProgram] = fromPrograms.Time_Constants[fromProgram];
        }

        private enum ErrorCodes
        {
            OK = 0,
            NoProgrammer = 1,
            NoInstrument = 2,
            BadArgument = 3,
            NotInitialized = 4,
            NotRead = 5,
            ChecksumError = 6,
            InvalidVersion = 7,
            ProgrammerError = 8,
            CMFError = 9,
            WrongInstrument = 10,
            BootError = 11,
            NoNLDriver = 12,
            NLInUse = 13
        }

        private enum Programmers
        {
            HiPro = 0,
            Microcard = 1,
            Simulation = 2,
            NOAHlink = 3,
            CF2 = 4,
            eMiniTec = 5
        }

        public static short getVcMaxIndex(short VC_Mode, short VC_Digital_Numsteps)
        {
            if (VC_Mode == 0)
            {
                return 31;
            }

            return (short)((VC_Digital_Numsteps + 1) * 5);
        }

        public static short getVcMaxDb(short VC_Mode, short VC_Digital_Numsteps, short VC_Digital_Stepsize, short VC_Analog_Range)
        {
            if (VC_Mode == 0)
            {
                return (short)(-(50 - 10 * VC_Analog_Range));
            }

            return (short)(-((VC_Digital_Numsteps + 1) * 5) * (VC_Digital_Stepsize + 1));
        }

        public static float getVcStepSizeDb(short VC_Mode, short VC_Digital_Stepsize, short VC_Analog_Range)
        {
            if (VC_Mode == 0)
            {
                return (50f - 10f * (float)VC_Analog_Range) / 31f;
            }

            return (float)VC_Digital_Stepsize + 1f;
        }

        public static short getVcDbfromIndex(short index, short VC_Mode, short VC_Digital_Stepsize, short VC_Analog_Range)
        {
            return (short)((float)index * getVcStepSizeDb(VC_Mode, VC_Digital_Stepsize, VC_Analog_Range));
        }

        public static short getVcIndexFromDb(short dB, short VC_Mode, short VC_Digital_Stepsize, short VC_Analog_Range)
        {
            return (short)((float)dB / getVcStepSizeDb(VC_Mode, VC_Digital_Stepsize, VC_Analog_Range) + 0.5f);
        }

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
    }
}