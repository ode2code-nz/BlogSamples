using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDo.Domain.Model.MasterTradingAgreements;
using ToDo.Domain.Model.ToDos;

namespace ToDo.Infrastructure.Interfaces
{
    // This is used by Command Handlers in CQRS
    public interface IUnitOfWork
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
        DbSet<MasterTradingAgreement> MasterTradingAgreements { get; set; }

        Result HandleDatabaseException(DbUpdateException dbUpdateException, string propertyName);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IExecutionStrategy CreateExecutionStrategy();
        Task BeginTransactionAsync(IsolationLevel level = IsolationLevel.ReadCommitted);
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}