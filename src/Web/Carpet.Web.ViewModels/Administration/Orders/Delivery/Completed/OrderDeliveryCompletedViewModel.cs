namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Completed
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderDeliveryCompletedViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public OrderDeliveryCompletedViewModel()
        {
            this.OrderItems = new HashSet<OrderOrderItemDeliveryCompletedModel>();
        }

        [Display(Name = OrderConstants.DisplayNameId)]
        public string Id { get; set; }

        [Display(Name = OrderConstants.DisplayNameOrderCreateDate)]
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string OrderItemId { get; set; }

        public string OrderInfo { get; set; }

        public string CustomerId { get; set; }

        public OrderCustomerDeliveryCompletedViewModel Customer { get; set; }

        public string CreatorId { get; set; }

        [Display(Name = OrderConstants.DisplayNameIsExpress)]
        public string IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameHasFlavor)]
        public string HasFlavor { get; set; }

        [Display(Name = OrderConstants.DisplayNamePickedUpDate)]
        public DateTime PickUpOn { get; set; }

        [Display(Name = OrderConstants.DisplayNameDeliverOnDate)]
        public DateTime DeliverOn { get; set; }

        [Display(Name = OrderConstants.DisplayNameDescription)]
        public string Description { get; set; }

        public string StatusName { get; set; }

        public int TotalItemQuantity => this.OrderItems.Count;

        public decimal TotalArea => this.OrderItems.Sum(oi => oi.ItemArea);

        public decimal TotalOrderAmount => this.OrderItems.Sum(oi => oi.TotalPrice);

        public ICollection<OrderOrderItemDeliveryCompletedModel> OrderItems { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            var totalPrice = this.OrderItems.Sum(oi => oi.TotalPrice);
            var totalArea = this.OrderItems.Sum(oi => oi.ItemArea);
            result.AppendLine($"{this.Customer.FirstName}{(this.Customer.LastName != null ? " " + this.Customer.LastName : string.Empty)}, {this.Customer.PhoneNumber}, {this.Customer.PickUpAddress}");

            return result.ToString();
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderDeliveryCompletedViewModel>()
                .ForMember(
                    destination => destination.OrderInfo,
                    opts => opts.MapFrom(origin => origin.ToString()))
                .ForMember(
                    destination => destination.StatusName,
                    opts => opts.MapFrom(origin => origin.Status.Name))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress == true ? GlobalConstants.YesString : GlobalConstants.NoString))
                .ForMember(
                    destination => destination.HasFlavor,
                    opts => opts.MapFrom(origin => origin.HasFlavor == true ? GlobalConstants.YesString : GlobalConstants.NoString));
        }
    }
}
