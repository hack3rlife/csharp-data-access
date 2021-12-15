using System.Data;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Xunit;

namespace CSharpDataAccess.Test
{
    public class SqlServer_ExecuteScalar_FunctionalTest : CSharpDataAccess_BaseClass
    {
        [Fact]
        public void ExecuteScalarTextTest()
        {
            //arrange
            var query1 = @"INSERT INTO [dbo].[dummy_t] ([description]) VALUES ('Testing ExecuteNonQueryTextTest');" + "SELECT CAST(scope_identity() AS int)";

            var query2 = "select max(id) from [dbo].[dummy_t]";

            var context = new DataAccessContext(ConnectionString, Provider);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(context);

            //act
            int result1 = sql.ExecuteScalar<int>(CommandType.Text, query1);
            int result2 = sql.ExecuteScalar<int>(CommandType.Text, query2);

            //assert
            Assert.Equal<int>(result1, result2);
        }
    }
}