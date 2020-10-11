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

        }
    }
}
