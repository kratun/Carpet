namespace Carpet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Data.Common.Models;

    public class Item : BaseDeletableModel<int>
    {
        public Item()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        [Required]
        public string Name { get; set; }

        public decimal OrdinaryPrice { get; set; }

        public decimal ExpressAddOnPrice { get; set; }

        public decimal VacuumCleaningAddOnPrice { get; set; }

        public decimal FlavorAddOnPrice { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
