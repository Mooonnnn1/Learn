using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class Sphere:GeometryObject
    {
        private Point3D _center;
        private double _radius;

        public const double kEpsilon = 1e-5;//定义一个极小数

        public double Radius { get => _radius; set => _radius = value; }
        internal Point3D Center { get => _center; set => _center = value; }

        public Sphere()
        {
            this.Center = new Point3D(0, 0, 0);
            this.Radius = 1.0;
        }

        public Sphere(Point3D _center,double _radius)
        {
            this.Center = _center;
            this.Radius = _radius;
        }

        public Vector3D GetNormalVector(Point3D p)
        {
            Vector3D normal = p - Center;
            normal.Normalize();
            return normal;
        }

        //球体与光线求交
        public override bool Hit(Ray ray,ShadeRec sr)
        {
            Vector3D oc = ray.Origin - Center;
            double r = Radius;

            double a = ray.Direction * ray.Direction;
            double b = 2 * (ray.Direction * oc);
            double c = oc * oc - r * r;
            double delta = b * b - 4.0 * a * c;
            //有交点
            if (delta > 0)
            {
                double t = (-b - Math.Sqrt(delta)) / (2 * a);
                if (t < 0.00001)
                {
                    t = (-b + Math.Sqrt(delta)) / (2 * a);
                }
                if (t > kEpsilon)
                {
                    //击中点参数t
                    sr.HitT = t;

                    //求交点
                    sr.Hitpoint = ray.GetPoint(t);

                    //求法线
                    sr.Normal = GetNormalVector(sr.Hitpoint);

                    //记录击中物体的材质
                    sr.HitObjMat = Mat;

                    sr.Hitobjtxture = Txture;

                    //存求交物体的全局材质
                    sr.Hitobjglomaterial = GloMaterial;

                    sr.Ishit = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                sr.Hitpoint = new Point3D();
                sr.Normal = new Vector3D();
                sr.Ishit = false;
                return false;
            }
        }

        //阴影光线的求交
        public override bool ShadowHit(Ray ray)
        {
            Vector3D oc = ray.Origin - Center;
            double r = Radius;

            double a = ray.Direction * ray.Direction;
            double b = 2 * (ray.Direction * oc);
            double c = oc * oc - r * r;
            double delta = b * b - 4.0 * a * c;

            if (delta > 0)
            {
                double t = (-b - Math.Sqrt(delta)) / (2 * a);
                if (t > kEpsilon)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
