using System.Net;
using WebBff.API.ViewModels;

namespace WebBff.API.Exceptions
{
    public class BusinessException : Exception
    {
        public ErrorResponseViewModel Error { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public BusinessException(ErrorResponseViewModel error, HttpStatusCode statusCode) : base(error.Message)
        {
            Error = error;
            StatusCode = statusCode;
        }
    }
}
