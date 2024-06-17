/*
Programa: Bip para cronometrar o tempo de um pitch
Autor: Llucas Lorenço Alves
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex011
{
    public class Alarme
    {

        private int _tempo;

        public Alarme()         //Construtor
        {
            Tempo = 3;
        }

        public Alarme(int tempo)
        {
            Tempo = tempo;
        }

        public void iniciar()   //Função membro de dados ou como dito em C# "Método"
        {
            //Contador
            Thread.Sleep(_tempo);  //O mesmo que o delay() no arduino.
            Console.Beep(250, 800); 

        }


        public int Tempo         //Controla o tempo do pitch

        {
            set
            {
                _tempo = 1000 * 60 * value;
            }
            get
            {
                return _tempo;
            }
        }


    }
}
