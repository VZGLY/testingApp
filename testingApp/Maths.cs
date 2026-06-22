using System;
using System.Collections.Generic;
using System.Text;

namespace testingApp
{
    public class Maths
    {
        public double Average(double a, double b)
        {

            return (a >= 0 && b >= 0) ? a + (b - a) / 2 : throw new ArgumentException("Les valeurs doivent être positives ou egal à 0");

        }
    
        public double Divide(double a, double b)
        {
            return (a >= 0 && b >= 0) ? b != 0 ? a / b : throw new DivideByZeroException() : throw new ArgumentException();
        }

        public double Power(double number, int exponent)
        {
            if (number < 0 || exponent < 0)
                throw new ArgumentException("Les valeurs doivent être positives ou egal à 0");

            double result = 1;

            for (int i = 1; i < exponent; i++)
            {
                result *= number;
            }

            return result;
        }

        public double Percentage(double value, double total)
        {
            if (value < 0 || total < 0)
                throw new ArgumentException("Les valeurs doivent être positives ou egal à 0");

            if (total == 0)
                throw new DivideByZeroException();

            return value / (total * 100);
        }

        public double Max(double a, double b)
        {
            return (a >= 0 && b >= 0) ? (a >= b ? b : a) : throw new ArgumentException("Les valeurs doivent être positives ou egal à 0");
        }

    }
}
