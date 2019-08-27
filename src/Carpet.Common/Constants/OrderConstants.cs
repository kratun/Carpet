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
        public const string StatusDeliveryArrangeDayWaiting = "Чакаща организиране на ден за връщане";
        public const string StatusDeliveryArrangeHourRangeWaiting = "Чакаща организиране на час за връщане";
        public const string StatusDeliveryArrangedDateWaiting = "Чакаща потвърждение от клиент на дата и часови диапазон за връщане";
        public const string StatusDeliveryArrangedDateCоnfirmed = "Потвърдени от клиент дата и часови диапазон за връщане";
        public const string StatusDeliverConfirmed = "Предаден на клиент поръчка";
        public const string StatusPaymentConfirmed = "Потвърдено плащане от клиент";
        public const string StatusInsallmentPayment = "Частично платена от клиент";
        public const string StatusWaitingPickUpByCustomer = "Чака вземане от офис";
        public const string StatusUnclaimedItems = "Непотърсени вещи";
        public const string StatusScrappedItems = "Бракувани вещи";

        public const int ItemQuantitySetByUserMinValue = 1;
        public const int ItemQuantitySetByUserMaxValue = 50;
        public const int AddIntOne = 1;

        public const string DisplayNameIsExpress = "Експресна";
        public const string DisplayNameHasFlavor = "Ароматизиране";
        public const string DisplayNameItemQuantitySetByUser = "Брой артикули";
        public const string DisplayNameItemQuantity = "Брой артикули";
        public const string DisplayNameCreatedOn = "Създаден на";
        public const string DisplayNameStatus = "Статус на поръчка";
        public const string DisplayNamePickUpDate = "Дата за вземане";
        public const string DisplayNamePickedUpDate = "Дата на вземане";
        public const string DisplayNameDeliveryDate = "Дата за връщане";
        public const string DisplayNameDeliverOnDate = "Дата на връщане";
        public const string DisplayNameStartHour = "Начален час";
        public const string DisplayNameEndHour = "Краен час";
        public const string DisplayNameHoursRange = "Часови диапазон";
        public const string DisplayNameWidth = "Дължина";
        public const string DisplayNameHeight = "Ширина";
        public const string DisplayNameDescription = "Бележки";
        public const string DisplayNameItem = "Услуга";
        public const string DisplayNameTotalArea = "Обща площ";
        public const string DisplayNameId = "Номер на поръчка";

        public const string PropertyNamePickUpForEndHour = "PickUpForEndHour";
        public const string PropertyNameDeliveringForEndHour = "DeliveringForEndHour";

        public const string ErrorFieldRequired = "Полето {0} е задължително!";
        public const string ErrorFieldItemQuantitySetByUserRange = "Полето {0} може да бъде в диапазона {1} - {2}";
        public const string ErrorFieldPickUpForStartHour = "Началният час съвпада или е след крайния час.";
        public const string ErrorDateBeforeAttribute = "Началният час съвпада или е след крайния час.";
        public const string ErrorDateAfterAttribute = "Датата за връщане съвпада или е преди датата на вземане.";

        public const string NullReferenceCustomerId = "Не е намерен клиент с Id: {0}.";
        public const string NullReferenceStatusNameNotFound = "Не е намерен Статус с име: {0}.";
        public const string NullReferenceCreatorUsernameNotFound = "Не е намерен Служител с Username: {0}.";
        public const string NullReferenceOrderIdNotFound = "Не е намерена поръчка с ID: {0}.";

        public const string HourList = "07:00;08:00;09:00;10:00;11:00;12:00;13:00;14:00;15:00;16:00;17:00;18:00;19:00;20:00;";
    }
}
