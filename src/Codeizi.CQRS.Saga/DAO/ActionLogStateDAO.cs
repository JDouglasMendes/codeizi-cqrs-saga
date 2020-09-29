using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.DAO
{
    public class ActionLogStateDAO
    {
        private readonly IServiceProvider _serviceProvider;

        public ActionLogStateDAO(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<List<SagaStateLog>> GetLogBySagaId(Guid id)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            return await context.
                LogStateAction.
                Where(x => x.SagaId.Equals(id)).
                ToListAsync();
        }

        internal async Task Save(SagaStateLog sagaLogStateAction)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            await context.LogStateAction.AddAsync(sagaLogStateAction);
            await context.SaveChangesAsync();
        }
    }
}