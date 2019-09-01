namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.RangeHours
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderDeliveryVehicleEmployeeDeliveryAddRangeHoursViewModel : IMapFrom<OrderDeliveryVehicleEmployee>
    {
        public string VehicleEmployeeId { get; set; }

        public VehicleEmployeeDeliveryAddRangeHoursViewModel VehicleEmployee { get; set; }
    }
}
