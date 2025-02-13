using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heranca
{
    class TelescopioSplit : TelescopioZorb
    {
        //filed
        private string nome;
        private double zoom;
        private double angulo;


        //coonstrutor
        public TelescopioSplit(string _nome, double _zoom, double _angulo): base(_zoom, _angulo)
        {
            NOME_ = _nome;
            Zoom_ = _zoom;
            Angulo_ = _angulo;
        }

        public string NOME_
        {
            get;
            set;

        }
        public double Zoom_
        {
            get { return zoom; }
            set { zoom = (value >= 0.0 && value <= 1000) ? value : 1000; }
        }
        public double Angulo_
        {
            get { return angulo; }
            set { angulo = (value >= 10.0 && value <= 45) ? value : 45; }
        }

    }
}
