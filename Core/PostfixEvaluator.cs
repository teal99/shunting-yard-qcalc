namespace ShuntingYard.Core;

public static class PostfixEvaluator
{
    public static (double, string?) Evaluate(List<string> postfixTokens)
    {
        Stack<double> evaluationStack = new();

        foreach (string token in postfixTokens)
        {
            // if it's a number, push into computation stack
            if (double.TryParse(token, out double num))
            {
                evaluationStack.Push(num);
            }

            // if it's a unary minus symbol
            else if (token == "u-")
            {
                if (evaluationStack.Count < 1) return (double.NaN, "There is a hanging negative sign with no trailing number.");
                double operand = evaluationStack.Pop();
                evaluationStack.Push(-operand);
            }

            else if (OperatorPrecedence.IsOperator(token))
            {
                if (evaluationStack.Count < 2) return (double.NaN, $"There are not enough numbers to calculate the '{token}' operation.");

                double rightOperand = evaluationStack.Pop();
                double leftOperand = evaluationStack.Pop();

                double result = token switch
                {
                    "+" => leftOperand + rightOperand,
                    "-" => leftOperand - rightOperand,
                    "*" => leftOperand * rightOperand,
                    "/" => leftOperand / rightOperand,
                    "^" => Math.Pow(leftOperand, rightOperand),
                    _ => double.NaN
                };

                evaluationStack.Push(result);
            }
        }

        return evaluationStack.Count == 1 ? (evaluationStack.Pop(), null) : (double.NaN, "An error has occurred.");
    }
}