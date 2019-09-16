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
        public Task<T> DeleteAsync<T>(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
        {
            return SendAsync<T>(HttpMethod.Delete, uri, headers, null, cancellationToken);
        }

        public Task<T> GetAsync<T>(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
        {
            return SendAsync<T>(HttpMethod.Get, uri, headers, null, cancellationToken);
        }

        public Task<T> PostAsync<T>(string uri, IDictionary<string, string> headers, string payload, CancellationToken cancellationToken = default)
        {
            return SendAsync<T>(HttpMethod.Post, uri, headers, payload, cancellationToken);
        }

        public Task<T> PutAsync<T>(string uri, IDictionary<string, string> headers, string payload, CancellationToken cancellationToken = default)
        {
            return SendAsync<T>(HttpMethod.Put, uri, headers, payload, cancellationToken);
        }

        private async Task<T> SendAsync<T>(HttpMethod method, string uri, IDictionary<string, string> headers,
            string payload, CancellationToken cancellationToken = default)
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

                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
        }

        private static readonly HttpClient HttpClient = new HttpClient();
    }
}
