using RestaurantReserves.Restaurant.Core.Exceptions;

namespace RestaurantReserves.Restaurant.Core.ValueObjects
{
    public class Phone
    {
        public string AreaCode { get; private set; }
        public string Number { get; private set; }

        public Phone(string areaCode, string number)
        {
            AreaCode = areaCode;
            Number = number;

            Validate();
        }

        private void Validate()
        {
            if (!int.TryParse(AreaCode, out int areaCodeInt) ||
                areaCodeInt <= 0 ||
                string.IsNullOrWhiteSpace(AreaCode) ||
                !long.TryParse(Number, out long numberLong) ||
                numberLong <= 0 || string.IsNullOrWhiteSpace(Number))
                throw new BusinessException("Invalid phone.");
        }
    }
}
