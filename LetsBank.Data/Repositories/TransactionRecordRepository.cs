using System;
using System.Collections.Generic;
using System.Linq;

using LetsBank.Core;
using LetsBank.Core.Entities;

namespace LetsBank.Data.Repositories
{
	public class TransactionRecordRepository : IRepository<TransactionRecord>
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
			r.Id = new Guid();
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
	}
}
