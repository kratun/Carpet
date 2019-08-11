namespace Carpet.Data.Models
{
    using System.Collections.Generic;

    using Carpet.Data.Common.Models;

    public class Vehicle : BaseDeletableModel<int>
    {
        public Vehicle()
        {
            this.VehicleEmployees = new HashSet<VehicleEmployee>();
        }

        public string RegistrationNumber { get; set; }

        public bool IsDamaged { get; set; }

        public virtual ICollection<VehicleEmployee> VehicleEmployees { get; set; }
    }
}
