using System.Collections.Generic;
using System.Linq;
using applevalApi.Entities;
using applevalApi.Persistence;
using Microsoft.EntityFrameworkCore;
using applevalApi.DAL.Interfaces;
using System;

using System.Threading.Tasks;


namespace applevalApi.DAL 
{
    public class ActiveTokenService : IActiveTokenService
    {
        private readonly ApplDbContext _context;


        public ActiveToken FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(activeToken => activeToken.Id == id);
        }

        private IEnumerable<ActiveToken> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.ActiveTokens;
                //.Include(book => book.BookCharacter)
                //.ThenInclude(bookCharacter => bookCharacter.Character)
                //.Include(activeToken => .BookSeries)
                //.ThenInclude(bookSeries => bookSeries.Series
                //);
        }

        public ActiveTokenService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext


        public IEnumerable<ActiveToken> GetAll()
        {
            //return _context.ActiveTokens;
            return BaseQuery();
        } // End IEnumerable

        public ActiveToken GetById(int id)
        {
            //return _context.ActiveTokens.Find(id);
            return BaseQuery()
               .FirstOrDefault(activeToken => activeToken.Id == id);
        } // End GetById

        public List<ActiveToken> GetTopOneActiveTokenBySourceIdentifier(string sourceIdentifier)
        {

            //DbContext.Database.CommandTimeout = 180;
            //_context.Database.tim
            return _context.ActiveTokens.Take(1).Where(c => c.SourceIdentifier == sourceIdentifier)
                .OrderByDescending(d => d.Id)
                .ToList();

        } // End GetById

        public ActiveToken Create(ActiveToken activeToken, string userName, string currentToken, string SourceIdentifier)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(userName))
                throw new ApplicationException("The username for the current active token can not be null or empty");
            if (string.IsNullOrWhiteSpace(currentToken))
                throw new ApplicationException("The current token  can not be null or empty");

            //if (_context.Users.Any(x => x.Username == user.Username))
            //    throw new AppException("Username \"" + user.Username + "\" is already taken");

            //byte[] passwordHash, passwordSalt;
            //CreatePasswordHash(password, out passwordHash, out passwordSalt);

            activeToken.UserName = userName;
            activeToken.CurrentToken = currentToken;
            activeToken.Status = true;
            activeToken.TokenDate = DateTime.Now;
            activeToken.SourceIdentifier = SourceIdentifier;
            // Hardcoded by now.... TODO: Fix this hardcoded value
            activeToken.TokenExpireDate = DateTime.Now.AddHours(24);

            _context.ActiveTokens.Add(activeToken);
            _context.SaveChanges();

            return activeToken;

        } // End Create


        public ActiveToken Update(ActiveToken activeTokenParam, string currentToken = null, string SourceIdentifier = null)
        {
            var activeToken = _context.ActiveTokens.Find(activeTokenParam.Id);
            if (activeToken == null)
                throw new ApplicationException("ActiveToken not found");

            if (activeTokenParam.CurrentToken != activeToken.CurrentToken)
            {
                // username has changed so check if the new username is already taken
                if (_context.ActiveTokens.Any(x => x.CurrentToken == activeTokenParam.CurrentToken))
                    throw new ApplicationException("CurrentToken " + activeTokenParam.CurrentToken + " is already taken");
            }


            // Update user properties

            activeToken.CurrentToken = activeTokenParam.CurrentToken;
            activeToken.UserName = activeTokenParam.UserName;
            activeToken.TokenDate = new DateTime();
            // Hardcoded by now.... TODO: Fix this hardcoded value
            activeToken.TokenExpireDate = DateTime.Now.AddHours(24);
            activeToken.SourceIdentifier = activeTokenParam.SourceIdentifier;

            // Update password if it was entered

            //if(!string.IsNullOrWhiteSpace(password))
            //{
            //    byte[] passwordHash, passwordSalt;
            //    CreatePasswordHash(password, out passwordHash, out passwordSalt);

            //    user.PasswordHash = passwordHash;
            //    user.PasswordSalt = passwordSalt;

            //}

            _context.ActiveTokens.Update(activeToken);
            _context.SaveChanges();
            return activeToken;
        } // End Update


        public void Delete(int id)
        {
            var activeToken = _context.ActiveTokens.Find(id);
            if (activeToken != null)
            {
                _context.ActiveTokens.Remove(activeToken);
                _context.SaveChanges();
            }
        } // End Delete




    } // End Class
}