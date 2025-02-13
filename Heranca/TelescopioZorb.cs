using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heranca
{
    class TelescopioZorb
    {
        //filed
        
        private double _zoom;
        private double _angulo;


        //coonstrutor
       public TelescopioZorb (double zoom, double angulo)
        {
           
            Zoom = zoom;
            Angulo = angulo;
        }

        
        public double Zoom
        {
            get { return _zoom; }
            set { _zoom = (value >=0.0 && value <=2000)? value: 2000; }
        }
        public double Angulo
        {
            get { return _angulo; }
            set { _angulo = (value >= 10.0 && value <= 95) ? value : 95; }
        }
    }
}
