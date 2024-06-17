using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex012
{
    public class Program
    {
        static void Main(string[] args)
        {

            Alarme alarme = new Alarme();
            
            char resposta = 'S';
            while(resposta!= 'N')
            {
                Console.Clear();//limpa a tela
                Console.WriteLine("Informe a hora do pitch");


                Console.WriteLine("Informe a duração do pitch");
                alarme.Tempo = Convert.ToInt32(Console.ReadLine());
                


                Console.WriteLine("Informe o efeito sonoro(1 até 500):");
                alarme.Frequencia = Convert.ToInt32(Console.ReadLine()); 

                //alarme.Tempo = tempo;
                
                alarme.iniciar();

                Console.WriteLine("Que pena!!! O seu tempo acabou!!");
                Console.WriteLine("Executar o programa novamente S/N");


                resposta = Convert.ToChar( Console.ReadLine().ToUpper() );
                    
            }           


            Console.ReadKey();

        }
    }
}
