using System.Text;

namespace Calculator.utils
{
    public class InfixToPostfixConvertor
    {

        private Queue<string> convertInfixToPostFix(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new InvalidOperationException("Expression is empty");

            if (!isParanthesisBalanced(expression))
                throw new ArgumentException("Expression has unbalanced paranthesis");

            Stack<char> operatorStack = new Stack<char>();
            Queue<string> outputQueue = new Queue<string>();

            StringBuilder number = new StringBuilder();
            bool lastWasOperator = true;

            expression = expression.Replace(" ", "");

            foreach (char token in expression)
            {
                if (char.IsDigit(token) || token == '.')
                {
                    number.Append(token);
                    lastWasOperator = false;
                }
                else if ((token == '-' || token == '+') && lastWasOperator)
                {
                        outputQueue.Enqueue("0");
                        operatorStack.Push(token);
                }
                else
                {
                    if (token != ')' && !char.IsWhiteSpace(token))
                        lastWasOperator = true;
                    if (number.Length != 0)
                    {
                        outputQueue.Enqueue(number.ToString());
                        number.Clear();
                    }

                    if (token == '(') 
                        operatorStack.Push(token);
                    else if (token == ')')
                    {
                        while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                            outputQueue.Enqueue(operatorStack.Pop().ToString());
                        operatorStack.Pop();
                    }
                    else
                    {
                        while (operatorStack.Count > 0 && precedence(token) <= precedence(operatorStack.Peek()))
                        {
                            outputQueue.Enqueue(operatorStack.Pop().ToString());
                        }
                        operatorStack.Push(token);

                    }
                }
            }

            if (number.Length != 0)
                outputQueue.Enqueue(number.ToString());

            while (operatorStack.Count > 0)
                outputQueue.Enqueue(operatorStack.Pop().ToString());

            return outputQueue;
        }
        private int precedence(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
                default: return -1;
            }
        }

        public bool isParanthesisBalanced(string expression)
        {
            int count = 0;

            foreach (char c in expression)
            {
                if (c == '(')
                    count++;
                else if (c == ')')
                    count--;

                if (count < 0) return false;
            }
            return count == 0;
        }

        public Queue<string> getPostfixAsQueue(string expression)
        {
            return convertInfixToPostFix(expression);
        }

        public string getPostfixAsString(string expression)
        {
            var queue = convertInfixToPostFix(expression);
            return string.Join(" ", queue);
        }


    }
}
