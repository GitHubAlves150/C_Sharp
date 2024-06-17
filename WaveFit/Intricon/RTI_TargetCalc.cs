using System;
using System.Runtime.InteropServices;

namespace IAmp
{
    internal class RTI_TargetCalc
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RTI_Target
        {
            public RTI_Target(bool init)
            {
                tar50 = new float[11];
                tar80 = new float[11];
                MPO = 0;
                ResGain = 0;
            }

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] tar50;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] tar80;
            public short MPO;
            public short ResGain;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NL2_2CH_Target
        {
            public NL2_2CH_Target(bool init)
            {
                sng50 = new float[11];
                sng80 = new float[11];
                CR = new float[2];
                TK = new float[2];
                MPO = 0;
                SpeechTK = 0;
                ResGain = 0;
                Use_CR = 0;
            }

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] sng50;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] sng80;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public float[] CR;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public float[] TK;
            public short MPO;
            public short SpeechTK;
            public short ResGain;
            public short Use_CR;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NL2_4CH_Target
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] sng50;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] sng80;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public float[] CR;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public float[] TK;
            public short MPO;
            public short SpeechTK;
            public short ResGain;
            public short Use_CR; //0 = calc CR from 50/80 targets, 1 = use provided CR and TK values
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NL2_6CH_Target
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] sng50;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] sng80;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public float[] CR;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public float[] MPO;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public float[] TK;
            public short SpeechTK;
            public short ResGain;
            public short Use_CR; //0 = calc CR from 50/80 targets, 1 = use provided CR and TK values
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NL2_8CH_Target
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] sng50;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public float[] sng80;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public float[] CR;
            public short SpeechTK;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public float[] MPO;
            public short ResGain;
            public short Use_CR; //0 = calc CR from 50/80 targets, 1 = use provided CR and TK values
        }

        //public const short numAud = 11; - Different way of representing the element array

        [StructLayout(LayoutKind.Sequential)]
        public struct RTI_Audiogram
        {
            public RTI_Audiogram(bool init)
            {
                element = new short[11];
                speechUCL = 100;
            }

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)] public short[] element;
            public short speechUCL;
        }

        [DllImport("RTI_TargetCalc.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern Int16 Calculate_Target(ref RTI_Target Target);

        [DllImport("RTI_TargetCalc.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern Int16 Set_Aud_Data(ref RTI_Audiogram audiogram);

        private enum errorCodeRtiTarget
        {
            tarOK = 0, //no error
            tarBadArgument = 1 //function was passed invalid date
        };

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
                    DriverErrorString = "(Error # 0) OK";
                    break;

                case 1:
                    DriverErrorString = "(Error # 1) No Programmer";
                    break;

                case 2:
                    DriverErrorString = "(Error # 2) No Instrument";
                    break;

                case 3:
                    DriverErrorString = "(Error # 3) Bad Argument";
                    break;

                case 4:
                    DriverErrorString = "(Error # 4) Not Initialized";
                    break;

                case 5:
                    DriverErrorString = "(Error # 5) Not Read";
                    break;

                case 6:
                    DriverErrorString = "(Error # 6) Check Sum Error";
                    break;

                case 7:
                    DriverErrorString = "(Error # 7) Invalid Version";
                    break;

                case 8:
                    DriverErrorString = "(Error # 8) Programmer Error";
                    break;

                case 9:
                    DriverErrorString = "(Error # 9) CMF Error";
                    break;

                case 10:
                    DriverErrorString = "(Error # 10) Wrong Instrument";
                    break;

                case 11:
                    DriverErrorString = "(Error # 11) Boot Error";
                    break;

                case 12:
                    DriverErrorString = "(Error # 1) No NL Driver";
                    break;

                case 13:
                    DriverErrorString = "(Error # 13) NL In Use";
                    break;

                default:
                    DriverErrorString = "Error # " + ErrorCode + " is an Unknown error code";
                    break;
            }
            return DriverErrorString;
        }

        /*

        RTITARGETSHARED_EXPORT short Set_Aud_Data(RTI_Audiogram *Aud);
        RTITARGETSHARED_EXPORT short Calculate_Target(RTI_Target *Target);
        RTITARGETSHARED_EXPORT short Set_Reserve_Gain(short reserve); */
    }
}