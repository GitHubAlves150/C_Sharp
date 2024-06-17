using System;
using System.Runtime.InteropServices;

namespace IAmp
{
    /// <summary>
    /// This is a .NET interface to the legacy SpinNR.dll.
    /// Includes some extra features such as Autofit target,
    /// file read/write, PGM file read/write, and VC calculations.
    /// </summary>
    public class SpinNR
    {
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

        [StructLayout(LayoutKind.Sequential)]
        public struct DetectData
        {
            public short Platform_ID;
            public short AlgVer_Major;
            public short AlgVer_Minor;
            public short MANF_ID;
            public short reserved1;
            public short reserved2;
            public short reserved3;
            public short reserved4;
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
            public short[] CRL;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] CRH;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] threshold;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Low_Cut;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] High_Cut;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Noise_Reduction;

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
            public short[] MPO_level;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] FBC_Enable;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Cal_Input;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public short[] Mic_Cal;

            public Params(bool init)
            {
                input_mux = new short[5];
                preamp_gain0 = new short[5];
                preamp_gain1 = new short[5];
                CRL = new short[5];
                CRH = new short[5];
                threshold = new short[5];
                Low_Cut = new short[5];
                High_Cut = new short[5];
                Noise_Reduction = new short[5];
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
                MPO_level = new short[5];
                FBC_Enable = new short[5];
                Cal_Input = new short[5];
                Mic_Cal = new short[5];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Config
        {
            public short number_of_programs;
            public short VC_MAP;
            public short VC_Range;
            public short VC_pos;
            public short TK_MAP;
            public short HC_MAP;
            public short LC_MAP;
            public short MPO_MAP;
            public short T1_DIR;
            public short T2_DIR;
            public short T3_DIR;
            public short CoilPGM;
            public short MANF_ID;
            public short OutMode;
            public short Switch_Tone;
            public short Low_Batt_Warning;
            public short Tone_Frequency;
            public short Tone_Level;
            public short ATC;
            public short TimeConstants;
            public short Mic_Expansion;
            public short reserved1;
            public short reserved2;
            public short reserved3;
            public short reserved4;
            public short test;
            public short T1_POS;
            public short T2_POS;
            public short T3_POS;
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
        public struct AudiogramParams
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Single[] aud;

            public short UCL;

            public AudiogramParams(bool init)
            {
                aud = new Single[11];
                UCL = 85;
            }
        }

        public enum RL_channel
        {
            channelLeft = 0,
            channelRight = 1
        }

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Initialize();

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Close();

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Read(short page);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Load(short page);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Lock();

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Detect(ref DetectData detdata);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Connected();

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Audio_on(short active_program);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_last_interface_error();

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Get_params(short version, ref Params Params);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern short Set_params(short version, ref Params Params);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_config(short version, ref Config Config);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_config(short version, ref Config Config);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_active_program(ref short program);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_active_program(short program);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetProgram(short program);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_RL_channel(short channel);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_RL_channel(ref short channel);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_interface_type(short type);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_interface_type(ref short type);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_FR_array(short input_level, ref Response SpinNR_Response);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_mic_response(ref Response mic_array);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_mic_response_Int(ref Response_Int mic_array);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_rec_response(ref Response rec_array);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_rec_response_Int(ref Response_Int rec_array);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short AutoFit(short Datversion, short ManID, ref TargetParams target_params);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short SetToTest(short Datversion, short ManID);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_platform_id(short ID);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Get_IntFR_array(short Input_Level, ref Response_Int response);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Mute();

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short ResetBootStatus();

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short TestTone(short numBeeps);

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short GetEEPROMData();

        [DllImport("SpinNR.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short Set_VC_Mode(short Mode);

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
            programs.CRL[toProgram] = programs.CRL[fromProgram];
            programs.CRH[toProgram] = programs.CRH[fromProgram];
            programs.threshold[toProgram] = programs.threshold[fromProgram];
            programs.Low_Cut[toProgram] = programs.Low_Cut[fromProgram];
            programs.High_Cut[toProgram] = programs.High_Cut[fromProgram];
            programs.Noise_Reduction[toProgram] = programs.Noise_Reduction[fromProgram];
            programs.MPO_level[toProgram] = programs.MPO_level[fromProgram];
            programs.FBC_Enable[toProgram] = programs.FBC_Enable[fromProgram];
            programs.input_mux[toProgram] = programs.input_mux[fromProgram];
            programs.matrix_gain[toProgram] = programs.matrix_gain[fromProgram];
            programs.Cal_Input[toProgram] = programs.Cal_Input[fromProgram];
            programs.preamp_gain0[toProgram] = programs.preamp_gain0[fromProgram];
            programs.preamp_gain1[toProgram] = programs.preamp_gain1[fromProgram];
            programs.Mic_Cal[toProgram] = programs.Mic_Cal[fromProgram];
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
            toPrograms.CRL[toProgram] = fromPrograms.CRL[fromProgram];
            toPrograms.CRH[toProgram] = fromPrograms.CRH[fromProgram];
            toPrograms.threshold[toProgram] = fromPrograms.threshold[fromProgram];
            toPrograms.Low_Cut[toProgram] = fromPrograms.Low_Cut[fromProgram];
            toPrograms.High_Cut[toProgram] = fromPrograms.High_Cut[fromProgram];
            toPrograms.Noise_Reduction[toProgram] = fromPrograms.Noise_Reduction[fromProgram];
            toPrograms.MPO_level[toProgram] = fromPrograms.MPO_level[fromProgram];
            toPrograms.FBC_Enable[toProgram] = fromPrograms.FBC_Enable[fromProgram];
            toPrograms.input_mux[toProgram] = fromPrograms.input_mux[fromProgram];
            toPrograms.matrix_gain[toProgram] = fromPrograms.matrix_gain[fromProgram];
            toPrograms.Cal_Input[toProgram] = fromPrograms.Cal_Input[fromProgram];
            toPrograms.preamp_gain0[toProgram] = fromPrograms.preamp_gain0[fromProgram];
            toPrograms.preamp_gain1[toProgram] = fromPrograms.preamp_gain1[fromProgram];
            toPrograms.Mic_Cal[toProgram] = fromPrograms.Mic_Cal[fromProgram];
        }

        public enum ErrorCodes
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

        public enum Programmers
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