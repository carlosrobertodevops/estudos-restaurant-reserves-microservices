using Security.API.Core.Events;

namespace EventBusMessages
{
    public class DeleteUserEvent : Event
    {
        public DeleteUserEvent(Guid id, Guid correlationId) : base(correlationId)
        {
            AggregateId = id;
        }

        public DeleteUserEvent() : base(Guid.NewGuid())
        {

        }
    }
}
