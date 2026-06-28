using System.Threading;
using System.Threading.Tasks;

namespace Utility.Core.Dispatcher
{
    public interface IPipelineBehavior<in TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        ValueTask<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken);
    }
}
