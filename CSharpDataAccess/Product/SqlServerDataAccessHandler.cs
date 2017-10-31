namespace CSharpDataAccess.Product
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using CSharpDataAccess;

    /// <summary>
    ///
    /// </summary>
    /// <remarks>Factory Method: Concrete Product</remarks>
    internal class SqlServerDataAccessHandler : IDataAccessHandler
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
        public SqlServerDataAccessHandler(IDataAccessContext context)
        {
            this._context = context;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SqlServerDataAccessHandler"/> class.
        ///// </summary>
        //public SqlServerDataAccessHandler(DbConnection connection)
        //{
        //    this.connection = connection;
        //}

        /// <summary>
        /// The Open
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool Open()
        {
            return _context.Open();
        }

        /// <summary>
        /// The Close
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool Close()
        {
            return this._context.Close();
        }

        /// <summary>
        /// The ExecuteScalar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandType">The <see cref="CommandType"/></param>
        /// <param name="commandText">The <see cref="string"/></param>
        /// <returns>The <see cref="T"/></returns>
        public T ExecuteScalar<T>(CommandType commandType, string commandText)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = commandText;
                    command.CommandType = commandType;
                    connection.Open();

                    var result = command.ExecuteScalar();

                    return (T)result;
                }
            }
            catch (Exception e)
            {
                throw new CSharpException("", e);
            }
            finally
            {
            }
        }

        /// <summary>
        /// The ExecuteNonQuery
        /// </summary>
        /// <param name="commandType">The <see cref="CommandType"/></param>
        /// <param name="commandText">The <see cref="string"/></param>
        /// <param name="parameters">The <see cref="IEnumerable{KeyValuePair{string, IConvertible}}"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, IEnumerable<KeyValuePair<string, IConvertible>> parameters = null)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = commandText;
                    command.CommandType = commandType;
                    connection.Open();

                    if (parameters != null && parameters.Any())
                    {
                        foreach (var param in parameters)
                        {
                            var parameter = command.CreateParameter();
                            parameter.ParameterName = param.Key;
                            parameter.Value = param.Value;
                            command.Parameters.Add(parameter);
                        }
                    }

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new CSharpException("", e);
            }
            finally
            {
            }
        }

        /// <summary>
        /// The ExecuteDataSet
        /// </summary>
        /// <param name="commandType">The <see cref="CommandType"/></param>
        /// <param name="commandText">The <see cref="string"/></param>
        /// <param name="parameters">The <see cref="IEnumerable{KeyValuePair{string, IConvertible}}"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, IEnumerable<KeyValuePair<string, IConvertible>> parameters = null)
        {
            using (var connection = _context.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;

                if (parameters != null && parameters.Any())
                {
                    foreach (var param in parameters)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = param.Key;
                        parameter.Value = param.Value;
                        command.Parameters.Add(parameter);
                    }
                }

                var adapter = _context.CreateAdapter();
                adapter.SelectCommand = command;

                connection.Open();

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
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
        public IEnumerable<T> ExecuteDataReader<T>(CommandBehavior commandBehavior, CommandType commandType, string commandText, Func<IDataRecord, T> selector, IEnumerable<KeyValuePair<string, IConvertible>> parameters = null)
        {
            using (var connection = _context.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;

                if (parameters != null && parameters.Any())
                {
                    foreach (var param in parameters)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = param.Key;
                        parameter.Value = param.Value;
                        command.Parameters.Add(parameter);
                    }
                }

                connection.Open();

                var reader = command.ExecuteReader(commandBehavior);

                while (reader.Read())
                {
                    if (selector != null)
                        yield return selector(reader);
                    else
                        yield return (T)reader;
                }
            }
        }
    }
}