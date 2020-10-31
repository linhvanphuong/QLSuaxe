using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace APP.MODELS
{
    public class APPDbContext : DbContext
    {
        private static readonly MethodInfo _propertyMethod = typeof(EF).GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(typeof(bool));
        public APPDbContext(DbContextOptions<APPDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.HasDefaultSchema("orcl");
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<JobPositions> JobPositions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Account_Roles> Account_Roles { get; set; }
        public DbSet<MotorManufacture> MotorManufacture { get; set; }
        public DbSet<MotorTypes> MotorTypes { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<MotorLifts> MotorLifts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Accessories> Accessories { get; set; }
        public DbSet<TemporaryBill> TemporaryBill { get; set; }
        public DbSet<TemporaryBill_Accesary> TemporaryBill_Accesary { get; set; }
        public DbSet<TemporaryBill_Service> TemporaryBill_Service { get; set; }
        public DbSet<ImportReceipt> ImportReceipt { get; set; }
        public DbSet<ImportReceipt_Accessory> ImportReceipt_Accessory { get; set; }
    }
}
