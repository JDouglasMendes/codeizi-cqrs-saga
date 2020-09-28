using Codeizi.CQRS.Saga.Execution;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.BackgroundServices
{
    public class ExecutionBackgroungTaskServive : BackgroundService
    {
        private readonly BackgroundTaskConfigurations _settings;
        private readonly ManagerExecution _managerExecution;

        public ExecutionBackgroungTaskServive(IOptions<BackgroundTaskConfigurations> settings,
                                              ManagerExecution managerExecution)
        {
            _settings = settings?.Value;
            _managerExecution = managerExecution;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _managerExecution.Execute();
                await Task.Delay(_settings.ExecutionCheckUpdateTime > 0 ? _settings.ExecutionCheckUpdateTime : 500,
                                stoppingToken);
            }
            await Task.CompletedTask;
        }
    }
}