/*
Programa: Construtor parte 2
Autor: Lucas Lorenço Alves
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ex007
{
    public class Pessoa
    {
        private int _idade;     // membros de dados privados
        private string _nome;   // Membros de dados privados

        //Construtor
        public Pessoa(string nome, int idade)
        {
            _idade = idade;
            _nome = nome;
        }//Fim do coonstrutor

        public void Exibe()
        {

            Console.WriteLine("Nome " + _nome);
            Console.WriteLine("Idade " + _idade);
        }



    }//Fim da classe Pessoa
}//Fim do namespace
