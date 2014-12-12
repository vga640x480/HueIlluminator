using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HueIlluminator.Misc
{
    class RainbowColor
    {

        public struct RGB
        {
            public double R
            {
                get;
                set;
            }
            public double G
            {
                get;
                set;
            }
            public double B
            {
                get;
                set;
            }
            public string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendFormat("RGB = (R={0},G={1},B={2})", R, G, B);
                return sb.ToString();
            }
        };

        static readonly Dictionary<int, RGB> rgbTable = new Dictionary<int, RGB>()
        {
            { 380, new RGB() { R=0.06076, G=0.00000, B=0.11058 }},
            { 390, new RGB() { R=0.08700 ,G=0.00000 ,B=0.16790 }},
            { 400, new RGB() { R=0.13772 ,G=0.00000 ,B=0.26354 }},
            { 410, new RGB() { R=0.20707 ,G=0.00000 ,B=0.39852 }},
            { 420, new RGB() { R=0.31129 ,G=0.00000 ,B=0.60684 }},
            { 430, new RGB() { R=0.39930 ,G=0.00000 ,B=0.80505 }},
            { 440, new RGB() { R=0.40542 ,G=0.00000 ,B=0.87684 }},
            { 450, new RGB() { R=0.34444 ,G=0.00000 ,B=0.88080 }},
            { 460, new RGB() { R=0.11139 ,G=0.00000 ,B=0.86037 }},
            { 470, new RGB() { R=0.00000 ,G=0.15233 ,B=0.77928 }},
            { 480, new RGB() { R=0.00000 ,G=0.38550 ,B=0.65217 }},
            { 490, new RGB() { R=0.00000 ,G=0.49412 ,B=0.51919 }},
            { 500, new RGB() { R=0.00000 ,G=0.59271 ,B=0.40008 }},
            { 510, new RGB() { R=0.00000 ,G=0.69549 ,B=0.25749 }},
            { 520, new RGB() { R=0.00000 ,G=0.77773 ,B=0.00000 }},
            { 530, new RGB() { R=0.00000 ,G=0.81692 ,B=0.00000 }},
            { 540, new RGB() { R=0.00000 ,G=0.82625 ,B=0.00000 }},
            { 550, new RGB() { R=0.00000 ,G=0.81204 ,B=0.00000 }},
            { 560, new RGB() { R=0.47369 ,G=0.77626 ,B=0.00000 }},
            { 570, new RGB() { R=0.70174 ,G=0.71523 ,B=0.00000 }},
            { 580, new RGB() { R=0.84922 ,G=0.62468 ,B=0.00000 }},
            { 590, new RGB() { R=0.94726 ,G=0.49713 ,B=0.00000 }},
            { 600, new RGB() { R=0.99803 ,G=0.31072 ,B=0.00000 }},
            { 610, new RGB() { R=1.00000 ,G=0.00000 ,B=0.00000 }},
            { 620, new RGB() { R=0.95520 ,G=0.00000 ,B=0.00000 }},
	        { 630, new RGB() { R=0.86620 ,G=0.00000 ,B=0.00000 }},
	        { 640, new RGB() { R=0.76170 ,G=0.00000 ,B=0.00000 }},
	        { 650, new RGB() { R=0.64495 ,G=0.00000 ,B=0.00000 }},
	        { 660, new RGB() { R=0.52857 ,G=0.00000 ,B=0.00000 }},
	        { 670, new RGB() { R=0.41817 ,G=0.00000 ,B=0.00000 }},
	        { 680, new RGB() { R=0.33202 ,G=0.00000 ,B=0.00000 }},
            { 690, new RGB() { R=0.25409 ,G=0.00000 ,B=0.00000 }},
            { 700, new RGB() { R=0.19695 ,G=0.00000 ,B=0.00000 }},
            { 710, new RGB() { R=0.15326 ,G=0.00000 ,B=0.00000 }},
            { 720, new RGB() { R=0.11902 ,G=0.00000 ,B=0.00000 }},
            { 730, new RGB() { R=0.09063 ,G=0.00000 ,B=0.00000 }},
            { 740, new RGB() { R=0.06898 ,G=0.00000 ,B=0.00000 }},
            { 750, new RGB() { R=0.05150 ,G=0.00000 ,B=0.00000 }},
            { 760, new RGB() { R=0.04264 ,G=0.00000 ,B=0.00000 }},
            { 770, new RGB() { R=0.03666 ,G=0.00000 ,B=0.00794 }},
            { 780, new RGB() { R=0.00000 ,G=0.00000 ,B=0.00000 }},
        };

        static readonly int wlenStep = 10;
        static readonly int wlenMin = 380;
        static readonly int wlenMax = 780;

        // 引数はnm単位の波長で380≦λ≦780,戻り値はR,G,Bそれぞれ0.0≦v≦1.0の浮動少数値です。 
        public static RGB wlen2rgb(int wlen)
        {
            wlen = Math.Max(wlenMin, wlen);
            wlen = Math.Min(wlenMax, wlen);
            Debug.WriteLine("wlen = {0}", wlen);
            int widx = wlen / wlenStep * wlenStep;
            int wlenSub = wlen % wlenStep;
            RGB ret = new RGB() { R = rgbTable[widx].R, G = rgbTable[widx].G, B = rgbTable[widx].B };
            if (wlenSub > 0)
            {
                ret.R += (rgbTable[widx + wlenStep].R - rgbTable[widx].R) * wlenSub / wlenStep;
                ret.G += (rgbTable[widx + wlenStep].G - rgbTable[widx].G) * wlenSub / wlenStep;
                ret.B += (rgbTable[widx + wlenStep].B - rgbTable[widx].B) * wlenSub / wlenStep;
            }
            Debug.WriteLine("ret = {0}", ret.ToString());
            return ret;
        }
    }
}
