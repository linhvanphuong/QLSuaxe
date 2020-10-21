using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface ITemporaryBillRepository : IRepository<TemporaryBill>
    {

    }
    public class TemporaryBillRepository : Repository<TemporaryBill>, ITemporaryBillRepository
    {
        public TemporaryBillRepository(DbContext context) : base(context)
        {

        }
    }
}
