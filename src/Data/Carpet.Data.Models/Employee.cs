namespace Carpet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Data.Common.Models;

    public class Employee : BaseDeletableModel<string>, INamableEntity
    {
        public Employee()
        {
            this.VehicleEmployees = new HashSet<VehicleEmployee>();
        }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName => string.Concat(this.FirstName != null ? this.FirstName + " " : string.Empty, this.LastName != null ? this.LastName : string.Empty).Trim();

        public decimal Salary { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public virtual CarpetUser User { get; set; }

        public virtual ICollection<VehicleEmployee> VehicleEmployees { get; set; }
    }
}
