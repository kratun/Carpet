namespace Carpet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpet.Services.Data;
    using Carpet.Web.InputModels.Administration.Items;
    using Carpet.Web.ViewModels.Administration.Items;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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
            List<ItemIndexViewModel> items = await this.itemsService.GetAllItems<ItemIndexViewModel>()
                .ToListAsync();

            return this.View(items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var itemToDetail = await this.itemsService.GetById<ItemDetailsViewModel>(id);
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
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(itemCreate);
                }

                // var item = AutoMapper.Mapper.Map<Item>(itemCreate);
                var result = await this.itemsService.Create(itemCreate);

                if (result != null)
                {
                    return this.RedirectToAction(nameof(this.Edit), result.Id);
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (ArgumentException e)
            {
                // TODO: Error message that item name exist
                this.ModelState.AddModelError(e.ParamName, e.Message);
                return this.View(itemCreate);
            }
            catch (NullReferenceException e)
            {
                // TODO: Error message that item name exist
                this.ModelState.AddModelError(e.InnerException.Message, e.Message);
                return this.View(itemCreate);
            }
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var itemToEdit = await this.itemsService.GetById<ItemEditViewModel>(id);
            return this.View(itemToEdit);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemEditInputModel itemEdit)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(itemEdit);
                }

                var result = await this.itemsService.Edit(id, itemEdit);

                if (result == null)
                {
                    return this.View(itemEdit);
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (ArgumentException e)
            {
                // TODO: Error message that item name exist
                this.ModelState.AddModelError(e.ParamName, e.Message);
                return this.View();
            }
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return this.View();
        }

        // POST: Items/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}
