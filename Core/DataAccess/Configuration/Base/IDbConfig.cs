namespace Core.DataAccess.Configuration.Base
{
    public interface IDbConfig
    {
        string DATABASE_NAME { get; set; }
        string NOSQL_CONNECTION_STRING { get; set; }
    }
}
