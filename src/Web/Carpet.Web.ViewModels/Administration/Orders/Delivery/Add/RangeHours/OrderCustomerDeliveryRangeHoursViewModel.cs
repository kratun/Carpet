namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.RangeHours
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderCustomerDeliveryRangeHoursViewModel : IMapFrom<Customer>
    {
        [Display(Name = CustomerConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Display(Name = CustomerConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = CustomerConstants.DisplayNameDeliveryAddress)]
        public string DeliveryAddress { get; set; }
    }
}
