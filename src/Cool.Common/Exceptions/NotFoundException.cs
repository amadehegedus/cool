using System.Net;

namespace Cool.Common.Exceptions
{
    public class NotFoundException : HttpResponseException
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
