namespace Carpet.Web.InputModels.Administration.Garage
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    public class EmployeeRemoveInputModel
    {
        [Required(ErrorMessage = GarageConstants.ErrorFieldRequired)]
        public string Id { get; set; }

        [Required(ErrorMessage = GarageConstants.ErrorFieldRequired)]
        [RegularExpression(VehicleConstants.RegistrationNumberValidation, ErrorMessage = VehicleConstants.ErrorFieldRegistrationNumberRegex)]
        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        public string RegistrationNumber { get; set; }
    }
}
