using LogicLayer.Data.ApiObjects.NewsAPI.Constants;

namespace LogicLayer.Data.ApiObjects.NewsAPI.Models
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
