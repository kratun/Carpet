namespace Carpet.Common.Constants
{
    public static class EmployeeConstants
    {
        public const int FirstNameMinValue = 2;
        public const int LastNameMinValue = 2;
        public const int AddressMinLength = 5;
        public const string SalaryMinValue = "1.00";
        public const string SalaryMaxValue = "10000.00";
        public const string PhoneValidation = "^0[1-9][0-9]{1,8}|\\+[1-9][0-9]{1,14}|00[1-9][0-9]{1,14}$";
        public const string NameValidation = "^([а-яА-Я]{1}[а-яА-Я]*[-| ]?[а-яА-Я]*[а-яА-Я]{1})$";

        public const string DisplayNameFirstName = "Име";
        public const string DisplayNameLastName = "Фамилия";
        public const string DisplayNamePhoneNumber = "Телефон";
        public const string DisplayNameSalary = "Заплата";
        public const string DisplayNameCreatedOn = "Създаден на";
        public const string DisplayNameEmployeeRoleName = "Роля";
        public const string DisplayNameEmployeeEmail = "Имейл";

        public const string ErrorFieldRequired = "Полето {0} е задължително!";
        public const string ErrorFieldFirstNameRegex = "{0}то трябва да е изписано на кирилица във формат \"Мария\" или \"Анна-Мария\", или \"Анна Мария\" без допълнителни интервали.";
        public const string ErrorFieldLastNameRegex = "{0}та трябва да е изписанa на кирилица във формат \"Иванова\" или \"Иванова-Тодорова\", или \"Иванова Тодорова\" без допълнителни интервали.";
        public const string ErrorFieldFirstNameLength = "{0}то трябва да е изписано на кирилица и да е минимум {1} букви.";
        public const string ErrorFieldLastNameLength = "{0}та трябва да е изписана на кирилица и да е минимум {1} букви.";
        public const string ErrorFieldSalaryRange = "{0}та може да е между {1} лв. и {2} лв.";

        public const string NullReferenceId = "Не е намерен служител с Id: {0}.";
        public const string ArgumentExceptionPhoneNumberExist = "Служител с телефон: {0} съществува.";
        public const string ArgumentExceptionPhoneNumberNotExist = "Потребител с този телефон: {0} не съществува.";
    }
}
