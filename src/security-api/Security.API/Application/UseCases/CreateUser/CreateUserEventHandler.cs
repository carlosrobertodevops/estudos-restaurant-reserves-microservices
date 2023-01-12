using Security.API.Core.ExternalServices;

namespace Security.API.UseCases.CreateUser
{
    public class CreateUserEventHandler : BackgroundService
    {
        private readonly IMessageBusManager _messageBus;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public CreateUserEventHandler(IMessageBusManager messageBus,
                                            IServiceProvider serviceProvider,
                                            IConfiguration configuration)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            SetResponder(cancellationToken);

            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder(CancellationToken.None);
        }

        private void SetResponder(CancellationToken cancellationToken)
        {
            _messageBus.RespondAsync<CreateUserEvent, CreateUserEventResponse>(async (request, requestCancellationToken) => await RespondAsync(request, requestCancellationToken),
                                                                                           configuration =>
                                                                                           {
                                                                                               configuration.WithQueueName(_configuration["MessageBus:CreateUser"]);
                                                                                           },
                                                                                           cancellationToken);

            _messageBus.AdvancedBus.Connected += OnConnect;
        }


        private async Task<CreateUserEventResponse> RespondAsync(CreateUserEvent message, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            return await mediator.Send(new CreateUserRequest(message.AsUserViewModel(), message.CorrelationId), cancellationToken);
        }
    }
}
