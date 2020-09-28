using Codeizi.CQRS.Saga.DAO;
using Codeizi.CQRS.Saga.Data;
using Codeizi.CQRS.Saga.Utils;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.Execution
{
    public class ManagerExecution
    {
        private readonly IServiceProvider _services;
        private readonly ReflectionUtil reflectionUtil;
        private readonly ActionSchudeleDAO actionSchudeleDAO;
        private readonly SagaActionsDAO actionsDAO;
        private readonly StateDAO stateDAO;
        private readonly ActionLogStateDAO actionLogStateDAO;

        public ManagerExecution(
            IServiceProvider services)
            : this()
        {
            _services = services;
            actionSchudeleDAO = services.GetRequiredService<ActionSchudeleDAO>();
            actionsDAO = services.GetRequiredService<SagaActionsDAO>();
            stateDAO = services.GetRequiredService<StateDAO>();
            actionLogStateDAO = services.GetRequiredService<ActionLogStateDAO>();
        }

        private ManagerExecution()
            => reflectionUtil = new ReflectionUtil();

        public async Task Execute()
        {
            var actions = await actionSchudeleDAO.GetAll();
            foreach (var scheduledAction in actions)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await ExecuteAsync(scheduledAction);
                    }
                    catch
                    {
                        await actionsDAO.UpdateToFail(
                            await actionsDAO.Get(scheduledAction.SagaActionId));
                    }
                });
                try
                {
                    await actionSchudeleDAO.Remove(scheduledAction);
                }
                catch { }
            }
        }

        private async Task ExecuteAsync(SagaScheduling scheduledAction)
        {
            var type = reflectionUtil.GetTypeByName(scheduledAction.Type);

            if (type == null)
                return;

            if (_services.GetService(type)
                      is IActionSaga executionAction)
            {
                var sagaState = await stateDAO.GetStateByActionId(scheduledAction.SagaId);

                var typeState = reflectionUtil.GetTypeByName(scheduledAction.TypeState);

                var state = new State(scheduledAction.SagaId);
                if (sagaState != null)
                    state.Value = JsonConvert.DeserializeObject(sagaState.ExtendedData, typeState);

                var action = await actionsDAO.Get(scheduledAction.SagaActionId);

                action.Initiate = DateTime.Now;

                State newState;
                if (scheduledAction.Cancel)
                    newState = await executionAction.CancelAction(state);
                else
                    newState = await executionAction.Handle(state);

                await UpdateState(sagaState, state, newState, action);

                action.Ended = DateTime.Now;
                action.Status = StatusOperation.Success;
                await actionsDAO.Update(action);
            }
        }

        private async Task UpdateState(
            SagaState sagaState,
            State initialState,
            State newState,
            SagaAction sagaActions)
        {
            sagaState.ExtendedData = JsonConvert.SerializeObject(newState.Value);
            await stateDAO.Update(sagaState);

            var sagaLog = new SagaStateLog
            {
                InitialState = JsonConvert.SerializeObject(initialState.Value),
                FinshedState = JsonConvert.SerializeObject(newState.Value),
                SagaId = sagaState.SagaInfoId,
                ActionId = sagaActions.Id
            };

            await actionLogStateDAO.Save(sagaLog);
        }
    }
}