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


        [Theory]
        [InlineData(10, 2, 100)]
        [InlineData(1, 5, 1)]
        [InlineData(150, 3, 3375000)]
        [InlineData(5, 2, 25)]
        [InlineData(12.5, 2, 156.25)]
        [InlineData(2.5, 2, 6.25)]
        [InlineData(2.5, 0, 1)]
        [InlineData(0, 2, 0)]
        public void Power_PositiveValues_ReturnPower(double number, int exponent, double expected)
        {
            // Arrange
            Maths math = new Maths();
            // Act
            double result = math.Power(number, exponent);
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(double.MaxValue, 27)]
        [InlineData(56, int.MaxValue)]
        [InlineData(150, 3879)]
        [InlineData(1555, 212)]
        [InlineData(125.5, 245)]
        [InlineData(2.5, 2586546)]
        public void Power_BigPositiveValues_ThrowOverflow(double number, int exponent)
        {
            // Arrange
            Maths math = new Maths();
            // Act && Assert
            Assert.Throws<OverflowException>(() => math.Power(number, exponent));
        }

        [Theory]
        [InlineData(-12, 12)]
        [InlineData(double.MinValue, int.MaxValue)]
        [InlineData(150, -3)]
        [InlineData(2.5, int.MinValue)]
        public void Power_NegativeOrZeroValues_ThrowArgument(double number, int exponent)
        {
            // Arrange
            Maths math = new Maths();
            // Act && Assert
            Assert.Throws<ArgumentException>(() => math.Power(number, exponent));
        }


        [Theory]
        [InlineData(20, 100, 20)]
        [InlineData(50, 200, 25)]
        [InlineData(0, 100, 0)]
        [InlineData(100, 100, 100)]
        [InlineData(double.MaxValue, double.MaxValue, 100)]
        [InlineData(double.MaxValue / 2, double.MaxValue, 50)]
        public void Percentage_PositiveValues_ReturnPercentage(double a, double b, double expected)
        {
            // Arrange
            Maths math = new Maths();
            // Act
            double result = math.Percentage(a, b);
            // Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(-20, 100)]
        [InlineData(100, -100)]
        [InlineData(double.MaxValue, double.MinValue)]
        [InlineData(double.MinValue, double.MaxValue)]
        public void Percentage_NegativeValues_ThrowArgument(double a, double b)
        {
            // Arrange
            Maths math = new Maths();
            // Act && Assert
            Assert.Throws<ArgumentException>(() => math.Percentage(a, b));
        }

        [Theory]
        [InlineData(100, 0)]
        [InlineData(double.MaxValue, 0)]
        public void Percentage_ZeroValue_ThrowDivideByZero(double a, double b)
        {
            // Arrange
            Maths math = new Maths();
            // Act && Assert
            Assert.Throws<DivideByZeroException>(() => math.Percentage(a, b));
        }


        [Theory]
        [InlineData(20, 100, 100)]
        [InlineData(50, 200, 200)]
        [InlineData(0, 100, 100)]
        [InlineData(100, 100, 100)]
        [InlineData(double.MaxValue, 12, double.MaxValue)]
        [InlineData(double.MaxValue / 2, double.MaxValue, double.MaxValue)]
        public void Max_PositiveValues_ReturnMax(double a, double b, double expected)
        {
            // Arrange
            Maths math = new Maths();
            // Act
            double result = math.Max(a, b);
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-20, 100)]
        [InlineData(100, -100)]
        [InlineData(double.MaxValue, double.MinValue)]
        [InlineData(double.MinValue, double.MaxValue)]
        public void Max_NegativeValues_ThrowArgument(double a, double b)
        {
            // Arrange
            Maths math = new Maths();
            // Act && Assert
            Assert.Throws<ArgumentException>(() => math.Max(a, b));
        }


        [Theory]
        [InlineData(20, 100, 20)]
        [InlineData(50, 200, 50)]
        [InlineData(0, 100, 0)]
        [InlineData(100, 100, 100)]
        [InlineData(double.MaxValue, 12, 12)]
        [InlineData(double.MaxValue / 2, double.MaxValue, double.MaxValue / 2)]
        public void Min_PositiveValues_ReturnMin(double a, double b, double expected)
        {
            // Arrange
            Maths math = new Maths();
            // Act
            double result = math.Min(a, b);
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-20, 100)]
        [InlineData(100, -100)]
        [InlineData(double.MaxValue, double.MinValue)]
        [InlineData(double.MinValue, double.MaxValue)]
        public void Min_NegativeValues_ThrowArgument(double a, double b)
        {
            // Arrange
            Maths math = new Maths();
            // Act && Assert
            Assert.Throws<ArgumentException>(() => math.Min(a, b));
        }
}}
