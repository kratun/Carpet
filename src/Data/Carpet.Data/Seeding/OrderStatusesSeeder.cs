namespace Carpet.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;

    internal class OrderStatusesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.OrderStatuses.Any())
            {
                return;
            }

            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusPickUpArrangeDayWaiting });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusPickUpArrangeHourRangeWaiting });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusPickUpArrangedDateWaiting });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusPickUpArrangedDateCоnfirmed });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusPickedUpConfirm });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusWashingProcessing });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusWashed });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusDeliveryArrangeDayWaiting });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusDeliveryArrangeHourRangeWaiting });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusDeliveryArrangedDateCоnfirmed });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusDeliverConfirmed });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusPaymentConfirmed });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusInsallmentPayment });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusWaitingPickUpByCustomer });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusUnclaimedItems });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = OrderConstants.StatusScrappedItems });
        }
    }
}
