using System.Net;

namespace Prody.Rest
{
    public class OperationResult<TResult>
    {
        public HttpStatusCode StatusCode { get; set; }

        public TResult Result { get; set; }

        public bool HasResult => Result != null;

        public OperationResult(HttpStatusCode statusCode, TResult result)
        {
            StatusCode = statusCode;
            Result = result;
        }

        public OperationResult(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
