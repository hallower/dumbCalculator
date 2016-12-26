using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Calculator;
using System.Collections.Generic;

namespace CalculatorImplTest
{
    [TestClass]
    public class CaculatorImplTest
    {
        private CalculatorImpl ci;

        public CaculatorImplTest()
        {
            ci = new CalculatorImpl();
        }

        [TestMethod]
        public void Expression_Basic_Operations_Test()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
                {
                    {"1 + 3", 4 },
                    {"1 - 3", -2 },
                    {"3 / 3", 1 },
                    {"1 * 3", 3 },
                    {"3 * 3", 9 },
                    {"1.23 + 4", 5.23 },
                    {"1.0 * 4 + 4.123", 8.123 },
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
            
            ci.Clear();
            foreach (var item in tc)
            {
                if (false == ci.SetExpression(item.Key))
                {
                    Assert.Fail();
                }

                Assert.AreEqual(item.Value, ci.Result, "Failed, Right({0}) != Answer({1})", item.Value.ToString(), ci.Result.ToString());
            }
        }

        [TestMethod]
        public void Expression_Exception_Cases_Test()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
                {
                    {"0 / 0", 0 },
                    {"2 + 4 * 5 # 6", 0 },
                    {"/4", 0 },
                };

            ci.Clear();
            foreach (var item in tc)
            {
                if (false == ci.SetExpression(item.Key))
                {
                    continue;
                }
                Assert.Fail("Failed, Should not calculated!!!, {0} Answer({1})", item.Key.ToString(), item.Value.ToString());
            }
        }

        [TestMethod]
        public void OneChar_Basic_Operations_Test()
        {

            ci.Clear();

            ci.SetOneWord("3");
            ci.SetOneWord("+");
            ci.SetOneWord("5");
            ci.SetOneWord("5");
            ci.SetOneWord("/");
            ci.SetOneWord("(");
            ci.SetOneWord("2");
            ci.SetOneWord("+");
            ci.SetOneWord("3");
            ci.SetOneWord(")");
            Assert.AreEqual(14.0, ci.Result, "Failed, Right({0}) != Answer({1})", "14", ci.Result.ToString());

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
            Assert.AreEqual(55.0, ci.Result, "Failed, Right({0}) != Answer({1})", "55", ci.Result.ToString());
            ci.Clear();
        }
    }
}
