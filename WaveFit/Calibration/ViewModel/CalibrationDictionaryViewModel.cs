using System;
using System.Collections.Generic;

namespace WaveFit2.Calibration.ViewModel
{
    public class CalibrationDictionaryViewModel
    {
        public Dictionary<int, int> Audion16InputMultiplexer;
        public Dictionary<int, int> Audion16AnalogPreAmpGain;
        public Dictionary<int, int> Audion16DigitalPreAmpGain;
        public Dictionary<int, int> Audion16RemoteMixRatio;
        public Dictionary<int, int> Audion16CompressionThresholds;
        public Dictionary<int, String> Audion16CompressionRatio;
        public Dictionary<int, String> Audion16OutputCompressionLimiter;
        public Dictionary<int, String> Audion16LowCutFilter;
        public Dictionary<int, String> Audion16LowLevelExpansion;
        public Dictionary<int, String> Audion16WindSuppression;
        public Dictionary<int, String> Audion16FeedbackCanceller;
        public Dictionary<int, int[]> Audion16TimeConstantsBarFarRrr;
        public Dictionary<int, String> Audion16NoiseReduction;
        public Dictionary<int, int> Audion16MatrixGain;
        public Dictionary<int, int> Audion16BandEqualizationFilter;
        public Dictionary<int, String> Audion16AdaptiveFeedbackCanceller;
        public Dictionary<int, String> Audion16VolumeControlMode;
        public Dictionary<int, String> Audion16VolumeControlAnalogRange;
        public Dictionary<int, int> Audion16VolumeControlDigitalStepSize;
        public Dictionary<int, int> Audion16VolumeControlDigitalNumberOfSteps;
        public Dictionary<int, String> Audion16NumberOfUserPrograms;
        public Dictionary<int, String> Audion16PowerOnProgram;
        public Dictionary<int, String> Audion16ToneFrequency;
        public Dictionary<int, String> Audion16PromptLevel;
        public Dictionary<int, String> Audion16ProgramPromptMode;
        public Dictionary<int, String> Audion16WarningPromptMode;
        public Dictionary<int, String> Audion16AutoSave;
        public Dictionary<int, String> Audion16PowerOnDelay;
        public Dictionary<int, String> Audion16PowerOnLevel;
        public Dictionary<int, String> Audion16AdaptiveDirectionalSensitivity;
        public Dictionary<int, String> Audion16DigitalNoiseGeneratorAmplitude;

        public Dictionary<int, int> Audion8InputMultiplexer;
        public Dictionary<int, int> Audion8PreAmpGain;
        public Dictionary<int, int> Audion8CompressionThresholds;
        public Dictionary<int, string> Audion8CompressionRatio;
        public Dictionary<int, string> Audion8OutputCompressionLimiter;
        public Dictionary<int, int[]> Audion8TimeConstantsBarFarRrr;
        public Dictionary<int, string> Audion8NoiseReduction;
        public Dictionary<int, int> Audion8MatrixGain;
        public Dictionary<int, int> Audion8BandEqualizationFilter;
        public Dictionary<int, string> Audion8AdaptiveFeedbackCanceller;
        public Dictionary<int, string> Audion8VolumeControlMode;
        public Dictionary<int, string> Audion8VolumeControlAnalogRange;
        public Dictionary<int, int> Audion8VolumeControlDigitalStepSize;
        public Dictionary<int, int> Audion8VolumeControlDigitalNumberOfSteps;
        public Dictionary<int, string> Audion8NumberOfUserPrograms;
        public Dictionary<int, int> Audion8ToneFrequency;
        public Dictionary<int, string> Audion8ProgramPromptMode;
        public Dictionary<int, string> Audion8LowBatteryPromptMode;
        public Dictionary<int, string> Audion8PromptLevelIndex;
        public Dictionary<int, string> Audion8AutoSave;
        public Dictionary<int, string> Audion8PowerOnDelay;
        public Dictionary<int, string> Audion8PowerOnLevel;
        public Dictionary<int, string> Audion8AdaptiveDirectionalSensitivity;
        public Dictionary<int, string> Audion8DigitalNoiseGeneratorAmplitude;

        public Dictionary<int, int> Audion6InputMultiplexer;
        public Dictionary<int, int> Audion6PreAmpGain;
        public Dictionary<int, int> Audion6CompressionThresholds;
        public Dictionary<int, String> Audion6CompressionRatio;
        public Dictionary<int, String> Audion6OutputCompressionLimiter;
        public Dictionary<int, String> Audion6ExpansionRatio;
        public Dictionary<int, String> Audion6ExpansionAttack;
        public Dictionary<int, String> Audion6ExpansionRelease;
        public Dictionary<int, String> Audion6ExpansionThresholds;
        public Dictionary<int, String> Audion6MicrophoneExpansion;
        public Dictionary<int, String> Audion6NoiseReduction;
        public Dictionary<int, int> Audion6MatrixGain;
        public Dictionary<int, int> Audion6BandEqualizationFilter;
        public Dictionary<int, int> Audion6LowCutFilter;
        public Dictionary<int, int> Audion6HighCutFilter;
        public Dictionary<int, String> Audion6AdaptiveFeedbackCanceller;
        public Dictionary<int, String> Audion6NumberOfUserPrograms;
        public Dictionary<int, String> Audion6ToneFrequency;
        public Dictionary<int, String> Audion6ToneLevel;
        public Dictionary<int, String> Audion6ProgramSwitchTones;
        public Dictionary<int, String> Audion6LowBatteryTones;
        public Dictionary<int, String> Audion6PowerOnDelay;
        public Dictionary<int, String> Audion6PowerOnLevel;

        public Dictionary<int, int> Audion4InputMultiplexer;
        public Dictionary<int, int> Audion4PreAmpGain;
        public Dictionary<int, int> Audion4CompressionThresholds;
        public Dictionary<int, string> Audion4CompressionRatio;
        public Dictionary<int, string> Audion4OutputCompressionLimiter;
        public Dictionary<int, int[]> Audion4TimeConstantsBarFarRrr;
        public Dictionary<int, string> Audion4NoiseReduction;
        public Dictionary<int, int> Audion4MatrixGain;
        public Dictionary<int, int> Audion4BandEqualizationFilter;
        public Dictionary<int, int> Audion4LowCutFilter;
        public Dictionary<int, int> Audion4HighCutFilter;
        public Dictionary<int, string> Audion4AdaptiveFeedbackCanceller;
        public Dictionary<int, string> Audion4VolumeControlEnable;
        public Dictionary<int, string> Audion4VolumeControlBeepEnable;
        public Dictionary<int, string> Audion4VolumeControlMode;
        public Dictionary<int, string> Audion4VolumeControlAnalogRange;
        public Dictionary<int, int> Audion4VolumeControlDigitalStepSize;
        public Dictionary<int, int> Audion4VolumeControlDigitalNumberOfSteps;
        public Dictionary<int, string> Audion4NumberOfUserPrograms;
        public Dictionary<int, int> Audion4ToneFrequency;
        public Dictionary<int, string> Audion4PowerOnDelay;
        public Dictionary<int, string> Audion4PowerOnLevel;

        public Dictionary<int, int> SpinNRInputMultiplexer;
        public Dictionary<int, int> SpinNRPreAmpGain;
        public Dictionary<int, int> SpinNRCompressionThresholds;
        public Dictionary<int, string> SpinNRCompressionRatio;
        public Dictionary<int, string> SpinNROutputCompressionLimiter;
        public Dictionary<int, int[]> SpinNRTimeConstantsBarFarRrr;
        public Dictionary<int, string> SpinNRNoiseReduction;
        public Dictionary<int, int> SpinNRMatrixGain;
        public Dictionary<int, int> SpinNRBandEqualizationFilter;
        public Dictionary<int, int> SpinNRLowCutFilter;
        public Dictionary<int, int> SpinNRHighCutFilter;
        public Dictionary<int, string> SpinNRAdaptiveFeedbackCanceller;
        public Dictionary<int, string> SpinNRVolumeControlMode;
        public Dictionary<int, string> SpinNRVolumeControlAnalogRange;
        public Dictionary<int, string> SpinNRNumberOfUserPrograms;
        public Dictionary<int, int> SpinNRToneFrequency;

        public CalibrationDictionaryViewModel()
        {
            Audion16Dictionary();
            Audion8Dictionary();
            Audion6Dictionary();
            Audion4Dictionary();
            SpinNRDictionary();
        }

        public void Audion16Dictionary()
        {
            Audion16InputMultiplexer = new Dictionary<int, int>
            {
                {0, 0},
                {3, 1},
            };

            Audion16AnalogPreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {1, 3},
                {2, 6},
                {3, 9},
                {4, 12},
                {5, 15},
                {6, 18},
                {7, 21},
                {8, 24},
                {9, 27},
                {10, 30},
                {11, 33},
                {12, 36},
            };

            Audion16DigitalPreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {1, 6},
                {2, 12},
                {3, 18},
            };

            Audion16RemoteMixRatio = new Dictionary<int, int>
            {
                {0, 0},
                {1, 3},
                {2, 6},
                {3, 9},
                {4, 12},
            };

            Audion16NoiseReduction = new Dictionary<int, string>
            {
                {0, "Off"},
                {1, "Low"},
                {2, "Medium"},
                {3, "High"},
                {4, "Max"},
            };

            Audion16LowLevelExpansion = new Dictionary<int, string>
            {
                {0, "Off"},
                {1, "On"},
            };

            Audion16WindSuppression = new Dictionary<int, string>
            {
                {0, "Off"},
                {1, "On"},
            };

            Audion16FeedbackCanceller = new Dictionary<int, string>
            {
                {0, "Off"},
                {1, "Normal"},
                {2, "Aggressive"},
                {3, "Auto Aggressive"},
            };

            Audion16MatrixGain = new Dictionary<int, int>
            {
                {0, 0},
                {1, -1},
                {2, -2},
                {3, -3},
                {4, -4},
                {5, -5},
                {6, -6},
                {7, -7},
                {8, -8},
                {9, -9},
                {10, -10},
                {11, -11},
                {12, -12},
                {13, -13},
                {14, -14},
                {15, -15},
                {16, -16},
                {17, -17},
                {18, -18},
                {19, -19},
                {20, -20},
                {21, -21},
                {22, -22},
                {23, -23},
                {24, -24},
                {25, -25},
                {26, -26},
                {27, -27},
                {28, -28},
                {29, -29},
                {30, -30},
                {31, -31},
                {32, -32},
                {33, -33},
                {34, -34},
                {35, -35},
                {36, -36},
                {37, -37},
                {38, -38},
                {39, -39},
                {40, -40},
                {41, -41},
                {42, -42},
                {43, -43},
                {44, -44},
                {45, -45},
                {46, -46},
                {47, -47}
            };

            Audion16BandEqualizationFilter = new Dictionary<int, int>
            {
                {0, -40},
                {1, -38},
                {2, -36},
                {3, -34},
                {4, -32},
                {5, -30},
                {6, -28},
                {7, -26},
                {8, -24},
                {9, -22},
                {10, -20},
                {11, -18},
                {12, -16},
                {13, -14},
                {14, -12},
                {15, -10},
                {16, -8},
                {17, -6},
                {18, -4},
                {19, -2},
                {20, 0},
            };

            Audion16CompressionRatio = new Dictionary<int, String>
            {
                {0, "1.00 : 1"},
                {1, "1.03 : 1"},
                {2, "1.05 : 1"},
                {3, "1.08 : 1"},
                {4, "1.11 : 1"},
                {5, "1.14 : 1"},
                {6, "1.18 : 1"},
                {7, "1.21 : 1"},
                {8, "1.25 : 1"},
                {9, "1.29 : 1"},
                {10, "1.33 : 1"},
                {11, "1.38 : 1"},
                {12, "1.43 : 1"},
                {13, "1.48 : 1"},
                {14, "1.54 : 1"},
                {15, "1.60 : 1"},
                {16, "1.67 : 1"},
                {17, "1.74 : 1"},
                {18, "1.82 : 1"},
                {19, "1.90 : 1"},
                {20, "2.00 : 1"},
                {21, "2.11 : 1"},
                {22, "2.22 : 1"},
                {23, "2.35 : 1"},
                {24, "2.50 : 1"},
                {25, "2.67 : 1"},
                {26, "2.86 : 1"},
                {27, "3.08 : 1"},
                {28, "3.33 : 1"},
                {29, "3.64 : 1"},
                {30, "4.00 : 1"},
                {31, "4.44 : 1"},
                {32, "5.00 : 1"},
                {33, "5.71 : 1"},
                {34, "6.67 : 1"},
                {35, "8.00 : 1"}
            };

            Audion16CompressionThresholds = new Dictionary<int, int>
            {
                {0, 20},
                {1, 22},
                {2, 24},
                {3, 26},
                {4, 28},
                {5, 30},
                {6, 32},
                {7, 34},
                {8, 36},
                {9, 38},
                {10, 40},
                {11, 42},
                {12, 44},
                {13, 46},
                {14, 48},
                {15, 50},
                {16, 52},
                {17, 54},
                {18, 56},
                {19, 58},
                {20, 60},
                {21, 62},
                {22, 64},
                {23, 66},
                {24, 68},
                {25, 70},
                {26, 72},
                {27, 74},
                {28, 76},
                {29, 78},
                {30, 80},
                {31, 82},
            };

            Audion16OutputCompressionLimiter = new Dictionary<int, String>
            {
                {0, "Off"},
                {1, "Max Undistorted Output (MUO)"},
                {2, "-2 dB"},
                {3, "-4 dB"},
                {4, "-6 dB"},
                {5, "-8 dB"},
                {6, "-10 dB"},
                {7, "-12 dB"},
                {8, "-14 dB"},
                {9, "-16 dB"},
                {10, "-18 dB"},
                {11, "-20 dB"},
                {12, "-22 dB"},
                {13, "-24 dB"},
                {14, "-26 dB"},
                {15, "-28 dB"},
            };

            Audion16LowCutFilter = new Dictionary<int, string>
            {
                {0, "Off"},
                {1, "250 Hz"},
                {2, "500 Hz"},
                {3, "750 Hz"},
                {4, "1000 Hz"},
                {5, "1250 Hz"},
                {6, "1500 Hz"},
                {7, "2000 Hz"},
                {8, "2500Hz"},
                {9, "3000 Hz"},
            };

            Audion16VolumeControlMode = new Dictionary<int, String>
            {
                {0, "Analog VC Enabled"},
                {1, "Digital VC Enabled"},
                {2, "Multi - function Rocker"},
                {3, "Single push button VC"},
            };

            Audion16VolumeControlAnalogRange = new Dictionary<int, String>
            {
                {0, "Linear mode(50dB Range)"},
                {1, "Linear mode(40dB Range)"},
                {2, "Linear mode(30dB Range)"},
                {3, "Linear mode(20dB Range)"},
                {4, "Linear mode(10dB Range)"},
            };

            Audion16VolumeControlDigitalStepSize = new Dictionary<int, int>
            {
                {0, 1},
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 5},
                {5, 6},
            };

            Audion16VolumeControlDigitalNumberOfSteps = new Dictionary<int, int>
            {
                {0, 5},
                {1, 10},
                {2, 15},
                {3, 20},
                {4, 25},
                {5, 30},
            };

            Audion16NumberOfUserPrograms = new Dictionary<int, String>
            {
                {0, "1 program"},
                {1, "2 programs"},
                {2, "3 programs"},
                {3, "4 programs"},
                {4, "5 programs"},
                {5, "6 programs"},
            };

            Audion16PowerOnProgram = new Dictionary<int, String>
            {
                {0, "Program 1"},
                {1, "Program 2"},
                {2, "Program 3"},
                {3, "Program 4"},
                {4, "Program 5"},
                {5, "Program 6"},
            };

            Audion16ToneFrequency = new Dictionary<int, String>
            {
                {0, "500 Hz"},
                {1, "1000 Hz"},
                {2, "1500 Hz"},
                {3, "2000 HZ"},
            };

            Audion16PromptLevel = new Dictionary<int, String>
            {
                {0, "70 dB SPL"},
                {1, "75 dB SPL"},
                {2, "80 dB SPL"},
                {3, "85 dB SPL"},
                {4, "90 dB SPL"},
                {5, "95 dB SPL"},
                {6, "100 dB SPL"},
            };

            Audion16ProgramPromptMode = new Dictionary<int, String>
            {
                {0, "Off"},
                {1, "Tones"},
                {2, "Voice Prompts"},
            };

            Audion16WarningPromptMode = new Dictionary<int, String>
            {
                {0, "Off"},
                {1, "Tones"},
                {2, "Voice Prompts"},
            };

            Audion16PowerOnDelay = new Dictionary<int, String>
            {
                {0, "None"},
                {1, "5 seg"},
                {2, "10 seg"},
                {3, "15 seg"},
            };

            Audion16PowerOnLevel = new Dictionary<int, String>
            {
                {0, "Mute"},
                {1, "-30 dB"},
                {2, "-20 dB"},
                {3, "-10 dB"},
            };

            Audion16AdaptiveDirectionalSensitivity = new Dictionary<int, String>
            {
                {0, "Low"},
                {1, "Mid"},
                {2, "High"},
                {3, "Highest"},
            };

            Audion16DigitalNoiseGeneratorAmplitude = new Dictionary<int, String>
            {
                {0, "30 dB SPL"},
                {1, "35 dB SPL"},
                {2, "40 dB SPL"},
                {3, "45 dB SPL"},
                {4, "50 dB SPL"},
                {5, "55 dB SPL"},
                {6, "60 dB SPL"},
                {7, "65 dB SPL"},
            };
        }

        public void Audion8Dictionary()
        {
            Audion8InputMultiplexer = new Dictionary<int, int>
            {
                {0, 0},
                {3, 1},
            };

            Audion8PreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {1, 12},
                {2, 15},
                {3, 18},
                {4, 21},
                {5, 24},
                {6, 27},
                {7, 30},
            };

            Audion8CompressionThresholds = new Dictionary<int, int>
            {
                {0, 40},
                {1, 45},
                {2, 50},
                {3, 55},
                {4, 60},
                {5, 65},
                {6, 70},
                {7, 75},
            };

            Audion8CompressionRatio = new Dictionary<int, string>
            {
                {0, "1 : 1"},
                {1, "1.05 : 1"},
                {2, "1.11 : 1"},
                {3, "1.18 : 1"},
                {4, "1.25 : 1"},
                {5, "1.33 : 1"},
                {6, "1.43 : 1"},
                {7, "1.54 : 1"},
                {8, "1.67 : 1"},
                {9, "1.82 : 1"},
                {10, "2 : 1"},
                {11, "2.22 : 1"},
                {12, "2.5 : 1"},
                {13, "2.86 : 1"},
                {14, "3.33 : 1"},
                {15, "4 : 1"},
            };

            Audion8OutputCompressionLimiter = new Dictionary<int, string>
            {
                {0, "Desligado"},
                {1, "Saída Máxima Sem Distorção"},
                {2, "-2 dB"},
                {3, "-4 dB"},
                {4, "-6 dB"},
                {5, "-8 dB"},
                {6, "-10 dB"},
                {7, "-12 dB"},
                {8, "-14 dB"},
                {9, "-16 dB"},
                {10, "-18 dB"},
                {11, "-20 dB"},
            };

            Audion8NoiseReduction = new Dictionary<int, string>
            {
                {0, "Desligado"},
                {1, "Baixo (7 dB)"},
                {2, "Médio (10 dB)"},
                {3, "Alto (13 dB)"},
                {4, "Máximo (17 dB)"},
            };

            Audion8MatrixGain = new Dictionary<int, int>
            {
                {0, 0},
                {1, -1},
                {2, -2},
                {3, -3},
                {4, -4},
                {5, -5},
                {6, -6},
                {7, -7},
                {8, -8},
                {9, -9},
                {10, -10},
                {11, -11},
                {12, -12},
                {13, -13},
                {14, -14},
                {15, -15},
                {16, -16},
                {17, -17},
                {18, -18},
                {19, -19},
                {20, -20},
                {21, -21},
                {22, -22},
                {23, -23},
                {24, -24},
                {25, -25},
                {26, -26},
                {27, -27},
                {28, -28},
                {29, -29},
                {30, -30},
                {31, -31},
                {32, -32},
                {33, -33},
                {34, -34},
                {35, -35},
                {36, -36},
                {37, -37},
                {38, -38},
                {39, -39},
                {40, -40},
                {41, -41},
                {42, -42},
                {43, -43},
                {44, -44},
                {45, -45},
                {46, -46},
                {47, -47},
            };

            Audion8BandEqualizationFilter = new Dictionary<int, int>
            {
                {0, -40},
                {1, -38},
                {2, -36},
                {3, -34},
                {4, -32},
                {5, -30},
                {6, -28},
                {7, -26},
                {8, -24},
                {9, -22},
                {10, -20},
                {11, -18},
                {12, -16},
                {13, -14},
                {14, -12},
                {15, -10},
                {16, -8},
                {17, -6},
                {18, -4},
                {19, -2},
                {20, 0},
            };

            Audion8AdaptiveFeedbackCanceller = new Dictionary<int, string>
            {
                {0, "Desligado"},
                {1, "Ligado"},
            };

            Audion8VolumeControlMode = new Dictionary<int, string>
            {
                {0, "Analog VC Enabled"},
                {1, "Digital VC Enabled"},
                {2, "Multi - function Rocker"},
                {3, "Single push button VC"},
            };

            Audion8VolumeControlAnalogRange = new Dictionary<int, string>
            {
                {0, "Linear mode(50dB Range)"},
                {1, "Linear mode(40dB Range)"},
                {2, "Linear mode(30dB Range)"},
                {3, "Linear mode(20dB Range)"},
                {4, "Linear mode(10dB Range)"},
            };

            Audion8VolumeControlDigitalStepSize = new Dictionary<int, int>
            {
                {0, 1},
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 5},
                {5, 6},
            };

            Audion8VolumeControlDigitalNumberOfSteps = new Dictionary<int, int>
            {
                {0, 5},
                {1, 10},
                {2, 15},
                {3, 20},
                {4, 25},
                {5, 30},
            };

            Audion8NumberOfUserPrograms = new Dictionary<int, string>
            {
                {0, "1 program"},
                {1, "2 programs"},
                {2, "3 programs"},
                {3, "4 programs"},
                {4, "AutoTelecoil Program"},
            };

            Audion8ToneFrequency = new Dictionary<int, int>
            {
                {0, 500},
                {1, 1000},
                {2, 1500},
                {3, 2000},
            };

            Audion8ProgramPromptMode = new Dictionary<int, string>
            {
                {0, "Desligado"},
                {1, "Beeps"},
                {2, "Voz"},
            };

            Audion8LowBatteryPromptMode = new Dictionary<int, string>
            {
                {0, "Desligado"},
                {1, "Beeps"},
                {2, "Voz"},
            };

            Audion8PromptLevelIndex = new Dictionary<int, string>
            {
                {0, "-30 dB"},
                {1, "-25 dB"},
                {2, "-20 dB"},
                {3, "-15 dB"},
                {4, "-10 dB"},
                {5, "-5 dB"},
                {6, "0 dB (padrão)"},
            };

            Audion8AutoSave = new Dictionary<int, string>
            {
                {0, "Use VC_Startup and PGM_Startup parameters when powering up"},
                {1, "Use last in use VC position and program when re-powering aid"},
            };

            Audion8PowerOnDelay = new Dictionary<int, string>
            {
                {0, "3 seg"},
                {1, "5 seg"},
                {2, "10 seg"},
                {3, "15 seg"},
            };

            Audion8PowerOnLevel = new Dictionary<int, string>
            {
                {0, "Mutar"},
                {1, "-10 dB"},
                {2, "-20 dB"},
                {3, "-30 dB"},
            };

            Audion8AdaptiveDirectionalSensitivity = new Dictionary<int, string>
            {
                {0, "Baixo"},
                {1, "Médio"},
                {2, "Alto"},
                {3, "Máximo"},
            };

            Audion8DigitalNoiseGeneratorAmplitude = new Dictionary<int, string>
            {
                {0, "30 dB SPL"},
                {1, "35 dB SPL"},
                {2, "40 dB SPL"},
                {3, "45 dB SPL"},
                {4, "50 dB SPL"},
                {5, "55 dB SPL"},
                {6, "60 dB SPL"},
                {7, "65 dB SPL"},
            };
        }

        public void Audion6Dictionary()
        {
            Audion6InputMultiplexer = new Dictionary<int, int>
            {
                {1, 0},
                {3, 1},
            };

            Audion6PreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {1, 12},
                {2, 15},
                {3, 18},
                {4, 21},
                {5, 24},
                {6, 27},
                {7, 30},
            };

            Audion6CompressionThresholds = new Dictionary<int, int>
            {
                {0, 34},
                {1, 36},
                {2, 38},
                {3, 40},
                {4, 42},
                {5, 44},
                {6, 46},
                {7, 48},
                {8, 50},
                {9, 52},
                {10, 54},
                {11, 56},
                {12, 58},
                {13, 60},
                {14, 62},
                {15, 64}
            };

            Audion6CompressionRatio = new Dictionary<int, string>
            {
                {0, "1 : 1"},
                {1, "1.05 : 1"},
                {2, "1.11 : 1"},
                {3, "1.18 : 1"},
                {4, "1.25 : 1"},
                {5, "1.33 : 1"},
                {6, "1.43 : 1"},
                {7, "1.54 : 1"},
                {8, "1.67 : 1"},
                {9, "1.82 : 1"},
                {10, "2 : 1"},
                {11, "2.22 : 1"},
                {12, "2.5 : 1"},
                {13, "2.86 : 1"},
                {14, "3.33 : 1"},
                {15, "4 : 1"}
            };

            Audion6OutputCompressionLimiter = new Dictionary<int, string>
            {
                {0, "Desligado"},
                {1, "Saída Máxima Sem Distorção"},
                {2, "-2 dB"},
                {3, "-4 dB"},
                {4, "-6 dB"},
                {5, "-8 dB"},
                {6, "-10 dB"},
                {7, "-12 dB"},
                {8, "-14 dB"},
                {9, "-16 dB"},
                {10, "-18 dB"},
                {11, "-20 dB"}
            };

            Audion8NoiseReduction = new Dictionary<int, string>
            {
                {0, "Off"},
                {1, "Low (7 dB)"},
                {2, "Medium (10 dB)"},
                {3, "High (13 dB)"},
                {4, "Highest (17 dB)"},
            };

            Audion6ExpansionRatio = new Dictionary<int, string>
            {
                {0, "1.25 : 1"},
                {7, "1.50 : 1"},
                {9, "1.75 : 1"},
                {10, "2 : 1"}
            };

            Audion6ExpansionAttack = new Dictionary<int, string>
            {
                {0, "3 ms"},
                {1, "6 ms"},
                {2, "12 ms"},
                {3, "24 ms"},
                {4, "48 ms"},
                {5, "96 ms"},
                {6, "192 ms"},
                {7, "384 ms"}
            };

            Audion6ExpansionRelease = new Dictionary<int, string>
            {
                {0, "30 ms"},
                {1, "60 ms"},
                {2, "120 ms"},
                {3, "240 ms"},
                {4, "480 ms"},
                {5, "960 ms"},
                {6, "1920 ms"},
                {7, "3840 ms"}
            };

            Audion6ExpansionThresholds = new Dictionary<int, string>
            {
                {0, "Desligado"},
                {1, "30 dB SPL"},
                {2, "32 dB SPL"},
                {3, "34 dB SPL"},
                {4, "36 dB SPL"},
                {5, "38 dB SPL"},
                {6, "40 dB SPL"},
                {7, "42 dB SPL"},
                {8, "44 dB SPL"},
                {9, "46 dB SPL"},
                {10, "48 dB SPL"},
                {11, "50 dB SPL"},
                {12, "52 dB SPL"},
                {13, "54 dB SPL"},
                {14, "56 dB SPL"},
                {15, "58 dB SPL"}
            };

            Audion6MatrixGain = new Dictionary<int, int>
            {
                {0, 0},
                {1, -1},
                {2, -2},
                {3, -3},
                {4, -4},
                {5, -5},
                {6, -6},
                {7, -7},
                {8, -8},
                {9, -9},
                {10, -10},
                {11, -11},
                {12, -12},
                {13, -13},
                {14, -14},
                {15, -15},
                {16, -16},
                {17, -17},
                {18, -18},
                {19, -19},
                {20, -20},
                {21, -21},
                {22, -22},
                {23, -23},
                {24, -24},
                {25, -25},
                {26, -26},
                {27, -27},
                {28, -28},
                {29, -29},
                {30, -30},
                {31, -31},
                {32, -32},
                {33, -33},
                {34, -34},
                {35, -35},
                {36, -36},
                {37, -37},
                {38, -38},
                {39, -39},
                {40, -40},
                {41, -41},
                {42, -42},
                {43, -43},
                {44, -44},
                {45, -45},
                {46, -46},
                {47, -47},
            };

            Audion6BandEqualizationFilter = new Dictionary<int, int>
            {
                {0, -40},
                {1, -38},
                {2, -36},
                {3, -34},
                {4, -32},
                {5, -30},
                {6, -28},
                {7, -26},
                {8, -24},
                {9, -22},
                {10, -20},
                {11, -18},
                {12, -16},
                {13, -14},
                {14, -12},
                {15, -10},
                {16, -8},
                {17, -6},
                {18, -4},
                {19, -2},
                {20, 0}
            };

            Audion6AdaptiveFeedbackCanceller = new Dictionary<int, string>
            {
                {0, "Desligado"},
                {1, "Ligado"},
            };
        }

        public void Audion4Dictionary()
        {
            Audion4InputMultiplexer = new Dictionary<int, int>
            {
                {1, 0},
                {3, 1},
            };

            Audion4PreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {1, 12},
                {2, 15},
                {3, 18},
                {4, 21},
                {5, 24},
                {6, 27},
                {7, 30},
            };

            Audion4CompressionThresholds = new Dictionary<int, int>
            {
                {0, 40},
                {1, 45},
                {2, 50},
                {3, 55},
                {4, 60},
                {5, 65},
                {6, 70},
            };

            Audion4CompressionRatio = new Dictionary<int, string>
            {
                {0, "1 : 1"},
                {1, "1.14 : 1"},
                {2, "1.33 : 1"},
                {3, "1.6 : 1"},
                {4, "2 : 1"},
                {5, "2.56 : 1"},
                {6, "4 : 1"},
            };

            Audion4OutputCompressionLimiter = new Dictionary<int, string>
            {
                {0, "Max undistroted output"},
                {1, "-4 dB"},
                {2, "-8 dB"},
                {3, "-12 dB"},
                {4, "-16 dB"},
                {5, "-20 dB"},
                {6, "-24 dB"},
            };

            Audion4NoiseReduction = new Dictionary<int, string>
            {
                {0, "Off"},
                {1, "Low (7 dB)"},
                {2, "Medium (10 dB)"},
                {3, "High (13 dB)"},
            };

            Audion4MatrixGain = new Dictionary<int, int>
    {
        {0, 0},
        {1, -1},
        {2, -2},
        {3, -3},
        {4, -4},
        {5, -5},
        {6, -6},
        {7, -7},
        {8, -8},
        {9, -9},
        {10, -10},
        {11, -11},
        {12, -12},
        {13, -13},
        {14, -14},
        {15, -15},
        {16, -16},
        {17, -17},
        {18, -18},
        {19, -19},
        {20, -20},
        {21, -21},
        {22, -22},
        {23, -23},
        {24, -24},
        {25, -25},
        {26, -26},
        {27, -27},
        {28, -28},
        {29, -29},
        {30, -30},
        {31, -31},
        {32, -32},
        {33, -33},
        {34, -34},
        {35, -35},
        {36, -36},
        {37, -37},
        {38, -38},
        {39, -39},
        {40, -40},
        {41, -41},
        {42, -42},
        {43, -43},
        {44, -44},
        {45, -45},
        {46, -46},
        {47, -47},
    };

            Audion4BandEqualizationFilter = new Dictionary<int, int>
    {
        {0, -30},
        {1, -28},
        {2, -26},
        {3, -24},
        {4, -22},
        {5, -20},
        {6, -18},
        {7, -16},
        {8, -14},
        {9, -12},
        {10, -10},
        {11, -8},
        {12, -6},
        {13, -4},
        {14, -2},
        {15, 0},
    };

            Audion4LowCutFilter = new Dictionary<int, int>
    {
        {0, 200},
        {1, 500},
        {2, 750},
        {3, 1000},
        {4, 1500},
        {5, 2000},
        {6, 3000},
    };

            Audion4HighCutFilter = new Dictionary<int, int>
    {
        {0, 8000},
        {1, 4000},
        {2, 3150},
        {3, 2500},
        {4, 2000},
        {5, 1600},
        {6, 1250},
    };

            Audion4AdaptiveFeedbackCanceller = new Dictionary<int, string>
    {
        {0, "Feedback Control off"},
        {1, "Feedback Control on"},
    };

            Audion4VolumeControlEnable = new Dictionary<int, string>
    {
        {0, "Disabled"},
        {1, "Enabled"},
    };

            Audion4VolumeControlBeepEnable = new Dictionary<int, string>
    {
        {0, "Disabled"},
        {1, "Enabled"},
    };

            Audion4VolumeControlMode = new Dictionary<int, string>
    {
        {0, "Analog VC Enabled"},
        {1, "Digital VC Enabled"},
        {2, "Rocker Control"},
    };

            Audion4VolumeControlAnalogRange = new Dictionary<int, string>
    {
        {0, "Linear mode(50dB Range)"},
        {1, "Linear mode(40dB Range)"},
        {2, "Linear mode(30dB Range)"},
        {3, "Linear mode(20dB Range)"},
        {4, "Linear mode(10dB Range)"},
    };

            Audion4VolumeControlDigitalStepSize = new Dictionary<int, int>
    {
        {0, 1},
        {1, 2},
        {2, 3},
        {3, 4},
        {4, 5},
        {5, 6},
    };

            Audion4VolumeControlDigitalNumberOfSteps = new Dictionary<int, int>
    {
        {0, 5},
        {1, 10},
        {2, 15},
        {3, 20},
        {4, 25},
        {5, 30},
    };

            Audion4NumberOfUserPrograms = new Dictionary<int, string>
    {
        {0, "1 program"},
        {1, "2 programs"},
        {2, "3 programs"},
        {3, "4 programs"},
    };

            Audion4ToneFrequency = new Dictionary<int, int>
    {
        {0, 500},
        {1, 1000},
        {2, 1500},
        {3, 2000},
    };

            Audion4PowerOnDelay = new Dictionary<int, string>
    {
        {0, "3 seg"},
        {1, "5 seg"},
        {2, "10 seg"},
        {3, "15 seg"},
    };

            Audion4PowerOnLevel = new Dictionary<int, string>
    {
        {0, "-60 dB (Mute)"},
        {1, "-30 dB"},
        {2, "-20 dB"},
        {3, "-10 dB"},
    };
        }

        public void SpinNRDictionary()
        {
            SpinNRInputMultiplexer = new Dictionary<int, int>
            {
                {1, 0},
                {3, 1},
            };

            SpinNRPreAmpGain = new Dictionary<int, int>
    {
        {0, 0},
        {1, 12},
        {2, 15},
        {3, 18},
        {4, 21},
        {5, 24},
        {6, 27},
        {7, 30},
    };

            SpinNRCompressionThresholds = new Dictionary<int, int>
    {
        {0, 45},
        {1, 50},
        {2, 55},
        {3, 60},
        {4, 65},
        {5, 70},
        {6, 75},
    };

            SpinNRCompressionRatio = new Dictionary<int, string>
    {
        {0, "1 : 1"},
        {1, "1.14 : 1"},
        {2, "1.33 : 1"},
        {3, "1.6 : 1"},
        {4, "2 : 1"},
        {5, "2.65 : 1"},
        {6, "4 : 1"},
    };

            SpinNROutputCompressionLimiter = new Dictionary<int, string>
    {
        {0, "Max undistroted output"},
        {1, "-4 dB"},
        {2, "-8 dB"},
        {3, "-12 dB"},
        {4, "-16 dB"},
        {5, "-20 dB"},
        {6, "-24 dB"},
    };

            SpinNRNoiseReduction = new Dictionary<int, string>
    {
        {0, "Off"},
        {1, "Low (7 dB)"},
        {2, "Medium (10 dB)"},
        {3, "High (13 dB)"},
    };

            SpinNRMatrixGain = new Dictionary<int, int>
    {
        {0, 0},
        {1, -1},
        {2, -2},
        {3, -3},
        {4, -4},
        {5, -5},
        {6, -6},
        {7, -7},
        {8, -8},
        {9, -9},
        {10, -10},
        {11, -11},
        {12, -12},
        {13, -13},
        {14, -14},
        {15, -15},
        {16, -16},
        {17, -17},
        {18, -18},
        {19, -19},
        {20, -20},
        {21, -21},
        {22, -22},
        {23, -23},
        {24, -24},
        {25, -25},
        {26, -26},
        {27, -27},
        {28, -28},
        {29, -29},
        {30, -30},
        {31, -31},
        {32, -32},
        {33, -33},
        {34, -34},
        {35, -35},
        {36, -36},
        {37, -37},
        {38, -38},
        {39, -39},
        {40, -40},
        {41, -41},
        {42, -42},
        {43, -43},
        {44, -44},
        {45, -45},
        {46, -46},
        {47, -47},
    };

            SpinNRBandEqualizationFilter = new Dictionary<int, int>
    {
        {0, -30},
        {1, -28},
        {2, -26},
        {3, -24},
        {4, -22},
        {5, -20},
        {6, -18},
        {7, -16},
        {8, -14},
        {9, -12},
        {10, -10},
        {11, -8},
        {12, -6},
        {13, -4},
        {14, -2},
        {15, 0},
    };

            SpinNRLowCutFilter = new Dictionary<int, int>
    {
        {0, 200},
        {1, 500},
        {2, 750},
        {3, 1000},
        {4, 1500},
        {5, 2000},
        {6, 3000},
    };

            SpinNRHighCutFilter = new Dictionary<int, int>
    {
        {0, 8000},
        {1, 4000},
        {2, 3150},
        {3, 2500},
        {4, 2000},
        {5, 1600},
        {6, 1250},
    };

            SpinNRAdaptiveFeedbackCanceller = new Dictionary<int, string>
    {
        {0, "Feedback Control off"},
        {1, "Feedback Control on"},
    };

            SpinNRVolumeControlMode = new Dictionary<int, string>
    {
        {0, "Analog VC Enabled"},
        {1, "Digital VC Enabled"},
    };

            SpinNRVolumeControlAnalogRange = new Dictionary<int, string>
    {
        {0, "Linear mode(50dB Range)"},
        {1, "Linear mode(40dB Range)"},
        {2, "Linear mode(30dB Range)"},
        {3, "Linear mode(20dB Range)"},
        {4, "Linear mode(10dB Range)"},
    };

            SpinNRNumberOfUserPrograms = new Dictionary<int, string>
    {
        {0, "1 program"},
        {1, "2 programs"},
        {2, "3 programs"},
        {3, "4 programs"},
        {4, "5 programs"},
    };

            SpinNRToneFrequency = new Dictionary<int, int>
    {
        {0, 500},
        {1, 1000},
        {2, 1500},
        {3, 2000},
    };
        }
    }
}