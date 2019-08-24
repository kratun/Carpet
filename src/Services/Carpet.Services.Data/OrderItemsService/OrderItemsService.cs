namespace Carpet.Services.Data.OrderItemsService
{
    using System.Threading.Tasks;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class OrderItemsService : IOrderItemsService
    {
        private readonly IDeletableEntityRepository<OrderItem> orderItemRepository;

        public OrderItemsService(IDeletableEntityRepository<OrderItem> orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var orderItem = await this.orderItemRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (orderItem == null)
            {
                return false;
            }

            this.orderItemRepository.Delete(orderItem);
            await this.orderItemRepository.SaveChangesAsync();

            return true;
        }
    }
}
