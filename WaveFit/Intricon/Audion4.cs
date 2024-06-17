using System;
using System.Runtime.InteropServices;

namespace IAmp
{
    /// <summary>
    /// This is a .NET interface to the legacy Audion4.dll. Includes some extra features such as Autofit target file read/write, PGM file
    /// read/write, and VC calculations.
    /// </summary>
    public class Audion4
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
            public short ModelID;
            public short reserved1;
            public short reserved2;
            public short reserved3;
            public short reserved4;
            public int MANF_reserve_1;
            public int MANF_reserve_2;
            public int MANF_reserve_3;
            public int MANF_reserve_4;
            public int MANF_reserve_5;
            public int MANF_reserve_6;
            public int MANF_reserve_7;
            public int MANF_reserve_8;
            public int MANF_reserve_9;
            public int MANF_reserve_10;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Params
        {
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
            public short[] C1_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C2_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C3_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] C4_Ratio;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Expansion_Enable;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] FBC_Enable;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] High_Cut;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] input_mux;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Low_Cut;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] matrix_gain;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] MPO_level;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Noise_Reduction;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] preamp_gain0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] preamp_gain1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] threshold;

            public Params(bool init)
            {
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
                C1_Ratio = new short[5];
                C2_Ratio = new short[5];
                C3_Ratio = new short[5];
                C4_Ratio = new short[5];
                Expansion_Enable = new short[5];
                FBC_Enable = new short[5];
                High_Cut = new short[5];
                input_mux = new short[5];
                Low_Cut = new short[5];
                matrix_gain = new short[5];
                MPO_level = new short[5];
                Noise_Reduction = new short[5];
                preamp_gain0 = new short[5];
                preamp_gain1 = new short[5];
                threshold = new short[5];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Config
        {
            public short ATC;
            public short Auto_Save;
            public short Cal_Input;
            public short Dir_Spacing;
            public short Low_Batt_Warning;
            public short MAP_HC;
            public short MAP_LC;
            public short MAP_MPO;
            public short MAP_TK;
            public short Mic_Cal;
            public short number_of_programs;
            public short Power_On_Level;
            public short Power_On_Delay;
            public short Program_StartUp;
            public short Out_Mode;
            public short Switch_Mode;
            public short Switch_Tone;
            public short T1_DIR;
            public short T2_DIR;
            public short test;
            public short Tone_Frequency;
            public short Tone_Level;
            public short Time_Constants;
            public short VC_AnalogRange;
            public short VC_Beep_Enable;
            public short VC_DigitalNumSteps;
            public short VC_DigitalStepSize;
            public short VC_Enable;
            public short VC_Mode;
            public short VC_Startup;
            public short Active_PGM;
            public short T1_POS;
            public short T2_POS;
            public short VC_Pos;
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
        {/*<Summary>
          * sng50: 50 dB input signal target gain values
          * sng80: 80 dB input signal target gain values
          * CR: actual compression ratio values from 1.0 to 4.0 to use if useCR is 1. Driver rounds this to nearest compression ratio available in the amp.
          * TK: This compression threshold is a dB SPL value from 45 to 75, and driver rounds to nearest value available in the amp. If MAP_TK is enabled then this is ignored and the threshold is set to 60 dB (middle of the range) during the rest of the Autofit algorithm
          * MPO: maximum power output in dB SPL. If MAP_MPO is enabled then this is ignored and the MPO is set to -12 dB (middle of the range) during the rest of the Autofit algorithm.
          * ResGain: This is the amount of headroom to leave in the volume control. In other words, this is amount of gain above this VC position to get full volume (how many dB below top of VC to to do the autofit at).
          * useCR: if = 0 then calculate compression ratios (ignore CR); if= 1 the use CR's passed in.
          * isNL2: if = 0 then target is from Intricon algorithm (NAL Linear + FIG6); if = 1 the target from NL2. Due to differences in the prescriptions, some things in the Autofit must be handled differently.
          * Data: 01/13/2023
          */

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Single[] sng50;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Single[] sng80;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Single[] CR;

            public short TK;
            public short MPO;
            public short ResGain;
            public short useCR;
            public short isNL2;

            public TargetParams(bool init)
            {
                sng50 = new Single[11];
                sng80 = new Single[11];
                CR = new Single[4];
                TK = 65;
                MPO = 95;
                ResGain = 6;
                useCR = 0;
                isNL2 = 0;
            }
        }

        public enum RL_channel
        {
            channelLeft = 0,
            channelRight = 1
        }

        public enum DataPass
        {
            loadProgramConfigUpdates = -2,
            passAll = -1,
            passParamsAndConfig = 0,
            passMDA = 1,
        }

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Audio_on(short active_program);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short AutoFit(short Datversion, short ManID, ref TargetParams target_params);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Close();

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Connected();

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Detect(ref DetectData detdata);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_active_program(ref short program);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_Autofit_Matrix_gain_ceiling(ref short ceiling);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_config(short version, ref Config Config);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short GetEEPROMData();

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_FR_array(short input_level, ref Response Response);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_interface_type(ref short type);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_last_interface_error();

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_MDA(short Passcode, short version, ref MDA MDAparams);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Get_params(short version, ref Params Params);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_RL_channel(ref short channel);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short HaveAnyParamErrors();

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short InitializeDriver();

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Load(short section);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Lock(short section);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Mute();

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Read(short section);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_active_program(short program);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_Active_VC_Position(short position);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_Autofit_Matrix_gain_ceiling(short ceiling);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_config(short version, ref Config Config);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_interface_type(short type);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_MDA(short version, ref MDA MDAparams);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_mic_response(ref Response mic_array);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Set_params(short version, ref Params Params);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_PassCode(Int32 CurrentPassCode, Int32 NewPassCode);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_platform_id(short ID);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetProgram(short program);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_rec_response(ref Response rec_array);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetRec_Saturation(Single Sat_level);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_RL_channel(short type);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetToTest(short Datversion, short ManID);

        [DllImport("Audion4.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short TestTone(short numBeeps);

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
            programs.C1_Ratio[toProgram] = programs.C1_Ratio[fromProgram];
            programs.C2_Ratio[toProgram] = programs.C2_Ratio[fromProgram];
            programs.C3_Ratio[toProgram] = programs.C3_Ratio[fromProgram];
            programs.C4_Ratio[toProgram] = programs.C4_Ratio[fromProgram];
            programs.Expansion_Enable[toProgram] = programs.Expansion_Enable[fromProgram];
            programs.FBC_Enable[toProgram] = programs.FBC_Enable[fromProgram];
            programs.High_Cut[toProgram] = programs.High_Cut[fromProgram];
            programs.input_mux[toProgram] = programs.input_mux[fromProgram];
            programs.Low_Cut[toProgram] = programs.Low_Cut[fromProgram];
            programs.matrix_gain[toProgram] = programs.matrix_gain[fromProgram];
            programs.MPO_level[toProgram] = programs.MPO_level[fromProgram];
            programs.Noise_Reduction[toProgram] = programs.Noise_Reduction[fromProgram];
            programs.preamp_gain0[toProgram] = programs.preamp_gain0[fromProgram];
            programs.preamp_gain1[toProgram] = programs.preamp_gain1[fromProgram];
            programs.threshold[toProgram] = programs.threshold[fromProgram];
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
            toPrograms.C1_Ratio[toProgram] = fromPrograms.C1_Ratio[fromProgram];
            toPrograms.C2_Ratio[toProgram] = fromPrograms.C2_Ratio[fromProgram];
            toPrograms.C3_Ratio[toProgram] = fromPrograms.C3_Ratio[fromProgram];
            toPrograms.C4_Ratio[toProgram] = fromPrograms.C4_Ratio[fromProgram];
            toPrograms.Expansion_Enable[toProgram] = fromPrograms.Expansion_Enable[fromProgram];
            toPrograms.FBC_Enable[toProgram] = fromPrograms.FBC_Enable[fromProgram];
            toPrograms.High_Cut[toProgram] = fromPrograms.High_Cut[fromProgram];
            toPrograms.input_mux[toProgram] = fromPrograms.input_mux[fromProgram];
            toPrograms.Low_Cut[toProgram] = fromPrograms.Low_Cut[fromProgram];
            toPrograms.matrix_gain[toProgram] = fromPrograms.matrix_gain[fromProgram];
            toPrograms.MPO_level[toProgram] = fromPrograms.MPO_level[fromProgram];
            toPrograms.Noise_Reduction[toProgram] = fromPrograms.Noise_Reduction[fromProgram];
            toPrograms.preamp_gain0[toProgram] = fromPrograms.preamp_gain0[fromProgram];
            toPrograms.preamp_gain1[toProgram] = fromPrograms.preamp_gain1[fromProgram];
            toPrograms.threshold[toProgram] = fromPrograms.threshold[fromProgram];
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