namespace Carpet.Web.InputModels.Administration.Orders.Create
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderCreateInputModel : IMapTo<Order>, IMapTo<Customer>
    {
        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        public string CustomerId { get; set; }

        [Display(Name = OrderConstants.DisplayNameIsExpress)]
        public bool IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameHasFlavor)]
        public bool HasFlavor { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [Range(OrderConstants.ItemQuantitySetByUserMinValue, OrderConstants.ItemQuantitySetByUserMaxValue, ErrorMessage = OrderConstants.ErrorFieldItemQuantitySetByUserRange)]
        [Display(Name = OrderConstants.DisplayNameItemQuantitySetByUser)]
        public int ItemQuantitySetByUser { get; set; }
    }
}
