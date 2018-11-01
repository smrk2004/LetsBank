using System.ComponentModel.DataAnnotations;

namespace LetsBank.Core.Entities
{
	public class User : BaseEntity
	{
		[Required]
		[EmailAddress]
		public string UserNameEmail { get; set; }

		[Required]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$")]   //Minimum eight and maximum 15 characters, at least one uppercase letter, one lowercase letter, one number and one special character
		public string Password { get; set; }
	}
}
