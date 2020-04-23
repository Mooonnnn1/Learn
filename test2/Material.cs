using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    //物体材质
    public class Material
    {
        private double _ka;//环境光反射系数
        private double _kd;//漫反射系数
        private double _ks;//镜面反射系数
        private double _ns;//镜面高光指数
        private SColor _matcolor;//物体颜色
        private bool _isTexture;

        public double Kd { get => _kd; set => _kd = value; }
        public double Ks { get => _ks; set => _ks = value; }
        public double Ns { get => _ns; set => _ns = value; }
        internal SColor Matcolor { get => _matcolor; set => _matcolor = value; }
        public double Ka { get => _ka; set => _ka = value; }
        public bool IsTexture { get => _isTexture; set => _isTexture = value; }

        public Material()
        {

        }

        public Material(double ka,double kd,double ks,double ns,SColor matcolor,bool istexture)
        {
            _ka = ka;
            _kd = kd;
            _ks = ks;
            _ns = ns;
            _matcolor = matcolor;
            _isTexture = istexture;
        }
        
    }
}
