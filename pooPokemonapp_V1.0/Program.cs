/*
Programa: Classe lista de nome e caracteristica de pokemon
Utilizando a sobrecarga de função membro
Autor: Lucas Lorenço Alves
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pooPokemonapp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Pokemon p = new Pokemon();
            p.Nome = "Bulbasauro";
            p.DadosPokemon ="Bulbasauro é um tipo planta";
            p.ExibirDadosPokemon();

            Console.WriteLine("================================");
            p = new Pokemon("È um tipo de criatura colossal", "Colapsauro");
            p.ExibirDadosPokemon();
            Console.ReadKey();



        }
    }
}
