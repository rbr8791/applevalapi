using System.Collections.Generic;
using System.Linq;
using applevalApi.Entities;
using applevalApi.Persistence;
using Microsoft.EntityFrameworkCore;
using applevalApi.DAL.Interfaces;
using System;
namespace applevalApi.DAL
{
    public class RoleService : IRoleService
    {
        //private ApplDbContext _dwContext;

      
        private ApplDbContext _context;

        public RoleService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext

        public Role FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(role => role.Id == id);
        }

        private IEnumerable<Role> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.Roles;
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }
        public IEnumerable<Role> GetAll()
        {
            return BaseQuery();
                //.FirstOrDefault(role => role.Id == id);
        } // End IEnumerable

        public Role GetById(int id)
        {
            return BaseQuery()
                .FirstOrDefault(role => role.Id == id);
        } // End GetById

        public Role Create(Role role, string roleName)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(roleName))
                throw new System.ApplicationException("The role name can not be null or empty");

            role.RoleName = roleName;

            _context.Roles.Add(role);
            _context.SaveChanges();

            return role;

        } // End Create


        public Role Update(Role roleParam, string roleName = null)
        {
            var role = _context.Roles.Find(roleParam.Id);
            if (role == null)
                throw new System.ApplicationException("Role not found");

            if (roleParam.Id != role.Id)
            {
                // username has changed so check if the new username is already taken
                if (_context.Roles.Any(x => x.RoleName == roleParam.RoleName))
                    throw new System.ApplicationException("Role Name " + roleParam.RoleName + " is already taken");
            }


            role.RoleName = roleParam.RoleName;


            _context.Roles.Update(role);
            _context.SaveChanges();
            return role;
        } // End Update


        public void Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        } // End Delete


    } // End Class
}