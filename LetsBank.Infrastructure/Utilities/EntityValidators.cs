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

			var isValid = Validator.TryValidateObject(user, context, results);

			if (!isValid)
			{
				foreach (var validationResult in results)
				{
					Console.WriteLine(validationResult.ErrorMessage);
				}
			}

			return isValid;
		}
	}
}
