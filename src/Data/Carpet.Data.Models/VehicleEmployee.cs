namespace Carpet.Data.Models
{
    using Carpet.Data.Common.Models;

    public class VehicleEmployee : BaseDeletableModel<string>
    {
        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
