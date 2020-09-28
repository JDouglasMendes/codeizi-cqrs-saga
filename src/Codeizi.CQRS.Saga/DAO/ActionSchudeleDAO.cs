using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.DAO
{
    public class ActionSchudeleDAO
    {
        private readonly IServiceProvider _serviceProvider;
        public ActionSchudeleDAO(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal async Task Save(SagaScheduling schudele)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            await context.ActionSchudele.AddAsync(schudele);
            await context.SaveChangesAsync();
        }

        internal async Task<List<SagaScheduling>> GetAll()
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            return await context.ActionSchudele.ToListAsync();
        }

        internal async Task Remove(SagaScheduling schudele)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            context.ActionSchudele.Remove(schudele);
            await context.SaveChangesAsync();
        }

    }
}
