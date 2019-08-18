namespace Carpet.Web.ViewModels.Administration.Garage.Add
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class VehicleEmployeeAddViewModel : IMapFrom<VehicleEmployee>
    {
        public int VehicleId { get; set; }

        public virtual string VehicleRegistrationNumber { get; set; }

        public string EmployeeId { get; set; }

        public EmployeeAddViewModel Employee { get; set; }
    }
}
