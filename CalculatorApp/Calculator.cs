using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorApp
{
    public class CalculatorError : Exception
    {
        public CalculatorError()
        {
        }

        public CalculatorError(string message)
            : base(message)
        {
        }

        public CalculatorError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class Calculator
    {
        private double sum = 0;
        private double prevSum = 0;
        private string input; // last input. could be a single number or n1 + n2 + n3
        public enum Operator { Pluss, Minus, Multiply, Divide, NotSet }
        private Operator curOperator; // = Operator.NotSet;

        public Calculator()
        {
            curOperator = Operator.NotSet;
            input = "0";
        }

        public Calculator(double initialValue) : this()
        {
            sum = initialValue;
        }

        public double Sum
        {
            get { return sum; }
            set { sum = value; }
        }

        public string Input
        {
            get;
            set;
        }

        // for some reason this fails to set curOperator
        //public Operator CurOperator
        //{
        //    get;
        //    set;
        //}

        public Operator CurOperator
        {
            get { return curOperator; }
            set { curOperator = value; }
        }

        public String CurOperatorSign()
        {
            return GetOperatorSign(curOperator);
        }

        public bool NoOperationIsSet()
        {
            return curOperator == Operator.NotSet;
        }

        public void ResetOperator()
        {
            curOperator = Operator.NotSet;
        }

        public double Addition(double value)
        {
            sum += value;
            return sum;
        }

        public double Addition(double[] values)
        {
            Array.ForEach(values, value => sum += value);
            return sum;
        }

        public double Subtraction(double value)
        {
            sum -= value;
            return sum;
        }

        public double Subtraction(double[] values)
        {
            Array.ForEach(values, value => sum -= value);
            return sum;
        }

        public double Multiplication(double value)
        {
            sum *= value;
            return sum;
        }

        public double Division(double value)
        {
            if (value == 0) throw new CalculatorError("div 0");
            sum /= value;
            return sum;
        }

        public static string MakeStringOfTerms(double[] terms)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (var term in terms)
            {
                if (first) sb.Append(String.Format(" {0} ", term));
                else sb.Append(String.Format("+ {0} ", term));
                first = false;
            }
            return String.Format("({0})", sb.ToString());
        }

        public double DoOperation(double[] value)
        {
            input = MakeStringOfTerms(value);
            prevSum = sum;
            double result = 0;
            switch (curOperator)
            {
                case Operator.Pluss:
                    result = Addition(value);
                    break;
                case Operator.Minus:
                    result = Subtraction(value);
                    break;
                case Operator.Multiply:
                    throw new NotImplementedException();
                case Operator.Divide:
                    throw new NotImplementedException();
                default:
                    throw new CalculatorError("Missing Operation");
            }
            sum = result;
            return result;
        }

        public double DoOperation(double value)
        {
            input = value.ToString();
            prevSum = sum;
            double result = 0;
            switch (curOperator)
            {
                case Operator.Pluss:
                    result = Addition(value);
                    break;
                case Operator.Minus:
                    result = Subtraction(value);
                    break;
                case Operator.Multiply:
                    result = Multiplication(value);
                    break;
                case Operator.Divide:
                    result = Division(value);
                    break;
                default:
                    throw new NotImplementedException();
            }
            sum = result;
            return result;
        }

        public static string GetOperatorSign(Operator op)
        {
            switch (op)
            {
                case Operator.Pluss:
                    return "+";
                case Operator.Minus:
                    return "-";
                case Operator.Multiply:
                    return "*";
                case Operator.Divide:
                    return "/";
                default:
                    return "(missing operation)";
            }
        }

        public static Operator OperatorFromString(String sign)
        {
            return sign switch
            {
                "+" => Operator.Pluss,
                "-" => Operator.Minus,
                "*" => Operator.Multiply,
                "/" => Operator.Divide,
                _ => Operator.NotSet,
            };
        }

        public string GetOperationExpression()
        {
            return String.Format("{0} {1} {2}", prevSum, GetOperatorSign(curOperator), input);
        }
    }
}
