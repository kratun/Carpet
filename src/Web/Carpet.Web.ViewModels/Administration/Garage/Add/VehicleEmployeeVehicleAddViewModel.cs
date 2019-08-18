namespace Carpet.Web.ViewModels.Administration.Garage.Add
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    public class VehicleEmployeeVehicleAddViewModel
    {
        [Display(Name = GarageConstants.DisplayNameVehicleRegistrationNumber)]
        public string RegistrationNumber { get; set; }
    }
}
