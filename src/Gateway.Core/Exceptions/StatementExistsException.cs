using System;

namespace Gateway.Exceptions
{
    public class StatementExistsException : Exception
    {
        public StatementExistsException():base(){}
        public StatementExistsException(string message) : base(message) { }
    }
}
