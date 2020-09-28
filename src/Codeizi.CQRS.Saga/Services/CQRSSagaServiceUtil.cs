using Codeizi.CQRS.Saga.BackgroundServices;
using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Codeizi.CQRS.Saga.Services
{
    public static class CQRSSagaServiceUtil
    {
 
        public static IServiceCollection AddCQRSSaga(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> options)
        {
            services.Configure<BackgroundTaskConfigurations>(
                options => 
                configuration.GetSection("BackgroundTaskConfigurations").Bind(options));

            services.AddDbContext<SagaContext>(options, ServiceLifetime.Transient);
            services.AddSingleton<ExecutionScheduling>();
            services.AddSingleton<ManagerExecution>();
            services.AddHostedService<ExecutionBackgroungTaskServive>();
            services.AddHostedService<SchedulingBackgroundService>();
            
            return services;
        }
    }
}