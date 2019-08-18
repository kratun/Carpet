namespace Carpet.Web.ViewModels.Administration.Garage.Remove
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.ViewModels.Administration.Garage.Add;

    public class VehicleEmployeeRemoveViewModel : IMapFrom<VehicleEmployee>
    {
        public int VehicleId { get; set; }

        public virtual string VehicleRegistrationNumber { get; set; }

        public string EmployeeId { get; set; }

        public EmployeeAddViewModel Employee { get; set; }
    }
}
