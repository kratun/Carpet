namespace Carpet.Data.Models
{
    using Carpet.Data.Common.Models;

    public class OrderPickUpVehicleEmployee : BaseDeletableModel<string>
    {
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string VehicleEmployeeId { get; set; }

        public virtual VehicleEmployee VehicleEmployee { get; set; }
    }
}
