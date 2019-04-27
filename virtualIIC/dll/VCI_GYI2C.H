//#include "GY7501_USBI2C.H"

#define DEV_GY7501		1//1ch-I2C
#define DEV_GY7512		2//2ch-I2C
#define DEV_GY7514		3//4ch-I2C
#define DEV_GY7518		4//8ch-I2C
#define DEV_GY7503		5//1ch-I2C
#define DEV_GY7506		6//1ch-I2C,module/
#define DEV_GY7601		7//1ch-I2C
#define DEV_GY7602		8//2ch-I2C
#define DEV_GY7604		9//4ch-I2C
#define DEV_GY7608		10//8ch-I2C

typedef struct GYI2C_DATA_INFO{
						BYTE SlaveAddr;//�豸������ַ��bit7-1��Ч
						BYTE Databuffer[520];//Data ���ĵ����ݣ�
						UINT WriteNum;//��ַ�����ݵ��ܸ���
						UINT ReadNum;//��Ҫ�������ݵĸ���
						BYTE IoSel;//1 ��ʾ��ѡ�񣬽�����/д
						BYTE IoData;//IO��״̬��bit3��0�ֱ��ʾ4��IO��
									//ֻ����IoSel��Ϊ1��λ��ͬ��λֵ��Ч
						UINT DlyMsRead;//I2C������ʱ��PC�������������ʱ����ms������������ݡ�
						BYTE Reserved[4];//Reserved ϵͳ������
						}GYI2C_DATA_INFO,*pGYI2C_DATA_INFO;





extern "C" 
{ 
	DWORD	__stdcall  GYI2C_Open(DWORD DeviceType,DWORD DeviceInd,DWORD Reserved);
	DWORD	__stdcall  GYI2C_Close(DWORD DeviceType,DWORD DeviceInd);
	DWORD	__stdcall GYI2C_SetMode(DWORD DeviceType,DWORD DeviceInd,BYTE ModeValue);
	DWORD	__stdcall GYI2C_GetMode(DWORD DeviceType,DWORD DeviceInd);
	DWORD	__stdcall  GYI2C_SetClk(DWORD DeviceType,DWORD DeviceInd,DWORD ClkValue);
	DWORD	__stdcall  GYI2C_GetClk(DWORD DeviceType,DWORD DeviceInd);
	DWORD	__stdcall  GYI2C_SetChannel(DWORD DeviceType,DWORD DeviceInd,BYTE ChannelValue);
	DWORD	__stdcall  GYI2C_GetChannel(DWORD DeviceType,DWORD DeviceInd);

	DWORD	__stdcall  GYI2C_Read(DWORD DeviceType,DWORD DeviceInd,pGYI2C_DATA_INFO  pDataInfo);
	DWORD	__stdcall  GYI2C_Write(DWORD DeviceType,DWORD DeviceInd,pGYI2C_DATA_INFO pDataInfo);

	DWORD	__stdcall  GYI2C_SetIO(DWORD DeviceType,DWORD DeviceInd,pGYI2C_DATA_INFO pDataInfo);
	DWORD	__stdcall  GYI2C_GetIO(DWORD DeviceType,DWORD DeviceInd,pGYI2C_DATA_INFO pDataInfo);

	DWORD	__stdcall  GYI2C_Start(DWORD DeviceType,DWORD DeviceInd);
	DWORD	__stdcall  GYI2C_WriteByte(DWORD DeviceType,DWORD DeviceInd,BYTE DataValue);
	DWORD	__stdcall  GYI2C_ReadByte(DWORD DeviceType,DWORD DeviceInd,BYTE AckValue);
	DWORD	__stdcall  GYI2C_Stop(DWORD DeviceType,DWORD DeviceInd);





}