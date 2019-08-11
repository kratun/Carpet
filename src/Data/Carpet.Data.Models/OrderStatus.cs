namespace Carpet.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Data.Common.Models;

    public class OrderStatus : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
