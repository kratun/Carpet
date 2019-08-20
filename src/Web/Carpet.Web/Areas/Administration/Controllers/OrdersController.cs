namespace Carpet.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Services.Data.CustomerService;
    using Carpet.Services.Data.OrdersService;
    using Carpet.Web.InputModels.Administration.Orders.Create;
    using Carpet.Web.ViewModels.Administration.Orders.Create;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;
        private readonly ICustomersService customersService;

        public OrdersController(IOrdersService ordersService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.customersService = customersService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(string id)
        {
            var customer = await this.customersService.GetByIdAsync<OrderCreateViewModel>(id);

            if (customer == null)
            {
                return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerCustomersName}");
            }

            return this.View(customer);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateInputModel orederCreate)
        {
            var userName = this.User.Identity.Name;
            var result = await this.ordersService.CreateAsync(orederCreate, userName, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerCustomersName}");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}