namespace Carpet.Data.Models
{
    using Carpet.Data.Common.Models;

    public class OrderDeliveryVehicleEmployee : BaseDeletableModel<string>
    {
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string VehicleEmployeeId { get; set; }

        public virtual VehicleEmployee VehicleEmployee { get; set; }
    }
}
