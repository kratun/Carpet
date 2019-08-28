namespace UnravelTravel.Services.Data.Tests
{
    using System;
    using System.Reflection;

    using Carpet.Data;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Data.Repositories;
    using Carpet.Services.Data;
    using Carpet.Services.Data.CustomerService;
    using Carpet.Services.Data.EmployeesService;
    using Carpet.Services.Data.OrderItemsService;
    using Carpet.Services.Data.OrdersService;
    using Carpet.Services.Data.OrderStatusService;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Items;
    using Carpet.Web.ViewModels;
    using Carpet.Web.ViewModels.Administration.Items;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class BaseServiceTests : IDisposable
    {
        protected BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services
                .AddIdentity<CarpetUser, CarpetRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IItemsService, ItemsService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IVehiclesService, VehiclesService>();
            services.AddTransient<IEmployeesService, EmployeesService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<ICarpetUsersService, CarpetUsersService>();
            services.AddTransient<IGarageService, GarageService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IOrderItemsService, OrderItemsService>();

            // Identity stores
            services.AddTransient<IUserStore<CarpetUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<CarpetRole>, ApplicationRoleStore>();

            // AutoMapper
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(ItemIndexViewModel).GetTypeInfo().Assembly,
                typeof(ItemCreateInputModel).GetTypeInfo().Assembly,
                typeof(Item).GetTypeInfo().Assembly);

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }
    }
}