using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX005
{
    public class Animal
    {

		//Nome do animal para função nome
		private string _Nome;
        //Função membro "nome" que gerencia duas outras funções "set" e "get"
		//Este tipo de construção vem do C++, o que diferencia é que no C++ tem que formar duas funções membros
		//Enquanto no c# uma função membro gerencia as duas que seriam set e get 

		public string nome		
		{
            set                 //Função "set" seta as propriedades da função
            {


                this._Nome = value.ToUpper();
            }
            get                 //Função get retorna
			{
				return this._Nome; 
			}
           	
		}//Fim da função nome

        //tipo do animal para função Tipo
        private string _Tipo;

        //Função membro "nome" que gerencia duas outras funções "set" e "get"
        //Este tipo de construção vem do C++, o que diferencia é que no C++ tem que formar duas funções membros
        //Enquanto no c# uma função membro gerencia as duas que seriam set e get
        public string Tipo
		{
			           
            set 
			{
				if(value == "Gato" || value == "Cachorro" || value =="Peixe")
				{
					_Tipo = value;
				}
				else
				{
					_Tipo="Peixe";
				}

			}
            get
            {
                return _Tipo;
            }


        }//Fim da função Tipo

		public void ExibirDados()
		{
			Console.WriteLine("Nome " + _Nome);
            Console.WriteLine("Tipo " + _Tipo);
        }

	}
}
