namespace Carpet.Web.ViewModels.Administration.Items
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class ItemIndexViewModel : IMapFrom<Item>, IMapTo<Item>
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Стандартна цена")]
        public decimal OrdinaryPrice { get; set; }

        [Display(Name = "Добавка към цена за експресна поръчка")]
        public decimal ExpressAddOnPrice { get; set; }

        [Display(Name = "Добавка към цена за прахосмучене")]
        public decimal VacuumCleaningAddOnPrice { get; set; }

        [Display(Name = "Добавка към цена за ароматизиране")]
        public decimal FlavorAddOnPrice { get; set; }
    }
}
