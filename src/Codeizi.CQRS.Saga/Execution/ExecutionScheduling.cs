using Codeizi.CQRS.Saga.BackgroundServices;
using Codeizi.CQRS.Saga.DAO;
using Codeizi.CQRS.Saga.Data;
using Codeizi.CQRS.Saga.Logs;
using Codeizi.CQRS.Saga.Utils;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.Execution
{
    public class ExecutionScheduling
    {
        private readonly SagaActionsDAO ActionDAO;
        private readonly ActionSchudeleDAO actionSchudeleDAO;
        private readonly ReflectionUtil reflectionUtil;
        private readonly SagaInfoDAO sagaInfoDAO;
        private readonly ActionLogStateDAO actionStateDAO;
        private readonly SagaLogDAO sagaLogDAO;
        private readonly StateDAO stateDAO;

        public ExecutionScheduling(IServiceProvider serviceProvider)
            : this()
            => (ActionDAO, actionSchudeleDAO, sagaInfoDAO, actionStateDAO, sagaLogDAO, stateDAO) =
                (serviceProvider.GetRequiredService<SagaActionsDAO>(),
                 serviceProvider.GetRequiredService<ActionSchudeleDAO>(),
                 serviceProvider.GetRequiredService<SagaInfoDAO>(),
                 serviceProvider.GetRequiredService<ActionLogStateDAO>(),
                 serviceProvider.GetRequiredService<SagaLogDAO>(),
                 serviceProvider.GetRequiredService<StateDAO>());

        private ExecutionScheduling()
            => reflectionUtil = new ReflectionUtil();

        public async Task ScheduleWait()
        {
            var pendingActions = ActionDAO.GetActions(StatusOperation.Wait);
            var pending = pendingActions.ConvertAll(
                new Converter<Dictionary<Guid, SagaAction>, SagaAction>((p) =>
           {
               return p.First().Value;
           }));

            await Scheduling(pending, StatusOperation.Wait);
        }

        public async Task SchuduleFail()
        {
            var pendingActions = ActionDAO.GetActionsForCancel();
            var pending = pendingActions.ConvertAll(
               new Converter<Dictionary<Guid, SagaAction>, SagaAction>((p) =>
               {
                   return p.First().Value;
               }));

            await Scheduling(pending, StatusOperation.WaitCancel);
        }

        private async Task Scheduling(
            List<SagaAction> actions,
            StatusOperation statusOperation)
        {
            foreach (var action in actions)
            {
                var schudele = new SagaScheduling
                {
                    DateCreate = DateTime.Now,
                    SagaId = action.IdSaga,
                    Type = action.Type,
                    Cancel = statusOperation == StatusOperation.Fail || statusOperation == StatusOperation.WaitCancel,
                    TypeState = action.TypeState,
                    SagaActionId = action.Id,
                };

                await actionSchudeleDAO.Save(schudele);

                action.Status = StatusOperation.Scheduled;
                action.Scheduled = DateTime.Now;
                await ActionDAO.Update(action);
            }
        }

        private static readonly SemaphoreLocker _locker = new SemaphoreLocker();

        public async Task ClearFinishedSaga()
        {
            await _locker.LockAsync(async () =>
            {
                try
                {
                    var actionsFinished = await ActionDAO.GetActionFinshed();

                    if (actionsFinished == null || !actionsFinished.Any())
                        return;

                    foreach(var actions in actionsFinished.GroupBy(x => x.IdSaga))
                    {
                        var saga = await sagaInfoDAO.Get(actions.Key);
                        var log = await GetLog(actions.ToList(), saga);
                        await sagaLogDAO.Save(log);
                        await ActionDAO.Remove(actions.ToList());
                        await sagaInfoDAO.Remove(saga);
                        await stateDAO.RemoveBySagaId(saga.Id);
                    }
                }
                catch { }
            });
        }

        private async Task<Log> GetLog(
            List<SagaAction> actionsFinished,
            SagaInfo saga)
        {
            var states = await actionStateDAO.GetLogBySagaId(saga.Id);

            var actionsList = new List<ActionLog>();

            actionsFinished.ForEach(x =>
            {
                var initialState = states.FirstOrDefault(y => y.ActionId.Equals(x.Id)).InitialState;
                var finishedState = states.FirstOrDefault(y => y.ActionId.Equals(x.Id)).FinshedState;
                var type = reflectionUtil.GetTypeByName(x.TypeState);
                var action = new ActionLog
                {
                    Created = x.Created,
                    Ended = x.Ended,
                    Initiate = x.Initiate,
                    Scheduled = x.Scheduled,
                    Type = x.Type,
                    Status = x.Status,
                    InitialState = JsonConvert.DeserializeObject(initialState, type),
                    FinishedState = JsonConvert.DeserializeObject(finishedState, type)
                };
                actionsList.Add(action);
            });

            var log = new Log
            {
                SagaId = actionsFinished.First().IdSaga,
                Created = saga.CreationDate,
                Ended = DateTime.Now,
                ActionsLog = actionsList
            };
            return log;
        }
    }
}