/*
Programa: Classe lista de nome e caracteristica de pokemon
Utilizando a sobrecarga de função membro
Autor: Lucas Lorenço Alves
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pooPokemonapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int resp = 100;
            Pokedex pokedex = new Pokedex();//Aqui eu já tenho um montão de pokemon na lista

            Pokemon p = pokedex.Pokemons[4];
            p.ExibirDadosPokemon();

            //Rotina
            while(resp != 0)
            {
                Console.Clear();
                resp = menu();
                if(resp ==1)
                {
                    pokedex.ListarPokemons();
                }
            Console.ReadKey();
            }


            


            



        }

    static int menu()
    {
        Console.WriteLine("Agenda Pokemons...");
        Console.WriteLine("<0> Sair da Agenda");
        Console.WriteLine("<1> Listar todos os pokemons");
        Console.WriteLine("O que deseja fazer..");

        int resposta = Convert.ToInt32(Console.ReadLine());
        return resposta;

    }


    }


    

}
