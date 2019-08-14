namespace Carpet.Common.Constants
{
    public static class VehicleConstants
    {
        public const int MakeMinValue = 2;
        public const int ModelMinValue = 2;
        public const string RegistrationNumberValidation = "^(^[a-zA-z0-9]{6,8}$)$";

        public const string DisplayNameMake = "Марка";
        public const string DisplayNameModel = "Модел";
        public const string DisplayNameRegistrationNumber = "Регистрационен номер";
        public const string DisplayNameIsDamage = "Повреден";
        public const string DisplayNameCreatedOn = "Закупен на";
        public const string ErrorFieldRequired = "Полето {0} е задължително!";
        public const string ErrorFieldRegistrationNumberRegex = "Номерът може да бъде между 6 и 8 знака (латински букви и или цифри). Например CW9977YF, CW999999, 0000000.";
        public const string ErrorFieldMakeLength = "Марката трябва да е минимум {1} букви.";
        public const string ErrorFieldModelLength = "Моделът трябва да е минимум {1} букви и цифри.";

        public const string NullReferenceId = "Не е намерен превозно средство с Id: {0}.";
        public const string ArgumentExceptionRegistrationNumber = "Превозно средство с рег. номер: {0} вече съществува.";
    }
}
