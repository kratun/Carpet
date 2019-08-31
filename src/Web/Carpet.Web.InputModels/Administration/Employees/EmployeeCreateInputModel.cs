namespace Carpet.Web.InputModels.Administration.Customers
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.ViewModels.Administration.Customers;

    public class EmployeeCreateInputModel : IMapTo<Employee>, IMapFrom<Employee>, IMapTo<EmployeeCreateViewModel>
    {
        public string Id { get; set; }

        [Required(ErrorMessage =EmployeeConstants.ErrorFieldRequired)]
        [MinLength(EmployeeConstants.FirstNameMinValue, ErrorMessage = EmployeeConstants.ErrorFieldFirstNameLength)]
        [RegularExpression(EmployeeConstants.NameValidation, ErrorMessage = EmployeeConstants.ErrorFieldFirstNameRegex)]
        [Display(Name = EmployeeConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = EmployeeConstants.ErrorFieldRequired)]
        [MinLength(EmployeeConstants.LastNameMinValue, ErrorMessage = EmployeeConstants.ErrorFieldLastNameLength)]
        [RegularExpression(EmployeeConstants.NameValidation, ErrorMessage = EmployeeConstants.ErrorFieldLastNameRegex)]
        [Display(Name = EmployeeConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => string.Concat(this.FirstName != null ? this.FirstName + " " : string.Empty, this.LastName != null ? this.LastName : string.Empty).Trim();

        [Required(ErrorMessage = EmployeeConstants.ErrorFieldRequired)]
        [RegularExpression(EmployeeConstants.PhoneValidation)]
        [Display(Name = EmployeeConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = EmployeeConstants.ErrorFieldRequired)]
        [Range(typeof(decimal), EmployeeConstants.SalaryMinValue, EmployeeConstants.SalaryMaxValue, ErrorMessage = EmployeeConstants.ErrorFieldSalaryRange)]
        [Display(Name = EmployeeConstants.DisplayNameSalary)]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = EmployeeConstants.ErrorFieldRequired)]
        [Display(Name = EmployeeConstants.DisplayNameEmployeeRoleName)]
        public string RoleName { get; set; }
    }
}
