namespace Carpet.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Services.Data.CustomerService;
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
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateInputModel customerCreate)
        {
            var result = await this.customersService.CreateAsync(customerCreate, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Customers/AddAddress{id}
        public async Task<IActionResult> AddAddress(string id = null)
        {
            var customerForView = await this.customersService.GetByIdAsync<CustomerAddAddressViewModel>(id);

            if (customerForView == null)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            return this.View(customerForView);
        }

        // POST: Customers/AddAddress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAddress(CustomerAddAddressInputModel customerAddAddress)
        {
            var result = await this.customersService.AddAddressToCustomerAsync(customerAddAddress, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.RedirectToAction(nameof(this.Index));
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
            var result = await this.customersService.EditByIdAsync(id, customerEdit, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.RedirectToAction(nameof(this.Index));
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
            var customer = await this.customersService.DeleteByIdAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
