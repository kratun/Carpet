﻿namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Data.Models;
    using Carpet.Web.InputModels.Administration.Items;
    using Carpet.Web.ViewModels.Administration.Items;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public interface IItemsService
    {
        IQueryable<TViewModel> GetAllItemsAsync<TViewModel>();

        Task<ItemIndexViewModel> CreateAsync(ItemCreateInputModel itemFromView, ModelStateDictionary modelState);

        Task<TViewModel> GetByIdAsync<TViewModel>(int id);

        Task<ItemEditViewModel> EditByIdAsync(int id, ItemEditInputModel itemFromView, ModelStateDictionary modelState);

        Task<ItemDeleteViewModel> DeleteByIdAsync(int id);
    }
}
