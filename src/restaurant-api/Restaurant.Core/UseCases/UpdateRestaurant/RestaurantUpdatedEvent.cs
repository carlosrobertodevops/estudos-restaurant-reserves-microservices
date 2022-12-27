namespace EventBusMessages
{
    public sealed class RestaurantUpdatedEvent : Event
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public RestaurantUpdatedEvent(Guid id, 
                                      string name,
                                      bool enabled,
                                      Guid correlationId) : base(correlationId)
        {
            AggregateId = id;
            Name = name;
            Enabled = enabled;
        }

        public RestaurantUpdatedEvent() : base(Guid.Empty)
        {

        }
    }
}
