using System;
using System.ComponentModel.DataAnnotations;

namespace LetsBank.Core.Entities
{
	public class Account : BaseEntity
	{
		public int Balance { get; set; } = 0;

		[Required]
		public Guid UserId { get; set; }
	}
}
