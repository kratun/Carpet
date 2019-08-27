namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Confirmed.Index
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderOrderItemItemDeliveryConfirmedIndexViewModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        [Display(Name = OrderConstants.DisplayNameItem)]
        public string Name { get; set; }

        public decimal OrdinaryPrice { get; set; }

        public decimal ExpressAddOnPrice { get; set; }

        public decimal VacuumCleaningAddOnPrice { get; set; }

        public decimal FlavorAddOnPrice { get; set; }
    }
}
