using Calculator.mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Calculator.utils;

namespace Calculator.mvc.Controllers
{
    public class NoJavascriptController : Controller
    {
        public IActionResult Index(InputModel? model)
        {
            model.expression = "string.Empty";
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToExpression(string value, bool clear, bool remove, string expression, bool calculate)
        {

            string error = string.Empty;
            if (calculate != true)
            {
                if (!string.IsNullOrEmpty(value))
                {
                      if (CanAddValue(expression, value))
                        {
                            expression += value;
                        }
                }
                else if (clear)
                {
                    expression = "";
                } else if (remove)
                {
                    if (!string.IsNullOrEmpty(expression))
                        expression = expression.Remove(expression.Length - 1, 1);
                }
            }
            else
            {
                try
                {
                    InfixCalculator calculator = new InfixCalculator();
                    double result = calculator.calculate(expression);
                    expression = result.ToString();
                } catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            var model = new InputModel()
            {
                expression = expression,
                errorMessage = error,
            };

            return RedirectToAction("Index", model);
        }

        public bool CanAddValue(string expression, string value)
        {
            List<char> operandList = new List<char>("+-*/^.");
            if (!string.IsNullOrEmpty(expression))
            {
                if (value[0] == '.' && lastWasOperator(expression, value, operandList) == false)
                {
                    char[] delimiters = { ' ', '+', '-', '*', '/', '^' };
                    int lastIndex = expression.LastIndexOfAny(delimiters);

                    string number = string.Empty;
                    if (lastIndex != 1)
                        number = expression.Substring(lastIndex + 1);
                    if (number.Contains("."))
                        return false;
                    else
                        return true;

                }
                else if (lastWasOperator(expression, value, operandList))
                    return false;
                else return true;
            }
            else if (!operandList.Contains(value[0]))
                return true;
            return false;
        }

        public bool lastWasOperator(string expression ,string value, List<char> operandList)
        {
            if (operandList.Contains(value[0]) && !operandList.Contains(expression[expression.Length - 1]))
                return false;
            if (!operandList.Contains(value[0]))
                return false;

            return true;
        }

    }
}
