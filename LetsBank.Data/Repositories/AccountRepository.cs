using System;
using System.Collections.Generic;
using System.Linq;

using LetsBank.Core.Entities;
using LetsBank.Core.Repositories;

namespace LetsBank.Data.Repositories
{
	public class AccountRepository : IAccountRepository
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
			user.Id = Guid.NewGuid();
			ApplicationDbContext.Accounts.Add(user);

			return user;
		}

		public void Update(Account updated)
		{
			var old = ApplicationDbContext.Accounts
											.FirstOrDefault(acc => acc.Id == updated.Id);
			if (old != null)
			{
				old.Id		= updated.Id;
				old.UserId	= updated.UserId;

				old.Balance = updated.Balance;  // This is the only property we actually care about, from users' point of view!
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

		public Account FindByUserId(Guid id)
		{		
			return ApplicationDbContext.Accounts
										.FirstOrDefault(acc => acc.UserId == id);
		}
	}
}
