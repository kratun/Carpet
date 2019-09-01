namespace Carpet.Web.InputModels.Administration.Orders.AddItems
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    public class OrderDeleteItemInputModel
    {
        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        public string Id { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        public string OrderItemId { get; set; }
    }
}
