
namespace Wonderland.Logic.SagePay
{
    /// <summary>
    /// Response types that could be received from SagePay
    /// </summary>
    public enum StatusType
    {
        Unknown,
        Ok,
        NotAuthed,
        Abort,
        Rejected,
        Authenticated,
        Registered,
        Malformed,
        Error,
        Invalid,
    }
}
