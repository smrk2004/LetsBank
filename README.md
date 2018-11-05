# LetsBank
Banking Ledger implementation - WebApp + ConsoleClient / C#, jQuery

Implements
1) Command Line Interface [LetsBank.ConsoleApp/Program.cs] - to access - Set LetsBank.ConsoleApp as 'Startup Project' [in Visual Studio] & Run

2) Web Interface [LetsBank.Web] - to access - Set LetsBank.Web as 'Startup Project' [in Visual Studio] & Run

_NOTE_: Web Interface uses Aspnet Identity to support Login/Logout - please run relevant initial database migrations prior to running the app from:

a) Visual Studio's Package Manager Console, the command: Update-Database

(OR)

b) Command line inside the project's LetsBank.Web folder, the command: dotnet ef database update
