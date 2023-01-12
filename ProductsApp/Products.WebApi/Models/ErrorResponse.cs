using System.Net;

namespace Products.WebApi.Models
{
    public class ErrorResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Error { get; set; }
    }
}
