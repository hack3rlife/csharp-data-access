using System;
using System.Collections.Generic;
using System.Text;
using CSharpDataAccess;

namespace CSharpDataAccess.Test
{
    public class CSharpDataAccess_BaseClass
    {
        private string _connectionString = @"Data Source=US00126205\SQLEXPRESS;Initial Catalog=csharpdataaccessdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private string _nwStringConnection = @"Data Source=US00126205\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private DataProvider provider = DataProvider.SQLServer;

        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        public string NwStringConnection { get => _nwStringConnection; set => _nwStringConnection = value; }
        public DataProvider Provider { get => provider; set => provider = value; }
    }
}