using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public interface IOperator
    {
        string GetOperator { get; }
        int Priority { get; }
        int NumberOfOperand { get; }
        bool GetResult(double left, double right, out double result);
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
}

