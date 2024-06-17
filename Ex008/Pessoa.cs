using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex008
{
    public class Pessoa
    {
        private string _nome;

        //Construtor
        public Pessoa() 
        {
            _nome = "default";
        } 
        public string nome(string Nome)
        {
           
           this._nome = Nome;
            return this._nome;
        }
       
        public void Exibe()
        {

            Console.WriteLine("Nome " + _nome);
        }
    }//Fim da classe
     
}//Fim do arquivo header
