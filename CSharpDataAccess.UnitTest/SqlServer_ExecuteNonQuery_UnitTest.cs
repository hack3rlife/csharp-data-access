using System;
using System.Collections.Generic;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Moq;
using Xunit;
using System.Data;

namespace CSharpDataAccess.UnitTest
{
    public class SqlServer_ExecuteNonQuery_UnitTest
    {
        private string _stringConnection = @"Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";

        [Fact]
        public void SqlServer_ExecuteScalar_Test()
        {
            // arrange
            var mockCommand = new Mock<IDbCommand>();
            mockCommand
                .Setup(c => c.ExecuteNonQuery())
                .Returns(1);

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.
                Setup(c => c.CreateCommand())
                .Returns(mockCommand.Object);

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory(_stringConnection, DataProvider.SQLServer);
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            var actualResult = sql.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateAll");

            // assert
            Assert.Equal<int>(1, actualResult);
        }

        [Fact]
        public void SqlServer_ExecuteScalar_WithParameters_Test()
        {
            // arrange
            var mockParams = new Mock<IDataParameterCollection>();

            var mockParameter = new Mock<IDbDataParameter>();
            mockParameter.SetupSet(p => p.ParameterName = It.IsAny<string>());
            mockParameter.SetupSet(p => p.Value = 1);

            mockParams
                .Setup(p => p.Add(mockParameter.Object))
                .Returns(0);

            var mockCommand = new Mock<IDbCommand>();

            mockCommand.SetupSet(c => c.CommandText = "UpdateById");
            mockCommand.SetupSet(c => c.CommandType = CommandType.StoredProcedure);
            mockCommand.SetupGet(c => c.Parameters).Returns(mockParams.Object);
            mockCommand
                .Setup(p => p.CreateParameter())
                .Returns(mockParameter.Object);

            mockCommand
                .Setup(c => c.ExecuteNonQuery())
                .Returns(1);

            var mockConnection = new Mock<IDbConnection>();
            mockConnection
                .Setup(c => c.CreateCommand())
                .Returns(mockCommand.Object);

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory(_stringConnection, DataProvider.SQLServer);
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            var parameters = new Dictionary<string, IConvertible>() { { "@Id", 1 } };

            var actualResult = sql.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateById", parameters);

            // assert
            Assert.Equal<int>(1, actualResult);
        }
    }
}