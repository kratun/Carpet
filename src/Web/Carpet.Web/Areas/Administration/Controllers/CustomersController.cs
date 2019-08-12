namespace Carpet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Carpet.Services.Data;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Customers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CustomersController : AdministrationController
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        // GET: Customers
        [Route("/Administration/Customers")]
        public async Task<IActionResult> Index()
        {
            var customers = await this.customersService.GetAllCustomersAsync<CustomerIndexViewModel>()
                .ToListAsync();

            return this.View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return this.View();
        }

        // GET: Customers/Create
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateInputModel customerCreate)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(customerCreate);
                }

                var result = await this.customersService.CreateAsync(customerCreate);
                return this.RedirectToAction(nameof(this.Index));
            }
            catch (ArgumentException e)
            {
                // TODO: Error message that item name exist
                this.ModelState.AddModelError(e.ParamName, e.Message);
                return this.View(customerCreate);
            }
            catch (NullReferenceException e)
            {
                // TODO: Error message that item name exist
                this.ModelState.AddModelError(e.InnerException.Message, e.Message);
                return this.View(customerCreate);
            }
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return this.View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerEditInputModel customerEdit)
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
        public async Task<IActionResult> Delete(int id)
        {
            return this.View();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CustomerDeleteViewModel customerDelete)
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
