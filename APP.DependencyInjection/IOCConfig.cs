using APP.MANAGER;
using APP.MODELS;
using APP.REPOSITORY;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.DependencyInjection
{
    public class IOCConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            // Db
            services.AddDbContext<APPDbContext>(ServiceLifetime.Scoped, ServiceLifetime.Singleton);

            services.AddTransient<IDbContextFactory<APPDbContext>, APPDbContextFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IJobPositionsManager, JobPositionsManager>();
            services.AddTransient<IEmployeeManager, EmployeeManager>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IMenuManager, MenuManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<IMotorManufactureManager, MotorManufactureManager>();
            services.AddTransient<IMotorTypesManager, MotorTypesManager>();
            services.AddTransient<IServicesManager, ServicesManager>();
            services.AddTransient<IMotorLiftsManager, MotorLiftsManager>();
            services.AddTransient<ISupplierManager, SupplierManager>();
            services.AddTransient<ICustomersManager, CustomersManager>();
            services.AddTransient<IAccessoriesManager, AccessoriesManager>();
            services.AddTransient<ITemporaryBillManager, TemporaryBillManager>();
        }
    }
}
