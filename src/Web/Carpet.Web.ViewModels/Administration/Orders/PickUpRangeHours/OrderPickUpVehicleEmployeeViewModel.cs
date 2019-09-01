namespace Carpet.Web.ViewModels.Administration.Orders.PickUpRangeHours
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderPickUpVehicleEmployeeViewModel : IMapFrom<OrderPickUpVehicleEmployee>
    {
        public string VehicleEmployeeId { get; set; }

        public OrderVehicleEmployeeViewModel VehicleEmployee { get; set; }
    }
}
