using Prody.Rest.Interfaces;

namespace Prody.Rest
{
    public class RequestBuilderFactory : IRequestBuilderFactory
    {
        public IRequestBuilder Get()
        {
            return new RequestBuilder();
        }
    }
}
