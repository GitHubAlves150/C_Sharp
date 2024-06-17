using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pooFrases
{
    public class PerguntaPlus:Pergunta //PerguntaPlus esta herdando propriedades da classe Pergunta
    {
       public PerguntaPlus():base() //A chamada da classe base() inicializa o construtor da classe pai-Maravilha cara!!! 
        {
            //Texto    = "";
            //Resposta = "";
            Dica     = "";
        }

        //======================================
        //--Sobre carga de construtor--
        //Posso passar a variavel texto e resposta para a chamada base() poder inicializar no construtor da classe pai
        public PerguntaPlus(String texto, String resposta, String dica):base(texto, resposta) //A chamada da classe base() inicializa o construtor da classe pai-Maravilha cara!!! 
        {
            Dica = dica;

        }
        //O que faz aqui é um mistério
        public String Dica
        {
            set;
            get;
        }
        //Fim-O que faz aqui é um mistério
    }
}
