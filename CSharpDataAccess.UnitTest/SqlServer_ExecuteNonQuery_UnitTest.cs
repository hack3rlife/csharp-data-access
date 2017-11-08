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
        [Fact]
        public void SqlServer_ExecuteScalar_Test()
        {
            // arrange
            var mockCommand = new Mock<IDbCommand>();
            mockCommand
                .Setup(c => c.ExecuteNonQuery())
                .Returns(1);

            var mockConnection = new Mock<IDbConnection>();

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
             .Setup(x => x.CreateCommand())
             .Returns(mockCommand.Object);

            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
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
                .Setup(c => c.ExecuteNonQuery())
                .Returns(1);

            var mockConnection = new Mock<IDbConnection>();

            var mockContext = new Mock<IDataAccessContext>();
            mockContext.
                SetupGet(c => c.DataProvider)
                .Returns(DataProvider.SQLServer);

            mockContext
               .Setup(x => x.CreateCommand())
               .Returns(mockCommand.Object);

            mockContext
                .Setup(x => x.CreateParameter())
                .Returns(mockParameter.Object);

            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            var dbParameterManager = new DbParameterManager(mockContext.Object);

            var parameters = new List<IDbDataParameter>()
            {
                dbParameterManager.CreateParamter("@Id", DbType.Int16, 1)
            };

            // act
            var actualResult = sql.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateById", null);

            // assert
            Assert.Equal<int>(1, actualResult);
        }
    }
}