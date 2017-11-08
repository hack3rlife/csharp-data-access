using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using csharpdataaccess.unittest;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Moq;
using Xunit;

namespace CSharpDataAccess.UnitTest
{
    public class SqlServer_ExecuteDataReader_UnitTest
    {
        [Fact]
        public void SqlServer_ExecuteDataReader_Test()
        {
            // arrange
            Mock<IDataReader> mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetName(0)).Returns("EmployeeId");
            mockDataReader.Setup(x => x.GetFieldType(0)).Returns(typeof(string));
            mockDataReader.Setup(x => x.GetOrdinal("EmployeeId")).Returns(0);
            mockDataReader.Setup(x => x["EmployeeId"]).Returns(1);

            mockDataReader.SetupSequence(x => x.Read())
                .Returns(true)
                .Returns(false);

            var mockParams = new Mock<IDataParameterCollection>();

            var mockParameter = new Mock<IDbDataParameter>();
            mockParameter.SetupSet(p => p.ParameterName = It.IsAny<string>());
            mockParameter.SetupSet(p => p.Value = 1);

            mockParams
                .Setup(p => p.Add(mockParameter.Object))
                .Returns(0);

            var mockCommand = new Mock<IDbCommand>();

            mockCommand.SetupSet(c => c.CommandText = "GetEmployeeId");
            mockCommand.SetupSet(c => c.CommandType = CommandType.StoredProcedure);
            mockCommand.SetupGet(c => c.Parameters).Returns(mockParams.Object);

            mockCommand
                .Setup(c => c.ExecuteReader(CommandBehavior.CloseConnection))
                .Returns(mockDataReader.Object);

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
            var actualResult = sql.ExecuteDataReader(CommandBehavior.CloseConnection,
                CommandType.StoredProcedure,
                "GetEmployeeById",
                x => new Employee()
                {
                    EmployeeId = (int)x["EmployeeId"],
                },
                parameters);

            // assert
            Assert.NotNull(actualResult);

            var employee = actualResult.FirstOrDefault(x => x.EmployeeId == 1);
            Assert.NotNull(employee);
        }
    }
}