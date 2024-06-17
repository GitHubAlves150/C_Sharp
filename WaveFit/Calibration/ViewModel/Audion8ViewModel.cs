using Audion8TestApp;
using HandyControl.Controls;
using IAmp;
using System;
using System.IO;
using WaveFit2.Calibration.Class;

namespace WaveFit2.Calibration.ViewModel
{
    public class Audion8ViewModel : Audion8
    {
        public bool ProgrammerInitialized;
        public int errorCode;

        public event Action<int> ErrorCodeChanged;

        public Int16 activeProgram;
        public Int16 activeSide;
        public Int16 DLLVER = 1;

        public Params[] Audion8Params = new Params[2];
        public Config[] Audion8Config = new Config[2];

        public int input_mux, preamp_gain0, preamp_gain1, C1_Ratio, C2_Ratio, C3_Ratio, C4_Ratio, C5_Ratio, C6_Ratio, C7_Ratio, C8_Ratio,
        C1_TK, C2_TK, C3_TK, C4_TK, C5_TK, C6_TK, C7_TK, C8_TK, C1_MPO, C2_MPO, C3_MPO, C4_MPO, C5_MPO, C6_MPO, C7_MPO, C8_MPO,
        BEQ1_gain, BEQ2_gain, BEQ3_gain, BEQ4_gain, BEQ5_gain, BEQ6_gain, BEQ7_gain, BEQ8_gain, BEQ9_gain, BEQ10_gain,
        BEQ11_gain, BEQ12_gain, matrix_gain, Noise_Reduction, FBC_Enable, Time_Constants;

        public int AutoSave_Enable, ATC, EnableHPmode, Noise_Level, POL, POD, AD_Sens, Cal_Input, Dir_Spacing, Mic_Cal,
        number_of_programs, PGM_Startup, Switch_Mode, Program_Prompt_Mode, Warning_Prompt_Mode, Tone_Frequency, Tone_Level,
        VC_Enable, VC_Analog_Range, VC_Digital_Numsteps, VC_Digital_Startup, VC_Digital_Stepsize, VC_Mode, VC_pos,
        VC_Prompt_Mode, AlgVer_Major, AlgVer_Minor, MANF_ID, PlatformID, reserved1, reserved2, test, MANF_reserve_1,
        MANF_reserve_2, MANF_reserve_3, MANF_reserve_4, MANF_reserve_5, MANF_reserve_6, MANF_reserve_7, MANF_reserve_8,
        MANF_reserve_9, MANF_reserve_10;

        public Target2Params target;
        public Audion8_AutofitParams AfParams;

        public DetectData detdata;

        public WaveRule waveRule;

        public Response micFR;
        public Response recFR;
        public Response FreqResp;

        private FileUtilities fileUtils = new FileUtilities();

        public string defaultReceptor, defaultMicrophone;

        public Audion8ViewModel()
        {
            initArrayParams();
            initStructureArrays();
            RecDirectory();
            MicDirectory();
        }

        private void initStructureArrays()
        {
            this.micFR.element = new Single[65];
            this.recFR.element = new Single[65];
            this.FreqResp.element = new Single[65];
        }

        private void initArrayParams()
        {
            target = new Target2Params(true);
            AfParams = new Audion8_AutofitParams(true);

            for (int i = 0; i <= 1; i++)
            {
                this.Audion8Params[i].BEQ1_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ2_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ3_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ4_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ5_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ6_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ7_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ8_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ9_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ10_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ11_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].BEQ12_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C1_MPO = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C2_MPO = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C3_MPO = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C4_MPO = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C5_MPO = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C6_MPO = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C7_MPO = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C8_MPO = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C1_Ratio = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C2_Ratio = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C3_Ratio = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C4_Ratio = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C5_Ratio = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C6_Ratio = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C7_Ratio = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C8_Ratio = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C1_TK = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C2_TK = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C3_TK = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C4_TK = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C5_TK = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C6_TK = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C7_TK = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].C8_TK = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].FBC_Enable = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].input_mux = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].matrix_gain = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].Noise_Reduction = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].preamp_gain0 = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].preamp_gain1 = new short[] { 0, 0, 0, 0, 0 };
                this.Audion8Params[i].Time_Constants = new short[] { 0, 0, 0, 0, 0 };
            }
        }

        public void RecDirectory()
        {
            string[] Receptors = { @"Resources\RecModel\BVA321.txt", @"Resources\RecModel\BVA530.txt" };
            defaultReceptor = Path.Combine(Directory.GetCurrentDirectory(), Receptors[0]);
        }

        public void MicDirectory()
        {
            string[] Microphone = { @"Resources\MicModel\APT-D Sonion 6295.txt" };
            defaultMicrophone = Path.Combine(Directory.GetCurrentDirectory(), Microphone[0]);
        }

        public void StartProgrammer()
        {
            Set_interface_type(0);
            InitializeProgramer();
        }

        public void CloseProgramer()
        {
            this.errorCode = Close();
            if (this.errorCode == 0)
            {
                this.ProgrammerInitialized = false;
                Growl.SuccessGlobal("Programadora desligada.");
            }
            else
            {
                Console.WriteLine("Close programmer:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
        }

        public void InitializeProgramer()
        {
            this.errorCode = InitializeDriver();
            if (this.errorCode == 0)
            {
                this.ProgrammerInitialized = true;
                Growl.SuccessGlobal("Programadora inicializada.");
            }
            else
            {
                Console.WriteLine("Init Programmer:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
        }

        public void GetMicRec()
        {
            loadMicModel();
            loadRecModel();

            this.errorCode = Set_mic_response(ref this.micFR);
            if (this.errorCode != 0)
            {
                Console.WriteLine("Set_mic_response() error:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }

            this.errorCode = Set_rec_response(ref this.recFR);
            if (this.errorCode != 0)
            {
                Console.WriteLine("Set_rec_response() error:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
        }

        public void ChangeSide(char side)
        {
            if (side == 'L')
            {
                activeSide = 0;
                this.errorCode = Set_RL_channel(this.activeSide);
                if (this.errorCode == 0)
                {
                    Growl.SuccessGlobal("Lado esquerdo escolhido com sucesso");
                }
                else
                {
                    Console.WriteLine("Set channel:" + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                }
            }
            else
            {
                activeSide = 1;
                this.errorCode = Set_RL_channel(this.activeSide);
                if (this.errorCode == 0)
                {
                    Growl.SuccessGlobal("Lado direito escolhido com sucesso");
                }
                else
                {
                    Console.WriteLine("Set channel:" + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                }
            }
        }

        public void GetResetParam()
        {
            GetAllParams(false);
        }

        public void ReadVM()
        {
            GetAllParams(true);
        }

        public void GetAllParams(bool doRead)
        {
            if (doRead == true)
            {
                this.errorCode = Read(-1);
            }

            if (this.errorCode != 0)
            {
                Console.WriteLine("Read:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Get_params(this.DLLVER, ref this.Audion8Params[this.activeSide]);
                if (this.errorCode != 0)
                {
                    Console.WriteLine("Read Get_params:" + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));

                    return;
                }
                else
                {
                    this.errorCode = Get_config(this.DLLVER, ref this.Audion8Config[this.activeSide]);
                    if (this.errorCode != 0)
                    {
                        Console.WriteLine("Read Get_config:" + DriverErrorString(errorCode));
                        Growl.ErrorGlobal(DriverErrorString(errorCode));
                        return;
                    }
                    else
                    {
                        Growl.SuccessGlobal("Parametros lidos com sucesso.");
                    }
                }
            }
            UpdateParamBoxesFromParams(activeSide, activeProgram);
            UpdateConfigBoxesFromConfigParams(activeSide);
        }

        public void UpdateParamBoxesFromParams(int activeSide, int activeProgram)
        {
            try
            {
                input_mux = Audion8Params[activeSide].input_mux[activeProgram];
                preamp_gain0 = Audion8Params[activeSide].preamp_gain0[activeProgram];
                preamp_gain1 = Audion8Params[activeSide].preamp_gain1[activeProgram];
                C1_Ratio = Audion8Params[activeSide].C1_Ratio[activeProgram];
                C2_Ratio = Audion8Params[activeSide].C2_Ratio[activeProgram];
                C3_Ratio = Audion8Params[activeSide].C3_Ratio[activeProgram];
                C4_Ratio = Audion8Params[activeSide].C4_Ratio[activeProgram];
                C5_Ratio = Audion8Params[activeSide].C5_Ratio[activeProgram];
                C6_Ratio = Audion8Params[activeSide].C6_Ratio[activeProgram];
                C7_Ratio = Audion8Params[activeSide].C7_Ratio[activeProgram];
                C8_Ratio = Audion8Params[activeSide].C8_Ratio[activeProgram];
                C1_TK = Audion8Params[activeSide].C1_TK[activeProgram];
                C2_TK = Audion8Params[activeSide].C2_TK[activeProgram];
                C3_TK = Audion8Params[activeSide].C3_TK[activeProgram];
                C4_TK = Audion8Params[activeSide].C4_TK[activeProgram];
                C5_TK = Audion8Params[activeSide].C5_TK[activeProgram];
                C6_TK = Audion8Params[activeSide].C6_TK[activeProgram];
                C7_TK = Audion8Params[activeSide].C7_TK[activeProgram];
                C8_TK = Audion8Params[activeSide].C8_TK[activeProgram];
                C1_MPO = Audion8Params[activeSide].C1_MPO[activeProgram];
                C2_MPO = Audion8Params[activeSide].C2_MPO[activeProgram];
                C3_MPO = Audion8Params[activeSide].C3_MPO[activeProgram];
                C4_MPO = Audion8Params[activeSide].C4_MPO[activeProgram];
                C5_MPO = Audion8Params[activeSide].C5_MPO[activeProgram];
                C6_MPO = Audion8Params[activeSide].C6_MPO[activeProgram];
                C7_MPO = Audion8Params[activeSide].C7_MPO[activeProgram];
                C8_MPO = Audion8Params[activeSide].C8_MPO[activeProgram];
                BEQ1_gain = Audion8Params[activeSide].BEQ1_gain[activeProgram];
                BEQ2_gain = Audion8Params[activeSide].BEQ2_gain[activeProgram];
                BEQ3_gain = Audion8Params[activeSide].BEQ3_gain[activeProgram];
                BEQ4_gain = Audion8Params[activeSide].BEQ4_gain[activeProgram];
                BEQ5_gain = Audion8Params[activeSide].BEQ5_gain[activeProgram];
                BEQ6_gain = Audion8Params[activeSide].BEQ6_gain[activeProgram];
                BEQ7_gain = Audion8Params[activeSide].BEQ7_gain[activeProgram];
                BEQ8_gain = Audion8Params[activeSide].BEQ8_gain[activeProgram];
                BEQ9_gain = Audion8Params[activeSide].BEQ9_gain[activeProgram];
                BEQ10_gain = Audion8Params[activeSide].BEQ10_gain[activeProgram];
                BEQ11_gain = Audion8Params[activeSide].BEQ11_gain[activeProgram];
                BEQ12_gain = Audion8Params[activeSide].BEQ12_gain[activeProgram];
                matrix_gain = Audion8Params[activeSide].matrix_gain[activeProgram];
                Noise_Reduction = Audion8Params[activeSide].Noise_Reduction[activeProgram];
                FBC_Enable = Audion8Params[activeSide].FBC_Enable[activeProgram];
                Time_Constants = Audion8Params[activeSide].Time_Constants[activeProgram];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateConfigBoxesFromConfigParams(int activeSide)
        {
            try
            {
                AutoSave_Enable = Audion8Config[activeSide].AutoSave_Enable;
                ATC = Audion8Config[activeSide].ATC;
                EnableHPmode = Audion8Config[activeSide].EnableHPmode;
                Noise_Level = Audion8Config[activeSide].Noise_Level;
                POL = Audion8Config[activeSide].POL;
                POD = Audion8Config[activeSide].POD;
                AD_Sens = Audion8Config[activeSide].AD_Sens;
                Cal_Input = Audion8Config[activeSide].Cal_Input;
                Dir_Spacing = Audion8Config[activeSide].Dir_Spacing;
                Mic_Cal = Audion8Config[activeSide].Mic_Cal;
                number_of_programs = Audion8Config[activeSide].number_of_programs;
                PGM_Startup = Audion8Config[activeSide].PGM_Startup;
                Switch_Mode = Audion8Config[activeSide].Switch_Mode;
                Program_Prompt_Mode = Audion8Config[activeSide].Program_Prompt_Mode;
                Warning_Prompt_Mode = Audion8Config[activeSide].Warning_Prompt_Mode;
                Tone_Frequency = Audion8Config[activeSide].Tone_Frequency;
                Tone_Level = Audion8Config[activeSide].Tone_Level;
                VC_Enable = Audion8Config[activeSide].VC_Enable;
                VC_Analog_Range = Audion8Config[activeSide].VC_Analog_Range;
                VC_Digital_Numsteps = Audion8Config[activeSide].VC_Digital_Numsteps;
                VC_Digital_Startup = Audion8Config[activeSide].VC_Digital_Startup;
                VC_Digital_Stepsize = Audion8Config[activeSide].VC_Digital_Stepsize;
                VC_Mode = Audion8Config[activeSide].VC_Mode;
                VC_pos = Audion8Config[activeSide].VC_pos;
                VC_Prompt_Mode = Audion8Config[activeSide].VC_Prompt_Mode;
                AlgVer_Major = Audion8Config[activeSide].AlgVer_Major;
                AlgVer_Minor = Audion8Config[activeSide].AlgVer_Minor;
                MANF_ID = Audion8Config[activeSide].MANF_ID;
                PlatformID = Audion8Config[activeSide].PlatformID;
                reserved1 = Audion8Config[activeSide].reserved1;
                reserved2 = Audion8Config[activeSide].reserved2;
                test = Audion8Config[activeSide].test;
                MANF_reserve_1 = Audion8Config[activeSide].MANF_reserve_1;
                MANF_reserve_2 = Audion8Config[activeSide].MANF_reserve_2;
                MANF_reserve_3 = Audion8Config[activeSide].MANF_reserve_3;
                MANF_reserve_4 = Audion8Config[activeSide].MANF_reserve_4;
                MANF_reserve_5 = Audion8Config[activeSide].MANF_reserve_5;
                MANF_reserve_6 = Audion8Config[activeSide].MANF_reserve_6;
                MANF_reserve_7 = Audion8Config[activeSide].MANF_reserve_7;
                MANF_reserve_8 = Audion8Config[activeSide].MANF_reserve_8;
                MANF_reserve_9 = Audion8Config[activeSide].MANF_reserve_9;
                MANF_reserve_10 = Audion8Config[activeSide].MANF_reserve_10;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetResetParam(bool write)
        {
            SetAllParams(write);
        }

        public void LoadVM()
        {
            SetAllParams(true);
        }

        public void SetAllParams(bool doLoad)
        {
            UpdateParamsFromParamBoxes(activeSide, activeProgram);
            UpdateConfigParamsFromConfigBoxes(activeSide);

            this.errorCode = Set_params(this.DLLVER, ref this.Audion8Params[this.activeSide]);

            if (this.errorCode != 0)
            {
                Console.WriteLine("Load Set_params: " + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Set_config(this.DLLVER, ref this.Audion8Config[this.activeSide]);

                if (this.errorCode != 0)
                {
                    Console.WriteLine("Load Set_config:" + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                }
                else if (doLoad)
                {
                    this.errorCode = Load(-2);
                    if (this.errorCode != 0)
                    {
                        Console.WriteLine("Loaded" + DriverErrorString(errorCode));
                        Growl.ErrorGlobal(DriverErrorString(errorCode));
                    }
                    else
                    {
                        Growl.SuccessGlobal("Parametros carregados com sucesso.");
                    }
                }
            }
        }

        public void UpdateParamsFromParamBoxes(int activeSide, int activeProgram)
        {
            try
            {
                Audion8Params[activeSide].input_mux[activeProgram] = Convert.ToInt16(input_mux);
                Audion8Params[activeSide].preamp_gain0[activeProgram] = Convert.ToInt16(preamp_gain0);
                Audion8Params[activeSide].preamp_gain1[activeProgram] = Convert.ToInt16(preamp_gain1);
                Audion8Params[activeSide].C1_Ratio[activeProgram] = Convert.ToInt16(C1_Ratio);
                Audion8Params[activeSide].C2_Ratio[activeProgram] = Convert.ToInt16(C2_Ratio);
                Audion8Params[activeSide].C3_Ratio[activeProgram] = Convert.ToInt16(C3_Ratio);
                Audion8Params[activeSide].C4_Ratio[activeProgram] = Convert.ToInt16(C4_Ratio);
                Audion8Params[activeSide].C5_Ratio[activeProgram] = Convert.ToInt16(C5_Ratio);
                Audion8Params[activeSide].C6_Ratio[activeProgram] = Convert.ToInt16(C6_Ratio);
                Audion8Params[activeSide].C7_Ratio[activeProgram] = Convert.ToInt16(C7_Ratio);
                Audion8Params[activeSide].C8_Ratio[activeProgram] = Convert.ToInt16(C8_Ratio);
                Audion8Params[activeSide].C1_TK[activeProgram] = Convert.ToInt16(C1_TK);
                Audion8Params[activeSide].C2_TK[activeProgram] = Convert.ToInt16(C2_TK);
                Audion8Params[activeSide].C3_TK[activeProgram] = Convert.ToInt16(C3_TK);
                Audion8Params[activeSide].C4_TK[activeProgram] = Convert.ToInt16(C4_TK);
                Audion8Params[activeSide].C5_TK[activeProgram] = Convert.ToInt16(C5_TK);
                Audion8Params[activeSide].C6_TK[activeProgram] = Convert.ToInt16(C6_TK);
                Audion8Params[activeSide].C7_TK[activeProgram] = Convert.ToInt16(C7_TK);
                Audion8Params[activeSide].C8_TK[activeProgram] = Convert.ToInt16(C8_TK);
                Audion8Params[activeSide].C1_MPO[activeProgram] = Convert.ToInt16(C1_MPO);
                Audion8Params[activeSide].C2_MPO[activeProgram] = Convert.ToInt16(C2_MPO);
                Audion8Params[activeSide].C3_MPO[activeProgram] = Convert.ToInt16(C3_MPO);
                Audion8Params[activeSide].C4_MPO[activeProgram] = Convert.ToInt16(C4_MPO);
                Audion8Params[activeSide].C5_MPO[activeProgram] = Convert.ToInt16(C5_MPO);
                Audion8Params[activeSide].C6_MPO[activeProgram] = Convert.ToInt16(C6_MPO);
                Audion8Params[activeSide].C7_MPO[activeProgram] = Convert.ToInt16(C7_MPO);
                Audion8Params[activeSide].C8_MPO[activeProgram] = Convert.ToInt16(C8_MPO);
                Audion8Params[activeSide].BEQ1_gain[activeProgram] = Convert.ToInt16(BEQ1_gain);
                Audion8Params[activeSide].BEQ2_gain[activeProgram] = Convert.ToInt16(BEQ2_gain);
                Audion8Params[activeSide].BEQ3_gain[activeProgram] = Convert.ToInt16(BEQ3_gain);
                Audion8Params[activeSide].BEQ4_gain[activeProgram] = Convert.ToInt16(BEQ4_gain);
                Audion8Params[activeSide].BEQ5_gain[activeProgram] = Convert.ToInt16(BEQ5_gain);
                Audion8Params[activeSide].BEQ6_gain[activeProgram] = Convert.ToInt16(BEQ6_gain);
                Audion8Params[activeSide].BEQ7_gain[activeProgram] = Convert.ToInt16(BEQ7_gain);
                Audion8Params[activeSide].BEQ8_gain[activeProgram] = Convert.ToInt16(BEQ8_gain);
                Audion8Params[activeSide].BEQ9_gain[activeProgram] = Convert.ToInt16(BEQ9_gain);
                Audion8Params[activeSide].BEQ10_gain[activeProgram] = Convert.ToInt16(BEQ10_gain);
                Audion8Params[activeSide].BEQ11_gain[activeProgram] = Convert.ToInt16(BEQ11_gain);
                Audion8Params[activeSide].BEQ12_gain[activeProgram] = Convert.ToInt16(BEQ12_gain);
                Audion8Params[activeSide].matrix_gain[activeProgram] = Convert.ToInt16(matrix_gain);
                Audion8Params[activeSide].Noise_Reduction[activeProgram] = Convert.ToInt16(Noise_Reduction);
                Audion8Params[activeSide].FBC_Enable[activeProgram] = Convert.ToInt16(FBC_Enable);
                Audion8Params[activeSide].Time_Constants[activeProgram] = Convert.ToInt16(Time_Constants);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateConfigParamsFromConfigBoxes(int activeSide)
        {
            try
            {
                Audion8Config[activeSide].AutoSave_Enable = Convert.ToInt16(AutoSave_Enable);
                Audion8Config[activeSide].ATC = Convert.ToInt16(ATC);
                Audion8Config[activeSide].EnableHPmode = Convert.ToInt16(EnableHPmode);
                Audion8Config[activeSide].Noise_Level = Convert.ToInt16(Noise_Level);
                Audion8Config[activeSide].POL = Convert.ToInt16(POL);
                Audion8Config[activeSide].POD = Convert.ToInt16(POD);
                Audion8Config[activeSide].AD_Sens = Convert.ToInt16(AD_Sens);
                Audion8Config[activeSide].Cal_Input = Convert.ToInt16(Cal_Input);
                Audion8Config[activeSide].Dir_Spacing = Convert.ToInt16(Dir_Spacing);
                Audion8Config[activeSide].Mic_Cal = Convert.ToInt16(Mic_Cal);
                Audion8Config[activeSide].number_of_programs = Convert.ToInt16(number_of_programs);
                Audion8Config[activeSide].PGM_Startup = Convert.ToInt16(PGM_Startup);
                Audion8Config[activeSide].Switch_Mode = Convert.ToInt16(Switch_Mode);
                Audion8Config[activeSide].Program_Prompt_Mode = Convert.ToInt16(Program_Prompt_Mode);
                Audion8Config[activeSide].Warning_Prompt_Mode = Convert.ToInt16(Warning_Prompt_Mode);
                Audion8Config[activeSide].Tone_Frequency = Convert.ToInt16(Tone_Frequency);
                Audion8Config[activeSide].Tone_Level = Convert.ToInt16(Tone_Level);
                Audion8Config[activeSide].VC_Enable = Convert.ToInt16(VC_Enable);
                Audion8Config[activeSide].VC_Analog_Range = Convert.ToInt16(VC_Analog_Range);
                Audion8Config[activeSide].VC_Digital_Numsteps = Convert.ToInt16(VC_Digital_Numsteps);
                Audion8Config[activeSide].VC_Digital_Startup = Convert.ToInt16(VC_Digital_Startup);
                Audion8Config[activeSide].VC_Digital_Stepsize = Convert.ToInt16(VC_Digital_Stepsize);
                Audion8Config[activeSide].VC_Mode = Convert.ToInt16(VC_Mode);
                Audion8Config[activeSide].VC_pos = Convert.ToInt16(VC_pos);
                Audion8Config[activeSide].VC_Prompt_Mode = Convert.ToInt16(VC_Prompt_Mode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LockVM()
        {
            this.errorCode = Lock();
            if (this.errorCode != 0)
            {
                Console.WriteLine("Lock:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                Growl.SuccessGlobal("Parametros gravados com sucesso.");
            }
        }

        public void Burn()
        {
            LoadVM();
            LockVM();
        }

        public void Write(bool write)
        {
            SetAllParams(write);
        }

        public void GetActiveProgram(ref short value)
        {
            Get_active_program(ref value);
        }

        public void SetActiveProgram(int value)
        {
            Set_active_program((short)value);
        }

        public void SetCurrentProgram(int value)
        {
            if (value >= 0)
            {
                activeProgram = (short)value;
                SetProgram(activeProgram);
            }
            else
            {
                activeProgram = 0;
            }
        }

        public bool PopulateTarget2FromControls(ref Target2Params target, ref Audion8_AutofitParams AfitParams, WaveRule waveRule)
        {
            try
            {
                target.sng50[0] = (float)waveRule.targetGains[0][0];
                target.sng50[1] = (float)waveRule.targetGains[0][1];
                target.sng50[2] = (float)waveRule.targetGains[0][2];
                target.sng50[3] = (float)waveRule.targetGains[0][3];
                target.sng50[4] = (float)waveRule.targetGains[0][4];
                target.sng50[5] = (float)waveRule.targetGains[0][5];
                target.sng50[6] = (float)waveRule.targetGains[0][6];
                target.sng50[7] = (float)waveRule.targetGains[0][7];
                target.sng50[8] = (float)waveRule.targetGains[0][8];
                target.sng50[9] = (float)waveRule.targetGains[0][9];
                target.sng50[10] = (float)waveRule.targetGains[0][10];

                target.sng80[0] = (float)waveRule.targetGains[1][0];
                target.sng80[1] = (float)waveRule.targetGains[1][1];
                target.sng80[2] = (float)waveRule.targetGains[1][2];
                target.sng80[3] = (float)waveRule.targetGains[1][3];
                target.sng80[4] = (float)waveRule.targetGains[1][4];
                target.sng80[5] = (float)waveRule.targetGains[1][5];
                target.sng80[6] = (float)waveRule.targetGains[1][6];
                target.sng80[7] = (float)waveRule.targetGains[1][7];
                target.sng80[8] = (float)waveRule.targetGains[1][8];
                target.sng80[9] = (float)waveRule.targetGains[1][9];
                target.sng80[10] = (float)waveRule.targetGains[1][10];

                target.MPO[0] = (short)waveRule.CalculateUCL(activeSide);
                target.MPO[1] = (short)waveRule.CalculateUCL(activeSide);
                target.MPO[2] = (short)waveRule.CalculateUCL(activeSide);
                target.MPO[3] = (short)waveRule.CalculateUCL(activeSide);
                target.MPO[4] = (short)waveRule.CalculateUCL(activeSide);
                target.MPO[5] = (short)waveRule.CalculateUCL(activeSide);
                target.MPO[6] = (short)waveRule.CalculateUCL(activeSide);
                target.MPO[7] = (short)waveRule.CalculateUCL(activeSide);

                target.CR[0] = 0;
                target.CR[1] = 0;
                target.CR[2] = 0;
                target.CR[3] = 0;
                target.CR[4] = 0;
                target.CR[5] = 0;
                target.CR[6] = 0;
                target.CR[7] = 0;

                AfitParams.TK[0] = 0;
                AfitParams.TK[1] = 0;
                AfitParams.TK[2] = 0;
                AfitParams.TK[3] = 0;
                AfitParams.TK[4] = 0;
                AfitParams.TK[5] = 0;
                AfitParams.TK[6] = 0;
                AfitParams.TK[7] = 0;

                target.ResGain = 6;

                target.SpeechTK = 55;

                int a = 0;
                int b = 0;
                int c = 0;
                int d = 0;
                target.Use_CR = (short)(a | b | c | d);

                //Console.WriteLine("Suave");
                //foreach (var value in waveRule.targetGains[0])
                //{
                //    Console.WriteLine(value);
                //}
                //Console.WriteLine("Moderado");
                //foreach (var value in waveRule.targetGains[1])
                //{
                //    Console.WriteLine(value);
                //}
                //Console.WriteLine("Alto");
                //foreach (var value in waveRule.targetGains[2])
                //{
                //    Console.WriteLine(value);
                //}
                //Console.WriteLine("UCL:");
                //Console.WriteLine(waveRule.CalculateUCL(activeSide));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void loadMicModel()
        {
            IntriconPlotObject micTable = new IntriconPlotObject();

            micTable = this.fileUtils.openCurveFileLegacy(defaultMicrophone);

            for (int i = 0; i <= 64; i++)
            {
                this.micFR.element[i] = (float)micTable.getResponsePoint(i);
            }
        }

        private void loadRecModel()
        {
            IntriconPlotObject recTable = new IntriconPlotObject();

            recTable = this.fileUtils.openCurveFileLegacy(defaultReceptor);

            for (int i = 0; i <= 64; i++)
            {
                this.recFR.element[i] = recTable.getResponsePoint(i);
            }
        }

        public void Unmute()
        {
            this.errorCode = Audio_on(this.activeProgram);
            if (this.errorCode != 0)
            {
                Console.WriteLine("UnMute:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                Growl.SuccessGlobal("Aparelho desmutado com sucesso.");
            }
        }

        public void Muted()
        {
            this.errorCode = Mute();
            if (this.errorCode != 0)
            {
                Console.WriteLine("Mute:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                Growl.SuccessGlobal("Aparelho mutado com sucesso.");
            }
        }

        public void Autofit(WaveRule waveRule)
        {
            short MGC = -20;

            if (!PopulateTarget2FromControls(ref target, ref AfParams, waveRule)) return;

            SetMatrixGainCeiling(Math.Abs(MGC));

            SetRec_Saturation(883883);

            GetMicRec();

            this.errorCode = AutoFit2(0, ref target);
            if (this.errorCode != 0)
            {
                Console.WriteLine("Autofit() error:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Get_params(this.DLLVER, ref this.Audion8Params[this.activeSide]);
                if (this.errorCode != 0)
                {
                    Console.WriteLine("Get_params(): error: " + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                }
                else
                {
                    UpdateParamBoxesFromParams(this.activeSide, this.activeProgram);
                    Growl.SuccessGlobal("Autofit realizado com sucesso.");
                }
            }
        }

        public void CopyProgramFromTo(short fromProgram, short toProgram, ref Params Program)
        {
            CopyProgram(fromProgram, toProgram, ref Program);
        }

        public void TestAlert(short program)
        {
            TestPrompt(program, 0, 0);
        }
    }
}