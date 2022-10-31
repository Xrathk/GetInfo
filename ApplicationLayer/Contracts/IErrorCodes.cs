using ApplicationLayer.Data;

namespace ApplicationLayer.Contracts
{
    /// <summary>
    /// Interface for the error code service.
    /// </summary>
    public interface IErrorCodes
    {
        /// <summary>
        /// List of all existing error codes. Each error code corresponds to a specific error.
        /// </summary>
        List<AppError> AppErrors { get; set; }

        /// <summary>
        /// Finds the description of an error.
        /// </summary>
        /// <param name="errorCode">The error code of the error</param>
        /// <returns>The corresponding description.</returns>
        string FindDescription(int errorCode);
    }
}