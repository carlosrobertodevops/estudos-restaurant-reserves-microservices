namespace Restaurant.Infrastructure.MessageBus
{
    public class RabbitMQManager : IMessageBusManager
    {
        private readonly string _connectionString;
        private readonly string _createRestaurant;
        private readonly string _deleteUser;
        private readonly string _restaurantDeleted;
        private readonly string _updateUser;
        private readonly string _restaurantUpdated;

        private IBus _messageBus;
        private IAdvancedBus _advancedBus;

        public RabbitMQManager(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("MessageBus");
            _createRestaurant = _configuration["MessageBus:CreateUser"];
            _deleteUser = _configuration["MessageBus:DeleteUser"];
            _restaurantDeleted = _configuration["MessageBus:RestaurantDeleted"];
            _updateUser = _configuration["MessageBus:UpdateUser"];
            _restaurantUpdated = _configuration["MessageBus:RestaurantUpdated"];

            CreateBus();
        }

        public bool IsConnected => _messageBus?.Advanced?.IsConnected ?? false;

        public async Task<CreateUserEventResponse> CreateRestaurantCredentials(CreateUserEvent createRestaurant, CancellationToken cancellationToken)
        {
            TryConnect();

            var restaurantCredentialsCreated = await _messageBus.Rpc.RequestAsync<CreateUserEvent, CreateUserEventResponse>
                (createRestaurant, configure => configure.WithQueueName(_createRestaurant), cancellationToken);

            if (restaurantCredentialsCreated.ValidationResult.IsValid)
            {
                return restaurantCredentialsCreated;
            }

            throw new BusinessException(restaurantCredentialsCreated.ValidationResult.ToDictionary(), "Unable to create restaurant credentials", createRestaurant.CorrelationId);
        }

        public async Task RestaurantDeleted(Guid id, Guid correlationId, CancellationToken cancellationToken)
        {
            await DeleteRestaurantCredentials(id, correlationId, cancellationToken);

            await _messageBus.PubSub.PublishAsync(new RestaurantDeletedEvent(id, correlationId), configure => configure.WithTopic(_restaurantDeleted), cancellationToken);
        }

        public async Task DeleteRestaurantCredentials(Guid id, Guid correlationId, CancellationToken cancellationToken)
        {
            TryConnect();

            await _messageBus.PubSub.PublishAsync(new DeleteUserEvent(id, correlationId), configure => configure.WithTopic(_deleteUser), cancellationToken);
        }

        public async Task RestaurantUpdated(Core.Entities.Restaurant restaurant, Guid correlationId, CancellationToken cancellationToken)
        {
            TryConnect();

            await _messageBus.PubSub.PublishAsync(new RestaurantUpdatedEvent(restaurant.Id, 
                                                                             restaurant.Name, 
                                                                             restaurant.Enabled, 
                                                                             correlationId), configure => configure.WithTopic(_restaurantUpdated), cancellationToken);

            await _messageBus.PubSub.PublishAsync(new UserUpdatedEvent(restaurant.User.FirstName, 
                                                                       restaurant.User.LastName, 
                                                                       restaurant.Id, 
                                                                       correlationId), configure => configure.WithTopic(_updateUser), cancellationToken);
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

        private void CreateBus()
        {
            _messageBus = RabbitHutch.CreateBus(
                connectionString: _connectionString,
                registerServices: s =>
                {
                    s.Register<ITypeNameSerializer, EventBusTypeNameSerializer>();
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
