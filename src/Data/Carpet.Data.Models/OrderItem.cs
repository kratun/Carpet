namespace Carpet.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Data.Common.Models;

    public class OrderItem : BaseDeletableModel<string>
    {
        public int ItemId { get; set; }

        public Item Item { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }

        public decimal ItemWidth { get; set; }

        public decimal ItemHeight { get; set; }

        [NotMapped]
        public decimal ItemArea => this.ItemWidth * this.ItemHeight;

        public bool HasVacuumCleaning { get; set; }

        [NotMapped]
        public decimal TotalPrice => this.GetCurrentPrice();

        public string Description { get; set; }

        private decimal GetCurrentPrice()
        {
            var currentPrice = this.Item.OrdinaryPrice;
            if (this.Order.HasFlavor)
            {
                currentPrice += this.Item.FlavorAddOnPrice;
            }

            if (this.HasVacuumCleaning)
            {
                currentPrice += this.Item.VacuumCleaningAddOnPrice;
            }

            if (this.Order.IsExpress)
            {
                currentPrice += this.Item.ExpressAddOnPrice;
            }

            return currentPrice;
        }
    }
}
