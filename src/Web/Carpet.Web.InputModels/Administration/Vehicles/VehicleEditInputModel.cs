namespace Carpet.Web.InputModels.Administration.Vehicles
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.ViewModels.Administration.Vehicles;

    public class VehicleEditInputModel : IMapTo<Vehicle>, IMapFrom<Vehicle>, IMapTo<VehicleEditViewModel>
    {
        [Required(ErrorMessage = VehicleConstants.ErrorFieldRequired)]
        [MinLength(VehicleConstants.MakeMinValue, ErrorMessage = VehicleConstants.ErrorFieldMakeLength)]
        [Display(Name = VehicleConstants.DisplayNameMake)]
        public string Make { get; set; }

        [Required(ErrorMessage = VehicleConstants.ErrorFieldRequired)]
        [MinLength(VehicleConstants.ModelMinValue, ErrorMessage = VehicleConstants.ErrorFieldModelLength)]
        [Display(Name = VehicleConstants.DisplayNameModel)]
        public string Model { get; set; }

        [Required]
        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        [RegularExpression(VehicleConstants.RegistrationNumberValidation, ErrorMessage = VehicleConstants.ErrorFieldRegistrationNumberRegex)]
        public string RegistrationNumber { get; set; }

        [Display(Name = VehicleConstants.DisplayNameIsDamage)]
        public bool IsDamaged { get; set; }
    }
}
