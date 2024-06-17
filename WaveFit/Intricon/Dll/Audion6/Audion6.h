''// Audion6.h : main header file for the Audion6 DLL
//

#pragma once

#include "CompileSwitches.h"
#include "specialDetails.h"

////// structures
//

#define numParamsClassic 55
const short numberOfClassicParams = 55;

typedef struct {
	short ActiveProgram;			//Read Only
	short BEQ1_gain[5];
	short BEQ2_gain[5];
	short BEQ3_gain[5];
	short BEQ4_gain[5];
	short BEQ5_gain[5];
	short BEQ6_gain[5];
	short BEQ7_gain[5];
	short BEQ8_gain[5];
	short BEQ9_gain[5];
	short BEQ10_gain[5];
	short BEQ11_gain[5];
	short BEQ12_gain[5];
	short C1_ExpTK[5];
	short C2_ExpTK[5];
	short C3_ExpTK[5];
	short C4_ExpTK[5];
	short C5_ExpTK[5];
	short C6_ExpTK[5];
	short C1_MPO[5];
	short C2_MPO[5];
	short C3_MPO[5];
	short C4_MPO[5];
	short C5_MPO[5];
	short C6_MPO[5];
	short C1_Ratio[5];
	short C2_Ratio[5];
	short C3_Ratio[5];
	short C4_Ratio[5];
	short C5_Ratio[5];
	short C6_Ratio[5];
	short C1_TK[5];
	short C2_TK[5];
	short C3_TK[5];
	short C4_TK[5];
	short C5_TK[5];
	short C6_TK[5];
	short Exp_Attack[5];
	short Exp_Ratio[5];
	short Exp_Release[5];
	short FBC_Enable[5];
	short input_mux[5];
	short matrix_gain[5];
	short MPO_Attack[5];
	short MPO_Release[5];
	short Noise_Reduction[5];
	short preamp_gain0[5];
	short preamp_gain1[5];
	short TimeConstants1[5];
	short TimeConstants2[5];
	short TimeConstants3[5];
	short TimeConstants4[5];
	short TimeConstants5[5];
	short TimeConstants6[5];
	short VcPosition;				//Read Only
} Audion6_ParamsClassic;

#define numParamsHighLowGain 73
const short numberOfHighLowGainParams = 73;

typedef struct {
	short ActiveProgram;			//Read Only
	short BEQ1_gain[5];
	short BEQ2_gain[5];
	short BEQ3_gain[5];
	short BEQ4_gain[5];
	short BEQ5_gain[5];
	short BEQ6_gain[5];
	short BEQ7_gain[5];
	short BEQ8_gain[5];
	short BEQ9_gain[5];
	short BEQ10_gain[5];
	short BEQ11_gain[5];
	short BEQ12_gain[5];
	short C1_ExpTK[5];
	short C2_ExpTK[5];
	short C3_ExpTK[5];
	short C4_ExpTK[5];
	short C5_ExpTK[5];
	short C6_ExpTK[5];
	short C1_HighGain[5];
	short C2_HighGain[5];
	short C3_HighGain[5];
	short C4_HighGain[5];
	short C5_HighGain[5];
	short C6_HighGain[5];
	short C1_HighGainMin[5];		//Read Only: The high gain range changes. This parameter indicates the current minimum value of the Highgain parameter. It is always 0 to
	short C2_HighGainMin[5];		//Read Only: The Cx_HighGain will have a range from 0 to Cx_HighGainMin and Cx_HighGainMin will be a negative number.
	short C3_HighGainMin[5];		//Read Only
	short C4_HighGainMin[5];		//Read Only
	short C5_HighGainMin[5];		//Read Only
	short C6_HighGainMin[5];		//Read Only
	float C1_Ratio[5];				//Read Only: This are the resultant compression ratios used to get the High gain and low gain values.
	float C2_Ratio[5];				//Read Only:   Returned only for display purposes in engineering tools and shouldn't be used in Fitting Applications.
	float C3_Ratio[5];				//Read Only
	float C4_Ratio[5];				//Read Only
	float C5_Ratio[5];				//Read Only
	float C6_Ratio[5];				//Read Only
	short C1_LowGain[5];
	short C2_LowGain[5];
	short C3_LowGain[5];
	short C4_LowGain[5];
	short C5_LowGain[5];
	short C6_LowGain[5];
	short C1_MPO[5];
	short C2_MPO[5];
	short C3_MPO[5];
	short C4_MPO[5];
	short C5_MPO[5];
	short C6_MPO[5];
	short C1_TK[5];
	short C2_TK[5];
	short C3_TK[5];
	short C4_TK[5];
	short C5_TK[5];
	short C6_TK[5];
	short Exp_Attack[5];
	short Exp_Ratio[5];
	short Exp_Release[5];
	short FBC_Enable[5];
	short input_mux[5];
	short matrix_gain[5];
	short MPO_Attack[5];
	short MPO_Release[5];
	short Noise_Reduction[5];
	short preamp_gain0[5];
	short preamp_gain1[5];
	short TimeConstants1[5];
	short TimeConstants2[5];
	short TimeConstants3[5];
	short TimeConstants4[5];
	short TimeConstants5[5];
	short TimeConstants6[5];
	short VcPosition;				//Read Only
} Audion6_ParamsHighLowGain;

#define numConfigs 20
const short numberOfCofigParms = 20;

typedef union {
	struct {
		short Auto_Telecoil_Enable;
		short Cal_Input;
		short Dir_Spacing;				//mic spacing for directional processing
		short Low_Battery_Warning;
		short Mic_Cal;
		short number_of_programs;
		short Output_Mode;
		short Power_On_Delay;
		short Power_On_Level;
		short Program_Beep_Enable;
		short Program_StartUp;
		short Switch_Mode;
		short Tone_Frequency;
		short Tone_Level;
		short VC_AnalogRange;
		short VC_Enable;
		short VC_Mode;
		short VC_DigitalNumSteps;
		short VC_StartUp;
		short VC_DigitalStepSize;
	};
	short configIndex[numConfigs];		// alternate way to access struct
}Audion6_Config;

#define numManfReserveWords 94
const short numberOfManfReserveWords = 94;

typedef struct {
	short AlgVer_Major; 		// Read Only
	short AlgVer_Minor;			// Read Only
	short LayoutVersion;		//4 bits (decimal 0 through 15)
	short ManufacturerID;		//12 bits (decimal 0 through 4095)
	short ModelID;				//16 bits (decimal -32,768 through 32,767)
	short PassCode;				//16 bits (decimal -32,768 through 32,767)
	short Platform_ID;			// Read Only
	short MANF_reserve[numManfReserveWords];	//16 bits (decimal -32,768 through 32,767)
} Audion6_MDA;

typedef struct {
	short Platform_ID;			// Read Only
	short AlgVer_Major;			// Read Only
	short AlgVer_Minor;			// Read Only
	short LayoutVersion;		//Audion6 only
	short MANF_ID;
	long  MANF_reserve_1;		//if Audion6 then same as MANF_reserve[0]
	long  MANF_reserve_2;		//if Audion6 then same as MANF_reserve[1]
	long  MANF_reserve_3;		//if Audion6 then same as MANF_reserve[2]
	long  MANF_reserve_4;		//if Audion6 then same as MANF_reserve[3]
	long  MANF_reserve_5;		//if Audion6 then same as MANF_reserve[4]
	long  MANF_reserve_6;		//if Audion6 then same as MANF_reserve[5]
	long  MANF_reserve_7;		//if Audion6 then same as MANF_reserve[6]
	long  MANF_reserve_8;		//if Audion6 then same as MANF_reserve[7]
	long  MANF_reserve_9;		//if Audion6 then same as MANF_reserve[8]
	long  MANF_reserve_10;		//if Audion6 then same as MANF_reserve[9]
	short ModelID;				//Audion6 only
	short reserved1;			//empty for Audion6
	short reserved2;			//empty for Audion6
	short reserved3;			//empty for Audion6
	short reserved4;			//empty for Audion6
} Generic_Detect_data;

//#define numNZTarget 4
typedef	struct {
	float sng50[11];
	float sng80[11];
	short MPO;
	short ResGain;
} Audion6_Target;

typedef	struct {
	float sng50[11];  //switch to 1/3 octave frequencies
	float sng80[11];
	float CR[6];
	short SpeechTK;
	short MPO[6];
	short ResGain;
}Audion6_Target2;

#ifndef NZConstants
#define NZConstants

//prebits = 0-4 command bits
//          5-8 address bits
//			9-15 security ID = 0x50
// 10 words per page
// read/write commands
enum nzCommand {
	cmdREADParams = 0,
	cmdREADConfig = 1,
	cmdREADMDA = 2,
	cmdLOADParams = 3,
	cmdDetect = 4,
	cmdLOADConfig = 5,
	cmdLOADMDA = 6,
	cmdLOCKParams = 7,
	cmdLOCKConfig = 8,
	cmdLOCKMDA = 9,
	cmdAUDIO_ON = 10,
	cmdSetVCActivePosition = 11,
	cmdTestTone = 12,
	cmdGetEEPROMData = 13,
	cmdSetValidationMode = 14,
	cmdPlayValidationTone = 15,
	cmdMute = 31,
	cmdConnected = 32,
	cmdDetectOld = 33,
};

//page parameter options for Read(), Load(), Lock()
enum dataPass {
	loadProgramUpdates = -2,  //For load only. Only works with program params
	passAll = -1,
	passParamsAndConfig = 0,
	passMDA = 1,
	passParams = 2,
	passConfig = 3,
};

// number_of_programs
const short nbrprog1Program = 0;
const short nbrprog2Program = 1;
const short nbrprog3Program = 2;
const short nbrprog4Program = 3;
const short nbrprog5Program = 4;

// program_switch_mode
const short swmodeMomentary = 0;
const short swmodeStatic = 1;

//Read inputs
const short readAll = -1;
const short readManf = -2;

//Load inputs
const short loadAll = -1;
const short	loadUpdates = -2;

// RL_channel
enum RL_channel {
	channelLeft = 0,
	channelRight = 1
};

// interface_type
enum interface_type {
	typeHipro = 0,
	typeMicrocard = 1,
	typeSimulation = 2,
	typeNoahlink = 3,
	typeEMiniTec = 5,
	typeNanoLink = 6,
	typeExtension = 100
};

// error codes
enum errorCode {
	nzOK = 0,		// no error
	nzNoProgrammer = 1,		// no programmer interface found
	nzNoInstrument = 2,		// no hearing instrument found
	nzBadArgument = 3,		// function was passed invalid data
	nzNotInitialized = 4,		// attempt to operate on aid prior to initialization of programming device
	nzNotRead = 5,		// attempt to operate on aid data prior to reading aid data
	nzChecksumError = 6,		// data transfer checksum calculation not matching passed in checksum
	nzInvalidVersion = 7,		// Version parameter passed to NZ_get/set_params or NZ_get/set_config is invalid
	nzProgrammerError = 8,		// generic Comm error with programmer
	nzCMFError = 9,		// Checksum Match Flag error (i.e. error during previous load)
	nzWrongInstrument = 10,		//Instrument connected is not the one specified in Set_Platform_ID
	nzBootError = 11,     //Recoverable boot error
	nzNoNLDriver = 12,		//Noahlink drivers have not been installed
	nzNLInUse = 13,     //Noahlink in use by another software package
	nzNLNo_Programmer = 14,     //Nanolink command alive failed (no remote prgrammer found)
	nzNL_Write_Error = 15,     //Nanolink Write error
	nzNL_Read_Error = 16,     //Nanolink read error
	nzNL_Alive_Failed = 17,     //Nanolink Alive command failed
	nzNL_LLCOM_Failed = 18,     //General LLCOM Failure
	// sta - extensions
	nzLoadExtension = 19,		// unable to load extension module
	nzCallExtension = 20,		// unable to call extension function
	nzBufferOverrun = 21,		// read buffer overrun

	//Parameter value errors
	nzParamsFromOtherParamsMode = 89,   //user is in either Classic or High-Low and the params were last written to device in other mode.
	nzProgramParameterOutOfRange = 90, //at least on param sent in through one of the Set_Params functions was out of range and has been corrected.
	nzConfigParameterOutOfRange = 91, //at least one config param sent in through one of the Set_Config functions was out of range and has been corrected.
	nzExpTkSetOverCompTkError = 100,  //invalid attempt to place expansion threshold equal to or higher than compression threshold
	nzHighLow_HighGainSetTooLow = 101,
	nzHighLow_HighGainSetHigherThanLowGain = 102
};

#endif

#ifndef NUMFR
#define NUMFR
const short numFR = 65;
#endif

typedef struct {
	float element[numFR];
} Audion6_Response;

// Signatures for the callback functions
typedef void __stdcall nz_ErrorCallback(void);
typedef void __stdcall nz_BatteryStatusCallback(BOOL BatteryLow, int reminderMinutes, BOOL showMessage);
typedef void __stdcall nz_PowerOffCallback(enum nl_poweroff_reason reason);

// API to register callback functions
void __stdcall SetSystemErrorCallback(nz_ErrorCallback* callback);
void __stdcall SetBatteryStatusCallback(nz_BatteryStatusCallback* callback);
void __stdcall SetPowerOffCallback(nz_PowerOffCallback* callback);
void __stdcall SetChannelErrorCallback(nz_ErrorCallback* callback);

////// functions
// all functions return an error code
short 	__stdcall Audio_on(short active_program);
short	__stdcall AutoFit(short datVersion, short ManID, Audion6_Target* target_params);
//short	__stdcall AutoFit2(short datVersion, short Earmold, Audion6_Target2 *target2_params);
short	__stdcall Close();
short 	__stdcall Connected(void);
short 	__stdcall Detect(Generic_Detect_data* detect);
short	__stdcall Get_active_program(short* program);
short	__stdcall Get_Autofit_Matrix_gain_ceiling(short* ceiling);
short	__stdcall Get_config(short version, Audion6_Config* config);
short	__stdcall Get_Auto_Save_Flag(short* enableFlag);
//short	__stdcall Get_config2(short version, Audion6_Config2 *configA);
short	__stdcall GetEEPROMData(void);
short	__stdcall Get_FR_array(short input_level, Audion6_Response* response);
short   __stdcall Get_HighLow_HighReference(short* level);
short	__stdcall Get_interface_type(short* type);
//short	__stdcall Get_IntFR_array(short input_level, Audion6_Response_Int *response);
short 	__stdcall Get_last_interface_error(void);
short	__stdcall Get_MDA(short Passcode, short version, Audion6_MDA* MDAparams);
short	__stdcall Get_paramsClassic(short version, Audion6_ParamsClassic* params);
short	__stdcall Get_paramsHighLowGain(short version, Audion6_ParamsHighLowGain* params);
//short   __stdcall getParameterName(int index, LPSTR ppszString);
short	__stdcall GetNextParamError(LPSTR paramName, LPSTR errorDescription);  //cycle through until paramName or errorDescription is empty.
short	__stdcall Get_RL_channel(short* channel);
short	__stdcall haveAnyParamErrors(void);
short 	__stdcall InitializeDriver(void);
short 	__stdcall Load(short page);
short 	__stdcall LoadConfig(void);
short 	__stdcall LoadMDA(void);
short 	__stdcall LoadParams(void);
short 	__stdcall LoadParamUpdates(void);
short 	__stdcall Lock(short page);
short 	__stdcall LockConfig(void);
short 	__stdcall LockMDA(void);
short 	__stdcall LockParams(void);
short	__stdcall Mute(void);
short	__stdcall PlayValidationTone(short Freq, short Level, short Duration);
short 	__stdcall Read(short page);
short	__stdcall ReadParams(void);
short 	__stdcall ReadConfig(void);
short 	__stdcall ReadMDA(void);
//short 	__stdcall ReadParams(void);
short	__stdcall Set_Autofit_Matrix_gain_ceiling(short ceiling);
short	__stdcall Set_active_program(short program);
short 	__stdcall Set_Active_VC_Position(short position);
short	__stdcall Set_config(short version, Audion6_Config* config);
short	__stdcall Set_Auto_Save_Flag(short enableFlag);
//short	__stdcall Set_config2(short version, Audion6_Config2 *configA);
short	__stdcall Set_extension(char* path);  //Sets path to Extended Programmer Driver when m_interface_type = typeExtension
short   __stdcall Set_HighLow_HighReference(short level);
short	__stdcall Set_interface_type(short type);
short	__stdcall Set_MDA(short version, Audion6_MDA* MDAparams);
short 	__stdcall Set_mic_response(Audion6_Response* mic_array);
short	__stdcall Set_paramsClassic(short version, Audion6_ParamsClassic* params);
short	__stdcall Set_paramsHighLowGain(short version, Audion6_ParamsHighLowGain* params);
short	__stdcall Set_PassCode(short CurrentPassCode, short NewPassCode);
short 	__stdcall Set_rec_response(Audion6_Response* rec_array);
short	__stdcall Set_RL_channel(short channel);
short	__stdcall SetRec_Saturation(float Sat_level);  //Defaults to 883883.0 (2.5Vpp converted to uVrms)
short	__stdcall SetToTest(short Datversion, short ManID);
short	__stdcall Set_platform_id(short ID);
short	__stdcall SetProgram(short program);
short	__stdcall SetValidationMode(short Mode);
short	__stdcall TestTone(short numBeeps);

//void    fill_platform_data(short Platform_ID);
void	init_variables();

#ifdef _AUDION6_ENGINEERING_VERSION  //USE THE "CLASSIC" FUNCTIONS ABOVE
short	__stdcall Get_params(short version, Audion6_Params* params);
short 	__stdcall ReadParams(void);
#endif

//short	__stdcall Load_Tube_Correction(TC_Table *tctable);
