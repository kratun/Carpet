namespace Carpet.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Web.InputModels.Administration.Items;
    using Carpet.Web.ViewModels.Administration.Items;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ItemServiceTests : BaseServiceTests
    {
        private const string FirstItemName = "Килим1";
        private const decimal FistItemOrdinaryPrice = 5.0m;
        private const decimal FirstItemExpressAddOnPrice = 1.0m;
        private const decimal FirstItemVacuumCleaningAddOnPrice = 2.0m;
        private const decimal FirstItemFlavorAddOnPrice = 3.0m;

        private const string SecondItemName = "Килим2";
        private const decimal SecondtemOrdinaryPrice = 15.0m;
        private const decimal SecondItemExpressAddOnPrice = 11.0m;
        private const decimal SecondItemVacuumCleaningAddOnPrice = 12.0m;
        private const decimal SecondItemFlavorAddOnPrice = 13.0m;

        private readonly string testUserId = Guid.NewGuid().ToString();

        private IItemsService ItemsServiceMock =>
            this.ServiceProvider.GetRequiredService<IItemsService>();

        [Fact]
        public async Task GetAllItemsAsyncReturnsAllItemss()
        {
            await this.AddTestingFirstItemToDb();
            this.DbContext.Items.Add(new Item
            {
                Id = 2,
                Name = SecondItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            });
            await this.DbContext.SaveChangesAsync();

            var firstItem = new ItemIndexViewModel
            {
                Id = 1,
                Name = FirstItemName,
                OrdinaryPrice = FistItemOrdinaryPrice,
                ExpressAddOnPrice = FirstItemExpressAddOnPrice,
                FlavorAddOnPrice = FirstItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = FirstItemVacuumCleaningAddOnPrice,
            };
            var secondItem = new ItemIndexViewModel
            {
                Id = 2,
                Name = SecondItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            };

            var expected = new List<ItemIndexViewModel>();
            expected.Add(firstItem);
            expected.Add(secondItem);

            var actual = await this.ItemsServiceMock.GetAllItemsAsync<ItemIndexViewModel>().ToListAsync();

            Assert.Collection(
                actual,
                elem1 =>
                {
                    Assert.Equal(expected[0].Id, elem1.Id);
                    Assert.Equal(expected[0].Name, elem1.Name);
                    Assert.Equal(expected[0].OrdinaryPrice, elem1.OrdinaryPrice);
                    Assert.Equal(expected[0].VacuumCleaningAddOnPrice, elem1.VacuumCleaningAddOnPrice);
                },
                elem2 =>
                {
                    Assert.Equal(expected[1].Id, elem2.Id);
                    Assert.Equal(expected[1].Name, elem2.Name);
                    Assert.Equal(expected[1].OrdinaryPrice, elem2.OrdinaryPrice);
                    Assert.Equal(expected[1].VacuumCleaningAddOnPrice, elem2.VacuumCleaningAddOnPrice);
                });
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public async Task CreateAsyncReturnsCorrectItem()
        {
            var secondItem = new ItemIndexViewModel
            {
                Id = 2,
                Name = SecondItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            };

            var expected = new List<ItemIndexViewModel>();
            expected.Add(secondItem);

            var addItem = new ItemCreateInputModel
            {
                Name = SecondItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            };

            var result = await this.ItemsServiceMock.CreateAsync(addItem);
            var actual = this.DbContext.Items;

            Assert.Equal(expected.Count, actual.Count());
            Assert.Equal(addItem.Name, result.Name);
        }

        [Fact]
        public async Task DeleteByIdAsyncWithWrongIdReturnsError()
        {
            await this.AddTestingFirstItemToDb();

            var wrongId = 123;
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this.ItemsServiceMock.DeleteByIdAsync(wrongId));
            Assert.Equal(string.Format(string.Format(ItemConstants.NullReferenceItemId, wrongId), new Exception(nameof(wrongId))), exception.Message);
        }

        [Fact]
        public async Task DeleteByIdAsyncReturnsItem()
        {
            await this.AddTestingFirstItemToDb();

            var result = await this.ItemsServiceMock.DeleteByIdAsync(1);

            Assert.Equal(FirstItemName, result.Name);
        }

        [Fact]
        public async Task EditByIdAsyncWithWrongIdReturnsError()
        {
            await this.AddTestingFirstItemToDb();
            await this.AddTestingSecondItemToDb();

            var item = new ItemEditInputModel
            {
                Name = FirstItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            };
            var wrongId = 122;
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this.ItemsServiceMock.EditByIdAsync(wrongId, item));
            Assert.Equal(string.Format(string.Format(ItemConstants.NullReferenceItemId, wrongId)), exception.Message);
        }

        [Fact]
        public async Task EditByIdAsyncReturnsItem()
        {
            var addItem = new ItemCreateInputModel
            {
                Name = SecondItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            };

            await this.ItemsServiceMock.CreateAsync(addItem);

            var item = new ItemEditInputModel
            {
                Name = FirstItemName,
                OrdinaryPrice = FistItemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            };

            var result = await this.ItemsServiceMock.EditByIdAsync(1, item);

            Assert.Equal(item.OrdinaryPrice, result.OrdinaryPrice);
        }

        [Fact]
        public async Task GetByIdAsyncReturnsItem()
        {
            var addItem = new ItemCreateInputModel
            {
                Name = SecondItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            };

            await this.ItemsServiceMock.CreateAsync(addItem);

            var id = 1;

            var result = await this.ItemsServiceMock.GetByIdAsync<ItemDetailsViewModel>(id);

            Assert.Equal(addItem.OrdinaryPrice, result.OrdinaryPrice);
        }

        [Fact]
        public async Task GetByIdWithDeletedAsyncReturnsItem()
        {
            var addItem = new ItemCreateInputModel
            {
                Name = SecondItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            };

            var item = await this.ItemsServiceMock.CreateAsync(addItem);

            var id = 1;

            var deleteItem = await this.ItemsServiceMock.DeleteByIdAsync(id);


            var result = await this.ItemsServiceMock.GetByIdWithDeletedAsync<ItemDetailsViewModel>(item.Id);

            Assert.Equal(deleteItem.Id, result.Id);
        }

        private async Task AddTestingFirstItemToDb()
        {
            this.DbContext.Items.Add(new Item
            {
                Id = 1,
                Name = FirstItemName,
                OrdinaryPrice = FistItemOrdinaryPrice,
                ExpressAddOnPrice = FirstItemExpressAddOnPrice,
                FlavorAddOnPrice = FirstItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = FirstItemVacuumCleaningAddOnPrice,
            });
            await this.DbContext.SaveChangesAsync();
        }

        private async Task AddTestingSecondItemToDb()
        {
            this.DbContext.Items.Add(new Item
            {
                Id = 2,
                Name = SecondItemName,
                OrdinaryPrice = SecondtemOrdinaryPrice,
                ExpressAddOnPrice = SecondItemExpressAddOnPrice,
                FlavorAddOnPrice = SecondItemFlavorAddOnPrice,
                VacuumCleaningAddOnPrice = SecondItemVacuumCleaningAddOnPrice,
            });
            await this.DbContext.SaveChangesAsync();
        }
    }
}
