namespace LogicLayer.Data.ApiObjects.NewsAPI.Constants
{
    /// <summary>
    /// Sorting types for NewsAPI requests
    /// </summary>
    public enum SortBys
    {
        /// <summary>
        /// Sort by publisher popularity
        /// </summary>
        Popularity,
        /// <summary>
        /// Sort by article publish date (newest first)
        /// </summary>
        PublishedAt,
        /// <summary>
        /// Sort by relevancy to the Q param
        /// </summary>
        Relevancy
    }
}
