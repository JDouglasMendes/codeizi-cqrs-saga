using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.DAO
{
    public class SagaInfoDAO
    {
        private readonly IServiceProvider _serviceProvider;

        public SagaInfoDAO(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<SagaInfo> Get(Guid id)
        {
            using var context = _serviceProvider.GetRequiredService<SagaContext>();
            return await context.SagaInfo.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}