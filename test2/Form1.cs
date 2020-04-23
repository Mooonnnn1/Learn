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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            Render();

        }

        Texture texture = new Texture();

        private void btnTexture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            texture.Bmp = new Bitmap(fd.FileName);

            /*AVIWriter aviWriter = new AVIWriter();

            //ps:avi中所有图像皆不能小于width及height
            Bitmap avi_frame = aviWriter.Create("mytest.avi", 24, 540, 667);
            
            bmpGirl.RotateFlip(RotateFlipType.Rotate180FlipX);


            //载入图像
            aviWriter.LoadFrame(bmpGirl);
            aviWriter.AddFrame();

            //释放资源
            aviWriter.Close();
            avi_frame.Dispose();

            MessageBox.Show("转换完毕");*/

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Render();
        }

        public void Render()
        {
            //观察点
            Point3D eye = new Point3D(0, 0, 4);

            World world = new World();

            world.Bulid();

            //环境光
            SColor Ia = new SColor(1, 1, 1);


            Light light = new Light();
            light.Lightcolor = new SColor(1, 1, 1);//光源亮度
            light.Position = new Point3D(3, 2, 2);//光源


            int hRes = 800;
            int vRes = 400;

            double hOffset = 4.0 / hRes;
            double vOffset = 2.0 / vRes;

            Bitmap bmp = new Bitmap(hRes, vRes);

            for (int i = 0; i < hRes; i++)
            {
                for (int j = 0; j < vRes; j++)
                {

                    //成像平面上的每个点
                    Point3D p = new Point3D(-2 + i * hOffset, 1 - j * vOffset, 2);
                    Vector3D dir = p - eye;

                    Ray primaryRay = new Ray(eye, dir);

                    ShadeRec sr;//击中点的记录

                    //求最近的交点信息
                    sr = world.HitAll(primaryRay);



                    if (sr != null)
                    {
                        

                        //交点到光源
                        Vector3D rayLight = light.Position - sr.Hitpoint;
                        rayLight.Normalize();

                        Ray shadowRay = new Ray(sr.Hitpoint, rayLight);

                        if (!world.ShadowHitAll(shadowRay))
                        {
                            //bool isHit = sphere.Hit(primaryRay,sr);

                            Vector3D L = light.Position - sr.Hitpoint;//入射光
                            L.Normalize();

                            //计算R：反射单位矢量
                            Vector3D R = 2.0 * (sr.Normal * L) * sr.Normal - L;
                            R.Normalize();
                            Vector3D V = eye - sr.Hitpoint;
                            V.Normalize();

                            double LN = L * sr.Normal;
                            double VR = V * R;
                            if (LN <= 0)
                            {
                                LN = 0;
                            }
                            if (VR <= 0)
                            {
                                VR = 0;
                            }

                            //环境光+Lambert漫反射+Phong镜面反射
                            //SColor Idiffuse = Ia * ka + Id * Kd * LN;
                            //SColor Idiffuse = sphere.Mat.Matcolor * ka + Id * sphere.Mat.Kd * LN + Id * sphere.Mat.Ks * Math.Pow(VR, sphere.Mat.Ns);

                            SColor Idiffuse = Ia * sr.HitObjMat.Ka + light.Lightcolor * sr.HitObjMat.Kd * LN + light.Lightcolor * sr.HitObjMat.Ks * Math.Pow(VR, sr.HitObjMat.Ns);

                            SColor color = Idiffuse * sr.HitObjMat.Matcolor;

                            //bmp.SetPixel(i, j, Idiffuse.GetRGB255Color());

                            double theta = Math.PI * trackBar1.Value / 180.0;
                            double x = sr.Hitpoint.X * Math.Cos(theta) + sr.Hitpoint.Z * Math.Sin(theta);
                            double z = -sr.Hitpoint.X * Math.Sin(theta) + sr.Hitpoint.Z * Math.Cos(theta);

                            if (sr.HitObjMat.IsTexture)
                            {

                                sr.Hitpoint.X = x;
                                sr.Hitpoint.Z = z;
                                //纹理颜色
                                Color textureColor = texture.getColor(sr);

                                //光照颜色
                                Color lightColor = color.GetRGB255Color();

                                //融合权重
                                double t = 0.7;

                                //融合
                                Color blendColor = Color.FromArgb(
                                    (int)(textureColor.R * t + lightColor.R * (1 - t)),
                                    (int)(textureColor.G * t + lightColor.G * (1 - t)),
                                    (int)(textureColor.B * t + lightColor.B * (1 - t))
                                    );
                                /*Color blendColor = Color.FromArgb(
                                    (int)(textureColor.R * (lightColor.R /255 ),
                                    (int)(textureColor.G * (lightColor.G /255 )),
                                    (int)(textureColor.B * (lightColor.B /255 ))
                                    );*/

                                bmp.SetPixel(i, j, blendColor);
                            }

                            else
                            {
                                bmp.SetPixel(i, j, color.GetRGB255Color());
                            }

                        }
                        else
                        {
                            bmp.SetPixel(i, j, Color.Black);
                        }
                    }
                    else
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(81, 74, 88));
                    }

                }
            }

            pic1.BackgroundImage = bmp;
        }
    }
}
