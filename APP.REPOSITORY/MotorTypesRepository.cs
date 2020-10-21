using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IMotorTypesRepository : IRepository<MotorTypes>
    {

    }
    public class MotorTypesRepository: Repository<MotorTypes>, IMotorTypesRepository
    {
        public MotorTypesRepository(DbContext context) : base(context)
        {

        }
    }
}
