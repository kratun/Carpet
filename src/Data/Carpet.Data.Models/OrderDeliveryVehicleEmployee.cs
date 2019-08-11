namespace Carpet.Data.Models
{
    using Carpet.Data.Common.Models;

    public class OrderDeliveryVehicleEmployee : BaseDeletableModel<string>
    {
        public string OrderId { get; set; }

        public Order Order { get; set; }

        public string VehicleEmployeeId { get; set; }

        public VehicleEmployee VehicleEmployee { get; set; }
    }
}
