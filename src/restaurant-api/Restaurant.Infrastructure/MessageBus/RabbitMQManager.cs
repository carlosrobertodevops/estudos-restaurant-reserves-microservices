using EasyNetQ;
using EasyNetQ.DI;
using EventBusMessages;
using Microsoft.Extensions.Configuration;
using Polly;
using RabbitMQ.Client.Exceptions;
using Restaurant.Core.Events;
using Restaurant.Core.UseCases.DeleteRestaurant;
using Restaurant.Infrastructure.MessageBus.Configurations;

namespace Restaurant.Infrastructure.MessageBus
{
    public class RabbitMQManager : IMessageBusManager
    {
        private readonly string _connectionString;
        private readonly string _createRestaurant;
        private readonly string _deleteRestaurant;

        private IBus _messageBus;
        private IAdvancedBus _advancedBus;

        public RabbitMQManager(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("MessageBus");
            _createRestaurant = _configuration["MessageBus:CreateUser"];
            _deleteRestaurant = _configuration["MessageBus:DeleteUser"];

            CreateBus();
        }

        public bool IsConnected => _messageBus?.Advanced?.IsConnected ?? false;

        public async Task<CreateRestaurantEventResponse> CreateRestaurantCredentials(CreateRestaurantEvent createRestaurant, CancellationToken cancellationToken)
        {
            TryConnect();

            var restaurantCredentialsCreated = await _messageBus.Rpc.RequestAsync<CreateRestaurantEvent, CreateRestaurantEventResponse>
                (createRestaurant, configure =>
                {
                    configure.WithQueueName(_createRestaurant);
                }, cancellationToken);

            if (restaurantCredentialsCreated.ValidationResult.IsValid)
            {
                return restaurantCredentialsCreated;
            }

            throw new BusinessException(restaurantCredentialsCreated.ValidationResult.ToDictionary(), "Unable to create restaurant credentials", createRestaurant.CorrelationId);
        }

        public async Task DeleteRestaurantCredentials(DeleteRestaurantEvent deleteRestaurant, CancellationToken cancellationToken)
        {
            TryConnect();

            await _messageBus.PubSub.PublishAsync(deleteRestaurant, configure => configure.WithTopic(_deleteRestaurant), cancellationToken);
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
            if (IsConnected) return;

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
