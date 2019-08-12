namespace Carpet.Web.InputModels.Administration.Items
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class ItemEditInputModel : IMapTo<Item>, IMapFrom<Item>
    {
        [Required]
        [Display(Name = ItemConstants.DisplayNameName)]
        public string Name { get; set; }

        [Display(Name = ItemConstants.DisplayNameOrdinaryPrice)]
        [Range(typeof(decimal), ItemConstants.PriceMinValue, ItemConstants.PriceMaxValue)]
        public decimal OrdinaryPrice { get; set; }

        [Display(Name = ItemConstants.DisplayNameExpressAddOnPrice)]
        [Range(typeof(decimal), ItemConstants.PriceMinValue, ItemConstants.PriceMaxValue)]
        public decimal ExpressAddOnPrice { get; set; }

        [Display(Name = ItemConstants.DisplayNameVacuumCleaningAddOnPrice)]
        [Range(typeof(decimal), ItemConstants.PriceMinValue, ItemConstants.PriceMaxValue)]
        public decimal VacuumCleaningAddOnPrice { get; set; }

        [Display(Name = ItemConstants.DisplayNameFlavorAddOnPrice)]
        [Range(typeof(decimal), ItemConstants.PriceMinValue, ItemConstants.PriceMaxValue)]
        public decimal FlavorAddOnPrice { get; set; }
    }
}
