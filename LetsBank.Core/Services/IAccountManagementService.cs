using System;
using System.Collections.Generic;
using LetsBank.Core.Entities;

namespace LetsBank.Core.Services
{
	public interface IAccountManagementService
	{
		int CheckBalance(Guid userId);
		bool CreateAccount(Account account);
		bool CreateAccount(Guid userId);
		Account GetAccountByUserId(Guid id);
		IEnumerable<TransactionRecord> GetTransactionHistory(Guid userId, int recentRecsCountToFetch = 100);
		bool RecordDeposit(Account account, Guid userId, int amount);
		bool RecordWithdrawal(Account account, Guid userId, int amount);
	}
}