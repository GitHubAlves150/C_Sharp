using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pooPokemonapp
{
    public class Pokedex
    {      
        public Pokedex()
        {

            InicializaLista();
        }
        //========================================================

        public List<Pokemon> _Pokemon;
        public List<Pokemon> Pokemons//Lista de pokemons
        {
            get 
            {
                return _Pokemon; 
            }
        }
        //========================================================

        private void InicializaLista()
        {
            //instanciar Lista
            _Pokemon = new List<Pokemon>();//Aqui a lista foi criada
            Pokemon p = new Pokemon("Tipo de lagarto", "Lagartomon");
            _Pokemon.Add(p);//adicioona a lista
            //Inserir 10 pokemons
            p = new Pokemon("Um tipo de animal tanso:",      "Tansosauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal bocó:",       "Bocóssauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal tonto:",      "Tontossauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal moleque:",    "Molecossauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal pateta:",     "Patetossauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal xarope:",     "Xaropossauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal esquecido:",  "Esquecidossauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal mané:",       "Manésauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal biruto:",     "Birutossauro");
            _Pokemon.Add(p);//
            p = new Pokemon("Um tipo de animal panca:",      "Panacasauro");
            _Pokemon.Add(p);//

        }

        public void ListarPokemons()
        {
            for(int i =0; i<_Pokemon.Count;i++)
            {
                _Pokemon[i].ExibirDadosPokemon();
                //Pokemon p = Pokemons[i];
                //p.ExibirDadosPokemon();
            }

        }


    }
}










 
