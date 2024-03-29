﻿namespace Carpet.Web.ViewModels.Administration.Orders.AddItems
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderOrderItemItemAddItemsViewModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal OrdinaryPrice { get; set; }

        public decimal ExpressAddOnPrice { get; set; }

        public decimal VacuumCleaningAddOnPrice { get; set; }

        public decimal FlavorAddOnPrice { get; set; }
    }
}
