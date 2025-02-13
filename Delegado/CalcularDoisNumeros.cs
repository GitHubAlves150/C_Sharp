using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegado
{
    delegate int returnOperation(int a, int b);

    internal class CalcularDoisNumeros
    {
        private int _x, _y;
        public CalcularDoisNumeros(int A, int B)
        {
            soma(A, B);
            subtrair(A, B);
        }
        public CalcularDoisNumeros()
        {
            
        }

        public int soma(int A, int B)
        {
            return A + B;
        }
        public int subtrair(int A, int B)
        {
            return A - B;
        }

    }

}
