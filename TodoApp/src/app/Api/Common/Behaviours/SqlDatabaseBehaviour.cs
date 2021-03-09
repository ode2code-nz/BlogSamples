using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Api.Common.Behaviours
{
public class SqlDatabaseBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase
    {
        private readonly IUnitOfWork _db;
        private readonly ILogger<TRequest> _logger;

        public SqlDatabaseBehaviour(IUnitOfWork db, ILogger<TRequest> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var strategy = _db.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                try
                {
                    await _db.BeginTransactionAsync(GetIsolationLevelBasedOnHandlerType());

                    var response = await next();

                    await _db.CommitTransactionAsync();

                    return response;
                }
                catch (DbUpdateException ex)
                {
                    _db.RollbackTransaction();

                    _logger.LogError(ex, "Database update exception.");

                    return _db.HandleDatabaseException(ex, typeof(TRequest).Name) as TResponse;
                }
            });
        }

        public IsolationLevel GetIsolationLevelBasedOnHandlerType()
        {
            return typeof(TRequest).FullName.EndsWith("QueryHandler") 
                ? IsolationLevel.ReadUncommitted 
                : IsolationLevel.ReadCommitted;
        }
    }
}
