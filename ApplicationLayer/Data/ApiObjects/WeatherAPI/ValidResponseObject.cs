namespace ApplicationLayer.Data.ApiObjects.WeatherAPI
{
    /// <summary>
    /// Response object for successful weather API calls.
    /// </summary>
    internal class ValidResponseObject
    {
        public WeatherLocation location { get; set; }

        public WeatherData current { get; set; }
    }
}
