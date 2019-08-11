namespace Carpet.Data.Models
{
    using System;
    using System.Collections.Generic;
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

        public int StatusId { get; set; }

        public OrderStatus Status { get; set; }

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

        public int TotalItemQuantity => this.OrderItems.Count;

        public decimal TotalArea => this.OrderItems.Sum(oi => oi.ItemArea);

        public decimal TotalPrice => this.OrderItems.Sum(oi => oi.TotalPrice);

        public bool IsPaid { get; set; }

        public decimal PaidAmount { get; set; }

        public string Description { get; set; }

        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public string CreatorId { get; set; }

        public virtual Employee Creator { get; set; }

        public virtual ICollection<OrderDeliveryVehicleEmployee> DeliveryVehicles { get; set; }

        public virtual ICollection<OrderPickUpVehicleEmployee> PickUpVehicles { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        // public ICollection<VehicleEmployee> VehicleEmployees { get; set; }
        public override string ToString()
        {
            var result = new StringBuilder();
            var totalPrice = this.TotalPrice;
            var totalArea = this.TotalArea;
            var itemTotalPrice = this.OrderItems.Sum(oi => oi.TotalPrice);
            var itemTotalArea = this.OrderItems.Sum(oi => oi.ItemArea);
            result.AppendLine($"Total price: {totalPrice}");
            result.AppendLine($"Total Area: {totalArea}");
            result.AppendLine($"Total price is equal: {totalPrice == itemTotalPrice}");
            result.AppendLine($"Total area is equal: {totalArea == itemTotalArea}");

            return result.ToString();
        }
    }
}
