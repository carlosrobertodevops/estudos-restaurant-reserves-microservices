using EasyNetQ;
using EasyNetQ.DI;
using Polly;
using RabbitMQ.Client.Exceptions;
using Security.API.Core.Events;
using Security.Infrastructure.MessageBus.Configurations;

namespace Security.API.Infrastructure.MessageBus
{
    public class RabbitMQManager : IMessageBusManager
    {
        private readonly string _connectionString;

        private IBus _messageBus;
        private IAdvancedBus _advancedBus;

        public RabbitMQManager(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("MessageBus");

            CreateBus();
        }

        public bool IsConnected => _messageBus?.Advanced?.IsConnected ?? false;

        public IAdvancedBus AdvancedBus => _messageBus?.Advanced;

        public async Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, CancellationToken, Task<TResponse>> responder, 
                                                                         Action<IResponderConfiguration> configuration,
                                                                         CancellationToken cancellationToken) 
            where TRequest : Event 
            where TResponse : ResponseMessage
        {
            TryConnect();

            return await _messageBus.Rpc.RespondAsync(responder, configuration, cancellationToken);
        }

        private void CreateBus()
        {
            _messageBus = RabbitHutch.CreateBus(
                connectionString: _connectionString,
                registerServices: s =>
                {
                    s.Register<ITypeNameSerializer, EventBusTypeNameSerializer>();
                });
        }

        private void TryConnect()
        {
            if (IsConnected) 
            {
                return;
            }

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                CreateBus();
                _advancedBus = _messageBus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
            });
        }

        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _messageBus.Dispose();
            }
        }
    }
}
