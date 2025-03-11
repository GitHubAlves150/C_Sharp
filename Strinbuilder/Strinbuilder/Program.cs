using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strinbuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder theString = new StringBuilder("Estou estudando C-Sharp");
            object objetVal = "-C# language-";
            string StringVal = "-learning a lot!-";

            int integerVal = 7;
            bool boolVal = false;
            double doubleVal = 43.2;

            theString.Append(objetVal);
            theString.Append(" ");
            theString.Append(StringVal);
            theString.Append(boolVal);
            theString.Append(" ");
            theString.Append(integerVal);
            theString.Append(" ");
            theString.Append(doubleVal);

            Console.WriteLine(theString.ToString());

            StringBuilder buffer = new StringBuilder();
            string information = "{0} é meu nome, {1} celulas no corpo.";

            object[] info1 = new object[2];
            object[] info2 = new object[2];

            info1[0] = "Lucas Alves";
            info1[1] = 3780000;

            info2[0] = " Atuando em Engenharia da Computação ";
            info2[1] = 2025;

            buffer.AppendFormat(information, info1);//Permite formatar strings com especificadores de formato como {0} olá sou um texto com {1} caracteres
            Console.WriteLine(buffer.ToString());//stringbuilder apesar de manipular cadeias de caracteres ele não é uma string. Por isso é preciso converte-lo para string
            /*
             texto.ToString(): Este método converte o conteúdo do objeto StringBuilder em uma string. 
            Embora o StringBuilder seja uma classe que manipula cadeias de caracteres de forma eficiente, 
            ele não é uma string em si. Portanto, é necessário chamar ToString() para obter a string final.
             */

            Console.WriteLine("Buffer nao limpo: " + buffer);

            buffer.Clear();
            Console.WriteLine("Buffer limpo: " + buffer);
            buffer.AppendFormat(information, info1);
            Console.WriteLine("inserindo ABA: " + buffer.Insert(0, "ABC "));
            Console.WriteLine("Removendo: "+ buffer.Remove(5, 2));//remove a partis do indice 5, 2 caracteres
            TestChar();

            Console.WriteLine("\n End of program");
            Console.ReadLine();
        }

        static void TestChar()
        {
            for( ; ; )
            {
                try
                {
                    Console.WriteLine("Enter a caractere: ");
                    char inputChar = char.Parse(Console.ReadLine());//char.parse converte string para caracter, por exemplo trasnforma "A" em 'A'
                    if (char.IsDigit(inputChar))
                        Console.Write(" You char  is digit..");
                    else if (char.IsLetter(inputChar))
                    {
                        
                          Console.WriteLine(" You char is letter..");
                        if(char.IsLower(inputChar))
                          Console.WriteLine("lowercase");
                        else
                          Console.WriteLine("upperCase");

                    }

                    else if (char.IsNumber(inputChar))
                        Console.WriteLine(" Is digit");
                    else if (char.IsPunctuation(inputChar))
                        Console.WriteLine("Is pontuation");
                    else
                        Console.WriteLine("is not considered pontuatuion");

                }
                catch(FormatException)
                {
                    Console.WriteLine("Erro.....");
                }
            }
        }


    }
}

/*
 O que é StringBuilder?

A StringBuilder é uma classe do namespace System.Text que permite a modificação dinâmica de cadeias de caracteres sem criar novos objetos String a cada alteração. Isso é útil porque a classe String em C# é imutável, o que significa que cada vez que você altera uma string, um novo objeto é criado na memória12.
Funcionalidades Principais

    Concatenação Eficiente: A StringBuilder permite concatenar várias cadeias de caracteres sem a sobrecarga de criar novos objetos String em cada operação. Isso melhora significativamente o desempenho em loops ou quando muitas concatenações são necessárias35.

    Métodos de Manipulação: Oferece métodos como Append, Insert, Replace, e Remove para manipular a cadeia de caracteres. Esses métodos modificam o conteúdo do StringBuilder diretamente, sem criar novos objetos48.

    Capacidade Dinâmica: A capacidade do StringBuilder pode ser especificada ou ajustada dinamicamente. Isso permite otimizar o uso de memória, pois o objeto só realoca memória quando necessário24.

    Conversão para String: Após as modificações, o conteúdo do StringBuilder pode ser facilmente convertido para uma string usando o método ToString()12.

 
 */










