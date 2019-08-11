namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Data.Models;
    using Carpet.Web.ViewModels.Administration.Items;

    public interface IItemsService
    {
        IQueryable<ItemIndexViewModel> GetAllItems();

        Task<bool> Create(Item item);

        Task<Item> GetById(int id);

        Task<bool> Edit(int id, Item item);

        Task<bool> Delete(int id);
    }
}
