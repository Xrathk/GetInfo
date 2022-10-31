using ApplicationLayer.Data.ApiObjects.NewsAPI.Models;

namespace ApplicationLayer.Contracts
{
    /// <summary>
    /// Interface for the news operations service.
    /// </summary>
    public interface INewsApiOperations
    {
        /// <summary>
        /// Gets news DATA via the open NewsApi.
        /// </summary>
        /// <param name="appUserId">The user's ID</param>
        /// <param name="keyword">The searched keyword</param>
        /// <returns>List with news data objects</returns>
        Task<ArticlesResult> GetNewsData(int appUserId, string keyword);

        /// <summary>
        /// Updates news request table (per keyword).
        /// </summary>
        /// <param name="keyword">Name of keyword requested</param>
        /// <param name="results">Results fetched from request</param>
        /// <param name="currentTimestamp">Timestamp of request</param>
        Task UpdateNewsRequestTable(string keyword, int results, DateTime currentTimestamp);

        /// <summary>
        /// Adds the news request to the user's overall request summary.
        /// </summary>
        /// <param name="appUserId">The app user ID</param>
        /// <param name="currentTimestamp">The timestamp of the last news request</param>
        Task UpdateUserRequests(int appUserId, DateTime currentTimestamp);
    }
}