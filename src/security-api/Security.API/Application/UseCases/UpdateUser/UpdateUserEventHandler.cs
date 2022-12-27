using Security.API.Infrastructure.MessageBus;

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
            SetResponder(cancellationToken);

            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder(CancellationToken.None);
        }

        private void SetResponder(CancellationToken cancellationToken)
        {
            
            //_messageBus.RespondAsync<UpdateUserEvent, UpdateUserEventResponse>(async (request, requestCancellationToken) => await RespondAsync(request, requestCancellationToken),
            //                                                                               configuration =>
            //                                                                               {
            //                                                                                   configuration.WithQueueName(_configuration["MessageBus:UpdateUser"]);
            //                                                                               },
            //                                                                               cancellationToken);

            _messageBus.AdvancedBus.Connected += OnConnect;
        }
    }
}
