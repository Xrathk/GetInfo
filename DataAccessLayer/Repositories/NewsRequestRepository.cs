using DataAccessLayer.Contacts;
using DataAccessLayer.Data;
using DomainLayer.Entities;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// News request repository class.
    /// </summary>
    public class RepositoryNewsRequest : IRepositoryNewsRequest
    {
        private readonly GetInfoDbContext dbContext;
        private readonly ILogger _logger;

        /// <summary>
        /// News request repository constructor.
        /// </summary>
        /// <param name="_dbContext">Database context</param>
        /// <param name="logger">Logger component</param>
        public RepositoryNewsRequest(GetInfoDbContext _dbContext, ILogger<NewsRequest> logger)
        {
            dbContext = _dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Add news request to database.
        /// </summary>
        /// <param name="newsrequest">New news request to be added</param>
        /// <returns></returns>
        public async Task<NewsRequest> Create(NewsRequest newsrequest)
        {
            try
            {
                if (newsrequest != null)
                {
                    var obj = dbContext.Add(newsrequest);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation("News request data object initiated successfully:\n Keyword: {keyword}, Results fetched: {results}", obj.Entity.Keyword, obj.Entity.TimesRequested);
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not store new news request to database. Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Delete a news request from the database.
        /// </summary>
        /// <param name="newsrequest">News request to be deleted</param>
        public void Delete(NewsRequest newsrequest)
        {
            try
            {
                if (newsrequest != null)
                {
                    var obj = dbContext.Remove(newsrequest);
                    if (obj != null)
                    {
                        dbContext.SaveChangesAsync();
                        _logger.LogInformation("News request object deleted successfully from database:\n Request ID:{id}, Keyword: {keyword}", newsrequest.Id, newsrequest.Keyword);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not delete news request data with ID \"{requestId}\" from database. Exception: {Ex}", newsrequest.Id, Ex);
            }
        }

        /// <summary>
        /// Retrieves all news requests from database.
        /// </summary>
        /// <returns>All news requests.</returns>
        public IEnumerable<NewsRequest> GetAll()
        {
            try
            {
                var obj = dbContext.NewsRequests.ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve news requests from database. Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a news request by its Id.
        /// </summary>
        /// <param name="Id">The news request ID</param>
        /// <returns>The news request whom the ID belongs to.</returns>
        public NewsRequest GetById(int Id)
        {
            try
            {
                var Obj = dbContext.NewsRequests.FirstOrDefault(x => x.Id == Id);
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve news request with ID \"{objId}\" from database. Exception: {Ex}", Id, Ex);
                return null;
            }
        }

        /// <summary>
        /// Update news request info.
        /// </summary>
        /// <param name="newsrequest">News request to be updated</param>
        public void Update(NewsRequest newsrequest)
        {
            try
            {
                if (newsrequest != null)
                {
                    var obj = dbContext.Update(newsrequest);
                    if (obj != null)
                    {
                        dbContext.SaveChanges();
                        _logger.LogInformation("News request data object updated successfully:\n Keyword: {keyword}, Total requests: {totalRequests}, Average results per request: {avgResults}", obj.Entity.Keyword, obj.Entity.TimesRequested, obj.Entity.AverageResults);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not update news request info with ID \"{objId}\". Exception: {Ex}", newsrequest.Id, Ex);
            }
        }
    }
}
