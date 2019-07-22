using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces 
{
    public interface IActiveTokenService
    {
        // Search and Get
        IEnumerable<ActiveToken> GetAll();
        ActiveToken GetById(int id);
        List<ActiveToken> GetTopOneActiveTokenBySourceIdentifier(string sourceIdentifier);
        ActiveToken Create(ActiveToken activeToken, string userName, string currentToken, string SourceIdentifier);
        ActiveToken Update(ActiveToken activeTokenParam, string currentToken = null, string SourceIdentifier = null);
        void Delete(int id);
        ActiveToken FindByOrdinal(int id);

    }
}