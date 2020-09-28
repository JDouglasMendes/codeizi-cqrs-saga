using System;
using System.Collections.Generic;

namespace Codeizi.CQRS.Saga.Logs
{
    public class Log
    {
        public Guid SagaId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Ended { get; set; }
        public List<ActionLog> ActionsLog { get; set; }
    }
}