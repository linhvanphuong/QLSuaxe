using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data
{
    public abstract class DbContextBase : DbContext
    {
        protected DbContextBase() : base()
        {

        }

        protected DbContextBase(DbContextOptions options) : base(options)
        {

        }

        

        

        //public override int SaveChanges()
        //{
        //    UpdateAuditInfo();
        //    return base.SaveChanges();
        //}

        //public override async Task<int> SaveChangesAsync()
        //{
        //    UpdateAuditInfo();
        //    return await base.SaveChangesAsync();
        //}

        

        //private void UpdateAuditInfo()
        //{
        //    var entries = ChangeTracker.Entries().Where(x => x.Entity is IAuditableEntity
        //                                                      && (x.State == EntityState.Added || x.State == EntityState.Modified))
        //        .Select(it => new { State = it.State, Entity = (IAuditableEntity)it.Entity });

        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedBy = GetCurrentUsername();
        //            entry.Entity.CreatedDate = DateTime.Now;
        //        }
        //        entry.Entity.LastModifiedBy = GetCurrentUsername();
        //        entry.Entity.LastModifiedDate = DateTime.Now;
        //    }
        //}
    }

}
