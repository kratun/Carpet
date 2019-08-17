namespace Carpet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Models;

    public class Employee : BaseDeletableModel<string>, INamableEntity
    {
        public Employee()
        {
            this.VehicleEmployees = new HashSet<VehicleEmployee>();
        }

        [Required(ErrorMessage = EmployeeConstants.ErrorFieldRequired)]
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

        public string RoleName { get; set; }

        public string UserId { get; set; }

        public virtual CarpetUser User { get; set; }

        public virtual ICollection<VehicleEmployee> VehicleEmployees { get; set; }
    }
}
