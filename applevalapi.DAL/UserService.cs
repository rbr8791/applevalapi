using System.Collections.Generic;
using System.Linq;
using applevalApi.Entities;
using applevalApi.Persistence;
using Microsoft.EntityFrameworkCore;
using applevalApi.DAL.Interfaces;
using System;
namespace applevalApi.DAL
{
    public class UserService : IUserService
    {
        //private ApplDbContext _dwContext;

        private ApplDbContext _context;

        public UserService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext

        public User FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(user => user.Id == id);
        }

        private IEnumerable<User> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.Users
                .Include(role => role.Role);
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.FirstOrDefault(x => x.Username == username);

            // Check if the username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // Authentication successful
            return user;

        } // End Authenticate

        public IEnumerable<User> GetAll()
        {
            //return _context.Users;
            return BaseQuery();
               
        } // End IEnumerable

        public User GetById(int id)
        {
            //return _context.Users.Find(id);
            return BaseQuery()
               .FirstOrDefault(user => user.Id == id);
        } // End GetById

        public User Create(User user, string password)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(password))
                throw new System.ApplicationException("Password is required to perfom this action");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new System.ApplicationException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;

        } // End Create


        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);
            if (user == null)
                throw new System.ApplicationException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new System.ApplicationException("UserName " + userParam.Username + " is already taken");
            }


            // Update user properties

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            // Update password if it was entered

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

            }

            _context.Users.Update(user);
            _context.SaveChanges();
        } // End Update


        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        } // End Delete


        // Private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("Password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value can not be empty or Whitespace only string.", "Password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        } // End of CreatePasswordHash


        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value can not be empty or Whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid lenght of password has (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid lenght of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        } // end VerifyPasswordHash
    }
}