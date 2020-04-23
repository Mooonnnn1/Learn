using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    public class SColor
    {
        private double _r;
        private double _g;
        private double _b;

        public double R
        {
            get
            {
                return _r;
            }
            set
            {
                _r = value;
                /*if(_r<0.0)
                {
                    _r = 0.0;
                }
                if(_r>1.0)
                {
                    _r = 1.0;
                }*/
            }
            
        }
        public double G
        {
            get
            {
                return _g;
            }
            set
            {
                _g = value;
                /*if (_g < 0.0)
                {
                    _g = 0.0;
                }
                if (_g > 1.0)
                {
                    _g = 1.0;
                }*/
            }

        }
        public double B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
                /*if (_b < 0.0)
                {
                    _b = 0.0;
                }
                if (_b > 1.0)
                {
                    _b = 1.0;
                }*/
            }

        }

        public Color GetRGB255Color()
        {
            if (R >= 1)
            {
                R = 0.99;
            }
            if (G >= 1)
            {
                G = 0.99;
            }
            if (B >= 1)
            {
                B = 0.99;
            }
            return Color.FromArgb((int)(R * 255), (int)(G * 255), (int)(B * 255));
        }

        public SColor()
        {
            this.R = 0.0;
            this.G = 0.0;
            this.B = 0.0;
        }

        public SColor(double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public static SColor operator*(SColor color,double d)
        {
            return new SColor(color.R * d, color.G * d, color.B * d);
        }

        public static SColor operator +(SColor color1, SColor color2)
        {
            return new SColor(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B);
        }

        public static SColor operator *(SColor color1, SColor color2)
        {
            return new SColor(color1.R * color2.R, color1.G * color2.G, color1.B * color2.B);
        }

        public static SColor operator *(double d , SColor color)
        {
            return new SColor(color.R * d, color.G * d, color.B * d);
        }

    }
}
