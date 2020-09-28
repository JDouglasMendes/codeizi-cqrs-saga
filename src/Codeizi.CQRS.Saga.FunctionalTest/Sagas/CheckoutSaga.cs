using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.FunctionalTest.Actions;
using Codeizi.DI.Anotations;

namespace Codeizi.CQRS.Saga.FunctionalTest.Sagas
{
    [InjectableScoped]
    public class CheckoutSaga : Saga
    {
        public CheckoutSaga(SagaContext db)
            : base(db)
        {
            AddActionSaga<OrderService>();
        }
    }
}