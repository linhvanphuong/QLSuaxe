using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface ITemporaryBill_AccesaryRepository : IRepository<TemporaryBill_Accesary>
    {

    }
    public class TemporaryBill_AccesaryRepository : Repository<TemporaryBill_Accesary>, ITemporaryBill_AccesaryRepository
    {
        public TemporaryBill_AccesaryRepository(DbContext context) : base(context)
        {

        }
    }
}
