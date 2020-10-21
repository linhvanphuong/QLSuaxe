using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IServicesRepository : IRepository<Services>
    {

    }
    public class ServicesRepository : Repository<Services>, IServicesRepository
    {
        public ServicesRepository(DbContext context) : base(context)
        {

        }
    }
}
