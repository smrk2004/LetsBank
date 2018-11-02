using LetsBank.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LetsBank.Infrastructure.Utilities
{
    public static class EntityValidators
    {
		public static bool UserIsValid(User user)
		{
			var context = new ValidationContext(user, serviceProvider: null, items: null);
			var results = new List<ValidationResult>();

			var isValid = Validator.TryValidateObject(user, context, results, validateAllProperties:true);

			if (!isValid)
			{
				Console.WriteLine();
				Console.WriteLine("-------------------------------------------------------------");
				Console.WriteLine("Entity failed validation :: User");
				Console.WriteLine("................................");
				foreach (var validationResult in results)
				{
					Console.WriteLine();
					Console.WriteLine(validationResult.ErrorMessage);
				}
				Console.WriteLine();
			}

			return isValid;
		}
		public static bool TransactionRecordIsValid(TransactionRecord r)
		{
			var context = new ValidationContext(r, serviceProvider: null, items: null);
			var results = new List<ValidationResult>();

			var isValid = Validator.TryValidateObject(r, context, results, validateAllProperties: true);

			if (!isValid)
			{
				Console.WriteLine();
				Console.WriteLine("-------------------------------------------------------------");
				Console.WriteLine("Entity failed validation :: TransactionRecord");
				Console.WriteLine(".............................................");
				foreach (var validationResult in results)
				{
					Console.WriteLine();
					Console.WriteLine(validationResult.ErrorMessage);
				}
				Console.WriteLine();
			}

			return isValid;
		}
		public static bool AccountIsValid(Account account)
		{
			var context = new ValidationContext(account, serviceProvider: null, items: null);
			var results = new List<ValidationResult>();

			var isValid = Validator.TryValidateObject(account, context, results, validateAllProperties: true);

			if (!isValid)
			{
				Console.WriteLine();
				Console.WriteLine("-------------------------------------------------------------");
				Console.WriteLine("Entity failed validation :: Account");
				Console.WriteLine("...................................");
				foreach (var validationResult in results)
				{
					Console.WriteLine();
					Console.WriteLine(validationResult.ErrorMessage);
				}
				Console.WriteLine();
			}

			return isValid;
		}
	}
}
