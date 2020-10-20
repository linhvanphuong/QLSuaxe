using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IPermissionsRepository : IRepository<Permissions>
    {

    }
    public class PermissionsRepository : Repository<Permissions>, IPermissionsRepository
    {
        public PermissionsRepository(DbContext context) : base(context)
        {

        }
    }
}
