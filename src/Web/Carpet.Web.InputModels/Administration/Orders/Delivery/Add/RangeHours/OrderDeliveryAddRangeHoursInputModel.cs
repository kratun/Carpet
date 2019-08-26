namespace Carpet.Web.InputModels.Administration.Orders.Delivery.Add.RangeHours
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Web.InputModels.Common.Attributes;

    public class OrderDeliveryAddRangeHoursInputModel
    {
        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        public string Id { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [DateBefore(OrderConstants.PropertyNameDeliveringForEndHour)]
        [Display(Name = OrderConstants.DisplayNameStartHour)]
        public string DeliveringForStartHour { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [Display(Name = OrderConstants.DisplayNameEndHour)]
        public string DeliveringForEndHour { get; set; }
    }
}
