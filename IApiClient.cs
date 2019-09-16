using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SingleTenantPipelineTriggerTool.Core
{
    public interface IApiClient
    {
        Task<T> DeleteAsync<T>(string uri, IDictionary<string, string> headers,
            CancellationToken cancellationToken = default);

        Task<T> GetAsync<T>(string uri, IDictionary<string, string> headers,
            CancellationToken cancellationToken = default);

        Task<T> PostAsync<T>(string uri, IDictionary<string, string> headers, string payload,
            CancellationToken cancellationToken = default);

        Task<T> PutAsync<T>(string uri, IDictionary<string, string> headers, string payload,
            CancellationToken cancellationToken = default);
    }
}
