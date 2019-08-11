namespace Carpet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Carpet.Data;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.ViewModels.Administration.Items;

    public class ItemsService : IItemsService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository<Item> itemRepository;

        public ItemsService(ApplicationDbContext context, IRepository<Item> itemRepository)
        {
            this.context = context;
            this.itemRepository = itemRepository;
        }

        public Task<bool> Create(Item item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(int id, Item item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ItemIndexViewModel> GetAllItems()
        {
            return this.itemRepository.All().Select(x => x.To<ItemIndexViewModel>());
        }

        public Task<Item> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
