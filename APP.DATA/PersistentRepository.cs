using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data
{
    public abstract class PersistentRepository<T> : Repository<T> where T : class, IAuditEntity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected PersistentRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected string CurrentUser =>
            _httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "Unknown";

        public async override Task<T> Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = CurrentUser;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = CurrentUser;
            //entity.DeletedAt = null;
            return await base.Add(entity);
        }

        //public async override Task Update(T entity)
        //{
        //    entity.LastUpdatedDate = DateTime.Now;
        //    entity.LastUpdatedBy = CurrentUser;
        //    await base.Update(entity);
        //}

        //public async override Task BulkUpdate(IList<T> items)
        //{
        //    if (items == null || !items.Any())
        //        return;

        //    await base.BulkUpdate(items.Select(x => { x.LastUpdatedDate = DateTime.Now; x.LastUpdatedBy = CurrentUser; return x; }).ToList());
        //}


        //public override async Task BulkInsert(IList<T> items)
        //{
        //    await base.BulkInsert(items.Select(x => { x.LastUpdatedDate = DateTime.Now; x.LastUpdatedBy = CurrentUser; x.CreatedDate = DateTime.Now; x.CreatedBy = CurrentUser; return x; }).ToArray());
        //}

        //public override async Task Delete(T entity)
        //{
        //    if (entity == null)
        //        return;

        //    entity.IsDeleted = true;
        //    entity.DeletedAt = DateTime.Now;
        //    entity.LastUpdatedBy = CurrentUser;
        //    entity.LastUpdatedDate = DateTime.Now;

        //    await Update(entity);
        //}

        //public override async Task BulkDelete(IList<T> items)
        //{
        //    if (items == null || !items.Any())
        //        return;

        //    await BulkUpdate(items.Select(x =>
        //    {
        //        x.IsDeleted = true; x.DeletedAt = DateTime.Now; return x;
        //    }).ToList());
        //}
        //public override async Task<int> BulkDelete(IQueryable<T> items)
        //{
        //    var entity = (T)Activator.CreateInstance(typeof(T));
        //    entity.IsDeleted = true;
        //    entity.DeletedAt = DateTime.Now;
        //    entity.LastUpdatedBy = CurrentUser;
        //    entity.LastUpdatedDate = DateTime.Now;
        //    return await items.BatchUpdateAsync(h => entity);
        //}
    }
}
