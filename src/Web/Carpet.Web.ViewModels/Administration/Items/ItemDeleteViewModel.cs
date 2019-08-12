namespace Carpet.Web.ViewModels.Administration.Items
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class ItemDeleteViewModel : IMapTo<Item>, IMapFrom<Item>
    {
        public int Id { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameName)]
        public string Name { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameOrdinaryPrice)]
        public decimal OrdinaryPrice { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameExpressAddOnPrice)]
        public decimal ExpressAddOnPrice { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameVacuumCleaningAddOnPrice)]
        public decimal VacuumCleaningAddOnPrice { get; set; }

        [Display(Name = ItemConstants.ItemDisplayNameFlavorAddOnPrice)]
        public decimal FlavorAddOnPrice { get; set; }
    }
}
