namespace Carpet.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Data.Common.Models;

    public class OrderItem : BaseDeletableModel<string>
    {
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public decimal ItemWidth { get; set; }

        public decimal ItemHeight { get; set; }

        [NotMapped]
        public decimal ItemArea => this.ItemWidth * this.ItemHeight;

        public bool HasVacuumCleaning { get; set; }

        public string Description { get; set; }
    }
}
