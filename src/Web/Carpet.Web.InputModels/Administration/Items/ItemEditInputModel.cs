namespace Carpet.Web.InputModels.Administration.Items
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class ItemEditInputModel : IMapTo<Item>, IMapFrom<Item>
    {
        [Required]
        [Display(Name = ItemConstants.ItemDisplayNameName)]
        public string Name { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameOrdinaryPrice)]
        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        public decimal OrdinaryPrice { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameExpressAddOnPrice)]
        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        public decimal ExpressAddOnPrice { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameVacuumCleaningAddOnPrice)]
        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        public decimal VacuumCleaningAddOnPrice { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameFlavorAddOnPrice)]
        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        public decimal FlavorAddOnPrice { get; set; }
    }
}
