using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.REPOSITORY
{
   
    public interface IUserRepository : IRepository<Users>
    {

    }
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }
    }
}
