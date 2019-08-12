namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Data.Models;
    using Carpet.Web.InputModels.Administration.Items;
    using Carpet.Web.ViewModels.Administration.Items;

    public interface IItemsService
    {
        IQueryable<TViewModel> GetAllItemsAsync<TViewModel>();

        Task<ItemIndexViewModel> CreateAsync(ItemCreateInputModel itemFromView);

        Task<TViewModel> GetByIdAsync<TViewModel>(int id);

        Task<ItemEditViewModel> EditByIdAsync(int id, ItemEditInputModel itemFromView);

        Task<ItemDeleteViewModel> DeleteByIdAsync(int id);
    }
}
