using System;
using System.Security.Cryptography;
using System.Text;

namespace LetsBank.Infrastructure.Utilities
{
	public class StringUtils
	{
		public static Guid StringToGUID(string value)
		{
			// Create a new instance of the MD5CryptoServiceProvider object.
			MD5 md5Hasher = MD5.Create();
			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
			return new Guid(data);
		}
	}
}
