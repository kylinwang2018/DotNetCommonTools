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
            try
            {
                result.r = byte.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                result.g = byte.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                result.b = byte.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }catch(Exception e)
            {
                throw new ArgumentException("Invalid HEX value");
            }

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
            if (rs.Substring(0, 1).Equals(rs.Substring(1, 1)) &&
                gs.Substring(0, 1).Equals(gs.Substring(1, 1)) &&
                bs.Substring(0, 1).Equals(bs.Substring(1, 1))
                )
            {
                rs = rs.Substring(0, 1);
                bs = bs.Substring(0, 1);
                gs = gs.Substring(0, 1);
            }
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
            if (h > 1 || sl > 1 || l > 1)
                throw new ArgumentException("H,S,L should in range of 0-1");

            double v;
            double r, g, b;
            r = l;   // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            ColorRGB rgb = new ColorRGB();
            rgb.r = Convert.ToByte(r * 255.0f);
            rgb.g = Convert.ToByte(g * 255.0f);
            rgb.b = Convert.ToByte(b * 255.0f);
            return rgb;
        }


        /// <summary>
        /// Given a Color (RGB Struct) in range of 0-255. Return H,S,L in range of 0-1.
        /// </summary>
        /// <param name="rgb">The <see cref="T:DotNetCommonTools.ColorHelper.ColorRGB" /> to convert.</param>
        /// <param name="h">Hue</param>
        /// <param name="s">Saturation</param>
        /// <param name="l">Luminance</param>
        public static void RGB2HSL(ColorRGB rgb, out double h, out double s, out double l)
        {
            double num = rgb.r / 255.0;
            double num2 = rgb.g / 255.0;
            double num3 = rgb.b / 255.0;
            h = 0.0;
            s = 0.0;
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
            if (dec >= 255)
                return "FF";
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
