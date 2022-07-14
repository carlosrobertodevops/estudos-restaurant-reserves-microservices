namespace RestaurantReserves.Restaurant.Core.Exceptions
{
    public class ObjectWithoutParamCreationAtemptException : BusinessException
    {
        public ObjectWithoutParamCreationAtemptException(string message = "Object creation without params are not allowed.") 
                                                        : base(message)
        {
        }
    }
}
