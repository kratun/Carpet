namespace Carpet.Web.InputModels.Administration.Orders.AddItems
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    public class OrderAddItemInputModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [Display(Name = OrderConstants.DisplayNameItem)]
        public int ItemId { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [Display(Name = OrderConstants.DisplayNameWidth)]
        public decimal ItemWidth { get; set; }

        [Required(ErrorMessage = OrderConstants.ErrorFieldRequired)]
        [Display(Name = OrderConstants.DisplayNameHeight)]
        public decimal ItemHeight { get; set; }

        [Display(Name = OrderConstants.DisplayNameDescription)]
        public string Description { get; set; }
    }
}
