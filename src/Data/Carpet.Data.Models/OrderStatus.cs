namespace Carpet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Data.Common.Models;

    public class OrderStatus : BaseModel<int>
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
