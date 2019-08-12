namespace Carpet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Items;
    using Carpet.Web.ViewModels.Administration.Items;
    using Microsoft.EntityFrameworkCore;

    public class ItemsService : IItemsService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository<Item> itemRepository;

        public ItemsService(ApplicationDbContext context, IDeletableEntityRepository<Item> itemRepository)
        {
            this.context = context;
            this.itemRepository = itemRepository;
        }

        public async Task<ItemIndexViewModel> CreateAsync(ItemCreateInputModel itemFromView)
        {
            var checkForName = this.itemRepository.All().FirstOrDefault(x => x.Name == itemFromView.Name);

            // If item exists return existing view model
            if (checkForName != null)
            {
                throw new ArgumentException(string.Format(ItemConstants.ArgumentExceptionItemName, itemFromView.Name), nameof(itemFromView.Name));
            }

            var itemToDb = itemFromView.To<Item>();

            await this.itemRepository.AddAsync(itemToDb);

            await this.itemRepository.SaveChangesAsync();

            return itemToDb.To<ItemIndexViewModel>();
        }

        public async Task<ItemDeleteViewModel> DeleteByIdAsync(int id)
        {
            var item = await this.itemRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (item == null)
            {
                throw new NullReferenceException(string.Format(ItemConstants.NullReferenceItemId, id), new Exception(nameof(id)));
            }

            // item.IsDeleted = true;
            this.itemRepository.Delete(item);
            await this.itemRepository.SaveChangesAsync();

            return item.To<ItemDeleteViewModel>();
        }

        public async Task<ItemEditViewModel> EditByIdAsync(int id, ItemEditInputModel itemFromView)
        {
            var item = this.itemRepository.All().FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                Exception innerException = new Exception(nameof(id));
                throw new NullReferenceException(string.Format(ItemConstants.NullReferenceItemId, id), innerException);
            }

            var checkForName = this.itemRepository.All().FirstOrDefault(x => x.Name == itemFromView.Name);

            if (checkForName != null && checkForName.Id != id)
            {
                throw new ArgumentException(string.Format(ItemConstants.ArgumentExceptionItemName, itemFromView.Name), nameof(itemFromView.Name));
            }

            item.Name = itemFromView.Name;
            item.OrdinaryPrice = itemFromView.OrdinaryPrice;
            item.VacuumCleaningAddOnPrice = itemFromView.VacuumCleaningAddOnPrice;
            item.FlavorAddOnPrice = itemFromView.FlavorAddOnPrice;
            item.ExpressAddOnPrice = itemFromView.ExpressAddOnPrice;

            this.itemRepository.Update(item);

            await this.itemRepository.SaveChangesAsync();

            return item.To<ItemEditViewModel>();
        }

        public IQueryable<TViewModel> GetAllItemsAsync<TViewModel>()
        {
            return this.itemRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(int id)
        {
            return await this.itemRepository.All().Where(x => x.Id == id).To<TViewModel>().FirstOrDefaultAsync();
        }
    }
}
