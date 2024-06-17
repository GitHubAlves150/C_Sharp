using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pooFrases
{
    public class Pergunta
    {
        public  String  _testA;
        private String  _textB;
        //===================================================
        //--Construtor--
        public Pergunta()
        {
            Texto = "";
            Resposta = "";
        }
        //===================================================
        //--Fim-Construtor--
        //===================================================
        //--Sobre carga do Construtor--
        public Pergunta(String texto, String resposta)
        {
            Texto = texto;
            Resposta = resposta;    

        }
        //===================================================
        //--Membro de dado _texto e Propriedade Texto--
        private String _texto;

        public String Texto
        {
            set
            {
                _texto = value;
            }
            get
            {
                return _texto;
            }
        }
        //===================================================
        //--Fim-Membro de dado _texto e Propriedade Texto--


        //===================================================
        //--Propriedade Resposta--
        public String Resposta
        {
            set;
            get;
        }

       

    }
}
