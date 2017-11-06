using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CSharpDataAccess;
using CSharpDataAccess.Factory;
using CSharpDataAccess.Product;
using Xunit;

namespace CSharpDataAccess.Test
{
    public class SqlServer_ExecuteReader_FunctionalTest : CSharpDataAccess_BaseClass
    {
        [Fact]
        public void ExecuteReaderTextTest()
        {
            // arrange
            var context = new DataAccessContext(this.ConnectionString, this.Provider);

            var query = @"SELECT * FROM [Northwind].[dbo].[Employees]";

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(context);

            // act
            var reader = sql.ExecuteDataReader(CommandBehavior.CloseConnection,
                CommandType.Text,
                query,
                x => new Employee()
                {
                    EmployeeId = (int)x["EmployeeId"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                });

            // assert
            Assert.NotNull(reader);

            var employee = reader.FirstOrDefault(x => x.EmployeeId == 5);
            Assert.NotNull(employee);
        }

        [Fact]
        public void ExecuteReaderIDataRecordTextTest()
        {
            // arrange
            var context = new DataAccessContext(this.ConnectionString, this.Provider);

            var query = @"SELECT * FROM [Northwind].[dbo].[Employees]";

            IDataAccessHandlerFactory factory = new DataAccessHandlerFactory();
            IDataAccessHandler sql = factory.CreateDataProvider(context);

            // act
            var reader = sql.ExecuteDataReader<IDataRecord>(CommandBehavior.CloseConnection,
                CommandType.Text,
                query,
                null,
                null);

            // assert
            Assert.NotNull(reader);

            var employee = reader.FirstOrDefault(x => (int)x.GetValue(x.GetOrdinal("EmployeeId")) == 1);
            Assert.NotNull(employee);
        }
    }
}