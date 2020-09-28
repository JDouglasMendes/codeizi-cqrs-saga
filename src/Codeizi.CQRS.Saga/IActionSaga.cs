using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga
{
    public interface IActionSaga
    {
        Task<State> Handle(State stateFullSaga);
        Task<State> CancelAction(State stateFullSaga);        
    }
}