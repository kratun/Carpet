// ReSharper disable VirtualMemberCallInConstructor
namespace Carpet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class CarpetUser : IdentityUser, IAuditInfo, IDeletableEntity, INamableEntity
    {
        public CarpetUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Customers = new HashSet<Customer>();
            this.Employees = new HashSet<Employee>();
        }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName => string.Concat(this.FirstName != null ? this.FirstName + " " : string.Empty, this.LastName != null ? this.LastName : string.Empty).Trim();

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        // TODO: Add Carpet Collections Customers and Employee
        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
