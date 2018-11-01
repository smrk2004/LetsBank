using System;
using System.Collections.Generic;
using System.Linq;

using LetsBank.Core;
using LetsBank.Core.Entities;

namespace LetsBank.Data.Repositories
{
	public class AccountRepository : IRepository<Account>
	{
		public Account Get(Guid id)
		{
			return ApplicationDbContext.Accounts
										.FirstOrDefault(acc => acc.Id == id);
		}

		public IEnumerable<Account> GetAll()
		{
			return ApplicationDbContext.Accounts;
		}

		public Account Add(Account user)
		{
			user.Id = new Guid();
			ApplicationDbContext.Accounts.Add(user);

			return user;
		}

		public void Update(Account updated)
		{
			var old = ApplicationDbContext.Accounts
											.FirstOrDefault(acc => acc.Id == updated.Id);
			if (old != null)
			{
				old = updated;
			}
		}

		public void Delete(Account existing)
		{
			var toDelete = ApplicationDbContext.Accounts
												.FirstOrDefault(acc => acc.Id == existing.Id);
			if (toDelete != null)
			{
				ApplicationDbContext.Accounts.Remove(toDelete);
			}
		}
	}
}
