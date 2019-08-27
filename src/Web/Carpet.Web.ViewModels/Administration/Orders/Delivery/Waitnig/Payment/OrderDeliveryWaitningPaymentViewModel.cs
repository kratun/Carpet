namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Waitnig.Payment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderDeliveryWaitningPaymentViewModel : IMapFrom<Order>, IMapFrom<Customer>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public OrderCustomerDeliveryWaitingPaymentViewModel Customer { get; set; }

        [Display(Name = CustomerConstants.DisplayNameFirstName)]
        public string FullName => string.Concat(this.Customer.FirstName != null ? this.Customer.FirstName + " " : string.Empty, this.Customer.LastName != null ? this.Customer.LastName : string.Empty).Trim();

        [Display(Name = OrderConstants.DisplayNameIsExpress)]
        public string IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameItemQuantitySetByUser)]
        public string ItemQuantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public string StatusName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = OrderConstants.DisplayNameDeliveryDate)]
        public DateTime DeliveringFor { get; set; }

        [Display(Name = OrderConstants.DisplayNameHoursRange)]
        public string HoursRange => this.DeliveringForStartHour + " - " + this.DeliveringForEndHour;

        [Display(Name = OrderConstants.DisplayNameStartHour)]
        public string DeliveringForStartHour { get; set; }

        [Display(Name = OrderConstants.DisplayNameEndHour)]
        public string DeliveringForEndHour { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderDeliveryWaitningPaymentViewModel>()
                .ForMember(
                    destination => destination.StatusName,
                    opts => opts.MapFrom(origin => origin.Status.Name))
                .ForMember(
                    destination => destination.ItemQuantity,
                    opts => opts.MapFrom(origin => origin.OrderItems.Count))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress == true ? GlobalConstants.YesString : GlobalConstants.NoString));
        }
    }
}
