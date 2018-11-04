using System;
using System.ComponentModel.DataAnnotations;

using LetsBank.Core.Enums;

namespace LetsBank.Core.Entities
{
	public class TransactionRecord : BaseEntity
	{
		[Required]
		public Guid AccountId { get; set; }

		[Required]
		public decimal Amount { get; set; }

		public TransactionType Type { get; set; } = TransactionType.Deposit;

		[Required]
		public decimal InitialBalance { get; set; }

		[Required]
		public decimal FinalBalance { get; set; }

		public DateTime Date { get; set; } = DateTime.Now;
	}
}
