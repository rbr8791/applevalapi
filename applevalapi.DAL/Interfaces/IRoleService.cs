using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        Role Create(Role role, string roleName);
        Role Update(Role role, string roleName = null);
        void Delete(int id);
        Role FindByOrdinal(int id);
    }
}