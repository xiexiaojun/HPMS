using HPMS.DB;
using HPMS.RightsControl;

namespace HPMS.Util
{
    /// <summary>
    /// 存放全局变量
    /// </summary>

    public class Gloabal
    {
        public static RightsWrapper GRightsWrapper;//权限控制类
        public static User GUser;                   //当前登录用户
        public static string freSpecFilePath = @"temp\\Freq Spec.txt";
        public static string timeSpecFilePath = @"temp\\Impedance Spec.txt";


    }
}
