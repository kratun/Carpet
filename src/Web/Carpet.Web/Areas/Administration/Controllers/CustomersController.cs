namespace Carpet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Services.Data;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Customers;
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
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var customerToDetail = await this.customersService.GetByIdAsync<CustomerDetailsViewModel>(id);
            return this.View(customerToDetail);
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
        public async Task<IActionResult> Edit(string id)
        {
            var customerToEdit = await this.customersService.GetByIdAsync<CustomerEditViewModel>(id);
            return this.View(customerToEdit);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CustomerEditInputModel customerEdit)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(customerEdit);
                }

                var result = await this.customersService.EditByIdAsync(id, customerEdit);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (ArgumentException e)
            {
                // TODO: Error message that customer with Registration nnumber same as edited exist
                this.ModelState.AddModelError(e.ParamName, e.Message);
                return this.View(customerEdit);
            }
            catch (NullReferenceException e)
            {
                // TODO: Error message that customer with Id does not exist
                this.ModelState.AddModelError(e.InnerException.Message, e.Message);
                return this.View(customerEdit);
            }
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var customerToDelete = await this.customersService.GetByIdAsync<CustomerDeleteViewModel>(id);
            return this.View(customerToDelete);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, CustomerDeleteInputModel customerDelete)
        {
            try
            {
                var customer = await this.customersService.DeleteByIdAsync(id);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (NullReferenceException e)
            {
                // TODO: Error message that item not exist
                // this.ModelState.Root.AttemptedValue = "ModelOnly";
                // this.ModelState.Root.RawValue = e.Message;
                this.ModelState.AddModelError(e.InnerException.Message, e.Message);
                return this.View(customerDelete);
            }
        }
    }
}
