using System;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CSharpDataAccess
{
    public class DbParameterManager : IDbParameterManager
    {
        private IDataAccessContext _dataAccessContext;

        public IDataAccessContext DataAccessContext => _dataAccessContext;

        public DbParameterManager(IDataAccessContext dataAccessContext)
        {
            this._dataAccessContext = dataAccessContext;
        }

        public IDbDataParameter CreateMySqlParamter<T>(string name, MySqlDbType dbType, T value, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateOracleParamter<T>(string name, DbType dbType, T value, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParamter<T>(string name, DbType dbType, T value, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            switch (this.DataAccessContext.DataProvider)
            {
                case DataProvider.SQLServer:
                    var sqlParameters = new SqlParameter()
                    {
                        DbType = dbType,
                        Direction = direction,
                        ParameterName = name,
                        Size = size,
                        Value = value,
                    };

                    return sqlParameters;

                case DataProvider.MySQL:
                    var mysqlParamters = new MySqlParameter()
                    {
                        DbType = dbType,
                        Direction = direction,
                        ParameterName = name,
                        Size = size,
                        Value = value,
                    };

                    return mysqlParamters;

                case DataProvider.Oracle:
                    var oracleParameters = new OracleParameter()
                    {
                        DbType = dbType,
                        Direction = direction,
                        ParameterName = name,
                        Size = 0,
                        Value = value,
                    };

                    return oracleParameters;

                default:
                    throw new InvalidOperationException("Invalid provider");
            }
        }

        public IDbDataParameter CreateSqlParamter<T>(string name, SqlDbType dbType, T value, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            switch (this.DataAccessContext.DataProvider)
            {
                case DataProvider.SQLServer:
                    var sqlParameters = new SqlParameter()
                    {
                        Direction = direction,
                        ParameterName = name,
                        Size = size,
                        SqlDbType = dbType,
                        Value = value,
                    };

                    return sqlParameters;

                default:
                    throw new InvalidOperationException("Invalid provider");
            }
        }
    }
}