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
    public class SqlServer_ExecuteNonQueryText_FunctionalTest : CSharpDataAccess_BaseClass
    {
        [Fact]
        public void ExecuteNonQueryTextTest()
        {
            // arrange
            var context = new DataAccessContext(ConnectionString, Provider);

            var query = @"INSERT INTO [Northwind].[dbo].[Categories] ([CategoryName] ,[Description],[Picture]) VALUES ('Test Category','Test Description', 'image.png')";

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory(NwStringConnection, Provider);
            IDataAccessHandler sql = factory.CreateDataProvider(context);

            // act
            int result = sql.ExecuteNonQuery(CommandType.Text, query);

            // assert
            Assert.Equal<int>(1, result);
        }
    }
}