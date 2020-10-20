using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IRolesRepository : IRepository<Roles>
    {

    }
    public class RolesRepository : Repository<Roles>, IRolesRepository
    {
        public RolesRepository(DbContext context) : base(context)
        {

        }
    }
}
