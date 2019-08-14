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
        private readonly IRepository<Item> itemRepository;

        public ItemsService(IDeletableEntityRepository<Item> itemRepository)
        {
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

            this.itemRepository.Delete(item);
            await this.itemRepository.SaveChangesAsync();

            return item.To<ItemDeleteViewModel>();
        }

        public async Task<ItemEditViewModel> EditByIdAsync(int id, ItemEditInputModel itemFromView)
        {
            var itemToDelete = this.itemRepository.All().FirstOrDefault(x => x.Id == id);

            if (itemToDelete == null)
            {
                Exception innerException = new Exception(nameof(id));
                throw new NullReferenceException(string.Format(ItemConstants.NullReferenceItemId, id), innerException);
            }

            var checkForName = this.itemRepository.All().FirstOrDefault(x => x.Name == itemFromView.Name);

            if (checkForName != null && checkForName.Id != itemToDelete.Id)
            {
                throw new ArgumentException(string.Format(ItemConstants.ArgumentExceptionItemName, itemFromView.Name), nameof(itemFromView.Name));
            }

            var newItem = itemFromView.To<Item>();

            this.itemRepository.Delete(itemToDelete);
            await this.itemRepository.AddAsync(newItem);
            await this.itemRepository.SaveChangesAsync();

            return newItem.To<ItemEditViewModel>();
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
