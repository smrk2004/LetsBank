using System;
using System.Linq;

using LetsBank.Core.Services;
using LetsBank.Infrastructure.Services;

namespace LetsBank.ConsoleApp
{
	/// <summary>
	///			Console App - Client/Interface to the LetsBank - Banking Ledger solution
	/// </summary>
	class Program
	{
		// NOTE: NOT using Dependency Injection here, since we are NOT using a DB + are using service' in-memory state, both from Web & ConsoleApp interfaces; also due to same reason state will NOT persist between console & webapp sessions + will be zero'd when app is restarted.
		private static readonly ILoginService loginService = new LoginService();
		private static readonly IAccountManagementService accountManagementService = new AccountManagementService();

		static void Main(string[] args)
		{
			Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine("Welcome to LetsBank!");
			Console.WriteLine("~~~~~~~~~~~~~~~~~~~~\n");

			var selection = string.Empty;

			do
			{
				PrintMenu();
				selection = Console.ReadLine();
				Console.WriteLine();

				switch (selection)
				{
					case "1":
						// Create a new account
						if (loginService.UserLoggedIn())
						{
							Logout();
						}
						CreateNewAccount();
						break;

					case "2":
						// Login
						if (loginService.UserLoggedIn())
						{
							Logout();
						}
						Login();
						break;

					case "3":
						if (loginService.UserLoggedIn())
						{
							// Record a deposit
							RecordDeposit();
							break;
						}
						continue;

					case "4":
						if (loginService.UserLoggedIn())
						{
							// Record a withdrawal
							RecordWithdrawal();
							break;
						}
						continue;

					case "5":
						if (loginService.UserLoggedIn())
						{
							// Check balance
							PrintBalance();
							break;
						}
						continue;

					case "6":
						if (loginService.UserLoggedIn())
						{
							// See transaction history (get last 100)
							ShowTransactionHistory();
							break;
						}
						continue;

					case "7":
						// Log out
						Logout();
						break;

					case "8":
						// EXIT Bank
						break;

					default:
						Console.WriteLine("Invalid Operation selected! Please Try again...");
						selection = string.Empty;
						break;
				}

				Console.WriteLine();
				Console.WriteLine("-------------------------------------------------------------");
				Console.WriteLine();

			} while (!selection.Equals("8"));

			Console.WriteLine("Thank you for choosing LetsBank!");

			Console.ReadKey();
		}

		private static void PrintMenu()
		{
			Console.WriteLine("----");
			Console.WriteLine("MENU");
			Console.WriteLine("----");
			Console.WriteLine();

			Console.WriteLine("Select Operation(NUMERIC [1 to 8])::\n");

			Console.WriteLine("1. Create a new account			// will Automatically-Log out, if previously Logged in");

			if (!loginService.UserLoggedIn())
			{
				Console.WriteLine("2. Login");
				Console.WriteLine("3. Record a deposit				-> [UNAVAILABLE]");
				Console.WriteLine("4. Record a withdrawal				-> [UNAVAILABLE]");
				Console.WriteLine("5. Check balance				-> [UNAVAILABLE]");
				Console.WriteLine("6. See transaction history			-> [UNAVAILABLE]");
				Console.WriteLine("7. Log out					-> [UNAVAILABLE]");
			}
			else
			{
				Console.WriteLine("2. Login					-> [UNAVAILABLE]");
				Console.WriteLine("3. Record a deposit");
				Console.WriteLine("4. Record a withdrawal");
				Console.WriteLine("5. Check balance");
				Console.WriteLine("6. See transaction history");
				Console.WriteLine("7. Log out");
			}

			Console.WriteLine("8. EXIT Bank");
			Console.WriteLine();
		}

		private static void CreateNewAccount()
		{
			Console.WriteLine("Please enter Username[MUST be valid Email]:\n");
			var username = Console.ReadLine();
			Console.WriteLine();

			Console.WriteLine("Please enter Password:");
			Console.WriteLine("[");
			Console.WriteLine("Minimum eight and maximum 15 characters			");
			Console.WriteLine("		At least one uppercase letter,				");
			Console.WriteLine("				 one lowercase letter,				");
			Console.WriteLine("				 one number				&			");
			Console.WriteLine("				 one special character @ $ ! % * ? &");
			Console.WriteLine("]");
			Console.WriteLine();

			var password = Console.ReadLine();
			Console.WriteLine();

			var userId = loginService.RegisterUser(username, password);

			if (accountManagementService.CreateAccount(userId))
			{
				Console.WriteLine("Account Creation Successful!");
			}
			else
			{
				Console.WriteLine("Account Creation Failed [/Could already exist]!");
			}
		}

		private static void Login()
		{
			Console.WriteLine("Please enter Username[MUST be valid Email]:\n");
			var username = Console.ReadLine();
			Console.WriteLine();

			Console.WriteLine("Please enter Password:\n");
			var password = Console.ReadLine();
			Console.WriteLine();

			if (loginService.LoginUser(username, password))
			{
				Console.WriteLine("Login Successful!");
			}
			else
			{
				Console.WriteLine("Login Failed!");
			}
		}

		private static void RecordDeposit()
		{
			Console.WriteLine("Enter Deposit Amount:\n");
			var amountString = Console.ReadLine();
			Console.WriteLine();

			var amount = 0;
			int.TryParse(amountString, out amount);
			if (amount == 0)
			{
				Console.WriteLine("Invalid Deposit Amount entered!");
			}
			else
			{
				var _loggedInUserId = loginService.GetLoggedInUserId();
				bool result = accountManagementService.RecordDeposit(accountManagementService.GetAccountByUserId(_loggedInUserId), _loggedInUserId, amount);

				if (!result)
				{
					Console.WriteLine("Invalid Deposit attempted!");
				}
				else
				{
					Console.WriteLine("Deposit SUCCESS!\n");

					Console.WriteLine("Final Balance = " + accountManagementService.CheckBalance(_loggedInUserId));
				}
			}
		}

		private static void RecordWithdrawal()
		{
			Console.WriteLine("Enter Withdrawal Amount:\n");
			var amountString = Console.ReadLine();
			Console.WriteLine();

			var amount = 0;
			int.TryParse(amountString, out amount);
			if (amount == 0)
			{
				Console.WriteLine("Invalid Withdrawal Amount entered!");
			}
			else
			{
				var _loggedInUserId = loginService.GetLoggedInUserId();
				bool result = accountManagementService.RecordWithdrawal(accountManagementService.GetAccountByUserId(_loggedInUserId), _loggedInUserId, amount);

				if (!result)
				{
					Console.WriteLine("Invalid Withdrawal / Overdraft attempted!");
				}
				else
				{
					Console.WriteLine("Withdrawal SUCCESS!\n");

					Console.WriteLine("Final Balance = " + accountManagementService.CheckBalance(_loggedInUserId));
				}
			}
		}

		private static void PrintBalance()
		{
			Console.WriteLine("Current Balance = " + accountManagementService.CheckBalance(loginService.GetLoggedInUserId()));
		}

		private static void ShowTransactionHistory()
		{
			var transactionHistory = accountManagementService.GetTransactionHistory(loginService.GetLoggedInUserId());
			if (transactionHistory.Any())
			{
				Console.WriteLine("Transaction History");
				Console.WriteLine();

				foreach (var rec in transactionHistory)
				{
					Console.WriteLine("[ " + rec.Type.ToString() + " ] :: [Amount = " + rec.Amount + "] :: [Final-Balance = " + rec.FinalBalance + "] on " + rec.Date);
				}
			}
			else
			{
				Console.WriteLine("EMPTY Transaction History [/No Transactions Yet]!");
				Console.WriteLine();
			}
		}

		private static void Logout()
		{
			loginService.LogoutUser();

			Console.WriteLine("You have been Logged out of existing session!\n");
		}
    }
}
