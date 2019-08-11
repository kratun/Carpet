namespace Carpet.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CustomersController : AdministrationController
    {
        // GET: Customers
        [Route("/Administration/Customers")]
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Customers/Edit/5
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

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Customers/Delete/5
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
