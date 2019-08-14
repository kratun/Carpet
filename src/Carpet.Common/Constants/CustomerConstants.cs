namespace Carpet.Common.Constants
{
    public static class CustomerConstants
    {
        public const int FirstNameMinValue = 2;
        public const int AddressMinLength = 5;
        public const string PhoneValidation = "^0[1-9][0-9]{1,8}|\\+[1-9][0-9]{1,14}|00[1-9][0-9]{1,14}$";
        public const string NameValidation = "^([а-яА-Я]{1}[а-яА-Я]*[-| ]?[а-яА-Я]*[а-яА-Я]{1})$";

        public const string DisplayNameFirstName = "Име";
        public const string DisplayNameLastName = "Фамилия";
        public const string DisplayNamePhoneNumber = "Телефон";
        public const string DisplayNamePickUpAddress = "Адрес за взинаме";
        public const string DisplayNameDeliveryAddress = "Адрес за доставка";
        public const string DisplayNameCreatedOn = "Създаден на";
        public const string ErrorFieldRequired = "Полето {0} е задължително!";
        public const string ErrorFieldNameRegex = "Името трябва да е във формат \"Мария\" или \"Анна-Мария\", или \"Анна Мария\" без допълнителни интервали.";
        public const string ErrorFieldNameLength = "Името трябва да е минимум {1} букви.";

        public const string NullReferenceCustomerId = "Не е намерен клиент с Id: {0}.";
        public const string ArgumentExceptionCustomerPhone = "Клиент с телефон: {0} съществува.";
    }
}
