namespace Carpet.Web.ViewModels.Administration.Employees.AllUsers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    public class EmployeeAllUsersViewModel
    {
        public string Id { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameEmployeeEmail)]
        public string Email { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameCreatedOn)]
        public DateTime CreatedOn { get; set; }
    }
}
