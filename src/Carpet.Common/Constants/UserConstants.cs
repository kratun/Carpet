namespace Carpet.Common.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class UserConstants
    {
        public const int FirstNameMinValue = 2;
        public const int LastNameMinValue = 2;
        public const int AddressMinLength = 5;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;
        public const string PhoneValidation = "^0[1-9][0-9]{1,8}|\\+[1-9][0-9]{1,14}|00[1-9][0-9]{1,14}$";
        public const string NameValidation = "^([а-яА-Я]{1}[а-яА-Я]*[-| ]?[а-яА-Я]*[а-яА-Я]{1})$";

        public const string DisplayNameFirstName = "Име";
        public const string DisplayNameLastName = "Фамилия";
        public const string DisplayNamePhoneNumber = "Телефон";
        public const string DisplayNameEmail = "Имейл";
        public const string DisplayNamePassword = "Парола";
        public const string DisplayNameConfirmPassword = "Потвърди парола";

        public const string ErrorFieldRequired = "Полето {0} е задължително!";
        public const string ErrorFieldFirstNameRegex = "{0}то трябва да е изписано на кирилица във формат \"Мария\" или \"Анна-Мария\", или \"Анна Мария\" без допълнителни интервали.";
        public const string ErrorFieldLastNameRegex = "{0}та трябва да е изписанa на кирилица във формат \"Иванова\" или \"Иванова-Тодорова\", или \"Иванова Тодорова\" без допълнителни интервали.";
        public const string ErrorFieldFirstNameLength = "{0}то трябва да е изписано на кирилица и да е минимум {1} букви.";
        public const string ErrorFieldLastNameLength = "{0}та трябва да е изписана на кирилица и да е минимум {1} букви.";
        public const string ErrorFieldPasswordMismatch = "Паролата не съвпада.";
        public const string ErrorFieldPasswordLength = "{0}та трабва е поне {2} имаксимум {1} символа.";

        //public const string NullReferenceId = "Не е намерен служител с Id: {0}.";
        //public const string ArgumentExceptionPhoneNumberExist = "Служител с телефон: {0} съществува.";
        //public const string ArgumentExceptionPhoneNumberNotExist = "Потребител с този телефон: {0} не съществува.";
    }
}
