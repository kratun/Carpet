using Carpet.Data.Models;
using Carpet.Services.Mapping;

namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.RangeHours
{
    public class OrderDeliveryVehicleEmployeeDeliveryAddRangeHoursViewModel : IMapFrom<OrderDeliveryVehicleEmployee>
    {
        public string VehicleEmployeeId { get; set; }

        public VehicleEmployeeDeliveryAddRangeHoursViewModel VehicleEmployee { get; set; }
    }
}