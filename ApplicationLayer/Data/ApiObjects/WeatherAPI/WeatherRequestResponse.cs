namespace ApplicationLayer.Data.ApiObjects.WeatherAPI
{
    /// <summary>
    /// Response object for weather requests.
    /// </summary>
    public class WeatherResponseObject
    {
        public System.Net.HttpStatusCode responseStatus{ get; set; }

        public WeatherData data { get; set; }
    }
}
