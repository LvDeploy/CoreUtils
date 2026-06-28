using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utility.Core.Dispatcher.Notification.Interfaces;

namespace Utility.Core.Dispatcher.Notification
{
    internal sealed class NotificationHandlerWrapper<TNotification> : NotificationHandlerBase
    where TNotification : INotification
    {
        public override async ValueTask Handle(
            object notification,
            IServiceProvider provider,
            CancellationToken cancellationToken)
        {
            var typed = (TNotification)notification;
            var handlers = provider.GetServices<INotificationHandler<TNotification>>();

            foreach (var handler in handlers)
            {
                await handler.Handle(typed, cancellationToken);
            }
        }
    }
}
