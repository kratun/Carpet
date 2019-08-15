namespace Carpet.Web.ViewModels.Administration.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class EmployeeIndexViewModel : IMapTo<Employee>, IMapFrom<Employee>
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

        [Display(Name = EmployeeConstants.DisplayNameCreatedOn)]
        public DateTime CreatedOn { get; set; }
    }
}
