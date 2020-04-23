using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    public class ShadeRec
    {
        private bool _ishit;//击中与否
        private double _hitT;//击中点参数t
        private Point3D _hitpoint;//击中点
        private Vector3D _normal;//击中点法线
        private Material _hitObjMat;//击中物体材质
        private GlobalMaterial _hitobjglomaterial;
        private Texture _hitobjtxture;

        public bool Ishit { get => _ishit; set => _ishit = value; }
        public double HitT { get => _hitT; set => _hitT = value; }
        public GlobalMaterial Hitobjglomaterial { get => _hitobjglomaterial; set => _hitobjglomaterial = value; }
        internal Point3D Hitpoint { get => _hitpoint; set => _hitpoint = value; }
        internal Vector3D Normal { get => _normal; set => _normal = value; }
        internal Material HitObjMat { get => _hitObjMat; set => _hitObjMat = value; }
        internal Texture Hitobjtxture { get => _hitobjtxture; set => _hitobjtxture = value; }
    }
}
