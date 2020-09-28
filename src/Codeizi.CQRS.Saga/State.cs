using System;
using System.Dynamic;

namespace Codeizi.CQRS.Saga
{
    public class State
    {
        public Guid IdSaga { get; }

        public State(Guid id)
            : this()
            => (IdSaga) = (id);

        private State()
            => Value = new ExpandoObject();

        public T Get<T>()
            => (T)Value;

        public dynamic Value { get; set; }
    }
}