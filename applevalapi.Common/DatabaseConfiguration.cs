using Microsoft.Extensions.Configuration;

namespace applevalApi.Common
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private string DbConnectionKey = "applevalApiConnection";
        public string GetDatabaseConnectionString()
        {
            return GetConfiguration().GetConnectionString(DbConnectionKey);
        }
    }
}