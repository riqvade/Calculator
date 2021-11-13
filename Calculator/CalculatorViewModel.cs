using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using DemoApp;

namespace Calculator
{
    class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calc;

        private int? _operand1;
        private char? _operator;
        private int? _operand2;
        private object _result;


        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand NumberCommand { get; private set; }
        public RelayCommand PlusCommand { get; private set; }
        public RelayCommand MinusCommand { get; private set; }
        public RelayCommand MultiplyCommand { get; private set; }
        public RelayCommand DivideCommand { get; private set; }
        public RelayCommand EqualsCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }


        private string _display;
        public string Display
        {
            get => _display;

            set
            {
                if (value != _display)
                {
                    _display = value;
                    NotifyPropertyChanged("Display");
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public CalculatorViewModel(CalculatorModel calculator)
        {
            _calc = calculator;

            Clear();

            NumberCommand = new RelayCommand(param => { Number(Convert.ToInt32(param)); },
                                            param => CanDoNumber());

            PlusCommand = new RelayCommand(param => { Plus(); },
                                            param => CanDoOperator());

            MinusCommand = new RelayCommand(param => { Minus(); },
                                            param => CanDoOperator());

            MultiplyCommand = new RelayCommand(param => { Multiply(); },
                                            param => CanDoOperator());

            DivideCommand = new RelayCommand(param => { Divide(); },
                                            param => CanDoOperator());

            EqualsCommand = new RelayCommand(param => { Calculate();},
                                            param => CanCalculate());

            ClearCommand = new RelayCommand(param => { Clear(); },
                                            param => CanClear());
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanDoOperator()
        {
            return _operand1.HasValue && !_operator.HasValue;
        }

        public void Plus()
        {
            DoOperator('+');
        }

        public void Minus()
        {
            DoOperator('-');
        }

        public void Multiply()
        {
            DoOperator('*');
        }

        public void Divide()
        {
            DoOperator('/');
        }

        private void DoOperator(char o)
        {
            if (!CanDoOperator())
            {
                throw new InvalidOperationException();
            }

            _operator = o;
            Display += " " + o + " ";
        }

        public bool CanDoNumber()
        {
            return _result == null;
        }

        public void Number(int num)
        {
            if (!CanDoNumber())
            {
                throw new InvalidOperationException();
            }

            if ((num < 0) || (num > 9))
            {
                throw new ArgumentException();
            }

            if (!_operator.HasValue)
            {
                if (!_operand1.HasValue)
                {
                    _operand1 = num;
                    Display = num.ToString();
                }
                else
                {
                    _operand1 = _operand1 * 10 + num;
                    Display += num;
                }
            }
            else
            {
                if (!_operand2.HasValue)
                {
                    _operand2 = num;
                }
                else
                {
                    _operand2 = _operand2 * 10 + num;
                }

                Display += num;
            }
        }

        private void UpdateOperand(int num, ref int? operand)
        {
            if (!operand.HasValue)
            {
                operand = num;
            }
            else
            {
                operand = operand * 10 + num;
            }
        }

        private void Calculate()
        {
            if (!CanCalculate())
            {
                throw new InvalidOperationException();
            }

            _result = _calc.Calculate(_operand1.Value, _operand2.Value, _operator.Value);
            if (_result == null)
            {
                MessageBox.Show("Ошибка вычисления!");
                return;
            }

            Display = _result.ToString();
        }

        public bool CanCalculate()
        {
            return (_operand1.HasValue && _operator.HasValue && _operand2.HasValue && _result == null);
        }

        public bool CanClear()
        {
            return true;
        }

        public void Clear()
        {
            _operand1 = null;
            _operator = null;
            _operand2 = null;
            _result = null;
            Display = "0";
        }
    }
}
