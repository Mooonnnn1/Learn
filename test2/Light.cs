using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class Light
    {
        private SColor _lightcolor;
        private Point3D _position;

        public SColor Lightcolor { get => _lightcolor; set => _lightcolor = value; }
        public Point3D Position { get => _position; set => _position = value; }
    }
}
