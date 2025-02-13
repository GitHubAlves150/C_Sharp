using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tratamento_Exeções_teste_Deepseek00
{
    internal class Engrenagem_Anelar
    {
        private int _anelar;
        private int _solar;
        private int _planetaria;


        public Engrenagem_Anelar(int solar, int planeta)
        {                       
            _solar= solar;
            _planetaria = planeta;
        }
        public Engrenagem_Anelar()
        {            
            _solar = Solar;
            _planetaria = Planeta;
        }

        public int Anelar
        {
            get { return _anelar; }
            set { _anelar = (value >= 1 && value <= 70) ? value : 70; }
        }

        public int Solar
        {
            get { return _solar; }
            set { _solar = (value >= 1 && value <= 70) ? value : 70; }
        }

        public int Planeta
        {
            get { return _planetaria; }
            set { _planetaria = (value >= 1 && value <= 70) ? value : 70; }
        }


        
    }
}
