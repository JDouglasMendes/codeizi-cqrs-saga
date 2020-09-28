using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.DAO
{
    internal class SagaActionsDAO
    {
        private readonly IServiceProvider _serviceProvider;

        internal SagaActionsDAO(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal List<Dictionary<Guid, SagaAction>> GetActions(StatusOperation statusOperation)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();

            var query = context.Actions
                .Where(x => x.Status == statusOperation)
                .AsEnumerable();

            return query.GroupBy(x => x.IdSaga).
                Select(x =>
                new Dictionary<Guid, SagaAction>()
                {
                    { x.Key, x.First() }
                }).ToList();
        }

        internal List<Dictionary<Guid, SagaAction>> GetActionsForCancel()
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();

            var query = context.Actions
                .Where(x => x.Status == StatusOperation.Fail || x.Status == StatusOperation.WaitCancel)
                .AsEnumerable();

            return query.OrderByDescending(x => x.Position).
                GroupBy(x => x.IdSaga).
                Select(x =>
                new Dictionary<Guid, SagaAction>()
                {
                    { x.Key, x.First() }
                }).ToList();
        }


        internal async Task<SagaAction> Get(Guid id)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            return await context.
                    Actions.
                    FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        internal async Task Update(SagaAction action)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            context.Actions.Update(action);
            await context.SaveChangesAsync();
        }

        internal async Task<List<SagaAction>> GetActionFinshed()
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            var results = from actions in context.Actions
                          where !context.Actions.Any(x => x.Status == StatusOperation.Wait ||
                          x.Status == StatusOperation.Scheduled)
                          select actions;

            return await results.ToListAsync();
        }

        internal async Task Remove(List<SagaAction> sagaActions)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            context.Actions.RemoveRange(sagaActions);
            await context.SaveChangesAsync();
        }

        internal async Task UpdateToFail(SagaAction sagaAction)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            var actions = await context.Actions.Where(x => x.IdSaga.Equals(sagaAction.IdSaga)).ToListAsync();

            actions.ForEach(x =>
            {
                if (x.Position == sagaAction.Position)
                    x.Status = StatusOperation.Fail;
                else if (x.Position < sagaAction.Position)
                    x.Status = StatusOperation.WaitCancel;
                else
                    x.Status = StatusOperation.NotExecuted;
            });            
            context.Actions.UpdateRange(actions);
            await context.SaveChangesAsync();
        }
    }
}