using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Core.Dispatcher.Notification
{
    internal abstract class NotificationHandlerBase
    {
        public abstract ValueTask Handle(
            object notification,
            IServiceProvider provider,
            CancellationToken cancellationToken);
    }
}
