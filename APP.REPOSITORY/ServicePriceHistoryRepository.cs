using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IServicePriceHistoryRepository : IRepository<ServicePriceHistory>
    {

    }
    public class ServicePriceHistoryRepository : Repository<ServicePriceHistory>, IServicePriceHistoryRepository
    {
        public ServicePriceHistoryRepository(DbContext context) : base(context)
        {

        }
    }
}
