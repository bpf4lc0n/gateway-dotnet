using System;

namespace Gateway.Exceptions
{
    public class Ipv4InvalidException : Exception
    {
        public Ipv4InvalidException(): base(){}
        public Ipv4InvalidException(string message) : base(message) { }
    }
}
