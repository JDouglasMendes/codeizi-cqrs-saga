using Codeizi.CQRS.Saga.Context;
using Codeizi.CQRS.Saga.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.CQRS.Saga
{
    public class Saga
    {
        private readonly SagaContext _db;

        public Saga(SagaContext db)
            : this()
            => _db = db;

        private readonly List<Type> actions;

        private Saga()
            => actions = new List<Type>();

        protected Saga AddActionSaga(Type actionSaga)
        {
            if (!CheckTypeIsAction(actionSaga))
                throw new ArgumentException($"{nameof(actionSaga)} not implementation {nameof(IActionSaga)}");

            actions.Add(actionSaga);
            return this;
        }

        protected Saga AddActionSaga<T>() where T : class
            => AddActionSaga(typeof(T));

        private bool CheckTypeIsAction(Type type)
            => type.GetInterfaces().Contains(typeof(IActionSaga));

        public async Task<Guid> Start(object initialState)
        {
            var idSaga = Guid.NewGuid();
            var state = new State(idSaga)
            {
                Value = initialState
            };

            await AddSaga(idSaga);

            await AddSagaActions(idSaga, initialState.GetType().FullName);

            await AddStateSaga(idSaga, state);

            await _db.SaveChangesAsync();

            return idSaga;
        }

        private async Task AddSaga(Guid idSaga)
        {
            await _db.SagaInfo.AddAsync(new SagaInfo
            {
                CreationDate = DateTime.Now,
                Id = idSaga,
            });
        }

        private async Task AddStateSaga(Guid idSaga, State state)
        {
            var sagaState = new SagaState
            {
                SagaInfoId = idSaga,
                ExtendedData = JsonConvert.SerializeObject(state.Value)
            };
            await _db.States.AddRangeAsync(sagaState);
        }

        private async Task AddSagaActions(Guid id, string typeState)
        {
            byte position = 0;
            var sagaActions = actions.ConvertAll(new Converter<Type, SagaAction>((type) =>
            {
                position++;
                return new SagaAction()
                {
                    IdSaga = id,
                    Status = StatusOperation.Wait,
                    Type = type.FullName,
                    Position = position,
                    TypeState = typeState,
                    Created = DateTime.Now,                    
                };
            }));

            await _db.AddRangeAsync(sagaActions);
        }
    }
}