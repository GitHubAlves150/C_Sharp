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

namespace Ex012
{
    public class Alarme
    {

        private int _tempo;
        
        //===============================================
        public Alarme()         //Construtor
        {
            Tempo = 3;
            Frequencia = 200;

        }
        //===============================================

        public Alarme(int tempo, int Freq)
        {
            Tempo = tempo;
            Frequencia = Freq;
        }

        public void iniciar()   //Função membro de dados ou como dito em C# "Método"
        {
            //Contador
            Thread.Sleep(_tempo);  //O mesmo que o delay() no arduino.
            Console.Beep(Frequencia, 1000); //O mesmo que o  "\a" no C

        }
        public int Frequencia
        {
            get;
            set;
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
