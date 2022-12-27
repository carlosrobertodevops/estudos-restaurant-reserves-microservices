namespace EventBusMessages
{ 
    public class RestaurantDeletedEvent : Event
    {
        public RestaurantDeletedEvent(Guid id, Guid correlationId) : base(correlationId)
        {
            AggregateId = id;
        }

        public RestaurantDeletedEvent() : base(Guid.NewGuid())
        {

        }
    }
}
