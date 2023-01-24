using Core.DataAccess.Configuration.Base;

namespace Core.DataAccess.Configuration
{
    public class DbConfig : IDbConfig
    {
        public string DATABASE_NAME { get; set; }
        public string NOSQL_CONNECTION_STRING { get; set; }
    }
}
