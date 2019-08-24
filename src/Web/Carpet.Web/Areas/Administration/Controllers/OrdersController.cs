﻿namespace Carpet.Web.Areas.Administration.Controllers
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
    using Carpet.Web.InputModels.Administration.Orders.PickUpRangeHours;
    using Carpet.Web.ViewModels.Administration.Orders.AddItems;
    using Carpet.Web.ViewModels.Administration.Orders.AddVehicleToPickUp;
    using Carpet.Web.ViewModels.Administration.Orders.AllCreated;
    using Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpConfirmation;
    using Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpFromCustomer;
    using Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpHours;
    using Carpet.Web.ViewModels.Administration.Orders.Create;
    using Carpet.Web.ViewModels.Administration.Orders.PickUpRangeHours;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;
        private readonly ICustomersService customersService;
        private readonly IGarageService garageService;
        private readonly IItemsService itemsService;
        private readonly IOrderItemsService orderItemsService;

        public OrdersController(IOrdersService ordersService, ICustomersService customersService, IGarageService garageService, IItemsService itemsService, IOrderItemsService orderItemsService)
        {
            this.ordersService = ordersService;
            this.customersService = customersService;
            this.garageService = garageService;
            this.itemsService = itemsService;
            this.orderItemsService = orderItemsService;
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

            order.PickUpFor = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + OrderConstants.AddIntOne);

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

            var result = await this.ordersService.OrderPickUpConfirmedAsync(id, userName, this.ModelState);

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

        // Get : Orders/AddItems
        [HttpGet]
        public async Task<IActionResult> AddItems(string id)
        {
            var order = await this.ordersService.GetAllAsNoTrackingAsync<OrderAddItemsViewModel>().FirstOrDefaultAsync(x => x.Id == id);

            if (order == null || order.StatusName != OrderConstants.StatusPickUpArrangedDateCоnfirmed)
            {
                return this.RedirectToAction(nameof(this.AllWaitingPickUpHours));
            }

            foreach (var item in order.OrderItems)
            {
                item.TotalPrice = this.GetTotalAmount(item);
            }

            order.ItemList = await this.itemsService.GetAllItemsAsync<OrderOrderItemItemAddItemsViewModel>().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

            return this.View(order);
        }

        // POST: Orders/AddItems
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItems(OrderAddItemInputModel orderfromView)
        {
            var userName = this.User.Identity.Name;

            var result = await this.ordersService.AddItemAsync(orderfromView, userName, this.ModelState);

            result.ItemList = await this.itemsService.GetAllItemsAsync<OrderOrderItemItemAddItemsViewModel>().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

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
        [HttpGet]
        public async Task<IActionResult> DeleteItem(OrderDeleteItemInputModel modelFromView)
        {
            var result = await this.orderItemsService.DeleteByIdAsync(modelFromView.OrderItemId);

            if (!result)
            {
                return this.RedirectToAction(nameof(this.AllWaitingPickUpHours));
            }

            return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerOrdersName}/{GlobalConstants.ActionAddItemsName}/{modelFromView.Id}");
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