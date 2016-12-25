using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorImpl
{
    public interface IOperator
    {
        string GetOperator { get; }
        int Priority { get; }
        int NumberOfOperand { get; }
        bool GetResult(double left, double right, out double result);
    }

    public static class Operators
    {
        private static Dictionary<String, IOperator> operators = new Dictionary<string, IOperator>()
        {
            { Plus.Operator, new Plus()},
            { Minus.Operator, new Minus()},
            { Multiply.Operator, new Multiply()},
            { Divide.Operator, new Divide()},
            { BraceL.Operator, new BraceL()},
            { BraceR.Operator, new BraceR()},
            { Percentage.Operator, new Percentage()},
        };

        public static Dictionary<String, IOperator> OperatorList
        {
            get
            {
                return operators;
            }
        }
    }

    class Plus : IOperator
    {
        public static string Operator = "+";
        public string GetOperator { get { return Operator; } }
        public int Priority { get { return 2; } }
        public int NumberOfOperand { get { return 2; } }

        public bool GetResult(double left, double right, out double result)
        {
            result = left + right;
            return true;
        }
    }
    class Minus : IOperator
    {
        public static string Operator = "-";
        public string GetOperator { get { return Operator; } }
        public int Priority { get { return 2; } }
        public int NumberOfOperand { get { return 2; } }

        public bool GetResult(double left, double right, out double result)
        {
            result = left - right;
            return true;
        }
    }
    class Multiply : IOperator
    {
        public static string Operator = "*";
        public string GetOperator { get { return Operator; } }
        public int Priority { get { return 3; } }
        public int NumberOfOperand { get { return 2; } }

        public bool GetResult(double left, double right, out double result)
        {
            result = left * right;
            return true;
        }
    }
    class Divide : IOperator
    {
        public static string Operator = "/";
        public string GetOperator { get { return Operator; } }
        public int Priority { get { return 3; } }
        public int NumberOfOperand { get { return 2; } }

        public bool GetResult(double left, double right, out double result)
        {
            result = 0;
            if (right == 0)
                return false;
            result = left / right;
            return true;
        }
    }

    class BraceL : IOperator
    {
        public static string Operator = "(";
        public string GetOperator { get { return Operator; } }
        public int Priority { get { return 1; } }
        public int NumberOfOperand { get { return 0; } }

        public bool GetResult(double left, double right, out double result)
        {
            result = 0;
            return true;
        }
    }

    class BraceR : IOperator
    {
        public static string Operator = ")";
        public string GetOperator { get { return Operator; } }
        public int Priority { get { return 1; } }
        public int NumberOfOperand { get { return 0; } }

        public bool GetResult(double left, double right, out double result)
        {
            result = 0;
            return true;
        }
    }

    class Percentage : IOperator
    {
        public static string Operator = "%";
        public string GetOperator { get { return Operator; } }
        public int Priority { get { return 3; } }
        public int NumberOfOperand { get { return 1; } }

        public bool GetResult(double left, double right, out double result)
        {
            result = left / 100D;
            return true;
        }
    }

    class Point : IOperator
    {
        public static string Operator = ".";
        public string GetOperator { get { return Operator; } }
        public int Priority { get { return 3; } }
        public int NumberOfOperand { get { return 2; } }

        public bool GetResult(double left, double right, out double result)
        {
            double digits = right;
            while(digits >= 1)
            {
                digits /= 10;
            }

            result = left + digits;
            return true;
        }
    }

}

