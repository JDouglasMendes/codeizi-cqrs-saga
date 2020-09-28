using Codeizi.CQRS.Saga.Data;
using Microsoft.EntityFrameworkCore;

namespace Codeizi.CQRS.Saga.Context
{
    public class SagaContext : DbContext
    {
        public SagaContext(DbContextOptions<SagaContext> options)
               : base(options)
        { }
        internal DbSet<SagaAction> Actions { get; set; }
        internal DbSet<SagaState> States { get; set; }
        internal DbSet<SagaInfo> SagaInfo { get; set; }
        internal DbSet<SagaScheduling>  ActionSchudele {get; set;}   
        internal DbSet<SagaStateLog> LogStateAction { get; set; }
        internal DbSet<SagaLog> SagaLog { get; set; }
    }
}