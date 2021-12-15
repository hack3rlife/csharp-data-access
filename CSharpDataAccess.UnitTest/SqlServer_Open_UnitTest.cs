using Moq;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Xunit;
using System.Data;

namespace CSharpDataAccess.UnitTest
{
    public class SqlServer_Open_UnitTest
    {
        private string _stringConnection = @"Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";

        [Fact]
        public void SqlServer_OpenConnection_Returns_True()
        {
            // arrange
            var mockConnection = new Mock<IDbConnection>();

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            bool condition = sql.TryOpen(out IDbConnection connection);

            // assert
            Assert.True(condition);
        }

        [Fact]
        public void SqlServer_OpenConnection_Returns_False()
        {
            // arrange
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupGet(x => x.State).Returns(ConnectionState.Open);

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            bool condition = sql.TryOpen(out IDbConnection connection);

            // assert
            Assert.False(condition);
        }
    }
}