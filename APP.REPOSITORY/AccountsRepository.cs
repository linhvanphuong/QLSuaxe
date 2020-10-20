using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IAccountsRepository : IRepository<Accounts>
    {

    }
    public class AccountsRepository : Repository<Accounts>, IAccountsRepository
    {
        public AccountsRepository(DbContext context) : base(context)
        {

        }
    }
}
