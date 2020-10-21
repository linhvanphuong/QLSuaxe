using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IAccessoriesRepository : IRepository<Accessories>
    {

    }
    public class AccessoriesRepository : Repository<Accessories>, IAccessoriesRepository
    {
        public AccessoriesRepository(DbContext context) : base(context)
        {

        }
    }
}
