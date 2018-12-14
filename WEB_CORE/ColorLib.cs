using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
namespace TLKJ.WebSys
{
    /// <summary>
    /// ColorLib 的摘要说明
    /// </summary>
    public class ColorLib
    {
        private static List<String> Colors = new List<string>();
        public static List<String> getColorList()
        {
            if (Colors.Count == 0)
            {
                InitConfig();
            }
            return Colors;
        }
        public static string GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  为了在白色背景上显示，尽量生成深色
            int iRed = RandomNum_First.Next(256);
            int iGreen = RandomNum_Sencond.Next(256);
            int iBlue = (iRed + iGreen > 400) ? 0 : 400 - iRed - iGreen;
            iBlue = (iBlue > 255) ? 255 : iBlue;

            return "'#" + Color.FromArgb(iRed, iGreen, iBlue).Name + "'";
        }
        public static void InitConfig()
        {
            Colors.Add("'#3f1fa7'");
            Colors.Add("'#155ea1'");
            Colors.Add("'#a4c2f4'");
            Colors.Add("'#20450c'");
            Colors.Add("'#4b2e2e'");
            Colors.Add("'#0b49ac'");
            Colors.Add("'#660000'");
            Colors.Add("'#40639c'");
            Colors.Add("'#4a6a70'");
            Colors.Add("'#70806a'");
            Colors.Add("'#bb9c3b'");
            Colors.Add("'#d69653'");
            Colors.Add("'#e18e8e'");
            Colors.Add("'#a18dd6'");
            Colors.Add("'#e3c28f'");
            Colors.Add("'#99ecec'");
            Colors.Add("'#e59b9b'");
            Colors.Add("'#695353'");
        }
    }
}