using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.DAO
{
    public class StateDAO
    {
        private readonly IServiceProvider _serviceProvider;

        public StateDAO(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal async Task<SagaState> GetStateByActionId(Guid id)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            return await context.
                        States.
                        FirstOrDefaultAsync(x => x.SagaInfoId.Equals(id));
        }

        internal async Task Update(SagaState sagaState)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            context.States.Update(sagaState);
            await context.SaveChangesAsync();
        }

    }
}