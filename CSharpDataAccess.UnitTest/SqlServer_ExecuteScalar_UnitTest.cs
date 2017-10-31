using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Moq;
using Xunit;
using System.Data;

namespace CSharpDataAccess.UnitTest
{
    public class SqlServer_ExecuteScalar_UnitTest
    {
        private string _stringConnection = @"Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";

        [Fact]
        public void SqlServer_ExecuteScalar_Test()
        {
            // arrange
            var commandMock = new Mock<IDbCommand>();
            commandMock
                .Setup(m => m.ExecuteScalar())
                .Returns("the value");

            var mockConnection = new Mock<IDbConnection>();
            mockConnection
                .Setup(x => x.CreateCommand())
                .Returns(commandMock.Object);

            mockConnection
                .Setup(x => x.Open());

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory(_stringConnection, DataProvider.SQLServer);
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            var actualResult = sql.ExecuteScalar<string>(CommandType.Text, "the query");

            // assert
            Assert.Equal("the value", actualResult);
        }
    }
}