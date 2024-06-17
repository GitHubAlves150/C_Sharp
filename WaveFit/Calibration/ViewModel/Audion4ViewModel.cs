using Audion8TestApp;
using HandyControl.Controls;
using IAmp;
using System;
using System.IO;
using WaveFit2.Calibration.Class;

namespace WaveFit2.Calibration.ViewModel
{
    public class Audion4ViewModel : Audion4
    {
        public bool ProgrammerInitialized;
        public int errorCode;

        public Int16 activeProgram;
        public Int16 activeSide;
        public Int16 DLLVER = 1;

        public Params[] Audion4Params = new Params[2];
        public Config[] Audion4Config = new Config[2];
        public MDA[] Audion4MDA = new MDA[2];

        public int BEQ1_gain, BEQ2_gain, BEQ3_gain, BEQ4_gain, BEQ5_gain, BEQ6_gain,
                   BEQ7_gain, BEQ8_gain, BEQ9_gain, BEQ10_gain, BEQ11_gain, BEQ12_gain,
                   C1_Ratio, C2_Ratio, C3_Ratio, C4_Ratio,
                   Expansion_Enable, FBC_Enable, High_Cut, input_mux, Low_Cut,
                   matrix_gain, MPO_level, Noise_Reduction, preamp_gain0, preamp_gain1, threshold;

        public int ATC, Auto_Save, Cal_Input, Dir_Spacing, Low_Batt_Warning, MAP_HC, MAP_LC, MAP_MPO, MAP_TK,
                   Mic_Cal, number_of_programs, Power_On_Level, Power_On_Delay, Program_StartUp, Out_Mode,
                   Switch_Mode, Switch_Tone, T1_DIR, T2_DIR, test, Tone_Frequency, Tone_Level, Time_Constants,
                   VC_AnalogRange, VC_Beep_Enable, VC_DigitalNumSteps, VC_DigitalStepSize, VC_Enable, VC_Mode, VC_Startup,
                   Active_PGM, T1_POS, T2_POS, VC_Pos;

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

        public Audion4ViewModel()
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
            if (this.errorCode != 0)
            {
                Console.WriteLine("Read:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Get_params(this.DLLVER, ref this.Audion4Params[this.activeSide]);
                if (this.errorCode != 0)
                {
                    Console.WriteLine("Read Get_params:" + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                    return;
                }
                else
                {
                    this.UpdateParamBoxesFromParams(activeSide, activeProgram);
                    Growl.SuccessGlobal("Parametros lidos com sucesso.");
                }
            }
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
                this.errorCode = Get_params(this.DLLVER, ref this.Audion4Params[this.activeSide]);
                if (this.errorCode != 0)
                {
                    Console.WriteLine("Read Get_params:" + DriverErrorString(errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                    return;
                }
                else
                {
                    this.errorCode = Get_config(this.DLLVER, ref this.Audion4Config[this.activeSide]);
                    if (this.errorCode != 0)
                    {
                        Console.WriteLine("Read Get_config:" + DriverErrorString(errorCode));
                        Growl.ErrorGlobal(DriverErrorString(errorCode));
                        return;
                    }
                    else
                    {
                        this.errorCode = Get_MDA(0, this.DLLVER, ref this.Audion4MDA[this.activeSide]);
                        if (this.errorCode != 0)
                        {
                            Console.WriteLine("Read Get_MDA:" + DriverErrorString(errorCode));
                            Growl.ErrorGlobal(DriverErrorString(errorCode));
                            return;
                        }
                        else
                        {
                            this.UpdateParamBoxesFromParams(activeSide, activeProgram);
                            this.UpdateConfigBoxesFromConfigParams(activeSide);
                            Growl.SuccessGlobal("Parametros lidos com sucesso.");
                        }
                    }
                }
            }
        }

        public void UpdateParamBoxesFromParams(int activeSide, int activeProgram)
        {
            try
            {
                BEQ1_gain = Audion4Params[activeSide].BEQ1_gain[activeProgram];
                BEQ2_gain = Audion4Params[activeSide].BEQ2_gain[activeProgram];
                BEQ3_gain = Audion4Params[activeSide].BEQ3_gain[activeProgram];
                BEQ4_gain = Audion4Params[activeSide].BEQ4_gain[activeProgram];
                BEQ5_gain = Audion4Params[activeSide].BEQ5_gain[activeProgram];
                BEQ6_gain = Audion4Params[activeSide].BEQ6_gain[activeProgram];
                BEQ7_gain = Audion4Params[activeSide].BEQ7_gain[activeProgram];
                BEQ8_gain = Audion4Params[activeSide].BEQ8_gain[activeProgram];
                BEQ9_gain = Audion4Params[activeSide].BEQ9_gain[activeProgram];
                BEQ10_gain = Audion4Params[activeSide].BEQ10_gain[activeProgram];
                BEQ11_gain = Audion4Params[activeSide].BEQ11_gain[activeProgram];
                BEQ12_gain = Audion4Params[activeSide].BEQ12_gain[activeProgram];
                C1_Ratio = Audion4Params[activeSide].C1_Ratio[activeProgram];
                C2_Ratio = Audion4Params[activeSide].C2_Ratio[activeProgram];
                C3_Ratio = Audion4Params[activeSide].C3_Ratio[activeProgram];
                C4_Ratio = Audion4Params[activeSide].C4_Ratio[activeProgram];
                Expansion_Enable = Audion4Params[activeSide].Expansion_Enable[activeProgram];
                FBC_Enable = Audion4Params[activeSide].FBC_Enable[activeProgram];
                High_Cut = Audion4Params[activeSide].High_Cut[activeProgram];
                input_mux = Audion4Params[activeSide].input_mux[activeProgram];
                Low_Cut = Audion4Params[activeSide].Low_Cut[activeProgram];
                matrix_gain = Audion4Params[activeSide].matrix_gain[activeProgram];
                MPO_level = Audion4Params[activeSide].MPO_level[activeProgram];
                Noise_Reduction = Audion4Params[activeSide].Noise_Reduction[activeProgram];
                preamp_gain0 = Audion4Params[activeSide].preamp_gain0[activeProgram];
                preamp_gain1 = Audion4Params[activeSide].preamp_gain1[activeProgram];
                threshold = Audion4Params[activeSide].threshold[activeProgram];
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
                ATC = Audion4Config[activeSide].ATC;
                Auto_Save = Audion4Config[activeSide].Auto_Save;
                Cal_Input = Audion4Config[activeSide].Cal_Input;
                Dir_Spacing = Audion4Config[activeSide].Dir_Spacing;
                Low_Batt_Warning = Audion4Config[activeSide].Low_Batt_Warning;
                MAP_HC = Audion4Config[activeSide].MAP_HC;
                MAP_LC = Audion4Config[activeSide].MAP_LC;
                MAP_MPO = Audion4Config[activeSide].MAP_MPO;
                MAP_TK = Audion4Config[activeSide].MAP_TK;
                Mic_Cal = Audion4Config[activeSide].Mic_Cal;
                number_of_programs = Audion4Config[activeSide].number_of_programs;
                Power_On_Level = Audion4Config[activeSide].Power_On_Level;
                Power_On_Delay = Audion4Config[activeSide].Power_On_Delay;
                Program_StartUp = Audion4Config[activeSide].Program_StartUp;
                Out_Mode = Audion4Config[activeSide].Out_Mode;
                Switch_Mode = Audion4Config[activeSide].Switch_Mode;
                Switch_Tone = Audion4Config[activeSide].Switch_Tone;
                T1_DIR = Audion4Config[activeSide].T1_DIR;
                T2_DIR = Audion4Config[activeSide].T2_DIR;
                test = Audion4Config[activeSide].test;
                Tone_Frequency = Audion4Config[activeSide].Tone_Frequency;
                Tone_Level = Audion4Config[activeSide].Tone_Level;
                Time_Constants = Audion4Config[activeSide].Time_Constants;
                VC_AnalogRange = Audion4Config[activeSide].VC_AnalogRange;
                VC_Beep_Enable = Audion4Config[activeSide].VC_Beep_Enable;
                VC_DigitalNumSteps = Audion4Config[activeSide].VC_DigitalNumSteps;
                VC_DigitalStepSize = Audion4Config[activeSide].VC_DigitalStepSize;
                VC_Enable = Audion4Config[activeSide].VC_Enable;
                VC_Mode = Audion4Config[activeSide].VC_Mode;
                VC_Startup = Audion4Config[activeSide].VC_Startup;
                Active_PGM = Audion4Config[activeSide].Active_PGM;
                T1_POS = Audion4Config[activeSide].T1_POS;
                T2_POS = Audion4Config[activeSide].T2_POS;
                VC_Pos = Audion4Config[activeSide].VC_Pos;

                MANF_reserve_1 = Audion4MDA[activeSide].MANF_reserve[0];
                MANF_reserve_2 = Audion4MDA[activeSide].MANF_reserve[1];
                MANF_reserve_3 = Audion4MDA[activeSide].MANF_reserve[2];
                MANF_reserve_4 = Audion4MDA[activeSide].MANF_reserve[3];
                MANF_reserve_5 = Audion4MDA[activeSide].MANF_reserve[4];
                MANF_reserve_6 = Audion4MDA[activeSide].MANF_reserve[5];
                MANF_reserve_7 = Audion4MDA[activeSide].MANF_reserve[6];
                MANF_reserve_8 = Audion4MDA[activeSide].MANF_reserve[7];
                MANF_reserve_9 = Audion4MDA[activeSide].MANF_reserve[8];
                MANF_reserve_10 = Audion4MDA[activeSide].MANF_reserve[9];

                Console.WriteLine("ATC: " + ATC);
                Console.WriteLine("Auto_Save: " + Auto_Save);
                Console.WriteLine("Cal_Input: " + Cal_Input);
                Console.WriteLine("Dir_Spacing: " + Dir_Spacing);
                Console.WriteLine("Low_Batt_Warning: " + Low_Batt_Warning);
                Console.WriteLine("MAP_HC: " + MAP_HC);
                Console.WriteLine("MAP_LC: " + MAP_LC);
                Console.WriteLine("MAP_MPO: " + MAP_MPO);
                Console.WriteLine("MAP_TK: " + MAP_TK);
                Console.WriteLine("Mic_Cal: " + Mic_Cal);
                Console.WriteLine("number_of_programs: " + number_of_programs);
                Console.WriteLine("Power_On_Level: " + Power_On_Level);
                Console.WriteLine("Power_On_Delay: " + Power_On_Delay);
                Console.WriteLine("Program_StartUp: " + Program_StartUp);
                Console.WriteLine("Out_Mode: " + Out_Mode);
                Console.WriteLine("Switch_Mode: " + Switch_Mode);
                Console.WriteLine("Switch_Tone: " + Switch_Tone);
                Console.WriteLine("T1_DIR: " + T1_DIR);
                Console.WriteLine("T2_DIR: " + T2_DIR);
                Console.WriteLine("test: " + test);
                Console.WriteLine("Tone_Frequency: " + Tone_Frequency);
                Console.WriteLine("Tone_Level: " + Tone_Level);
                Console.WriteLine("Time_Constants: " + Time_Constants);
                Console.WriteLine("VC_AnalogRange: " + VC_AnalogRange);
                Console.WriteLine("VC_Beep_Enable: " + VC_Beep_Enable);
                Console.WriteLine("VC_DigitalNumSteps: " + VC_DigitalNumSteps);
                Console.WriteLine("VC_DigitalStepSize: " + VC_DigitalStepSize);
                Console.WriteLine("VC_Enable: " + VC_Enable);
                Console.WriteLine("VC_Mode: " + VC_Mode);
                Console.WriteLine("VC_Startup: " + VC_Startup);
                Console.WriteLine("Active_PGM: " + Active_PGM);
                Console.WriteLine("T1_POS: " + T1_POS);
                Console.WriteLine("T2_POS: " + T2_POS);
                Console.WriteLine("VC_Pos: " + VC_Pos);

                Console.WriteLine("MANF_reserve_1: " + MANF_reserve_1);
                Console.WriteLine("MANF_reserve_2: " + MANF_reserve_2);
                Console.WriteLine("MANF_reserve_3: " + MANF_reserve_3);
                Console.WriteLine("MANF_reserve_4: " + MANF_reserve_4);
                Console.WriteLine("MANF_reserve_5: " + MANF_reserve_5);
                Console.WriteLine("MANF_reserve_6: " + MANF_reserve_6);
                Console.WriteLine("MANF_reserve_7: " + MANF_reserve_7);
                Console.WriteLine("MANF_reserve_8: " + MANF_reserve_8);
                Console.WriteLine("MANF_reserve_9: " + MANF_reserve_9);
                Console.WriteLine("MANF_reserve_10: " + MANF_reserve_10);
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

            this.errorCode = Set_params(this.DLLVER, ref this.Audion4Params[this.activeSide]);

            if (this.errorCode != 0)
            {
                Console.WriteLine("Load Set_params: " + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Set_config(this.DLLVER, ref this.Audion4Config[this.activeSide]);

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
                Audion4Params[activeSide].BEQ1_gain[activeProgram] = Convert.ToInt16(BEQ1_gain);
                Audion4Params[activeSide].BEQ2_gain[activeProgram] = Convert.ToInt16(BEQ2_gain);
                Audion4Params[activeSide].BEQ3_gain[activeProgram] = Convert.ToInt16(BEQ3_gain);
                Audion4Params[activeSide].BEQ4_gain[activeProgram] = Convert.ToInt16(BEQ4_gain);
                Audion4Params[activeSide].BEQ5_gain[activeProgram] = Convert.ToInt16(BEQ5_gain);
                Audion4Params[activeSide].BEQ6_gain[activeProgram] = Convert.ToInt16(BEQ6_gain);
                Audion4Params[activeSide].BEQ7_gain[activeProgram] = Convert.ToInt16(BEQ7_gain);
                Audion4Params[activeSide].BEQ8_gain[activeProgram] = Convert.ToInt16(BEQ8_gain);
                Audion4Params[activeSide].BEQ9_gain[activeProgram] = Convert.ToInt16(BEQ9_gain);
                Audion4Params[activeSide].BEQ10_gain[activeProgram] = Convert.ToInt16(BEQ10_gain);
                Audion4Params[activeSide].BEQ11_gain[activeProgram] = Convert.ToInt16(BEQ11_gain);
                Audion4Params[activeSide].BEQ12_gain[activeProgram] = Convert.ToInt16(BEQ12_gain);
                Audion4Params[activeSide].C1_Ratio[activeProgram] = Convert.ToInt16(C1_Ratio);
                Audion4Params[activeSide].C2_Ratio[activeProgram] = Convert.ToInt16(C2_Ratio);
                Audion4Params[activeSide].C3_Ratio[activeProgram] = Convert.ToInt16(C3_Ratio);
                Audion4Params[activeSide].C4_Ratio[activeProgram] = Convert.ToInt16(C4_Ratio);
                Audion4Params[activeSide].Expansion_Enable[activeProgram] = Convert.ToInt16(Expansion_Enable);
                Audion4Params[activeSide].FBC_Enable[activeProgram] = Convert.ToInt16(FBC_Enable);
                Audion4Params[activeSide].High_Cut[activeProgram] = Convert.ToInt16(High_Cut);
                Audion4Params[activeSide].input_mux[activeProgram] = Convert.ToInt16(input_mux);
                Audion4Params[activeSide].Low_Cut[activeProgram] = Convert.ToInt16(Low_Cut);
                Audion4Params[activeSide].matrix_gain[activeProgram] = Convert.ToInt16(matrix_gain);
                Audion4Params[activeSide].MPO_level[activeProgram] = Convert.ToInt16(MPO_level);
                Audion4Params[activeSide].Noise_Reduction[activeProgram] = Convert.ToInt16(Noise_Reduction);
                Audion4Params[activeSide].preamp_gain0[activeProgram] = Convert.ToInt16(preamp_gain0);
                Audion4Params[activeSide].preamp_gain1[activeProgram] = Convert.ToInt16(preamp_gain1);
                Audion4Params[activeSide].threshold[activeProgram] = Convert.ToInt16(threshold);
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
                Audion4Config[activeSide].ATC = Convert.ToInt16(ATC);
                Audion4Config[activeSide].Auto_Save = Convert.ToInt16(Auto_Save);
                Audion4Config[activeSide].Cal_Input = Convert.ToInt16(Cal_Input);
                Audion4Config[activeSide].Dir_Spacing = Convert.ToInt16(Dir_Spacing);
                Audion4Config[activeSide].Low_Batt_Warning = Convert.ToInt16(Low_Batt_Warning);
                Audion4Config[activeSide].MAP_HC = Convert.ToInt16(MAP_HC);
                Audion4Config[activeSide].MAP_LC = Convert.ToInt16(MAP_LC);
                Audion4Config[activeSide].MAP_MPO = Convert.ToInt16(MAP_MPO);
                Audion4Config[activeSide].MAP_TK = Convert.ToInt16(MAP_TK);
                Audion4Config[activeSide].Mic_Cal = Convert.ToInt16(Mic_Cal);
                Audion4Config[activeSide].number_of_programs = Convert.ToInt16(number_of_programs);
                Audion4Config[activeSide].Power_On_Level = Convert.ToInt16(Power_On_Level);
                Audion4Config[activeSide].Power_On_Delay = Convert.ToInt16(Power_On_Delay);
                Audion4Config[activeSide].Program_StartUp = Convert.ToInt16(Program_StartUp);
                Audion4Config[activeSide].Out_Mode = Convert.ToInt16(Out_Mode);
                Audion4Config[activeSide].Switch_Mode = Convert.ToInt16(Switch_Mode);
                Audion4Config[activeSide].Switch_Tone = Convert.ToInt16(Switch_Tone);
                Audion4Config[activeSide].T1_DIR = Convert.ToInt16(T1_DIR);
                Audion4Config[activeSide].T2_DIR = Convert.ToInt16(T2_DIR);
                Audion4Config[activeSide].test = Convert.ToInt16(test);
                Audion4Config[activeSide].Tone_Frequency = Convert.ToInt16(Tone_Frequency);
                Audion4Config[activeSide].Tone_Level = Convert.ToInt16(Tone_Level);
                Audion4Config[activeSide].Time_Constants = Convert.ToInt16(Time_Constants);
                Audion4Config[activeSide].VC_AnalogRange = Convert.ToInt16(VC_AnalogRange);
                Audion4Config[activeSide].VC_Beep_Enable = Convert.ToInt16(VC_Beep_Enable);
                Audion4Config[activeSide].VC_DigitalNumSteps = Convert.ToInt16(VC_DigitalNumSteps);
                Audion4Config[activeSide].VC_DigitalStepSize = Convert.ToInt16(VC_DigitalStepSize);
                Audion4Config[activeSide].VC_Enable = Convert.ToInt16(VC_Enable);
                Audion4Config[activeSide].VC_Mode = Convert.ToInt16(VC_Mode);
                Audion4Config[activeSide].VC_Startup = Convert.ToInt16(VC_Startup);
                Audion4Config[activeSide].Active_PGM = Convert.ToInt16(Active_PGM);
                Audion4Config[activeSide].T1_POS = Convert.ToInt16(T1_POS);
                Audion4Config[activeSide].T2_POS = Convert.ToInt16(T2_POS);
                Audion4Config[activeSide].VC_Pos = Convert.ToInt16(VC_Pos);
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

                target.CR[0] = 0;
                target.CR[1] = 0;
                target.CR[2] = 0;
                target.CR[3] = 0;

                target.TK = 55;
                target.ResGain = 6;
                target.MPO = (short)waveRule.CalculateUCL(activeSide);

                target.useCR = 0;
                target.isNL2 = 0;

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
                MessageBox.Show("You must have valid numerical values in all the Target cells to do Autofit. Try again. Error:\n\n" + err.Message);
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

            this.errorCode = AutoFit(21, 0, ref target);
            if (this.errorCode != 0)
            {
                Console.WriteLine("Autofit() error:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = Get_params(this.DLLVER, ref this.Audion4Params[this.activeSide]);
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