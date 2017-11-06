using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CSharpDataAccess;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Xunit;

namespace CSharpDataAccess.Test
{
    public class SqlServer_ExecuteDataSet_FunctionalTest : CSharpDataAccess_BaseClass
    {
        [Fact]
        public void ExecuteDataSetTextTest()
        {
            // arrange
            var context = new DataAccessContext(NwStringConnection, Provider);

            var query = @"SELECT * FROM [Northwind].[dbo].[Employees]";

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(context);

            // act
            DataSet ds = sql.ExecuteDataSet(CommandType.Text, query);

            // assert
            Assert.NotNull(ds);
            Assert.True(ds.Tables.Count > 0);
        }
    }
}