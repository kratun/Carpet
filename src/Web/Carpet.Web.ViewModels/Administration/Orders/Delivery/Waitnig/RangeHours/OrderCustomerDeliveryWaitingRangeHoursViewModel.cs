namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Waitnig.RangeHours
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderCustomerDeliveryWaitingRangeHoursViewModel : IMapFrom<Customer>
    {
        [Display(Name = CustomerConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePickUpAddress)]
        public string DeliveryAddress { get; set; }
    }
}
