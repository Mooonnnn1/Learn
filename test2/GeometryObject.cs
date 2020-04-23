using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    public abstract class GeometryObject
    {
        private Material _mat;
        private Texture _txture;
        //全局材质
        GlobalMaterial _gloMaterial;

        public GlobalMaterial GloMaterial { get => _gloMaterial; set => _gloMaterial = value; }
        internal Material Mat { get => _mat; set => _mat = value; }
        internal Texture Txture { get => _txture; set => _txture = value; }

        public abstract bool Hit(Ray ray,ShadeRec sr);//几何体与光线求交，具体方法由子类实现

        public abstract bool ShadowHit(Ray ray);
        
    }
}
