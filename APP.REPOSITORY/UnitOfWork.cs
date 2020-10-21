using APP.MODELS;
using Microsoft.EntityFrameworkCore.Storage;
using Portal.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APP.REPOSITORY
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IAccountsRepository AccountsRepository { get; }
        public IAccount_RolesRepository Account_RolesRepository { get; }
        public IRolesRepository RolesRepository { get; }
        public IPermissionsRepository PermissionsRepository { get; }
        public IEmployeesRepository EmployeesRepository { get; }
        public IMenusRepository MenusRepository { get; }
        public IJobPositionsRepository JobPositionsRepository { get; }
        public IMotorManufactureRepository MotorManufactureRepository { get; }
        public IMotorTypesRepository MotorTypesRepository { get; }

        Task CreateTransaction();
        Task Commit();
        Task Rollback();
        Task SaveChange();
    }
    public class UnitOfWork : IUnitOfWork
    {
        APPDbContext _dbContext;
        IDbContextTransaction _transaction;

        public UnitOfWork(IDbContextFactory<APPDbContext> dbContextFactory, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContextFactory.GetContext();
            UserRepository = new UserRepository(_dbContext);
            AccountsRepository = new AccountsRepository(_dbContext);
            Account_RolesRepository = new Account_RolesRepository(_dbContext);
            RolesRepository = new RolesRepository(_dbContext);
            PermissionsRepository = new PermissionsRepository(_dbContext);
            EmployeesRepository = new EmployeesRepository(_dbContext);
            MenusRepository = new MenusRepository(_dbContext);
            JobPositionsRepository = new JobPositionsRepository(_dbContext);
            MotorManufactureRepository = new MotorManufactureRepository(_dbContext);
            MotorTypesRepository = new MotorTypesRepository(_dbContext);
        }
        #region Transaction
        public async Task CreateTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public async Task SaveChange()
        {
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        private bool disposedValue = false; // To detect redundant calls
        public IUserRepository UserRepository { get; }
        public IAccountsRepository AccountsRepository { get; }
        public IAccount_RolesRepository Account_RolesRepository { get; }
        public IRolesRepository RolesRepository { get; }
        public IPermissionsRepository PermissionsRepository { get; }
        public IEmployeesRepository EmployeesRepository { get; }
        public IMenusRepository MenusRepository { get; }
        public IJobPositionsRepository JobPositionsRepository { get; }
        public IMotorManufactureRepository MotorManufactureRepository { get; }
        public IMotorTypesRepository MotorTypesRepository { get; }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
