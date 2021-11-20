using System;
using System.Net;

namespace Cool.Common.Exceptions
{
    public class HttpResponseException : Exception
    {
        public virtual HttpStatusCode StatusCode { get; set; }

        public HttpResponseException(string message) : base(message)
        {
        }
    }
}
