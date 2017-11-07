using System.Data;

namespace CSharpDataAccess
{
    public interface IDbParameter
    {
        IDbDataParameter CreateParamter(IDbCommand command);
    }
}