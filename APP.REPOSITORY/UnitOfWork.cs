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
        public IServicesRepository ServicesRepository { get; }
        public IMotorLiftsRepository MotorLiftsRepository { get; }
        public ISupplierRepository SupplierRepository { get; }
        public ICustomersRepository CustomersRepository { get; }
        public IAccessoriesRepository AccessoriesRepository { get; set; }
        public ITemporaryBill_ServiceRepository TemporaryBill_ServiceRepository { get; set; }
        public ITemporaryBillRepository TemporaryBillRepository { get; set; }
        public ITemporaryBill_AccesaryRepository TemporaryBill_AccesaryRepository { get; set; }
        public IImportReceiptRepository ImportReceiptRepository { get; set; }
        public IImportReceipt_AccessoryRepository ImportReceipt_AccessoryRepository { get; set; }
        public IServicePriceHistoryRepository ServicePriceHistoryRepository { get; set; }
        public IAccessoryPriceHistoryRepository AccessoryPriceHistoryRepository { get; set; }

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
            ServicesRepository = new ServicesRepository(_dbContext);
            MotorLiftsRepository = new MotorLiftsRepository(_dbContext);
            SupplierRepository = new SupplierRepository(_dbContext);
            CustomersRepository = new CustomersRepository(_dbContext);
            AccessoriesRepository = new AccessoriesRepository(_dbContext);
            TemporaryBill_ServiceRepository = new TemporaryBill_ServiceRepository(_dbContext);
            TemporaryBillRepository = new TemporaryBillRepository(_dbContext);
            TemporaryBill_AccesaryRepository = new TemporaryBill_AccesaryRepository(_dbContext);
            ImportReceiptRepository = new ImportReceiptRepository(_dbContext);
            ImportReceipt_AccessoryRepository = new ImportReceipt_AccessoryRepository(_dbContext);
            ServicePriceHistoryRepository = new ServicePriceHistoryRepository(_dbContext);
            AccessoryPriceHistoryRepository = new AccessoryPriceHistoryRepository(_dbContext);
            
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
        public IServicesRepository ServicesRepository { get; }
        public IMotorLiftsRepository MotorLiftsRepository { get; }
        public ISupplierRepository SupplierRepository { get; }
        public ICustomersRepository CustomersRepository { get; }
        public IAccessoriesRepository AccessoriesRepository { get; set; }
        public ITemporaryBill_ServiceRepository TemporaryBill_ServiceRepository { get; set; }
        public ITemporaryBillRepository TemporaryBillRepository { get; set; }
        public ITemporaryBill_AccesaryRepository TemporaryBill_AccesaryRepository { get; set; }
        public IImportReceiptRepository ImportReceiptRepository { get; set; }
        public IImportReceipt_AccessoryRepository ImportReceipt_AccessoryRepository { get; set; }
        public IServicePriceHistoryRepository ServicePriceHistoryRepository { get; set; }
        public IAccessoryPriceHistoryRepository AccessoryPriceHistoryRepository { get; set; }
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
