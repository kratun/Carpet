namespace Carpet.Web.ViewModels.Administration.Items
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class ItemEditViewModel : IMapTo<Item>, IMapFrom<Item>
    {
        public int Id { get; set; }

        [Display(Name = ItemConstants.DisplayNameName)]
        public string Name { get; set; }

        [Display(Name = ItemConstants.DisplayNameOrdinaryPrice)]
        public decimal OrdinaryPrice { get; set; }

        [Display(Name = ItemConstants.DisplayNameExpressAddOnPrice)]
        public decimal ExpressAddOnPrice { get; set; }

        [Display(Name = ItemConstants.DisplayNameVacuumCleaningAddOnPrice)]
        public decimal VacuumCleaningAddOnPrice { get; set; }

        [Display(Name = ItemConstants.DisplayNameFlavorAddOnPrice)]
        public decimal FlavorAddOnPrice { get; set; }
    }
}
