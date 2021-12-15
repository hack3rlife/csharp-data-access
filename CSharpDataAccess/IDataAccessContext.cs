using System.Data;

namespace CSharpDataAccess
{
    public interface IDataAccessContext
    {
        string ConnectionString { get; set; }

        DataProvider DataProvider { get; set; }

        IDbConnection CreateConnection();

        IDbCommand CreateCommand();

        IDbDataAdapter CreateAdapter();

        IDbDataParameter CreateParameter();

        IDbTransaction CreateTransaction();
    }
}