using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    public abstract class GlobalMaterial
    {
        //颜色
        private SColor attenuation;

        public GlobalMaterial()
        {

        }

        public GlobalMaterial(SColor a)
        {
            Attenuation = a;
        }
        
        public SColor Attenuation { get => attenuation; set => attenuation = value; }

        //散射特性---漫反射、镜面反射
        //已知：入射光线、击中点信息；返回：散射出去的光线
        public abstract bool scatter(Ray rayIn, ShadeRec sr, out Ray rayScatter);
    }

    //Lambert漫反射 全局材质
    public class Lambert : GlobalMaterial
    {
        public Lambert(SColor clr) : base(clr)
        {

        }

        //随机漫反射
        public override bool scatter(Ray rayIn, ShadeRec sr, out Ray rayScatter)
        {
            Point3D target = (sr.Hitpoint + sr.Normal) + Vector3D.RandomInUnitSphere();
            rayScatter=new Ray(sr.Hitpoint,target - sr.Hitpoint);

            return true;
        }
    }

    //镜面反射
    class Metal : GlobalMaterial
    {
        public Metal(SColor clr) : base(clr)
        {

        }

        //模糊参数
        double fuzz;

        public Metal(SColor clr,double f) : base(clr)
        {
            fuzz = f;
        }

        //镜面反射
        public override bool scatter(Ray rayIn, ShadeRec sr ,out Ray rayScatter)
        {
            //得到镜面反射光线
            Vector3D reflectedDir = Vector3D.GetReflactedDir(rayIn.Direction, sr.Normal);
            reflectedDir.Normalize();

            //生成反射光线
            //rayScatter = new Ray(sr.Hitpoint, reflectedDir + Vector3D.RandomInUnitSphere());非哑光
            rayScatter = new Ray(sr.Hitpoint, reflectedDir + fuzz * Vector3D.RandomInUnitSphere());

            rayScatter.Direction.Normalize();
            return (rayScatter.Direction * sr.Normal) > 0;
            
        }
    }

    //透明材质
    public class Dielectrics : GlobalMaterial
    {
        bool refract(Vector3D v, Vector3D n, double ni_over_nt, out Vector3D refracted)
        {
            v.Normalize();
            double dt = v * n;
            double discriminant = 1.0 - ni_over_nt * ni_over_nt * (1 - dt * dt);
            if (discriminant > 0)
            {
                refracted = ni_over_nt * (v - n * dt) - n * Math.Sqrt(discriminant);
                return true;
            }
            else
            {
                refracted = new Vector3D(0, 0, 0);
                return false;
            }
        }
        
        double ref_idx;

        public Dielectrics(double r) : base()
        {
            ref_idx = r;
        }

        /*public static double Schlick(double cosine, double ref_Idx)
        {
            double r0 = (1 - ref_Idx) / (1 + ref_Idx);
            r0 = r0 * r0;
            return r0 + (1 - r0) * Math.Pow((1 - cosine), 5);
        }*/

        private static double random()
        {
            var seed = Guid.NewGuid().GetHashCode();
            Random r = new Random(seed);
            double i = r.Next(0, 1000);
            return 1 / 100000.0;
        }

        public override bool scatter(Ray rayIn, ShadeRec sr, out Ray rayScatter)
        {
            Vector3D outward_normal;
            Vector3D reflected = Vector3D.GetReflactedDir(rayIn.Direction, sr.Normal);
            double ni_over_nt;
            Attenuation = new SColor(1.0, 1.0, 1.0);
            Vector3D refracted;
            double reflect_prob;
            //double cosine;
            if ((rayIn.Direction * sr.Normal) > 0)
            {
                outward_normal = -sr.Normal;
                ni_over_nt = ref_idx;
                //cosine = (ref_idx * rayIn.Direction * sr.Normal) / (rayIn.Direction.SquaredMagnitude());
            }
            else
            {
                outward_normal = sr.Normal;
                ni_over_nt = 1.0 / ref_idx;
                //cosine = -(ref_idx * rayIn.Direction * sr.Normal) / (rayIn.Direction.SquaredMagnitude());
            }
            /*if(refract(rayIn.Direction,outward_normal,ni_over_nt,out refracted))
            {
                reflect_prob = -Schlick(cosine, ref_idx);
            }
            else
            {
                reflect_prob = 1.0;
            }
            if (random() < reflect_prob)
            {
                rayScatter = new Ray(sr.Hitpoint, refracted);
            }
            else
            {
                rayScatter = new Ray(sr.Hitpoint, reflected);
            }
            return true;*/
            if (refract(rayIn.Direction, outward_normal, ni_over_nt, out refracted))
            {
                rayScatter = new Ray(sr.Hitpoint, refracted);
            }
            else
            {
                rayScatter = new Ray(sr.Hitpoint, reflected);
            }
            return true;
        }
    }
}
