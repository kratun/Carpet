namespace Carpet.Web.InputModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    public class ItemCreateInputModel // TODO : IMapTo<ItemDTO>, IMapFrom<ItemDTO>
    {
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Стандартна цена")]
        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        public decimal OrdinaryPrice { get; set; }

        [Display(Name = "Добавка към цена при експресна поръчка")]
        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        public decimal ExpressAddOnPrice { get; set; }

        [Display(Name = "Добавка към цена при прахосмучене")]
        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        public decimal VacuumCleaningAddOnPrice { get; set; }

        [Display(Name = "Добавка към цена при ароматизиране")]
        [Range(typeof(decimal), ItemConstants.ItemPriceMinValue, ItemConstants.ItemPriceMaxValue)]
        public decimal FlavorAddOnPrice { get; set; }
    }
}
