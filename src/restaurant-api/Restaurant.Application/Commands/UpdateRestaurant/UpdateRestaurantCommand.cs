﻿namespace Restaurant.Application.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand : IRequest
    {
        public Guid Id { get; private set; }
        public RestaurantViewModel Restaurant { get; private set; }

        public UpdateRestaurantCommand(Guid id, RestaurantViewModel restaurant)
        {
            Id = id;
            Restaurant = restaurant;
        }
    }
}