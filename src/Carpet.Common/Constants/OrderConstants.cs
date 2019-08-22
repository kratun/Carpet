namespace Carpet.Common.Constants
{
    public class OrderConstants
    {
        // Order Statuses
        public const string StatusPickUpArrangeDayWaiting = "Чакаща организиране на ден за вземане";
        public const string StatusPickUpArrangeHourRangeWaiting = "Чакаща организиране на час за вземане";
        public const string StatusPickUpArrangedDateWaiting = "Чакаща потвърждение от клиент на дата и часови диапазон за вземане";
        public const string StatusPickUpArrangedDateCоnfirmed = "Потвърдени от клиент дата и часови диапазон за вземане";
        public const string StatusPickedUpConfirm = "Взета от клиент поръчка";
        public const string StatusWashingProcessing = "Вашата поръчка се обработва. След приключване, ще се свържем с Вас за да организираме ден на доставка.";
        public const string StatusWashed = "Очаквайте връзка с оператор за организиране на ден и час на доставка.";
        public const string StatusDeliveryArrangeDayWaiting = "Чакаща организиране на ден за предаване";
        public const string StatusDeliveryArrangeHourRangeWaiting = "Чакаща организиране на час за предаване";
        public const string StatusDeliveryArrangedDateCоnfirmed = "Потвърдени от клиент дата и часови диапазон за предаване";
        public const string StatusDeliverConfirmed = "Предаден на клиент поръчка";
        public const string StatusPaymentConfirmed = "Потвърдено плащане от клиент";
        public const string StatusInsallmentPayment = "Частично платена от клиент";
        public const string StatusWaitingPickUpByCustomer = "Чака взимане от офис";
        public const string StatusUnclaimedItems = "Непотърсени вещи";
        public const string StatusScrappedItems = "Бракувани вещи";

        public const int ItemQuantitySetByUserMinValue = 1;
        public const int ItemQuantitySetByUserMaxValue = 50;
        public const int AddIntOne = 1;

        //public const int AddressMinLength = 5;
        //public const string PhoneValidation = "^0[1-9][0-9]{1,8}|\\+[1-9][0-9]{1,14}|00[1-9][0-9]{1,14}$";
        //public const string NameValidation = "^([а-яА-Я]{1}[а-яА-Я]*[-| ]?[а-яА-Я]*[а-яА-Я]{1})$";

        public const string DisplayNameIsExpress = "Експресна";
        public const string DisplayNameHasFlavor = "Ароматизиране";
        public const string DisplayNameItemQuantitySetByUser = "Брой артикули";
        public const string DisplayNameCreatedOn = "Създаден на";
        public const string DisplayNameStatus = "Статус на поръчка";
        public const string DisplayNamePickUpDate = "Дата за взимане";

        public const string ErrorFieldRequired = "Полето {0} е задължително!";
        public const string ErrorFieldItemQuantitySetByUserRange = "Полето {0} може да бъде в диапазона {1} - {2}";
        //public const string ErrorFieldLastNameRegex = "{0}та трябва да е изписанa на кирилица във формат \"Иванова\" или \"Иванова-Тодорова\", или \"Иванова Тодорова\" без допълнителни интервали.";
        //public const string ErrorFieldNameLength = "{0}то трябва да е изписано на кирилица и да е минимум {1} букви.";

        public const string NullReferenceCustomerId = "Не е намерен клиент с Id: {0}.";
        public const string NullReferenceStatusNameNotFound = "Не е намерен Статус с име: {0}.";
        public const string NullReferenceCreatorUsernameNotFound = "Не е намерен Служител с Username: {0}.";
        public const string NullReferenceOrderIdNotFound = "Не е намерена поръчка с ID: {0}.";
        //public const string ArgumentExceptionCustomerPhone = "Клиент с телефон: {0} съществува. Моля добавете нов адрес към него.";
        //public const string ArgumentExceptionCustomerExistAddAddress = "Клиент с тези данни съществува. Моля добавете нов адрес към него.";
        //public const string ArgumentExceptionCustomerExist = "Клиент с тези данни съществува или не сте направили редакция на текущия клиент.";
    }
}
