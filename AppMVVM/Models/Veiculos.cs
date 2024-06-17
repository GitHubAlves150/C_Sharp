using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMVVM.Models
{
    public class Veiculos
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Modelo { get; set; }

        public short Ano { get; set; }

        public List<Veiculos> ObterVeivulos()
        {

            List<Veiculos> listaVeiculos = new List<Veiculos>()
            {
                new Veiculos {Id=1, Nome="Ford Fiesta", Modelo="1.0 MPI PERSONALITÉ SEDAN 4P", Ano=2005},
                new Veiculos {Id=2, Nome="Honda crv", Modelo="2.0LX 4x2 16V", Ano=2018},
                new Veiculos {Id=3, Nome="Ladn Rover", Modelo="3.0 SE 4X4", Ano=2021},

            };
            return listaVeiculos;
        }


    }
}
