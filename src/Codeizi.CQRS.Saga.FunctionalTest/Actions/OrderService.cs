using Codeizi.CQRS.Saga.FunctionalTest.ViewModels;
using Codeizi.DI.Anotations;
using System;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga.FunctionalTest.Actions
{
    [InjectableScoped]
    public class OrderService : IActionSaga
    {

        public Task<State> CancelAction(State stateFullSaga)
        {
            return Task.FromResult(stateFullSaga);
        }

        public async Task<State> Handle(State stateFullSaga)
        {
            await Task.Delay(200);
            var idOrder = Guid.NewGuid();
            var state = stateFullSaga.Get<InitialStateCheckoutViewModel>();
            return stateFullSaga;
        }
    }
}