using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ShareMemNet
{
    public enum CmdRead
    {
        ELEM_X = 0,

        ELEM_Y = 2,

        ELEM_M = 4,

        ELEM_SM = 6,

        ELEM_S = 8,

        ELEM_TT = 10,

        ELEM_TV = 12,

        ELEM_CT = 14,

        ELEM_CV16 = 16,

        ELEM_CV32 = 18,

        ELEM_D = 20,

        ELEM_SD = 22,

        ELEM_R = 24,

        ELEM_RD = 38,

        ELEM_VD = 40,

    }

    public enum CmdWrite
    {
        ELEM_X = 1,

        ELEM_Y = 3,

        ELEM_M = 5,

        ELEM_SM = 7,

        ELEM_S = 9,

        ELEM_TT = 11,

        ELEM_TV = 13,

        ELEM_CT = 15,

        ELEM_CV16 = 17,

        ELEM_CV32 = 19,

        ELEM_D = 21,

        ELEM_SD = 23,

        ELEM_R = 25,

        ELEM_RD = 39,

        ELEM_VD = 41,
    }

    public class ShareMemProH
    {
        public const ushort RW_MAX_NYUM = 256;

        [DllImport("LibSMx64.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PH_Init();

        [DllImport("LibSMx64.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PH_Write(ushort cmd_type, ushort elem_start, ushort elem_num, short[] val);

        [DllImport("LibSMx64.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PH_Read(ushort cmd_type, ushort elem_start, ushort elem_num, short[] val);

        [DllImport("LibSMx64.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PH_Exit();

    }

}
