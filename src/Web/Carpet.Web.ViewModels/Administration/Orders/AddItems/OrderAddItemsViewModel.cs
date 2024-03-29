﻿namespace Carpet.Web.ViewModels.Administration.Orders.AddItems
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
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class OrderAddItemsViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public OrderAddItemsViewModel()
        {
            this.OrderItems = new HashSet<OrderOrderItemAddItemsViewModel>();

            this.ItemList = new HashSet<SelectListItem>();
        }

        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        public string OrderItemId { get; set; }

        public string OrderInfo { get; set; }

        public string CustomerId { get; set; }

        public OrderCustomerAddItemsViewModel Customer { get; set; }

        public string CreatorId { get; set; }

        public DateTime? PickUpOn { get; set; }

        public string IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameItem)]
        public int ItemId { get; set; }

        [Display(Name = OrderConstants.DisplayNameWidth)]
        public decimal ItemWidth { get; set; }

        [Display(Name = OrderConstants.DisplayNameHeight)]
        public decimal ItemHeight { get; set; }

        [Display(Name = OrderConstants.DisplayNameDescription)]
        public string Description { get; set; }

        public string StatusName { get; set; }

        public int TotalItemQuantity => this.OrderItems.Count;

        public decimal TotalArea => this.OrderItems.Sum(oi => oi.ItemArea);

        public decimal TotalOrderAmount => this.OrderItems.Sum(oi => oi.TotalPrice);

        public ICollection<OrderOrderItemAddItemsViewModel> OrderItems { get; set; }

        public ICollection<SelectListItem> ItemList { get; set; }

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
                .CreateMap<Order, OrderAddItemsViewModel>()
                .ForMember(
                    destination => destination.OrderInfo,
                    opts => opts.MapFrom(origin => origin.ToString()))
                .ForMember(
                    destination => destination.StatusName,
                    opts => opts.MapFrom(origin => origin.Status.Name))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress == true ? GlobalConstants.YesString : GlobalConstants.NoString));
        }
    }
}
