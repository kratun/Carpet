namespace Carpet.Common.Constants
{
    public class GarageConstants
    {
        public const string RegistrationNumberValidation = "^(^[a-zA-z0-9]{6,8}$)$";

        public const string DisplayNameFirstName = "Име";
        public const string DisplayNameLastName = "Фамилия";
        public const string DisplayNamePhoneNumber = "Телефон";
        public const string DisplayNameVehicleRegistrationNumber = "Регистрационен номер";

        public const string ErrorFieldRequired = "Полето {0} е задължително!";

        public const string NullReferenceId = "Не е намерен служител с Id: {0}.";
        public const string ArgumentExceptionVehicleEmployeeExist = "Служителет вече е добавен към кола с рег. номер: {0}.";
        public const string ArgumentExceptionVehicleNotExist = "Превозноро средство с рег. номер {0} не е намерено.";
        public const string ArgumentExceptionVehicleInIncorrectStatus = "Премахнете превозноро средство с рег. номер {0} от съществуващите поръчки за вземане и връщане, преди да премахнете служутеля от него.";
    }
}
