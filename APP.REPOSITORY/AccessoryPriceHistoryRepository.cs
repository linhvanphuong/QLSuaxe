using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IAccessoryPriceHistoryRepository : IRepository<AccessoryPriceHistory>
    {

    }
    public class AccessoryPriceHistoryRepository : Repository<AccessoryPriceHistory>, IAccessoryPriceHistoryRepository
    {
        public AccessoryPriceHistoryRepository(DbContext context) : base(context)
        {

        }
    }
}
