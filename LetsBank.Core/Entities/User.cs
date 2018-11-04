using System.ComponentModel.DataAnnotations;

namespace LetsBank.Core.Entities
{
	public class User : BaseEntity
	{
		[Required]
		[EmailAddress]
		public string UserNameEmail { get; set; }

		/// <summary>
		///			Minimum eight characters
		///			At least one uppercase letter,
		///					 one lowercase letter,
		///					 one number				&
		///					 one special character @ $ ! % * ? &
		/// </summary>
		[Required]
		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight and maximum 15 characters, at least one uppercase letter, one lowercase letter, one number & one special character @ $ ! % * ? &")]
		public string Password { get; set; }
	}
}
