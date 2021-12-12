using System;
using System.Runtime.Serialization;

namespace TestProject.Calculator.MathOperations
{
    [Serializable]
    public class MathSyntaxException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public MathSyntaxException()
        {
        }

        public MathSyntaxException(string message) : base(message)
        {
        }

        public MathSyntaxException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MathSyntaxException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}