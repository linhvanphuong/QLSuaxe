using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IImportReceipt_AccessoryRepository : IRepository<ImportReceipt_Accessory>
    {

    }
    public class ImportReceipt_AccessoryRepository : Repository<ImportReceipt_Accessory>, IImportReceipt_AccessoryRepository
    {
        public ImportReceipt_AccessoryRepository(DbContext context) : base(context)
        {

        }
    }
}
