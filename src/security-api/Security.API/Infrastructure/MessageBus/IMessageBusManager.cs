using EasyNetQ;
using Security.API.Core.Events;

namespace Security.API.Infrastructure.MessageBus
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
    }
}
