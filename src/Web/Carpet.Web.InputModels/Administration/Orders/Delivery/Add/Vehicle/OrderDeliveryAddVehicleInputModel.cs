namespace Carpet.Web.InputModels.Administration.Orders.Delivery.Add.Vehicle
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Web.InputModels.Common.Attributes;

    public class OrderDeliveryAddVehicleInputModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = GarageConstants.ErrorFieldRequired)]
        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = GarageConstants.ErrorFieldRequired)]
        [Display(Name = OrderConstants.DisplayNamePickedUpDate)]
        public DateTime PickUpOn { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [DateAfter("PickUpOn")]
        [Display(Name = OrderConstants.DisplayNameDeliveryDate)]
        public DateTime DeliveringFor { get; set; }

    }
}
