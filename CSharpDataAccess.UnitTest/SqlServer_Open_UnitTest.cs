using Moq;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Xunit;

namespace CSharpDataAccess.UnitTest
{
    public class SqlServer_Open_UnitTest
    {
        private string _stringConnection = @"Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";

        [Fact]
        public void SqlServer_OpenConnection_Returns_True()
        {
            // arrange
            var mockContext = new Mock<IDataAccessContext>();
            mockContext.Setup(x => x.Open()).Returns(true);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory(_stringConnection, DataProvider.SQLServer);
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            bool condition = sql.Open();

            // assert
            Assert.True(condition);
        }

        [Fact]
        public void SqlServer_OpenConnection_Returns_False()
        {
            // arrange
            var mockContext = new Mock<IDataAccessContext>();
            mockContext.Setup(x => x.Open()).Returns(false);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory(_stringConnection, DataProvider.SQLServer);
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            bool condition = sql.Open();

            // assert
            Assert.False(condition);
        }
    }
}