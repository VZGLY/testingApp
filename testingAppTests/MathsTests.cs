using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using testingApp;

namespace testingAppTests
{
    public class MathsTests
    {

        [Theory]
        [InlineData(10, 20, 15)]
        [InlineData(0, 0, 0)]
        [InlineData(0, double.MaxValue, 8.988465674311579E+307)]
        [InlineData(double.MaxValue, double.MaxValue, double.MaxValue)]
        [InlineData(2.5, 7.5, 5)]
        [InlineData(1.5, 2.5, 2)]
        [InlineData(0, 5, 2.5)]
        public void Average_PositiveValues_ReturnAverage(double a, double b, double expected)
        {
            // Arrange 
            Maths math = new Maths();

            // Act
            double result = math.Average(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-10, -20)]
        [InlineData(0, -12)]
        [InlineData(-15, 0)]
        [InlineData(15, -15)]
        [InlineData(-15,15)]
        [InlineData(double.MaxValue, double.MinValue)]
        [InlineData(double.MinValue, double.MaxValue)]
        public void Average_NegativeValues_ThrowArgumentException(double a, double b)
        {
            // Arrange
            Maths math = new Maths();

            // Act && Assert
            Assert.Throws<ArgumentException>(() => math.Average(a, b));
        }


        [Theory]
        [InlineData(10,2,5)]
        [InlineData(0, 5, 0)]
        [InlineData(150, 3, 50)]
        [InlineData(5, 2.0, 2.5)]
        [InlineData(double.MaxValue, 2, 8.988465674311579E+307)]
        [InlineData(2.5, 2.5, 1)]
        public void Divide_PositiveValues_ReturnDivide(double a, double b, double expected)
        {
            // Arrange
            Maths maths = new Maths();

            // Act
            double result = maths.Divide(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-10, -20)]
        [InlineData(10, -20)]
        [InlineData(-10, 20)]
        [InlineData(0, double.MinValue)]
        [InlineData(double.MinValue, 25)]
        [InlineData(double.MinValue, double.MaxValue)]
        [InlineData(-12, 0)]
        public void Divide_NegativeValues_ThrowArgment(double a, double b)
        {
            // Arrange
            Maths maths= new Maths();

            // Act && Assert
            Assert.Throws<ArgumentException>(() => maths.Divide(a, b));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 0)]
        [InlineData(double.MaxValue, 0)]
        public void Divide_ZeroValue_ThrowDivideByZero(double a, double b)
        {
            // Arrange
            Maths math = new Maths();

            // Act && assert
            Assert.Throws<DivideByZeroException>(() => math.Divide(a, b));
        }
    }
}
