using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SingleTenantPipelineTriggerTool.Core
{
    public interface IApiClient
    {
        Task DeleteAsync(string uri, IDictionary<string, string> headers,
            CancellationToken cancellationToken);

        Task<T> DeleteAsync<T>(string uri, IDictionary<string, string> headers,
            CancellationToken cancellationToken);

        Task GetAsync(string uri, IDictionary<string, string> headers,
            CancellationToken cancellationToken);

        Task<T> GetAsync<T>(string uri, IDictionary<string, string> headers,
            CancellationToken cancellationToken);

        Task PostAsync(string uri, IDictionary<string, string> headers, string payload,
            CancellationToken cancellationToken);

        Task<T> PostAsync<T>(string uri, IDictionary<string, string> headers, string payload,
            CancellationToken cancellationToken);

        Task PutAsync(string uri, IDictionary<string, string> headers, string payload,
            CancellationToken cancellationToken);

        Task<T> PutAsync<T>(string uri, IDictionary<string, string> headers, string payload,
            CancellationToken cancellationToken);
    }
}
