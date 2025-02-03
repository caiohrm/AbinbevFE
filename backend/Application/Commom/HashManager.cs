using System;
using CrossCutting.Models;

namespace Application.Commom
{
	public static class HashManager
	{
		public static string GetStringHash(string text)
        {
			return BCrypt.Net.BCrypt.HashPassword(text);
        }

		public static bool ValidateHash(string hash,string password)
		{
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
	}
}

