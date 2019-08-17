namespace Carpet.Web.InputModels.Administration.Employees.Delete
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class EmployeeDeleteViewModel : IMapFrom<Employee>
    {
        public string Id { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameSalary)]
        public decimal Salary { get; set; }

        public string RoleId { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameEmployeeRoleName)]
        public string RoleName { get; set; }
    }
}
