using System;
using System.Collections.Generic;
using System.Linq;

using LetsBank.Core.Entities;
using LetsBank.Core.Repositories;

namespace LetsBank.Data.Repositories
{
	public class TransactionRecordRepository : ITransactionRecordRepository
	{
		public TransactionRecord Get(Guid id)
		{
			return ApplicationDbContext.TransactionRecords
										.FirstOrDefault(r => r.Id == id);
		}

		public IEnumerable<TransactionRecord> GetAll()
		{
			return ApplicationDbContext.TransactionRecords;
		}

		public TransactionRecord Add(TransactionRecord r)
		{
			r.Id = Guid.NewGuid();
			ApplicationDbContext.TransactionRecords.Add(r);

			return r;
		}

		public void Update(TransactionRecord updated)
		{
			var old = ApplicationDbContext.TransactionRecords
											.FirstOrDefault(r => r.Id == updated.Id);
			if (old != null)
			{
				old = updated;
			}
		}

		public void Delete(TransactionRecord existing)
		{
			var toDelete = ApplicationDbContext.TransactionRecords
												.FirstOrDefault(r => r.Id == existing.Id);
			if (toDelete != null)
			{
				ApplicationDbContext.TransactionRecords.Remove(toDelete);
			}
		}

		public IEnumerable<TransactionRecord> TransactionHistoryByAccountId(Guid id, int recentRecsCountToFetch)
		{
			return ApplicationDbContext.TransactionRecords
										.Where(r => r.AccountId == id)
										.OrderByDescending(r => r.Date)
										.Take(recentRecsCountToFetch);
		}
	}
}
