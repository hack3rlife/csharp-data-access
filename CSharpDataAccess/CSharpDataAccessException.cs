using System;

namespace CSharpDataAccess
{
    [Serializable]
    public class CSharpException : Exception
    {
        public CSharpException() { }
        public CSharpException(string message) : base(message) { }
        public CSharpException(string message, Exception inner) : base(message, inner) { }
        protected CSharpException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
