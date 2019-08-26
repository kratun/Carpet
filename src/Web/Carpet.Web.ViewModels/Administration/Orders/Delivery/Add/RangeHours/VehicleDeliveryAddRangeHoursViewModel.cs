namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.RangeHours
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class VehicleDeliveryAddRangeHoursViewModel : IMapFrom<Vehicle>
    {
        public string RegistrationNumber { get; set; }
    }
}
