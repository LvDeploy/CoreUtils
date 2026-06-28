using System.Threading;
using System.Threading.Tasks;

namespace Utility.Core.Dispatcher.Notification.Interfaces
{
    public interface IPublisher
    {
        ValueTask Publish<TNotification>(
            TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification;
    }
}
