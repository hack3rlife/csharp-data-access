using System;
using System.Collections.Generic;
using System.Data;

namespace CSharpDataAccess.Product
{
    /// <summary>
    /// Defines the contract that clients implements to Log information
    /// </summary>
    /// <remarks>Factory Method: Product</remarks>
    public interface IDataAccessHandler
    {
        /// <summary>
        ///
        /// </summary>
        string StringConnection { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool TryOpen(out IDbConnection connection);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool TryClose(IDbConnection connection);

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns>The first column of the first row in the result set, or a null reference (Nothing in Visual Basic) if the result set is empty. Returns a maximum of 2033 characters.</returns>
        /// <remarks>Use the ExecuteScalar method to retrieve a single value (for example, an aggregate value) from a database. This requires less code than using the ExecuteReader method, and then performing the
        /// operations that you need to generate the single value using the data returned by a SqlDataReader</remarks>
        /// <see cref="https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.executescalar(v=vs.110).aspx"/>
        T ExecuteScalar<T>(CommandType commandType, string commandText);

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.executenonquery(v=vs.100).aspx"/>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command. For all other types of statements,
        /// the return value is -1. If a rollback occurs, the return value is also -1.</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, IEnumerable<KeyValuePair<string, IConvertible>> parameters = null);

        /// <summary>
        /// Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(CommandType commandType, string commandText, IEnumerable<KeyValuePair<string, IConvertible>> parameters = null);

        /// <summary>
        /// Executes the CommandText against the Connection, and builds an IDataReader using one of the CommandBehavior values.
        /// </summary>
        /// <param name="commandBehavior"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteDataReader<T>(CommandBehavior commandBehavior, CommandType commandType, string commandText, Func<IDataRecord, T> selector, IEnumerable<KeyValuePair<string, IConvertible>> parameters = null);
    }
}