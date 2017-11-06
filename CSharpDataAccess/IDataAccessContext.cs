using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using CSharpDataAccess;

namespace CSharpDataAccess
{
    public interface IDataAccessContext
    {
        string ConnectionString { get; set; }

        DataProvider DataProvider { get; set; }

        IDbConnection CreateConnection();

        IDbCommand CreateCommand();

        IDbDataParameter CreateParameter();

        IDbDataAdapter CreateAdapter();
    }
}