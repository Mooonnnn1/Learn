using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public SColor Render (Ray r, int depth)
        {
            //起始光线与场景中的所有物体求交
            ShadeRec sr = world.HitAll(r);
            Ray scattered = new Ray();
            
            if (sr != null)
            {
                //物体
                if (sr.Ishit&&sr.Hitobjglomaterial!=null)
                {
                    if(depth<50&&sr.Hitobjglomaterial.scatter(r,sr,out scattered))
                    {
                        return sr.Hitobjglomaterial.Attenuation * Render(scattered, depth + 1);
                    }
                    else
                    {
                        return new SColor(0, 0, 0);
                    }
                }
                else
                {
                    return new SColor(0, 0, 0);
                }
            }
            else
            {
                r.Direction.Normalize();
                double t = 0.5 * (r.Direction.Y + 1.0);

                return (1.0 - t) * new SColor(1.0, 1.0, 1.0)
                    + t * new SColor(0.5, 0.7, 1.0);
            }
        }

        private double random()
        {
            var seed = Guid.NewGuid().GetHashCode();
            Random r = new Random(seed);
            int i = r.Next(0, 100000);
            return (double)i / 100000;
        }

        Vector3D random_in_unit_sphere()
        {
            Vector3D p;
            Random r = new Random();
            do
            {
                Vector3D rndV = new Vector3D(random(),
                  random(),
                  random());

                Vector3D unitV = new Vector3D(1, 1, 1);
                p = 2.0 * rndV - unitV;

            } while (p.SquaredMagnitude() >= 1.0);

            return p;
        }


        /*public SColor Render(Ray r)
        {
            //起始光线与场景中的所有物体求交
            ShadeRec sr = world.HitAll(r);

            if (sr != null)
            {
                //物体
                if (sr.Ishit)
                {
                    //已击中点为中心，半球上面生成一个随机的光线
                    Point3D target = sr.Hitpoint + sr.Normal + random_in_unit_sphere();
                    Vector3D dir = target - sr.Hitpoint;
                    dir.Normalize();
                    Ray rndRay = new Ray(sr.Hitpoint, dir);

                    //递归的追踪该随机光线
                    return 0.5 * Render(rndRay);
                }
                else
                {
                    r.Direction.Normalize();
                    double t = 0.5 * (r.Direction.Y + 1.0);

                    return (1.0 - t) * new SColor(1.0, 1.0, 1.0)
                        + t * new SColor(0.5, 0.7, 1.0);
                }
            }
            else
            {
                r.Direction.Normalize();
                double t = 0.5 * (r.Direction.Y + 1.0);

                return (1.0 - t) * new SColor(1.0, 1.0, 1.0)
                    + t * new SColor(0.5, 0.7, 1.0);
            }
            
            
        }*/

        

        World world;
        private void btnTest_Click(object sender, EventArgs e)
        {
            //观察点位置
            Point3D eye = new Point3D(0, 0, 0);

            //用于做显示的bmp---
            Bitmap bmp = new Bitmap(200, 100);


            world = new World();

            Random r = new Random();

            //小球
            Sphere sphere = new Sphere(new Point3D(0, 0, -1), 0.5);//球体位置
            sphere.GloMaterial = new Lambert(new SColor(0.8, 0.3, 0.3));
            world.AddGeoObj(sphere);

            //小球
            Sphere sphere2 = new Sphere(new Point3D(1, 0, -1), 0.5);//球体位置
            sphere2.GloMaterial = new Metal(new SColor(0.8, 0.6, 0.2),0.5);
            world.AddGeoObj(sphere2);

            //小球
            Sphere sphere3 = new Sphere(new Point3D(-1, 0, -1), 0.5);//球体位置
            sphere3.GloMaterial = new Dielectrics(1.5);
            //sphere3.GloMaterial = new Metal(new SColor(0.8, 0.8, 0.8),0.5);
            world.AddGeoObj(sphere3);


            //底部大球
            Sphere sphere4 = new Sphere(new Point3D(0, -100.5, -1), 100);//球体位置
            sphere4.GloMaterial = new Lambert(new SColor(0.8, 0.8, 0.0));
            world.AddGeoObj(sphere4);

            double step = 0.02;

            //采样点数量
            int sp = 100;

            Point3D p;
            Vector3D dir;
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    //颜色
                    SColor clr = new SColor(0, 0, 0);

                    //随机采样
                    for (int s = 0; s < sp; s++)
                    {
                        //成像平面上的每个点的位置
                        p = new Point3D(-2 + step * (i + random()),
                           1 - step * (j + random()),
                           -1);

                        //起始光线的方向
                        dir = p - eye;
                        dir.Normalize();
                        Ray primaryRay = new Ray(eye, dir);

                        int depth = 0;

                        //渲染。。。
                        clr += Render(primaryRay,depth);
                    }
                    //
                    clr *= 1.0 / sp;

                    clr = new SColor(Math.Sqrt(clr.R),
                        Math.Sqrt(clr.G),
                        Math.Sqrt(clr.B));
                    //渲染到bmp
                    bmp.SetPixel(i, j, clr.GetRGB255Color());
                }
            }



            picRt.BackgroundImage = bmp;
        }
    }
}

