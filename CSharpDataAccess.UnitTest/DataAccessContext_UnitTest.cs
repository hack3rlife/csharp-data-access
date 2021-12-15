using Xunit;
using System.Data.SqlClient;

namespace CSharpDataAccess.UnitTest
{
    public class DataAccessContext_UnitTest
    {
        [Fact]
        public void DataAccessContext_CreateCommand_SqlComand_Test()
        {
            // arrange
            var context = new DataAccessContext(string.Empty, DataProvider.SQLServer);

            // act
            var command = context.CreateCommand();

            // assert
            Assert.Equal(typeof(SqlCommand), command.GetType());
        }

        [Fact]
        public void DataAccessContext_CreateConnection_SqlConnection_Test()
        {
            // arrange
            var context = new DataAccessContext(string.Empty, DataProvider.SQLServer);

            // act
            var connection = context.CreateConnection();

            // assert
            Assert.Equal(typeof(SqlConnection), connection.GetType());
        }

        [Fact]
        public void DataAccessContext_CreateParameter_SqlParameter_Test()
        {
            // arrange
            var context = new DataAccessContext(string.Empty, DataProvider.SQLServer);

            // act
            var parameter = context.CreateParameter();

            // assert
            Assert.Equal(typeof(SqlParameter), parameter.GetType());
        }

        [Fact]
        public void DataAccessContext_CreateDataAdapter_SqlDataAdapter_Test()
        {
            // arrange
            var context = new DataAccessContext(string.Empty, DataProvider.SQLServer);

            // act
            var adapter = context.CreateAdapter();

            // assert
            Assert.Equal(typeof(SqlDataAdapter), adapter.GetType());
        }
    }
}