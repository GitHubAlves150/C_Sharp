using Audion8TestApp;
using HandyControl.Controls;
using IAmp;
using System;
using System.IO;
using WaveFit2.Calibration.Class;

namespace WaveFit2.Calibration.ViewModel
{
    public class SpinNRViewModel : SpinNR
    {
        public bool ProgrammerInitialized;
        public int errorCode;

        public Int16 activeProgram;
        public Int16 activeSide;
        public Int16 DLLVER = 1;

        public Params[] SpinNRParams = new Params[2];

        public int input_mux, preamp_gain0, preamp_gain1, CRL, CRH, threshold, Low_Cut, High_Cut, Noise_Reduction,
                   BEQ1_gain, BEQ2_gain, BEQ3_gain, BEQ4_gain, BEQ5_gain, BEQ6_gain, BEQ7_gain, BEQ8_gain, BEQ9_gain, BEQ10_gain, BEQ11_gain, BEQ12_gain,
                   matrix_gain, MPO_level, FBC_Enable, Cal_Input, Mic_Cal;

        public Config[] SpinNRConfig = new Config[2];

        public int number_of_programs, VC_MAP, VC_Range, VC_pos, TK_MAP, HC_MAP, LC_MAP, MPO_MAP, T1_DIR, T2_DIR, T3_DIR,
                   CoilPGM, MANF_ID, OutMode, Switch_Tone, Low_Batt_Warning, Tone_Frequency, Tone_Level, ATC, TimeConstants,
                   Mic_Expansion, reserved1, reserved2, reserved3, reserved4, test, T1_POS, T2_POS, T3_POS,
                   MANF_reserve_1, MANF_reserve_2, MANF_reserve_3, MANF_reserve_4, MANF_reserve_5,
                   MANF_reserve_6, MANF_reserve_7, MANF_reserve_8, MANF_reserve_9, MANF_reserve_10;

        public TargetParams target;

        public DetectData detdata;

        public int[] detectedHI = { 0, 0 };
        public int detectProgrammer = 0;

        public WaveRule waveRule;

        public Response micFR;
        public Response recFR;
        public Response FreqResp;

        private FileUtilities fileUtils = new FileUtilities();

        public string defaultReceptor, defaultMicrophone;

        public bool loaded = false;

        public SpinNRViewModel()
        {
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
            this.errorCode = Initialize();
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
                Growl.ErrorGlobal("Read: " + DriverErrorString(this.errorCode));
            }
            else
            {
                this.errorCode = Get_params(this.DLLVER, ref this.SpinNRParams[this.activeSide]);
                if (this.errorCode != 0)
                {
                    Growl.ErrorGlobal("Read Get_params: " + DriverErrorString(this.errorCode));
                    return;
                }
                else
                {
                    this.errorCode = Get_config(this.DLLVER, ref this.SpinNRConfig[this.activeSide]);
                    if (this.errorCode != 0)
                    {
                        Growl.ErrorGlobal("Read Get_config: " + DriverErrorString(this.errorCode));
                        return;
                    }
                    else
                    {
                        this.errorCode = Detect(ref this.detdata);
                        if (this.errorCode != 0)
                        {
                            Growl.ErrorGlobal("Detect: " + DriverErrorString(this.errorCode));
                            return;
                        }
                        else
                        {
                            Growl.SuccessGlobal("Parametros lidos com sucesso.");
                        }
                    }
                }
            }
            UpdateParamBoxesFromParams(activeSide, activeProgram);
            UpdateConfigBoxesFromConfigParams(activeSide);
        }

        public void UpdateParamBoxesFromParams(int activeSide, int activeProgram)
        {
            input_mux = SpinNRParams[activeSide].input_mux[activeProgram];
            preamp_gain0 = SpinNRParams[activeSide].preamp_gain0[activeProgram];
            preamp_gain1 = SpinNRParams[activeSide].preamp_gain1[activeProgram];
            CRL = SpinNRParams[activeSide].CRL[activeProgram];
            CRH = SpinNRParams[activeSide].CRH[activeProgram];
            threshold = SpinNRParams[activeSide].threshold[activeProgram];
            Low_Cut = SpinNRParams[activeSide].Low_Cut[activeProgram];
            High_Cut = SpinNRParams[activeSide].High_Cut[activeProgram];
            Noise_Reduction = SpinNRParams[activeSide].Noise_Reduction[activeProgram];
            BEQ1_gain = SpinNRParams[activeSide].BEQ1_gain[activeProgram];
            BEQ2_gain = SpinNRParams[activeSide].BEQ2_gain[activeProgram];
            BEQ3_gain = SpinNRParams[activeSide].BEQ3_gain[activeProgram];
            BEQ4_gain = SpinNRParams[activeSide].BEQ4_gain[activeProgram];
            BEQ5_gain = SpinNRParams[activeSide].BEQ5_gain[activeProgram];
            BEQ6_gain = SpinNRParams[activeSide].BEQ6_gain[activeProgram];
            BEQ7_gain = SpinNRParams[activeSide].BEQ7_gain[activeProgram];
            BEQ8_gain = SpinNRParams[activeSide].BEQ8_gain[activeProgram];
            BEQ9_gain = SpinNRParams[activeSide].BEQ9_gain[activeProgram];
            BEQ10_gain = SpinNRParams[activeSide].BEQ10_gain[activeProgram];
            BEQ11_gain = SpinNRParams[activeSide].BEQ11_gain[activeProgram];
            BEQ12_gain = SpinNRParams[activeSide].BEQ12_gain[activeProgram];
            matrix_gain = SpinNRParams[activeSide].matrix_gain[activeProgram];
            MPO_level = SpinNRParams[activeSide].MPO_level[activeProgram];
            FBC_Enable = SpinNRParams[activeSide].FBC_Enable[activeProgram];
            Cal_Input = SpinNRParams[activeSide].Cal_Input[activeProgram];
            Mic_Cal = SpinNRParams[activeSide].Mic_Cal[activeProgram];
        }

        public void UpdateConfigBoxesFromConfigParams(int activeSide)
        {
            number_of_programs = SpinNRConfig[activeSide].number_of_programs;
            VC_MAP = SpinNRConfig[activeSide].VC_MAP;
            VC_Range = SpinNRConfig[activeSide].VC_Range;
            VC_pos = SpinNRConfig[activeSide].VC_pos;
            TK_MAP = SpinNRConfig[activeSide].TK_MAP;
            HC_MAP = SpinNRConfig[activeSide].HC_MAP;
            LC_MAP = SpinNRConfig[activeSide].LC_MAP;
            MPO_MAP = SpinNRConfig[activeSide].MPO_MAP;
            T1_DIR = SpinNRConfig[activeSide].T1_DIR;
            T2_DIR = SpinNRConfig[activeSide].T2_DIR;
            T3_DIR = SpinNRConfig[activeSide].T3_DIR;
            CoilPGM = SpinNRConfig[activeSide].CoilPGM;
            MANF_ID = SpinNRConfig[activeSide].MANF_ID;
            OutMode = SpinNRConfig[activeSide].OutMode;
            Switch_Tone = SpinNRConfig[activeSide].Switch_Tone;
            Low_Batt_Warning = SpinNRConfig[activeSide].Low_Batt_Warning;
            Tone_Frequency = SpinNRConfig[activeSide].Tone_Frequency;
            Tone_Level = SpinNRConfig[activeSide].Tone_Level;
            ATC = SpinNRConfig[activeSide].ATC;
            TimeConstants = SpinNRConfig[activeSide].TimeConstants;
            Mic_Expansion = SpinNRConfig[activeSide].Mic_Expansion;
            reserved1 = SpinNRConfig[activeSide].reserved1;
            reserved2 = SpinNRConfig[activeSide].reserved2;
            reserved3 = SpinNRConfig[activeSide].reserved3;
            reserved4 = SpinNRConfig[activeSide].reserved4;
            test = SpinNRConfig[activeSide].test;
            T1_POS = SpinNRConfig[activeSide].T1_POS;
            T2_POS = SpinNRConfig[activeSide].T2_POS;
            T3_POS = SpinNRConfig[activeSide].T3_POS;

            MANF_reserve_1 = this.detdata.MANF_reserve_1;
            MANF_reserve_2 = this.detdata.MANF_reserve_2;
            MANF_reserve_3 = this.detdata.MANF_reserve_3;
            MANF_reserve_4 = this.detdata.MANF_reserve_4;
            MANF_reserve_5 = this.detdata.MANF_reserve_5;
            MANF_reserve_6 = this.detdata.MANF_reserve_6;
            MANF_reserve_7 = this.detdata.MANF_reserve_7;
            MANF_reserve_8 = this.detdata.MANF_reserve_8;
            MANF_reserve_9 = this.detdata.MANF_reserve_9;
            MANF_reserve_10 = this.detdata.MANF_reserve_10;

            Console.WriteLine("Detect Data: ");
            Console.WriteLine("Platform_ID     = " + this.detdata.Platform_ID);
            Console.WriteLine("MANF_ID         = " + this.detdata.MANF_ID);

            Console.WriteLine($"reserved1: {reserved1}");
            Console.WriteLine($"reserved2: {reserved2}");
            Console.WriteLine($"reserved3: {reserved3}");

            Console.WriteLine($"MANF_reserve_1: {MANF_reserve_1}");
            Console.WriteLine($"MANF_reserve_2: {MANF_reserve_2}");
            Console.WriteLine($"MANF_reserve_3: {MANF_reserve_3}");
            Console.WriteLine($"MANF_reserve_4: {MANF_reserve_4}");
            Console.WriteLine($"MANF_reserve_5: {MANF_reserve_5}");

            Console.WriteLine("MANF_reserve_1  = " + SpinNRConfig[activeSide].MANF_reserve_1);
            Console.WriteLine("MANF_reserve_2  = " + SpinNRConfig[activeSide].MANF_reserve_2);
            Console.WriteLine("MANF_reserve_3  = " + SpinNRConfig[activeSide].MANF_reserve_3);
            Console.WriteLine("MANF_reserve_4  = " + SpinNRConfig[activeSide].MANF_reserve_4);
            Console.WriteLine("MANF_reserve_5  = " + SpinNRConfig[activeSide].MANF_reserve_5);
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

            this.errorCode = Set_params(this.DLLVER, ref this.SpinNRParams[this.activeSide]);

            if (this.errorCode != 0)
            {
                Console.WriteLine("Load Set_params: " + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Set_config(this.DLLVER, ref this.SpinNRConfig[this.activeSide]);

                if (this.errorCode != 0)
                {
                    Console.WriteLine("Load Set_config:" + DriverErrorString(errorCode));
                    //Growl.ErrorGlobal(DriverErrorString(errorCode));
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
                SpinNRParams[activeSide].input_mux[activeProgram] = Convert.ToInt16(input_mux);
                SpinNRParams[activeSide].preamp_gain0[activeProgram] = Convert.ToInt16(preamp_gain0);
                SpinNRParams[activeSide].preamp_gain1[activeProgram] = Convert.ToInt16(preamp_gain1);
                SpinNRParams[activeSide].CRL[activeProgram] = Convert.ToInt16(CRL);
                SpinNRParams[activeSide].CRH[activeProgram] = Convert.ToInt16(CRH);
                SpinNRParams[activeSide].threshold[activeProgram] = Convert.ToInt16(threshold);
                SpinNRParams[activeSide].Low_Cut[activeProgram] = Convert.ToInt16(Low_Cut);
                SpinNRParams[activeSide].High_Cut[activeProgram] = Convert.ToInt16(High_Cut);
                SpinNRParams[activeSide].Noise_Reduction[activeProgram] = Convert.ToInt16(Noise_Reduction);
                SpinNRParams[activeSide].BEQ1_gain[activeProgram] = Convert.ToInt16(BEQ1_gain);
                SpinNRParams[activeSide].BEQ2_gain[activeProgram] = Convert.ToInt16(BEQ2_gain);
                SpinNRParams[activeSide].BEQ3_gain[activeProgram] = Convert.ToInt16(BEQ3_gain);
                SpinNRParams[activeSide].BEQ4_gain[activeProgram] = Convert.ToInt16(BEQ4_gain);
                SpinNRParams[activeSide].BEQ5_gain[activeProgram] = Convert.ToInt16(BEQ5_gain);
                SpinNRParams[activeSide].BEQ6_gain[activeProgram] = Convert.ToInt16(BEQ6_gain);
                SpinNRParams[activeSide].BEQ7_gain[activeProgram] = Convert.ToInt16(BEQ7_gain);
                SpinNRParams[activeSide].BEQ8_gain[activeProgram] = Convert.ToInt16(BEQ8_gain);
                SpinNRParams[activeSide].BEQ9_gain[activeProgram] = Convert.ToInt16(BEQ9_gain);
                SpinNRParams[activeSide].BEQ10_gain[activeProgram] = Convert.ToInt16(BEQ10_gain);
                SpinNRParams[activeSide].BEQ11_gain[activeProgram] = Convert.ToInt16(BEQ11_gain);
                SpinNRParams[activeSide].BEQ12_gain[activeProgram] = Convert.ToInt16(BEQ12_gain);
                SpinNRParams[activeSide].matrix_gain[activeProgram] = Convert.ToInt16(matrix_gain);
                SpinNRParams[activeSide].MPO_level[activeProgram] = Convert.ToInt16(MPO_level);
                SpinNRParams[activeSide].FBC_Enable[activeProgram] = Convert.ToInt16(FBC_Enable);
                SpinNRParams[activeSide].Cal_Input[activeProgram] = Convert.ToInt16(Cal_Input);
                SpinNRParams[activeSide].Mic_Cal[activeProgram] = Convert.ToInt16(Mic_Cal);
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
                SpinNRConfig[activeSide].number_of_programs = Convert.ToInt16(number_of_programs);
                SpinNRConfig[activeSide].VC_MAP = Convert.ToInt16(VC_MAP);
                SpinNRConfig[activeSide].VC_Range = Convert.ToInt16(VC_Range);
                SpinNRConfig[activeSide].VC_pos = Convert.ToInt16(VC_pos);
                SpinNRConfig[activeSide].TK_MAP = Convert.ToInt16(TK_MAP);
                SpinNRConfig[activeSide].HC_MAP = Convert.ToInt16(HC_MAP);
                SpinNRConfig[activeSide].LC_MAP = Convert.ToInt16(LC_MAP);
                SpinNRConfig[activeSide].MPO_MAP = Convert.ToInt16(MPO_MAP);
                SpinNRConfig[activeSide].T1_DIR = Convert.ToInt16(T1_DIR);
                SpinNRConfig[activeSide].T2_DIR = Convert.ToInt16(T2_DIR);
                SpinNRConfig[activeSide].T3_DIR = Convert.ToInt16(T3_DIR);
                SpinNRConfig[activeSide].CoilPGM = Convert.ToInt16(CoilPGM);
                SpinNRConfig[activeSide].MANF_ID = Convert.ToInt16(MANF_ID);
                SpinNRConfig[activeSide].OutMode = Convert.ToInt16(OutMode);
                SpinNRConfig[activeSide].Switch_Tone = Convert.ToInt16(Switch_Tone);
                SpinNRConfig[activeSide].Low_Batt_Warning = Convert.ToInt16(Low_Batt_Warning);
                SpinNRConfig[activeSide].Tone_Frequency = Convert.ToInt16(Tone_Frequency);
                SpinNRConfig[activeSide].Tone_Level = Convert.ToInt16(Tone_Level);
                SpinNRConfig[activeSide].ATC = Convert.ToInt16(ATC);
                SpinNRConfig[activeSide].TimeConstants = Convert.ToInt16(TimeConstants);
                SpinNRConfig[activeSide].Mic_Expansion = Convert.ToInt16(Mic_Expansion);

                SpinNRConfig[activeSide].reserved1 = Convert.ToInt16(reserved1);
                SpinNRConfig[activeSide].reserved2 = Convert.ToInt16(reserved2);
                SpinNRConfig[activeSide].reserved3 = Convert.ToInt16(reserved3);
                SpinNRConfig[activeSide].reserved4 = Convert.ToInt16(reserved4);
                SpinNRConfig[activeSide].test = Convert.ToInt16(test);
                SpinNRConfig[activeSide].T1_POS = Convert.ToInt16(T1_POS);
                SpinNRConfig[activeSide].T2_POS = Convert.ToInt16(T2_POS);
                SpinNRConfig[activeSide].T3_POS = Convert.ToInt16(T3_POS);

                SpinNRConfig[activeSide].MANF_reserve_1 = MANF_reserve_1;
                SpinNRConfig[activeSide].MANF_reserve_2 = MANF_reserve_2;
                SpinNRConfig[activeSide].MANF_reserve_3 = MANF_reserve_3;
                SpinNRConfig[activeSide].MANF_reserve_4 = MANF_reserve_4;
                SpinNRConfig[activeSide].MANF_reserve_5 = MANF_reserve_5;
                SpinNRConfig[activeSide].MANF_reserve_6 = MANF_reserve_6;
                SpinNRConfig[activeSide].MANF_reserve_7 = MANF_reserve_7;
                SpinNRConfig[activeSide].MANF_reserve_8 = MANF_reserve_8;
                SpinNRConfig[activeSide].MANF_reserve_9 = MANF_reserve_9;
                SpinNRConfig[activeSide].MANF_reserve_10 = MANF_reserve_10;
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

        public bool PopulateTarget2FromControls(ref TargetParams target, WaveRule waveRule)
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

                target.MPO = (short)waveRule.CalculateUCL(activeSide);
                target.ResGain = 6;

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
            catch (Exception err)
            {
                return false;
            }
        }

        private void loadMicModel()
        {
            this.fileUtils = new FileUtilities();
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
            target = new TargetParams(true);
            short MGC = -20;

            if (!PopulateTarget2FromControls(ref target, waveRule)) return;

            GetMicRec();

            this.errorCode = AutoFit(16, 0, ref target);
            if (this.errorCode != 0)
            {
                Console.WriteLine("Autofit() error:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Get_params(this.DLLVER, ref this.SpinNRParams[this.activeSide]);
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
            TestTone(program);
        }
    }
}