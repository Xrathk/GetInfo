using ApplicationLayer.Data;

namespace ApplicationLayer.Contracts
{
    /// <summary>
    /// Interface for the error code service.
    /// </summary>
    public interface IErrorCodes
    {
        List<AppError> AppErrors { get; set; }

        string FindDescription(int errorCode);
    }
}