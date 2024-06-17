using G_Enums;
using GenericAudion16;
using GenericCommon;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WaveFit2.Calibration.ViewModel
{
    public class IntriconViewModel
    {
        public Dictionary<int, String> HearingDevices;

        private G_Common GDriver;

        private ParamsDictionary detectDataParams;

        //Audion8 audion8 = new Audion8();

        public Int16 activeProgram;
        public Int16 activeSide;
        public Int16 DLLVER = 1;

        public bool ProgrammerInitialized;
        public int errorCode;

        public int[] detectedHI = { 0, 0 };
        public int detectProgrammer = 0;

        public IntriconViewModel()
        {
            GDriver = new G_Audion16();
            Dictionary();
        }

        public void StartProgrammer(char side)
        {
            if (detectProgrammer == 1)
            {
                SetChannel(side);
                Detect(side);
            }
            else
            {
                detectedHI[0] = 0;
                detectedHI[1] = 0;
                detectProgrammer = 0;
            }
        }

        public void SetChannel(char side)
        {
            if (side == 'L')
            {
                activeSide = 0;
                this.errorCode = GDriver.SetRLChannel(this.activeSide);
                if (this.errorCode == 0)
                {
                    Growl.SuccessGlobal("Canal esquerdo conectado");
                }
                else
                {
                    Growl.WarningGlobal(DriverErrorString(errorCode));
                }
            }
            else
            {
                activeSide = 1;
                this.errorCode = GDriver.SetRLChannel(this.activeSide);
                if (this.errorCode == 0)
                {
                    Growl.SuccessGlobal("Canal direito conectado");
                }
                else
                {
                    Growl.WarningGlobal(DriverErrorString(errorCode));
                }
            }
        }

        public void InitializeProgramer()
        {
            this.errorCode = GDriver.SetInterfaceType(interface_type.typeHipro);
            if (this.errorCode == 0)
            {
                this.ProgrammerInitialized = true;
                Growl.SuccessGlobal("Programadora inicializada.");
                detectProgrammer = 1;
            }
            else
            {
                Growl.WarningGlobal(DriverErrorString(errorCode));
                detectProgrammer = 0;
            }
        }

        public void CloseProgramer()
        {
            this.errorCode = GDriver.CloseInterface();
        }

        public void Detect(char side)
        {
            if (side == 'L')
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                this.errorCode = GDriver.Detect();
                detectDataParams = GDriver.GetDetectionInfo();

                if (this.errorCode != 0)
                {
                    Growl.WarningGlobal(DriverErrorString(errorCode));
                    detectedHI[0] = 0;
                    Properties.Settings.Default.ChipIDL = "Null";
                }
                else
                {
                    Growl.SuccessGlobal("Aparelho esquerdo detectado:" + ChipName());
                    detectedHI[0] = 1;
                    Properties.Settings.Default.ChipIDL = HearingDevices[GDriver.GetAmpTypeDetected()];
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
            }
            else
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                this.errorCode = GDriver.Detect();
                detectDataParams = GDriver.GetDetectionInfo();

                if (this.errorCode != 0)
                {
                    Growl.WarningGlobal(DriverErrorString(errorCode));
                    detectedHI[1] = 0;
                    Properties.Settings.Default.ChipIDR = "Null";
                }
                else
                {
                    Growl.SuccessGlobal("Aparelho direito detectado: " + ChipName());
                    detectedHI[1] = 1;
                    Properties.Settings.Default.ChipIDR = HearingDevices[GDriver.GetAmpTypeDetected()];
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
            }
        }

        public string ChipName()
        {
            string typechip = GDriver.GetAmpDetected().ToString();

            switch (typechip)
            {
                case "typeSpinNr":
                    return "Nise";

                case "typeAudion4":
                    return "Mauá";

                case "typeAudion6":
                    return "Ada";

                case "typeAudion8":
                    return "Dumont";

                case "typeAudion16":
                    return "Landell";

                default:
                    return "";
            }
        }

        public void Dictionary()
        {
            HearingDevices = new Dictionary<int, String>
            {
                {16, "SpinNR"},
                {21, "Audion4"},
                {20, "Audion6"},
                {23, "Audion8"},
                {24, "Audion16"}
            };
        }

        public string DriverErrorString(int ErrorCode)
        {
            string DriverErrorString;

            switch (ErrorCode)
            {
                case 0:
                    DriverErrorString = "Êxito: Sem erros detectados. Operação concluída com sucesso.";
                    break;

                case 1:
                    DriverErrorString = "Sem programadora: Verifique a conexão entre programadora e o PC.";
                    break;

                case 2:
                    DriverErrorString = "Aparelho auditivo não conectado: Verifique a conexão entre o aparelho e programadora.";
                    break;

                case 3:
                    DriverErrorString = "Argumento inválido: Um argumento fornecido é inválido ou incorreto.";
                    break;

                case 4:
                    DriverErrorString = "Programadora não Inicializada: A programadora não foi inicializada corretamente.";
                    break;

                case 5:
                    DriverErrorString = "Não Lido: Os dados não puderam ser lidos.";
                    break;

                case 6:
                    DriverErrorString = "Erro de Soma de Verificação: Houve um erro na verificação da integridade dos dados.";
                    break;

                case 7:
                    DriverErrorString = "Versão Inválida: A versão do software ou do dispositivo é incompatível.";
                    break;

                case 8:
                    DriverErrorString = "Erro do Programador: Houve um erro durante a execução do programa.";
                    break;

                case 9:
                    DriverErrorString = "Erro CMF: Ocorreu um erro no fluxo de comunicação.";
                    break;

                case 10:
                    DriverErrorString = "Aparelho Errado: O dispositivo conectado não é o correto para esta operação.";
                    break;

                case 11:
                    DriverErrorString = "Erro de Inicialização: Houve um erro durante a inicialização do sistema.";
                    break;

                case 12:
                    DriverErrorString = "Sem Driver NL: O driver necessário para o dispositivo não está instalado.";
                    break;

                case 13:
                    DriverErrorString = "NL em Uso: O dispositivo já está em uso por outro processo.";
                    break;

                default:
                    DriverErrorString = "Erro # " + ErrorCode + " é um código de erro desconhecido.";
                    break;
            }
            return DriverErrorString;
        }
    }
}