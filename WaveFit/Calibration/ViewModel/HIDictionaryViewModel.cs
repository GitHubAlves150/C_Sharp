using System;
using System.Collections.Generic;

namespace WaveFit2.Calibration.ViewModel
{
    public class HIDictionaryViewModel
    {
        public Dictionary<String, int> Audion16InputMultiplexer;
        public Dictionary<int, int> Audion16AnalogPreAmpGain;
        public Dictionary<int, int> Audion16DigitalPreAmpGain;
        public Dictionary<int, int> Audion16RemoteMixRatio;
        public Dictionary<int, int> Audion16CompressionThresholds;
        public Dictionary<String, int> Audion16CompressionRatio;
        public Dictionary<String, int> Audion16OutputCompressionLimiter;
        public Dictionary<String, int> Audion16LowCutFilter;
        public Dictionary<String, int> Audion16LowLevelExpansion;
        public Dictionary<String, int> Audion16WindSuppression;
        public Dictionary<String, int> Audion16FeedbackCanceller;
        public Dictionary<int, int[]> Audion16TimeConstantsBarFarRrr;
        public Dictionary<String, int> Audion16NoiseReduction;
        public Dictionary<int, int> Audion16MatrixGain;
        public Dictionary<int, int> Audion16BandEqualizationFilter;
        public Dictionary<String, int> Audion16AdaptiveFeedbackCanceller;
        public Dictionary<String, int> Audion16VolumeControlMode;
        public Dictionary<String, int> Audion16VolumeControlAnalogRange;
        public Dictionary<int, int> Audion16VolumeControlDigitalStepSize;
        public Dictionary<int, int> Audion16VolumeControlDigitalNumberOfSteps;
        public Dictionary<String, int> Audion16NumberOfUserPrograms;
        public Dictionary<String, int> Audion16PowerOnProgram;
        public Dictionary<String, int> Audion16ToneFrequency;
        public Dictionary<String, int> Audion16PromptLevel;
        public Dictionary<String, int> Audion16ProgramPromptMode;
        public Dictionary<String, int> Audion16WarningPromptMode;
        public Dictionary<String, int> Audion16AutoSave;
        public Dictionary<String, int> Audion16PowerOnDelay;
        public Dictionary<String, int> Audion16PowerOnLevel;
        public Dictionary<String, int> Audion16AdaptiveDirectionalSensitivity;
        public Dictionary<String, int> Audion16DigitalNoiseGeneratorAmplitude;

        public Dictionary<String, int> Audion8InputMultiplexer;
        public Dictionary<int, int> Audion8PreAmpGain;
        public Dictionary<int, int> Audion8CompressionThresholds;
        public Dictionary<String, int> Audion8CompressionRatio;
        public Dictionary<String, int> Audion8OutputCompressionLimiter;
        public Dictionary<int, int[]> Audion8TimeConstantsBarFarRrr;
        public Dictionary<String, int> Audion8NoiseReduction;
        public Dictionary<int, int> Audion8MatrixGain;
        public Dictionary<int, int> Audion8BandEqualizationFilter;
        public Dictionary<String, int> Audion8AdaptiveFeedbackCanceller;
        public Dictionary<String, int> Audion8VolumeControlMode;
        public Dictionary<String, int> Audion8VolumeControlAnalogRange;
        public Dictionary<int, int> Audion8VolumeControlDigitalStepSize;
        public Dictionary<int, int> Audion8VolumeControlDigitalNumberOfSteps;
        public Dictionary<String, int> Audion8NumberOfUserPrograms;
        public Dictionary<String, int> Audion8ToneFrequency;
        public Dictionary<String, int> Audion8PromptLevelIndexTL;
        public Dictionary<String, int> Audion8PromptLevelIndexVP;
        public Dictionary<String, int> Audion8ProgramPromptMode;
        public Dictionary<String, int> Audion8LowBatteryPromptMode;
        public Dictionary<String, int> Audion8AutoSave;
        public Dictionary<String, int> Audion8PowerOnDelay;
        public Dictionary<String, int> Audion8PowerOnLevel;
        public Dictionary<String, int> Audion8AdaptiveDirectionalSensitivity;
        public Dictionary<String, int> Audion8DigitalNoiseGeneratorAmplitude;

        public Dictionary<String, int> Audion6InputMultiplexer;
        public Dictionary<int, int> Audion6PreAmpGain;
        public Dictionary<int, int> Audion6CompressionThresholds;
        public Dictionary<String, int> Audion6CompressionRatio;
        public Dictionary<String, int> Audion6OutputCompressionLimiter;
        public Dictionary<String, int> Audion6ExpansionRatio;
        public Dictionary<String, int> Audion6ExpansionAttack;
        public Dictionary<String, int> Audion6ExpansionRelease;
        public Dictionary<String, int> Audion6ExpansionThresholds;
        public Dictionary<String, int> Audion6MicrophoneExpansion;
        public Dictionary<int, int[]> Audion6TimeConstantsBarFarRrr;
        public Dictionary<String, int> Audion6NoiseReduction;
        public Dictionary<int, int> Audion6MatrixGain;
        public Dictionary<int, int> Audion6BandEqualizationFilter;
        public Dictionary<int, int> Audion6LowCutFilter;
        public Dictionary<int, int> Audion6HighCutFilter;
        public Dictionary<String, int> Audion6AdaptiveFeedbackCanceller;
        public Dictionary<String, int> Audion6NumberOfUserPrograms;
        public Dictionary<String, int> Audion6ToneFrequency;
        public Dictionary<String, int> Audion6ToneLevel;
        public Dictionary<String, int> Audion6ProgramSwitchTones;
        public Dictionary<String, int> Audion6LowBatteryTones;
        public Dictionary<String, int> Audion6PowerOnDelay;
        public Dictionary<String, int> Audion6PowerOnLevel;

        public Dictionary<String, int> Audion4InputMultiplexer;
        public Dictionary<int, int> Audion4PreAmpGain;
        public Dictionary<int, int> Audion4CompressionThresholds;
        public Dictionary<String, int> Audion4CompressionRatio;
        public Dictionary<String, int> Audion4OutputCompressionLimiter;
        public Dictionary<String, int> Audion4MicrophoneExpansion;
        public Dictionary<int, int[]> Audion4TimeConstantsBarFarRrr;
        public Dictionary<String, int> Audion4NoiseReduction;
        public Dictionary<int, int> Audion4MatrixGain;
        public Dictionary<int, int> Audion4BandEqualizationFilter;
        public Dictionary<int, int> Audion4LowCutFilter;
        public Dictionary<int, int> Audion4HighCutFilter;
        public Dictionary<String, int> Audion4AdaptiveFeedbackCanceller;
        public Dictionary<String, int> Audion4VolumeControlEnable;
        public Dictionary<String, int> Audion4VolumeControlBeepEnable;
        public Dictionary<String, int> Audion4VolumeControlMode;
        public Dictionary<String, int> Audion4VolumeControlAnalogRange;
        public Dictionary<int, int> Audion4VolumeControlDigitalStepSize;
        public Dictionary<int, int> Audion4VolumeControlDigitalNumberOfSteps;
        public Dictionary<String, int> Audion4NumberOfUserPrograms;
        public Dictionary<String, int> Audion4ToneFrequency;
        public Dictionary<String, int> Audion4ToneLevel;
        public Dictionary<String, int> Audion4ProgramSwitchTones;
        public Dictionary<String, int> Audion4LowBatteryTones;
        public Dictionary<String, int> Audion4PowerOnDelay;
        public Dictionary<String, int> Audion4PowerOnLevel;

        public Dictionary<String, int> SpinNRInputMultiplexer;
        public Dictionary<int, int> SpinNRPreAmpGain;
        public Dictionary<int, int> SpinNRCompressionThresholds;
        public Dictionary<String, int> SpinNRCompressionRatio;
        public Dictionary<String, int> SpinNROutputCompressionLimiter;
        public Dictionary<String, int> SpinNRMicrophoneExpansion;
        public Dictionary<int, int[]> SpinNRTimeConstantsBarFarRrr;
        public Dictionary<String, int> SpinNRNoiseReduction;
        public Dictionary<int, int> SpinNRMatrixGain;
        public Dictionary<int, int> SpinNRBandEqualizationFilter;
        public Dictionary<int, int> SpinNRLowCutFilter;
        public Dictionary<int, int> SpinNRHighCutFilter;
        public Dictionary<String, int> SpinNRAdaptiveFeedbackCanceller;
        public Dictionary<String, int> SpinNRVolumeControlMode;
        public Dictionary<String, int> SpinNRVolumeControlAnalogRange;
        public Dictionary<String, int> SpinNRVolumeControlRockerRange;
        public Dictionary<String, int> SpinNRNumberOfUserPrograms;
        public Dictionary<String, int> SpinNRToneFrequency;
        public Dictionary<String, int> SpinNRToneLevel;
        public Dictionary<String, int> SpinNRProgramSwitchTones;
        public Dictionary<String, int> SpinNRLowBatteryTones;

        public HIDictionaryViewModel()
        {
            Audion16Dictionary();
            Audion8Dictionary();
            Audion6Dictionary();
            Audion4Dictionary();
            SpinNRDictionary();
        }

        public void Audion16Dictionary()
        {
            Audion16InputMultiplexer = new Dictionary<String, int>
            {
                {"Microfone 1", 0},
                {"Microfone 2", 3},
            };

            Audion16AnalogPreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {3, 1},
                {6, 2},
                {9, 3},
                {12, 4},
                {15, 5},
                {18, 6},
                {21, 7},
                {24, 8},
                {27, 9},
                {30, 10},
                {33, 11},
                {36, 12},
            };

            Audion16DigitalPreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {6, 1},
                {12, 2},
                {18, 3},
            };

            Audion16RemoteMixRatio = new Dictionary<int, int>
            {
                {0, 0},
                {3, 1},
                {6, 2},
                {9, 3},
                {12, 4},
            };

            Audion16NoiseReduction = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Baixa", 1},
                {"Média", 2},
                {"Alta", 3},
                {"Maxima", 4},
            };

            Audion16LowLevelExpansion = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion16WindSuppression = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion16FeedbackCanceller = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Normal", 1},
                {"Agressiva", 2},
                {"Auto Agressiva", 3},
            };

            Audion16MatrixGain = new Dictionary<int, int>
            {
                {0, 0},
                {-1, 1},
                {-2, 2},
                {-3, 3},
                {-4, 4},
                {-5, 5},
                {-6, 6},
                {-7, 7},
                {-8, 8},
                {-9, 9},
                {-10, 10},
                {-11, 11},
                {-12, 12},
                {-13, 13},
                {-14, 14},
                {-15, 15},
                {-16, 16},
                {-17, 17},
                {-18, 18},
                {-19, 19},
                {-20, 20},
                {-21, 21},
                {-22, 22},
                {-23, 23},
                {-24, 24},
                {-25, 25},
                {-26, 26},
                {-27, 27},
                {-28, 28},
                {-29, 29},
                {-30, 30},
                {-31, 31},
                {-32, 32},
                {-33, 33},
                {-34, 34},
                {-35, 35},
                {-36, 36},
                {-37, 37},
                {-38, 38},
                {-39, 39},
                {-40, 40},
                {-41, 41},
                {-42, 42},
                {-43, 43},
                {-44, 44},
                {-45, 45},
                {-46, 46},
                {-47, 47},
            };

            Audion16BandEqualizationFilter = new Dictionary<int, int>
            {
                {-40, 0},
                {-38, 1},
                {-36, 2},
                {-34, 3},
                {-32, 4},
                {-30, 5},
                {-28, 6},
                {-26, 7},
                {-24, 8},
                {-22, 9},
                {-20, 10},
                {-18, 11},
                {-16, 12},
                {-14, 13},
                {-12, 14},
                {-10, 15},
                {-8, 16},
                {-6, 17},
                {-4, 18},
                {-2, 19},
                {0, 20},
            };

            Audion16CompressionRatio = new Dictionary<String, int>
            {
                { "1.00 : 1", 0},
                { "1.03 : 1", 1},
                { "1.05 : 1", 2},
                { "1.08 : 1", 3},
                { "1.11 : 1", 4},
                { "1.14 : 1", 5},
                { "1.18 : 1", 6},
                { "1.21 : 1", 7},
                { "1.25 : 1", 8},
                { "1.29 : 1", 9},
                { "1.33 : 1", 10},
                { "1.38 : 1", 11},
                { "1.43 : 1", 12},
                { "1.48 : 1", 13},
                { "1.54 : 1", 14},
                { "1.60 : 1", 15},
                { "1.67 : 1", 16},
                { "1.74 : 1", 17},
                { "1.82 : 1", 18},
                { "1.90 : 1", 19},
                { "2.00 : 1", 20},
                { "2.11 : 1", 21},
                { "2.22 : 1", 22},
                { "2.35 : 1", 23},
                { "2.50 : 1", 24},
                { "2.67 : 1", 25},
                { "2.86 : 1", 26},
                { "3.08 : 1", 27},
                { "3.33 : 1", 28},
                { "3.64 : 1", 29},
                { "4.00 : 1", 30},
                { "4.44 : 1", 31},
                { "5.00 : 1", 32},
                { "5.71 : 1", 33},
                { "6.67 : 1", 34},
                { "8.00 : 1", 35}
            };

            Audion16CompressionThresholds = new Dictionary<int, int>
                {
                    { 20, 0},
                    { 22, 1},
                    { 24, 2},
                    { 26, 3},
                    { 28, 4},
                    { 30, 5},
                    { 32, 6},
                    { 34, 7},
                    { 36, 8},
                    { 38, 9},
                    { 40, 10},
                    { 42, 11},
                    { 44, 12},
                    { 46, 13},
                    { 48, 14},
                    { 50, 15},
                    { 52, 16},
                    { 54, 17},
                    { 56, 18},
                    { 58, 19},
                    { 60, 20},
                    { 62, 21},
                    { 64, 22},
                    { 66, 23},
                    { 68, 24},
                    { 70, 25},
                    { 72, 26},
                    { 74, 27},
                    { 76, 28},
                    { 78, 29},
                    { 80, 30},
                    { 82, 31}
                };

            Audion16OutputCompressionLimiter = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Saída Máxima Sem Distorção", 1},
                {"-2 dB", 2},
                {"-4 dB", 3},
                {"-6 dB", 4},
                {"-8 dB", 5},
                {"-10 dB", 6},
                {"-12 dB", 7},
                {"-14 dB", 8},
                {"-16 dB", 9},
                {"-18 dB", 10},
                {"-20 dB", 11},
                {"-22 dB", 12},
                {"-24 dB", 13},
                {"-26 dB", 14},
                {"-28 dB", 15},
            };

            Audion16LowCutFilter = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"250 Hz", 1},
                {"500 Hz", 2},
                {"750 Hz", 3},
                {"1000 Hz", 4},
                {"1250 Hz", 5},
                {"1500 Hz", 6},
                {"2000 Hz", 7},
                {"2500Hz", 8},
                {"3000 Hz", 9}
            };

            Audion16VolumeControlMode = new Dictionary<String, int>
            {
                {"Analog VC Enabled", 0},
                {"Digital VC Enabled", 1},
                {"Multi - function Rocker", 2},
                {"Single push button VC", 3},
            };

            Audion16VolumeControlAnalogRange = new Dictionary<String, int>
            {
                {"Linear mode(50dB Range)", 0},
                {"Linear mode(40dB Range)", 1},
                {"Linear mode(30dB Range)", 2},
                {"Linear mode(20dB Range)", 3},
                {"Linear mode(10dB Range)", 4},
            };

            Audion16VolumeControlDigitalStepSize = new Dictionary<int, int>
            {
                {1, 0},
                {2, 1},
                {3, 2},
                {4, 3},
                {5, 4},
                {6, 5},
            };

            Audion16VolumeControlDigitalNumberOfSteps = new Dictionary<int, int>
            {
                {5, 0},
                {10, 1},
                {15, 2},
                {20, 3},
                {25, 4},
                {30, 5},
            };

            Audion16NumberOfUserPrograms = new Dictionary<String, int>
            {
                {"1 programa", 0},
                {"2 programas", 1},
                {"3 programas", 2},
                {"4 programas", 3},
                {"5 programas", 4},
                {"6 programas", 5}
            };

            Audion16PowerOnProgram = new Dictionary<String, int>
            {
                {"Programa 1", 0},
                {"Programa 2", 1},
                {"Programa 3", 2},
                {"Programa 4", 3},
                {"Programa 5", 4},
                {"Programa 6", 5}
            };

            Audion16ToneFrequency = new Dictionary<String, int>
            {
                {"500 Hz", 0},
                {"1000 Hz", 1},
                {"1500 Hz", 2},
                {"2000 HZ", 3},
            };

            Audion16PromptLevel = new Dictionary<String, int>
            {
                {"70 dB SPL", 0},
                {"75 dB SPL", 1},
                {"80 dB SPL", 2},
                {"85 dB SPL", 3},
                {"90 dB SPL", 4},
                {"95 dB SPL", 5},
                {"100 dB SPL", 6},
            };

            Audion16ProgramPromptMode = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Tons", 1},
                {"Messagem de voz", 2},
            };

            Audion16WarningPromptMode = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Tons", 1},
                {"Messagem de voz", 2},
            };

            Audion16PowerOnDelay = new Dictionary<String, int>
            {
                {"Nenhum", 0},
                {"5 seg", 1},
                {"10 seg", 2},
                {"15 seg", 3},
            };

            Audion16PowerOnLevel = new Dictionary<String, int>
            {
                {"Mutado", 0},
                {"-30 dB", 1},
                {"-20 dB", 2},
                {"-10 dB", 3},
            };

            Audion16AdaptiveDirectionalSensitivity = new Dictionary<String, int>
            {
                {"Baixo", 0},
                {"Médio", 1},
                {"Alto", 2},
                {"Máximo", 3},
            };

            Audion16DigitalNoiseGeneratorAmplitude = new Dictionary<String, int>
            {
                {"30 dB SPL", 0},
                {"35 dB SPL", 1},
                {"40 dB SPL", 2},
                {"45 dB SPL", 3},
                {"50 dB SPL", 4},
                {"55 dB SPL", 5},
                {"60 dB SPL", 6},
                {"65 dB SPL", 7},
            };
        }

        public void Audion8Dictionary()
        {
            Audion8InputMultiplexer = new Dictionary<String, int>
            {
                {"Microfone 1", 0},
                {"Microfone 2", 3},
            };

            Audion8PreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {12, 1},
                {15, 2},
                {18, 3},
                {21, 4},
                {24, 5},
                {27, 6},
                {30, 7},
            };

            Audion8CompressionThresholds = new Dictionary<int, int>
            {
                {40, 0},
                {45, 1},
                {50, 2},
                {55, 3},
                {60, 4},
                {65, 5},
                {70, 6},
                {75, 7},
            };

            Audion8CompressionRatio = new Dictionary<String, int>
            {
                {"1 : 1", 0},
                {"1.05 : 1", 1},
                {"1.11 : 1", 2},
                {"1.18 : 1", 3},
                {"1.25 : 1", 4},
                {"1.33 : 1", 5},
                {"1.43 : 1", 6},
                {"1.54 : 1", 7},
                {"1.67 : 1", 8},
                {"1.82 : 1", 9},
                {"2 : 1", 10},
                {"2.22 : 1", 11},
                {"2.5 : 1", 12},
                {"2.86 : 1", 13},
                {"3.33 : 1", 14},
                {"4 : 1", 15},
            };

            Audion8OutputCompressionLimiter = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Saída Máxima Sem Distorção", 1},
                {"-2 dB", 2},
                {"-4 dB", 3},
                {"-6 dB", 4},
                {"-8 dB", 5},
                {"-10 dB", 6},
                {"-12 dB", 7},
                {"-14 dB", 8},
                {"-16 dB", 9},
                {"-18 dB", 10},
                {"-20 dB", 11},
            };

            Audion8NoiseReduction = new Dictionary<String, int>
            {
                {"Desligada", 0},
                {"Baixa (7 dB)", 1},
                {"Média (10 dB)", 2},
                {"Alta (13 dB)", 3},
                {"Máxima (17 dB)", 4},
            };

            Audion8MatrixGain = new Dictionary<int, int>
            {
                {0, 0},
                {-1, 1},
                {-2, 2},
                {-3, 3},
                {-4, 4},
                {-5, 5},
                {-6, 6},
                {-7, 7},
                {-8, 8},
                {-9, 9},
                {-10, 10},
                {-11, 11},
                {-12, 12},
                {-13, 13},
                {-14, 14},
                {-15, 15},
                {-16, 16},
                {-17, 17},
                {-18, 18},
                {-19, 19},
                {-20, 20},
                {-21, 21},
                {-22, 22},
                {-23, 23},
                {-24, 24},
                {-25, 25},
                {-26, 26},
                {-27, 27},
                {-28, 28},
                {-29, 29},
                {-30, 30},
                {-31, 31},
                {-32, 32},
                {-33, 33},
                {-34, 34},
                {-35, 35},
                {-36, 36},
                {-37, 37},
                {-38, 38},
                {-39, 39},
                {-40, 40},
                {-41, 41},
                {-42, 42},
                {-43, 43},
                {-44, 44},
                {-45, 45},
                {-46, 46},
                {-47, 47},
            };

            Audion8BandEqualizationFilter = new Dictionary<int, int>
            {
                {-40, 0},
                {-38, 1},
                {-36, 2},
                {-34, 3},
                {-32, 4},
                {-30, 5},
                {-28, 6},
                {-26, 7},
                {-24, 8},
                {-22, 9},
                {-20, 10},
                {-18, 11},
                {-16, 12},
                {-14, 13},
                {-12, 14},
                {-10, 15},
                {-8, 16},
                {-6, 17},
                {-4, 18},
                {-2, 19},
                {0, 20},
            };

            Audion8AdaptiveFeedbackCanceller = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion8VolumeControlMode = new Dictionary<String, int>
            {
                {"Analog VC Enabled", 0},
                {"Digital VC Enabled", 1},
                {"Multi - function Rocker", 2},
                {"Single push button VC", 3},
            };

            Audion8VolumeControlAnalogRange = new Dictionary<String, int>
            {
                {"Linear mode(50dB Range)", 0},
                {"Linear mode(40dB Range)", 1},
                {"Linear mode(30dB Range)", 2},
                {"Linear mode(20dB Range)", 3},
                {"Linear mode(10dB Range)", 4},
            };

            Audion8VolumeControlDigitalStepSize = new Dictionary<int, int>
            {
                {1, 0},
                {2, 1},
                {3, 2},
                {4, 3},
                {5, 4},
                {6, 5},
            };

            Audion8VolumeControlDigitalNumberOfSteps = new Dictionary<int, int>
            {
                {5, 0},
                {10, 1},
                {15, 2},
                {20, 3},
                {25, 4},
                {30, 5},
            };

            Audion8NumberOfUserPrograms = new Dictionary<String, int>
            {
                {"1 programa", 0},
                {"2 programas", 1},
                {"3 programas", 2},
                {"4 programas", 3},
                {"AutoTelecoil Programa", 4},
            };

            Audion8ToneFrequency = new Dictionary<String, int>
            {
                {"500 Hz", 0},
                {"1000 Hz", 1},
                {"1500 Hz", 2},
                {"2000 HZ", 3},
            };

            Audion8PromptLevelIndexTL = new Dictionary<String, int>
            {
                {"-30 dB", 0},
                {"-25 dB", 1},
                {"-20 dB", 2},
                {"-15 dB", 3},
                {"-10 dB", 4},
                {"-5 dB", 5},
                {"0 dB (reference", 6},
            };

            Audion8PromptLevelIndexVP = new Dictionary<String, int>
            {
                {"-35 dB", 0},
                {"-30 dB", 1},
                {"-25 dB", 2},
                {"-20 dB", 3},
                {"-15 dB", 4},
                {"-10 dB", 5},
                {"-5 dB", 6},
            };

            Audion8ProgramPromptMode = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Tons", 1},
                {"Messagem de voz", 2},
            };

            Audion8LowBatteryPromptMode = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Tons", 1},
                {"Messagem de voz", 2},
            };

            Audion8AutoSave = new Dictionary<String, int>
            {
                {"Use VC_Startup and PGM_Startup parameters when powering up", 0},
                {"Use last in use VC position and program when re-powering aid", 1},
            };

            Audion8PowerOnDelay = new Dictionary<String, int>
            {
                {"3 seg", 0},
                {"5 seg", 1},
                {"10 seg", 2},
                {"15 seg", 3},
            };

            Audion8PowerOnLevel = new Dictionary<String, int>
            {
                {"Mutado", 0},
                {"-10 dB", 1},
                {"-20 dB", 2},
                {"-30 dB", 3},
            };

            Audion8AdaptiveDirectionalSensitivity = new Dictionary<String, int>
            {
                {"Baixo", 0},
                {"Médio", 1},
                {"Alto", 2},
                {"Máximo", 3},
            };

            Audion8DigitalNoiseGeneratorAmplitude = new Dictionary<String, int>
            {
                {"30 dB SPL", 0},
                {"35 dB SPL", 1},
                {"40 dB SPL", 2},
                {"45 dB SPL", 3},
                {"50 dB SPL", 4},
                {"55 dB SPL", 5},
                {"60 dB SPL", 6},
                {"65 dB SPL", 7},
            };
        }

        public void Audion6Dictionary()
        {
            Audion6InputMultiplexer = new Dictionary<String, int>
            {
                {"Microfone 1", 1},
                {"Microfone 2", 3},
            };

            Audion6PreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {12, 1},
                {15, 2},
                {18, 3},
                {21, 4},
                {24, 5},
                {27, 6},
                {30, 7},
            };

            Audion6CompressionThresholds = new Dictionary<int, int>
            {
                {34, 0},
                {36, 1},
                {38, 2},
                {40, 3},
                {42, 4},
                {44, 5},
                {46, 6},
                {48, 7},
                {50, 8},
                {52, 9},
                {54, 10},
                {56, 11},
                {58, 12},
                {60, 13},
                {62, 14},
                {64, 15},
            };

            Audion6CompressionRatio = new Dictionary<String, int>
            {
                {"1 : 1", 0},
                {"1.05 : 1", 1},
                {"1.11 : 1", 2},
                {"1.18 : 1", 3},
                {"1.25 : 1", 4},
                {"1.33 : 1", 5},
                {"1.43 : 1", 6},
                {"1.54 : 1", 7},
                {"1.67 : 1", 8},
                {"1.82 : 1", 9},
                {"2 : 1", 10},
                {"2.22 : 1", 11},
                {"2.5 : 1", 12},
                {"2.86 : 1", 13},
                {"3.33 : 1", 14},
                {"4 : 1", 15},
            };

            Audion6OutputCompressionLimiter = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Saída Máxima Sem Distorção", 1},
                {"-2 dB", 2},
                {"-4 dB", 3},
                {"-6 dB", 4},
                {"-8 dB", 5},
                {"-10 dB", 6},
                {"-12 dB", 7},
                {"-14 dB", 8},
                {"-16 dB", 9},
                {"-18 dB", 10},
                {"-20 dB", 11},
            };

            Audion6ExpansionRatio = new Dictionary<String, int>
            {
                {"1.25 : 1", 0},
                {"1.50 : 1", 7},
                {"1.75 : 1", 9},
                {"2 : 1", 10},
            };

            Audion6ExpansionAttack = new Dictionary<String, int>
            {
                {"3 ms", 0},
                {"6 ms", 1},
                {"12 ms", 2},
                {"24 ms", 3},
                {"48 ms", 4},
                {"96 ms", 5},
                {"192 ms", 6},
                {"384 ms", 7},
            };

            Audion6ExpansionRelease = new Dictionary<String, int>
            {
                {"30 ms", 0},
                {"60 ms", 1},
                {"120 ms", 2},
                {"240 ms", 3},
                {"480 ms", 4},
                {"960 ms", 5},
                {"1920 ms", 6},
                {"3840 ms", 7},
            };

            Audion6ExpansionThresholds = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"30 dB SPL", 1},
                {"32 dB SPL", 2},
                {"34 dB SPL", 3},
                {"36 dB SPL", 4},
                {"38 dB SPL", 5},
                {"40 dB SPL", 6},
                {"42 dB SPL", 7},
                {"44 dB SPL", 8},
                {"46 dB SPL", 9},
                {"48 dB SPL", 10},
                {"50 dB SPL", 11},
                {"52 dB SPL", 12},
                {"54 dB SPL", 13},
                {"56 dB SPL", 14},
                {"58 dB SPL", 15},
            };

            Audion6AdaptiveFeedbackCanceller = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion6MatrixGain = new Dictionary<int, int>
            {
                {0, 0},
                {-1, 1},
                {-2, 2},
                {-3, 3},
                {-4, 4},
                {-5, 5},
                {-6, 6},
                {-7, 7},
                {-8, 8},
                {-9, 9},
                {-10, 10},
                {-11, 11},
                {-12, 12},
                {-13, 13},
                {-14, 14},
                {-15, 15},
                {-16, 16},
                {-17, 17},
                {-18, 18},
                {-19, 19},
                {-20, 20},
                {-21, 21},
                {-22, 22},
                {-23, 23},
                {-24, 24},
                {-25, 25},
                {-26, 26},
                {-27, 27},
                {-28, 28},
                {-29, 29},
                {-30, 30},
                {-31, 31},
                {-32, 32},
                {-33, 33},
                {-34, 34},
                {-35, 35},
                {-36, 36},
                {-37, 37},
                {-38, 38},
                {-39, 39},
                {-40, 40},
                {-41, 41},
                {-42, 42},
                {-43, 43},
                {-44, 44},
                {-45, 45},
                {-46, 46},
                {-47, 47},
            };

            Audion6NoiseReduction = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Baixo (7 dB)", 1},
                {"Médio (10 dB)", 2},
                {"Alto (13 dB)", 3},
            };

            Audion6BandEqualizationFilter = new Dictionary<int, int>
            {
                {-40, 0},
                {-38, 1},
                {-36, 2},
                {-34, 3},
                {-32, 4},
                {-30, 5},
                {-28, 6},
                {-26, 7},
                {-24, 8},
                {-22, 9},
                {-20, 10},
                {-18, 11},
                {-16, 12},
                {-14, 13},
                {-12, 14},
                {-10, 15},
                {-8, 16},
                {-6, 17},
                {-4, 18},
                {-2, 19},
                {0, 20},
            };

            Audion6NumberOfUserPrograms = new Dictionary<String, int>
            {
                {"1 programa", 0},
                {"2 programas", 1},
                {"3 programas", 2},
                {"4 programas", 3},
            };

            Audion6ToneFrequency = new Dictionary<string, int>
            {
                {"500 Hz", 0},
                {"1000 Hz", 1},
                {"1500 Hz", 2},
                {"2000 Hz", 3},
            };

            Audion6ToneLevel = new Dictionary<string, int>
            {
                { "60 dB", 0},
                { "66 dB", 1},
                { "72 dB", 2},
                { "78 dB", 3},
            };

            Audion6LowBatteryTones = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion6ProgramSwitchTones = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion6PowerOnDelay = new Dictionary<String, int>
            {
                {"3 seg", 0},
                {"5 seg", 1},
                {"10 seg", 2},
                {"15 seg", 3},
            };

            Audion6PowerOnLevel = new Dictionary<String, int>
            {
                {"-60 dB (Mutado)", 0},
                {"-30 dB", 1},
                {"-20 dB", 2},
                {"-10 dB", 3},
            };
        }

        public void Audion4Dictionary()
        {
            Audion4InputMultiplexer = new Dictionary<String, int>
            {
                {"Microfone 1", 1},
                {"Microfone 2", 3},
            };

            Audion4PreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {12, 1},
                {15, 2},
                {18, 3},
                {21, 4},
                {24, 5},
                {27, 6},
                {30, 7},
            };

            Audion4CompressionThresholds = new Dictionary<int, int>
            {
                {40, 0},
                {45, 1},
                {50, 2},
                {55, 3},
                {60, 4},
                {65, 5},
                {70, 6},
            };

            Audion4CompressionRatio = new Dictionary<String, int>
            {
                {"1 : 1", 0},
                {"1.14 : 1", 1},
                {"1.33 : 1", 2},
                {"1.6 : 1", 3},
                {"2 : 1", 4},
                {"2.56 : 1", 5},
                {"4 : 1", 6},
            };

            Audion4OutputCompressionLimiter = new Dictionary<String, int>
            {
                {"Saída Máxima Sem Distorção", 0},
                {"-4 dB", 1},
                {"-8 dB", 2},
                {"-12 dB", 3},
                {"-16 dB", 4},
                {"-20 dB", 5},
                {"-24 dB", 6},
            };

            Audion4MicrophoneExpansion = new Dictionary<String, int>
            {
                {"Desabilitado", 0},
                {"Habilitado", 1},
            };

            Audion4NoiseReduction = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Baixo (7 dB)", 1},
                {"Médio (10 dB)", 2},
                {"Alto (13 dB)", 3},
            };

            Audion4MatrixGain = new Dictionary<int, int>
            {
                {0, 0},
                {-1, 1},
                {-2, 2},
                {-3, 3},
                {-4, 4},
                {-5, 5},
                {-6, 6},
                {-7, 7},
                {-8, 8},
                {-9, 9},
                {-10, 10},
                {-11, 11},
                {-12, 12},
                {-13, 13},
                {-14, 14},
                {-15, 15},
                {-16, 16},
                {-17, 17},
                {-18, 18},
                {-19, 19},
                {-20, 20},
                {-21, 21},
                {-22, 22},
                {-23, 23},
                {-24, 24},
                {-25, 25},
                {-26, 26},
                {-27, 27},
                {-28, 28},
                {-29, 29},
                {-30, 30},
                {-31, 31},
                {-32, 32},
                {-33, 33},
                {-34, 34},
                {-35, 35},
                {-36, 36},
                {-37, 37},
                {-38, 38},
                {-39, 39},
                {-40, 40},
                {-41, 41},
                {-42, 42},
                {-43, 43},
                {-44, 44},
                {-45, 45},
                {-46, 46},
                {-47, 47},
            };

            Audion4BandEqualizationFilter = new Dictionary<int, int>
            {
                {-30, 0},
                {-28, 1},
                {-26, 2},
                {-24, 3},
                {-22, 4},
                {-20, 5},
                {-18, 6},
                {-16, 7},
                {-14, 8},
                {-12, 9},
                {-10, 10},
                {-8, 11},
                {-6, 12},
                {-4, 13},
                {-2, 14},
                {0, 15},
            };

            Audion4LowCutFilter = new Dictionary<int, int>
            {
                {200, 0},
                {500, 1},
                {750, 2},
                {1000, 3},
                {1500, 4},
                {2000, 5},
                {3000, 6}
            };

            Audion4HighCutFilter = new Dictionary<int, int>
            {
                {8000, 0},
                {4000, 1},
                {3150, 2},
                {2500, 3},
                {2000, 4},
                {1600, 5},
                {1250, 6}
            };

            Audion4AdaptiveFeedbackCanceller = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion4VolumeControlEnable = new Dictionary<String, int>
            {
                {"Disabled", 0},
                {"Enabled", 1},
            };

            Audion4VolumeControlBeepEnable = new Dictionary<String, int>
            {
                {"Disabled", 0},
                {"Enabled", 1},
            };

            Audion4VolumeControlMode = new Dictionary<String, int>
            {
                {"Analog VC Enabled", 0},
                {"Digital VC Enabled", 1},
                {"Rocker Control", 2},
            };

            Audion4VolumeControlAnalogRange = new Dictionary<String, int>
            {
                {"Linear mode(50dB Range)", 0},
                {"Linear mode(40dB Range)", 1},
                {"Linear mode(30dB Range)", 2},
                {"Linear mode(20dB Range)", 3},
                {"Linear mode(10dB Range)", 4},
            };

            Audion4VolumeControlDigitalStepSize = new Dictionary<int, int>
            {
                {1, 0},
                {2, 1},
                {3, 2},
                {4, 3},
                {5, 4},
                {6, 5},
            };

            Audion4VolumeControlDigitalNumberOfSteps = new Dictionary<int, int>
            {
                {5, 0},
                {10, 1},
                {15, 2},
                {20, 3},
                {25, 4},
                {30, 5},
            };

            Audion4NumberOfUserPrograms = new Dictionary<String, int>
            {
                {"1 programa", 0},
                {"2 programas", 1},
                {"3 programas", 2},
                {"4 programas", 3},
            };

            Audion4ToneFrequency = new Dictionary<string, int>
            {
                {"500 Hz", 0},
                {"1000 Hz", 1},
                {"1500 Hz", 2},
                {"2000 Hz", 3},
            };

            Audion4ToneLevel = new Dictionary<string, int>
            {
                { "60 dB", 0},
                { "66 dB", 1},
                { "72 dB", 2},
                { "78 dB", 3},
            };

            Audion4LowBatteryTones = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion4ProgramSwitchTones = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            Audion4PowerOnDelay = new Dictionary<String, int>
            {
                {"3 seg", 0},
                {"5 seg", 1},
                {"10 seg", 2},
                {"15 seg", 3},
            };

            Audion4PowerOnLevel = new Dictionary<String, int>
            {
                {"-60 dB (Mutado)", 0},
                {"-30 dB", 1},
                {"-20 dB", 2},
                {"-10 dB", 3},
            };
        }

        public void SpinNRDictionary()
        {
            SpinNRInputMultiplexer = new Dictionary<String, int>
            {
                    {"Microfone 1", 1},
                    {"Microfone 2", 3},
            };

            SpinNRPreAmpGain = new Dictionary<int, int>
            {
                {0, 0},
                {12, 1},
                {15, 2},
                {18, 3},
                {21, 4},
                {24, 5},
                {27, 6},
                {30, 7},
            };

            SpinNRCompressionThresholds = new Dictionary<int, int>
            {
                {45, 0},
                {50, 1},
                {55, 2},
                {60, 3},
                {65, 4},
                {70, 5},
                {75, 6},
            };

            SpinNRCompressionRatio = new Dictionary<String, int>
            {
                {"1 : 1", 0},
                {"1.14 : 1", 1},
                {"1.33 : 1", 2},
                {"1.6 : 1", 3},
                {"2 : 1", 4},
                {"2.65 : 1", 5},
                {"4 : 1", 6},
            };

            SpinNROutputCompressionLimiter = new Dictionary<String, int>
            {
                {"Saída Máxima Sem Distorção", 0},
                {"-4 dB", 1},
                {"-8 dB", 2},
                {"-12 dB", 3},
                {"-16 dB", 4},
                {"-20 dB", 5},
                {"-24 dB", 6},
            };

            SpinNRMicrophoneExpansion = new Dictionary<String, int>
            {
                {"Desabilitado", 0},
                {"Habilitado", 1},
            };

            SpinNRNoiseReduction = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Baixo (7 dB)", 1},
                {"Médio (10 dB)", 2},
                {"Alto (13 dB)", 3},
            };

            SpinNRMatrixGain = new Dictionary<int, int>
            {
                {0, 0},
                {-1, 1},
                {-2, 2},
                {-3, 3},
                {-4, 4},
                {-5, 5},
                {-6, 6},
                {-7, 7},
                {-8, 8},
                {-9, 9},
                {-10, 10},
                {-11, 11},
                {-12, 12},
                {-13, 13},
                {-14, 14},
                {-15, 15},
                {-16, 16},
                {-17, 17},
                {-18, 18},
                {-19, 19},
                {-20, 20},
                {-21, 21},
                {-22, 22},
                {-23, 23},
                {-24, 24},
                {-25, 25},
                {-26, 26},
                {-27, 27},
                {-28, 28},
                {-29, 29},
                {-30, 30},
                {-31, 31},
                {-32, 32},
                {-33, 33},
                {-34, 34},
                {-35, 35},
                {-36, 36},
                {-37, 37},
                {-38, 38},
                {-39, 39},
                {-40, 40},
                {-41, 41},
                {-42, 42},
                {-43, 43},
                {-44, 44},
                {-45, 45},
                {-46, 46},
                {-47, 47},
            };

            SpinNRBandEqualizationFilter = new Dictionary<int, int>
            {
                {-30, 0},
                {-28, 1},
                {-26, 2},
                {-24, 3},
                {-22, 4},
                {-20, 5},
                {-18, 6},
                {-16, 7},
                {-14, 8},
                {-12, 9},
                {-10, 10},
                {-8, 11},
                {-6, 12},
                {-4, 13},
                {-2, 14},
                {0, 15},
            };

            SpinNRLowCutFilter = new Dictionary<int, int>
            {
                {200, 0},
                {500, 1},
                {750, 2},
                {1000, 3},
                {1500, 4},
                {2000, 5},
                {3000, 6}
            };

            SpinNRHighCutFilter = new Dictionary<int, int>
            {
                {8000, 0},
                {4000, 1},
                {3150, 2},
                {2500, 3},
                {2000, 4},
                {1600, 5},
                {1250, 6}
            };

            SpinNRAdaptiveFeedbackCanceller = new Dictionary<String, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            SpinNRVolumeControlMode = new Dictionary<String, int>
            {
                {"Analog VC", 0},
                {"Rocker VC", 1},
            };

            SpinNRVolumeControlAnalogRange = new Dictionary<String, int>
            {
                {"Linear mode(50dB Range)", 0},
                {"Linear mode(40dB Range)", 1},
                {"Linear mode(30dB Range)", 2},
                {"Linear mode(20dB Range)", 3},
                {"Linear mode(10dB Range)", 4},
            };

            SpinNRVolumeControlRockerRange = new Dictionary<String, int>
            {
                {"14 Steps – 3dB Stepsize(42dB Range)", 0},
                {"10 Steps – 3dB Stepsize(30dB Range)", 1},
                {"15 Steps – 2dB Stepsize(30dB Range)", 2},
                {"10 Steps – 2dB Stepsize(20dB Range)", 3},
                {"5 Steps – 2dB Stepsize(10dB Range)", 4},
            };

            SpinNRNumberOfUserPrograms = new Dictionary<String, int>
            {
                {"1 programa", 0},
                {"2 programas", 1},
                {"3 programas", 2},
                {"4 programas", 3},
                {"5 programas", 4},
            };

            SpinNRToneFrequency = new Dictionary<string, int>
            {
                {"500 Hz", 0},
                {"1000 Hz", 1},
                {"1500 Hz", 2},
                {"2000 Hz", 3},
            };

            SpinNRToneLevel = new Dictionary<string, int>
            {
                {"60 dB", 0},
                {"66 dB", 1},
                {"72 dB", 2},
                {"78 dB", 3},
            };

            SpinNRLowBatteryTones = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };

            SpinNRProgramSwitchTones = new Dictionary<string, int>
            {
                {"Desligado", 0},
                {"Ligado", 1},
            };
        }
    }
}