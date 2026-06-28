using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Utility.Core.Dispatcher.Notification;
using Utility.Core.Dispatcher.Notification.Interfaces;

namespace Utility.Core.Dispatcher
{
    internal sealed class Dispatcher : ISender, IPublisher
    {
        private readonly DispatcherRegistry _registry;
        private readonly IServiceProvider _provider;
        public Dispatcher(IServiceProvider provider, DispatcherRegistry registry)
        {
            _provider = provider;
            _registry = registry;
        }
        public ValueTask<TResponse> Send<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException();

            if (!_registry.RequestWrappers.TryGetValue(request.GetType(), out var wrapper))
            {
                throw new InvalidOperationException(
                    $"No handler found for '{request.GetType().FullName}'.");
            }

            return ((RequestHandlerBase<TResponse>)wrapper).Handle(request, _provider, cancellationToken);
        }

        public ValueTask Publish<TNotification>(
            TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            if (notification is null) throw new ArgumentNullException();

            if (!_registry.NotificationWrappers.TryGetValue(notification.GetType(), out var wrapper))
            {
                return default;
            }

            return wrapper.Handle(notification, _provider, cancellationToken);
        }
    }
    internal sealed class DispatcherRegistry
    {
        public DispatcherRegistry(ImmutableDictionary<Type, RequestHandlerBase> requestWrappers, ImmutableDictionary<Type, NotificationHandlerBase> notificationWrappers)
        {
            RequestWrappers = requestWrappers;
            NotificationWrappers = notificationWrappers;
        }
        public ImmutableDictionary<Type, RequestHandlerBase> RequestWrappers { get; }
        public ImmutableDictionary<Type, NotificationHandlerBase> NotificationWrappers { get; }
    }
}
