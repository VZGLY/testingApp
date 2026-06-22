using System;
using System.Collections.Generic;
using System.Text;
using testingApp;

namespace testingAppTests
{
    public class MathsTests
    {

        [Fact]
        public void Average_PositiveValues_ReturnAverage()
        {
            // Arrange 
            Maths math = new Maths();
            double a = 10;
            double b = 20;
            double expected = 15;

            // Act
            double result = math.Average(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Average_NegativeValues_ThrowArgumentException()
        {
            // Arrange
            Maths math = new Maths();
            double a = -10;
            double b = -20;

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

        [Fact]
        public void Divide_ZeroValue_ThrowDivideByZero()
        {
            // Arrange
            Maths math = new Maths();
            double a = 10;
            double b = 0;

            // Act && assert
            Assert.Throws<DivideByZeroException>(() => math.Divide(a, b));
        }
    }
}
