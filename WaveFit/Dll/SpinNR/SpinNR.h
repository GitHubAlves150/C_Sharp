// SpinNR.h : main header file for the SPINNR DLL
//

////// structures
//
#define numParams 26

typedef union {
	struct {
		short input_mux[5];
		short preamp_gain0[5];
		short preamp_gain1[5];
		short CRL[5];				//short CA_ratio[5];
		short CRH[5];				//short CB_ratio[5];
		//short CC_ratio[5];
		//short CD_ratio[5];
		short threshold[5];
		short Low_Cut[5];
		short High_Cut[5];
		short Noise_Reduction[5];
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
		short MPO_level[5];
		short FBC_Enable[5];
		short Cal_Input[5];
		short Mic_Cal[5];

	};
	short param3[numParams][5];						// alternate way to access struct
} SpinNR_Params;


#define numConfig 49			//Note: count long data types as 2 words (2*short data type)

typedef union {
	struct {
		short number_of_programs;
		short VC_MAP;
		short VC_Range;				// (Read Only)
		short VC_pos;
		short TK_MAP;
		short HC_MAP;
		short LC_MAP;
		short MPO_MAP;
		short T1_DIR;
		short T2_DIR;
		short T3_DIR;
		//short Channel_Config;
		short CoilPGM;
		//short Dir_Spacing;			//mic spacing for directional processing
		short MANF_ID;
		short OutMode;					// OUT_MODE
		short Switch_Tone;
		short Low_Batt_Warning;
		short Tone_Frequency;
		short Tone_Level;
		short ATC;
		short TimeConstants;
		short Mic_Expansion;
		short reserved1;			//PlatformID (Read Only)
		short reserved2;			// CCR - 8 bits		(Read Only)
		short reserved3;			
		short reserved4;			// checksum - 16 bits  (Read Only)
		short test;					// access flag for changing reserved data
		short T1_POS;
		short T2_POS;
		short T3_POS;
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
	short config3[numConfig];
} SpinNR_Config;


#ifndef NZConstants
#define NZConstants

const BYTE Spin_ID_CODE = 0x56;
const BYTE SpinNR_ID_CODE = 0x50;

const BYTE InTune_CHIP_ID_CODE = 0x58;
const BYTE NZ_CHIP_ID_CODE = 0x5A;
const BYTE NZ2_CHIP_ID_CODE = 0x55;
const BYTE NZ3_CHIP_ID_CODE = 0x59;
const BYTE Ethos_ID_CODE = 0x54;
const BYTE Clik_ID_CODE = 0x53;

const short numPreBits = 16;
const short numDataBits = 64;
const short numNZBits = numPreBits + numDataBits;

typedef struct {
	short Platform_ID;
	short AlgVer_Major;			// Read Only
	short AlgVer_Minor;			// Read Only
	short MANF_ID;
	short reserved1;				//platformID
	short reserved2;				
	short reserved3;				
	short reserved4;				//detect checksum
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
} SpinNR_Detect_data;

//#define numNZTarget 4
typedef	struct {
		float sng50[11];
		float sng80[11];
		short MPO;
		short ResGain;
} SpinNR_Target;


#ifndef NUMFR
#define NUMFR
const short numFR = 65;
#endif

typedef struct {
	float element[numFR];
} SpinNR_Response;


typedef struct {
	short element[numFR];
} SpinNR_Response_Int;


//num_TCtable
//#ifndef NUMTCtable
//#define NUMTCtable
//const short numTCtable = 17;
//#endif

//typedef struct {
//	short element[numTCtable];
//} TC_Table;
		
// read/write commands
	enum nzCommand {
		cmdREAD =		0,
		cmdLOAD =		1,
		cmdLOCK =		2,
		cmdAUDIO_ON =	3,
		cmdDETECT	=	4,
		cmdGetStatus =  6,
		cmdResetStatus = 7,
		cmdConnected = 8,
		cmdTestTone = 9,
		cmdGetEEPROMData = 10,
		cmdMute = 15,
	};

// number_of_programs
	const short nbrprog1Program =	0;
	const short nbrprog2Program =	1;
	const short nbrprog3Program =	2;
	const short nbrprog4Program =	3;
	const short nbrprog5Program =	4;

// program_switch_mode
//	const short swmodeMomentary =	0;
//	const short swmodeStatic =		1;

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
		typeCF2 =			4,
		typeEMiniTec =		5
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
	};

#endif
	
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

short 	__stdcall Initialize();
short	__stdcall Close();
short 	__stdcall Read(short page);
short 	__stdcall Load(short page);
short 	__stdcall Lock();
short 	__stdcall Detect(SpinNR_Detect_data *detect);
short 	__stdcall Connected();
short 	__stdcall Audio_on(short active_program);
short 	__stdcall Get_last_interface_error();
short	__stdcall Get_params(short version, SpinNR_Params *params);
short	__stdcall Set_params(short version, SpinNR_Params *params);
short	__stdcall Get_config(short version, SpinNR_Config *config);
short	__stdcall Set_config(short version, SpinNR_Config *config);
short	__stdcall Get_active_program(short *program);
short	__stdcall Set_active_program(short program);
short	__stdcall SetProgram(short program);
short	__stdcall Set_RL_channel(short channel);
short	__stdcall Get_RL_channel(short *channel);
short	__stdcall Set_interface_type(short type);
short	__stdcall Get_interface_type(short *type);
short	__stdcall Get_FR_array(short input_level, SpinNR_Response *response);
short 	__stdcall Set_mic_response(SpinNR_Response *mic_array);
short 	__stdcall Set_mic_response_Int(SpinNR_Response_Int *mic_array);
short 	__stdcall Set_rec_response(SpinNR_Response *rec_array);
short 	__stdcall Set_rec_response_Int(SpinNR_Response_Int *rec_array);
short	__stdcall AutoFit(short Datversion, short ManID, SpinNR_Target *target_params);
short	__stdcall SetToTest(short Datversion, short ManID);
short	__stdcall Set_platform_id(short ID);
short	__stdcall Get_IntFR_array(short input_level, SpinNR_Response_Int *response);
short	__stdcall Mute(void);
short	__stdcall ResetBootStatus(void);
short	__stdcall TestTone(short numBeeps);
short	__stdcall GetEEPROMData(void);
short 	__stdcall Set_VC_Mode(short mode);

void    fill_platform_data(short Platform_ID);
void	init_variables();