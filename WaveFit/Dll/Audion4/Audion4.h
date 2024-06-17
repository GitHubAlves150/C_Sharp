// Audion4.h : main header file for the Audion4 DLL

/*	Revision history:
		
version 4.0.0.0 - first pass read & load functions for Audion4 device
version 4.0.1.11 - first dll beta release
version 4.0.1.12 - DXS
	* Now validating Program set by user in Set_active_program, SetProgram, and Audio_on. Takes values for Number_of_programs and ATC into account.
	 - Not allowing Active_PGM to be set by config param even when simulating. Must use functions set aside for that.
	* Remove CoilPGM config param and force it to program 5 like other amps.
	* Finished remaining params/configs range testing. 
	  - Fixed Switch_Tone MAX, input_mux MAX, Program_StartUp MAX, VC_Startup MAX, Trimmer positions max, and VC Position max.
	  - No longer can set active program even when simulating.
	  - Config params for Trimmer positions and VC Position can now only be set when simulating. Otherwise it's forced to the last read values.
	* Fixed SetProgram to call correct function when called from VB6 or other ODL based languages.
	* Remove ReadMDA from ODL.
version 4.0.1.13 - DXS
	* Fix Program_StartUp range validation to make sure it sets back to a in-range value.
	* Read now sets the active program to the Active_PGM value read from device.
	* Fix possible bug in checksum validation due to wrong data type declaration of locally stored value.
	* Add parameter validation to Read() for the very unlikely cases of EEPROM issue or comm issue that checksum doesn't catch.
	* Fix return value from Read() which was unnesscarily returning the positive result of a chip ID Validation.
version 4.0.1.14 - DXS
	* Finish off code for NoahLink. Untested.
version 4.0.1.15 - DXS
	* Fix to digital and Rocker VC position calcuations in Modeler and Autofit.
	* nzTrimmerMappedMoreThanOnce error value changed to elimiate clash with another amp type.
version 4.0.1.16 - DXS
	* Fix to modeler to show properly adjusted response when input mux is hyper-cardiod.
	* Add "test" param back into config params to protect from accidential overwriting of calibration settings.
version 4.0.1.17 - DXS
	* Reordering of start of Autofit algorithm to make sure VC position is at max and everything is max linear when MPO is calculated.
version 4.0.1.18 - DXS
	* Fixed a problem where VC position, and trimmer positions for a right side detected aid would show the left aid values. The
	  same problem would cause range corrected values on the right side to end up with the left side's values.
version 4.0.1.19 - DXS
	* Fixed a problem in Autofit where Threshold was getting set to the wrong value off by 1 index. This was due to a change in the threshold values 
	  in the amp that didn't get propagated to the driver.
version 4.0.1.20 - DXS
	* Fixed a problem with Set_active_program where it returns nzNotInitialized when simulating. Now Set_active_program will work with simulate.
version 4.0.1.2x - JJ
	* Fixed error with Noahlink programmer.  (It should have faild the same way with other programmers??)
version 4.0.1.25 - JJ
	* Fixed Noahlink detect error detecting Audion 6 devices.	
version 4.0.1.30 - JJ
	* Fixed Noahlink issue loading MDA
version 4.0.1.31 - DXS
    * Fix Load MDA Checksum issue with binaural instruments.
	* Add function Set_extension() to set the extension programmer path & name
version 4.0.1.32 - DXS
	* Fix: Change Set_MDA to not allow changing of Read Only params causing checksum error if GetMDA not used.
version 4.0.2.0 - DXS
	* Fix: Validate_Chip_ID changed to returning nzNoInstrument	if issue (was returning nzWrongInstrument)
*/


#pragma once

////// structures

#define numParams 27

typedef union {
	struct {
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
		short C1_ratio[5];
		short C2_ratio[5];
		short C3_ratio[5];
		short C4_ratio[5];
		short Expansion_Enable[5];
		short FBC_Enable[5];
		short High_Cut[5];
		short input_mux[5];
		short Low_Cut[5];
		short matrix_gain[5];
		short MPO_level[5];
		short Noise_Reduction[5];
		short preamp_gain0[5];
		short preamp_gain1[5];
		short threshold[5];
	};
	short param3[numParams][5];						// alternate way to access struct
} Audion4_Params;


#define numConfig 34

typedef union {
	struct {
		short ATC;
		short Auto_Save;
		short Cal_Input;
		short Dir_Spacing;			//mic spacing for directional processing
		short Low_Batt_Warning;
		short MAP_HC;
		short MAP_LC;
		short MAP_MPO;
		short MAP_TK;
		short Mic_Cal;
		short number_of_programs;
	    short Power_On_Level;
		short Power_On_Delay;	
		short Program_StartUp;
		short Out_Mode;					// OUT_MODE
	    short Switch_Mode;
		short Switch_Tone;
		short T1_DIR;
		short T2_DIR;
		short test;		// access flag for changing calibration config params
		short Tone_Frequency;
		short Tone_Level;
		short Time_Constants;
		short VC_AnalogRange;
		short VC_Beep_Enable;
	    short VC_DigitalNumSteps;
		short VC_DigitalStepSize;
		short VC_Enable;
		short VC_Mode;	
		short VC_Startup;	
		short Active_PGM; //read only from amp
		short T1_POS;  //read only from amp (in simulate can use this to adjust modeler)
		short T2_POS;  //read only from amp (in simulate can use this to adjust modeler)
		short VC_Pos;  //read only from amp (in simulate can use this to adjust modeler)
	};
	short config3[numConfig];
} Audion4_Config;


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


#define numManfReserveWords 94     		  

typedef struct {
	short AlgVer_Major; 		// Read Only
	short AlgVer_Minor;			// Read Only
	short LayoutVersion;		//4 bits (decimal 0 through 15)
	short ManufacturerID;		//12 bits (decimal 0 through 4095)
	short Platform_ID;			// Read Only
	long ModelID;				//ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long PassCode;				//ONLY 16 BITS!!!! (decimal 0 through 65,535)
	long MANF_reserve[numManfReserveWords];	//ONLY 16 BITS!!!! (decimal 0 through 65,535)
} Audion4_MDA;


//typedef	struct {
//		float sng50[11];
//		float sng80[11];
//		short MPO;
//		short ResGain;
//} Audion4_Target;

typedef	struct {
	float sng50[11]; //these are 50 dB target gain values
	float sng80[11]; //these are 80 dB target gain values
	float CR[4];  //These are actual ratio values from 1.0 to 4.0 and driver rounds to nearest value. Only used if userCR = 1.
	short TK;	 // uses this no matter what, but MAP_TK trimmer will override. This a dB SPL value from 45 to 75 and driver rounds to nearest value. In past for Intricon Algorithm autofit would set to 50 dB.
	short MPO;	 // uses this no matter what, but MAP_MPO trimmer will override.
	short ResGain; //this is amount of gain above this VC position to get full volume (how many dB below top of VC to to do the autofit at)
	short useCR; //0 = calculate compression ratios, 1 = use CR's passed in
	short isNL2; //0 = target from Intricon algorithm (NAL Linear + FIG6), 1 = target from NL2.
} Audion4_Target;


#ifndef NZConstants
	#define NZConstants

	//Sizing specific for Audion4 Params+Config
	const short numPreBits = 16;
	const short numDataBits = 64;
	const short numNZBits = numPreBits + numDataBits;

	const short numMdaDataBits = 160;
	const short numMdaNZBits = numPreBits + numMdaDataBits;

	#ifndef NUMFR
		#define NUMFR
		const short numFR = 65;
	#endif

	typedef struct {
		float element[numFR];
	} Audion4_Response;
		
	// read/write commands
	enum nzCommand {
		cmdREAD =		0,
		cmdLOAD =		1,
		cmdLOCK =		2,
		cmdAUDIO_ON =	3,
		cmdDETECT	=	4,

		cmdReadMDA =	5,
		cmdLoadMDA =	6,
		cmdLockMDA	=	7,

		cmdTestTone =		9,

		cmdGetEEPROMData = 10,

		cmdSetActiveVCPostion = 14,

		cmdGetStatus =  90,
		cmdResetStatus = 91,
		cmdConnected = 92,
		cmdMute = 93,

		cmdDetectOld = 102  //temporary for NoahLink

	};

	// number_of_programs
	const short nbrprog1Program =	0;
	const short nbrprog2Program =	1;
	const short nbrprog3Program =	2;
	const short nbrprog4Program =	3;
	const short nbrprog5Program =	4;

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
		typeExtension =    100
	};

	//section parameter options for Read(), Load()
	enum dataPass { 
		loadProgramConfigUpdates = -2,  //For load only. Only loads programs and config changes since last params update via load or read
		passAll = -1,
		passParamsAndConfig = 0,
		passMDA = 1,
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
		// sta - extensions
		nzLoadExtension   = 19,		//unable to load extension module
		nzCallExtension   = 20,		//unable to call extension function
		nzBufferOverrun   = 21,		//read buffer overrun
		nzWrongVCMode       = 48,	//function or arguments used are invalid for the current VC mode
		nzWrongPasscode   = 49,
		nzMemoryAllocationError = 50, //failed attempt to acquisition memory for the requested process

		//Parameter value errors: the following parameter errors are automatically corrected for so they can generally be ignored
		nzProgramParameterOutOfRange = 90, //at least on param sent in through one of the Set_Params functions was out of range and remains unchanged. 
		nzConfigParameterOutOfRange = 91, //at least one config param sent in through one of the Set_Config function was out of range and remains unchanged. 
		nzExpTkSetOverCompTkError = 100,  //invalid attempt to place expansion threshold equal to or higher than compression threshold
		nzTrimmerMappedMoreThanOnce = 103  //at least one trimmer attempted to be mapped to more than one parameter and the mappings remain unchanged. 
	};

#endif  //#define NZConstants
	

////// functions
// all functions return an error code

short 	__stdcall Audio_on(short active_program);
short	__stdcall AutoFit(short Datversion, short ManID, Audion4_Target *target_params);
short	__stdcall Close(void);
short 	__stdcall Connected(void);
short 	__stdcall Detect(Generic_Detect_data *detect);
short	__stdcall Get_active_program(short *program);
short	__stdcall Get_Autofit_Matrix_gain_ceiling(short *ceiling);
short	__stdcall Get_config(short version, Audion4_Config *config);
short	__stdcall GetEEPROMData(void);
short	__stdcall Get_FR_array(short input_level, Audion4_Response *response);
short	__stdcall Get_interface_type(short *type);
short 	__stdcall Get_last_interface_error(void);
short	__stdcall Get_MDA(long Passcode, short version, Audion4_MDA *MDAparams); //NOTE: Passcode is only 16bits: 0 to 65535.
short	__stdcall GetNextParamError(LPSTR paramName, LPSTR errorDescription);  //cycle through until paramName or errorDescription is empty.
short	__stdcall Get_params(short version, Audion4_Params *params);
short	__stdcall Get_RL_channel(short *channel);
short	__stdcall haveAnyParamErrors(void);
short 	__stdcall InitializeDriver(void);
short 	__stdcall Load(short section);
short 	__stdcall Lock(short section);
short	__stdcall Mute(void);
short 	__stdcall Read(short section);
short	__stdcall Set_active_program(short program);
short 	__stdcall Set_Active_VC_Position(short position);
short	__stdcall Set_Autofit_Matrix_gain_ceiling(short ceiling);
short	__stdcall Set_config(short version, Audion4_Config *config);
short	__stdcall Set_extension(char* path);  //Sets path to Extended Programmer Driver when m_interface_type = typeExtension
short	__stdcall Set_interface_type(short type);
short	__stdcall Set_MDA(short version, Audion4_MDA *MDAparams);
short 	__stdcall Set_mic_response(Audion4_Response *mic_array);
short	__stdcall Set_params(short version, Audion4_Params *params);
short	__stdcall Set_PassCode(long CurrentPassCode, long NewPassCode); //NOTE: both CurrentPassCode and NewPassCode are only 16bits: 0 to 65535.
short	__stdcall Set_platform_id(short ID);
short	__stdcall SetProgram(short program);
short 	__stdcall Set_rec_response(Audion4_Response *rec_array);
short	__stdcall SetRec_Saturation(float Sat_level);  //Defaults to 883883.0 (2.5Vpp converted to uVrms)
short	__stdcall Set_RL_channel(short channel);
short	__stdcall SetToTest(short Datversion, short ManID);
short	__stdcall TestTone(short numBeeps);


