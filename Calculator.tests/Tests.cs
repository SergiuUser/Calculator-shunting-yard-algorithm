using Calculator.utils;

namespace Calculator.tests
{
    [TestClass]
    public class Tests
    {

        private InfixCalculator calculator;

        [TestInitialize]
        public void TestInitialize()
        {
            calculator = new InfixCalculator();
        }


        [TestMethod]
        public void TestAddition()
        {
            var testCases = new Dictionary<string, double>
            {
                {"10 + 2", 12 },
                {"5 + 2 + 7", 14 },
                {"2.5 + 10 + 2", 14.5 },
                {"127 + 378 + (-2)", 503 },
                {"-5 + (-3)", -8 },
            };

            foreach (var testCase in testCases)
            {
                var expression = testCase.Key;
                var expectedResult = testCase.Value;

                var result = calculator.calculate(expression);

                Assert.AreEqual(expectedResult, result, $"Expression '{expression}' failed");

            }
        }

        [TestMethod]
        public void TestSubstraction()
        {
            var testCases = new Dictionary<string, double>
            {
                {"10 - 8", 2 },
                {"7 - 5 - 1", 1 },
                {"5.5 - 8 - 2", -4.5 },
                {"-2 - (-3)", 1},
                {"10 - (-5)", 15 },
            };

            foreach (var testCase in testCases)
            {
                var expression = testCase.Key;
                var expectedResult = testCase.Value;

                var result = calculator.calculate(expression);

                Assert.AreEqual(expectedResult, result, $"Expression '{expression}' failed");

            }
        }

        [TestMethod]
        public void TestMultiply()
        {
            var testCases = new Dictionary<string, double>
            {
                {"5 * 7", 35 },
                {"7 * 2.2", 15.4 },
                {"-5 * 2", -10 },
                {"-4 * (-8)", 32},
                {"15 * 8 * (-8)", -960 },
            };

            foreach (var testCase in testCases)
            {
                var expression = testCase.Key;
                var expectedResult = testCase.Value;

                var result = calculator.calculate(expression);

                Assert.AreEqual(expectedResult, result, $"Expression '{expression}' failed");

            }
        }

        [TestMethod]
        public void TestDivision()
        {
            var testCases = new Dictionary<string, double>
            {
                {"10/2", 5 },
                {"10 / 2 / 5", 1 },
                {"-52 / 8", -6.5},
                {"-10 / (-3)", 3.33},
                {"5/2", 2.5 },
            };

            foreach (var testCase in testCases)
            {
                var expression = testCase.Key;
                var expectedResult = testCase.Value;

                var result = calculator.calculate(expression);

                Assert.AreEqual(expectedResult, result, $"Expression '{expression}' failed");

            }
        }

        [TestMethod]
        public void TestBalancedParenthesis()
        {
            var result = calculator.calculate("10 * (2 + 3)");
            var expectedResult = 50;
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestUnhingedParenthesis()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                calculator.calculate("10 * (2 + 3");
            });
        }


        [TestMethod]
        public void TestDividingByZero()
        {
            Assert.ThrowsException<DivideByZeroException>(() =>
            {
                calculator.calculate("10 / 0");
            });
        }

        [TestMethod]
        public void TestLackOfOperator()
        {
            var result = calculator.calculate("10 + 2 2");
            var expectedResult = 32;
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestToManyOperatorsValid()
        {
            var testCases = new Dictionary<string, double>
            {
                {"3 -- 8", 11},
                {"5 +-+ 3", 2 },
                {"5 --+ 3", 8 },
                {"5 --+-- 3", 8 },
                {"5 ++ 3 -- 2", 10 },
            };

            foreach (var testCase in testCases)
            {
                var expression = testCase.Key;
                var expectedResult = testCase.Value;

                var result = calculator.calculate(expression);

                Assert.AreEqual(expectedResult, result, $"Expression '{expression}' failed");

            }
        }

        [TestMethod]
        public void TestToManyOperatorsError()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                calculator.calculate("10 + * 5");
            });
        }

        [TestMethod]
        public void TestNegativeNumbers()
        {
            var testCases = new Dictionary<string, double>
            {
                {"-3 + (-3) - (-3)", -3 },
                {"2 + (-(-(-5)))", -3 },
            };

            foreach (var testCase in testCases)
            {
                var expression = testCase.Key;
                var expectedResult = testCase.Value;

                var result = calculator.calculate(expression);

                Assert.AreEqual(expectedResult, result, $"Expression '{expression}' failed");

            }
        }

        [TestMethod]
        public void TestComma()
        {
            var testCases = new Dictionary<string, double>
            {
                {"5 / 2", 2.5 },
                {"8.5 + 5 / 1.5", 11.83 },
            };

            foreach (var testCase in testCases)
            {
                var expression = testCase.Key;
                var expectedResult = testCase.Value;

                var result = calculator.calculate(expression);

                Assert.AreEqual(expectedResult, result, $"Expression '{expression}' failed");
            }
        }

        [TestMethod]
        public void TestJustPower()
        {
            var testCases = new Dictionary<string, double>
            {
                {"5 ^ 3", 125 },
                {"(3^2 + 4 * 5) - (6 /2) * 3", 20 },
            };

            foreach (var testCase in testCases)
            {
                var expression = testCase.Key;
                var expectedResult = testCase.Value;

                var result = calculator.calculate(expression);

                Assert.AreEqual(expectedResult, result, $"Expression '{expression}' failed");

            }
        }

    }
}
