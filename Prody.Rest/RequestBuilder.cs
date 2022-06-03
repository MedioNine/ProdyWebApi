using Prody.Rest.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Prody.Rest
{
    public class RequestBuilder : IRequestBuilder
    {
        private HttpMethod method;
        private readonly List<string> urlParts;
        private readonly List<IRequestParameter> queryParameters;
        private readonly List<IRequestParameter> bodyParameters;
        private readonly HttpRequestHeaders headers;

        public RequestBuilder()
        {
            urlParts = new List<string>();
            queryParameters = new List<IRequestParameter>();
            bodyParameters = new List<IRequestParameter>();
        }
        public IRequestBuilder AddBodyParameter(IRequestParameter parameter)
        {
            bodyParameters.Add(parameter);
            return this;
        }

        public IRequestBuilder AddBodyParameter<T>(IEnumerable<IRequestParameter<T>> parameters)
        {
            bodyParameters.AddRange(parameters);
            return this;
        }

        public IRequestBuilder AddQueryParameter<T>(IRequestParameter<T> parameter)
        {
            queryParameters.Add(parameter);
            return this;
        }

        public IRequestBuilder AddQueryParameter<T>(IEnumerable<IRequestParameter<T>> parameters)
        {
            queryParameters.AddRange(parameters);
            return this;
        }

        public IRequestBuilder AddUriPart(string uriPart)
        {
            urlParts.Add(uriPart);
            return this;
        }

        public IRequest<T> Build<T>()
        {
            StringBuilder url = new StringBuilder(string.Empty);

            foreach (string part in urlParts)
            {
                url.Append(part);
                url.Append('/');
            }

            if (queryParameters.Any())
            {
                url.Append('?');
                url.Append(string.Join('&', queryParameters.Select(param => string.Join('=', param.Name, param.StringValue))));
            }

            HttpContent content = null;

            if (bodyParameters.Any())
            {
                content = GetContent();
            }

            return new Request<T>(url.ToString(),
                method,
                content,
                headers);
            /*StringBuilder url = new StringBuilder(string.Empty);
            foreach (var uriPart in urlParts)
            {
                url.Append(uriPart);
                url.Append('/');
            }
            url = url.Remove(url.Length - 1, 1);
            StringContent content = new StringContent(JsonConvert.SerializeObject(new { method = "GetMainPageCategories", data = new { merchantId = 1, filialId = 2043, deliveryType = 2, size = 15 } }), Encoding.UTF8, "application/json");

            return new Request<T>(url.ToString(), method,  content, headers)*/
        }

        public IRequestBuilder SetInitialData(string baseUrlPart)
        {
            urlParts.Add(baseUrlPart);
            return this;
        }

        public IRequestBuilder SetMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public IRequestBuilder AddRequestHeader(string name, string value)
        {
            headers.Add(name, value);
            return this;
        }

        private HttpContent GetContent()
        {
            return new StringContent(bodyParameters[0].StringValue, Encoding.UTF8,
                                    "application/json");
        }
    }
}
