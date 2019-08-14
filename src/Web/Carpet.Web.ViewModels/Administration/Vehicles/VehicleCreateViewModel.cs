namespace Carpet.Web.ViewModels.Administration.Vehicles
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class VehicleCreateViewModel : IMapFrom<Vehicle>
    {
        [Display(Name = VehicleConstants.DisplayNameMake)]
        public string Make { get; set; }

        [Display(Name = VehicleConstants.DisplayNameModel)]
        public string Model { get; set; }

        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        public string RegistrationNumber { get; set; }

        [Display(Name = VehicleConstants.DisplayNameIsDamage)]
        public bool IsDamaged { get; set; }
    }
}
