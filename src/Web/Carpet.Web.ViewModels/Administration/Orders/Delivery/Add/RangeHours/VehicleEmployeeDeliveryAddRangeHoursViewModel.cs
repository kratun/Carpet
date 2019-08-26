namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.RangeHours
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class VehicleEmployeeDeliveryAddRangeHoursViewModel : IMapFrom<VehicleEmployee>
    {
        public int VehicleId { get; set; }

        public VehicleDeliveryAddRangeHoursViewModel Vehicle { get; set; }

    }
}
