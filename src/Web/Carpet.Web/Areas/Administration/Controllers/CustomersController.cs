namespace Carpet.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
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
            if (!this.ModelState.IsValid)
            {
                return this.View(AutoMapper.Mapper.Map<CustomerCreateViewModel>(customerCreate));
            }

            var hasAnyCustomerWithPhoneNumber = await this.customersService.GetAllCustomersAsync<CustomerCreateViewModel>().AnyAsync(x => x.PhoneNumber == customerCreate.PhoneNumber);

            // If customer with phone number exists return existing view model
            if (hasAnyCustomerWithPhoneNumber)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(CustomerConstants.ArgumentExceptionCustomerExistAddAddress));
                return this.View(AutoMapper.Mapper.Map<CustomerCreateViewModel>(customerCreate));
            }

            if (string.IsNullOrEmpty(customerCreate.DeliveryAddress))
            {
                customerCreate.DeliveryAddress = customerCreate.PickUpAddress;
            }

            var result = await this.customersService.CreateAsync(customerCreate);

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
            if (!this.ModelState.IsValid)
            {
                return this.View(AutoMapper.Mapper.Map<CustomerAddAddressViewModel>(customerAddAddress));
            }

            if (string.IsNullOrEmpty(customerAddAddress.DeliveryAddress))
            {
                customerAddAddress.DeliveryAddress = customerAddAddress.PickUpAddress;
            }

            var hasAnyCustomerWithMaxSameData = await this.customersService.GetAllCustomersAsync<CustomerAddAddressViewModel>().Where(x => x.PhoneNumber == customerAddAddress.PhoneNumber).AnyAsync(x => HasMaxSameCustomerData(x, customerAddAddress.FirstName, customerAddAddress.LastName, customerAddAddress.PhoneNumber, customerAddAddress.PickUpAddress, customerAddAddress.DeliveryAddress));

            if (hasAnyCustomerWithMaxSameData)
            {
                this.ModelState.AddModelError(string.Empty, CustomerConstants.ArgumentExceptionCustomerExist);
                return this.View(AutoMapper.Mapper.Map<CustomerAddAddressViewModel>(customerAddAddress));
            }

            var hasAnyCustomerWithMinSameData = await this.customersService.GetAllCustomersAsync<CustomerAddAddressViewModel>().Where(x => x.PhoneNumber == customerAddAddress.PhoneNumber).AnyAsync(x => !HasMinSameCustomerData(x, customerAddAddress.FirstName, customerAddAddress.LastName, customerAddAddress.PhoneNumber));

            if (hasAnyCustomerWithMinSameData)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(CustomerConstants.ArgumentExceptionCustomerPhone, customerAddAddress.PhoneNumber));
                return this.View(AutoMapper.Mapper.Map<CustomerAddAddressViewModel>(customerAddAddress));
            }

            var result = await this.customersService.AddAddressToCustomerAsync(customerAddAddress);

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
            if (!this.ModelState.IsValid)
            {
                return this.View(this.View(AutoMapper.Mapper.Map<CustomerEditViewModel>(customerEdit)));
            }

            if (string.IsNullOrEmpty(customerEdit.DeliveryAddress))
            {
                customerEdit.DeliveryAddress = customerEdit.PickUpAddress;
            }

            var hasAnyCustomerWithSameData = await this.customersService
                .GetAllCustomersAsync<CustomerEditViewModel>()
                .AnyAsync(x => x != null && x.FirstName == customerEdit.FirstName && x.LastName == customerEdit.LastName && x.PhoneNumber == customerEdit.PhoneNumber && x.PickUpAddress == customerEdit.PickUpAddress && ((x.DeliveryAddress == null && customerEdit.DeliveryAddress == null) || x.DeliveryAddress == customerEdit.DeliveryAddress));

            if (hasAnyCustomerWithSameData)
            {
                this.ModelState.AddModelError(string.Empty, CustomerConstants.ArgumentExceptionCustomerExist);

                return this.View(AutoMapper.Mapper.Map<CustomerEditViewModel>(customerEdit));
            }

            var result = await this.customersService.EditByIdAsync(id, customerEdit);

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

        private static bool HasMaxSameCustomerData(CustomerAddAddressViewModel checkForCustomer, string firstName, string lastName, string phoneNumber, string pickUpAddress, string deliveryAddress)
        {
            return HasMinSameCustomerData(checkForCustomer, firstName, lastName, phoneNumber) &&
                            checkForCustomer.PickUpAddress == pickUpAddress && ((checkForCustomer.DeliveryAddress == null && deliveryAddress == null) || checkForCustomer.DeliveryAddress == deliveryAddress);
        }

        private static bool HasMinSameCustomerData(CustomerAddAddressViewModel checkForCustomer, string firstName, string lastName, string phoneNumber)
        {
            return checkForCustomer != null &&
                                        checkForCustomer.FirstName == firstName &&
                                        checkForCustomer.LastName == lastName &&
                                        checkForCustomer.PhoneNumber == phoneNumber;
        }
    }
}
