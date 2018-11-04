using System;
using System.Collections.Generic;
using LetsBank.Core.Entities;
using LetsBank.Core.Enums;
using LetsBank.Core.Repositories;
using LetsBank.Core.Services;
using LetsBank.Data.Repositories;
using LetsBank.Infrastructure.Utilities;

namespace LetsBank.Infrastructure.Services
{
	public class AccountManagementService : IAccountManagementService
	{
		// NOTE: NOT using Dependency Injection here, since we are NOT using a DB + are using service' in-memory state, both from Web & ConsoleApp interfaces; also due to same reason state will NOT persist between console & webapp sessions + will be zero'd when app is restarted.
		private readonly IAccountRepository				accountRepo				= new AccountRepository();
		private readonly ITransactionRecordRepository	transactionRecordRepo	= new TransactionRecordRepository();

		public bool CreateAccount(Account account)
		{
			if (EntityValidators.AccountIsValid(account) && account.UserId != Guid.Empty)
			{
				accountRepo.Add(account);
				return true;
			}

			return false;
		}

		public bool CreateAccount(Guid userId)
		{
			var account = new Account { UserId = userId };
			if (EntityValidators.AccountIsValid(account) && account.UserId != Guid.Empty)
			{
				accountRepo.Add(account);
				return true;
			}

			return false;
		}

		public Account GetAccountByUserId(Guid id)
		{
			return accountRepo.FindByUserId(id);
		}

		public bool RecordDeposit(Account account, Guid userId, decimal amount)
		{
			// Check if deposit should be permitted
			if (!EntityValidators.AccountIsValid(account) || account.UserId == Guid.Empty || account.UserId != userId || amount <= 0.0m)
			{
				return false;
			}

			// Prepare transactionRec + account
			var transactionRec = new TransactionRecord
									{
										AccountId		= account.Id,
										Amount			= amount,
										Type			= TransactionType.Deposit,
										InitialBalance	= account.Balance,
										FinalBalance	= account.Balance + amount,
									};

			// Update account data store
				account.Balance += amount;

			// Update history data store
				transactionRecordRepo.Add(transactionRec);

			return true;
		}

		public bool RecordWithdrawal(Account account, Guid userId, decimal amount)
		{
			// Check if withdrawal should be permitted
			if (!EntityValidators.AccountIsValid(account) || account.UserId == Guid.Empty || account.UserId != userId || account.Balance < amount || amount <= 0.0m)
			{
				return false;
			}

			// Prepare transactionRec + account
			var transactionRec = new TransactionRecord
									{
										AccountId		= account.Id,
										Amount			= amount,
										Type			= TransactionType.Withdrawal,
										InitialBalance	= account.Balance,
										FinalBalance	= account.Balance - amount,
									};

			// Update account data store
				account.Balance -= amount;

			// Update history data store
				transactionRecordRepo.Add(transactionRec);

			return true;
		}

		public decimal CheckBalance(Guid userId)
		{
			return GetAccountByUserId(userId).Balance;
		}

		public IEnumerable<TransactionRecord> GetTransactionHistory(Guid userId, int recentRecsCountToFetch)
		{
			var account = GetAccountByUserId(userId);
			return transactionRecordRepo.TransactionHistoryByAccountId(account.Id, recentRecsCountToFetch);
		}
	}
}