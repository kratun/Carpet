// ReSharper disable VirtualMemberCallInConstructor
namespace Carpet.Data.Models
{
    using System;

    using Carpet.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class CarpetRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public CarpetRole()
            : this(null)
        {
        }

        public CarpetRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
