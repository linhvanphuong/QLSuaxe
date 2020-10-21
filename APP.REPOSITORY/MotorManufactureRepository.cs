using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IMotorManufactureRepository : IRepository<MotorManufacture>
    {

    }
    public class MotorManufactureRepository: Repository<MotorManufacture>, IMotorManufactureRepository
    {
        public MotorManufactureRepository(DbContext context) : base(context)
        {

        }
    }
}
