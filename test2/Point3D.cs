﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    public class Point3D
    {
        //快捷键 ctrl+r+e
        private double _x;
        private double _y;
        private double _z;

        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }
        public double Z { get => _z; set => _z = value; }

        public Point3D()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public Point3D(double x,double y,double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        //两个点的减法
        public static Vector3D operator-(Point3D p1, Point3D p2)
        {
            return new Vector3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        //点+向量
        public static Point3D operator+(Point3D p,Vector3D v)
        {
            return new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }
    }
}
