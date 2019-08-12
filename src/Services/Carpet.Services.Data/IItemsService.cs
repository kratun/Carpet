namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Data.Models;
    using Carpet.Web.InputModels.Administration.Items;
    using Carpet.Web.ViewModels.Administration.Items;

    public interface IItemsService
    {
        IQueryable<TViewModel> GetAllItems<TViewModel>();

        Task<ItemIndexViewModel> Create(ItemCreateInputModel itemFromView);

        Task<TViewModel> GetById<TViewModel>(int id);

        Task<ItemEditViewModel> Edit(int id, ItemEditInputModel itemFromView);

        Task<ItemIndexViewModel> Delete(int id);
    }
}
