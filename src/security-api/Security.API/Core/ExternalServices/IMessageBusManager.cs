using EasyNetQ;
using Security.API.Core.Events;

namespace Security.API.Core.ExternalServices
{
    public interface IMessageBusManager : IDisposable
    {
        bool IsConnected { get; }
        IAdvancedBus AdvancedBus { get; }

        Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, CancellationToken, Task<TResponse>> responder,
                                                            Action<IResponderConfiguration> configuration,
                                                            CancellationToken cancellationToken)
                                                            where TRequest : Event
                                                            where TResponse : ResponseMessage;

        Task SubscribeAsync<T>(string subscriptionId,
                                          Func<T, CancellationToken, Task> onMessage,
                                          Action<ISubscriptionConfiguration> configuration,
                                          CancellationToken cancellationToken)
                                          where T : Event;
    }
}
