namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Confirmed.Index
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderOrderItemDeliveryConfirmedIndexViewModel : IMapFrom<OrderItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public int ItemId { get; set; }

        public OrderOrderItemItemDeliveryConfirmedIndexViewModel Item { get; set; }

        [Display(Name = OrderConstants.DisplayNameWidth)]
        public decimal ItemWidth { get; set; }

        [Display(Name = OrderConstants.DisplayNameHeight)]
        public decimal ItemHeight { get; set; }

        [Display(Name = ItemConstants.DisplayNameArea)]
        public decimal ItemArea => this.ItemWidth * this.ItemHeight;

        [Display(Name = OrderConstants.DisplayNameHasFlavor)]
        public bool HasFlavor { get; set; }

        public bool IsExpress { get; set; }

        [Display(Name = ItemConstants.DisplayNameVacuumCleaningAddOnPrice)]
        public string HasVacuumCleaning { get; set; }

        [Display(Name = ItemConstants.DisplayNameTotalPrice)]
        public decimal TotalPrice { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderOrderItemDeliveryConfirmedIndexViewModel>()
                .ForMember(
                    destination => destination.HasFlavor,
                    opts => opts.MapFrom(origin => origin.HasFlavor))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress))
                .ForMember(
                    destination => destination.HasVacuumCleaning,
                    opts => opts.MapFrom(origin => origin.OrderItems.Select(x => x.HasVacuumCleaning == true ? GlobalConstants.YesString : GlobalConstants.NoString)));
        }
    }
}
