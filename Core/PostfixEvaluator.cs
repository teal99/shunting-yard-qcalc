namespace ShuntingYard.Core;

public static class Evaluator
{
    public static double Evaluate(List<string> postfixTokens)
    {
        Stack<double> evaluationStack = new();

        foreach (string token in postfixTokens)
        {
            // if it's a number, push into computation stack
            if (double.TryParse(token, out double num))
            {
                evaluationStack.Push(num);
            }

            else if (OperatorPrecedence.IsOperator(token))
            {
                double rightOperand = evaluationStack.Pop();
                double leftOperand = evaluationStack.Pop();
                double result = 0;

                switch (token)
                {
                    case "+": result = leftOperand + rightOperand; break;
                    case "-": result = leftOperand - rightOperand; break;
                    case "*": result = leftOperand * rightOperand; break;
                    case "/": result = leftOperand / rightOperand; break;
                    case "^": result = Math.Pow(leftOperand, rightOperand); break;
                }

                evaluationStack.Push(result);
            }
        }

        return evaluationStack.Pop();
    }
}