using System;
using System.Collections.Generic;
using System.Linq;

using LetsBank.Core.Entities;
using LetsBank.Core.Repositories;

namespace LetsBank.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		public User Get(Guid id)
		{
			return ApplicationDbContext.Users
										.FirstOrDefault(user => user.Id == id);
		}

		public IEnumerable<User> GetAll()
		{
			return ApplicationDbContext.Users;
		}

		public User Add(User user)
		{
			user.Id = Guid.NewGuid();
			ApplicationDbContext.Users.Add(user);

			return user;
		}

		public void Update(User updated)
		{
			var old = ApplicationDbContext.Users
											.FirstOrDefault(user => user.Id == updated.Id);
			if (old != null)
			{
				old = updated;
			}
		}

		public void Delete(User existing)
		{
			var toDelete = ApplicationDbContext.Users
												.FirstOrDefault(user => user.Id == existing.Id);
			if (toDelete != null)
			{
				ApplicationDbContext.Users.Remove(toDelete);
			}
		}

		public User FindByCredentials(User user)
		{
			return ApplicationDbContext.Users
										.FirstOrDefault(u =>	u.UserNameEmail == user.UserNameEmail);
		}
	}
}
