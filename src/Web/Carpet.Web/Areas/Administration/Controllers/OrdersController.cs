namespace Carpet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Services.Data;
    using Carpet.Services.Data.CustomerService;
    using Carpet.Services.Data.OrderItemsService;
    using Carpet.Services.Data.OrdersService;
    using Carpet.Web.InputModels.Administration.Orders.AddItems;
    using Carpet.Web.InputModels.Administration.Orders.AddVehicleForPickUp;
    using Carpet.Web.InputModels.Administration.Orders.Create;
    using Carpet.Web.InputModels.Administration.Orders.Delivery.Add.RangeHours;
    using Carpet.Web.InputModels.Administration.Orders.Delivery.Add.Vehicle;
    using Carpet.Web.InputModels.Administration.Orders.Delivery.Confirmed;
    using Carpet.Web.InputModels.Administration.Orders.Delivery.Print;
    using Carpet.Web.InputModels.Administration.Orders.Delivery.Waiting.BackToArrangeDay;
    using Carpet.Web.InputModels.Administration.Orders.Delivery.Waiting.Confirmation;
    using Carpet.Web.InputModels.Administration.Orders.PickUp.Waiting.BackToArrangeDay;
    using Carpet.Web.InputModels.Administration.Orders.PickUpRangeHours;
    using Carpet.Web.ViewModels.Administration.Orders.AddItems;
    using Carpet.Web.ViewModels.Administration.Orders.AddVehicleToPickUp;
    using Carpet.Web.ViewModels.Administration.Orders.AllCreated;
    using Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpConfirmation;
    using Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpFromCustomer;
    using Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpHours;
    using Carpet.Web.ViewModels.Administration.Orders.Create;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.RangeHours;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.Vehicles;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Confirmed.Index;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Print;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Waitnig;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Waitnig.Confirmation;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Waitnig.Payment;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Waitnig.RangeHours;
    using Carpet.Web.ViewModels.Administration.Orders.PickUpRangeHours;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SelectPdf;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;
        private readonly ICustomersService customersService;
        private readonly IGarageService garageService;
        private readonly IItemsService itemsService;
        private readonly IOrderItemsService orderItemsService;
        private readonly ICloudinaryService cloudinaryService;

        public OrdersController(IOrdersService ordersService, ICustomersService customersService, IGarageService garageService, IItemsService itemsService, IOrderItemsService orderItemsService, ICloudinaryService cloudinaryService)
        {
            this.ordersService = ordersService;
            this.customersService = customersService;
            this.garageService = garageService;
            this.itemsService = itemsService;
            this.orderItemsService = orderItemsService;
            this.cloudinaryService = cloudinaryService;
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

        // GET: Orders/AllCreated
        public async Task<IActionResult> AllCreated()
        {
            var orders = await this.ordersService.GetAllCreatedAsync<OrderAllCreatedViewModel>()
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(orders);
        }

        // GET: Orders/AddVehicleForPickUp
        public async Task<IActionResult> AddVehicleForPickUp(string id)
        {
            var order = await this.ordersService.GetByIdAsync<OrderAddVehicleToPickUpViewModel>(id);

            if (order == null)
            {
                return this.RedirectToAction(nameof(this.AllCreated));
            }

            order.VehicleList = await this.garageService.GetVehicleNames();

            order.PickUpFor = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).AddDays((double)OrderConstants.AddIntOne);

            return this.View(order);
        }

        // POST: Orders/AddVehicleForPickUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicleForPickUp(OrderAddVehicleForPickUpInputModel orederVehicleFOrPickUp)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.AddVehicleForPickUpAsync(orederVehicleFOrPickUp, userName, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                result.VehicleList = await this.garageService.GetVehicleNames();
                return this.View(result);
            }

            return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerOrdersName}/{GlobalConstants.ActionAllCreatedName}");
        }

        // GET: Orders/AllWaitingPickUpHours
        public async Task<IActionResult> AllWaitingPickUpHours()
        {
            var orders = await this.ordersService.GetAllAsNoTrackingAsync<OrderAllWaitingPickUpHoursViewModel>()
                .Where(x => x.StatusName == OrderConstants.StatusPickUpArrangeHourRangeWaiting)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(orders);
        }

        // GET: Orders/PickUpRangeHours
        public async Task<IActionResult> PickUpRangeHours(string id)
        {
            var order = await this.ordersService.GetByIdAsync<OrderPickUpRangeHoursViewModel>(id);

            if (order == null || order.StatusName != OrderConstants.StatusPickUpArrangeHourRangeWaiting)
            {
                return this.RedirectToAction(nameof(this.AllWaitingPickUpHours));
            }

            var hourList = OrderConstants.HourList.Split(';', StringSplitOptions.RemoveEmptyEntries);

            order.PickUpForStartHours = hourList.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            order.PickUpForEndHours = hourList.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

            return this.View(order);
        }

        // POST: Orders/PickUpRangeHours
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PickUpRangeHours(OrderPickUpRangeHoursInputModel orderFromView)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.SetPickUpRangeHoursAsync(orderFromView, userName, this.ModelState);

            var hourList = OrderConstants.HourList.Split(';', StringSplitOptions.RemoveEmptyEntries);

            result.PickUpForStartHours = hourList.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            result.PickUpForEndHours = hourList.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.RedirectToAction(nameof(this.AllWaitingPickUpHours));
        }

        // GET: Orders/AllWaitingPickUpConfirmation
        public async Task<IActionResult> AllWaitingPickUpConfirmation()
        {
            var orders = await this.ordersService.GetAllAsNoTrackingAsync<OrderAllWaitingPickUpConfirmationViewModel>()
                .Where(x => x.StatusName == OrderConstants.StatusPickUpArrangedDateWaiting)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(orders);
        }

        // GET: Orders/PickUpDateConfirmedByCustomer
        [HttpGet]
        public async Task<IActionResult> PickUpDateConfirmedByCustomer(string id)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.OrderGangeStatusAsync(id, userName, OrderConstants.StatusPickUpArrangedDateCоnfirmed, this.ModelState);

            return this.RedirectToAction(nameof(this.AllWaitingPickUpConfirmation));
        }

        // GET: Orders/AllWaitngPickedUp
        public async Task<IActionResult> AllWaitngPickedUp()
        {
            var orders = await this.ordersService.GetAllAsNoTrackingAsync<OrderAllWaitingPickUpFromCustomerViewModel>()
                .Where(x => x.StatusName == OrderConstants.StatusPickUpArrangedDateCоnfirmed)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PickUpWaitingBackToArrangeDay(OrderPickUpWaitingBackToArrangeDayInputModel orderFromView)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.OrderGangeStatusAndRemovePickUpVehicleAsync(orderFromView.Id, userName, OrderConstants.StatusPickUpArrangeDayWaiting, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(orderFromView.RouteString);
            }

            return this.RedirectToAction(orderFromView.RouteString);
        }

        // Get : Orders/AddItems
        [HttpGet]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionAddItemsName + "/{id?}", Name = GlobalConstants.RouteOrdersAddItems)]
        public async Task<IActionResult> AddItems(string id)
        {
            var order = await this.ordersService
                .GetAllAsNoTrackingWithDeteletedAsync<OrderAddItemsViewModel>()
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order == null || (order.StatusName != OrderConstants.StatusPickUpArrangedDateCоnfirmed && order.StatusName != OrderConstants.StatusDeliveryArrangeDayWaiting))
            {
                return this.RedirectToAction(nameof(this.AllWaitingPickUpHours));
            }

            order.OrderItems = order.OrderItems.Where(oi => !oi.IsDeleted).ToList();

            foreach (var item in order.OrderItems)
            {
                item.TotalPrice = this.GetTotalAmount(item);
            }

            order.ItemList = await this.itemsService.GetAllItemsAsync<OrderOrderItemItemAddItemsViewModel>().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

            return this.View(order);
        }

        // POST: Orders/AddItems
        [HttpPost]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionAddItemsName + "/{id?}", Name = GlobalConstants.RouteOrdersAddItems)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItems(OrderAddItemInputModel orderfromView)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.AddItemAsync(orderfromView, userName, this.ModelState);

            result.ItemList = await this.itemsService.GetAllItemsAsync<OrderOrderItemItemAddItemsViewModel>().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

            result.OrderItems = await this.orderItemsService
                .GetAllAsNoTrackingWithDeteletedAsync<OrderOrderItemAddItemsViewModel>()
                .Where(oi => oi.OrderId == result.Id && !oi.IsDeleted)
                .ToListAsync();

            foreach (var item in result.OrderItems)
            {
                item.TotalPrice = this.GetTotalAmount(item);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerOrdersName}/{GlobalConstants.ActionAddItemsName}/{result.Id}");
        }

        // DeleteItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItem(OrderDeleteItemInputModel modelFromView)
        {
            var result = await this.orderItemsService.DeleteByIdAsync(modelFromView.OrderItemId);

            if (!result)
            {
                return this.RedirectToAction(nameof(this.AllWaitngPickedUp));
            }

            return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerOrdersName}/{GlobalConstants.ActionAddItemsName}/{modelFromView.Id}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PickUpConfirmed(string id)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.OrderGangeStatusAsync(id, userName, OrderConstants.StatusPickedUpConfirm, this.ModelState);

            return this.RedirectToAction(nameof(this.AllWaitingPickUpConfirmation));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeliveryStart(string id)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.OrderGangeStatusAsync(id, userName, OrderConstants.StatusDeliveryArrangeDayWaiting, this.ModelState);

            return this.RedirectToAction(nameof(this.AllWaitngPickedUp));
        }

        // GET: Orders/Delivery/Waiting/Vehicle
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryWaitingVehicleName, Name = GlobalConstants.RouteOrdersDeliveryWaitingVehicle)]
        public async Task<IActionResult> DeliveryWaitingVehicle()
        {
            var orders = await this.ordersService.GetAllAsNoTrackingAsync<OrderDeliveryWaitingAddVehicleViewModel>()
                .Where(x => x.StatusName == OrderConstants.StatusDeliveryArrangeDayWaiting)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(orders);
        }

        // GET: Orders/Delivery/Add/Vehicle
        [HttpGet]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryAddVehicleName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryAddVehicle)]
        public async Task<IActionResult> DeliveryAddVehicle(string id)
        {
            var order = await this.ordersService.GetByIdAsync<OrderDeliveryAddVehicleViewModel>(id);

            if (order == null)
            {
                return this.RedirectToAction(nameof(this.AllCreated));
            }

            order.VehicleList = await this.garageService.GetVehicleNames();

            order.DeliveringFor = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).AddDays((double)OrderConstants.AddIntOne);

            return this.View(order);
        }

        // POST: Orders/Delivery/Add/Vehicle
        [HttpPost]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryAddVehicleName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryAddVehicle)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeliveryAddVehicle(OrderDeliveryAddVehicleInputModel orderFromView)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.DeliveryAddVehicleAsync(orderFromView, userName, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                result.VehicleList = await this.garageService.GetVehicleNames();
                return this.View(result);
            }

            return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerOrdersName}/{GlobalConstants.ActionDeliveryWaitingVehicleName}");
        }

        // GET: Orders/Delivery/Waiting/RangeHours
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryWaitingRangeHoursName, Name = GlobalConstants.RouteOrdersDeliveryWaitingRangeHours)]
        public async Task<IActionResult> DeliveryWaitingRangeHours()
        {
            var orders = await this.ordersService.GetAllAsNoTrackingAsync<OrderDeliveryWaitingRangeHoursViewModel>()
                .Where(x => x.StatusName == OrderConstants.StatusDeliveryArrangeHourRangeWaiting)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(orders);
        }

        // GET: Orders/Delivery/Add/RangeHours
        [HttpGet]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryAddRangeHoursName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryAddRangeHours)]
        public async Task<IActionResult> DeliveryAddRangeHours(string id)
        {
            var order = await this.ordersService.GetByIdAsync<OrderDeliveryAddRangeHoursViewModel>(id);

            if (order == null || order.StatusName != OrderConstants.StatusDeliveryArrangeHourRangeWaiting)
            {
                return this.RedirectToAction(nameof(this.DeliveryWaitingRangeHours));
            }

            var hourList = OrderConstants.HourList.Split(';', StringSplitOptions.RemoveEmptyEntries);

            order.DeliveryForStartHours = hourList.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            order.DeliveryForEndHours = hourList.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

            return this.View(order);
        }

        // POST: Orders/Delivery/Add/RangeHours
        [HttpPost]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryAddRangeHoursName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryAddRangeHours)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeliveryAddRangeHours(OrderDeliveryAddRangeHoursInputModel orderFromView)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.SetDeliveryRangeHoursAsync(orderFromView, userName, this.ModelState);

            var hourList = OrderConstants.HourList.Split(';', StringSplitOptions.RemoveEmptyEntries);

            result.DeliveryForStartHours = hourList.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            result.DeliveryForEndHours = hourList.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.RedirectToAction(nameof(this.DeliveryWaitingRangeHours));
        }

        // GET: Orders/Delivery/Waiting/Confirmation
        [HttpGet]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryWaitingConfirmationName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryWaitingConfirmation)]
        public async Task<IActionResult> DeliveryWaitingConfirmation()
        {
            var orders = await this.ordersService.GetAllAsNoTrackingAsync<OrderDeliveryWaitingConfirmationViewModel>()
                .Where(x => x.StatusName == OrderConstants.StatusDeliveryArrangedDateWaiting)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(orders);
        }

        // GET: Orders/Delivery/Waiting/Confirmation
        [HttpPost]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryWaitingConfirmationName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryWaitingConfirmation)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeliveryWaitingConfirmation(OrderDeliveryWaitingConfirmationInputModel orderFromView)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.OrderGangeStatusAsync(orderFromView.Id, userName, OrderConstants.StatusDeliveryArrangedDateCоnfirmed, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.RedirectToAction(nameof(this.DeliveryWaitingConfirmation));
        }

        [HttpPost]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryWaitingBackToArrangeDayName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryWaitingBackToArrangeDay)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeliveryWaitingBackToArrangeDay(OrderDeliveryWaitingBackToArrangeDayInputModel orderFromView)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.OrderGangeStatusAndRemoveVehicleAsync(orderFromView.Id, userName, OrderConstants.StatusDeliveryArrangeDayWaiting, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(orderFromView.RouteString);
            }

            return this.RedirectToAction(orderFromView.RouteString);
        }

        // GET: Orders/Delivery/Waiting/Payment
        [HttpGet]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryWaitingPaymentName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryWaitingPayment)]
        public async Task<IActionResult> DeliveryWaitingPayment()
        {
            var orders = await this.ordersService.GetAllAsNoTrackingAsync<OrderDeliveryWaitningPaymentViewModel>()
                .Where(x => x.StatusName == OrderConstants.StatusDeliveryArrangedDateCоnfirmed)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(orders);
        }

        [HttpGet]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryConfirmedName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryConfirmed)]
        public async Task<IActionResult> DeliveryConfirmed(string id)
        {
            var order = await this.ordersService.GetByIdAsync<OrderDeliveryConfirmedIndexViewModel>(id);

            if (order == null || order.StatusName != OrderConstants.StatusDeliveryArrangedDateCоnfirmed)
            {
                return this.RedirectToAction(nameof(this.DeliveryWaitingPayment));
            }

            order.OrderItems = await this.orderItemsService
                .GetAllAsNoTrackingWithDeteletedAsync<OrderOrderItemDeliveryConfirmedIndexViewModel>()
                .Where(x => x.OrderId == id)
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return this.View(order);
        }

        [HttpPost]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryConfirmedName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryConfirmed)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeliveryConfirmed(OrderDeliveryConfirmedByOperatorInputModel model)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.OrderGangeStatusAsync(model.Id, userName, OrderConstants.StatusDeliverConfirmed, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.RedirectToAction(nameof(this.DeliveryWaitingPayment));
        }

        [HttpGet]
        [Route(GlobalConstants.AreaAdministrationName + "/" + GlobalConstants.ContollerOrdersName + "/" + GlobalConstants.ActionDeliveryGetPDFName + "/{id?}", Name = GlobalConstants.RouteOrdersDeliveryGetPDF)]
        public IActionResult DeliveryGetPDF(OrderDeliveryPrintUrlInputModel model)
        {
            if (string.IsNullOrEmpty(model.PhoneNumber))
            {
                model.PhoneNumber = "Document_";
            }
            else
            {
                model.PhoneNumber += "_";
            }

            var fileName = model.PhoneNumber + DateTime.UtcNow.ToString("dd.MM.yyyy") + ".pdf";

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // create a new pdf document converting an url
            var urlAsString = "https://localhost:44319/Pdf/Print/" + model.Id;
            PdfDocument doc = converter.ConvertUrl(urlAsString);

            // save pdf document
            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();

            this.cloudinaryService.UploadPdfBytesAsync(pdf, fileName);

            // return resulted pdf document
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");

            fileResult.FileDownloadName = fileName;
            return fileResult;
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> DeliveryPrint(string id)
        {
            var order = await this.ordersService.GetAllAsNoTrackingAsync<OrderDeliveryPrintViewModel>().FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                order = new OrderDeliveryPrintViewModel();
            }

            return this.View(order);
        }

        private decimal GetTotalAmount(OrderOrderItemAddItemsViewModel orderItem)
        {
            var currentPrice = orderItem.Item.OrdinaryPrice;
            if (orderItem.HasFlavor)
            {
                currentPrice += orderItem.Item.FlavorAddOnPrice;
            }

            if (orderItem.HasVacuumCleaning)
            {
                currentPrice += orderItem.Item.VacuumCleaningAddOnPrice;
            }

            if (orderItem.IsExpress)
            {
                currentPrice += orderItem.Item.ExpressAddOnPrice;
            }

            return currentPrice * orderItem.ItemArea;
        }
    }
}
