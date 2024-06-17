/*
programa: Calsse pessoa que calcula idade

Conceito deste programa é trabalhar com a classe pessoa que calcula pega 
o nome e ano de nascimento e calcula a idade, usando apenas propriedade
get ou set.
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex004
{
    public class Pessoa
    {
        private int _AnoNascimento;
        private string _Name;


        //controle do ano de nascimento
        public int Anonascimento
        {

            get
            {
                return this._AnoNascimento;
            }
            set
            {
                this._AnoNascimento = value;
            }

        }

        //Controle da string
        public string Name
        {

            get
            {
                return _Name;
            }
            set
            {
                _Name = value.ToUpper();
            }
        }
            //Vamos melhorar
            private int idade;

        public int Idade
        {

            get 
            { 
                this.CalcularIdade();
                return this.idade; 
                
            }//Vou utilizar o metodo get 
            //set { this.idade = value; }


        }

        public void ExibirDados()
        {
            Console.WriteLine("Nome " + this._Name);
            Console.WriteLine("Ano de nascimento " + this._AnoNascimento );
            this.CalcularIdade();
            this.idade=this.Idade;//Coloco na variavel idade o retorno da função membro 
            Console.WriteLine("Idade " + this.idade);
        }

        private void CalcularIdade()
        {
            DateTime data = DateTime.Now;
            int ano = data.Year;
            //int idade = ano - this._AnoNascimento;            
            this.idade = ano-this._AnoNascimento;//uso a variavel idade criada na propriedade
            //return idade;
        }

    }
}

