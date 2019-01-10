using System;
using System.Drawing;

namespace MM.Helper.Base
{
    /// <summary>
    /// 颜色帮助类
    /// </summary>
    public class Colour
    {
        /// <summary>
        /// 随机色
        /// </summary>
        /// <param name="colour">色系 亮色/暗色（light/dark） 红色/绿色/蓝色（red/green/blue）</param>
        /// <returns>返回颜色值</returns>
        public string Rand(string colour = null)
        {
            if (string.IsNullOrEmpty(colour))
            {
                return colour;
            }
            Random _random = new Random(DateTime.Now.Millisecond);
            int Rmin = 0;
            int Gmin = 0;
            int Bmin = 0;
            int Rmax = 255;
            int Gmax = 255;
            int Bmax = 255;
            if (colour != null)
            {
                switch (colour.ToLower())
                {
                    case "light":
                        Rmin = 128;
                        Gmin = 128;
                        Bmin = 128;
                        break;
                    case "dark":
                        Rmax = 128;
                        Gmax = 128;
                        Bmax = 128;
                        break;
                    case "red":
                        Rmin = _random.Next(30, 255);
                        Gmax = Rmin - 30;
                        Bmax = Rmin - 30;
                        break;
                    case "green":
                        Gmin = _random.Next(30, 255);
                        Rmax = Gmin - 30;
                        Bmax = Gmin - 30;
                        break;
                    case "blue":
                        Bmin = _random.Next(30, 255);
                        Rmax = Bmin - 30;
                        Gmax = Bmin - 30;
                        break;
                    case "lightred":
                        Rmin = _random.Next(158, 255);
                        Gmax = Rmin - 30;
                        Bmax = Rmin - 30;
                        break;
                    case "lightgreen":
                        Gmin = _random.Next(158, 255);
                        Rmax = Gmin - 30;
                        Bmax = Gmin - 30;
                        break;
                    case "lightblue":
                        Bmin = _random.Next(158, 255);
                        Rmax = Bmin - 30;
                        Gmax = Bmin - 30;
                        break;
                    case "darkred":
                        Rmin = _random.Next(30, 128);
                        Gmax = Rmin - 30;
                        Bmax = Rmin - 30;
                        break;
                    case "darkgreen":
                        Gmin = _random.Next(30, 128);
                        Rmax = Gmin - 30;
                        Bmax = Gmin - 30;
                        break;
                    case "darkblue":
                        Bmin = _random.Next(30, 128);
                        Rmax = Bmin - 30;
                        Gmax = Bmin - 30;
                        break;
                }
            }
            int R = _random.Next(Rmin, Rmax);
            int G = _random.Next(Gmin, Gmax);
            int B = _random.Next(Bmin, Bmax);
            return ToHx16(R, G, B);
        }

        /// <summary>
        /// RGB转16进制色值
        /// </summary>
        /// <param name="Red">红色值，范围0-255</param>
        /// <param name="Green">绿色值，范围0-255</param>
        /// <param name="Blue">蓝色值，范围0-255</param>
        /// <returns>返回16进制色值</returns>
        public string ToHx16(int Red, int Green, int Blue)
        {
            Color color = Color.FromArgb(Red, Green, Blue);
            if (color.IsEmpty)
            {
                return "#000000";
            }
            string R = Convert.ToString(color.R, 16);
            if (R == "0")
            {
                R = "00";
            }
            string G = Convert.ToString(color.G, 16);
            if (G == "0")
            {
                G = "00";
            }
            string B = Convert.ToString(color.B, 16);
            if (B == "0")
            {
                B = "00";
            }
            string HexColor = "#" + R + G + B;
            return HexColor.ToUpper();
        }

        /// <summary>
        /// 16进制颜色值转RBG
        /// </summary>
        /// <param name="value">16进制颜色值</param>
        /// <returns>返回RGB模型</returns>
        public RGB ToRGB(string value) {
            int r = Convert.ToInt32("0x" + value.Substring(1, 2), 16);
            int g = Convert.ToInt32("0x" + value.Substring(3, 2), 16);
            int b = Convert.ToInt32("0x" + value.Substring(5, 2), 16);

            return new RGB() { B = b, G = g, R = r };
        }
    }

    /// <summary>
    /// RGB模型
    /// </summary>
    public class RGB {
        /// <summary>
        /// 红色值
        /// </summary>
        public int R { get; set; }

        /// <summary>
        /// 绿色值
        /// </summary>
        public int G { get; set; }

        /// <summary>
        /// 蓝色值
        /// </summary>
        public int B { get; set; }
    }
}
