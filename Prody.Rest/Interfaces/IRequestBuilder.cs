using System.Collections.Generic;
using System.Net.Http;

namespace Prody.Rest.Interfaces
{
    public interface IRequestBuilder
    {
        IRequestBuilder SetInitialData(string baseUrlPart);

        IRequestBuilder SetMethod(HttpMethod method);

        IRequestBuilder AddUriPart(string uriPart);

        IRequestBuilder AddQueryParameter<T>(IRequestParameter<T> parameter);

        IRequestBuilder AddQueryParameter<T>(IEnumerable<IRequestParameter<T>> parameter);

        IRequestBuilder AddBodyParameter(IRequestParameter parameter);

        IRequestBuilder AddBodyParameter<T>(IEnumerable<IRequestParameter<T>> parameter);

        IRequestBuilder AddRequestHeader(string name, string value);

        IRequest<T> Build<T>();
    }
}
