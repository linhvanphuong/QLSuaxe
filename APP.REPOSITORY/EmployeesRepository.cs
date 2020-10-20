using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IEmployeesRepository : IRepository<Employees>
    {

    }
    public class EmployeesRepository : Repository<Employees>, IEmployeesRepository
    {
        public EmployeesRepository(DbContext context) : base(context)
        {

        }
    }
}
