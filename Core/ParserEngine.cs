namespace ShuntingYard.Core;

public static class Parser
{
    public static List<string> ConvertToPostfix(Equation equation)
    {
        List<string> outputQueue = new();
        Stack<string> operatorStack = new();

        foreach (string token in equation.Tokens)
        {
            // if it's a number, place to output queue immediately
            if (double.TryParse(token, out _))
            {
                outputQueue.Add(token);
            }

            // if it's an opening parenthesis
            else if (token == "(")
            {
                operatorStack.Push(token);
            }

            // if it's a closing parenthesis
            else if (token == ")")
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    outputQueue.Add(operatorStack.Pop());
                }

                if (operatorStack.Count > 0)
                {
                    operatorStack.Pop(); // discard '('
                }
            }

            // if it's an operator, check the stack priority
            else if (OperatorPrecedence.IsOperator(token))
            {
                while (operatorStack.Count > 0 && 
                       operatorStack.Peek() != "(" &&
                       (OperatorPrecedence.GetWeight(operatorStack.Peek()) > OperatorPrecedence.GetWeight(token) ||
                       (OperatorPrecedence.GetWeight(operatorStack.Peek()) == OperatorPrecedence.GetWeight(token) && token != "u-")))
                {
                    outputQueue.Add(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
        }

        // flush remaining operators out of the operator stack into the output queue
        while (operatorStack.Count > 0)
        {
            outputQueue.Add(operatorStack.Pop());
        }

        return outputQueue;
    }
}