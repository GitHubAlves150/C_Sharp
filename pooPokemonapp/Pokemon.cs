/*
Programa: Classe lista de nome e caracteristica de pokemon
Autor: Lucas Lorenço Alves
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pooPokemonapp
{
    public class Pokemon
    {

        public Pokemon()
        {
            this._dadosPokem = " ";
            this._nome = " ";
        }

        private String _nome;      
        private String _dadosPokem;//Armazena o valor dos dos dados do pokemon

        public String Nome 
        {           
            set
            {
                String texto=value.ToUpper();
                _nome = texto;

            }
            get
            {
                return _nome;
            }
        }

        public String DadosPokemon//Seta as caracteristcas do pokemnon
        {
            set
            {
                _dadosPokem = value.ToUpper();
            }
            get
            {
                return _dadosPokem;
            }
        }

        public void ExibirDadosPokemon()
        {
            Console.WriteLine("Nome do Pokemon " + _nome);
            Console.WriteLine("Dados do Pokemon " + _dadosPokem);
        }
             



    }
}
