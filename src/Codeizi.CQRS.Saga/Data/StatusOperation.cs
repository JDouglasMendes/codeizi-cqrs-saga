namespace Codeizi.CQRS.Saga.Data
{
    public enum StatusOperation
    {
        Wait,
        Scheduled,
        Run,
        Success,
        Fail,
        WaitCancel,
        CancelSuccess,
        NotExecuted
    }
}