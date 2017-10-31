using CSharpDataAccess;
using CSharpDataAccess.Product;

namespace CSharpDataAccess.Factory
{
    /// <summary>
    /// Defines the contract that class factories must implement to create new IDataAccessHandler objects.
    /// </summary>
    /// <remarks>Factory Method: Creator</remarks>
    public interface IDataAccessHandlerFactory
    {
        IDataAccessHandler CreateDataProvider(IDataAccessContext context);
    }
}