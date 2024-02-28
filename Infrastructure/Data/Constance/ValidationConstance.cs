namespace HouseRentingSystem.Infrastructure.Data.Constance
{
    public static class ValidationConstance
    {
        public const int CategoryNameMaxLength = 50;

        public const int HouseTitleMinLength = 10;
        public const int HouseTitleMaxLength = 50;

        public const int HouseAddressMinLength = 30;
        public const int HouseAddressMaxLength = 150;

        public const int HouseDescriptionMinLength = 50;
        public const int HouseDescriptionMaxLength = 500;

        public const decimal HousePricePerMonthMinValue = 0.00m;
        public const decimal HousePricePerMonthMaxValue = 2000.00m;

        public const int AgentPhoneNumberMinLength = 7;
        public const int AgentPhoneNumberMaxLength = 15;

    }
}
