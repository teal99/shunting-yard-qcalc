# Shunting-Yard Quick Calculator (`qcalc`)

An object-oriented mathematical expression evaluator built in C# using Edsger Dijkstra's famous **Shunting-Yard Algorithm**!

This engine safely parses standard infix math strings, rearranges them into Postfix notation (Reverse Polish Notation) using an operator stack machine, and evaluates the final numerical result.

## - Key Features
* **OOP Architecture**: Includes a custom self-solving `Equation` instance wrapper.
* **Error handling**: Cascading error-handling framework tracking empty inputs, division by zero, and unclosed brackets with context-aware pluralized feedback (with grammar)!
* **Unary Minus Semi-Support**: Contextual look-back array tokenization to securely bind negative value sign prefixes (e.g., `5 * -2`).
* **High-Priority Operations**: Full mathematical precedence hierarchy support handling parentheses `()` and exponents `^` (`Math.Pow`).
* **Dual Execution Modes**: Seamlessly switches between a global active console shell loop and instant single-line argument processing!

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

---

## - Example Terminal Output

Here is exactly what you see on your screen when testing the interactive loop, command line parameters, or the custom safety validation routines:

```text
Welcome to the Shunting-Yard train station!

Enter an expression ('e' to exit): (2 + 3) ^ 2 * 2
Result = 50

Enter an expression ('e' to exit): 5 * -2
Result = -10

Enter an expression ('e' to exit): 100 / 0
The program has caught errors: This equation contains division by zero.

Enter an expression ('e' to exit): ((3 + 5) * 2
The program has caught errors: This equation is missing 1 closing parenthesis

Enter an expression ('e' to exit): e
Exiting program..
```
