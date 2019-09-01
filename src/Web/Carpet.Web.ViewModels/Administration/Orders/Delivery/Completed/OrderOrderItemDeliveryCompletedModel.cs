namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Completed
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderOrderItemDeliveryCompletedModel : IMapFrom<OrderItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public int ItemId { get; set; }

        public OrderOrderItemItemDeliveryCompletedViewModel Item { get; set; }

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
        public bool HasVacuumCleaning { get; set; }

        [Display(Name = ItemConstants.DisplayNameTotalPrice)]
        public decimal TotalPrice => this.GetTotalPrice();

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderOrderItemDeliveryCompletedModel>()
                .ForMember(
                    destination => destination.HasFlavor,
                    opts => opts.MapFrom(origin => origin.HasFlavor))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress));
        }

        private decimal GetTotalPrice()
        {
            var totalPrice = this.Item.OrdinaryPrice;
            if (this.IsExpress)
            {
                totalPrice += this.Item.ExpressAddOnPrice;
            }

            if (this.HasVacuumCleaning)
            {
                totalPrice += this.Item.VacuumCleaningAddOnPrice;
            }

            if (this.HasFlavor)
            {
                totalPrice += this.Item.FlavorAddOnPrice;
            }

            totalPrice *= this.ItemArea;
            return totalPrice;
        }
    }
}
