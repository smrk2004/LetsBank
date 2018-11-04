using System;

using LetsBank.Core.Enums;

namespace LetsBank.Core.Models
{
	public class TxnRecordModel
	{
		public decimal Amount { get; set; }

		public String Type { get; set; }

		public decimal InitialBalance { get; set; }

		public decimal FinalBalance { get; set; }

		public DateTime Date { get; set; }
	}
}
