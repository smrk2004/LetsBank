using System;
using System.Collections.Generic;

using LetsBank.Core.Entities;

namespace LetsBank.Core.Repositories
{
	public interface ITransactionRecordRepository : IRepository<TransactionRecord>
	{
		IEnumerable<TransactionRecord> TransactionHistoryByAccountId(Guid id, int recentRecsCountToFetch);
	}
}