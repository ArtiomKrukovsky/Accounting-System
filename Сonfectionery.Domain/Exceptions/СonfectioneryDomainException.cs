using System;

namespace Сonfectionery.Domain.Exceptions
{
    public class СonfectioneryDomainException : Exception
    {
        public СonfectioneryDomainException()
        { }

        public СonfectioneryDomainException(string message)
            : base(message)
        { }

        public СonfectioneryDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
