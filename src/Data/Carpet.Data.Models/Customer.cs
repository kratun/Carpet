namespace Carpet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Carpet.Data.Common.Models;

    public class Customer : BaseDeletableModel<string>, INamableEntity
    {
        public Customer()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName => string.Concat(this.FirstName != null ? this.FirstName + " " : string.Empty, this.LastName != null ? this.LastName : string.Empty).Trim();

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PickUpAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public string CreatorId { get; set; }

        public virtual Employee Creator { get; set; }

        public string UserId { get; set; }

        public virtual CarpetUser User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
