using System;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CSharpDataAccess
{
    public class DataAccessContext : IDataAccessContext
    {
        public string ConnectionString { get; set; }

        public DataProvider DataProvider { get; set; }

        public DataAccessContext(string connectionString, DataProvider provider)
        {
            this.ConnectionString = connectionString;
            this.DataProvider = provider;
        }

        public IDbCommand CreateCommand()
        {
            switch (this.DataProvider)
            {
                case DataProvider.SQLServer:
                    return new SqlCommand();

                case DataProvider.MySQL:
                    return new SqlCommand();

                case DataProvider.Oracle:
                    return new SqlCommand();

                default:
                    return null;
            }
        }

        public IDbConnection CreateConnection()
        {
            switch (this.DataProvider)
            {
                case DataProvider.SQLServer:
                    return new SqlConnection(this.ConnectionString);

                case DataProvider.MySQL:
                    return new MySqlConnection(this.ConnectionString);

                case DataProvider.Oracle:
                    return new OracleConnection(this.ConnectionString);

                default:
                    return null;
            }
        }

        public IDbDataParameter CreateParameter()
        {
            switch (this.DataProvider)
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

        public IDbDataAdapter CreateAdapter()
        {
            switch (this.DataProvider)
            {
                case DataProvider.SQLServer:
                    return new SqlDataAdapter();

                case DataProvider.MySQL:
                    throw new NotSupportedException("MySQL does not support DataAdapter");

                case DataProvider.Oracle:
                    return new OracleDataAdapter();

                default:
                    return null;
            }
        }

        //public static IDbTransaction GetTransaction(DataProvider provider)
        //{
        //    IDbConnection iDbConnection = GetConnection(provider);
        //    IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
        //    return iDbTransaction;
        //}
    }
}