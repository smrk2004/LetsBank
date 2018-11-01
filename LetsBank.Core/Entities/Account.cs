using System;

namespace LetsBank.Core.Entities
{
	public class Account : BaseEntity
	{
		public int Balance { get; set; } = 0;
		public int UserId { get; set; }
	}
}
