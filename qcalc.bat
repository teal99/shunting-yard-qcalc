@echo off
rem -- Get the absolute directory path where this script lives
set SCRIPT=DIR%~dp0

rem -- Execute the C# file silently via dotnet, passing all the arguemnts right through
dotnet run --project "%SCRIPT_DIR%shunting-yard-qcalc.csproj" -- %*