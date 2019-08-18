namespace Carpet.Web.ViewModels.Administration.Garage.Add
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EmployeeAddViewModel : IMapFrom<Employee>
    {
        public EmployeeAddViewModel()
        {
            this.VehicleEmployees = new HashSet<VehicleEmployeeAddViewModel>();
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

        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        public string RegistrationNumber => this.VehicleEmployees.Count == 0 ? string.Empty : this.VehicleEmployees.FirstOrDefault().VehicleRegistrationNumber;

        public ICollection<VehicleEmployeeAddViewModel> VehicleEmployees { get; set; }

        public ICollection<SelectListItem> VehicleList { get; set; }
    }
}
