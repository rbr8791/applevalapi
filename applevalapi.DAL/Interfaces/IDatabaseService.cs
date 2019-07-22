using System.Collections.Generic;
using System.Threading.Tasks;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IDatabaseService
    {
        bool ClearDatabase();

        int SeedDatabase();

       

        Task<int> SaveAnyChanges();
    }
}