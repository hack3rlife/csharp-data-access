using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CSharpDataAccess.Product
{
    /// <summary>
    ///
    /// </summary>
    /// <remarks>Factory Method: Concrete Product</remarks>
    internal class DataAccessHandler : IDataAccessHandler
    {
        private IDataAccessContext _context;

        /// <summary>
        /// Gets or sets the StringConnection
        /// </summary>
        public string StringConnection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDataAccessHandler"/> class.
        /// </summary>
        /// <param name="stringConnection">The <see cref="string"/></param>
        public DataAccessHandler(IDataAccessContext context)
        {
            this._context = context;
        }

        public DataAccessHandler()
        {
        }

        /// <summary>
        /// The Open
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool TryOpen(out IDbConnection connection)
        {
            var conn = _context.CreateConnection();

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                connection = conn;
                return true;
            }

            connection = null;
            return false;
        }

        /// <summary>
        /// The Close
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool TryClose(IDbConnection connection)
        {
            using (connection)
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// The ExecuteScalar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandType">The <see cref="CommandType"/></param>
        /// <param name="commandText">The <see cref="string"/></param>
        /// <returns>The <see cref="T"/></returns>
        /// <exception cref="CSharpException"></exception>
        public T ExecuteScalar<T>(CommandType commandType, string commandText)
        {
            IDbConnection connection = null;

            try
            {
                if (this.TryOpen(out connection))
                {
                    var command = CreateSqlCommand(commandType, commandText, parameters: null, connection: connection);

                    var result = command.ExecuteScalar();

                    return (T)result;
                }

                return default(T);
            }
            catch (Exception e)
            {
                throw new CSharpException("ExecuteScalar.Exception", e);
            }
            finally
            {
                this.TryClose(connection);
            }
        }

        /// <summary>
        /// The ExecuteNonQuery
        /// </summary>
        /// <param name="commandType">The <see cref="CommandType"/></param>
        /// <param name="commandText">The <see cref="string"/></param>
        /// <param name="parameters">The <see cref="IEnumerable{KeyValuePair{string, IConvertible}}"/></param>
        /// <returns>The <see cref="int"/></returns>
        /// <exception cref="CSharpException"></exception>
        public int ExecuteNonQuery(CommandType commandType, string commandText, IEnumerable<IDbDataParameter> parameters = null)
        {
            IDbConnection connection = null;

            try
            {
                if (this.TryOpen(out connection))
                {
                    var command = CreateDbCommand(commandType, commandText, parameters, connection);

                    return command.ExecuteNonQuery();
                }

                return -1;
            }
            catch (Exception e)
            {
                throw new CSharpException("ExecuteNonQuery.Exception", e);
            }
            finally
            {
                this.TryClose(connection);
            }
        }

        /// <summary>
        /// The ExecuteDataSet
        /// </summary>
        /// <param name="commandType">The <see cref="CommandType"/></param>
        /// <param name="commandText">The <see cref="string"/></param>
        /// <param name="parameters">The <see cref="IEnumerable{KeyValuePair{string, IConvertible}}"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        /// <exception cref="CSharpException"></exception>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, IEnumerable<IDbDataParameter> parameters = null)
        {
            IDbConnection connection = null;
            DataSet dataSet = new DataSet();

            try
            {
                if (this.TryOpen(out connection))
                {
                    var command = CreateDbCommand(commandType, commandText, parameters, connection);

                    var adapter = _context.CreateAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet);
                }

                return dataSet;
            }
            catch (Exception e)
            {
                throw new CSharpException("ExecuteDataSet.Exception", e);
            }
            finally
            {
                this.TryClose(connection);
            }
        }

        /// <summary>
        /// The ExecuteDataReader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandBehavior">The <see cref="CommandBehavior"/></param>
        /// <param name="commandText">The <see cref="string"/></param>
        /// <param name="selector">The <see cref="Func{IDataRecord, T}"/></param>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="CSharpException"></exception>
        public IEnumerable<T> ExecuteDataReader<T>(CommandBehavior commandBehavior, CommandType commandType, string commandText, Func<IDataRecord, T> selector, IEnumerable<IDbDataParameter> parameters = null)
        {
            if (this.TryOpen(out IDbConnection connection))
            {
                {
                    var command = CreateDbCommand(commandType, commandText, parameters, connection);

                    var reader = command.ExecuteReader(commandBehavior);

                    while (reader.Read())
                    {
                        if (selector != null)
                            yield return selector(reader);
                        else
                            yield return (T)reader;
                    }
                }

                this.TryClose(connection);
            }
        }

        public IDbCommand CreateSqlCommand(CommandType commandType, string commandText, IEnumerable<KeyValuePair<string, IConvertible>> parameters, IDbConnection connection)
        {
            var command = _context.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Connection = connection;

            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    var parameter = _context.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value;
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        public IDbCommand CreateDbCommand(CommandType commandType, string commandText, IEnumerable<IDbDataParameter> parameters, IDbConnection connection)
        {
            var command = _context.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Connection = connection;

            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    var parameter = _context.CreateParameter();
                    parameter.DbType = param.DbType;
                    parameter.Direction = param.Direction;
                    parameter.ParameterName = param.ParameterName;
                    parameter.Size = param.Size;
                    parameter.Value = param.Value;

                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
    }
}