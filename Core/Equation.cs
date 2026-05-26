using System.Text;

namespace ShuntingYard.Core;

public class Equation
{
    public string RawText { get; private set; }
    public List<string> Tokens { get; private set; }

    public Equation(string rawInput)
    {
        RawText = rawInput ?? "";
        Tokens = Split();
    }

    public (double, string) Evaluate()
    {
        var (Success, Error) = IsValid();
        if (!Success) return (double.NaN, Error);

        List<string> postfixResult = Parser.ConvertToPostfix(this);
        return (Evaluator.Evaluate(postfixResult), "No errors!");
    }

    public (bool, string) IsValid()
    {
        if (string.IsNullOrWhiteSpace(RawText)) return (false, "This equation is empty.");

        string zeroSpacesRemoved = RawText.Replace(" ", "");
        if (zeroSpacesRemoved.Contains("/0") && !zeroSpacesRemoved.Contains("/0."))
        {
            return (false, "This equation contains division by zero.");
        }

        int parenBalance = 0;
        foreach (char c in zeroSpacesRemoved)
        {
            if (c == '(') parenBalance++;
            if (c == ')') parenBalance--;

            if (parenBalance < 0) return (false, $"This equation is missing {Math.Abs(parenBalance)} opening {(Math.Abs(parenBalance) == 1 ? "parenthesis" : "parentheses")}");
        }

        if (parenBalance == 0)
        {
            return (true, "");
        }

        return (false, $"This equation is missing {parenBalance} closing {(parenBalance == 1 ? "parenthesis" : "parentheses")}");
    }

    private List<string> Split()
    {
        string clean = RawText.Replace(" ", "");
        List<string> tokensList = new List<string>();
        StringBuilder currentNumber = new StringBuilder();
        StringBuilder currentWord = new StringBuilder();

        for (int i = 0; i < clean.Length; i++)
        {
            char c = clean[i];

            // If we hit a symbol or number, flush any building variable word first
            if (!char.IsLetter(c) && currentWord.Length > 0)
            {
                FlushWord(currentWord.ToString(), tokensList);
                currentWord.Clear();
            }

            // 1. Unary Minus Check
            if (c == '-' && currentNumber.Length == 0 && 
                (tokensList.Count == 0 || OperatorPrecedence.IsOperator(tokensList[tokensList.Count - 1]) || tokensList[tokensList.Count - 1] == "("))
            {
                currentNumber.Append(c);
                continue;
            }

            // 2. Tokenizing Logic Branches
            if (char.IsDigit(c) || c == '.')
            {
                currentNumber.Append(c);
            }
            else if (char.IsLetter(c))
            {
                currentWord.Append(c);
            }
            else
            {
                if (currentNumber.Length > 0)
                {
                    tokensList.Add(currentNumber.ToString());
                    currentNumber.Clear();
                }
                tokensList.Add(c.ToString());
            }
        }
        
        if (currentWord.Length > 0) FlushWord(currentWord.ToString(), tokensList);
        if (currentNumber.Length > 0) tokensList.Add(currentNumber.ToString());

        return tokensList;
    }

    // Helper method to look up a variable and replace it with its double value
    private static void FlushWord(string word, List<string> tokensList)
    {
        if (VariableRegistry.TryGet(word, out double val))
        {
            tokensList.Add(val.ToString());
        }
        else
        {
            // If the user uses a variable name that hasn't been defined yet, default it to 0
            tokensList.Add("0");
        }
    }
}