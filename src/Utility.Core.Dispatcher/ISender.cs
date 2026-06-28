using System.Threading;
using System.Threading.Tasks;

namespace Utility.Core.Dispatcher
{
    public interface ISender
    {
        ValueTask<TResponse> Send<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default);
    }
}
