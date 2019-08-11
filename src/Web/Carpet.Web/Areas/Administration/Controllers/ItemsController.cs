namespace Carpet.Web.Areas.Administration.Controllers
{
    using Carpet.Web.InputModels.Administration;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsController : AdministrationController
    {
        // GET: Items
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Items/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCreateInputModel itemCreate)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(itemCreate);
                }

                // TODO: Add insert logic here
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.View(itemCreate);
            }
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Items/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
