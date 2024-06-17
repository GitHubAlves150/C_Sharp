using System;

namespace IAmp
{
    public static class Utilities
    {
        public static bool NearlyEqual(double a, double b, double epsilon)
        {
            double num = Math.Abs(a);
            double num2 = Math.Abs(b);
            double num3 = Math.Abs(a - b);
            if (a == b)
            {
                return true;
            }

            if (a == 0.0 || b == 0.0 || num3 < double.Epsilon)
            {
                return num3 < epsilon;
            }

            return num3 / (num + num2) < epsilon;
        }
    }
}