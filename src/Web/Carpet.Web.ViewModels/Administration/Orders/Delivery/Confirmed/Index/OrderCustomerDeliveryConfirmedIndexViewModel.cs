namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Confirmed.Index
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderCustomerDeliveryConfirmedIndexViewModel : IMapFrom<Customer>
    {
        [Display(Name = CustomerConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Display(Name = CustomerConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => string.Concat(this.FirstName != null ? this.FirstName + " " : string.Empty, this.LastName != null ? this.LastName : string.Empty).Trim();

        [Display(Name = CustomerConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePickUpAddress)]
        public string PickUpAddress { get; set; }

        [Display(Name = CustomerConstants.DisplayNameDeliveryAddress)]
        public string DeliveryAddress { get; set; }
    }
}
