using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex001
{
    public class Menssagem
    {



        private string textMenssagem;

        public string TextMenssagem
        {
            get
            {
                return this.textMenssagem;
            }
            set
            {
                this.textMenssagem = value.ToLower();
            }
        }
        
        public void ExibirMenssagem()
        {
            Console.WriteLine(this.textMenssagem);
        }

    
    }



}
