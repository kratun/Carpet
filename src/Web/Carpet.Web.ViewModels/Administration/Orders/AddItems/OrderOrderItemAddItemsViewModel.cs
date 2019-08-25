namespace Carpet.Web.ViewModels.Administration.Orders.AddItems
{
    using System.ComponentModel.DataAnnotations.Schema;

    using AutoMapper;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderOrderItemAddItemsViewModel : IMapFrom<OrderItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public int ItemId { get; set; }

        public OrderOrderItemItemAddItemsViewModel Item { get; set; }

        public decimal ItemWidth { get; set; }

        public decimal ItemHeight { get; set; }

        public decimal ItemArea => this.ItemWidth * this.ItemHeight;

        public bool HasFlavor { get; set; }

        public bool IsExpress { get; set; }

        public bool HasVacuumCleaning { get; set; }

        public decimal TotalPrice { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderOrderItemAddItemsViewModel>()
                .ForMember(
                    destination => destination.HasFlavor,
                    opts => opts.MapFrom(origin => origin.HasFlavor))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress));
        }
    }
}
