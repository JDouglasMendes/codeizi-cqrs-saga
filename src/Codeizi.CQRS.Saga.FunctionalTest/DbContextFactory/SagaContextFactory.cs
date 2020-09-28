using Codeizi.CQRS.Saga.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Codeizi.CQRS.Saga.FunctionalTest.DbContextFactory
{
    public class SagaContextFactory : IDesignTimeDbContextFactory<SagaContext>
    {
        public SagaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SagaContext>();
            optionsBuilder.UseSqlServer(GetConfiguration().GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Codeizi.CQRS.Saga.FunctionalTest"));

            return new SagaContext(optionsBuilder.Options);
        }

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}