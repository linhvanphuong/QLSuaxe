using System;
using System.Collections.Generic;
using System.Text;
using APP.MODELS;
using Microsoft.EntityFrameworkCore;
using Portal.Data;

namespace APP.REPOSITORY
{
    public interface IImportReceiptRepository : IRepository<ImportReceipt>
    {

    }
    public class ImportReceiptRepository : Repository<ImportReceipt>, IImportReceiptRepository
    {
        public ImportReceiptRepository(DbContext context) : base(context)
        {

        }
    }
}
