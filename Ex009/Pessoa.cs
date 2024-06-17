/*
Programa: Construtores
Autor: Lucas Lorenço Alves
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Ex009
{
    public class Pessoa
    {
        private string _Nombre;
        public Pessoa() 
        {
          nombre = "Default";
        }

        public string nombre
        {
            set 
            {                
                this._Nombre = value;  //Precisa receber o valor para poder ser tratado esse dado
                if( _Nombre == "Alicia")
                {
                    _Nombre = "Alicia Shulz";
                }
              
            }
            get
            {
                return _Nombre;
            }
        }

        public void Exibe()
        {

            Console.WriteLine("Nome " + _Nombre);
        }


    }

}
