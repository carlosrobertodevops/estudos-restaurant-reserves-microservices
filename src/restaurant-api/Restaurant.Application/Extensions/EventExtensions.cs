namespace Restaurant.Application.Extensions
{
    public static class EventExtensions
    {
        public static CreateUserEvent AsCreateUserEvent(this CreateRestaurantCommand request)
        {
            return new CreateUserEvent(request.Restaurant.User.Username,
                                       request.Restaurant.User.Password,
                                       request.Restaurant.User.FirstName,
                                       request.Restaurant.User.LastName,
                                       request.CorrelationId);
        }

        public static AccessTokenViewModel AsAccessToken(this CreateUserEventResponse response)
        {
            return new AccessTokenViewModel
            {
                AccessToken = response.AccessToken,
                ExpiresIn = response.ExpiresIn,
                RefreshToken = response.RefreshToken,
                Scope = response.Scope,
                NotBeforePolicy = response.NotBeforePolicy,
                RefreshExpiresIn = response.RefreshExpiresIn,
                SessionState = response.SessionState,
                TokenType = response.TokenType
            };
        }

        public static RestaurantViewModel WithAccessToken(this RestaurantViewModel restaurantViewModel, AccessTokenViewModel accessToken)
        {
            restaurantViewModel.User.AccessToken = accessToken;

            return restaurantViewModel;
        }
    }
}
