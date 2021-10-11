using System;
using Xunit;
using CalculatorApp;

namespace Test
{
    public class UnitTest1 // not perfect naming, but keeping it 
    {
        int someInitialValue = 4, someSecondValue = 5;
        private Calculator.Operator someOperation = Calculator.Operator.Pluss;
        private Calculator calc;

        public UnitTest1()
        {
            calc = new Calculator(someInitialValue);
        }

        [Fact]
        public void Addition()
        {
            var nextValue = calc.Addition(someSecondValue);
            Assert.Equal(someInitialValue + someSecondValue, nextValue);
        }

        [Fact]
        public void Addition_givenArray()
        {
            calc.Sum = someInitialValue;
            double[] array = { 1.0, -2.0 };
            // act
            var nextValue = calc.Addition(array);
            Assert.Equal(someInitialValue + array[0] + array[1], nextValue);
        }

        [Fact]
        public void Subtraction()
        {
            var nextValue = calc.Subtraction(someSecondValue);
            Assert.Equal(someInitialValue - someSecondValue, nextValue);
        }

        [Fact]
        public void Subtraction_givenArray()
        {
            double[] array = { 1.0, -2.0 };
            // act
            var nextValue = calc.Subtraction(array);
            Assert.Equal(someInitialValue - (array[0] + array[1]), nextValue);
        }

        [Fact]
        public void Multiplication()
        {
            var nextValue = calc.Multiplication(someSecondValue);
            Assert.Equal(someInitialValue * someSecondValue, nextValue);
        }

        [Fact]
        public void Division()
        {
            var nextValue = calc.Division(someSecondValue);
            Assert.Equal(someInitialValue / (double)someSecondValue, nextValue);
        }

        [Fact]
        public void Division_withZero()
        {
            Assert.Throws<CalculatorError>(() => calc.Division(0));
        }

        [Fact]
        public void GetOperationExpression_afterAddition_ShouldGivePlussSign()
        {
            calc.CurOperator = Calculator.Operator.Pluss;
            // act
            calc.DoOperation(someSecondValue);
            // should look somthing like: 3 + 4
            Assert.Contains("+", calc.GetOperationExpression());
        }

        [Fact]
        public void GetOperationExpression_afterSubtraction_ShouldGiveMinusSign()
        {
            calc.CurOperator = Calculator.Operator.Minus;
            // act
            calc.DoOperation(someSecondValue);
            // should look somthing like: 3 - 4
            Assert.Contains("-", calc.GetOperationExpression());
        }

        [Fact]
        public void GetOperationExpression_afterMultiplication_ShouldGiveMultiplicationSign()
        {
            calc.CurOperator = Calculator.Operator.Multiply;
            // act
            calc.DoOperation(someSecondValue);
            // Expression should look somthing like: 3 * 4
            Assert.Contains("*", calc.GetOperationExpression());
        }

        [Fact]
        public void GetOperationExpression_afterDivision_ShouldGiveDivitionSign()
        {
            calc.CurOperator = Calculator.Operator.Divide;
            // act
            calc.DoOperation(someSecondValue);
            // Expression should look somthing like: 3 / 4
            Assert.Contains("/", calc.GetOperationExpression());
        }

        [Fact]
        public void GetOperationExpression_shouldContainPreviusValue()
        {
            int prevValue = someInitialValue + 1; // make sure its not same as someInitialValue
            calc.Sum = prevValue;
            calc.CurOperator = someOperation;
            // act
            calc.DoOperation(someSecondValue);
            // assert, should look something like: 4 + 5, checking if the 4 is there
            Assert.Contains(prevValue.ToString(), calc.GetOperationExpression());
        }

        [Fact]
        public void GetOperationExpression_shouldContainSecondValue()
        {
            int secondValue = someSecondValue + 1;
            calc.Sum = someInitialValue;
            calc.CurOperator = someOperation;
            // act
            calc.DoOperation(secondValue);
            // assert, should look something like: 4 + 5, checking if the 5 is there
            Assert.Contains(secondValue.ToString(), calc.GetOperationExpression());
        }

        [Fact]
        public void NoOperationSet_whenSet_shouldGiveFalse()
        {
            calc.CurOperator = Calculator.Operator.Divide;
            Assert.False(calc.NoOperationIsSet());
        }
    }
}
