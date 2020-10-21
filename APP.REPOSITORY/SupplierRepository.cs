using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface ISupplierRepository : IRepository<Supplier>
    {

    }
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DbContext context) : base(context)
        {

        }
    }
}
