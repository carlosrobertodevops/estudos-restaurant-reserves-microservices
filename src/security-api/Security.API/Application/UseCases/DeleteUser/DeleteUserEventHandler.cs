using Security.API.Core.ExternalServices;

namespace Security.API.Application.UseCases.DeleteUser
{
    public class DeleteUserEventHandler : BackgroundService
    {
        private readonly IMessageBusManager _messageBus;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DeleteUserEventHandler(IMessageBusManager messageBus,
                                      IServiceProvider serviceProvider,
                                      IConfiguration configuration)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _messageBus.SubscribeAsync<DeleteUserEvent>(_configuration["MessageBus:DeleteUser"], 
                                                        async (request, requestCancellationToken) => await DeleteUser(request, requestCancellationToken),
                                                        configuration => configuration.WithQueueName(_configuration["MessageBus:DeleteUser"]),
                                                        cancellationToken);

            return Task.CompletedTask;
        }

        private async Task DeleteUser(DeleteUserEvent message, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new DeleteUserRequest(message.AggregateId, message.CorrelationId), cancellationToken);
        }
    }
}
