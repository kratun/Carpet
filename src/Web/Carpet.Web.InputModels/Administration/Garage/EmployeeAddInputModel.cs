namespace Carpet.Web.InputModels.Administration.Garage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EmployeeAddInputModel : IMapTo<Employee>
    {
        public EmployeeAddInputModel()
        {
            this.VehicleList = new HashSet<SelectListItem>();
        }

        [Required(ErrorMessage = GarageConstants.NullReferenceId)]
        public string Id { get; set; }

        [Display(Name = GarageConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Display(Name = GarageConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [Display(Name = GarageConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        public string VehicleId { get; set; }

        [Required(ErrorMessage = GarageConstants.ErrorFieldRequired)]
        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        [RegularExpression(VehicleConstants.RegistrationNumberValidation, ErrorMessage = VehicleConstants.ErrorFieldRegistrationNumberRegex)]
        public string RegistrationNumber { get; set; }

        public ICollection<SelectListItem> VehicleList { get; set; }
    }
}
