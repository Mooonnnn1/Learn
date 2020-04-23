using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class World
    {
        List<GeometryObject> _lstGeoObj = new List<GeometryObject>();

        public List<GeometryObject> LstGeoObj { get => _lstGeoObj; set => _lstGeoObj = value; }

        //添加物体
        public void AddGeoObj(GeometryObject geoObj)
        {
            _lstGeoObj.Add(geoObj);
        }

        public void Bulid()
        {
            //球体1
            Sphere sphere1 = new Sphere(new Point3D(0, 0, 0), 1);
            Material mat1 = new Material(0.2, 0.5, 0.3, 500, new SColor(1, 1, 1),true);
            sphere1.Mat = mat1;

            //球体2
            Sphere sphere2 = new Sphere(new Point3D(0, -101, -1), 100);
            Material mat2 = new Material(0.2, 0.8, 0.3, 50, new SColor(0.38, 0.36, 0.47),false);
            sphere2.Mat = mat2;
            

            AddGeoObj(sphere1);
            AddGeoObj(sphere2);
        }

        //和所有物体计算最近的交点
        public ShadeRec HitAll(Ray ray)
        {
            ShadeRec srNearest = null;
            
            double tMin = 1e10;

            for (int i = 0; i < _lstGeoObj.Count; i++)
            {
                ShadeRec sr = new ShadeRec();
                if (_lstGeoObj[i].Hit(ray, sr)&&sr.HitT<tMin)
                {
                    srNearest = sr;
                    tMin = sr.HitT;
                }
            }

            return srNearest;
        }

        public bool ShadowHitAll(Ray ray)
        {
            for(int i=0;i<_lstGeoObj.Count;i++)
            {
                if(_lstGeoObj[i].ShadowHit(ray))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
