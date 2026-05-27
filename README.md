# Shunting-Yard Quick Calculator (`qcalc`)

An object-oriented mathematical expression evaluator built in C# using Edsger Dijkstra's famous **Shunting-Yard Algorithm**!

This engine safely parses standard infix math expressions, converts them to Postfix (Reverse Polish) notation using an operator stack machine, and evaluates the resulting numerical value.

---

## - How To Run

### 1. Interactive Shell Mode
Launches a continuous interactive command deck that stays open until you hit 'e':
```bash
dotnet run
```

### 2. Argument Mode
Bypasses the menu to immediately compute and log an inline expression:
```bash
dotnet run -- "(2 + 3) ^ 2 * 2"
```

### 3. Quick Windows Shortcut (`qcalc`)
Use the included `qcalc.bat` file to execute formulas instantly without quotes from any directory:
```powershell
.\qcalc 5 * -2
```

---

## Complete Feature Visual Trace

Here is exactly what you see on your monitor screen when running the engine through its entire lifecycle, from variables and exponents to negativity and custom safety checks:

```text
Welcome to the Shunting-Yard train station!

Enter an expression ('e' to exit): dv shield
Variable saved: shield = 0

Enter an expression ('e' to exit): dv potion 25
Variable saved: potion = 25

Enter an expression ('e' to exit): potion * 2 + shield
Result = 50

Enter an expression ('e' to exit): del potion
Variable 'potion' has been deleted.

Enter an expression ('e' to exit): (2 + 3) ^ 2 * 2
Result = 50

Enter an expression ('e' to exit): 5 * -2
Result = -10

Enter an expression ('e' to exit): 100 / 0
The program has caught errors: This equation contains division by zero.

Enter an expression ('e' to exit): ((3 + 5) * 2
The program has caught errors: This equation is missing 1 closing parenthesis.

Enter an expression ('e' to exit): e
Exiting program..
```
