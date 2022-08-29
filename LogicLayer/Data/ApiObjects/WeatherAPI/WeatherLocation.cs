namespace LogicLayer.Data.ApiObjects.WeatherAPI
{
    /// <summary>
    /// Location data about an enquired city, for weather API requests.
    /// </summary>
    internal class WeatherLocation
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string tz_id { get; set; }
        public int localtime_epoch { get; set; }
        public string localtime { get; set; }
    }
}
