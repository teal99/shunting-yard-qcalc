namespace ShuntingYard.Core;

public static class OperatorPrecedence
{
    private static readonly Dictionary<string, int> Weight = new()
    {
        { "+", 1 }, { "-", 1 },
        { "*", 2 }, { "/", 2 },
        { "^", 3 },
        { "(", 0 }, { ")", 0 }
    };

    public static bool IsOperator(string token) => Weight.ContainsKey(token);
    public static int GetWeight(string op) => Weight.TryGetValue(op, out int weight) ? weight : 0;
}