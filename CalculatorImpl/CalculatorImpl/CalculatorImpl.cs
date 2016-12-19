using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorImpl
{
    class CalculatorImpl
    {
        public enum WorkingStatus
        {
            WORKING,
            ERROR,
        }
        public WorkingStatus Status
        {
            get;
            private set;
        }

        // TODO : add more operators
        private Dictionary<String, IOperator> operatorsPriority = new Dictionary<string, IOperator>()
        {
            { Plus.Operator, new Plus()},
            { Minus.Operator, new Minus()},
            { Multiply.Operator, new Multiply()},
            { Divide.Operator, new Divide()},
            { BraceL.Operator, new BraceL()},
            { BraceR.Operator, new BraceR()},
        };

        private List<string> seperated = new List<string>();
        public List<string> Expression
        {
            get
            {
                return seperated;
            }
        }

        public double Result
        {
            get;
            private set;
        }
        public CalculatorImpl()
        {
            Status = WorkingStatus.WORKING;
        }

        public bool SetExpression(string infixNotation)
        {
            Clear();

            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            seperated = new List<string>(infixNotation.Split(delimiterChars));

            bool res = Calculate();
            if (res)
                Status = WorkingStatus.WORKING;
            else
                Status = WorkingStatus.ERROR;
            return res;
        }
        public bool SetOneWord(string oneWord)
        {
            double inputNumber;

            if (Double.TryParse(oneWord, out inputNumber))
            {
                double lastNumber;
                if (seperated.Count > 0 &&
                    Double.TryParse(seperated.Last(), out lastNumber))
                {
                    seperated[seperated.Count - 1] = (lastNumber * 10 + inputNumber).ToString();
                }
                else
                {
                    seperated.Add(oneWord);
                }
            }
            else
            {
                if (false == operatorsPriority.ContainsKey(oneWord))
                {
                    Console.Out.WriteLine("Wrong character inserted!!!, " + oneWord);
                    return false;
                }
                seperated.Add(oneWord);
            }

            Calculate();
            return true;
        }
        public void Clear()
        {
            seperated.Clear();
        }
        public void DeleteOneWord()
        {
            double lastNumber;
            if (seperated.Count == 0)
            {
                return;
            }

            if (Double.TryParse(seperated.Last(), out lastNumber))
            {
                if (lastNumber < 10)
                    seperated.RemoveAt(seperated.Count - 1);
                else
                    seperated[seperated.Count - 1] = ((lastNumber - lastNumber % 10) / 10).ToString();
            }
            else
            {
                seperated.RemoveAt(seperated.Count - 1);
            }

            Calculate();
        }

        private bool Calculate()
        {
            if (seperated.Count < 1)
                return false;

            {
                Console.Out.Write("Infix => ");
                foreach (var item in seperated)
                {
                    Console.Out.Write(item + " ");
                }
                Console.Out.WriteLine();
            }

            Queue<string> postfix = new Queue<string>();
            if (false == ChangeToPostfix(out postfix))
            {
                Console.Out.WriteLine("Can't Convert to postfix");
                return false;
            }

            {
                Console.Out.Write("Postfix => ");
                foreach (var item in postfix)
                {
                    Console.Out.Write(item + " ");
                }
                Console.Out.WriteLine();
            }

            Stack<string> operand = new Stack<string>();

            try
            {
                foreach (var token in postfix)
                {
                    double result;
                    if (Double.TryParse(token, out result))
                    {
                        operand.Push(token);
                    }
                    else
                    {
                        IOperator op = operatorsPriority[token];
                        string op1 = "";
                        string op2 = "";

                        switch (op.NumberOfOperand)
                        {
                            case 2:
                                op2 = operand.Pop();
                                op1 = operand.Pop();
                                break;
                            case 1:
                                op1 = operand.Pop();
                                break;
                        }

                        double _op1;
                        double _op2;

                        Double.TryParse(op1, out _op1);
                        Double.TryParse(op2, out _op2);
                        if (false == op.GetResult(_op1, _op2, out result))
                        {
                            // TODO : throw with each operator's reason
                            Console.Out.WriteLine("Calculate is failed!!!");
                            return false;
                        }
                        operand.Push(result.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Exception, Calculate is failed!!!, " + e.Message);
                return false;
            }

            double ret;
            if (false == Double.TryParse(operand.Peek(), out ret))
            {
                return false;
            }
            Result = ret;
            Console.Out.WriteLine("Result = " + Result);
            return true;
        }

        private bool ChangeToPostfix(out Queue<string> ret)
        {
            Queue<string> postfix = new Queue<string>();
            Stack<string> operators = new Stack<string>();

            try
            {
                foreach (string token in seperated)
                {
                    double result;
                    if (Double.TryParse(token, out result))
                    {
                        postfix.Enqueue(result.ToString());
                    }
                    else if (0 == token.CompareTo("("))
                    {
                        operators.Push(token);
                    }
                    else if (0 == token.CompareTo(")"))
                    {
                        string topToken = operators.Pop();
                        while (0 != topToken.CompareTo("("))
                        {
                            postfix.Enqueue(topToken);
                            topToken = operators.Pop();
                        }
                    }
                    else
                    {
                        while ((operators.Count > 0) &&
                            (operatorsPriority[operators.Peek()].Priority >= operatorsPriority[token].Priority))
                        {
                            postfix.Enqueue(operators.Pop());
                        }
                        operators.Push(token);
                    }
                }

                while (operators.Count > 0)
                    postfix.Enqueue(operators.Pop());
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Exception : " + e.Message);
                postfix.Clear();
                ret = postfix;
                return false;
            }

            ret = postfix;
            return true;
        }
    }
}
