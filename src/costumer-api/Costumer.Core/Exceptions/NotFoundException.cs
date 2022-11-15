namespace Costumer.Core.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message = "Not found") : base(message)
        {
        }
    }
}
