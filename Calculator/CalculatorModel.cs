using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorModel
    {
        public CalculatorModel()
        {
            
        }

        public object Calculate(int operand1, int operand2, char operation)
        {
            if (operation.Equals('+'))
            {
                return operand1 + operand2;
            }

            if (operation.Equals('-'))
            {
                return operand1 - operand2;
            }

            if (operation.Equals('*'))
            {
                return operand1 * operand2;
            }

            if (operation.Equals('/'))
            {
                return operand1 / operand2;
            }

            return null;
        }
    }
}
