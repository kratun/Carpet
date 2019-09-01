namespace Carpet.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Services.Data;
    using Carpet.Web.InputModels.Administration.Items;
    using Carpet.Web.ViewModels.Administration.Items;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ItemsController : AdministrationController
    {
        private readonly IItemsService itemsService;

        public ItemsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        // GET: Items
        public async Task<ActionResult> Index()
        {
            List<ItemIndexViewModel> items = await this.itemsService.GetAllItemsAsync<ItemIndexViewModel>()
                .ToListAsync();

            return this.View(items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var itemToDetail = await this.itemsService.GetByIdAsync<ItemDetailsViewModel>(id);
            return this.View(itemToDetail);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCreateInputModel itemCreate)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(AutoMapper.Mapper.Map<ItemIndexViewModel>(itemCreate));
            }

            var isNameExist = await this.itemsService.GetAllItemsAsync<ItemIndexViewModel>().AnyAsync(x => x.Name == itemCreate.Name);

            if (isNameExist)
            {
                this.ModelState.AddModelError(nameof(itemCreate.Name), string.Format(ItemConstants.ArgumentExceptionItemName, itemCreate.Name));
                return this.View(AutoMapper.Mapper.Map<ItemIndexViewModel>(itemCreate));
            }

            var result = await this.itemsService.CreateAsync(itemCreate);

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var itemToEdit = await this.itemsService.GetByIdAsync<ItemEditViewModel>(id);
            return this.View(itemToEdit);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemEditInputModel itemEdit)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(AutoMapper.Mapper.Map<ItemEditViewModel>(itemEdit));
            }

            var isNameExist = await this.itemsService.GetAllItemsAsync<ItemEditViewModel>().FirstOrDefaultAsync(x => x.Name == itemEdit.Name);

            if (isNameExist != null && isNameExist.Id != id)
            {
                this.ModelState.AddModelError(nameof(itemEdit.Name), string.Format(ItemConstants.ArgumentExceptionItemName, itemEdit.Name));
                return this.View(AutoMapper.Mapper.Map<ItemIndexViewModel>(itemEdit));
            }

            var result = await this.itemsService.EditByIdAsync(id, itemEdit);

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var itemToDelete = await this.itemsService.GetByIdAsync<ItemDeleteViewModel>(id);
            return this.View(itemToDelete);
        }

        // POST: Items/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ItemDeleteViewModel itemDelete)
        {
            var item = await this.itemsService.DeleteByIdAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
