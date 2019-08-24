namespace Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpFromCustomer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderAllWaitingPickUpFromCustomerViewModel : IMapFrom<Order>, IMapFrom<Customer>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public OrderCustomerAllWaitingPickUpFromCustomerViewModel Customer { get; set; }

        [Display(Name = CustomerConstants.DisplayNameFirstName)]
        public string FullName => string.Concat(this.Customer.FirstName != null ? this.Customer.FirstName + " " : string.Empty, this.Customer.LastName != null ? this.Customer.LastName : string.Empty).Trim();

        [Display(Name = OrderConstants.DisplayNameIsExpress)]
        public string IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameItemQuantitySetByUser)]
        public int ItemQuantitySetByUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public string StatusName { get; set; }

        [Display(Name = OrderConstants.DisplayNamePickUpDate)]
        public DateTime PickUpFor { get; set; }

        [Display(Name = OrderConstants.DisplayNameHoursRange)]
        public string HoursRange => this.PickUpForStartHour + " - " + this.PickUpForEndHour;

        [Display(Name = OrderConstants.DisplayNamePickUpForStartHour)]
        public string PickUpForStartHour { get; set; }

        [Display(Name = OrderConstants.DisplayNamePickUpForEndHour)]
        public string PickUpForEndHour { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderAllWaitingPickUpFromCustomerViewModel>()
                .ForMember(
                    destination => destination.StatusName,
                    opts => opts.MapFrom(origin => origin.Status.Name))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress == true ? GlobalConstants.YesString : GlobalConstants.NoString));
        }
    }
}
