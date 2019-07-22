using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAll();
        Country GetById(int id);
        void Delete(int id);
        Country FindByOrdinal(int id);
    }
}