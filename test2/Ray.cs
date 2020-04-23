using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    public class Ray
    {
        private Point3D _origin;//光源点
        private Vector3D _direction;//方向

        internal Point3D Origin { get => _origin; set => _origin = value; }
        internal Vector3D Direction { get => _direction; set => _direction = value; }

        public Ray()//快捷键ctor 按两次tab
        {
            this.Origin = new Point3D();
            this.Direction = new Vector3D();
        }

        public Ray(Point3D origin,Vector3D direction)
        {
            this.Origin =origin;
            this.Direction = direction;
        }


        //依据参数t的值，得到射线上的某点
        public Point3D GetPoint(double t)
        {
            return Origin + t * Direction;
        }
    }
}
