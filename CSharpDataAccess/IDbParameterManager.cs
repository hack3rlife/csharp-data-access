using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CSharpDataAccess
{
    public interface IDbParameterManager
    {
        IDataAccessContext DataAccessContext { get; }

        IDbDataParameter CreateParamter<T>(string name, DbType dbType, T value, int size = 0, ParameterDirection direction = ParameterDirection.Input);

        IDbDataParameter CreateSqlParamter<T>(string name, SqlDbType dbType, T value, int size = 0, ParameterDirection direction = ParameterDirection.Input);

        IDbDataParameter CreateMySqlParamter<T>(string name, MySqlDbType dbType, T value, int size = 0, ParameterDirection direction = ParameterDirection.Input);

        IDbDataParameter CreateOracleParamter<T>(string name, DbType dbType, T value, int size = 0, ParameterDirection direction = ParameterDirection.Input);
    }
}