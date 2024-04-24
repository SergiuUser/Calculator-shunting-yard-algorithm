using Calculator.utils;
 
InfixCalculator calculator = new InfixCalculator();
InfixToPostfixConvertor convertor = new InfixToPostfixConvertor();

while (true)
{
    Console.WriteLine("Please type the expression below");
    string expresion = Console.ReadLine();
    try
    {
        Console.WriteLine("Infix to postfix: " + convertor.getPostfixAsString(expresion));
        Console.WriteLine("Result of calculation: " + calculator.calculate(expresion));
    }
    catch (Exception ex)
    {
        showErrorAndInput(ex.Message, expresion);
    }

    Console.WriteLine("Restart the program? (y/n)");
    string restart = Console.ReadLine();

    if (!restart.ToLower().Equals("y")) break;
    else Console.Clear();
}

void showErrorAndInput(string message, string input)
{
    Console.Clear();
    Console.WriteLine("ERROR: " + message);
    Console.WriteLine("Input: " + input);
}
