namespace Carpet.Web.InputModels.Administration.Orders.PickUpRangeHours
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Web.InputModels.Common.Attributes;

    public class OrderPickUpRangeHoursInputModel
    {
        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        public string Id { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [DateBefore(OrderConstants.PropertyNamePickUpForEndHour)]
        [Display(Name = OrderConstants.DisplayNameStartHour)]
        public string PickUpForStartHour { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [Display(Name = OrderConstants.DisplayNameEndHour)]
        public string PickUpForEndHour { get; set; }
    }
}
