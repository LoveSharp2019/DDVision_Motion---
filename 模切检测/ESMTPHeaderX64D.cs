using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Start
{
    public class ESMTPHeader
    {
        public const int PORTERROR = -99;
        public const int UKERROR = -100;
        public const int PARRERROR = -5;
        public const int Board_EMontion = 1;
        public const int Board_EMontionA = 2;
        public const int Board_EDIO = 16;


        public delegate void EVENTCALLBACK(System.IntPtr pszIP, uint Int_SerialNo, ushort Item_No, ushort Factor_No);

        [DllImport("ESMTP_X64D.dll", EntryPoint = "SetEventCallBack", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SetEventCallBack(int BoardType, EVENTCALLBACK param1);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "Connect", CallingConvention = CallingConvention.StdCall)]
        public static extern long Connect(int BoardType, System.IntPtr pszIP);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "Disconnect", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort Disconnect(int BoardType, long hESMTP);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "Get_DllVer", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort Get_DllVer();

        //返回设备的ip，以逗号间隔
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SearchDevice", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SearchDevice(string pszDevice);

        //返回设备的ip，以逗号间隔
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SearchDevice_1", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SearchDevice_1(ref int CardNO);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SetNewIP", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SetNewIP(int BoardType, long hESMTP, System.IntPtr pszIP, System.IntPtr pszMask, System.IntPtr pszGateway);

        /*
        获取卡名
        CardName：
        运动控制卡的卡名，从0~255，暂定如下：
            0	PCD4641A
            1	PCL6045BL
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Get_card_name", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Get_card_name(int BoardType, long hESMTP, ref ushort CardName);

        /*
        获取卡名
        HWVersion : 硬件版本信息
        SWVersion ：软件版本信息
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Get_card_Version", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Get_card_Version(int BoardType, long hESMTP, ref uint HWVersion, ref uint SWVersion);

        /*
       板硬件初始化
       */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_HWinitial", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_HWinitial(int BoardType, long hESMTP);

        /*
        hESMTP: 运动控制模块句柄.
        DI_Data:返回当前的传感器状态，32bit，16bit有效
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Read_d_input", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Read_d_input(int BoardType, long hESMTP, ref uint DI_Data);


        /*
        DLLAPI int SY_MC_Write_d_output(long long hESMTP,U32 DO_Data);
        hESMTP: 运动控制模块句柄.
        DO_Data:按位输出DO状态
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Write_d_output", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Write_d_output(int BoardType, long hESMTP, uint DO_Data);


        /*
        hESMTP: 运动控制模块句柄..
        *DO_Data:返回当前各输出端口状态
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Read_d_output", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Read_d_output(int BoardType, long hESMTP, ref uint DO_Data);

        /*
        设置单通道DO状态
        hESMTP:运动控制模块句柄..
        Ch_No:通道号
        DO_Data: 输出，0 或 1
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Write_d_Channel_output", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Write_d_Channel_output(int BoardType, long hESMTP, uint Ch_No, uint DO_Data);

        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_WritePort_d_output", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_WritePort_d_output(int BoardType, long hESMTP, uint ChannelNumber, uint ChannelOnOff);



        /*
        读取单通道DO状态
        hESMTP:运动控制模块句柄..
        Ch_No:通道号
        DO_Data: 当前输出状态
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Read_d_Channel_output", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Read_d_Channel_output(int BoardType, long hESMTP, uint Ch_No, ref uint DO_Data);


        /*
        读取单通道DI状态
        hESMTP:运动控制模块句柄..
        Ch_No:通道号
        DI_Data: 返回当前通道的传感器状态
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Read_d_Channel_input", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Read_d_Channel_input(int BoardType, long hESMTP, uint Ch_No, ref uint DI_Data);


        /*DIO专门函数
            U16 SY_MC_Set_FilterTime
            U16 SY_MC_Get_card_ManuInfo
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_FilterTime", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_FilterTime(int BoardType, long hESMTP, uint FilterTime);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Get_card_ManuInfo", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Get_card_ManuInfo(int BoardType, long hESMTP, ref ushort Manu_Year, System.IntPtr Manu_Mon, System.IntPtr Manu_Day, ref uint Manu_Sum);


        /*
        Value	含义
        模式有四种 ：
        0 单脉冲+方向方式：脉冲为负脉冲；[+]方向时，DIR =H 
        1 单脉冲+方向方式：脉冲为正脉冲；[+]方向时，DIR =L 
        2 双脉冲输出： [+]方向时，脉冲为负脉冲，Direction为电平信号 Direction+ = 1 且 Direction- = 0
                       [-]方向时，脉冲为电平信号，Pulse+ = 1 且 Pulse- = 0；Direction为负脉冲

        3 双脉冲输出：[+]方向时，脉冲为正脉冲， ；Direction为电平信号，Direction+ = 0 且 Direction- = 1
                      [-]方向时，脉冲为电平信号，Pulse+ = 0 且 Pulse- =1；Direction为正脉冲
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_pls_outmode", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_pls_outmode(int BoardType, long hESMTP, ushort AxisNo, ushort pls_outmode);


        /*
        hESMTP: 运动控制模块句柄.
        AxisNo	U16	轴参数0~3
        sd_enable	U16	Enable/disable SD减速信号
                Value	含义
                0	Disabled(default)
                1	Enabled
        sd_logic	U16	设置SD减速信号逻辑
                Value	含义
                0	Low active
                1	High active
        sd_latch	U16	设置锁存信号控制SD减速信号
                Value	含义
                0	No latch
                1	Latch
        sd_mode	U16	设置SD减速信号响应模式
                Value	含义
                0	Slow down only
                1	Slow down and stop
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_sd", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_sd(int BoardType, long hESMTP, ushort AxisNo, ushort sd_enable, ushort sd_logic, ushort sd_latch, ushort sd_mode);


        /*
        hESMTP: 运动控制模块句柄.
        AxisNo		U16	轴参数0~3
        el_logic	U16	设置限位信号逻辑
                    Value	含义
                    0	高（光耦导通）有效
                    1	低（光耦截止）有效
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_ELLogic", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_ELLogic(int BoardType, long hESMTP, ushort AxisNo, ushort el_logic);


        /*
        hESMTP: 运动控制模块句柄.
        AxisNo		U16	轴参数0~3
        org_logic	U16	设置原点信号逻辑
                    Value	含义
                    0	高（光耦导通）有效
                    1	低（光耦截止）有效
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_ORGLogic", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_ORGLogic(int BoardType, long hESMTP, ushort AxisNo, ushort org_logic);


        /*
        hESMTP: 运动控制模块句柄.
        AxisNo			U16	轴参数0~3
        Exchange_Status	U16	设置限位硬件信号互换，设定为1后，正负极限互换
                        Value	含义
                        0	正负限位不变
                        1	正负限位互换
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_Exchange_EL", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_Exchange_EL(int BoardType, long hESMTP, ushort AxisNo, ushort Exchange_Status);


        /*
        IO_status 定义:
        Bit	名称	描述
        0	RDY	RDY信号
        1	ALM	报警信号
        2	PEL	正限位信号，1表示正限位有效，0表示无效
        3	MEL	负限位信号，1表示负限位有效，0表示无效
        4	ORG	原点信号，1表示原点有效，0表示无效
        5	DIR	DIR，运动方向输出信号，0表示当前运动为正向，1表示当前运动为负方向
        6	EMG	EMG信号，1表示EMG输入为高，0表示输入为低；该信号低电平有效。
        7	-	-
        8	ERC	ERC输出信号
        9	EZ	Index信号，1表示EZ为高电平，0表示低电平
        10	-	-
        11	Latch	位置锁存信号
        12	PSD	正向减速信号
        13	INP	到位信号
        14	SVON	伺服使能输出信号
        15  MSD	负向减速信号
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_get_io_status", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_get_io_status(int BoardType, long hESMTP, ushort AxisNo, ref uint IO_status);


        /*
        hESMTP: 运动控制模块句柄.
        AxisNo	U16	轴参数0~3
        Direction	U8	运动方向
                Value	含义
                0	Positive(+) direction
                1	Negative(-) direction
        StrVel	U32	初始速度，单位为脉冲/秒（Pulse/S）
        MaxVel	U32	最大速度，单位为脉冲/秒（Pulse/S）
        Tacc	F32	加减速度时间，单位为秒(S)
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Home_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Home_move(int BoardType, long hESMTP, ushort AxisNo, byte Direction, uint StrVel, uint MaxVel, float Tacc);


        /*
         功能描述：
        配置原点开关有效电平、编码器index信号、回零模式；
        参数：
        hESMTP: 运动控制模块句柄.
        AxisNo 轴号
        Home_mode 回零模式, 范围: 0~3
        Org_logic 原点信号的有效电平； 0 – 低有效； 1 – 高有效；
        Ez_logic 编码器的index信号有效电平；0 – 低有效； 1 – 高有效；-
        Ez_count ORG有效后，需要计的index脉冲数目，范围:0 ~ 15
        Erc_out 回零结束后是否要自动输出ERC信号；0 – 禁止自动ERC输出； 1 – 使能自动ERC输出
        返回值：
        正常返回0；
        出现错误时返回非0值； 
        Ez_logic Ez_count Erc_out参数无效
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_set_home_config", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_set_home_config(int BoardType, long hESMTP, ushort AxisNo, ushort home_mode, ushort org_logic, ushort ez_logic, ushort ez_count, ushort erc_out);


        /*
        hESMTP: 运动控制模块句柄.
        AxisNo	U16	轴参数0~3
        Dir	U8	运动方向
                Value	含义
                0	Positive(+) direction
                1	Negative(-) direction
        StrVel	U32	初始速度，单位为脉冲/秒（Pulse/S）
        MaxVel	U32	最大速度，单位为脉冲/秒（Pulse/S）
        Tacc	F32	加减速度时间，单位为秒(S)
        ORGOffset	I32	ORGOffset 离开原点的位置
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_home_search", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_home_search(int BoardType, long hESMTP, ushort AxisNo, byte Dir, uint StrVel, uint MaxVel, float Tacc, int ORGOffset);


        /*
        运动指令操作
        hESMTP:		运动控制模块句柄.
        AxisNo		U16	轴参数0~3
        Direction	U8	运动方向
                    Value	含义
                    0	Positive(+) direction
                    1	Negative(-) direction
        StrVel		U32	初始速度，单位为脉冲/秒（Pulse/S）
        MaxVel		U32	最大速度，单位为脉冲/秒（Pulse/S）
        Tacc		F32	加减速度时间，单位为秒(S)
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_tv_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_tv_move(int BoardType, long hESMTP, short AxisNo, byte Direction, uint StrVel, uint MaxVel, float Tacc);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_sv_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_sv_move(int BoardType, long hESMTP, short AxisNo, byte Direction, uint StrVel, uint MaxVel, float Tacc);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_tr_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_tr_move(int BoardType, long hESMTP, short AxisNo, int Dist, uint StrVel, uint MaxVel, float Tacc);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_sr_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_sr_move(int BoardType, long hESMTP, short AxisNo, int Dist, uint StrVel, uint MaxVel, float Tacc);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_ta_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_ta_move(int BoardType, long hESMTP, short AxisNo, int Dist, uint StrVel, uint MaxVel, float Tacc);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_sa_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_sa_move(int BoardType, long hESMTP, short AxisNo, int Dist, uint StrVel, uint MaxVel, float Tacc);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Stop_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Stop_move(int BoardType, long hESMTP, ushort AxisNo);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Emg_stop", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Emg_stop(int BoardType, long hESMTP, ushort AxisNo);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_v_change", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_v_change(int BoardType, long hESMTP, short AxisNo, uint NewVel, float Time);


        /*
        名称	类型	描述
        hESMTP: 运动控制模块句柄.
        AxisNo	U16	轴参数0~3
        Action	U8	Active type
                Value	含义
                0	INT only
                1	Stop immediately
                2	Slow down stop
                3	Disable softlimit(默认值)
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_set_soft_limit", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_set_soft_limit(int BoardType, long hESMTP, short AxisNo, byte Action, int PLimit, int MLimit);


        /*
         运动状态返回值
        Value	含义
        0	Stop
        1	Wait STA
        2	Wait ERC finish
        3	Wait dir change
        4	Back lashing
        5	Wait PA/PB
        6	In FA motion
        7	In FL motion
        8	Acceleration
        9	In FH motion
        10	Deceleration
        11	Wait INP
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Motion_status", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Motion_status(int BoardType, long hESMTP, short AxisNo, ref ushort MoSt);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_P_change", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_P_change(int BoardType, long hESMTP, short AxisNo, int NewPos);


        /*
        hESMTP: 运动控制模块句柄.
        AxisNo	U16	轴参数0~3
        CompNo	I16	Comparator Number(1~3)
        CmpSrc	I16	位置比较源计数器
                Value	含义
                0	Command Counter
                1	Feedback Counter
                2	Error Counter
        CmpMethod	I16	 位置比较方法
                Value	含义
                0	No compare
                1	=Counter(Directionless)
                2	=Counter(+Dir)
                3	=Counter(-Dir)
                4	<Counter
                5	>Counter
        CmpAction	I16	位置比较响应
                Value	含义
                0	Flag，INT only
                1	Immediately Stop
                2	Slow down stop
                3	Don't use
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_set_comparator_mode", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_set_comparator_mode(int BoardType, long hESMTP, short AxisNo, short CompNo, short CmpSrc, short CmpMethod, short CmpAction);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_set_comparator_data", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_set_comparator_data(int BoardType, long hESMTP, short AxisNo, short CompNo, int Pos);

        //8、计数器操作
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_position", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_position(int BoardType, long hESMTP, short AxisNo, int Position);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Get_position", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Get_position(int BoardType, long hESMTP, short AxisNo, ref int Position);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_command", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_command(int BoardType, long hESMTP, short AxisNo, int command);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Get_command", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Get_command(int BoardType, long hESMTP, short AxisNo, ref int command);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_target_position", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_target_position(int BoardType, long hESMTP, short AxisNo, int Targ_Pos);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Get_target_position", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Get_target_position(int BoardType, long hESMTP, short AxisNo, ref int Targ_Pos);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Set_error_position", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Set_error_position(int BoardType, long hESMTP, short AxisNo, int Err_Pos);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Get_error_position", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Get_error_position(int BoardType, long hESMTP, short AxisNo, ref int Err_Pos);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_int_enable", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_int_enable(int BoardType, long hESMTP, ushort Enable);


        /*
        Interrupt Item Definition Table[Item_No]
        Item No.	描述
        0~7	Axis interrupts
        For PCD4641A Item is from 0 to 3(4~7 is reserved.)
        8	System interrupts
        9	DI-Rising edge interrupts
        10	DI-Falling edge interrupts

        Axis interrupt factors
        Factor No.	Define	描述
        0	IALM	Servo alarm signal turn ON
        1	IPEL	Positive end limit switch turn ON
        2	IMEL	Minus end limit switch turn ON
        3	IORG	Home switch turn ON
        4	IEZ	EZ passed signal turn ON
        5	IINP	In position
        6	IEMG	EMG signal turn ON
        7	Reserved	Reserved,always be 0
        8	ICSTP	Command stop
        9	IVM	In Maximum velocity
        10	IACC	In acceleration
        11	IDEC	In deceleration
        12	IMDN	Motion done
        13	IASTP	Abnormal stop
        14	Reserved	Reserved,always be 0
        15	ISPEL	In positive soft limit
        16	ISMEL	In negative soft limit
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_set_int_factor", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_set_int_factor(int BoardType, long hESMTP, ushort Item_No, ushort Factor_No, ushort Enable);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_get_int_factor", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_get_int_factor(int BoardType, long hESMTP, ushort Item_No, ushort Factor_No, ref ushort Enable);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_wait_single_int", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_wait_single_int(int BoardType, long hESMTP, ushort Int_No, ushort Time_Out);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_reset_int", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_reset_int(int BoardType, long hESMTP, ushort Int_No);

        /*
        Linear Interpolated Motion插补指令
        hESMTP: 运动控制模块句柄.
        moveMode   :  0--TR ; 1 --TA ; 2 --SR ; 3 --SA
        Axis_A	U16	: 线性插补主轴 。 0--x ；1--Y； 2--Z ；3--U
        Axis_B	U16	: 线性插补第二轴 。 0--x ；1--Y； 2--Z ；3--U
        Axis_C	U16	: 线性插补第三轴 。 0--x ；1--Y； 2--Z ；3--U
        Axis_D	U16	: 线性插补第四轴 。 0--x ；1--Y； 2--Z ；3--U
        ABCD轴号不能相同

        I32 Position_A : 线性插补主轴的位置
        I32 Position_B : 线性插补第二轴的位置
        I32 Position_C : 线性插补第三轴的位置
        I32 Position_D : 线性插补第四轴的位置
        StrVel	U32	主轴插补运动的初速度,单位pulse/s
        MaxVel	U32	主轴插补运动的最大速度,单位pulse/s
        Tacc	F32	主轴加速时间，单位秒(s)
        Tdec	F32	主轴减速时间，单位秒(s)
        */
        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_linear_move_2d", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_linear_move_2d(int BoardType, long hESMTP, ushort moveMode, ushort Axis_A, ushort Axis_B, int Position_A, int Position_B, uint StrVel, uint MaxVel, float Tacc, float Tdec);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_linear_move_3d", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_linear_move_3d(int BoardType, long hESMTP, ushort moveMode, ushort Axis_A, ushort Axis_B, ushort Axis_C, int Position_A, int Position_B, int Position_C, uint StrVel, uint MaxVel, float Tacc, float Tdec);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_linear_move_4d", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_linear_move_4d(int BoardType, long hESMTP, ushort moveMode, ushort Axis_A, ushort Axis_B, ushort Axis_C, ushort Axis_D, int Position_A, int Position_B, int Position_C, int Position_D, uint StrVel, uint MaxVel, float Tacc, float Tdec);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_Stop_linear_move", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_Stop_linear_move(int BoardType, long hESMTP, ushort Axis_A, ushort Axis_B, ushort Axis_C, ushort Axis_D);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_SET_EMG_CFG", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_SET_EMG_CFG(int BoardType, long hESMTP, int nEmg_Mode);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_GET_EMG_CFG", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_GET_EMG_CFG(int BoardType, long hESMTP, ref int nEmg_Mode);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_CFG_d_InputTrigger", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_CFG_d_InputTrigger(int BoardType, long hESMTP, byte PulseWidthA, byte PulseWidthB, byte PulseWidthC, byte PulseWidthD, byte ModeA, byte ModeB, byte ModeC, byte ModeD);


        [DllImport("ESMTP_X64D.dll", EntryPoint = "SY_MC_GET_d_InputTriggerStatus", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort SY_MC_GET_d_InputTriggerStatus(int BoardType, long hESMTP, ref uint TrigCounterA, ref uint TrigCounterB, ref uint TrigCounterC, ref uint TrigCounterD);
    }
}
