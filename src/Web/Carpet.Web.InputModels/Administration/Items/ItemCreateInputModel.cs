namespace Carpet.Web.InputModels.Administration.Items
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class ItemCreateInputModel : IMapTo<Item>, IMapFrom<Item>
    {
        [Required]
        [Display(Name = ItemConstants.ItemDisplayNameName)]
        public string Name { get; set; }

        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        [Display(Name = ItemConstants.ItemDisplayNameOrdinaryPrice)]
        public decimal OrdinaryPrice { get; set; }

        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        [Display(Name = ItemConstants.ItemDisplayNameExpressAddOnPrice)]
        public decimal ExpressAddOnPrice { get; set; }

        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        [Display(Name = ItemConstants.ItemDisplayNameVacuumCleaningAddOnPrice)]
        public decimal VacuumCleaningAddOnPrice { get; set; }

        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        [Display(Name = ItemConstants.ItemDisplayNameFlavorAddOnPrice)]
        public decimal FlavorAddOnPrice { get; set; }
    }
}
