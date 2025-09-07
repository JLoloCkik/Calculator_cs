using System;
using System.Collections.Generic;
using System.Globalization; // Kell a tizedespontok helyes kezeléséhez

namespace JLolo
{
    public class Calculator
    {
        public static void Main(string[] args)
        {
            const string OPERATORS = "*/+-";

            Console.WriteLine("Számoljunk:");

            string input = Console.ReadLine().Replace(" ", "");

            ProcessInput(OPERATORS, input);
        }

        private static void ProcessInput(string operators, string input)
        {
            char[] operatorChars = operators.ToCharArray();
            string[] numsAsStrings = input.Split(operatorChars);

            List<double> numbers = new List<double>();
            
            foreach (string numStr in numsAsStrings)
                {
                numbers.Add(Convert.ToDouble(numStr));
                }

            List<char> operatorSymbols = new List<char>();

            foreach (char c in input)
                {
                if (findOperator(operators, c))
                    {
                    operatorSymbols.Add(c);
                    }
                }

            double finalResult = CalculateResult(operatorSymbols, numbers);
            Console.WriteLine($"\nA végeredmény: {finalResult}");
        }

        private static double CalculateResult(List<char> operatorSymbols, List<double> numbers)
        {
            for (int i = 0; i < operatorSymbols.Count; i++)
                {
                if (operatorSymbols[i] == '*' || operatorSymbols[i] == '/')
                    {
                    if (numbers[i + 1] == 0 && operatorSymbols[i] == '/')
                        {
                        Console.WriteLine("0 nem osztunk!!!");
                        return 0;
                        }

                    double result;

                    if (operatorSymbols[i] == '*')
                        {
                        result = numbers[i] * numbers[i + 1];
                        }
                    else
                        {
                        result = numbers[i] / numbers[i + 1];
                        }

                    numbers[i] = result;

                    numbers.RemoveAt(i + 1);
                    operatorSymbols.RemoveAt(i);

                    i--;
                    }

                }
            
            double finalResult = numbers[0];
            
            for (int i = 0; i < operatorSymbols.Count; i++)
                {
                if (operatorSymbols[i] == '+')
                    {
                    finalResult += numbers[i + 1];
                    }
                else 
                    {
                    finalResult -= numbers[i + 1];
                    }
                }

            return finalResult;
        }

        private static bool findOperator(string operators, char findElements)
        {
            foreach (char c in operators)
                {
                if (c == findElements)
                    {
                    return true;
                    }
                }
            return false;
        }
    }
}