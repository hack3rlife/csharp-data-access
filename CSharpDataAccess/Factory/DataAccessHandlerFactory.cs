using System;
using System.Data.SqlClient;
using CSharpDataAccess;
using CSharpDataAccess.Product;

namespace CSharpDataAccess.Factory
{
    /// <summary>
    /// By default, return a SqlServer object
    /// </summary>
    /// <remarks>Factory Method: ConcreteCreator</remarks>
    public sealed class DataAccessHandlerFactory : IDataAccessHandlerFactory
    {
        private IDataAccessContext dataAccessContext;

        public DataAccessHandlerFactory(string stringConnection, DataProvider provider)
        {
            this.dataAccessContext = new DataAccessContext(stringConnection, provider);
        }

        public IDataAccessHandler CreateDataProvider(IDataAccessContext context)
        {
            switch (context.DataProvider)
            {
                case DataProvider.SQLServer:
                    return new SqlServerDataAccessHandler(context);

                case DataProvider.MySQL:
                    throw new NotImplementedException();

                case DataProvider.Oracle:
                    throw new NotImplementedException();

                default:
                    return new SqlServerDataAccessHandler(context);
            }
        }
    }
}