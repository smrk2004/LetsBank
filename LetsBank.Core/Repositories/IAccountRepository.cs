using System;
using LetsBank.Core.Entities;

namespace LetsBank.Core.Repositories
{
	public interface IAccountRepository: IRepository<Account>
    {
		Account FindByUserId(Guid id);
	}
}