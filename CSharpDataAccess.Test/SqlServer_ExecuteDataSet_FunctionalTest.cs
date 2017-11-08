using System.Collections.Generic;
using System.Data;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Xunit;

namespace CSharpDataAccess.Test
{
    public class SqlServer_ExecuteDataSet_FunctionalTest : CSharpDataAccess_BaseClass
    {
        [Fact]
        public void ExecuteDataSet_Test()
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

        [Fact]
        public void ExecuteDataSet_WithParameters_Test()
        {
            // arrange
            var context = new DataAccessContext(NwStringConnection, Provider);

            var query = @"[dbo].[CustOrderHist]";

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(context);

            var dbParameterManager = new DbParameterManager(context);
            var parameters = new List<IDbDataParameter>()
            {
                dbParameterManager.CreateSqlParamter("@CustomerID", SqlDbType.NChar, "ALFKI", 5)
            };

            // act
            DataSet ds = sql.ExecuteDataSet(CommandType.StoredProcedure, query, parameters);

            // assert
            Assert.NotNull(ds);
            Assert.True(ds.Tables.Count > 0);
        }
    }
}