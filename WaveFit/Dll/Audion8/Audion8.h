// Audion8.h : main header file for the Audion8 DLL
/*
/////////   Revision history  //////////////////////////
		
version 0.6.0 -	2013/10/11
	*First internal beta release
version 0.6.1.4 - 2014/03/27
	* First releasable version
version 1.0.0.0 - 2014/09/02
	* Added and Get_Voice_Prompt_Language() and VpLanguage_type enum.
version 1.1.0.0 - 2015/01/07
	* Fixed bug with Detecting Audion 8 using Noahlink Programmer
version 1.2.0.0 - 2015/02/16
	* Adjustments to AFit_Audion8_N():
		- Now CR rounds down for any value below next setting.
		- Now SpeechTK uses negative numbers to set all TKs to absolute value of passed in value.



//////////////////////////////////////////////////////
*/

#pragma once

////// structures
//
#define numAudion8Params 43

typedef union {
	struct {
		short input_mux[5];
		short preamp_gain0[5];
		short preamp_gain1[5];
		short C1_Ratio[5];				//short CA_ratio[5];
		short C2_Ratio[5];				//short CB_ratio[5];
		short C3_Ratio[5];
		short C4_Ratio[5];
		short C5_Ratio[5];				//short CA_ratio[5];
		short C6_Ratio[5];				//short CB_ratio[5];
		short C7_Ratio[5];
		short C8_Ratio[5];
		short C1_TK[5];
		short C2_TK[5];
		short C3_TK[5];
		short C4_TK[5];
		short C5_TK[5];
		short C6_TK[5];
		short C7_TK[5];
		short C8_TK[5];
		short C1_MPO[5];
		short C2_MPO[5];
		short C3_MPO[5];
		short C4_MPO[5];
		short C5_MPO[5];
		short C6_MPO[5];
		short C7_MPO[5];
		short C8_MPO[5];
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
		short matrix_gain[5];
		short Noise_Reduction[5];
		short FBC_Enable[5];
		short TimeConstants[5];
	};
	short param4[numAudion8Params][5];						// alternate way to access struct
} Audion8_Params;

#define numAudion8Config 52     	//Note: count long data types as 2 words (2*short data type)

typedef union {
	struct {
        short AutoSave_Enable;		// New for Audion 8 (remembers current program and VC setting, otherwise uses 'startup' params)
		short ATC;
		short EnableHPmode;	
		short Noise_Level;
		short POL;					//power on level
		short POD;					//power on delay
		short AD_Sens;				//sensitivity control for adaptive directional
		short Cal_Input;
		short Dir_Spacing;			//mic spacing for directional processing
		short Mic_Cal;
		short number_of_programs;
		short PGM_Startup;			// New for Audion 8
		short Switch_Mode;
		short Program_Prompt_Mode;  // New for Audion 8
		short Warning_Prompt_Mode;  // New for Audion 8  //low battery & other warnings
		short Tone_Frequency;
		short Tone_Level;
		short VC_Enable;			// New for Audion 8
		short VC_Analog_Range;	
        short VC_Digital_Numsteps;			// New for Audion 8
		short VC_Digital_Startup;			// New for Audion 8
        short VC_Digital_Stepsize;			// New for Audion 8
		short VC_Mode;
		short VC_pos;				// (Read Only)
		short VC_Prompt_Mode;		// New for Audion 8
		short AlgVer_Major;			// (Read Only)
		short AlgVer_Minor;			// (Read Only)
		short MANF_ID;
		short PlatformID;			//PlatformID (Read Only)
		short reserved1;
		short reserved2;			//checksum  (Read Only)
		short test;                 // access flag for changing reserved data
		long  MANF_reserve_1;
		long  MANF_reserve_2;
		long  MANF_reserve_3;
		long  MANF_reserve_4;
		long  MANF_reserve_5;
		long  MANF_reserve_6;
		long  MANF_reserve_7;
		long  MANF_reserve_8;
		long  MANF_reserve_9;
		long  MANF_reserve_10;
	};
	short config4[numAudion8Config];
} Audion8_Config;


typedef struct {			//Note: count long data types as 2 words (2*short data type)
	short Platform_ID;			// Read Only
	short AlgVer_Major;			// Read Only
	short AlgVer_Minor;			// Read Only
	short LayoutVersion;		//Audion4 or Audion6 only
	short MANF_ID;				
	short ModelID;				//Audion4 or Audion6 only
	short reserved1;			//empty for Audion4 or Audion6
	short reserved2;			//empty for Audion4 or Audion6
	short reserved3;			//empty for Audion4 or Audion6
	short reserved4;			//empty for Audion4 or Audion6
	long  MANF_reserve_1;		//if Audion4 or Audion6 then same as MANF_reserve[0]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_2;		//if Audion4 or Audion6 then same as MANF_reserve[1]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_3;		//if Audion4 or Audion6 then same as MANF_reserve[2]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_4;		//if Audion4 or Audion6 then same as MANF_reserve[3]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_5;		//if Audion4 or Audion6 then same as MANF_reserve[4]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_6;		//if Audion4 or Audion6 then same as MANF_reserve[5]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_7;		//if Audion4 or Audion6 then same as MANF_reserve[6]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_8;		//if Audion4 or Audion6 then same as MANF_reserve[7]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_9;		//if Audion4 or Audion6 then same as MANF_reserve[8]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long  MANF_reserve_10;		//if Audion4 or Audion6 then same as MANF_reserve[9]. ONLY 16 BITS!!!! (decimal 0 through 65,535)
} Generic_Detect_data;


//#define numNZTarget 4
typedef	struct {
		float sng50[11];
		float sng80[11];
		short MPO;
		short ResGain;
} Audion8_Target;

typedef	struct {
		float sng50[11];  //switch to 1/3 octave frequencies 
		float sng80[11];
		float CR[8];
		short SpeechTK; //55 = 55dB speech equivilant (55,55,50,50,45,45,40,40); 60 = 65 dB speech equivilant, neg values set all TK to that absolute value
		short MPO[8];
		short ResGain;
		short Use_CR;  //1 = use values passed in with CR, 0 = calculate them based on sng80 to sng50 difference
}Audion8_Target2;		


const short numDataLogBits = 2424;  //per page
const short numDataLogPages = 5;
const short numTotalDataLogWords = ((numDataLogBits * numDataLogPages) / 24);

#ifndef NZConstants
#define NZConstants


const short numPreBits = 16;
const short numDataBits = 64;  //per page
const short numNZBits = numPreBits + numDataBits;


// read/write commands
	enum nzCommand {
		cmdREAD =		0,
		cmdLOAD =		1,
		cmdLOCK =		2,
		cmdAUDIO_ON =	3,
		cmdDETECT	=	4,
		cmdGetDataLog =	5,
		cmdSetActiveVCPos =  6,
		//cmdGetVCStart = 7,
		cmdConnected = 8,
		cmdTestPrompt = 9,
		cmdGetEEPROMData = 10,
		cmdResetDataLog = 11,
		cmdSetValidationMode = 12,
		cmdDoValTone = 13,
		cmdMute = 15,

		cmdDetectOld = 102  //temporary for NoahLink
	};

// number_of_programs
	const short nbrprog1Program =	0;
	const short nbrprog2Program =	1;
	const short nbrprog3Program =	2;
	const short nbrprog4Program =	3;
	const short nbrprog5Program =	4;

// program_switch_mode
	const short swmodeMomentary =	0;
	const short swmodeStatic =		1;
	const short swmodemultiPB =		2;

//Read inputs
	const short readAll		=	-1;
	const short readManf	=	-2;

//Load inputs
	const short loadAll		=	-1;
	const short	loadUpdates	=	-2;

// RL_channel
	enum RL_channel {
		channelLeft =		0,
		channelRight =		1
	};

// interface_type
	enum interface_type {
		typeHipro =			0,
		typeMicrocard =		1,
		typeSimulation =	2,
		typeNoahlink =		3,
		typeEMiniTec =		5,
		typeNanoLink =		6,
		typeExtension =		100
	};

	//Types for voice prompt language
	typedef enum VpLanguage_type {
		English =	1,
		Russian =	2,
		Turkish =	3,
		Chinese =	4,
		German  =	5
	};

	// error codes
	enum errorCode {
		nzOK =				0,		// no error
		nzNoProgrammer =	1,		// no programmer interface found
		nzNoInstrument =	2,		// no hearing instrument found
		nzBadArgument =		3,		// function was passed invalid data
		nzNotInitialized =	4,		// attempt to operate on aid prior to initialization of programming device
		nzNotRead =			5,		// attempt to operate on aid data prior to reading aid data
		nzChecksumError =	6,
		nzInvalidVersion =	7,		// Version parameter passed to NZ_get/set_params or NZ_get/set_config is invalid
		nzProgrammerError = 8,		// generic Comm error with programmer
		nzCMFError =		9,		// Checksum Match Flag error (i.e. error during previous load)
		nzWrongInstrument = 10,		//Instrument connected is not the one specified in Set_Platform_ID
		nzBootError       = 11,     //Recoverable boot error
		nzNoNLDriver      = 12,		//Noahlink drivers have not been installed
		nzNLInUse		  = 13,     //Noahlink in use by another software package
		nzNLNo_Programmer = 14,     //Nanolink command alive failed (no remote prgrammer found)
		nzNL_Write_Error  = 15,     //Nanolink Write error
		nzNL_Read_Error  = 16,     //Nanolink read error
		nzNL_Alive_Failed  = 17,     //Nanolink Alive command failed
		nzNL_LLCOM_Failed  = 18,     //General LLCOM Failure
		nzLoadExtension   = 19,		//unable to load extension module
		nzCallExtension   = 20,		//unable to call extension function
		nzBufferOverrun   = 21,		//read buffer overrun
//		nzWrongVCMode       = 48,	//function or arguments used are invalid for the current VC mode
		nzMemoryAllocationError = 50 //failed attempt to acquisition memory for the requested process

	};


#endif





#ifndef NUMFR
#define NUMFR
const short numFR = 65;
#endif

typedef struct {
	float element[numFR];
} Audion8_Response;


typedef struct {
	short element[numFR];
} Audion8_Response_Int;

typedef struct {
	long EventStatus;
} Audion8_Events;

const short numEvents = 500;

typedef struct {
	long DateCode;
	long Clock;
	long EventNum;
	long EventPtr;
	Audion8_Events Events[numEvents];
} Audion8_DataLog;



//num_TCtable
//#ifndef NUMTCtable
//#define NUMTCtable
//const short numTCtable = 17;
//#endif

//typedef struct {
//	short element[numTCtable];
//} TC_Table;
		
	

	// Signatures for the callback functions
typedef void __stdcall nz_ErrorCallback( void );
typedef void __stdcall nz_BatteryStatusCallback( BOOL BatteryLow, int reminderMinutes, BOOL showMessage );
typedef void __stdcall nz_PowerOffCallback( enum nl_poweroff_reason reason );

// API to register callback functions
void __stdcall SetSystemErrorCallback( nz_ErrorCallback *callback );
void __stdcall SetBatteryStatusCallback( nz_BatteryStatusCallback *callback );
void __stdcall SetPowerOffCallback( nz_PowerOffCallback *callback );
void __stdcall SetChannelErrorCallback( nz_ErrorCallback *callback );


////// functions
// all functions return an error code

short 	__stdcall InitializeDriver();
short	__stdcall Close();
short 	__stdcall Read(short page);
short 	__stdcall Load(short page);
short 	__stdcall Lock();
short 	__stdcall Detect(Generic_Detect_data *detect);
short 	__stdcall Connected();
short 	__stdcall Audio_on(short active_program);
short 	__stdcall Get_last_interface_error();
short	__stdcall Get_params(short version, Audion8_Params *params);
short	__stdcall Set_params(short version, Audion8_Params *params);
short	__stdcall Get_config(short version, Audion8_Config *config);
short	__stdcall Set_config(short version, Audion8_Config *config);
short	__stdcall Get_active_program(short *program);
short	__stdcall Set_active_program(short program);
short	__stdcall Set_RL_channel(short channel);
short	__stdcall Get_RL_channel(short *channel);
short	__stdcall Set_interface_type(short type);
short	__stdcall Get_interface_type(short *type);
short	__stdcall Get_FR_array(short input_level, Audion8_Response *response);
short   __stdcall Get_Voice_Prompt_Language(short *languageValue);
short 	__stdcall Set_mic_response(Audion8_Response *mic_array);
short 	__stdcall Set_rec_response(Audion8_Response *rec_array);
short	__stdcall AutoFit(short Datversion, short ManID, Audion8_Target *target_params);
short	__stdcall AutoFit2(short Earmold, Audion8_Target2 *target2_params);
short	__stdcall SetToTest(short Datversion, short ManID);
short	__stdcall Set_platform_id(short ID);
short	__stdcall Mute(void);
//short	__stdcall ResetBootStatus(void);
//short	__stdcall TestTone(short numBeeps);
short	__stdcall GetEEPROMData(void);
short	__stdcall Set_extension(char* path);  //Sets path to Extended Programmer Driver when m_interface_type = typeExtension
short	__stdcall SetValidationMode(short Mode);
short	__stdcall PlayTone(short Freq, short Level, short Duration);
short	__stdcall SetActive_VCPos(short VC_Postion);
//short	__stdcall GetVC_Start(short *Start_Val);
short	__stdcall Get_DataLog(short version, Audion8_DataLog *Data_Log);
short	__stdcall Reset_DataLog(short version);
short	__stdcall SetProgram(short program);
short	__stdcall SetRec_Saturation(float Sat_level);
short	__stdcall TestPrompt(short numBeeps, short Prompt1, short Prompt2);
short 	__stdcall isConnectedStealth(void);


void    fill_platform_data(short Platform_ID);
void	init_variables();

//short	__stdcall Load_Tube_Correction(TC_Table *tctable);
