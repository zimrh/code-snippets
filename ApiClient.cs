using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeSnippets
{
    internal class ApiClient : IApiClient
    {
        public Task DeleteAsync(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            return SendAsync(HttpMethod.Delete, uri, headers, null, cancellationToken);
        }

        public async Task<T> DeleteAsync<T>(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            var response = await SendAsync(HttpMethod.Delete, uri, headers, null, cancellationToken);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public Task GetAsync(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            return SendAsync(HttpMethod.Get, uri, headers, null, cancellationToken);
        }

        public async Task<T> GetAsync<T>(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            var response = await  SendAsync(HttpMethod.Get, uri, headers, null, cancellationToken);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public Task PostAsync(string uri, IDictionary<string, string> headers, string payload, CancellationToken cancellationToken)
        {
            return SendAsync(HttpMethod.Post, uri, headers, payload, cancellationToken);
        }

        public async Task<T> PostAsync<T>(string uri, IDictionary<string, string> headers, string payload, CancellationToken cancellationToken)
        {
            var response = await SendAsync(HttpMethod.Post, uri, headers, payload, cancellationToken);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public Task PutAsync(string uri, IDictionary<string, string> headers, string payload, CancellationToken cancellationToken)
        {
            return SendAsync(HttpMethod.Put, uri, headers, payload, cancellationToken);
        }

        public async Task<T> PutAsync<T>(string uri, IDictionary<string, string> headers, string payload, CancellationToken cancellationToken)
        {
            var response = await SendAsync(HttpMethod.Put, uri, headers, payload, cancellationToken);
            return JsonConvert.DeserializeObject<T>(response);
        }

        private async Task<string> SendAsync(HttpMethod method, string uri, IDictionary<string, string> headers,
            string payload, CancellationToken cancellationToken)
        {
            using (var httpRequestMessage = new HttpRequestMessage(method, uri))
            {
                if (payload != null)
                {
                    httpRequestMessage.Content = new StringContent(payload, Encoding.UTF8, "application/json");
                }

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        httpRequestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }

                using (var response = await HttpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false))
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException($"Issue when calling {method} {uri}, received {response.StatusCode} {responseContent}");
                    }

                    return responseContent;
                }
            }
        }

        private static readonly HttpClient HttpClient = new HttpClient();
    }
}
