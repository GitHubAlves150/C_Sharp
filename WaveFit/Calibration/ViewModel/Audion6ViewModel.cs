using Audion8TestApp;
using HandyControl.Controls;
using IAmp;
using System;
using System.IO;
using WaveFit2.Calibration.Class;

namespace WaveFit2.Calibration.ViewModel
{
    public class Audion6ViewModel : Audion6
    {
        public bool ProgrammerInitialized;
        public int errorCode;

        public Int16 activeProgram;
        public Int16 activeSide;
        public Int16 DLLVER = 1;

        public ParamsClassic[] Audion6Params = new ParamsClassic[2];
        public Config[] Audion6Config = new Config[2];
        public MDA[] Audion6MDA = new MDA[2];

        public int ActiveProgram, BEQ1_gain, BEQ2_gain, BEQ3_gain, BEQ4_gain, BEQ5_gain, BEQ6_gain,
            BEQ7_gain, BEQ8_gain, BEQ9_gain, BEQ10_gain, BEQ11_gain, BEQ12_gain,
            C1_ExpTK, C2_ExpTK, C3_ExpTK, C4_ExpTK, C5_ExpTK, C6_ExpTK,
            C1_MPO, C2_MPO, C3_MPO, C4_MPO, C5_MPO, C6_MPO,
            C1_Ratio, C2_Ratio, C3_Ratio, C4_Ratio, C5_Ratio, C6_Ratio,
            C1_TK, C2_TK, C3_TK, C4_TK, C5_TK, C6_TK,
            Exp_Attack, Exp_Ratio, Exp_Release, FBC_Enable, input_mux, matrix_gain,
            MPO_Attack, MPO_Release, Noise_Reduction, preamp_gain0, preamp_gain1,
            TimeConstants1, TimeConstants2, TimeConstants3, TimeConstants4, TimeConstants5, TimeConstants6,
            VcPosition;

        public int Auto_Telecoil_Enable, Cal_Input,
            Dir_Spacing, Low_Battery_Warning, Mic_Cal,
            number_of_programs, Output_Mode,
            Power_On_Delay, Power_On_Level,
            Program_Beep_Enable, Program_StartUp, Switch_Mode,
            Tone_Frequency, Tone_Level, VC_AnalogRange, VC_Enable,
            VC_Mode, VC_DigitalNumSteps, VC_StartUp, VC_DigitalStepSize;

        public int MANF_reserve_1, MANF_reserve_2, MANF_reserve_3, MANF_reserve_4, MANF_reserve_5,
            MANF_reserve_6, MANF_reserve_7, MANF_reserve_8, MANF_reserve_9, MANF_reserve_10;

        public TargetParams target;
        public DetectData detdata;

        public WaveRule waveRule;

        public Response micFR;
        public Response recFR;
        public Response FreqResp;

        private FileUtilities fileUtils = new FileUtilities();

        public string defaultReceptor, defaultMicrophone;

        public Audion6ViewModel()
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
                this.errorCode = Get_paramsClassic(this.DLLVER, ref this.Audion6Params[this.activeSide]);
                if (this.errorCode != 0)
                {
                    Console.WriteLine("Read Get_params:" + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));

                    return;
                }
                else
                {
                    this.errorCode = Get_config(this.DLLVER, ref this.Audion6Config[this.activeSide]);
                    if (this.errorCode != 0)
                    {
                        Console.WriteLine("Read Get_config:" + DriverErrorString(errorCode));
                        Growl.ErrorGlobal(DriverErrorString(errorCode));
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
                            Growl.SuccessGlobal("Parameters read successfully.");
                        }
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
                BEQ1_gain = Audion6Params[activeSide].BEQ1_gain[activeProgram];
                BEQ2_gain = Audion6Params[activeSide].BEQ2_gain[activeProgram];
                BEQ3_gain = Audion6Params[activeSide].BEQ3_gain[activeProgram];
                BEQ4_gain = Audion6Params[activeSide].BEQ4_gain[activeProgram];
                BEQ5_gain = Audion6Params[activeSide].BEQ5_gain[activeProgram];
                BEQ6_gain = Audion6Params[activeSide].BEQ6_gain[activeProgram];
                BEQ7_gain = Audion6Params[activeSide].BEQ7_gain[activeProgram];
                BEQ8_gain = Audion6Params[activeSide].BEQ8_gain[activeProgram];
                BEQ9_gain = Audion6Params[activeSide].BEQ9_gain[activeProgram];
                BEQ10_gain = Audion6Params[activeSide].BEQ10_gain[activeProgram];
                BEQ11_gain = Audion6Params[activeSide].BEQ11_gain[activeProgram];
                BEQ12_gain = Audion6Params[activeSide].BEQ12_gain[activeProgram];

                C1_ExpTK = Audion6Params[activeSide].C1_ExpTK[activeProgram];
                C2_ExpTK = Audion6Params[activeSide].C2_ExpTK[activeProgram];
                C3_ExpTK = Audion6Params[activeSide].C3_ExpTK[activeProgram];
                C4_ExpTK = Audion6Params[activeSide].C4_ExpTK[activeProgram];
                C5_ExpTK = Audion6Params[activeSide].C5_ExpTK[activeProgram];
                C6_ExpTK = Audion6Params[activeSide].C6_ExpTK[activeProgram];

                C1_MPO = Audion6Params[activeSide].C1_MPO[activeProgram];
                C2_MPO = Audion6Params[activeSide].C2_MPO[activeProgram];
                C3_MPO = Audion6Params[activeSide].C3_MPO[activeProgram];
                C4_MPO = Audion6Params[activeSide].C4_MPO[activeProgram];
                C5_MPO = Audion6Params[activeSide].C5_MPO[activeProgram];
                C6_MPO = Audion6Params[activeSide].C6_MPO[activeProgram];

                C1_Ratio = Audion6Params[activeSide].C1_Ratio[activeProgram];
                C2_Ratio = Audion6Params[activeSide].C2_Ratio[activeProgram];
                C3_Ratio = Audion6Params[activeSide].C3_Ratio[activeProgram];
                C4_Ratio = Audion6Params[activeSide].C4_Ratio[activeProgram];
                C5_Ratio = Audion6Params[activeSide].C5_Ratio[activeProgram];
                C6_Ratio = Audion6Params[activeSide].C6_Ratio[activeProgram];

                C1_TK = Audion6Params[activeSide].C1_TK[activeProgram];
                C2_TK = Audion6Params[activeSide].C2_TK[activeProgram];
                C3_TK = Audion6Params[activeSide].C3_TK[activeProgram];
                C4_TK = Audion6Params[activeSide].C4_TK[activeProgram];
                C5_TK = Audion6Params[activeSide].C5_TK[activeProgram];
                C6_TK = Audion6Params[activeSide].C6_TK[activeProgram];

                Exp_Attack = Audion6Params[activeSide].Exp_Attack[activeProgram];
                Exp_Ratio = Audion6Params[activeSide].Exp_Ratio[activeProgram];
                Exp_Release = Audion6Params[activeSide].Exp_Release[activeProgram];

                FBC_Enable = Audion6Params[activeSide].FBC_Enable[activeProgram];

                input_mux = Audion6Params[activeSide].input_mux[activeProgram];
                matrix_gain = Audion6Params[activeSide].matrix_gain[activeProgram];

                MPO_Attack = Audion6Params[activeSide].MPO_Attack[activeProgram];
                MPO_Release = Audion6Params[activeSide].MPO_Release[activeProgram];

                Noise_Reduction = Audion6Params[activeSide].Noise_Reduction[activeProgram];

                preamp_gain0 = Audion6Params[activeSide].preamp_gain0[activeProgram];
                preamp_gain1 = Audion6Params[activeSide].preamp_gain1[activeProgram];

                TimeConstants1 = Audion6Params[activeSide].TimeConstants1[activeProgram];
                TimeConstants2 = Audion6Params[activeSide].TimeConstants2[activeProgram];
                TimeConstants3 = Audion6Params[activeSide].TimeConstants3[activeProgram];
                TimeConstants4 = Audion6Params[activeSide].TimeConstants4[activeProgram];
                TimeConstants5 = Audion6Params[activeSide].TimeConstants5[activeProgram];
                TimeConstants6 = Audion6Params[activeSide].TimeConstants6[activeProgram];

                //Console.WriteLine($"ActiveProgram: {ActiveProgram}");

                //Console.WriteLine($"BEQ1_gain: {BEQ1_gain}");
                //Console.WriteLine($"BEQ2_gain: {BEQ2_gain}");
                //Console.WriteLine($"BEQ3_gain: {BEQ3_gain}");
                //Console.WriteLine($"BEQ4_gain: {BEQ4_gain}");
                //Console.WriteLine($"BEQ5_gain: {BEQ5_gain}");
                //Console.WriteLine($"BEQ6_gain: {BEQ6_gain}");
                //Console.WriteLine($"BEQ7_gain: {BEQ7_gain}");
                //Console.WriteLine($"BEQ8_gain: {BEQ8_gain}");
                //Console.WriteLine($"BEQ9_gain: {BEQ9_gain}");
                //Console.WriteLine($"BEQ10_gain: {BEQ10_gain}");
                //Console.WriteLine($"BEQ11_gain: {BEQ11_gain}");
                //Console.WriteLine($"BEQ12_gain: {BEQ12_gain}");

                //Console.WriteLine($"C1_ExpTK: {C1_ExpTK}");
                //Console.WriteLine($"C2_ExpTK: {C2_ExpTK}");
                //Console.WriteLine($"C3_ExpTK: {C3_ExpTK}");
                //Console.WriteLine($"C4_ExpTK: {C4_ExpTK}");
                //Console.WriteLine($"C5_ExpTK: {C5_ExpTK}");
                //Console.WriteLine($"C6_ExpTK: {C6_ExpTK}");

                //Console.WriteLine($"C1_MPO: {C1_MPO}");
                //Console.WriteLine($"C2_MPO: {C2_MPO}");
                //Console.WriteLine($"C3_MPO: {C3_MPO}");
                //Console.WriteLine($"C4_MPO: {C4_MPO}");
                //Console.WriteLine($"C5_MPO: {C5_MPO}");
                //Console.WriteLine($"C6_MPO: {C6_MPO}");

                //Console.WriteLine($"C1_Ratio: {C1_Ratio}");
                //Console.WriteLine($"C2_Ratio: {C2_Ratio}");
                //Console.WriteLine($"C3_Ratio: {C3_Ratio}");
                //Console.WriteLine($"C4_Ratio: {C4_Ratio}");
                //Console.WriteLine($"C5_Ratio: {C5_Ratio}");
                //Console.WriteLine($"C6_Ratio: {C6_Ratio}");

                //Console.WriteLine($"C1_TK: {C1_TK}");
                //Console.WriteLine($"C2_TK: {C2_TK}");
                //Console.WriteLine($"C3_TK: {C3_TK}");
                //Console.WriteLine($"C4_TK: {C4_TK}");
                //Console.WriteLine($"C5_TK: {C5_TK}");
                //Console.WriteLine($"C6_TK: {C6_TK}");

                //Console.WriteLine($"Exp_Attack: {Exp_Attack}");
                //Console.WriteLine($"Exp_Ratio: {Exp_Ratio}");
                //Console.WriteLine($"Exp_Release: {Exp_Release}");

                //Console.WriteLine($"FBC_Enable: {FBC_Enable}");

                //Console.WriteLine($"input_mux: {input_mux}");
                //Console.WriteLine($"matrix_gain: {matrix_gain}");

                //Console.WriteLine($"MPO_Attack: {MPO_Attack}");
                //Console.WriteLine($"MPO_Release: {MPO_Release}");

                //Console.WriteLine($"Noise_Reduction: {Noise_Reduction}");

                //Console.WriteLine($"preamp_gain0: {preamp_gain0}");
                //Console.WriteLine($"preamp_gain1: {preamp_gain1}");

                //Console.WriteLine($"TimeConstants1: {TimeConstants1}");
                //Console.WriteLine($"TimeConstants2: {TimeConstants2}");
                //Console.WriteLine($"TimeConstants3: {TimeConstants3}");
                //Console.WriteLine($"TimeConstants4: {TimeConstants4}");
                //Console.WriteLine($"TimeConstants5: {TimeConstants5}");
                //Console.WriteLine($"TimeConstants6: {TimeConstants6}");

                //Console.WriteLine($"VcPosition: {VcPosition}");
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
                Auto_Telecoil_Enable = Audion6Config[activeSide].Auto_Telecoil_Enable;
                Cal_Input = Audion6Config[activeSide].Cal_Input;
                Dir_Spacing = Audion6Config[activeSide].Dir_Spacing;
                Low_Battery_Warning = Audion6Config[activeSide].Low_Battery_Warning;
                Mic_Cal = Audion6Config[activeSide].Mic_Cal;
                number_of_programs = Audion6Config[activeSide].number_of_programs;
                Output_Mode = Audion6Config[activeSide].Output_Mode;
                Power_On_Delay = Audion6Config[activeSide].Power_On_Delay;
                Power_On_Level = Audion6Config[activeSide].Power_On_Level;
                Program_Beep_Enable = Audion6Config[activeSide].Program_Beep_Enable;
                Program_StartUp = Audion6Config[activeSide].Program_StartUp;
                Switch_Mode = Audion6Config[activeSide].Switch_Mode;
                Tone_Frequency = Audion6Config[activeSide].Tone_Frequency;
                Tone_Level = Audion6Config[activeSide].Tone_Level;
                VC_AnalogRange = Audion6Config[activeSide].VC_AnalogRange;
                VC_Enable = Audion6Config[activeSide].VC_Enable;
                VC_Mode = Audion6Config[activeSide].VC_Mode;
                VC_DigitalNumSteps = Audion6Config[activeSide].VC_DigitalNumSteps;
                VC_StartUp = Audion6Config[activeSide].VC_StartUp;
                VC_DigitalStepSize = Audion6Config[activeSide].VC_DigitalStepSize;

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

                Console.WriteLine($"MANF_reserve_1: {MANF_reserve_1}");
                Console.WriteLine($"MANF_reserve_2: {MANF_reserve_2}");
                Console.WriteLine($"MANF_reserve_3: {MANF_reserve_3}");
                Console.WriteLine($"MANF_reserve_4: {MANF_reserve_4}");
                Console.WriteLine($"MANF_reserve_5: {MANF_reserve_5}");
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

            this.errorCode = Set_paramsClassic(this.DLLVER, ref this.Audion6Params[this.activeSide]);

            if (this.errorCode != 0)
            {
                Console.WriteLine("Load Set_params: " + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Set_config(this.DLLVER, ref this.Audion6Config[this.activeSide]);

                if (this.errorCode != 0)
                {
                    Console.WriteLine("Load Set_config:" + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                }
                else if (doLoad)
                {
                    this.errorCode = Load(-1);
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
                Audion6Params[activeSide].BEQ1_gain[activeProgram] = Convert.ToInt16(BEQ1_gain);
                Audion6Params[activeSide].BEQ2_gain[activeProgram] = Convert.ToInt16(BEQ2_gain);
                Audion6Params[activeSide].BEQ3_gain[activeProgram] = Convert.ToInt16(BEQ3_gain);
                Audion6Params[activeSide].BEQ4_gain[activeProgram] = Convert.ToInt16(BEQ4_gain);
                Audion6Params[activeSide].BEQ5_gain[activeProgram] = Convert.ToInt16(BEQ5_gain);
                Audion6Params[activeSide].BEQ6_gain[activeProgram] = Convert.ToInt16(BEQ6_gain);
                Audion6Params[activeSide].BEQ7_gain[activeProgram] = Convert.ToInt16(BEQ7_gain);
                Audion6Params[activeSide].BEQ8_gain[activeProgram] = Convert.ToInt16(BEQ8_gain);
                Audion6Params[activeSide].BEQ9_gain[activeProgram] = Convert.ToInt16(BEQ9_gain);
                Audion6Params[activeSide].BEQ10_gain[activeProgram] = Convert.ToInt16(BEQ10_gain);
                Audion6Params[activeSide].BEQ11_gain[activeProgram] = Convert.ToInt16(BEQ11_gain);
                Audion6Params[activeSide].BEQ12_gain[activeProgram] = Convert.ToInt16(BEQ12_gain);

                Audion6Params[activeSide].C1_ExpTK[activeProgram] = Convert.ToInt16(C1_ExpTK);
                Audion6Params[activeSide].C2_ExpTK[activeProgram] = Convert.ToInt16(C2_ExpTK);
                Audion6Params[activeSide].C3_ExpTK[activeProgram] = Convert.ToInt16(C3_ExpTK);
                Audion6Params[activeSide].C4_ExpTK[activeProgram] = Convert.ToInt16(C4_ExpTK);
                Audion6Params[activeSide].C5_ExpTK[activeProgram] = Convert.ToInt16(C5_ExpTK);
                Audion6Params[activeSide].C6_ExpTK[activeProgram] = Convert.ToInt16(C6_ExpTK);

                Audion6Params[activeSide].C1_MPO[activeProgram] = Convert.ToInt16(C1_MPO);
                Audion6Params[activeSide].C2_MPO[activeProgram] = Convert.ToInt16(C2_MPO);
                Audion6Params[activeSide].C3_MPO[activeProgram] = Convert.ToInt16(C3_MPO);
                Audion6Params[activeSide].C4_MPO[activeProgram] = Convert.ToInt16(C4_MPO);
                Audion6Params[activeSide].C5_MPO[activeProgram] = Convert.ToInt16(C5_MPO);
                Audion6Params[activeSide].C6_MPO[activeProgram] = Convert.ToInt16(C6_MPO);

                Audion6Params[activeSide].C1_Ratio[activeProgram] = Convert.ToInt16(C1_Ratio);
                Audion6Params[activeSide].C2_Ratio[activeProgram] = Convert.ToInt16(C2_Ratio);
                Audion6Params[activeSide].C3_Ratio[activeProgram] = Convert.ToInt16(C3_Ratio);
                Audion6Params[activeSide].C4_Ratio[activeProgram] = Convert.ToInt16(C4_Ratio);
                Audion6Params[activeSide].C5_Ratio[activeProgram] = Convert.ToInt16(C5_Ratio);
                Audion6Params[activeSide].C6_Ratio[activeProgram] = Convert.ToInt16(C6_Ratio);

                Audion6Params[activeSide].C1_TK[activeProgram] = Convert.ToInt16(C1_TK);
                Audion6Params[activeSide].C2_TK[activeProgram] = Convert.ToInt16(C2_TK);
                Audion6Params[activeSide].C3_TK[activeProgram] = Convert.ToInt16(C3_TK);
                Audion6Params[activeSide].C4_TK[activeProgram] = Convert.ToInt16(C4_TK);
                Audion6Params[activeSide].C5_TK[activeProgram] = Convert.ToInt16(C5_TK);
                Audion6Params[activeSide].C6_TK[activeProgram] = Convert.ToInt16(C6_TK);

                Audion6Params[activeSide].Exp_Attack[activeProgram] = Convert.ToInt16(Exp_Attack);
                Audion6Params[activeSide].Exp_Ratio[activeProgram] = Convert.ToInt16(Exp_Ratio);
                Audion6Params[activeSide].Exp_Release[activeProgram] = Convert.ToInt16(Exp_Release);

                Audion6Params[activeSide].FBC_Enable[activeProgram] = Convert.ToInt16(FBC_Enable);

                Audion6Params[activeSide].input_mux[activeProgram] = Convert.ToInt16(input_mux);
                Audion6Params[activeSide].matrix_gain[activeProgram] = Convert.ToInt16(matrix_gain);

                Audion6Params[activeSide].MPO_Attack[activeProgram] = Convert.ToInt16(MPO_Attack);
                Audion6Params[activeSide].MPO_Release[activeProgram] = Convert.ToInt16(MPO_Release);

                Audion6Params[activeSide].Noise_Reduction[activeProgram] = Convert.ToInt16(Noise_Reduction);

                Audion6Params[activeSide].preamp_gain0[activeProgram] = Convert.ToInt16(preamp_gain0);
                Audion6Params[activeSide].preamp_gain1[activeProgram] = Convert.ToInt16(preamp_gain1);

                Audion6Params[activeSide].TimeConstants1[activeProgram] = Convert.ToInt16(TimeConstants1);
                Audion6Params[activeSide].TimeConstants2[activeProgram] = Convert.ToInt16(TimeConstants2);
                Audion6Params[activeSide].TimeConstants3[activeProgram] = Convert.ToInt16(TimeConstants3);
                Audion6Params[activeSide].TimeConstants4[activeProgram] = Convert.ToInt16(TimeConstants4);
                Audion6Params[activeSide].TimeConstants5[activeProgram] = Convert.ToInt16(TimeConstants5);
                Audion6Params[activeSide].TimeConstants6[activeProgram] = Convert.ToInt16(TimeConstants6);
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
                Audion6Config[activeSide].Auto_Telecoil_Enable = Convert.ToInt16(Auto_Telecoil_Enable);
                Audion6Config[activeSide].Cal_Input = Convert.ToInt16(Cal_Input);
                Audion6Config[activeSide].Dir_Spacing = Convert.ToInt16(Dir_Spacing);
                Audion6Config[activeSide].Low_Battery_Warning = Convert.ToInt16(Low_Battery_Warning);
                Audion6Config[activeSide].Mic_Cal = Convert.ToInt16(Mic_Cal);
                Audion6Config[activeSide].number_of_programs = Convert.ToInt16(number_of_programs);
                Audion6Config[activeSide].Output_Mode = Convert.ToInt16(Output_Mode);
                Audion6Config[activeSide].Power_On_Delay = Convert.ToInt16(Power_On_Delay);
                Audion6Config[activeSide].Power_On_Level = Convert.ToInt16(Power_On_Level);
                Audion6Config[activeSide].Program_Beep_Enable = Convert.ToInt16(Program_Beep_Enable);
                Audion6Config[activeSide].Program_StartUp = Convert.ToInt16(Program_StartUp);
                Audion6Config[activeSide].Switch_Mode = Convert.ToInt16(Switch_Mode);
                Audion6Config[activeSide].Tone_Frequency = Convert.ToInt16(Tone_Frequency);
                Audion6Config[activeSide].Tone_Level = Convert.ToInt16(Tone_Level);
                Audion6Config[activeSide].VC_AnalogRange = Convert.ToInt16(VC_AnalogRange);
                Audion6Config[activeSide].VC_Enable = Convert.ToInt16(VC_Enable);
                Audion6Config[activeSide].VC_Mode = Convert.ToInt16(VC_Mode);
                Audion6Config[activeSide].VC_DigitalNumSteps = Convert.ToInt16(VC_DigitalNumSteps);
                Audion6Config[activeSide].VC_StartUp = Convert.ToInt16(VC_StartUp);
                Audion6Config[activeSide].VC_DigitalStepSize = Convert.ToInt16(VC_DigitalStepSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LockVM()
        {
            this.errorCode = Lock(-1);
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
                MessageBox.Show("Você deve ter valores numéricos válidos em todas as células de destino para fazer o ajuste automático. Tente novamente. Erro:\n\n" + err.Message);
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
                this.errorCode = Get_paramsClassic(this.DLLVER, ref this.Audion6Params[this.activeSide]);
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

        public void CopyProgramFromTo(short fromProgram, short toProgram, ref ParamsClassic Program)
        {
            CopyProgram(fromProgram, toProgram, ref Program);
        }

        public void TestAlert(short program)
        {
            TestTone(program);
        }
    }
}