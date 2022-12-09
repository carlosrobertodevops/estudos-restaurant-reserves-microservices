namespace Restaurant.Application.Services
{
    public interface IRestaurantService
    {
        Task DeleteRestaurant(DeleteRestaurantCommand request);
        Task UpdateRestaurant(UpdateRestaurantCommand request);
    }
}
