using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
            var context = new DataAccessContext(this.ConnectionString, this.Provider);

            var query = @"INSERT INTO [Northwind].[dbo].[Categories] ([CategoryName], [Description],[Picture]) VALUES (@CategoryName,@Description, @Picture)";

            var dbParameterManager = new DbParameterManager(context);

            var parameters = new List<IDbDataParameter>
            {
                dbParameterManager.CreateSqlParamter("@CategoryName", SqlDbType.VarChar, "TestCategory"),
                dbParameterManager.CreateSqlParamter("@Description", SqlDbType.VarChar, "TestDescription"),
                dbParameterManager.CreateSqlParamter("@Picture", SqlDbType.Image, Encoding.ASCII.GetBytes("PTestPicture.png"))
            };

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(context);

            // act
            int result = sql.ExecuteNonQuery(CommandType.Text, query, parameters);

            // assert
            Assert.Equal<int>(1, result);
        }
    }
}