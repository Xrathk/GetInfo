using ApplicationLayer.Data.ApiObjects.NewsAPI.Constants;

namespace ApplicationLayer.Data.ApiObjects.NewsAPI.Models
{
    /// <summary>
    /// NewsAPI API respospe properties.
    /// </summary>
    internal class ApiResponse
    {
        public StatusesNewsApi Status { get; set; }
        public ErrorCodesNewsAPI Code { get; set; }
        public string Message { get; set; }
        public List<Article> Articles { get; set; }
        public int TotalResults { get; set; }
    }
}
