using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IAccount_RolesRepository : IRepository<Account_Roles>
    {

    }
    public class Account_RolesRepository : Repository<Account_Roles>, IAccount_RolesRepository
    {
        public Account_RolesRepository(DbContext context) : base(context)
        {

        }
    }
}
