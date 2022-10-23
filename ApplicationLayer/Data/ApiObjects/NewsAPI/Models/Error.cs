using ApplicationLayer.Data.ApiObjects.NewsAPI.Constants;

namespace ApplicationLayer.Data.ApiObjects.NewsAPI.Models
{
    /// <summary>
    /// Error codes and appropriate messages.
    /// </summary>
    public class Error
    {
        public ErrorCodesNewsAPI Code { get; set; }
        public string Message { get; set; }
    }
}
