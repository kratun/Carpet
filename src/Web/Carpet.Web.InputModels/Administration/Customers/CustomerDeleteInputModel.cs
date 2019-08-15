namespace Carpet.Web.InputModels.Administration.Customers
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class CustomerDeleteInputModel : IMapTo<Customer>, IMapFrom<Customer>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = CustomerConstants.ErrorFieldRequired)]
        [MinLength(CustomerConstants.FirstNameMinValue, ErrorMessage = CustomerConstants.ErrorFieldNameLength)]
        [RegularExpression(CustomerConstants.NameValidation, ErrorMessage = CustomerConstants.ErrorFieldFirstNameRegex)]
        [Display(Name = CustomerConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [RegularExpression(CustomerConstants.NameValidation, ErrorMessage = CustomerConstants.ErrorFieldLastNameRegex)]
        [Display(Name = CustomerConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => string.Concat(this.FirstName != null ? this.FirstName + " " : string.Empty, this.LastName != null ? this.LastName : string.Empty).Trim();

        [Required]
        [Display(Name = CustomerConstants.DisplayNamePhoneNumber)]
        [RegularExpression(CustomerConstants.PhoneValidation)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = CustomerConstants.DisplayNamePickUpAddress)]
        [MinLength(CustomerConstants.AddressMinLength)]
        public string PickUpAddress { get; set; }

        [Display(Name = CustomerConstants.DisplayNameDeliveryAddress)]
        public string DeliveryAddress { get; set; }
    }
}
