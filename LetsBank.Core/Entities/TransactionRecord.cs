using LetsBank.Core.Enums;
using System;

namespace LetsBank.Core.Entities
{
	public class TransactionRecord : BaseEntity
	{
		public Guid AccountId { get; set; }
		public TransactionType Type { get; set; } = TransactionType.Deposit;
		public int Amount { get; set; } = 0;
		public int FinalBalance { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
	}
}
