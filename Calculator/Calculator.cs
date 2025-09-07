using System;
using System.Collections.Generic;
using System.Globalization;

namespace JLolo
{
    public class Calculator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Számoljunk:");

            string input = Console.ReadLine().Replace(" ", "");

            Console.WriteLine(CalculateResult(input));
        }

        private static double CalculateResult(string input)
        {
            Queue<object> nums = new Queue<object>();
            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
                {
                int startIndex = i;

                if (char.IsDigit(input[i]))
                    {
                    while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.'))
                        {
                        i++;
                        }

                    nums.Enqueue(double.Parse(input.Substring(startIndex, i - startIndex), CultureInfo.InvariantCulture));
                    i--;
                    }
                else
                    {
                    char op1 = input[i];
                    while (operators.Count > 0 && GetPrecedence(operators.Peek()) >= GetPrecedence(op1))
                        {
                        nums.Enqueue(operators.Pop());
                        }

                    operators.Push(op1);
                    }
                }

            while (operators.Count > 0)
                {
                nums.Enqueue(operators.Pop());
                }

            Stack<double> evaluationStack = new Stack<double>();

            while (nums.Count > 0)
                {
                object item = nums.Dequeue();
                
                if (item is double)
                    {
                    evaluationStack.Push((double)item);
                    }
                else if (item is char)
                    {
                    double rightOperand = evaluationStack.Pop();
                    double leftOperand = evaluationStack.Pop();

                    switch ((char)item)
                        {
                            case '+':
                                double resultPlus = leftOperand + rightOperand;
                                evaluationStack.Push(resultPlus);
                                break;
                            case '-':
                                double resultMinus = leftOperand - rightOperand;
                                evaluationStack.Push(resultMinus);
                                break;
                            case '*':
                                double resultMultiply = leftOperand * rightOperand;
                                evaluationStack.Push(resultMultiply);
                                break;

                            case '/':
                                if (rightOperand == 0)
                                    {
                                    Console.WriteLine("Nullával nem lehet osztani!");
                                    return 0;
                                    }

                                double resultDivide = leftOperand / rightOperand;
                                evaluationStack.Push(resultDivide);
                                break;
                        }
                    }
                }

            double finalResult = evaluationStack.Pop();
            return finalResult;
        }

        private static int GetPrecedence(char op)
        {
            if (op == '*' || op == '/')
                {
                return 2;
                }
            else if (op == '+' || op == '-')

                {
                return 1;
                }

            return 0;
        }
    }
}
//Régi kod kvadratikus, O(n²)
// Uj kod  konstans idejűek, O(1)
// Shunting-yard https://en.wikipedia.org/wiki/Shunting_yard_algorithm