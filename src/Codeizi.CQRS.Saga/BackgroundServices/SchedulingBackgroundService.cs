using Codeizi.CQRS.Saga.Execution;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.BackgroundServices
{
    public class SchedulingBackgroundService : BackgroundService
    {
        private readonly BackgroundTaskConfigurations _settings;
        private readonly ExecutionScheduling _executionScheduling;

        public SchedulingBackgroundService(IOptions<BackgroundTaskConfigurations> settings,
                                           ExecutionScheduling executionScheduling)
        {
            _settings = settings?.Value;
            _executionScheduling = executionScheduling;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _executionScheduling.ScheduleWait();
                await _executionScheduling.SchuduleFail();

                await Task.Delay(_settings.SchudeleCheckUpdateTime > 0 ? _settings.SchudeleCheckUpdateTime : 100,
                                stoppingToken);

                await _executionScheduling.ClearFinishedSaga();
            }
            await Task.CompletedTask;
        }
    }
}