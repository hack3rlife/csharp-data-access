using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Moq;
using Xunit;
using System.Data;
using System.Data.SqlClient;

namespace CSharpDataAccess.UnitTest
{
    public class SqlServer_ExecuteScalar_UnitTest
    {
        [Fact]
        public void SqlServer_ExecuteScalar_Test()
        {
            // arrange
            var commandMock = new Mock<IDbCommand>();
            commandMock
                .Setup(m => m.ExecuteScalar())
                .Returns("the value");

            var mockConnection = new Mock<IDbConnection>();

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
                .Setup(x => x.CreateCommand())
                .Returns(commandMock.Object);

            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            mockContext.SetupSet(x => x.DataProvider = DataProvider.SQLServer);
            mockContext.SetupGet(x => x.DataProvider).Returns(DataProvider.SQLServer);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            var actualResult = sql.ExecuteScalar<string>(CommandType.Text, "the query");

            // assert
            Assert.Equal("the value", actualResult);
        }
    }
}