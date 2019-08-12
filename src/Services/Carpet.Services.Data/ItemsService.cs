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

        public ItemsService(ApplicationDbContext context, IRepository<Item> itemRepository)
        {
            this.context = context;
            this.itemRepository = itemRepository;
        }

        public async Task<ItemIndexViewModel> Create(ItemCreateInputModel itemFromView)
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

        public Task<ItemIndexViewModel> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemEditViewModel> Edit(int id, ItemEditInputModel itemFromView)
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

        public IQueryable<TViewModel> GetAllItems<TViewModel>()
        {
            return this.itemRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetById<TViewModel>(int id)
        {
            return await this.itemRepository.All().Where(x => x.Id == id).To<TViewModel>().FirstOrDefaultAsync();
        }
    }
}
