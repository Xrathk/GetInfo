namespace ApplicationLayer.Data.ApiObjects.WeatherAPI
{
    /// <summary>
    /// Weather API error data.
    /// </summary>
    public class WeatherError
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}