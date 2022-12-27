namespace Restaurant.Application.Services
{
    public interface IRestaurantService
    {
        Task DeleteRestaurant(DeleteRestaurantCommand request);
        Task<Core.Entities.Restaurant> UpdateRestaurant(UpdateRestaurantCommand request);
    }
}
