using LetsBank.Core.Entities;
using System.Collections.Generic;

namespace LetsBank.Data
{
	public class ApplicationDbContext
    {
		public static HashSet<Account> Accounts { get; set; }  = new HashSet<Account>();
		public static HashSet<User> Users { get; set; } = new HashSet<User>();
		public static HashSet<TransactionRecord> TransactionRecords { get; set; } = new HashSet<TransactionRecord>();
	}
}
