using System;
using System.Collections.Generic;

namespace CalculatorApp
{
    class Program
    {
        static ConsoleColor defaultColor = ConsoleColor.White;

        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            Console.WriteLine("Enter either: \n" +
                " 1) single number,\n" +
                " 2) operator [+-/*]\n" +
                " 3) multiple numbers separated by + (new feature for + or - operation)\n" +
                " 4) nothing to exit\n" +
                " or and press enter");

            bool keepRunning = true;
            do
            {
                try
                {
                    keepRunning = HandleInput(calculator);
                }
                catch (CalculatorError e)
                {
                    ErrorMessage(e.Message, calculator);
                }
            } while (keepRunning);
            Console.WriteLine("Exit");
        }

        static bool HandleInput(Calculator calculator)
        {
            var line = Console.ReadLine().Trim();
            double value;
            if (line.Length == 0)
            {
                return false; // interpret this as user want to end application
            }
            else if (double.TryParse(line, out value))
            {
                if (calculator.NoOperationIsSet()) HandleInitialValueFromUser(calculator, value);
                else HandleSecondValueFromUser(calculator, value);
            }
            else if (line == "+" || line == "-" || line == "*" || line == "/")
            {
                HandleOperatorInputFromUser(calculator, line);
            }
            else
            {
                HandleArrayValueFromUser(calculator, line);
            }
            return true;
        }

        static void HandleArrayValueFromUser(Calculator calculator, string line)
        {
            string[] terms = line.Split('+');
            var numbers = new List<double>();
            double sumInputs = 0;
            double dValue;
            foreach (var term in terms)
            {
                if (!double.TryParse(term, out dValue)) throw new CalculatorError("bad input"); // TODO: should be other kind of exception 
                sumInputs += dValue;
                numbers.Add(dValue);
            }
            double[] numbersArr = numbers.ToArray();

            Console.ForegroundColor = ConsoleColor.Yellow; // yellow intends to signal operation in progress
            calculator.DoOperation(numbersArr);
            DiplayOperationResult(calculator);
            Console.ForegroundColor = defaultColor;
        }

        static void HandleInitialValueFromUser(Calculator calculator, double value)
        {
            calculator.Sum = value;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format(" = {0}", calculator.Sum));
            Console.ForegroundColor = defaultColor;
        }

        static void HandleSecondValueFromUser(Calculator calculator, double value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // yellow intends to signal operation in progress
            calculator.DoOperation(value);
            DiplayOperationResult(calculator);
            Console.ForegroundColor = defaultColor;
        }

        static void HandleOperatorInputFromUser(Calculator calculator, string line)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // yellow intends to signal operation in progress
            Console.WriteLine(String.Format(" = {0} {1} ", calculator.Sum, line));
            Console.ForegroundColor = defaultColor;
            calculator.CurOperator = Calculator.OperatorFromString(line);
        }

        static void DiplayOperationResult(Calculator calculator)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(String.Format(" = {0}", calculator.GetOperationExpression()));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format(" = {0}", calculator.Sum));
            Console.ForegroundColor = defaultColor;
            calculator.ResetOperator();
        }

        static void ErrorMessage(string message, Calculator calculator)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format(" = {0} {1} ", calculator.Sum, calculator.CurOperatorSign()));
            Console.ForegroundColor = defaultColor;
        }
    }
}
