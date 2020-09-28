using Codeizi.CQRS.Saga.Data;
using System;

namespace Codeizi.CQRS.Saga.Logs
{
    public class ActionLog
    {
        public string Type { get; set; }
        public StatusOperation Status { get; set; }
        public object InitialState { get; set; }
        public object FinishedState { get; set; }
        public DateTime Created { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime Initiate { get; set; }
        public DateTime Ended { get; set; }
    }
}