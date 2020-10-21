using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface ICustomersRepository : IRepository<Customers>
    {

    }
    public class CustomersRepository : Repository<Customers>, ICustomersRepository
    {
        public CustomersRepository(DbContext context) : base(context)
        {

        }
    }
}
