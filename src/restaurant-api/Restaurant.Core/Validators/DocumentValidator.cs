namespace Restaurant.Core.Validators
{
    public sealed class DocumentValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override string Name => "DocumentValidator";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            string document = value.ToString().ParseCorrectFormat();

            return document.IsValid();
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{PropertyName} is invalid.";
        }
    }

    public static class DocumentValidatorHelper
    {
        private static readonly int[] _firstMultiplier = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] _secondMultiplier = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public static string ParseCorrectFormat(this string document)
        {
            return document.Trim()
                           .Replace(".", "")
                           .Replace("-", "")
                           .Replace("/", "");
        }

        public static bool IsValid(this string document)
        {
            if (document.Length != 14 || !IsNumbersOnly(document))
            {
                return false;
            }

            string tempDocument = document[..12];

            var rest = ResolveForMultiplier(tempDocument, _firstMultiplier);

            var digit = rest.ToString();

            tempDocument += digit;

            rest = ResolveForMultiplier(tempDocument, _secondMultiplier);

            digit += rest.ToString();

            return document.EndsWith(digit);
        }

        public static bool IsNumbersOnly(string word)
        {
            foreach (char letter in word)
            {
                if (letter < '0' || letter > '9')
                {
                    return false;
                }
            }

            return true;
        }

        private static int ResolveForMultiplier(string tempDocument, int[] multiplier)
        {
            var sum = 0;

            for (int i = 0; i < multiplier.Length; i++)
            {
                sum += int.Parse(tempDocument[i].ToString()) * multiplier[i];
            }

            var rest = sum % 11;

            if (rest < 2)
            {
                return 0;
            }

            return 11 - rest;
        }
    }
}
