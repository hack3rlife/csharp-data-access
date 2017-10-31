using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CSharpDataAccess;

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

        public bool Close()
        {
            throw new NotImplementedException();
        }

        public IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(this.ConnectionString);
        }

        public IDbDataParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public bool Open()
        {
            throw new NotImplementedException();
        }

        public IDbDataAdapter CreateAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}