using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex010
{
    public class Retangulo
    {
        //Lados de uma figura geometrica retângulo o quandrado.
        private Double _Base,
                      _Altura;

        public Retangulo()
        {
            _Base   = 1.0f  ;
            _Altura = 1.0f  ;
        }


        public void Area_Ret_Quad(Double A, Double B) //Propriedade
        {
           
                _Base   = A;
                _Altura = B;
                if(_Base <=0)
                {
                    Console.WriteLine("Media menor que o aceitável");
                    _Base = 1;
                    _Altura = 1;                    
                }
                //Calculo da Area do triangulo
                Calculo();             
                       
           

        }//Fim da classe função Area_Ret_Quad


        //Realiza Calculo da area da figura geométrica
        public Double Calculo()
        {

            return _Base * _Altura;
            
        }


        //Função classe que  exibe os valores no console
        public void Exibe()
        {

            Console.WriteLine("O valor da Area é " + Calculo() );

        }
    }
}//#Include "Lib_H" no C++
