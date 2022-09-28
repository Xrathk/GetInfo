using LogicLayer.Data.ApiObjects.NewsAPI.Constants;

namespace LogicLayer.Data.ApiObjects.NewsAPI.Models
{
    /// <summary>
    /// List of articles returned from successful NewsAPI requests.
    /// </summary>
    public class ArticlesResult
    {
        public StatusesNewsApi Status { get; set; }
        public Error Error { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }
    }
}
