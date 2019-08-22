namespace Carpet.Web.InputModels.Administration.Orders.AddVehicleForPickUp
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    public class OrderAddVehicleForPickUpInputModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = GarageConstants.ErrorFieldRequired)]
        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [Display(Name = OrderConstants.DisplayNamePickUpDate)]
        public DateTime PickUpFor { get; set; }
    }
}
