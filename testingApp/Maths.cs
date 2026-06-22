using System;
using System.Collections.Generic;
using System.Text;

namespace testingApp
{
    public class Maths
    {
        public double Average(double a, double b)
        {

            return (a >= 0 || b >= 0) ? (a + b) / 2 : throw new ArgumentException("Les valeurs doivent être positives ou egal à 0");

        }
    
        public double Divide(double a, double b)
        {
            return (a >= 0 && b >= 0) ? b != 0 ? a / b : throw new DivideByZeroException() : throw new ArgumentException();
        }
    
    }
}
