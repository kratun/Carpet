namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Waitnig.Payment
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderCustomerDeliveryWaitingPaymentViewModel : IMapFrom<Customer>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = CustomerConstants.DisplayNameDeliveryAddress)]
        public string DeliveryAddress { get; set; }
    }
}
