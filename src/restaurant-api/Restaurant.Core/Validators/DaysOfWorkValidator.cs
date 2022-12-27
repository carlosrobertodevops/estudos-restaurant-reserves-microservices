namespace Restaurant.Core.Validators
{
    public sealed class DaysOfWorkValidator<T, TProperty> : PropertyValidator<T, TProperty> where TProperty : ICollection<DayOfWork>
    {
        public override string Name => "DaysOfWorkValidator";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            return value.IsValid();
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{PropertyName} is invalid.";
        }
    }

    public static class DaysOfWorkValidatorHelper
    {
        public static bool IsValid(this ICollection<DayOfWork> daysOfWork)
        {
            return daysOfWork.Select(d => d.DayOfWeek).Distinct().Count() == daysOfWork.Count &&
                   daysOfWork.Any(d => d.IsValid());
        }

        public static bool IsValid(this DayOfWork dayOfWork)
        {
            return dayOfWork.OpensAt <= 24 &&
                   dayOfWork.OpensAt >= 0 &&
                   dayOfWork.ClosesAt <= 24 &&
                   dayOfWork.ClosesAt >= 0 &&
                   dayOfWork.OpensAt < dayOfWork.ClosesAt;
        }
    }
}
