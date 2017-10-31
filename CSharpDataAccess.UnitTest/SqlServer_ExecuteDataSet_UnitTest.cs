using System;
using System.Collections.Generic;
using System.Data;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Moq;
using Xunit;

namespace CSharpDataAccess.UnitTest
{
    public class SqlServer_ExecuteDataSet_UnitTest
    {
        private string _stringConnection = @"Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";

        [Fact]
        public void SqlServer_ExecuteDataSet_Test()
        {
            // arrange
            var mockCommand = new Mock<IDbCommand>();
            mockCommand.SetupSet(c => c.CommandText = It.IsAny<string>());
            mockCommand.SetupSet(c => c.CommandType = It.IsAny<CommandType>());

            var mockAdapter = new Mock<IDbDataAdapter>();
            mockAdapter
                .Setup(x => x.Fill(It.IsAny<DataSet>()))
                .Returns(0);

            mockAdapter
                .SetupSet(a => a.SelectCommand = mockCommand.Object);

            var mockConnection = new Mock<IDbConnection>();
            mockConnection
                .Setup(c => c.CreateCommand())
                .Returns(mockCommand.Object);

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            mockContext
                .Setup(x => x.CreateAdapter())
                .Returns(mockAdapter.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory(_stringConnection, DataProvider.SQLServer);
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            var actualResult = sql.ExecuteDataSet(CommandType.StoredProcedure, "mySqlQuery");

            // assert
            Assert.NotNull(actualResult);
        }

        [Fact]
        public void SqlServer_ExecuteDataSet_WithParameters_Test()
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
            mockCommand.SetupSet(c => c.CommandText = It.IsAny<string>());
            mockCommand.SetupSet(c => c.CommandType = It.IsAny<CommandType>());
            mockCommand.SetupGet(c => c.Parameters).Returns(mockParams.Object);
            mockCommand
                .Setup(p => p.CreateParameter())
                .Returns(mockParameter.Object);

            var mockAdapter = new Mock<IDbDataAdapter>();
            mockAdapter
                .Setup(x => x.Fill(It.IsAny<DataSet>()))
                .Returns(0);

            mockAdapter
                .SetupSet(a => a.SelectCommand = mockCommand.Object);

            var mockConnection = new Mock<IDbConnection>();
            mockConnection
                .Setup(c => c.CreateCommand())
                .Returns(mockCommand.Object);

            var mockContext = new Mock<IDataAccessContext>();
            mockContext
                .Setup(x => x.CreateConnection())
                .Returns(mockConnection.Object);

            mockContext
                .Setup(x => x.CreateAdapter())
                .Returns(mockAdapter.Object);

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory(_stringConnection, DataProvider.SQLServer);
            IDataAccessHandler sql = factory.CreateDataProvider(mockContext.Object);

            // act
            var parameters = new Dictionary<string, IConvertible>() { { "@Id", 1 } };

            var actualResult = sql.ExecuteDataSet(CommandType.StoredProcedure, "myStoreProcedureName", parameters);

            // assert
            Assert.NotNull(actualResult);
        }
    }
}