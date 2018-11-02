using System;
using LetsBank.Core.Entities;

namespace LetsBank.Core.Services
{
	public interface ILoginService
	{
		Guid GetLoggedInUserId();
		bool LoginUser(string userNameEmail, string password);
		bool LoginUser(User user);
		void LoginUserById(Guid userId);
		void LogoutUser();
		Guid RegisterUser(string userNameEmail, string password);
		Guid RegisterUser(User user);
		bool UserLoggedIn();
		bool UserLoggedIn(Guid userId);
	}
}