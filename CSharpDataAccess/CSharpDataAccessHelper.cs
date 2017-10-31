using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CSharpDataAccess
{
    public enum QueryType
    {
        Query = 1,
        StoreProcedure = 2
    }

    /// <summary>
    /// https://blogs.msdn.microsoft.com/dotnet/2016/11/09/net-core-data-access/
    /// </summary>
    public enum DataProvider
    {
        SQLServer = 1,
        MySQL = 2,
        Oracle = 4,
    }

    public abstract class CSharpDataAccessHelper
    {
        public static IDbConnection GetConnection(DataProvider provider)
        {
            switch (provider)
            {
                case DataProvider.SQLServer:
                    return new SqlConnection();

                case DataProvider.MySQL:
                    return new MySqlConnection();

                case DataProvider.Oracle:
                    return new OracleConnection();

                default:
                    return null;
            }
        }

        public static IDbCommand GetCommand(DataProvider provider)
        {
            switch (provider)
            {
                case DataProvider.SQLServer:
                    return new SqlCommand();

                case DataProvider.MySQL:
                    return new MySqlCommand();

                case DataProvider.Oracle:
                    return new OracleCommand();

                default:
                    return null;
            }
        }

        public static IDbDataAdapter GetDataAdapter(DataProvider provider)
        {
            switch (provider)
            {
                case DataProvider.SQLServer:
                    return new SqlDataAdapter();

                ////case DataProvider.MySQL:
                ////    return new MySqlDataAdater();

                case DataProvider.Oracle:
                    return new OracleDataAdapter();

                default:
                    return null;
            }
        }

        public static IDbTransaction GetTransaction(DataProvider provider)
        {
            IDbConnection iDbConnection = GetConnection(provider);
            IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
            return iDbTransaction;
        }

        public static IDataParameter GetParameter(DataProvider provider)
        {
            switch (provider)
            {
                case DataProvider.SQLServer:
                    return new SqlParameter();

                case DataProvider.MySQL:
                    return new MySqlParameter();

                case DataProvider.Oracle:
                    return new OracleParameter();

                default:
                    return null;
            }
        }

        public static List<IDbDataParameter> GetParameters(DataProvider provider, int count)
        {
            return new List<IDbDataParameter>(count);
        }
    }
}