namespace Restaurant.Application.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccessTokenViewModel AccessToken { get; set; }
    }
}
