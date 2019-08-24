namespace Carpet.Web.ViewModels.Administration.Orders.PickUpRangeHours
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderVehicleViewModel : IMapFrom<Vehicle>
    {
        public string RegistrationNumber { get; set; }
    }
}
