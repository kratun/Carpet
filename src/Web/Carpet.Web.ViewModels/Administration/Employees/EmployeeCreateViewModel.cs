namespace Carpet.Web.ViewModels.Administration.Customers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EmployeeCreateViewModel : IMapTo<Employee>, IMapFrom<Employee>, IMapTo<CarpetUser>, IMapFrom<CarpetUser>
    {
        public EmployeeCreateViewModel()
        {
            this.RoleList = new List<SelectListItem>();
        }

        public string Id { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameSalary)]
        public decimal Salary { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameEmployeeRoleName)]
        public string RoleName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameEmployeeRoleName)]
        public ICollection<SelectListItem> RoleList { get; set; }
    }
}
