using System;

using LetsBank.Core.Entities;
using LetsBank.Core.Repositories;
using LetsBank.Core.Services;
using LetsBank.Data.Repositories;
using LetsBank.Infrastructure.Utilities;

namespace LetsBank.Infrastructure.Services
{
	public class LoginService : ILoginService
	{
		// NOTE: NOT using Dependency Injection here, since we are NOT using a DB + are using service' in-memory state, both from Web & ConsoleApp interfaces; also due to same reason state will NOT persist between console & webapp sessions + will be zero'd when app is restarted.
		private readonly IUserRepository userRepo = new UserRepository();
		private Guid loggedInUserId = Guid.Empty;

		public Guid RegisterUser(User user)
		{
			if (EntityValidators.UserIsValid(user) && userRepo.FindByCredentials(user) == null)
			{
				return userRepo.Add(user).Id;
			}

			return user.Id;
		}

		public Guid RegisterUser(string userNameEmail, string password)
		{
			// reconstruct model
			var user = new User	{
									UserNameEmail	= userNameEmail,
									Password		= password
								};

			return RegisterUser(user);
		}

		public void LoginUserById(Guid userId)
		{
			loggedInUserId = userId; // set logged in user!
		}

		public bool LoginUser(User user)
		{
			if (EntityValidators.UserIsValid(user))
			{
				var userId = userRepo.FindByCredentials(user)?.Id ?? Guid.Empty;
				LoginUserById(userId);

				return true;
			}

			return false;
		}

		public bool LoginUser(string userNameEmail, string password)
		{
			// reconstruct model
			var user = new User	{
									UserNameEmail	= userNameEmail,
									Password		= password
								};
			return LoginUser(user);
		}

		public void LogoutUser()
		{
			loggedInUserId = Guid.Empty; // clear logged in user!
		}

		public Guid GetLoggedInUserId()
		{
			return loggedInUserId;
		}

		public bool UserLoggedIn(Guid userId)
		{
			return loggedInUserId == userId && UserLoggedIn();
		}

		public bool UserLoggedIn()
		{
			return loggedInUserId != Guid.Empty;
		}
	}
}
