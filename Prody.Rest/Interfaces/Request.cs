using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Prody.Rest.Interfaces
{
    public class Request<T> : IRequest<T>
    {
        private readonly string url;
        private readonly HttpMethod method;
        private readonly HttpContent content;
        private readonly HttpRequestHeaders headers;

        public Request(string url, HttpMethod method, HttpContent content, HttpRequestHeaders headers)
        {
            this.url = url;
            this.method = method;
            this.content = content;
            this.headers = headers;
        }

        public async Task<OperationResult<T>> Execute(HttpClient client)
        {
            HttpResponseMessage result = await client.SendAsync(CreateMessage());

            try
            {
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    T deserializedResponse = JsonSerializer.Deserialize<T>(response);
                    return new OperationResult<T>(result.StatusCode, deserializedResponse);
                }

                return new OperationResult<T>(result.StatusCode);
            }
            catch (Exception)
            {
                return new OperationResult<T>(result.StatusCode);
            }
        }

        private HttpRequestMessage CreateMessage()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, url);
            if (content != null)
            {
                requestMessage.Content = content;
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }
            return requestMessage;
        }
    }
}
