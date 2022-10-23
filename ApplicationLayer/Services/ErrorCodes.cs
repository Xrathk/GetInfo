using ApplicationLayer.Contracts;
using ApplicationLayer.Data;

namespace ApplicationLayer.Services
{
    /// <summary>
    /// Class that handles error codes. Includes all relevant error codes for the app.
    /// </summary>
    public class ErrorCodes : IErrorCodes
    {
        // List of errors
        public List<AppError> AppErrors { get; set; }

        /// <summary>
        /// Constructor. Initializes error list on app start-up.
        /// </summary>
        public ErrorCodes()
        {
            AppErrors = InitializeErrorList();
        }

        /// <summary>
        /// Finds the description for an error code.
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <returns>Error code description.</returns>
        public string FindDescription(int errorCode)
        {
            return AppErrors.Where(x => x.ErrorCode == errorCode).FirstOrDefault().ErrorDescription;
        }

        /// <summary>
        /// Includes a list of all available errors in the app, for all operations.
        /// </summary>
        /// <returns>A list of all possible errors (code-description).</returns>
        public static List<AppError> InitializeErrorList()
        {
            List<AppError> errorList = new List<AppError>();

            // Success code
            errorList.Add(new AppError
            {
                ErrorCode = 0,
                ErrorDescription = "Success!"
            });

            // Error codes
            errorList.Add(new AppError
            {
                ErrorCode = 1,
                ErrorDescription = "Error - New account cannot be created: This username already exists. Try changing your username!"
            });
            errorList.Add(new AppError
            {
                ErrorCode = 2,
                ErrorDescription = "Error - New account cannot be created: An account is already registered for this e-mail address."
            });
            errorList.Add(new AppError
            {
                ErrorCode = 3,
                ErrorDescription = "Error - Cannot login: Invalid credentials. Make sure your username and password are correct."
            });
            errorList.Add(new AppError
            {
                ErrorCode = 4,
                ErrorDescription = "Error - Cannot login: Your account is banned. Contract our support department for more details."
            });
            errorList.Add(new AppError
            {
                ErrorCode = 5,
                ErrorDescription = "Error - Cannot login: Authentication cookie is wrong or has expired."
            });
            errorList.Add(new AppError // Blanket error for exceptions
            {
                ErrorCode = 6,
                ErrorDescription = "Error - Something went wrong. Please try again."
            });

            // Return error list
            return errorList;
        }
    }
}
