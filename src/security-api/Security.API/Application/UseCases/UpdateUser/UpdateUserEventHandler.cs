using Security.API.Application.UseCases.UpdateUser;
using Security.API.Core.ExternalServices;

namespace EventBusMessages
{
    public class UpdateUserEventHandler : BackgroundService
    {
        private readonly IMessageBusManager _messageBus;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public UpdateUserEventHandler(IMessageBusManager messageBus,
                                      IServiceProvider serviceProvider,
                                      IConfiguration configuration)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _messageBus.SubscribeAsync<UpdateUserEvent>(_configuration["MessageBus:UpdateUser"],
                                                        async (request, requestCancellationToken) => await DeleteUser(request, requestCancellationToken),
                                                        configuration => configuration.WithQueueName(_configuration["MessageBus:UpdateUser"]),
                                                        cancellationToken);

            return Task.CompletedTask;
        }

        private async Task DeleteUser(UpdateUserEvent message, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new UpdateUserRequest(message.AggregateId, message.FirstName, message.LastName, message.CorrelationId), cancellationToken);
        }
    }
}
