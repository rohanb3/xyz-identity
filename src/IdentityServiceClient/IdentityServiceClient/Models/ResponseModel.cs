using System.Net;

namespace IdentityServiceClient.Models
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Payload { get; set; }
    }
}
