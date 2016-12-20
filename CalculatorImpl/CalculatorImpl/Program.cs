using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorImpl
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorImpl ci = new CalculatorImpl();

            {
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

                Console.Out.WriteLine("------------------------------------------------------------------");

                ci.Clear();
                foreach (var item in tc)
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
                    if (item.Value == ci.Result)
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

                Console.Out.WriteLine("Result : All {0}, Passed {1}, Failed {2}, NA {3}", all, passed, all - passed - na, na);

            }

            ci.Clear();

            ci.SetOneWord("3");
            ci.SetOneWord("+");
            ci.SetOneWord("5");
            ci.SetOneWord("/");
            ci.SetOneWord("(");
            ci.SetOneWord("2");
            ci.SetOneWord("+");
            ci.SetOneWord("3");
            ci.SetOneWord("2");
            ci.SetOneWord(")");

            ci.Clear();

            ci.SetOneWord("1");
            ci.SetOneWord("+");
            ci.SetOneWord("2");
            ci.SetOneWord("+");
            ci.SetOneWord("3");
            ci.SetOneWord("+");
            ci.SetOneWord("4");
            ci.SetOneWord("+");
            ci.SetOneWord("5");
            ci.SetOneWord("+");
            ci.SetOneWord("6");
            ci.SetOneWord("+");
            ci.SetOneWord("7");
            ci.SetOneWord("+");
            ci.SetOneWord("8");
            ci.SetOneWord("+");
            ci.SetOneWord("9");
            ci.SetOneWord("+");
            ci.SetOneWord("10");

            ci.Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Clear(c), Delete Last(b), Quit(q)");
            sb.AppendLine("+/-(r), ");

            char key = Console.ReadKey(true).KeyChar;
            while (key != 'q')
            {
                switch(key)
                {
                    case 'c': ci.Clear(); break;
                    case 'd': ci.DeleteOneWord(); break;
                    case 'r': ci.ReverseSign(); break;
                    case 'q': break;
                    default:
                        ci.SetOneWord(key.ToString());
                        break;
                }
                key = Console.ReadKey(true).KeyChar;
            }

            return;
        }
    }
}
