
namespace Wonderland.Logic.SagePay
{
    /// <summary>
    /// Response types that could be received from SagePay
    /// </summary>
    public enum TransactionRegistrationStatus
    {
        OK,

        OKREPEATED,

        MALFORMED,

        INVALID,

        ERROR
    }
}
