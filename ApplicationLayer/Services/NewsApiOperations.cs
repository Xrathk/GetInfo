using Microsoft.Extensions.Logging;
using DataAccessLayer.Contacts;
using DomainLayer.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApplicationLayer.Data.ApiObjects.NewsAPI.Constants;
using ApplicationLayer.Data.ApiObjects.NewsAPI.Models;
using ApplicationLayer.Contracts;

namespace ApplicationLayer.Services
{
    /// <summary>
    /// Class that handles news requests and all database operations pertaining to them.
    /// Implemented endpoints: /everything
    /// Must be implemented in the future: /topheadlines
    /// </summary>
    public class NewsApiOperations : INewsApiOperations
    {
        // Fields
        private readonly ILogger<AccountOperations> Logger;
        private readonly IConfiguration configuration;
        // DAL repos
        public readonly IRepositoryAppUserRequests _userReqRepo;
        public readonly IRepositoryNewsRequest _newsReqRepo;

        // API Key
        public string NewsAPIKey;

        /// <summary>
        /// Service constuctor.
        /// </summary>
        /// <param name="logger">Logger component</param>
        public NewsApiOperations(
            ILogger<AccountOperations> logger,
            IConfiguration _configuration,
            IRepositoryAppUserRequests userReqRepo,
            IRepositoryNewsRequest newsReqRepo
        )
        {
            Logger = logger;
            configuration = _configuration;
            _userReqRepo = userReqRepo;
            _newsReqRepo = newsReqRepo;

            // Fetch news API key from configuration
            NewsAPIKey = configuration.GetValue<string>("NewsApiKey"); // From configuration

        }


        /// <summary>
        /// Gets news DATA via the open NewsApi.
        /// </summary>
        /// <param name="appUserId">The user's ID</param>
        /// <param name="keyword">The searched keyword</param>
        /// <returns>List with news data objects</returns>
        public async Task<ArticlesResult> GetNewsData(int appUserId, string keyword)
        {
            // Formulating request and getting output
            var requestId = UtilityMethods.BackendUtilityMethods.GetRandomAlphanumericString(16);
            Logger.LogInformation("A user has requested news data.\n" +
                "Request ID:{reqId}, User ID: {userId}, Keyword: {keyword}", requestId, appUserId, keyword);

            // Perform request
            var articlesResult = new ArticlesResult(); // Return object from API request

            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("GET"), "http://newsapi.org/v2/everything?q=" + keyword + "&language=en" + "&apiKey=" + NewsAPIKey);
            httpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            httpClient.DefaultRequestHeaders.Add("x-api-key", NewsAPIKey);
            var response = await httpClient.SendAsync(request);

            // Get status
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content.ReadAsStringAsync().Result); // Deserialize
                articlesResult.Status = apiResponse.Status;
                if (articlesResult.Status == StatusesNewsApi.Ok)
                {
                    articlesResult.TotalResults = apiResponse.TotalResults;
                    articlesResult.Articles = apiResponse.Articles;
                }

                // Return results based on status code
                if (articlesResult.Status == StatusesNewsApi.Ok)
                {
                    DateTime currentTime = DateTime.Now; // Current timestamp
                    Logger.LogInformation("Request with ID \"{requestID}\" has returned the status code \"{statusCode}\".\n" +
                        "Keyword: {location}, Results fetched: {currentNews}", 
                        requestId, articlesResult.Status, keyword, articlesResult.TotalResults); // Logging 

                    // Update news and user requests table
                    try
                    {
                        await UpdateNewsRequestTable(keyword, articlesResult.TotalResults, currentTime);
                    }
                    catch (Exception Ex)
                    {
                        Logger.LogError("Could not store news request data.\n" +
                            "Keyword: {keyword}, Exception: {Exception}", keyword, Ex);
                    }
                    try
                    {
                        await UpdateUserRequests(appUserId, currentTime);
                    }
                    catch (Exception Ex)
                    {
                        Logger.LogError("Could not store news request data for user.\n" +
                            "UserId: {appUserId}, Exception: {Exception}", appUserId, Ex);
                    }

                }
                else
                {
                    ErrorCodesNewsAPI errorCode = ErrorCodesNewsAPI.UnknownError;
                    try
                    {
                        errorCode = apiResponse.Code;
                    }
                    catch (Exception Ex)
                    {
                        Logger.LogError("Could not store news request data for user.\n" +
                            "UserId: {appUserId}, Exception: {Exception}", appUserId, Ex);
                    }

                    articlesResult.Error = new Error
                    {
                        Code = errorCode,
                        Message = apiResponse.Message
                    };
                }

                // Return response object
                return articlesResult;
            }
            else // If request unsuccessful, print appropriate error message.
            {
                articlesResult.Status = StatusesNewsApi.Error;
                articlesResult.Error = new Error
                {
                    Code = ErrorCodesNewsAPI.UnexpectedError,
                    Message = "The API returned an empty response. Are you connected to the internet?"
                };

                Logger.LogError("Could not fetch news data about keyword.\n" +
                    "Keyword: {keyword}, Error Code: {errorCode}", keyword, articlesResult.Status); // Logging 
                                                                                                                                                            // Return error object
                return articlesResult;
            }


        }

        /// <summary>
        /// Updates news request table (per keyword).
        /// </summary>
        /// <param name="keyword">Name of keyword requested</param>
        /// <param name="results">Results fetched from request</param>
        /// <param name="currentTimestamp">Timestamp of request</param>
        public async Task UpdateNewsRequestTable(string keyword, int results, DateTime currentTimestamp)
        {
            // Fetch keywords
            var keywords = _newsReqRepo.GetAll();

            // Check if keyword exists
            var currentKeywordData = keywords.Where(x => x.Keyword.ToLower().Equals(keyword.ToLower())).FirstOrDefault();

            // If yes, update object accordingly
            if (currentKeywordData != null)
            {
                var newAverage = (currentKeywordData.AverageResults * currentKeywordData.TimesRequested + results) / (currentKeywordData.TimesRequested + 1);
                currentKeywordData.TimesRequested++;
                currentKeywordData.LastRequested = currentTimestamp;
                currentKeywordData.AverageResults = newAverage;

                // Update via repo
                _newsReqRepo.Update(currentKeywordData);
                Logger.LogInformation("News request data for keyword updated.\n " +
                    "Keyword: {keyword}, Times Keyword Requested: {timesRequested}, New Result Average per Request: {newAverage}", 
                    currentKeywordData.Keyword, currentKeywordData.TimesRequested, newAverage);

            }
            else // If no, create new object
            {
                var newKeywordData = new NewsRequest
                {
                    Keyword = keyword,
                    TimesRequested = 1,
                    AverageResults = results,
                    LastRequested = currentTimestamp
                };

                // Create via repo
                await _newsReqRepo.Create(newKeywordData);
                Logger.LogInformation("News request data for keyword keyword created. " +
                    "Keyword: {keyword}, First request recorded at: {timestamp}", newKeywordData.Keyword, newKeywordData.LastRequested);

            }

        }

        /// <summary>
        /// Adds the news request to the user's overall request summary.
        /// </summary>
        /// <param name="appUserId">The app user ID</param>
        /// <param name="currentTimestamp">The timestamp of the last news request</param>
        public async Task UpdateUserRequests(int appUserId, DateTime currentTimestamp)
        {
            // Fetch requests
            var requests = _userReqRepo.GetAll();

            // Check if user
            var currentUserData = requests.Where(x => x.AppUserId == appUserId).FirstOrDefault();

            // If yes, update object accordingly
            if (currentUserData != null)
            {
                currentUserData.NewsRequests++;
                currentUserData.LastNewsRequest = currentTimestamp;

                // Update via repo
                _userReqRepo.Update(currentUserData);
                Logger.LogInformation("User request data for user updated.\n" +
                    "User ID: {userId}, Total news requests: {totalRequests}", appUserId, currentUserData.NewsRequests);

            }
            else // If no, create new object
            {
                var newUserData = new AppUserRequests
                {
                    NewsRequests = 1,
                    LastNewsRequest = currentTimestamp,
                    AppUserId = appUserId
                };

                // Create via repo
                await _userReqRepo.Create(newUserData);
                Logger.LogInformation("User request data for user created.\n" +
                    "User ID: {userId}, First news request recorded at: {timestamp}", appUserId, newUserData.LastNewsRequest);

            }

        }



    }

}
