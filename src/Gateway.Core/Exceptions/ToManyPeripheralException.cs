using System;

namespace Gateway.Exceptions
{
    public class ToManyPeripheralException : Exception
    {
        public ToManyPeripheralException(): base(){}
        public ToManyPeripheralException(string message) : base(message) { }
    }
}
