namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Waitnig.RangeHours
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderDeliveryWaitingRangeHoursViewModel : IMapFrom<Order>, IMapFrom<Customer>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public OrderCustomerDeliveryWaitingRangeHoursViewModel Customer { get; set; }

        [Display(Name = OrderConstants.DisplayNameIsExpress)]
        public string IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameItemQuantity)]
        public int ItemQuantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime PickUpOn { get; set; }

        public string StatusName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderDeliveryWaitingRangeHoursViewModel>()
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
