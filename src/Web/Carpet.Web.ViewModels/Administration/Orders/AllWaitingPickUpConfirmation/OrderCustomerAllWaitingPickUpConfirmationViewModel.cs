namespace Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpConfirmation
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderCustomerAllWaitingPickUpConfirmationViewModel : IMapFrom<Customer>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePickUpAddress)]
        public string PickUpAddress { get; set; }
    }
}
