using System.Threading;
using System.Threading.Tasks;

namespace Utility.Core.Dispatcher.Notification.Interfaces
{
    public interface INotificationHandler<in TNotification>
    where TNotification : INotification
    {
        ValueTask Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
