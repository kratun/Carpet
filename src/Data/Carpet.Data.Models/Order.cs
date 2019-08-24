namespace Carpet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using Carpet.Data.Common.Models;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
            this.PickUpVehicles = new HashSet<OrderPickUpVehicleEmployee>();
            this.DeliveryVehicles = new HashSet<OrderDeliveryVehicleEmployee>();
        }

        public bool IsExpress { get; set; }

        public bool HasFlavor { get; set; }

        public DateTime? PickUpFor { get; set; }

        public string PickUpForStartHour { get; set; }

        public string PickUpForEndHour { get; set; }

        public DateTime? PickUpOn { get; set; }

        public DateTime? WashingOn { get; set; }

        public DateTime? DeliveringFor { get; set; }

        public string DeliveringForStartHour { get; set; }

        public string DeliveringForEndHour { get; set; }

        public DateTime? DeliverOn { get; set; }

        public int ItemQuantitySetByUser { get; set; }

        public bool IsPaid { get; set; }

        public decimal PaidAmount { get; set; }

        public string Description { get; set; }

        [Required]
        public int StatusId { get; set; }

        public virtual OrderStatus Status { get; set; }

        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public string CreatorId { get; set; }

        public virtual Employee Creator { get; set; }

        public virtual ICollection<OrderDeliveryVehicleEmployee> DeliveryVehicles { get; set; }

        public virtual ICollection<OrderPickUpVehicleEmployee> PickUpVehicles { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
