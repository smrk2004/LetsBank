using System;
using System.ComponentModel.DataAnnotations;

namespace LetsBank.Core.Entities
{
	public class Account : BaseEntity
	{
		public decimal Balance { get; set; } = 0.0m;

		[Required]
		public Guid UserId { get; set; }
	}
}
