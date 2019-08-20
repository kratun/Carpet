namespace Carpet.Services.Data.OrderStatusService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> orderStatusRepository;

        public OrderStatusService(IRepository<OrderStatus> orderStatusRepository)
        {
            this.orderStatusRepository = orderStatusRepository;
        }

        public async Task<OrderStatus> CreateAsync(string orderStatusName)
        {
            var orderStatus = new OrderStatus { Name = orderStatusName };

            await this.orderStatusRepository.AddAsync(orderStatus);

            await this.orderStatusRepository.SaveChangesAsync();

            return orderStatus;
        }

        public async Task<int> GetIdByNameAsync(string name)
        {
            return await this.orderStatusRepository.All().Where(x => x.Name == name).Select(x => x.Id).FirstOrDefaultAsync();
        }
    }
}
