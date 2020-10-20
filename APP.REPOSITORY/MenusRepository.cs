using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IMenusRepository : IRepository<Menus>
    {

    }
    public class MenusRepository : Repository<Menus>, IMenusRepository
    {
        public MenusRepository(DbContext context) : base(context)
        {

        }
    }
}
