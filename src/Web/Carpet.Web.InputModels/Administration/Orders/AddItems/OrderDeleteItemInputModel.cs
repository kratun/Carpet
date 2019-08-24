using Carpet.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Carpet.Web.InputModels.Administration.Orders.AddItems
{
    public class OrderDeleteItemInputModel
    {
        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        public string Id { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        public string OrderItemId { get; set; }
    }
}
