namespace LogicLayer.Data.ApiObjects.WeatherAPI
{
    /// <summary>
    /// Weather condition overview (property of WeatherData object).
    /// </summary>
    public class WeatherCondition
    {
        public string text { get; set; }
        public string icon { get; set; }
        public int code { get; set; }
    }
}
