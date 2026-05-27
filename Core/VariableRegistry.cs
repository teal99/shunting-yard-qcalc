namespace ShuntingYard.Core;

public static class VariableRegistry
{
    private static readonly Dictionary<string, double> Storage = new(StringComparer.OrdinalIgnoreCase);

    public static void Set(string name, double val) => Storage[name] = val;
    public static bool TryGet(string name, out double value) => Storage.TryGetValue(name, out value);
    public static bool Delete(string name) => Storage.Remove(name);
    public static bool Contains(string name) => Storage.ContainsKey(name);
}