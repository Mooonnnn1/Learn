using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class Texture
    {
        //用作纹理的图片
        Bitmap bmp = new Bitmap(100, 100);

        //纹理分辨率
        int hres = 100;
        int vres = 100;

        //设置用作纹理的图片
        public Bitmap Bmp
        {
            get
            {
                return bmp;
            }
            set
            {
                bmp = value;
                hres = bmp.Width;
                vres = bmp.Height;
            }
        }

        //依据击中点三维坐标，得到图片坐标
        public void getTextCoordinate(Point3D pHit,out int row,out int column)
        {
            double theta = Math.Acos(pHit.Y);
            double phi = Math.Atan2(pHit.X, pHit.Z);

            if (phi < 0)
            {
                phi += 2.0 * Math.PI;
            }

            double u = phi / (2.0 * Math.PI);
            double v = 1.0 - theta / Math.PI;

            column = (int)((hres - 1) * u);
            row = (int)((vres - 1) * v);
        }

        //得到贴图上的具体颜色值
        public Color getColor(ShadeRec sr)
        {
            int row;
            int column;

            getTextCoordinate(sr.Hitpoint, out row, out column);

            return bmp.GetPixel(column, vres - row - 1);
        }
        
    }
}
