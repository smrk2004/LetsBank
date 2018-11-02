using LetsBank.Core.Entities;

namespace LetsBank.Core.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		User FindByCredentials(User user);
	}
}