using System;
using System.Globalization;
using System.Text;

namespace DotNetCommonTools
{
    /// <summary>
    /// <para>Provides RGB, HEX and HSL conversion functions.</para>
    /// </summary>
    public class ColorHelper
    {
        /// <summary>
        /// Given HEX string starts with #. Returns a Color (RGB struct) in range of 0-255.
        /// </summary>
        /// <param name="hexString">HEX string</param>
        /// <returns></returns>
        public static ColorRGB HEX2RGB(string hexString)
        {
            ColorRGB result;
            if (hexString.IndexOf('#') != -1)
                hexString = hexString.Replace("#", "");
            if (int.TryParse(hexString, out int k) == false)
                throw new ArgumentException("Invalid HEX value");

            if (hexString.Length == 3)
            {
                char[] charArray = hexString.ToCharArray();
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < charArray.Length; ++i)
                {
                    stringBuilder.Append(charArray[i]);
                    stringBuilder.Append(charArray[i]);
                }
                hexString = stringBuilder.ToString();
            }
            else if (hexString.Length != 6)
                throw new ArgumentException("Invalid HEX value");
            result.r = byte.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            result.g = byte.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            result.b = byte.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            return result;
        }

        /// <summary>
        /// Given a Color (RGB Struct) in range of 0-255. Return a HEX string starts with #.
        /// </summary>
        /// <param name="rgb">The <see cref="T:DotNetCommonTools.ColorHelper.ColorRGB" /> to convert.</param>
        /// <returns></returns>
        public static string RGB2HEX(ColorRGB rgb)
        {
            string rs = DecimalToHexadecimal(rgb.r);
            string gs = DecimalToHexadecimal(rgb.g);
            string bs = DecimalToHexadecimal(rgb.b);

            return '#' + rs + gs + bs;
        }

        /// <summary>
        /// Given H,S,L in range of 0-1. Returns a Color (RGB struct) in range of 0-255.
        /// </summary>
        /// <param name="h">Hue</param>
        /// <param name="sl">Saturation</param>
        /// <param name="l">Luminance</param>
        /// <returns></returns>
        /// 
        public static ColorRGB HSL2RGB(double h, double sl, double l)
        {
            double num = l;
            double num2 = l;
            double num3 = l;
            double num4 = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            bool flag = num4 > 0.0;
            if (flag)
            {
                double num5 = l + l - num4;
                double num6 = (num4 - num5) / num4;
                h *= 6.0;
                int num7 = (int)h;
                double num8 = h - (double)num7;
                double num9 = num4 * num6 * num8;
                double num10 = num5 + num9;
                double num11 = num4 - num9;
                switch (num7)
                {
                    case 0:
                        num = num4;
                        num2 = num10;
                        num3 = num5;
                        break;
                    case 1:
                        num = num11;
                        num2 = num4;
                        num3 = num5;
                        break;
                    case 2:
                        num = num5;
                        num2 = num4;
                        num3 = num10;
                        break;
                    case 3:
                        num = num5;
                        num2 = num11;
                        num3 = num4;
                        break;
                    case 4:
                        num = num10;
                        num2 = num5;
                        num3 = num4;
                        break;
                    case 5:
                        num = num4;
                        num2 = num5;
                        num3 = num11;
                        break;
                }
            }
            ColorRGB result;
            result.r = Convert.ToByte(num * 255.0);
            result.g = Convert.ToByte(num2 * 255.0);
            result.b = Convert.ToByte(num3 * 255.0);
            return result;
        }

        /// <summary>
        /// Given a Color (RGB Struct) in range of 0-255. Return H,S,L in range of 0-1.
        /// </summary>
        /// <param name="rgb">The <see cref="T:DotNetCommonTools.ColorHelper.ColorRGB" /> to convert.</param>
        /// <param name="h">Hue</param>
        /// <param name="s">Saturation</param>
        /// <param name="l">Luminance</param>
        // Token: 0x0600000E RID: 14 RVA: 0x00002404 File Offset: 0x00000604
        public static void RGB2HSL(ColorRGB rgb, out double h, out double s, out double l)
        {
            double num = rgb.r / 255.0;
            double num2 = rgb.g / 255.0;
            double num3 = rgb.b / 255.0;
            h = 0.0;
            s = 0.0;
            l = 0.0;
            double num4 = Math.Max(num, num2);
            num4 = Math.Max(num4, num3);
            double num5 = Math.Min(num, num2);
            num5 = Math.Min(num5, num3);
            l = (num5 + num4) / 2.0;
            if (!(l <= 0.0))
            {
                double num6 = num4 - num5;
                s = num6;
                if (s > 0.0)
                {
                    s /= ((l <= 0.5) ? (num4 + num5) : (2.0 - num4 - num5));
                    double num7 = (num4 - num) / num6;
                    double num8 = (num4 - num2) / num6;
                    double num9 = (num4 - num3) / num6;
                    if (num == num4)
                        h = ((num2 == num5) ? (5.0 + num9) : (1.0 - num8));
                    else
                    {
                        if (num2 == num4)
                            h = ((num3 == num5) ? (1.0 + num7) : (3.0 - num9));
                        else
                            h = ((num == num5) ? (3.0 + num8) : (5.0 - num7));
                    }
                    h /= 6.0;
                }
            }
        }

        /// <summary>
        /// RGB color.
        /// </summary>
        public struct ColorRGB
        {
            public byte r;
            public byte g;
            public byte b;
            /// <summary>
            /// RGB color.
            /// </summary>
            /// <param name="r">Red</param>
            /// <param name="g">Green</param>
            /// <param name="b">Blue</param>
            public ColorRGB(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

        }

        private static string DecimalToHexadecimal(int dec)
        {
            if (dec <= 0)
                return "00";

            int hex = dec;
            string hexStr = string.Empty;

            while (dec > 0)
            {
                hex = dec % 16;

                if (hex < 10)
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
                else
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());

                dec /= 16;
            }

            return hexStr;
        }
    }

}
