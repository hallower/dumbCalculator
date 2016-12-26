using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorImpl ci = new CalculatorImpl();
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Clear(c), Delete Last(b), Quit(q)");
            sb.AppendLine("=> ");

            char key = Console.ReadKey(true).KeyChar;
            while (key != 'q')
            {
                Console.Out.WriteLine(sb);
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
