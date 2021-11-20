using System.Net;

namespace Cool.Common.Exceptions
{
    public class BadRequestException : HttpResponseException
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

        public BadRequestException(string message) : base(message)
        {
        }
    }
}
