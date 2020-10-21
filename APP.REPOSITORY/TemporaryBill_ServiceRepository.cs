using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface ITemporaryBill_ServiceRepository : IRepository<TemporaryBill_Service>
    {

    }
    public class TemporaryBill_ServiceRepository : Repository<TemporaryBill_Service>, ITemporaryBill_ServiceRepository
    {
        public TemporaryBill_ServiceRepository(DbContext context) : base(context)
        {

        }
    }
}
