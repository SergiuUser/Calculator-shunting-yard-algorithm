namespace Calculator.utils
{
    public class InfixCalculator
    {
        InfixToPostfixConvertor _convertor = new InfixToPostfixConvertor();

        public double calculate(string expression)
        {
            Queue<string> output = _convertor.getPostfixAsQueue(expression);

            Stack<double> stack = new Stack<double>();

            if (output.Count == 0)
                throw new InvalidOperationException("Expression is empty");

            while (output.Count > 0)
            {
                string token = output.Dequeue();

                if (isOperator(token))
                {

                    if (stack.Count < 2)
                        throw new InvalidOperationException("Invalid expression. No operands for operator: " + token);
                        double nr2 = stack.Pop();
                        double nr1 = stack.Pop();
                        stack.Push(calculateNumbers(token[0], nr1, nr2));
                }
                else
                {
                    double number;
                    if (!double.TryParse(token, out number))
                        throw new InvalidOperationException("Invalid expression. Non numeric token: " + token);
                    stack.Push(Convert.ToDouble(token));


                }
            }
            double result = Math.Round(stack.Pop(), 2);
            return result;
        }

        private bool isOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
        }

        private double calculateNumbers(char token, double nr1, double nr2)
        {
            switch (token)
            {
                case '+': return nr1 + nr2;
                case '-': return nr1 - nr2;
                case '*': return nr1 * nr2;
                case '/':
                    {
                        if (nr2 == 0)
                            throw new DivideByZeroException("Dividing by zero is prohibited");
                        else
                            return nr1 / nr2;

                    }
                case '^': return Math.Pow(nr1, nr2);
                default: throw new InvalidOperationException("Unknown operator: " + token);
            }
        }

    }
}
