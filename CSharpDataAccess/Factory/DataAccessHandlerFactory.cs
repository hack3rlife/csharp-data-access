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
                case DataProvider.MySQL:
                case DataProvider.Oracle:
                    return new DataAccessHandler(context);

                default:
                    return new DataAccessHandler(context);
            }
        }
    }
}