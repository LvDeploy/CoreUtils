using System.Threading;
using System.Threading.Tasks;

namespace Utility.Core.Dispatcher
{
    public interface IRequestHandler<in TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
    public interface IRequestHandler<in TRequest> : IRequestHandler<TRequest, Unit>
    where TRequest : IRequest<Unit>
    { }

    public delegate ValueTask<TResponse> RequestHandlerDelegate<TResponse>();
}
