namespace LogicLayer.Data.ApiObjects.WeatherAPI
{
    /// <summary>
    /// Response object for unsuccessful weather API calls.
    /// </summary>
    internal class InvalidResponseObject
    {
        public WeatherError error { get; set; }
    }
}
