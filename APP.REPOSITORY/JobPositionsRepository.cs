using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IJobPositionsRepository : IRepository<JobPositions>
    {

    }
    public class JobPositionsRepository : Repository<JobPositions>, IJobPositionsRepository
    {
        public JobPositionsRepository(DbContext context) : base(context)
        {

        }
    }
}
