namespace Carpet.Web.ViewModels.Administration.Orders.PickUpRangeHours
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderVehicleEmployeeViewModel : IMapFrom<VehicleEmployee>
    {
        public int VehicleId { get; set; }

        public OrderVehicleViewModel Vehicle { get; set; }
    }
}
