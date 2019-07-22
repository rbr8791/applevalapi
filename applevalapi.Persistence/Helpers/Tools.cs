using System;
using System.Collections.Generic;
using System.Text;

namespace applevalApi.Persistence.Helpers
{
    public static class Tools
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("Password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value can not be empty or Whitespace only string.", "Password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        } // End of CreatePasswordHash

    }
}
