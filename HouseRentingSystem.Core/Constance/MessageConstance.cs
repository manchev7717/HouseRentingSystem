namespace HouseRentingSystem.Core.Constants
{
    public static class MessageConstance
    {
        public const string RequiredErrorMessage = "The field {0} is required";

        public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1} characters long";

        public const string PhoneNumberExistMessage = "Phone number already exists. Enter another one";

        public const string NoRentsToBecomeAgentMessage = "You should have no rents to become an agent!";

        public const string PricePerMonthMessage = "Price Pet Month must be a positive number and less than {2} leva.";

    }
}
