using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.Data;
using Codeizi.CQRS.Saga.Logs;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.DAO
{
    public class SagaLogDAO
    {
        private readonly IServiceProvider _serviceProvider;

        public SagaLogDAO(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Save(Log log)
        {
            var sagaLog = new SagaLog
            {
                Created = log.Created,
                Ended = log.Ended,
                SagaId = log.SagaId,
                DataLog = JsonConvert.SerializeObject(log)
            };

            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            await context.AddAsync(sagaLog);
            await context.SaveChangesAsync();
        }
    }
}