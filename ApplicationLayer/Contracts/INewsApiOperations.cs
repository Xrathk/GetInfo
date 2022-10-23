using ApplicationLayer.Data.ApiObjects.NewsAPI.Models;

namespace ApplicationLayer.Contracts
{
    /// <summary>
    /// Interface for the news operations service.
    /// </summary>
    public interface INewsApiOperations
    {
        Task<ArticlesResult> GetNewsData(int appUserId, string keyword);
        Task UpdateNewsRequestTable(string keyword, int results, DateTime currentTimestamp);
        Task UpdateUserRequests(int appUserId, DateTime currentTimestamp);
    }
}