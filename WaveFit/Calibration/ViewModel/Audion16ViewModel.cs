using Audion8TestApp;
using G_Enums;
using GenericAudion16;
using GenericCommon;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using WaveFit2.Calibration.Class;

namespace WaveFit2.Calibration.ViewModel
{
    public class Audion16ViewModel : G_Audion16
    {
        public bool ProgrammerInitialized;

        public int errorCode;

        public bool loaded = false;

        public G_Common GDriver;

        private ParamsDictionary detectDataParams;

        public ProgramsDictionary[] ampParams = new ProgramsDictionary[2];

        public ParamsDictionary[] ampConfig = new ParamsDictionary[2];

        public BiquadDictionary[] ampConfigBiquads = new BiquadDictionary[2];

        public ParamsDictionary[] ampMDA = new ParamsDictionary[2];

        public ParamsDictionary[] ampStatus = new ParamsDictionary[2];

        public ParamsDictionary[] ampDatalog = new ParamsDictionary[2];

        public float[] micFR;

        public float[] recFR;

        public float[] FreqResp;

        private FileUtilities fileUtils = new FileUtilities();

        public Int16 activeProgram;
        public Int16 activeSide;
        public Int16 DLLVER = 1;

        public string[] paramsOrder;
        private int[] paramsTabIndex;
        public string[] configOrder;
        public List<string> mdaOrder = new List<string>();

        public int input_mux, matrix_gain, preamp_gain1, preamp_gain2, preamp_gain_digital_1, preamp_gain_digital_2,
                     feedback_canceller, noise_reduction, wind_suppression, input_filter_low_cut, low_level_expansion,
                     beq_gain_1, mpo_threshold_1, mpo_attack, beq_gain_2, mpo_threshold_2, mpo_release,
                     beq_gain_3, mpo_threshold_3, beq_gain_4, mpo_threshold_4, beq_gain_5, mpo_threshold_5,
                     beq_gain_6, mpo_threshold_6, beq_gain_7, mpo_threshold_7, beq_gain_8, mpo_threshold_8,
                     beq_gain_9, mpo_threshold_9, beq_gain_10, mpo_threshold_10, beq_gain_11, mpo_threshold_11,
                     beq_gain_12, mpo_threshold_12, beq_gain_13, mpo_threshold_13, beq_gain_14, mpo_threshold_14,
                     beq_gain_15, mpo_threshold_15, beq_gain_16, mpo_threshold_16,
                     comp_ratio_1, comp_threshold_1, comp_time_consts_1,
                     comp_ratio_2, comp_threshold_2, comp_time_consts_2,
                     comp_ratio_3, comp_threshold_3, comp_time_consts_3,
                     comp_ratio_4, comp_threshold_4, comp_time_consts_4,
                     comp_ratio_5, comp_threshold_5, comp_time_consts_5,
                     comp_ratio_6, comp_threshold_6, comp_time_consts_6,
                     comp_ratio_7, comp_threshold_7, comp_time_consts_7,
                     comp_ratio_8, comp_threshold_8, comp_time_consts_8,
                     comp_ratio_9, comp_threshold_9, comp_time_consts_9,
                     comp_ratio_10, comp_threshold_10, comp_time_consts_10,
                     comp_ratio_11, comp_threshold_11, comp_time_consts_11,
                     comp_ratio_12, comp_threshold_12, comp_time_consts_12,
                     comp_ratio_13, comp_threshold_13, comp_time_consts_13,
                     comp_ratio_14, comp_threshold_14, comp_time_consts_14,
                     comp_ratio_15, comp_threshold_15, comp_time_consts_15,
                     comp_ratio_16, comp_threshold_16, comp_time_consts_16;

        public int Switch_Mode, VC_Mode, VC_Enable, Auto_Save, VC_Prompt_Mode, Program_Prompt_Mode,
                     Warning_Prompt_Mode, Power_On_VC, Power_On_Program, VC_Num_Steps, VC_Step_Size,
                     VC_Analog_Range, Num_Programs, Prompt_Level, Tone_Frequency,
                     ADir_Sensitivity, Auto_Telecoil, Acoustap_Mode, Acoustap_Sensitivity, Power_On_Level, Power_On_Delay,
                     Noise_Level, High_Power_Mode, Dir_Mic_Cal, Dir_Mic_Cal_Input, Dir_Spacing, test,
                     Output_Filter_Enable, Output_Filter_1, Output_Filter_2, Noise_Filter_Ref, Noise_Filter_1, Noise_Filter_2;

        public int Platform_ID, AlgVer_Major, AlgVer_Minor, AlgVer_Build, MANF_ID, ModelID,
            MDA_1, MDA_2, MDA_3, MDA_4, MDA_5, MDA_6, MDA_7, MDA_8, MDA_9, MDA_10;

        public string defaultReceptor, defaultMicrophone;

        public TargetAudion16 targetAudion16;

        private double[] GenericFrequencies = new double[] { 125, 160, 200, 250, 315, 400, 500, 630, 750, 800, 1000, 1250, 1500, 1600, 2000, 2500, 3000, 3150, 4000, 5000, 6000, 6300, 8000 };

        public Audion16ViewModel()
        {
            GDriver = new G_Audion16();

            InitializeAllSideBasedParameters();
            initStructureArrays();
            RecDirectory();
            MicDirectory();
        }

        public void initStructureArrays()
        {
            this.micFR = new float[65];
            this.recFR = new float[65];
            this.FreqResp = new float[65];
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

        public void InitializeAllSideBasedParameters()
        {
            for (int i = 0; i < 2; i++)
            {
                ampParams[i] = new ProgramsDictionary();
                ampConfig[i] = new ParamsDictionary();
                ampConfigBiquads[i] = new BiquadDictionary();
                ampMDA[i] = new ParamsDictionary();
                ampStatus[i] = new ParamsDictionary();
                ampDatalog[i] = new ParamsDictionary();
            }
        }

        public void StartProgrammer()
        {
            InitializeProgramer();
        }

        public void GetMicRec()
        {
            loadMicModel();
            loadRecModel();

            this.errorCode = (GDriver as G_Audion16).SetMicResponse(this.micFR);
            if (this.errorCode != 0)
            {
                Console.WriteLine("Set_mic_response() error:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }

            this.errorCode = (GDriver as G_Audion16).SetRecResponse(this.recFR);
            if (this.errorCode != 0)
            {
                Console.WriteLine("Set_rec_response() error:" + DriverErrorString(errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
        }

        public void InitializeProgramer()
        {
            this.errorCode = GDriver.SetInterfaceType(interface_type.typeHipro);
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

        public void ChangeSide(char side)
        {
            SetChannel(side);
        }

        public void SetChannel(char side)
        {
            if (side == 'L')
            {
                activeSide = 0;
                this.errorCode = GDriver.SetRLChannel(this.activeSide);
                if (this.errorCode == 0)
                {
                    Growl.SuccessGlobal("Lado esquerdo escolhido com sucesso");
                }
                else
                {
                    Console.WriteLine("Set channel: " + DriverErrorString(this.errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                }
            }
            else
            {
                activeSide = 1;
                this.errorCode = GDriver.SetRLChannel(this.activeSide);
                if (this.errorCode == 0)
                {
                    Growl.SuccessGlobal("Lado direito escolhido com sucesso");
                }
                else
                {
                    Console.WriteLine("Set channel: " + DriverErrorString(this.errorCode));
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
                this.errorCode = GDriver.Read();
            }

            if (this.errorCode != 0)
            {
                Console.WriteLine("Read: " + DriverErrorString(this.errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                ampParams[activeSide] = GDriver.GetParams();
                if (this.errorCode != 0)
                {
                    Console.WriteLine("Read Get_params: " + DriverErrorString(this.errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));

                    return;
                }
                else
                {
                    ampConfig[activeSide] = GDriver.GetConfig();
                    ampConfigBiquads[activeSide] = (GDriver as G_Audion16).GetConfigBiquads();
                    (GDriver as G_Audion16).ReadMda();
                    ampMDA[activeSide] = (GDriver as G_Audion16).GetMda(0);
                    if (this.errorCode != 0)
                    {
                        Console.WriteLine("Read Get_config: " + DriverErrorString(this.errorCode));
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
                input_mux = ampParams[activeSide][activeProgram]["Input_Mux"];
                matrix_gain = ampParams[activeSide][activeProgram]["Matrix_Gain"];
                preamp_gain1 = ampParams[activeSide][activeProgram]["Preamp_Gain_1"];
                preamp_gain_digital_1 = ampParams[activeSide][activeProgram]["Preamp_Gain_Digital_1"];

                preamp_gain2 = ampParams[activeSide][activeProgram]["Preamp_Gain_2"];
                preamp_gain_digital_2 = ampParams[activeSide][activeProgram]["Preamp_Gain_Digital_2"];

                feedback_canceller = ampParams[activeSide][activeProgram]["Feedback_Canceller"];
                noise_reduction = ampParams[activeSide][activeProgram]["Noise_Reduction"];
                wind_suppression = ampParams[activeSide][activeProgram]["Wind_Suppression"];

                input_filter_low_cut = ampParams[activeSide][activeProgram]["Input_Filter_Low_Cut"];
                low_level_expansion = ampParams[activeSide][activeProgram]["Low_Level_Expansion"];

                beq_gain_1 = ampParams[activeSide][activeProgram]["BEQ_Gain_1"];
                mpo_threshold_1 = ampParams[activeSide][activeProgram]["MPO_Threshold_1"];
                mpo_attack = ampParams[activeSide][activeProgram]["MPO_Attack"];

                beq_gain_2 = ampParams[activeSide][activeProgram]["BEQ_Gain_2"];
                mpo_threshold_2 = ampParams[activeSide][activeProgram]["MPO_Threshold_2"];
                mpo_release = ampParams[activeSide][activeProgram]["MPO_Release"];

                beq_gain_3 = ampParams[activeSide][activeProgram]["BEQ_Gain_3"];
                mpo_threshold_3 = ampParams[activeSide][activeProgram]["MPO_Threshold_3"];

                beq_gain_4 = ampParams[activeSide][activeProgram]["BEQ_Gain_4"];
                mpo_threshold_4 = ampParams[activeSide][activeProgram]["MPO_Threshold_4"];

                beq_gain_5 = ampParams[activeSide][activeProgram]["BEQ_Gain_5"];
                mpo_threshold_5 = ampParams[activeSide][activeProgram]["MPO_Threshold_5"];

                beq_gain_6 = ampParams[activeSide][activeProgram]["BEQ_Gain_6"];
                mpo_threshold_6 = ampParams[activeSide][activeProgram]["MPO_Threshold_6"];

                beq_gain_7 = ampParams[activeSide][activeProgram]["BEQ_Gain_7"];
                mpo_threshold_7 = ampParams[activeSide][activeProgram]["MPO_Threshold_7"];

                beq_gain_8 = ampParams[activeSide][activeProgram]["BEQ_Gain_8"];
                mpo_threshold_8 = ampParams[activeSide][activeProgram]["MPO_Threshold_8"];

                beq_gain_9 = ampParams[activeSide][activeProgram]["BEQ_Gain_9"];
                mpo_threshold_9 = ampParams[activeSide][activeProgram]["MPO_Threshold_9"];

                beq_gain_10 = ampParams[activeSide][activeProgram]["BEQ_Gain_10"];
                mpo_threshold_10 = ampParams[activeSide][activeProgram]["MPO_Threshold_10"];

                beq_gain_11 = ampParams[activeSide][activeProgram]["BEQ_Gain_11"];
                mpo_threshold_11 = ampParams[activeSide][activeProgram]["MPO_Threshold_11"];

                beq_gain_12 = ampParams[activeSide][activeProgram]["BEQ_Gain_12"];
                mpo_threshold_12 = ampParams[activeSide][activeProgram]["MPO_Threshold_12"];

                beq_gain_13 = ampParams[activeSide][activeProgram]["BEQ_Gain_13"];
                mpo_threshold_13 = ampParams[activeSide][activeProgram]["MPO_Threshold_13"];

                beq_gain_14 = ampParams[activeSide][activeProgram]["BEQ_Gain_14"];
                mpo_threshold_14 = ampParams[activeSide][activeProgram]["MPO_Threshold_14"];

                beq_gain_15 = ampParams[activeSide][activeProgram]["BEQ_Gain_15"];
                mpo_threshold_15 = ampParams[activeSide][activeProgram]["MPO_Threshold_15"];

                beq_gain_16 = ampParams[activeSide][activeProgram]["BEQ_Gain_16"];
                mpo_threshold_16 = ampParams[activeSide][activeProgram]["MPO_Threshold_16"];

                comp_ratio_1 = ampParams[activeSide][activeProgram]["Comp_Ratio_1"];
                comp_threshold_1 = ampParams[activeSide][activeProgram]["Comp_Threshold_1"];
                comp_time_consts_1 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_1"];

                comp_ratio_2 = ampParams[activeSide][activeProgram]["Comp_Ratio_2"];
                comp_threshold_2 = ampParams[activeSide][activeProgram]["Comp_Threshold_2"];
                comp_time_consts_2 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_2"];

                comp_ratio_3 = ampParams[activeSide][activeProgram]["Comp_Ratio_3"];
                comp_threshold_3 = ampParams[activeSide][activeProgram]["Comp_Threshold_3"];
                comp_time_consts_3 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_3"];

                comp_ratio_4 = ampParams[activeSide][activeProgram]["Comp_Ratio_4"];
                comp_threshold_4 = ampParams[activeSide][activeProgram]["Comp_Threshold_4"];
                comp_time_consts_4 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_4"];

                comp_ratio_5 = ampParams[activeSide][activeProgram]["Comp_Ratio_5"];
                comp_threshold_5 = ampParams[activeSide][activeProgram]["Comp_Threshold_5"];
                comp_time_consts_5 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_5"];

                comp_ratio_6 = ampParams[activeSide][activeProgram]["Comp_Ratio_6"];
                comp_threshold_6 = ampParams[activeSide][activeProgram]["Comp_Threshold_6"];
                comp_time_consts_6 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_6"];

                comp_ratio_7 = ampParams[activeSide][activeProgram]["Comp_Ratio_7"];
                comp_threshold_7 = ampParams[activeSide][activeProgram]["Comp_Threshold_7"];
                comp_time_consts_7 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_7"];

                comp_ratio_8 = ampParams[activeSide][activeProgram]["Comp_Ratio_8"];
                comp_threshold_8 = ampParams[activeSide][activeProgram]["Comp_Threshold_8"];
                comp_time_consts_8 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_8"];

                comp_ratio_9 = ampParams[activeSide][activeProgram]["Comp_Ratio_9"];
                comp_threshold_9 = ampParams[activeSide][activeProgram]["Comp_Threshold_9"];
                comp_time_consts_9 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_9"];

                comp_ratio_10 = ampParams[activeSide][activeProgram]["Comp_Ratio_10"];
                comp_threshold_10 = ampParams[activeSide][activeProgram]["Comp_Threshold_10"];
                comp_time_consts_10 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_10"];

                comp_ratio_11 = ampParams[activeSide][activeProgram]["Comp_Ratio_11"];
                comp_threshold_11 = ampParams[activeSide][activeProgram]["Comp_Threshold_11"];
                comp_time_consts_11 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_11"];

                comp_ratio_12 = ampParams[activeSide][activeProgram]["Comp_Ratio_12"];
                comp_threshold_12 = ampParams[activeSide][activeProgram]["Comp_Threshold_12"];
                comp_time_consts_12 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_12"];

                comp_ratio_13 = ampParams[activeSide][activeProgram]["Comp_Ratio_13"];
                comp_threshold_13 = ampParams[activeSide][activeProgram]["Comp_Threshold_13"];
                comp_time_consts_13 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_13"];

                comp_ratio_14 = ampParams[activeSide][activeProgram]["Comp_Ratio_14"];
                comp_threshold_14 = ampParams[activeSide][activeProgram]["Comp_Threshold_14"];
                comp_time_consts_14 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_14"];

                comp_ratio_15 = ampParams[activeSide][activeProgram]["Comp_Ratio_15"];
                comp_threshold_15 = ampParams[activeSide][activeProgram]["Comp_Threshold_15"];
                comp_time_consts_15 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_15"];

                comp_ratio_16 = ampParams[activeSide][activeProgram]["Comp_Ratio_16"];
                comp_threshold_16 = ampParams[activeSide][activeProgram]["Comp_Threshold_16"];
                comp_time_consts_16 = ampParams[activeSide][activeProgram]["Comp_Time_Consts_16"];
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public void UpdateConfigBoxesFromConfigParams(int activeSide)
        {
            try
            {
                Switch_Mode = ampConfig[activeSide]["Switch_Mode"];
                VC_Mode = ampConfig[activeSide]["VC_Mode"];
                VC_Enable = ampConfig[activeSide]["VC_Enable"];
                Auto_Save = ampConfig[activeSide]["Auto_Save"];
                VC_Prompt_Mode = ampConfig[activeSide]["VC_Prompt_Mode"];
                Program_Prompt_Mode = ampConfig[activeSide]["Program_Prompt_Mode"];

                Warning_Prompt_Mode = ampConfig[activeSide]["Warning_Prompt_Mode"];
                Power_On_VC = ampConfig[activeSide]["Power_On_VC"];
                Power_On_Program = ampConfig[activeSide]["Power_On_Program"];
                VC_Num_Steps = ampConfig[activeSide]["VC_Num_Steps"];
                VC_Step_Size = ampConfig[activeSide]["VC_Step_Size"];

                VC_Analog_Range = ampConfig[activeSide]["VC_Analog_Range"];
                Num_Programs = ampConfig[activeSide]["Num_Programs"];
                Prompt_Level = ampConfig[activeSide]["Prompt_Level"];
                Tone_Frequency = ampConfig[activeSide]["Tone_Frequency"];

                ADir_Sensitivity = ampConfig[activeSide]["ADir_Sensitivity"];
                Auto_Telecoil = ampConfig[activeSide]["Auto_Telecoil"];
                Acoustap_Mode = ampConfig[activeSide]["Acoustap_Mode"];
                Acoustap_Sensitivity = ampConfig[activeSide]["Acoustap_Sensitivity"];
                Power_On_Level = ampConfig[activeSide]["Power_On_Level"];
                Power_On_Delay = ampConfig[activeSide]["Power_On_Delay"];

                Noise_Level = ampConfig[activeSide]["Noise_Level"];
                High_Power_Mode = ampConfig[activeSide]["High_Power_Mode"];
                Dir_Mic_Cal = ampConfig[activeSide]["Dir_Mic_Cal"];
                Dir_Mic_Cal_Input = ampConfig[activeSide]["Dir_Mic_Cal_Input"];
                Dir_Spacing = ampConfig[activeSide]["Dir_Spacing"];
                test = ampConfig[activeSide]["test"];

                Output_Filter_Enable = ampConfig[activeSide]["Output_Filter_Enable"];
                Output_Filter_1 = ampConfig[activeSide]["Output_Filter_1"];
                Output_Filter_2 = ampConfig[activeSide]["Output_Filter_2"];
                Noise_Filter_Ref = ampConfig[activeSide]["Noise_Filter_Ref"];
                Noise_Filter_1 = ampConfig[activeSide]["Noise_Filter_1"];
                Noise_Filter_2 = ampConfig[activeSide]["Noise_Filter_2"];

                Platform_ID = ampMDA[activeSide]["Platform_ID"];
                AlgVer_Major = ampMDA[activeSide]["AlgVer_Major"];
                AlgVer_Minor = ampMDA[activeSide]["AlgVer_Minor"];
                AlgVer_Build = ampMDA[activeSide]["AlgVer_Build"];
                MANF_ID = ampMDA[activeSide]["MANF_ID"];
                ModelID = ampMDA[activeSide]["ModelID"];
                MDA_1 = ampMDA[activeSide]["MDA_1"];
                MDA_2 = ampMDA[activeSide]["MDA_2"];
                MDA_3 = ampMDA[activeSide]["MDA_3"];
                MDA_4 = ampMDA[activeSide]["MDA_4"];
                MDA_5 = ampMDA[activeSide]["MDA_5"];
                MDA_6 = ampMDA[activeSide]["MDA_6"];
                MDA_7 = ampMDA[activeSide]["MDA_7"];
                MDA_8 = ampMDA[activeSide]["MDA_8"];
                MDA_9 = ampMDA[activeSide]["MDA_9"];
                MDA_10 = ampMDA[activeSide]["MDA_10"];

                Console.WriteLine(MDA_2.ToString());
                Console.WriteLine(MDA_3.ToString());
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
            this.errorCode = GDriver.SetParams(this.ampParams[this.activeSide]);

            if (this.errorCode != 0)
            {
                Console.WriteLine("Load Set_params: " + DriverErrorString(this.errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                this.errorCode = GDriver.SetConfig(this.ampConfig[this.activeSide]);
                if (this.errorCode != 0)
                {
                    Console.WriteLine("Load Set_config: " + DriverErrorString(this.errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                }
                else if (doLoad)
                {
                    this.errorCode = GDriver.Load();
                    if (this.errorCode != 0)
                    {
                        Console.WriteLine("Loaded" + DriverErrorString(this.errorCode));
                        Growl.ErrorGlobal(DriverErrorString(errorCode));
                    }
                    else if (this.errorCode == 0 && loaded == false)
                    {
                        Growl.SuccessGlobal("Parametros carregados com sucesso.");
                        loaded = true;
                    }
                }
            }
        }

        public void UpdateParamsFromParamBoxes(int activeSide, int activeProgram)
        {
            try
            {
                ampParams[activeSide][activeProgram]["Input_Mux"] = Convert.ToInt16(input_mux);
                ampParams[activeSide][activeProgram]["Matrix_Gain"] = Convert.ToInt16(matrix_gain);

                ampParams[activeSide][activeProgram]["Preamp_Gain_1"] = Convert.ToInt16(preamp_gain1);
                ampParams[activeSide][activeProgram]["Preamp_Gain_2"] = Convert.ToInt16(preamp_gain2);

                ampParams[activeSide][activeProgram]["Preamp_Gain_Digital_1"] = Convert.ToInt16(preamp_gain_digital_1);
                ampParams[activeSide][activeProgram]["Preamp_Gain_Digital_2"] = Convert.ToInt16(preamp_gain_digital_2);

                ampParams[activeSide][activeProgram]["Feedback_Canceller"] = Convert.ToInt16(feedback_canceller);
                ampParams[activeSide][activeProgram]["Noise_Reduction"] = Convert.ToInt16(noise_reduction);
                ampParams[activeSide][activeProgram]["Wind_Suppression"] = Convert.ToInt16(wind_suppression);

                ampParams[activeSide][activeProgram]["Input_Filter_Low_Cut"] = Convert.ToInt16(input_filter_low_cut);
                ampParams[activeSide][activeProgram]["Low_Level_Expansion"] = Convert.ToInt16(low_level_expansion);

                ampParams[activeSide][activeProgram]["BEQ_Gain_1"] = Convert.ToInt16(beq_gain_1);
                ampParams[activeSide][activeProgram]["MPO_Threshold_1"] = Convert.ToInt16(mpo_threshold_1);
                ampParams[activeSide][activeProgram]["BEQ_Gain_2"] = Convert.ToInt16(beq_gain_2);
                ampParams[activeSide][activeProgram]["MPO_Threshold_2"] = Convert.ToInt16(mpo_threshold_2);
                ampParams[activeSide][activeProgram]["BEQ_Gain_3"] = Convert.ToInt16(beq_gain_3);
                ampParams[activeSide][activeProgram]["MPO_Threshold_3"] = Convert.ToInt16(mpo_threshold_3);
                ampParams[activeSide][activeProgram]["BEQ_Gain_4"] = Convert.ToInt16(beq_gain_4);
                ampParams[activeSide][activeProgram]["MPO_Threshold_4"] = Convert.ToInt16(mpo_threshold_4);
                ampParams[activeSide][activeProgram]["BEQ_Gain_5"] = Convert.ToInt16(beq_gain_5);
                ampParams[activeSide][activeProgram]["MPO_Threshold_5"] = Convert.ToInt16(mpo_threshold_5);
                ampParams[activeSide][activeProgram]["BEQ_Gain_6"] = Convert.ToInt16(beq_gain_6);
                ampParams[activeSide][activeProgram]["MPO_Threshold_6"] = Convert.ToInt16(mpo_threshold_6);
                ampParams[activeSide][activeProgram]["BEQ_Gain_7"] = Convert.ToInt16(beq_gain_7);
                ampParams[activeSide][activeProgram]["MPO_Threshold_7"] = Convert.ToInt16(mpo_threshold_7);
                ampParams[activeSide][activeProgram]["BEQ_Gain_8"] = Convert.ToInt16(beq_gain_8);
                ampParams[activeSide][activeProgram]["MPO_Threshold_8"] = Convert.ToInt16(mpo_threshold_8);
                ampParams[activeSide][activeProgram]["BEQ_Gain_9"] = Convert.ToInt16(beq_gain_9);
                ampParams[activeSide][activeProgram]["MPO_Threshold_9"] = Convert.ToInt16(mpo_threshold_9);
                ampParams[activeSide][activeProgram]["BEQ_Gain_10"] = Convert.ToInt16(beq_gain_10);
                ampParams[activeSide][activeProgram]["MPO_Threshold_10"] = Convert.ToInt16(mpo_threshold_10);
                ampParams[activeSide][activeProgram]["BEQ_Gain_11"] = Convert.ToInt16(beq_gain_11);
                ampParams[activeSide][activeProgram]["MPO_Threshold_11"] = Convert.ToInt16(mpo_threshold_11);
                ampParams[activeSide][activeProgram]["BEQ_Gain_12"] = Convert.ToInt16(beq_gain_12);
                ampParams[activeSide][activeProgram]["MPO_Threshold_12"] = Convert.ToInt16(mpo_threshold_12);
                ampParams[activeSide][activeProgram]["BEQ_Gain_13"] = Convert.ToInt16(beq_gain_13);
                ampParams[activeSide][activeProgram]["MPO_Threshold_13"] = Convert.ToInt16(mpo_threshold_13);
                ampParams[activeSide][activeProgram]["BEQ_Gain_14"] = Convert.ToInt16(beq_gain_14);
                ampParams[activeSide][activeProgram]["MPO_Threshold_14"] = Convert.ToInt16(mpo_threshold_14);
                ampParams[activeSide][activeProgram]["BEQ_Gain_15"] = Convert.ToInt16(beq_gain_15);
                ampParams[activeSide][activeProgram]["MPO_Threshold_15"] = Convert.ToInt16(mpo_threshold_15);
                ampParams[activeSide][activeProgram]["BEQ_Gain_16"] = Convert.ToInt16(beq_gain_16);
                ampParams[activeSide][activeProgram]["MPO_Threshold_16"] = Convert.ToInt16(mpo_threshold_16);

                ampParams[activeSide][activeProgram]["MPO_Attack"] = Convert.ToInt16(mpo_attack);
                ampParams[activeSide][activeProgram]["MPO_Release"] = Convert.ToInt16(mpo_release);

                ampParams[activeSide][activeProgram]["Comp_Ratio_1"] = Convert.ToInt16(comp_ratio_1);
                ampParams[activeSide][activeProgram]["Comp_Threshold_1"] = Convert.ToInt16(comp_threshold_1);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_1"] = Convert.ToInt16(comp_time_consts_1);
                ampParams[activeSide][activeProgram]["Comp_Ratio_2"] = Convert.ToInt16(comp_ratio_2);
                ampParams[activeSide][activeProgram]["Comp_Threshold_2"] = Convert.ToInt16(comp_threshold_2);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_2"] = Convert.ToInt16(comp_time_consts_2);
                ampParams[activeSide][activeProgram]["Comp_Ratio_3"] = Convert.ToInt16(comp_ratio_3);
                ampParams[activeSide][activeProgram]["Comp_Threshold_3"] = Convert.ToInt16(comp_threshold_3);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_3"] = Convert.ToInt16(comp_time_consts_3);
                ampParams[activeSide][activeProgram]["Comp_Ratio_4"] = Convert.ToInt16(comp_ratio_4);
                ampParams[activeSide][activeProgram]["Comp_Threshold_4"] = Convert.ToInt16(comp_threshold_4);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_4"] = Convert.ToInt16(comp_time_consts_4);
                ampParams[activeSide][activeProgram]["Comp_Ratio_5"] = Convert.ToInt16(comp_ratio_5);
                ampParams[activeSide][activeProgram]["Comp_Threshold_5"] = Convert.ToInt16(comp_threshold_5);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_5"] = Convert.ToInt16(comp_time_consts_5);
                ampParams[activeSide][activeProgram]["Comp_Ratio_6"] = Convert.ToInt16(comp_ratio_6);
                ampParams[activeSide][activeProgram]["Comp_Threshold_6"] = Convert.ToInt16(comp_threshold_6);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_6"] = Convert.ToInt16(comp_time_consts_6);
                ampParams[activeSide][activeProgram]["Comp_Ratio_7"] = Convert.ToInt16(comp_ratio_7);
                ampParams[activeSide][activeProgram]["Comp_Threshold_7"] = Convert.ToInt16(comp_threshold_7);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_7"] = Convert.ToInt16(comp_time_consts_7);
                ampParams[activeSide][activeProgram]["Comp_Ratio_8"] = Convert.ToInt16(comp_ratio_8);
                ampParams[activeSide][activeProgram]["Comp_Threshold_8"] = Convert.ToInt16(comp_threshold_8);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_8"] = Convert.ToInt16(comp_time_consts_8);
                ampParams[activeSide][activeProgram]["Comp_Ratio_9"] = Convert.ToInt16(comp_ratio_9);
                ampParams[activeSide][activeProgram]["Comp_Threshold_9"] = Convert.ToInt16(comp_threshold_9);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_9"] = Convert.ToInt16(comp_time_consts_9);
                ampParams[activeSide][activeProgram]["Comp_Ratio_10"] = Convert.ToInt16(comp_ratio_10);
                ampParams[activeSide][activeProgram]["Comp_Threshold_10"] = Convert.ToInt16(comp_threshold_10);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_10"] = Convert.ToInt16(comp_time_consts_10);
                ampParams[activeSide][activeProgram]["Comp_Ratio_11"] = Convert.ToInt16(comp_ratio_11);
                ampParams[activeSide][activeProgram]["Comp_Threshold_11"] = Convert.ToInt16(comp_threshold_11);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_11"] = Convert.ToInt16(comp_time_consts_11);
                ampParams[activeSide][activeProgram]["Comp_Ratio_12"] = Convert.ToInt16(comp_ratio_12);
                ampParams[activeSide][activeProgram]["Comp_Threshold_12"] = Convert.ToInt16(comp_threshold_12);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_12"] = Convert.ToInt16(comp_time_consts_12);
                ampParams[activeSide][activeProgram]["Comp_Ratio_13"] = Convert.ToInt16(comp_ratio_13);
                ampParams[activeSide][activeProgram]["Comp_Threshold_13"] = Convert.ToInt16(comp_threshold_13);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_13"] = Convert.ToInt16(comp_time_consts_13);
                ampParams[activeSide][activeProgram]["Comp_Ratio_14"] = Convert.ToInt16(comp_ratio_14);
                ampParams[activeSide][activeProgram]["Comp_Threshold_14"] = Convert.ToInt16(comp_threshold_14);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_14"] = Convert.ToInt16(comp_time_consts_14);
                ampParams[activeSide][activeProgram]["Comp_Ratio_15"] = Convert.ToInt16(comp_ratio_15);
                ampParams[activeSide][activeProgram]["Comp_Threshold_15"] = Convert.ToInt16(comp_threshold_15);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_15"] = Convert.ToInt16(comp_time_consts_15);
                ampParams[activeSide][activeProgram]["Comp_Ratio_16"] = Convert.ToInt16(comp_ratio_16);
                ampParams[activeSide][activeProgram]["Comp_Threshold_16"] = Convert.ToInt16(comp_threshold_16);
                ampParams[activeSide][activeProgram]["Comp_Time_Consts_16"] = Convert.ToInt16(comp_time_consts_16);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public void UpdateConfigParamsFromConfigBoxes(int activeSide)
        {
            try
            {
                ampConfig[activeSide]["Switch_Mode"] = Convert.ToInt16(Switch_Mode);
                ampConfig[activeSide]["VC_Mode"] = Convert.ToInt16(VC_Mode);
                ampConfig[activeSide]["VC_Enable"] = Convert.ToInt16(VC_Enable);
                ampConfig[activeSide]["Auto_Save"] = Convert.ToInt16(Auto_Save);
                ampConfig[activeSide]["VC_Prompt_Mode"] = Convert.ToInt16(VC_Prompt_Mode);
                ampConfig[activeSide]["Program_Prompt_Mode"] = Convert.ToInt16(Program_Prompt_Mode);
                ampConfig[activeSide]["Warning_Prompt_Mode"] = Convert.ToInt16(Warning_Prompt_Mode);
                ampConfig[activeSide]["Power_On_VC"] = Convert.ToInt16(Power_On_VC);
                ampConfig[activeSide]["Power_On_Program"] = Convert.ToInt16(Power_On_Program);
                ampConfig[activeSide]["VC_Num_Steps"] = Convert.ToInt16(VC_Num_Steps);
                ampConfig[activeSide]["VC_Step_Size"] = Convert.ToInt16(VC_Step_Size);
                ampConfig[activeSide]["VC_Analog_Range"] = Convert.ToInt16(VC_Analog_Range);
                ampConfig[activeSide]["Num_Programs"] = Convert.ToInt16(Num_Programs);
                ampConfig[activeSide]["Prompt_Level"] = Convert.ToInt16(Prompt_Level);
                ampConfig[activeSide]["Tone_Frequency"] = Convert.ToInt16(Tone_Frequency);
                ampConfig[activeSide]["ADir_Sensitivity"] = Convert.ToInt16(ADir_Sensitivity);
                ampConfig[activeSide]["Auto_Telecoil"] = Convert.ToInt16(Auto_Telecoil);
                ampConfig[activeSide]["Acoustap_Mode"] = Convert.ToInt16(Acoustap_Mode);
                ampConfig[activeSide]["Acoustap_Sensitivity"] = Convert.ToInt16(Acoustap_Sensitivity);
                ampConfig[activeSide]["Power_On_Level"] = Convert.ToInt16(Power_On_Level);
                ampConfig[activeSide]["Power_On_Delay"] = Convert.ToInt16(Power_On_Delay);
                ampConfig[activeSide]["Noise_Level"] = Convert.ToInt16(Noise_Level);
                ampConfig[activeSide]["High_Power_Mode"] = Convert.ToInt16(High_Power_Mode);
                ampConfig[activeSide]["Dir_Mic_Cal"] = Convert.ToInt16(Dir_Mic_Cal);
                ampConfig[activeSide]["Dir_Mic_Cal_Input"] = Convert.ToInt16(Dir_Mic_Cal_Input);
                ampConfig[activeSide]["Dir_Spacing"] = Convert.ToInt16(Dir_Spacing);
                ampConfig[activeSide]["test"] = Convert.ToInt16(test);
                ampConfig[activeSide]["Output_Filter_Enable"] = Convert.ToInt16(Output_Filter_Enable);
                ampConfig[activeSide]["Output_Filter_1"] = Convert.ToInt16(Output_Filter_1);
                ampConfig[activeSide]["Output_Filter_2"] = Convert.ToInt16(Output_Filter_2);
                ampConfig[activeSide]["Noise_Filter_Ref"] = Convert.ToInt16(Noise_Filter_Ref);
                ampConfig[activeSide]["Noise_Filter_1"] = Convert.ToInt16(Noise_Filter_1);
                ampConfig[activeSide]["Noise_Filter_2"] = Convert.ToInt16(Noise_Filter_2);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public void LockVM()
        {
            this.errorCode = GDriver.Lock();
            if (this.errorCode != 0)
            {
                Console.WriteLine("Lock: " + DriverErrorString(this.errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else if (this.errorCode == 0 && loaded == true)
            {
                Growl.SuccessGlobal("Parametros gravados com sucesso.");
                loaded = false;
            }
        }

        public void Unmute()
        {
            this.errorCode = GDriver.AudioOn(this.activeProgram);
            if (this.errorCode != 0)
            {
                Console.WriteLine("UnMute: " + DriverErrorString(this.errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                Growl.SuccessGlobal("Aparelho desmutado com sucesso.");
            }
        }

        public void Mute()
        {
            this.errorCode = GDriver.AudioOff();
            if (this.errorCode != 0)
            {
                Console.WriteLine("Mute: " + DriverErrorString(this.errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                Growl.SuccessGlobal("Aparelho mutado com sucesso.");
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

        public void SetActiveProgram(int value)
        {
            GDriver.SetProgram((short)value);
        }

        public void SetCurrentProgram(int value)
        {
            if (value >= 0)
            {
                activeProgram = (short)value;
                GDriver.SetProgram(activeProgram);
            }
            else
            {
                activeProgram = 0;
            }
        }

        public void WriteTarget(TargetAudion16 targetData, WaveRule waveRule)
        {
            try
            {
                targetData.CalculateCR = 1;
                targetData.UseMpoCh = 1;
                targetData.MatrixGainCeiling = -20;
                targetData.ForceReserveGain = 0;
                targetData.ReserveGain = 6;
                targetData.DisableHighFreqRollOff = 1;
                targetData.SpeechTK = 65;
                targetData.UseTK = 0;

                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);
                targetData.CR.Add(0);

                targetData.TK.Add(60);
                targetData.TK.Add(60);
                targetData.TK.Add(60);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);
                targetData.TK.Add(65);

                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPOch.Add((short)waveRule.CalculateUCL(activeSide));

                targetData.TargetGain50.Add((float)waveRule.targetGains[0][0]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][1]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][2]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][3]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][4]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][5]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][6]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][7]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][8]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][9]);
                targetData.TargetGain50.Add(-999);
                targetData.TargetGain50.Add((float)waveRule.targetGains[0][10]);

                targetData.TargetGain80.Add((float)waveRule.targetGains[1][0]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][1]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][2]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][3]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][4]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][5]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][6]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][7]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][8]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][9]);
                targetData.TargetGain80.Add(-999);
                targetData.TargetGain80.Add((float)waveRule.targetGains[1][10]);

                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));
                targetData.TargetMPO.Add((short)waveRule.CalculateUCL(activeSide));

                Console.WriteLine("Suave");
                foreach (var value in waveRule.targetGains[0])
                {
                    Console.WriteLine(value);
                }
                Console.WriteLine("Moderado");
                foreach (var value in waveRule.targetGains[1])
                {
                    Console.WriteLine(value);
                }
                Console.WriteLine("Alto");
                foreach (var value in waveRule.targetGains[2])
                {
                    Console.WriteLine(value);
                }
                Console.WriteLine("UCL:");
                Console.WriteLine(waveRule.CalculateUCL(activeSide));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void loadMicModel()
        {
            IntriconPlotObject micTable = new IntriconPlotObject();

            micTable = this.fileUtils.openCurveFileLegacy(defaultMicrophone);

            for (int i = 0; i <= 64; i++)
            {
                this.micFR[i] = (float)micTable.getResponsePoint(i);
            }
        }

        public void loadRecModel()
        {
            IntriconPlotObject recTable = new IntriconPlotObject();

            recTable = this.fileUtils.openCurveFileLegacy(defaultReceptor);

            for (int i = 0; i <= 64; i++)
            {
                this.recFR[i] = recTable.getResponsePoint(i);
            }
        }

        public float[] CopySingleCollection23ToArray(SingleCollection input, Int16 colSize)
        {
            float[] output = new float[colSize];
            for (int i = 0; i < colSize; i++)
            {
                output[i] = input[i];
            }
            return output;
        }

        public SingleCollection CopyArray23ToStringCollection(float[] input, Int16 colSize)
        {
            SingleCollection output = new SingleCollection();
            for (int i = 0; i < colSize; i++)
            {
                output.Add(input[i]);
            }
            return output;
        }

        public void Autofit(WaveRule waveRule)
        {
            targetAudion16 = new TargetAudion16();

            WriteTarget(targetAudion16, waveRule);

            float[] targ50 = CopySingleCollection23ToArray(targetAudion16.TargetGain50, 23);
            float[] targ80 = CopySingleCollection23ToArray(targetAudion16.TargetGain80, 23);
            float[] targMpo = CopySingleCollection23ToArray(targetAudion16.TargetMPO, 23);

            targ50 = (GDriver as G_Audion16).InterpolateTarget(targ50);
            targ80 = (GDriver as G_Audion16).InterpolateTarget(targ80);
            targMpo = (GDriver as G_Audion16).InterpolateTarget(targMpo);

            targetAudion16.TargetGain50 = CopyArray23ToStringCollection(targ50, 23);
            targetAudion16.TargetGain80 = CopyArray23ToStringCollection(targ80, 23);
            targetAudion16.TargetMPO = CopyArray23ToStringCollection(targMpo, 23);

            GDriver.SetRecSaturation(883883);

            GetMicRec();

            this.errorCode = (GDriver as G_Audion16).AutoFit(24, 0, targetAudion16);
            if (this.errorCode != 0)
            {
                Console.WriteLine("Autofit() error: " + DriverErrorString(this.errorCode));
                Growl.ErrorGlobal(DriverErrorString(errorCode));
            }
            else
            {
                ampParams[activeSide] = GDriver.GetParams();
                ampConfig[activeSide] = GDriver.GetConfig();

                if (this.errorCode != 0)
                {
                    Console.WriteLine("Get_params(): error: " + DriverErrorString(this.errorCode));
                    Growl.ErrorGlobal(DriverErrorString(errorCode));
                }
                else
                {
                    Growl.SuccessGlobal("Autofit realizado com sucesso.");
                    UpdateParamBoxesFromParams(this.activeSide, this.activeProgram);
                }
            }
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

        public void CopyProgram(short fromProgram, short toProgram, short activeSide)
        {
            ampParams[activeSide][toProgram]["Input_Mux"] = ampParams[activeSide][fromProgram]["Input_Mux"];
            ampParams[activeSide][toProgram]["Matrix_Gain"] = ampParams[activeSide][fromProgram]["Matrix_Gain"];

            ampParams[activeSide][toProgram]["Preamp_Gain_1"] = ampParams[activeSide][fromProgram]["Preamp_Gain_1"];
            ampParams[activeSide][toProgram]["Preamp_Gain_Digital_1"] = ampParams[activeSide][fromProgram]["Preamp_Gain_Digital_1"];
            ampParams[activeSide][toProgram]["Preamp_Gain_2"] = ampParams[activeSide][fromProgram]["Preamp_Gain_2"];
            ampParams[activeSide][toProgram]["Preamp_Gain_Digital_2"] = ampParams[activeSide][fromProgram]["Preamp_Gain_Digital_2"];

            ampParams[activeSide][toProgram]["Feedback_Canceller"] = ampParams[activeSide][fromProgram]["Feedback_Canceller"];
            ampParams[activeSide][toProgram]["Noise_Reduction"] = ampParams[activeSide][fromProgram]["Noise_Reduction"];
            ampParams[activeSide][toProgram]["Wind_Suppression"] = ampParams[activeSide][fromProgram]["Wind_Suppression"];
            ampParams[activeSide][toProgram]["Input_Filter_Low_Cut"] = ampParams[activeSide][fromProgram]["Input_Filter_Low_Cut"];
            ampParams[activeSide][toProgram]["Low_Level_Expansion"] = ampParams[activeSide][fromProgram]["Low_Level_Expansion"];

            ampParams[activeSide][toProgram]["MPO_Attack"] = ampParams[activeSide][fromProgram]["MPO_Attack"];
            ampParams[activeSide][toProgram]["MPO_Release"] = ampParams[activeSide][fromProgram]["MPO_Release"];

            for (int i = 1; i <= 16; i++)
            {
                ampParams[activeSide][toProgram][$"BEQ_Gain_{i}"] = ampParams[activeSide][fromProgram][$"BEQ_Gain_{i}"];
                ampParams[activeSide][toProgram][$"MPO_Threshold_{i}"] = ampParams[activeSide][fromProgram][$"MPO_Threshold_{i}"];
            }

            for (int i = 1; i <= 16; i++)
            {
                ampParams[activeSide][toProgram][$"Comp_Ratio_{i}"] = ampParams[activeSide][fromProgram][$"Comp_Ratio_{i}"];
                ampParams[activeSide][toProgram][$"Comp_Threshold_{i}"] = ampParams[activeSide][fromProgram][$"Comp_Threshold_{i}"];
                ampParams[activeSide][toProgram][$"Comp_Time_Consts_{i}"] = ampParams[activeSide][fromProgram][$"Comp_Time_Consts_{i}"];
            }
        }

        public void CopyProgramFromTo(short fromProgram, short toProgram, short activeSide)
        {
            CopyProgram(fromProgram, toProgram, activeSide);
        }

        public void TestAlert(short program)
        {
            GDriver.TestTone(program);
        }
    }
}