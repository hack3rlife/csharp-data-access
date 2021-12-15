using System.Data;
using Moq;

namespace CSharpDataAccess.UnitTest
{
    public static class MockFactoryExtensionMethods
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ///
        public static Mock<IDbCommand> CreateDbCommand<T>(this MockRepository factory, string commandText, CommandType commandType, T parameters) where T : class
        {
            var cmd = factory.Create<IDbCommand>();

            return cmd;
        }
    }
}