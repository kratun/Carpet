namespace Carpet.Web.Controllers
{
    using System.Threading.Tasks;

    using Carpet.Services.Data.OrdersService;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Print;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PdfController : Controller
    {
        private readonly IOrdersService ordersService;

        public PdfController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> Print(string id)
        {
            var order = await this.ordersService.GetAllAsNoTrackingAsync<OrderDeliveryPrintViewModel>().FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                order = new OrderDeliveryPrintViewModel();
            }

            return this.View(order);
        }
    }
}