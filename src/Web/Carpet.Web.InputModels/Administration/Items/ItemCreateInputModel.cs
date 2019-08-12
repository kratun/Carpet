namespace Carpet.Web.InputModels.Administration.Items
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class ItemCreateInputModel : IMapTo<Item>, IMapFrom<Item>
    {
        [Required]
        [Display(Name = ItemConstants.DisplayNameName)]
        public string Name { get; set; }

        [Range(typeof(decimal), ItemConstants.PriceMinValue, ItemConstants.PriceMaxValue)]
        [Display(Name = ItemConstants.DisplayNameOrdinaryPrice)]
        public decimal OrdinaryPrice { get; set; }

        [Range(typeof(decimal), ItemConstants.PriceMinValue, ItemConstants.PriceMaxValue)]
        [Display(Name = ItemConstants.DisplayNameExpressAddOnPrice)]
        public decimal ExpressAddOnPrice { get; set; }

        [Range(typeof(decimal), ItemConstants.PriceMinValue, ItemConstants.PriceMaxValue)]
        [Display(Name = ItemConstants.DisplayNameVacuumCleaningAddOnPrice)]
        public decimal VacuumCleaningAddOnPrice { get; set; }

        [Range(typeof(decimal), ItemConstants.PriceMinValue, ItemConstants.PriceMaxValue)]
        [Display(Name = ItemConstants.DisplayNameFlavorAddOnPrice)]
        public decimal FlavorAddOnPrice { get; set; }
    }
}
