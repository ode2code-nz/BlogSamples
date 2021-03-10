using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ApiSample.Domain.Model.MasterTradingAgreements;
using ApiSample.Domain.Model.StaticData;
using ApiSample.Domain.Model.ToDos;
using ApiSample.Infrastructure.Data.DataSeed;
using ApiSample.Infrastructure.Identity;
using ApiSample.Infrastructure.Interfaces;

namespace ApiSample.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<MasterTradingAgreement> MasterTradingAgreements { get; set; }

        // static data
        public DbSet<Company> Companies { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            StaticDataSeed.Seed(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                // Just log SQL statements
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            optionsBuilder
                //.UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();      // should be used in dev environment only
        }


        #region Transactions

        private IDbContextTransaction _currentTransaction;

        public Result HandleDatabaseException(DbUpdateException dbUpdateException, string propertyName)
        {
            return DbUtil.HandleDatabaseException(dbUpdateException, propertyName);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }

        public async Task BeginTransactionAsync(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await Database.BeginTransactionAsync(level);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        #endregion
    }
}