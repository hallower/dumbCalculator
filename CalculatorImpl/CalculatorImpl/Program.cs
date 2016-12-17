using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
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
        };

        private Queue<string> seperated = new Queue<string>();
        public Queue<string> Expression
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
            seperated.Clear();
            
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            seperated = new Queue<string>(infixNotation.Split(delimiterChars));

            bool res = Calculate();
            if(res)
                Status = WorkingStatus.WORKING;
            else
                Status = WorkingStatus.ERROR;
            return res;
        }

        private bool Calculate()
        {
            if (seperated.Count < 1)
                return false;

            Queue<string> postfix = new Queue<string>();
            if(false == ChangeToPostfix(out postfix))
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

                        switch(op.NumberOfOperand)
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
            }catch(Exception e)
            {
                Console.Out.WriteLine("Exception, Calculate is failed!!!, " + e.Message);
                return false;
            }

            double ret;
            if(false == Double.TryParse(operand.Peek(), out ret))
            {
                return false;
            }
            Result = ret;
            return true;
        }

        private bool ChangeToPostfix(out Queue<string> ret)
        {
            Queue<string> postfix = new Queue<string>();
            Stack<string> operators = new Stack<string>();
            string token;
            
            try
            {
                do
                {
                    double result;
                    token = seperated.Dequeue();
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
                } while (seperated.Count > 0);
                
                while (operators.Count > 0)
                    postfix.Enqueue(operators.Pop());
            }
            catch(Exception e)
            {
                postfix.Clear();
                ret = postfix;
                return false;
            }

            ret = postfix;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CalculatorImpl ci = new CalculatorImpl();

            Dictionary<String, Double> tc = new Dictionary<String, Double>
            {
                {"1 + 3", 4 },
                {"0 / 0", 0 },
                {"2 + 4 * 5 # 6", 0 },
                {"2 + 3 - 5 * 5 + 6 / 2", -17 },
                {"23 * 5 / 2 + 4 * 6", 81.5 },
                {"45 + 3 * 5 - 2 + 5 / 2 * 7", 75.5 },
                {"( 1 + 2 ) * ( 3 + 4 )", 21 },
                {"( 1 + 2 ) * 3", 9 },
                {"1 + 2 * 3", 7 },
                {"( 2 * ( 3 + 6 / 2 ) + 2 ) * 4 / 2", 28 },
                {"10 + 3 * 5 / ( 16 - 4 )", 11.25 },
                {"3 + 8 / 2 * 4 - 20 / 5 + ( ( 2 + 16 / 4 ) / ( 2 + 3 ) )", 16.2 },
            };
            
            int all = tc.Count;
            int passed = 0;
            int na = 0;

            foreach(var item in tc)
            {
                Console.Out.WriteLine("TC : " + item.Key);

                if (false == ci.SetExpression(item.Key))
                {
                    if (ci.Status == CalculatorImpl.WorkingStatus.ERROR)
                    {
                        na += 1;
                        Console.Out.WriteLine("Error Occured -> NA ");
                    }
                    else
                    {
                        Console.Out.WriteLine("Error Occured -> Failed");
                    }
                    Console.Out.WriteLine();
                    continue;
                }
                if(item.Value == ci.Result)
                {
                    passed += 1;
                    Console.Out.WriteLine("TC Passed");
                }
                else
                {
                    Console.Out.WriteLine("TC Failed, Right({0}) != Answer({1})", item.Value, ci.Result);
                }
                Console.Out.WriteLine();
            }

            Console.Out.WriteLine("Result : All {0}, Passed {1}, Failed {2}, NA {3}", all, passed, all-passed-na, na);
           
        }
    }
}
