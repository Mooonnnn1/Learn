using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    public class Vector3D
    {
        private double _x;
        private double _y;
        private double _z;

        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }
        public double Z { get => _z; set => _z = value; }

        public Vector3D()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public Vector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        //向量点乘
        public static double operator *(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        //向量加法
        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        //向量减法
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        //标量*向量
        public static Vector3D operator *(double d, Vector3D v)
        {
            return new Vector3D(d * v.X, d * v.Y, d * v.Z);
        }

        public static Vector3D operator *(Vector3D v, double d)
        {
            return new Vector3D(d * v.X, d * v.Y, d * v.Z);
        }

        public static Vector3D operator -(Vector3D v, Point3D p)
        {
            return new Vector3D(v.X - p.X, v.Y - p.Y, v.Z - p.Z);
        }
        public static Vector3D operator -(Vector3D v)
        {
            return new Vector3D(- v.X , - v.Y , - v.Z );
        }

        //归一化
        public void Normalize()
        {
            double len = Length();
            _x = _x / len;
            _y = _y / len;
            _z = _z / len;
        }

        //向量长度
        public double Length()
        {
            return Math.Sqrt(_x * _x + _y * _y + _z * _z);
        }

        public double SquaredMagnitude()
        {
            return (X * X + Y * Y + Z * Z);
        }

        private static Random rd = new Random();
        public static Vector3D RandomInUnitSphere()
        {
            Vector3D p = new Vector3D();
            do
            {
                p = 2.0 * new Vector3D(rd.NextDouble(), rd.NextDouble(), rd.NextDouble()) - new Point3D(1, 1, 1);
            } while (p.SquaredMagnitude() >= 1.0);

            return p;
        }

        //计算反射向量
        public static Vector3D GetReflactedDir(Vector3D v, Vector3D n)
        {
            v.Normalize();
            n.Normalize();
            return v - 2 * (v * n) * n;
        }
    }
}
