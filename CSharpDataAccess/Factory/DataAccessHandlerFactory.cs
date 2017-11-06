using System;
using CSharpDataAccess.Product;

namespace CSharpDataAccess.Factory
{
    /// <summary>
    /// By default, return a SqlServer object
    /// </summary>
    /// <remarks>Factory Method: ConcreteCreator</remarks>
    public sealed class DataAccessHandlerFactory : IDataAccessHandlerFactory
    {
        public DataAccessHandlerFactory()
        {
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