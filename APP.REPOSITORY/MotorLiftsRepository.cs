using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IMotorLiftsRepository : IRepository<MotorLifts>
    {

    }
    public class MotorLiftsRepository : Repository<MotorLifts>, IMotorLiftsRepository
    {
        public MotorLiftsRepository(DbContext context) : base(context)
        {

        }
    }
}
