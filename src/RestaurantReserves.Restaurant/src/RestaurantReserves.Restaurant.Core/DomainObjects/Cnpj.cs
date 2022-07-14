namespace RestaurantReserves.Restaurant.Core.DomainObjects
{
    public class Cnpj
    {
		public const int CNPJ_MAX_LEN = 14;

		public string Number { get; private set; }

		public Cnpj(string number)
		{
			if (!Validate(number)) throw new BusinessException("Invalid document.");

			Number = number.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
		}

		public static bool Validate(string cnpj)
		{
			cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

			int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int sum;
			int rest;
			string digit;
			string tempCnpj;

			if (cnpj.Length != CNPJ_MAX_LEN)
				return false;

			tempCnpj = cnpj.Substring(0, 12);
			sum = 0;
			for (int i = 0; i < 12; i++)
				sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

			rest = (sum % 11);

			if (rest < 2) rest = 0;

			else rest = 11 - rest;

			digit = rest.ToString();
			tempCnpj += digit;

			sum = 0;
			for (int i = 0; i < 13; i++)
				sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

			rest = (sum % 11);

			if (rest < 2) rest = 0;

			else rest = 11 - rest;

			digit += rest.ToString();

			return cnpj.EndsWith(digit);
		}
	}
}
