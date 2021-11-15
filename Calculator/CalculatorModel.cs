using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    public class CalculatorModel
    {
        public CalculatorModel()
        {

        }

        public object Calculate(int operand1, int operand2, char operation)
        {
            switch (operation)
            {
                case ('+'):
                    return Sum(operand1, operand2);

                case ('-'):
                    return Subtraction(operand1, operand2);

                case ('*'):
                    return Multiplication(operand1, operand2);

                case ('/'):
                    return Division(operand1, operand2);
            }

            return null;
        }

        private int Sum (int operand1, int operand2)
        {
            return operand1 + operand2;
        }

        private int Subtraction(int operand1, int operand2)
        {
            return operand1 - operand2;
        }

        private int Multiplication(int operand1, int operand2)
        {
            return operand1 * operand2;
        }

        private object Division(int operand1, int operand2)
        {
            if (operand2.Equals(0))
            {
                return "Ошибка!";
            }

            return (double)operand1 / (double)operand2;
        }
    }
}
